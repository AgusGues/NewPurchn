using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using BusinessFacade;
using DataAccessLayer;
using Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormHistoryHargaBarang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            
        }

        [WebMethod]
        public static string GetHargaBarang()
        {
           List<HargaBarang> objHargaBarang = new List<HargaBarang>();
            objHargaBarang = HargaBarangFacade.RetrieveAll();
            return JsonConvert.SerializeObject(objHargaBarang);
        }
        [WebMethod]
        public static string GetHargaBarangByCriteria(string strField, string strValue)
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            int viewprice = user.ViewPrice;
            List<HargaBarang> objHargaBarang = new List<HargaBarang>();
            objHargaBarang = HargaBarangFacade.RetrieveByCriteria2(strField, strValue, viewprice);
            return JsonConvert.SerializeObject(objHargaBarang);
        }

    }
}