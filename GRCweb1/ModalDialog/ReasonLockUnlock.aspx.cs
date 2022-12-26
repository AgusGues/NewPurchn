using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.ModalDialog
{
    public partial class ReasonLockUnlock : System.Web.UI.Page
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
            //Response.Write("<script language='javascript'>window.opener.document.getElementById('btnDelete').click();</script>");
            Response.Write("<script language='javascript'>window.close();</script>");
        }
    }
}