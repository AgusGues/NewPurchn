using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.ModalDialog
{
    public partial class ReasonCancelWO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["AlasanCancel"] = null;
                Session["AlasanBatal"] = null;
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            txtAlasanCancel.Text = string.Empty;
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Session["AlasanCancel"] = txtAlasanCancel.Text;
            Session["AlasanBatal"] = txtAlasanCancel.Text;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
    }
}