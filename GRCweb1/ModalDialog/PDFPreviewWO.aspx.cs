using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using System.Configuration;

namespace GRCweb1.ModalDialog
{
    public partial class PDFPreviewWO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string IDLampiran = Request.QueryString["wrk"].ToString();
                Preview(IDLampiran);
            }
        }

        private void Preview(string idLampiran)
        {

            string embed = "<object data=\"{0}{1}\"type=\"application/pdf\" width=\"100%\" height=\"550px\" >";
            //string embed = "<object  data="path/to/file.pdf?#zoom=85&scrollbar=0&toolbar=0&navpanes=0" type="application/pdf">;
            embed += "</object>";
            pdfView.Text = string.Format(embed, ResolveUrl("~/ModalDialog/PreviewHandlerWO.ashx?wrk="), idLampiran);
        }
    }
}