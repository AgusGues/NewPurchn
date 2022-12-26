using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ListSPPnew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["AlasanCancel"] = null;
                LoadSPP(Request.QueryString["approve"].ToString());
            }
        }
        protected void btnFormPO_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormPOPurchn.aspx");
        }
        private void LoadSPP(string aproval)
        {
            SPPFacade sppFacade = new SPPFacade();
            ArrayList arrSPPDetail = new ArrayList();
            string Group = (((Users)Session["Users"]).UserName == "admin") ? string.Empty : " and s.GroupID =" + ((Users)Session["Users"]).GroupID.ToString();
            Session["status"] = " and status=0 and Approval=2) as x where x.Detail >0 order by x.ApproveDate3";
            arrSPPDetail = sppFacade.ListSPP(aproval);
            if (sppFacade.Error == string.Empty)
            {
                lstSPP.DataSource = arrSPPDetail;
                lstSPP.DataBind();
            }
        }
        protected void lstSPP_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            SPP objSPP = (SPP)e.Item.DataItem;
            ArrayList arrSPP = new ArrayList();
            arrSPP = new SPPFacade().ListDetailSPP(objSPP.ID);
            if (arrSPP.Count > 0)
            {
                var lst = e.Item.FindControl("lstDetailSPP") as Repeater;
                if (lst != null)
                {
                    lst.DataSource = arrSPP;
                    lst.DataBind();
                }
            }
            else
            {
                e.Item.Visible = false;

            }
        }
        protected void SPP_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string noSPP = e.CommandArgument.ToString();
            if (e.CommandName.ToString() == "add")
            {
                Response.Redirect("FormPOPurchn.aspx?NoSPP=" + noSPP);
            }
        }
        protected void lsd_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //Image pnd = (Image)e.Item.FindControl("pndSPP");
            //Image cls = (Image)e.Item.FindControl("clsSPP");
            //SPPDetail SPD = (SPPDetail)e.Item.DataItem;
            //if (SPD.Quantity > SPD.QtyPO)
            //{
            //    pnd.Visible = (SPD.PendingPO > 0) ? false : true;
            //    cls.Visible = (SPD.PendingPO > 0) ? true : false;
            //}
            //else
            //{
            //    pnd.Visible = false;
            //    cls.Visible = (SPD.PendingPO > 0) ? true : false;
            //}
        }
        protected void lsd_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string act = e.CommandName.ToString();
            if (act == "unlock")
            {
                int ID = int.Parse(e.CommandArgument.ToString());
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Info", "PendingPO('" + ID + "')", true);
            }
            else if (act == "lock")
            {
                int ID = int.Parse(e.CommandArgument.ToString());
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Info", "ReleaseSPP('" + ID + "')", true);
            }
        }
    }
}