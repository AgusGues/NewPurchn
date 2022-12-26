using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Domain;
using System.Data.SqlClient;
using BusinessFacade;
using System.Collections;
using DataAccessLayer;
using System.Text;
using BusinessFacade.GL;
using System.Web.Services;
using Newtonsoft.Json;

namespace GRCweb1.Modul.SasaranMutu
{
    public partial class PencapaianSarmut  : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {

        }

        [WebMethod]
        public static string PlantKrwg(int Tahun)
        {
            List<SPD_Sarmut> Partno = new List<SPD_Sarmut>();
            Partno = SarmutFacade.GetSarmutKrwg(Tahun);
            return JsonConvert.SerializeObject(Partno);
        }

        [WebMethod]
        public static string PlantCtrp(int Tahun)
        {
            List<SPD_Sarmut> Partno = new List<SPD_Sarmut>();
            Partno = SarmutFacade.GetSarmutCtrp(Tahun);
            return JsonConvert.SerializeObject(Partno);
        }

        [WebMethod]
        public static string PlantJmng(int Tahun)
        {
            List<SPD_Sarmut> Partno = new List<SPD_Sarmut>();
            Partno = SarmutFacade.GetSarmutJmng(Tahun);
            return JsonConvert.SerializeObject(Partno);
        }

    }
}
