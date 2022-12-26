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
using BusinessFacade;
using Domain;
using System.Net;

namespace GRCweb1.Modul.ISO
{
    public partial class StrukturOrg : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                string[] UloadDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UploadDoc", "PES").Split(',');
                int pos = Array.IndexOf(UloadDept, ((Users)Session["Users"]).DeptID.ToString());
                btnUpload.Visible = (pos > -1) ? true : false;
                
                 prev.Attributes["height"] = "435px";
                 prev.Attributes["width"] = "1100x";


            }
        }

        private void LoadDept(bool old)
        {
            DeptFacade dp = new DeptFacade();
            ArrayList arrData = new ArrayList();
            arrData = dp.RetrieveAliasDept();
            ddlDept.Items.Clear();
            foreach (Dept d in arrData)
            {
                ddlDept.Items.Add(new ListItem(d.AlisName.ToUpper(), d.ID.ToString()));
            }
            ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
        private void LoadDept()
        {
            ISO_PES ip = new ISO_PES();
            ArrayList arrData = new ArrayList();
            ip.Tahun = 2016;// int.Parse(ddlTahun.SelectedValue.ToString());
            arrData = ip.LoadDept();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--Pilih Dept--", "0"));
            foreach (PES2016 d in arrData)
            {
                ddlDept.Items.Add(new ListItem(d.DeptName, d.DeptID.ToString()));
            }
            ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            Organisasi ip = new Organisasi();
            ip.Limit = "TOP 1 ";
            string Criteria = " And DeptID=" + ddlDept.SelectedValue.ToString();
            ArrayList arrData = ip.Struktur(Criteria);
            string Filename = "";
            foreach (PESOrg p in arrData)
            {
                Filename = p.FileName;
            }
            Preview(Filename);

        }
        private void Preview(string FileName)
        {
            //prev.Attributes["src"] = ResolveUrl("StrukturOrgPDF.aspx?ba=" + FileName);
            prev.Attributes["src"] = ResolveUrl("../ISO_UPD/StrukturOrgPDF.aspx?ba=" + FileName);
        }
    }
}