using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.ModalDialog
{
    public partial class PopUpDialog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Keterangan"] = null;
        }
        protected void simpan_Click(object sender, EventArgs e)
        {
            Session["Keterangan"] = txtKeterangan.Text;
            Response.Write("<script language='javascript'>window.close();</script>");
        }
    }
}