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

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormWarnOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<RekapWarningOrderNf.ParamTypeItem> ListTypeItem()
        {
            List<RekapWarningOrderNf.ParamTypeItem> list = new List<RekapWarningOrderNf.ParamTypeItem>();
            list = RekapWarningOrderNfFacade.GetListTypeItem();
            return list;
        }

        [WebMethod]
        public static List<RekapWarningOrderNf.ParamData> ListData(int ItemId, string Tanggal)
        {
            string periodeAwal = DateTime.Parse(Tanggal).ToString("yyyyMMdd");
            string periodeAkhir = DateTime.Parse(Tanggal).ToString("yyyyMMdd");
            DateTime tgl1 = new DateTime(DateTime.Parse(Tanggal).Year, DateTime.Parse(Tanggal).Month, 1);
            DateTime tgl2 = new DateTime();
            if (DateTime.Parse(Tanggal).Day == 1)
            {
                tgl2 = new DateTime(DateTime.Parse(Tanggal).Year,
                    DateTime.Parse(Tanggal).Month, 1);
            }
            else
            {
                tgl2 = new DateTime(DateTime.Parse(Tanggal).Year,
                    DateTime.Parse(Tanggal).Month, DateTime.Parse(Tanggal).Day - 1);
            }

            string tglAwal = tgl1.ToString("yyyyMMdd");
            string tglAkhir = tgl2.ToString("yyyyMMdd");
            string txPeriodeAwal = Tanggal;
            string txPeriodeAkhir = Tanggal;
            string strError = string.Empty;
            int thn = DateTime.Parse(Tanggal).Year;
            int blnLalu = DateTime.Parse(Tanggal).Month;

            string ketBlnLalu = string.Empty;
            if (blnLalu - 1 == 0) { ketBlnLalu = "DesQty"; thn = thn - 1; }
            else if (blnLalu - 1 == 1) { ketBlnLalu = "JanQty"; }
            else if (blnLalu - 1 == 2) { ketBlnLalu = "FebQty"; }
            else if (blnLalu - 1 == 3) { ketBlnLalu = "MarQty"; }
            else if (blnLalu - 1 == 4) { ketBlnLalu = "AprQty"; }
            else if (blnLalu - 1 == 5) { ketBlnLalu = "MeiQty"; }
            else if (blnLalu - 1 == 6) { ketBlnLalu = "JunQty"; }
            else if (blnLalu - 1 == 7) { ketBlnLalu = "JulQty"; }
            else if (blnLalu - 1 == 8) { ketBlnLalu = "AguQty"; }
            else if (blnLalu - 1 == 9) { ketBlnLalu = "SepQty"; }
            else if (blnLalu - 1 == 10) { ketBlnLalu = "OktQty"; }
            else if (blnLalu - 1 == 11) { ketBlnLalu = "NovQty"; }

            int groupID = Convert.ToInt32(ItemId);
            string strQuery = string.Empty;
            if (DateTime.Parse(Tanggal).Day == 1)
            {
                strQuery = RekapWarningOrderNfFacade.ViewWarningOrdera(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID);
            }
            else
            {
                strQuery = RekapWarningOrderNfFacade.ViewWarningOrder(ketBlnLalu, thn, tglAwal, tglAkhir, periodeAwal, periodeAkhir, groupID);
            }

            List<RekapWarningOrderNf.ParamData> list = new List<RekapWarningOrderNf.ParamData>();
            list = RekapWarningOrderNfFacade.GetListData(strQuery);
            return list;
        }
    }
}