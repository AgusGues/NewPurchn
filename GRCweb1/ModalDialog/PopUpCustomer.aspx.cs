using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace GRCweb1.ModalDialog
{
    public partial class PopUpCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //protected void Page_Unload(object sender, EventArgs e)
        //{
        //    if (txtCustomerName.Text.Trim() != string.Empty)
        //    {
        //        Session["CustomerName"] = txtCustomerName.Text.Trim();
        //    }
        //    else
        //    {
        //        Session["CustomerName"] = null;
        //    }
        //}
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            if (txtCustomerName.Text.Trim() != string.Empty)
            {
                Session["CustomerName"] = txtCustomerName.Text.Trim();
                Response.Write("<script language='javascript'>window.close();</script>");
            }
            else
            {
                Session["CustomerName"] = null;
            }
        }
        protected void BtnClose_ServerClick(object sender, EventArgs e)
        {
            Session["CustomerName"] = null;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
    }
}