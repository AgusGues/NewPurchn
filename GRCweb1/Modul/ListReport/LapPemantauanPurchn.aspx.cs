using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Domain;
using BusinessFacade;
using Newtonsoft.Json;
using System.Data;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapPemantauanPurchn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetLapPurchn(string drtgl, string sdtgl)
        {
            List<object> lappurchn = new List<object>();
            //List<ReportFacade> objHargaBarang = new List<ReportFacade>();
            //string lappurchn;
            lappurchn = ReportFacade.ViewLapPemantauanPurchn(drtgl, sdtgl, 0);
            return JsonConvert.SerializeObject(lappurchn);
        }

        [WebMethod]
        public static string GetHargaBarang()
        {
            List<HargaBarang> objHargaBarang = new List<HargaBarang>();
            objHargaBarang = HargaBarangFacade.RetrieveAll();
            return JsonConvert.SerializeObject(objHargaBarang);
        }
    }
}