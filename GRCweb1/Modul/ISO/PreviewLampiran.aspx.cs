using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Data.OleDb;
using Domain;
using BusinessFacade;
using System.Drawing.Imaging;
using AjaxControlToolkit;
using System.Web.Services;
using System.Web.Script.Services;

namespace GRCweb1.Modul.ISO
{
    public partial class PreviewLampiran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["TaskId"] != null && Convert.ToInt32(Session["TaskId"]) != 0 && Convert.ToString(Session["TaskId"]) != string.Empty)
                { loadJustPreview(Convert.ToInt32(Session["TaskId"])); }
            }
            CekI();
        }

        protected void btnNext_ServerClick(object sender, EventArgs e)
        {
            int i = 0;
            if (Session["i"] != null) { i = Convert.ToInt32(Session["i"]) + 1; }
            else { i = 1; }
            Session["i"] = i;
            CekI();
        }

        protected void btnPrev_ServerClick(object sender, EventArgs e)
        {
            int i = 0;
            if (Session["i"] != null) { i = Convert.ToInt32(Session["i"]) - 1; }
            else { i = 1; }
            Session["i"] = i;
            CekI();
        }

        private void CekI()
        {
            int i = 0;

            if (Session["i"] != null) { i = Convert.ToInt32(Session["i"]); }

            if (Convert.ToString(Session["TempIdLampiran"]) != string.Empty && (ArrayList)Session["ArrImgLampiran"] != null)
            {
                ArrayList arrImgLampiran = new ArrayList();
                arrImgLampiran = (ArrayList)Session["ArrImgLampiran"];

                int j = arrImgLampiran.Count;

                lblImage.Text = "Lampiran " + (i + 1).ToString();
                Image1.ImageUrl = "~/Resource_Web/Lampiran_iso/" + Convert.ToString(arrImgLampiran[i]);

                PanelPreview.Visible = true;

                if ((i + 1) < j) { btnNext.Enabled = true; }
                else { btnNext.Enabled = false; }
                if (i < j && i > 0) { btnPrevious.Enabled = true; }
                else { btnPrevious.Enabled = false; }
                if (j == 1) { btnNext.Enabled = false; btnPrevious.Enabled = false; }
            }
            else
            { PanelPreview.Visible = false; }
        }

        private void loadJustPreview(int Id)
        {
            try
            {
                TaskFacade taskFacade1 = new TaskFacade();
                ArrayList arrImgLampiran1 = taskFacade1.RetrieveByArrLampiran(Id);
                Session["i"] = arrImgLampiran1.Count - 1;

                ArrayList arrImgLampiran = new ArrayList();
                foreach (Task dtImg in arrImgLampiran1)
                {
                    arrImgLampiran.Add(dtImg.Image);
                }
                Session["ArrImgLampiran"] = arrImgLampiran;
                string a = arrImgLampiran[0].ToString();
                Session["TempIdLampiran"] = a.Substring(2, 17);
            }
            catch { }
    }
    }
}