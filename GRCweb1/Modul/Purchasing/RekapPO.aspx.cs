using BusinessFacade;
using CrystalDecisions.CrystalReports.Engine;
using Dapper;
using DataAccessLayer;
using Domain;
using Factory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using Factory.DataSets;
using System.Configuration;
using System.Data;
using System.Web.Hosting;
using System.Web.UI;

namespace GRCweb1.Modul.Purchasing
{
    public partial class RekapPO : System.Web.UI.Page
    {
        private ReportDocument objRpt1;
        protected void Page_Init(object sender, EventArgs e)
        {

            
            //string id = Request.Form["idrekap"];
            //string awal = Request.Form["tglawal"];
            //string akhir = Request.Form["tglakhir"];
            //string aw;
            //string ak;
            //DateTime today = DateTime.Today;
            //if (awal == null || akhir == null)
            //{

            //    aw = today.ToString("yyyyMMdd");
            //    ak = today.ToString("yyyyMMdd");
            //}
            //else
            //{
            //    string a = awal.Substring(0, 2);
            //    string b = awal.Substring(3, 2);
            //    string c = awal.Substring(6, 4);
            //    string d = akhir.Substring(0, 2);
            //    string f = akhir.Substring(3, 2);
            //    string g = akhir.Substring(6, 4);
            //    aw = c + b + a;
            //    ak = g + f + d;
            //}
            if (!IsPostBack)
            {
                //ReportDocument crystalReport = new ReportDocument();
                //crystalReport.Load(Server.MapPath("~/Modul/CrystalReport/RekapPO.rpt"));
                //dsPO dsPersonInformations = GetRekapPO(aw, ak);
                //crystalReport.SetDataSource(dsPersonInformations);
                //crRekapPo.ReportSource = crystalReport;
                //Session["ReportDocument"] = crystalReport;

            }
            else
            {
                //ReportDocument doc = (ReportDocument)Session["ReportDocument"];
                //doc.Load(Server.MapPath("~/Modul/CrystalReport/RekapPO.rpt"));
                //dsPO dsPersonInformations = GetRekapPO(aw, ak);
                //doc.SetDataSource(dsPersonInformations);
                //crRekapPo.ReportSource = doc;
                ////ClientScript.RegisterStartupScript(this.GetType(), "Pop", "OpenModal();", true);
                ////ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
               
            }
            //ViewRekapPO();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (objRpt1 != null)
            {
                objRpt1.Close();
                objRpt1.Dispose();
            }
        }
        protected void RekapDetail(object sender, EventArgs e)
        {

            string id = Request.Form["idrekap"];
            string awal = Request.Form["tglawal"];
            string akhir = Request.Form["tglakhir"];
            string idpo = Request.Form["idrekap"];
            string aw;
            string ak;
            DateTime today = DateTime.Today;
            if (awal == null || akhir == null)
            {

                aw = today.ToString("yyyyMMdd");
                ak = today.ToString("yyyyMMdd");
            }
            else
            {
                string a = awal.Substring(0, 2);
                string b = awal.Substring(3, 2);
                string c = awal.Substring(6, 4);
                string d = akhir.Substring(0, 2);
                string f = akhir.Substring(3, 2);
                string g = akhir.Substring(6, 4);
                aw = c + b + a;
                ak = g + f + d;
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Modul/CrystalReport/RptRekapPO.rpt"));

            dsPO dsPersonInformations = GetRekapPO(aw, ak);
            crystalReport.SetDataSource(dsPersonInformations);
            crRekapPo.Height = 100;
            crRekapPo.ReportSource = crystalReport;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "OpenModal();", true);
        }

        [WebMethod]
        public static string GetListRekapPO(string awal, string akhir)
        {
            List<Domain.RekapPO> RekapPo = new List<Domain.RekapPO>();
            Users users = (Users)HttpContext.Current.Session["Users"];
            int viewprice = users.ViewPrice;
            RekapPo = POPurchnFacade.GetRekapPO(awal, akhir, viewprice);
            return JsonConvert.SerializeObject(RekapPo);
        }


        [WebMethod]
        public static List<Domain.RekapPO> GetDetailPO(string nopo)
        {
            Users users = (Users)HttpContext.Current.Session["Users"];
            int depo = users.DepoID;
            List<Domain.RekapPO> RekapPo = new List<Domain.RekapPO>();
            RekapPo = POPurchnFacade.GetDetailPO(nopo,depo);
            
            return RekapPo;
        }

        private static dsPO GetRekapPO(string awal, string akhir)
        {
            string conString = ConfigurationManager.ConnectionStrings["GRCBoard"].ConnectionString;
            SqlCommand cmd = new SqlCommand("SELECT POPurchn.ID,POPurchn.NoPO, SuppPurch.SupplierName,case when POPurchn.Approval=0 then 'Open' when POPurchn.Approval=1 then 'Head' when POPurchn.Approval=2 then 'Manager Corp.' end Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, " +
                            "CASE POPurchnDetail.ItemTypeID WHEN 1 THEN (SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) ELSE CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID))  THEN(select ItemName from Biaya where Biaya.ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+  " +
                            "(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID and  SPPDetail.SPPID=POPurchnDetail.SPPID) ELSE  (select ItemName from biaya where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END  END AS ItemName, SPP.NoSPP,  POPurchnDetail.Qty,  UOM.UOMCode Satuan, POPurchn.Disc, 0 Price,0 as Total, POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc,POPurchn.Cetak FROM POPurchn INNER JOIN POPurchnDetail ON POPurchn.ID = POPurchnDetail.POID INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID INNER JOIN SuppPurch ON POPurchn.SupplierID = SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and convert(varchar,POPurchn.POPurchndate,112)>='" + awal + "' and  convert(varchar,POPurchn.POPurchndate,112)<='" + akhir + "' and POPurchnDetail.Status >-1 order by groupdesc,POPurchn.NoPO");
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    sda.SelectCommand = cmd;
                    using (dsPO dspo = new dsPO())
                    {
                        sda.Fill(dspo, "RekapPO");
                        return dspo;
                    }
                }
            }

        }

        //protected  void ViewRekapPO()
        //{
        //    string id = Request.Form["idrekap"];
        //    string awal = Request.Form["tglawal"];
        //    string akhir = Request.Form["tglakhir"];
        //    string aw;
        //    string ak;
        //    DateTime today = DateTime.Today;
        //    if (awal == null || akhir == null)
        //    {

        //        aw = today.ToString("yyyyMMdd");
        //        ak = today.ToString("yyyyMMdd");
        //    }
        //    else
        //    {
        //        string a = awal.Substring(0, 2);
        //        string b = awal.Substring(3, 2);
        //        string c = awal.Substring(6, 4);
        //        string d = akhir.Substring(0, 2);
        //        string f = akhir.Substring(3, 2);
        //        string g = akhir.Substring(6, 4);
        //        aw = c + b + a;
        //        ak = g + f + d;
        //    }
        //    SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
        //    //try
        //    //{
        //    string strsql = "SELECT POPurchn.ID,POPurchn.NoPO, SuppPurch.SupplierName,case when POPurchn.Approval = 0 then 'Open' when POPurchn.Approval = 1 then 'Head' " +
        //        "when POPurchn.Approval = 2 then 'Manager Corp.' end Approval, POPurchn.PPN,POPurchn.PPH, MataUang.Nama M_Uang, CASE POPurchnDetail.ItemTypeID WHEN 1 THEN " +
        //        "(SELECT ItemName FROM Inventory WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) WHEN 2 THEN (SELECT ItemName FROM Asset WHERE ID = POPurchnDetail.ItemID AND RowStatus > - 1) " +
        //        "ELSE CASE WHEN(isnull((Select ModifiedTime From BiayaNew where RowStatus=1),GETDATE())<=  (Select CreatedTime from SPP where SPP.ID=POPurchnDetail.SPPID))  " +
        //        "THEN(select ItemName from Biaya where Biaya.ID=POPurchnDetail.ItemID and Biaya.RowStatus>-1)+' - '+  " +
        //                    "(Select SPPDetail.Keterangan From SPPDetail where SPPDetail.ID=POPurchnDetail.SPPDetailID and  SPPDetail.SPPID=POPurchnDetail.SPPID) ELSE  (select ItemName from biaya " +
        //                    "where ID=POPurchnDetail.ItemID and biaya.RowStatus>-1) END  END AS ItemName, SPP.NoSPP,  POPurchnDetail.Qty,  UOM.UOMCode Satuan, POPurchn.Disc, 0 Price,0 as Total, " +
        //                    "POPurchnDetail.Price * POPurchnDetail.Qty AS TOT2, POPurchn.POPurchnDate, GROUPsPURCHN.groupdescription as groupdesc,POPurchn.Cetak FROM POPurchn INNER JOIN POPurchnDetail " +
        //                    "ON POPurchn.ID = POPurchnDetail.POID INNER JOIN SPP ON POPurchnDetail.SPPID = SPP.ID INNER JOIN UOM ON POPurchnDetail.UOMID = UOM.ID INNER JOIN SuppPurch ON POPurchn.SupplierID =" +
        //                    " SuppPurch.ID INNER JOIN MataUang ON POPurchn.Crc = MataUang.ID INNER JOIN GROUPsPURCHN ON POPurchnDetail.groupid = GROUPsPURCHN.id where  POPurchn.status>-1 and " +
        //                    "convert(varchar,POPurchn.POPurchndate,112)>='"+ aw + "' and  convert(varchar,POPurchn.POPurchndate,112)<='"+ak+"' and POPurchnDetail.Status >-1 order by groupdesc," +
        //                    "POPurchn.NoPO";
        //    sqlCon.Open();
            
        //        SqlDataAdapter da = new SqlDataAdapter(strsql, sqlCon); da.SelectCommand.CommandTimeout = 0;
        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        objRpt1 = new ReportDocument();
        //        crRekapPo.DisplayToolbar = true;
        //        //crViewer.SeparatePages = false;
        //        Users users = (Users)Session["Users"];

        //        objRpt1.Load(this.Server.MapPath("~/Modul/Report/RptRekapPO.rpt"));
        //        objRpt1.SetDataSource(dt);
        //        crRekapPo.ReportSource = objRpt1;

        //        //SetCurrentValuesForParameterField(objRpt1, Session["drTgl"].ToString(), "drTanggal");

        //        //SetCurrentValuesForParameterField(objRpt1, Session["sdTgl"].ToString(), "sdTanggal");
        //    //}
        //    //catch
        //    //{

        //    //}
        //}


        protected void Button1_Click(object sender, EventArgs e)
        {
            PanelReport.Visible = false;
            PanelView.Visible = true;
        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {
           // ViewRekapPO();
            PanelReport.Visible = true;
            PanelView.Visible = false;
        }

    }
}