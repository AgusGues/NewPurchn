using BusinessFacade;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Purchasing
{
    public partial class HistorySPP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string GetHistPO(string strField, string strValue)
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            int viewprice = user.ViewPrice;
            List<HistSPP> objHist = new List<HistSPP>();

            if (strField == "POPurchnDetail.ItemID in(select ID from Inventory where ItemCode like" || strField == "popurchndetail.itemtypeid=1 and popurchndetail.itemid in (select id from inventory where itemname like" || strField == "popurchndetail.itemtypeid=2 and popurchndetail.itemid in (select id from asset where itemname like" || strField == "3")
            {
                if (viewprice > 0)
                {
                    if (viewprice == 1)
                    {
                        objHist = HistSPPFacade.ViewHistPO(strField, strValue, ")");
                        //return JsonConvert.SerializeObject(objHist);
                    }
                    else if (viewprice == 2)
                    {
                        objHist = HistSPPFacade.ViewHistPO2(strField, strValue, ")");
                    }
                }
                else
                {
                    objHist = HistSPPFacade.ViewHistPOByPrice0(strField, strValue, ")");
                }
            }
            else
            {
                if (viewprice > 0)
                {
                    if (viewprice == 1)
                    {
                        objHist = HistSPPFacade.ViewHistPO(strField, strValue, "");
                    }
                    else if (viewprice == 2)
                    {
                        objHist = HistSPPFacade.ViewHistPO2(strField, strValue, "");
                    }
                }
                else
                {
                    objHist = HistSPPFacade.ViewHistPOByPrice0(strField, strValue, "");
                }
            }
            return JsonConvert.SerializeObject(objHist);
        }
    }
}