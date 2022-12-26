using BusinessFacade;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Purchasing
{
    public partial class KasbonOSPemantauan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       
        [WebMethod]
        public static List<Kasbon> GetPIC()
        {
            List<Kasbon> pic = new List<Kasbon>();
            pic = KasbonFacade.RetrieveNew();
            //return JsonConvert.SerializeObject(pic);
            return pic;
        }
        [WebMethod]
        public static string Getkasbon(string drTgl, string sdTgl,string Criteria)
        {
            List<Kasbon> lapkasbon = new List<Kasbon>();
            lapkasbon = KasbonFacade.RetrieveOSKasbonNew(drTgl, sdTgl, Criteria);
            return JsonConvert.SerializeObject(lapkasbon);
        }
    }
}