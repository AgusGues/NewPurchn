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
    public partial class RekapAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<RekapAssetNf.ParamData> ListData()
        {
            List<RekapAssetNf.ParamData> list = new List<RekapAssetNf.ParamData>();
            list = RekapAssetNfFacade.GetListData();
            return list;
        }
    }
}