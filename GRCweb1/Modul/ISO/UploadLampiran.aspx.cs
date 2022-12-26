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
    public partial class UploadLampiran : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Users)Session["Users"] == null || Convert.ToString((Users)Session["Users"]) == string.Empty)
            {
                PanelUpload.Visible = false;
            }

            if (Session["TaskId"] != null && Convert.ToInt32(Session["TaskId"]) != 0 && Convert.ToString(Session["TaskId"]) != string.Empty)
            {
                PanelUpload.Visible = false;
            }
        }

       
        protected void fileUploadSuccess(object sender, AsyncFileUploadEventArgs e)
        {
            string fileExtension = Path.GetExtension(AjaxfileUpload.PostedFile.FileName);

            

            if (Session["i"] != null) { Session["i"] = Convert.ToInt32(Session["i"]) + 1; }
            else { Session["i"] = 0; }

            ArrayList arrImgLampiran = new ArrayList();
            if (Session["ArrImgLampiran"] != null)
            { arrImgLampiran = (ArrayList)Session["ArrImgLampiran"]; }

            string MyString = "LT" + Convert.ToString(Session["TempIdLampiran"]) + "_" + Convert.ToString(Convert.ToInt32(Session["i"]) + 1) + ".jpg";

            System.Threading.Thread.Sleep(1000);
            //string FileName = System.IO.Path.GetFileName(AjaxfileUpload.FileName);
            AjaxfileUpload.SaveAs(Server.MapPath("~\\Resource_Web\\Lampiran_iso\\") + MyString);

            arrImgLampiran.Add(MyString);
            Session["ArrImgLampiran"] = arrImgLampiran;
        }
    }
}