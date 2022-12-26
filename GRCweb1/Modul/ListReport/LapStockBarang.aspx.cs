using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using GRCweb1.Models;
using Newtonsoft.Json;
using Factory;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Collections;

namespace GRCweb1.Modul.ListReport
{
    public partial class LapStockBarang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<LapStockBarangNf.ParamTypeItem> ListTypeItem()
        {
            List<LapStockBarangNf.ParamTypeItem> list = new List<LapStockBarangNf.ParamTypeItem>();
            list = LapStockBarangNfFacade.GetListTypeItem();
            return list;
        }

        [WebMethod]
        public static List<LapStockBarangNf.ParamGroupItem> ListGroupItem()
        {
            List<LapStockBarangNf.ParamGroupItem> list = new List<LapStockBarangNf.ParamGroupItem>();
            list = LapStockBarangNfFacade.GetListGroupItem();
            return list;
        }

        [WebMethod]
        public static List<LapStockBarangNf.ParamData> ListData(int TypeItem, int GroupItem, int TypeStock, int TypeStatus)
        {
            string allQuery = string.Empty;
            string strAktif = string.Empty;
            string strStock = string.Empty;
            int valstock = TypeStock;
            strStock = "Stock dan Non Stock";
            if (valstock == 1) { strStock = "Stock"; }
            if (valstock == 2) { strStock = "Non Stock"; }
            int valgroup = GroupItem;
            int valaktif = TypeStatus;
            strAktif = "Aktif dan Non Aktif";
            if (valaktif == 1) { strAktif = "Aktif"; }
            if (valaktif == 2) { strAktif = "Non Aktif"; }
            allQuery = LapStockBarangNfFacade.ViewLapBarang(TypeItem, valstock, valgroup, valaktif);

            List<LapStockBarangNf.ParamData> list = new List<LapStockBarangNf.ParamData>();
            list = LapStockBarangNfFacade.GetListData(allQuery);
            return list;
        }
    }
}