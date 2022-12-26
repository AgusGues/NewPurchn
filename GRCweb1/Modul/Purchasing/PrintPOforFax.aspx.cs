using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;

namespace GRCweb1.Modul.Purchasing
{
    public partial class PrintPOforFax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {

            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            POPurchn pOPurchn = pOPurchnFacade.RetrieveByNo(txtNOPO.Text);
            Session["ID"] = pOPurchn.ID;

            if (pOPurchn.Approval < 3)
            {
                DisplayAJAXMessage(this, "Belum Di Approve Sampai Plant Manager..!");
                return;
            }
            Response.Redirect("../../Report/Report2.aspx?IdReport=POPurchn2");


        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}