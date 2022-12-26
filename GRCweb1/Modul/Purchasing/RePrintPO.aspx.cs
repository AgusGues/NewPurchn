using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    public partial class RePrintPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]

        public static int ReprintPO(string nopo, string alasanreprint)
        {
            POPurchn p = new POPurchn();
            p.NoPO = nopo;
            p.AlasanReprint = alasanreprint;
            
            int insert;

            POPurchnFacade pf = new POPurchnFacade();
            insert = pf.UpdateAlasanPrint(p);
            return insert;
        }
        [WebMethod]
        public static int CetakPOulang2(string nopo1)
        {
            POPurchn c = new POPurchn();
            c.NoPO = nopo1;

            int update;
            POPurchnFacade pd = new POPurchnFacade();
            update = pd.CetakUlangPO(c);

            return update;

        }

    }
}