using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace GRCweb1.Modul.ISO
{
    public partial class EditPes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                ddlDept.Enabled = false;
                ddlPIC.Enabled = false;
                ddlTahun.Enabled = false;
                ddlBulan.Enabled = false;
                btncancel.Enabled = false;
            }

        }

        protected void ddlPES_Change(object sender, EventArgs e)
        {
            LoadDept();

            ddlTahun.Enabled = false; ddlBulan.Items.Clear();
            ddlBulan.Enabled = false; ddlTahun.Items.Clear();
            ddlPIC.Enabled = false; ddlPIC.Items.Clear();
        }

        private void LoadDept()
        {
            ddlDept.Enabled = true;

            ArrayList arrDept = new ArrayList();
            ISO_SOPFacade PESFacade = new ISO_SOPFacade();
            arrDept = PESFacade.RetrieveDept();

            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (ISO_SOP dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }


            lstBA.DataSource = string.Empty;
            lstBA.DataBind();
        }

        protected void ddlDept_Change(object sender, EventArgs e)
        {
            ddlPIC.Enabled = true;
            ddlTahun.Enabled = false; ddlBulan.Items.Clear();
            ddlBulan.Enabled = false; ddlTahun.Items.Clear(); ddlDept.Enabled = false;
            string deptname = string.Empty;
            if (ddlDept.SelectedItem.Text.Trim().Length >= 5)
                deptname = ddlDept.SelectedItem.Text.Trim().Substring(0, 5).ToUpper();
            else
                deptname = ddlDept.SelectedItem.Text.Trim().ToUpper();
            if (deptname == "MAINT")
            {
                int DeptID = 19; Session["DeptID"] = DeptID;
            }
            else
            {
                int DeptID = Convert.ToInt32(ddlDept.SelectedValue); Session["DeptID"] = DeptID;
            }
            int DeptIDPES = Convert.ToInt32(Session["DeptID"]);
            ArrayList arrPIC = new ArrayList();
            ISO_SOPFacade PESFacade2 = new ISO_SOPFacade();
            arrPIC = PESFacade2.RetrievePIC(Convert.ToInt32(DeptIDPES));
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("-- Pilih --", "0"));

            foreach (ISO_SOP pic in arrPIC)
            {
                ddlPIC.Items.Add(new ListItem(pic.Pic, pic.ID.ToString()));
            }
        }

        protected void ddlPIC_Change(object sender, EventArgs e)
        {
            ddlTahun.Enabled = true; ddlPIC.Enabled = true;

            ArrayList arrTahun = new ArrayList();
            ISO_SOPFacade PESFacade3 = new ISO_SOPFacade();
            arrTahun = PESFacade3.RetrieveTahun(Convert.ToInt32(ddlPIC.SelectedValue), ddlPES.SelectedItem.ToString().Trim());
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (ISO_SOP tahun in arrTahun)
            {
                ddlTahun.Items.Add(new ListItem(tahun.Tahun.ToString(), tahun.Tahun.ToString()));
            }
        }

        protected void ddlTahun_Change(object sender, EventArgs e)
        {
            //ddlBulan.Enabled = true;
            //ArrayList arrBulan = new ArrayList();
            //ISO_SOPFacade PESFacade4 = new ISO_SOPFacade();
            //arrBulan = PESFacade4.RetrieveBulan(Convert.ToInt32(ddlPIC.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));
            //ddlBulan.Items.Clear();
            //ddlBulan.Items.Add(new ListItem("-- Pilih --", "0"));
            //foreach (ISO_SOP bulan in arrBulan)
            //{
            //    ddlBulan.Items.Add(new ListItem(bulan.NamaBulan, bulan.ID.ToString()));
            //}
            LoadDataBulan();
        }

        protected void LoadDataBulan()
        {
            ddlBulan.Enabled = true;
            ArrayList arrBulan = new ArrayList();
            ISO_SOPFacade PESFacade4 = new ISO_SOPFacade();
            arrBulan = PESFacade4.RetrieveBulan(Convert.ToInt32(ddlPIC.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue));
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (ISO_SOP bulan in arrBulan)
            {
                ddlBulan.Items.Add(new ListItem(bulan.NamaBulan, bulan.ID.ToString()));
            }
        }

        protected void ddlBulan_Change(object sender, EventArgs e)
        {
            LoadDataPES();
        }

        protected void LoadDataPES()
        {
            ArrayList arrPES = new ArrayList();
            ISO_SOPFacade PESFacade5 = new ISO_SOPFacade();
            arrPES = PESFacade5.RetrieveDataKPISOP(Convert.ToInt32(ddlPIC.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue),
                Convert.ToInt32(ddlBulan.SelectedValue), ddlPES.SelectedItem.ToString().Trim());

            Session["ListOfDataPES"] = arrPES;

            if (arrPES.Count > 0)
            {
                btncancel.Enabled = true;
            }
            else { btncancel.Enabled = false; }

            lstBA.DataSource = arrPES;
            lstBA.DataBind();

            if (arrPES.Count == 0)
            {
                DisplayAJAXMessage(this, "Data tidak ada !! "); return;
            }
        }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ISO_SOP KPISOP = (ISO_SOP)e.Item.DataItem;
            Image att = (Image)e.Item.FindControl("att");
            Image att2 = (Image)e.Item.FindControl("att2");

            if (KPISOP.Status > -1)
            {
                if (KPISOP.Status == 0)
                {
                    att2.Visible = true;
                    att.Visible = false;
                }
                else if (KPISOP.Status == 2)
                {
                    att2.Visible = false;
                    att.Visible = true;
                }
            }

        }

        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        {
            Repeater rpt = (Repeater)sender;
            Image pre = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("att");
            Image pre2 = (Image)rpt.Items[int.Parse(e.CommandArgument.ToString())].FindControl("att2");

            switch (e.CommandName)
            {
                case "attach":
                    ISO_SOP pes = new ISO_SOP();
                    ISO_SOPFacade pesFacade = new ISO_SOPFacade();
                    pes.ID = Convert.ToInt32(pre.CssClass);

                    int intResult = 0;
                    intResult = pesFacade.Unlock(pes.ID, ddlPES.SelectedItem.ToString().Trim());
                    LoadDataPES();
                    break;

                case "attach2":
                    ISO_SOP pes2 = new ISO_SOP();
                    ISO_SOPFacade pesFacade2 = new ISO_SOPFacade();
                    pes2.ID = Convert.ToInt32(pre.CssClass);

                    int intResult1 = 0;
                    intResult1 = pesFacade2.Lock(pes2.ID, ddlPES.SelectedItem.ToString().Trim());
                    LoadDataPES();
                    break;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ISO_SOP pes3 = new ISO_SOP();
            ISO_SOPFacade pesFacade3 = new ISO_SOPFacade();

            int intResult2 = 0;
            intResult2 = pesFacade3.Cancel(ddlPES.SelectedItem.ToString().Trim(), Convert.ToInt32(ddlPIC.SelectedValue), Convert.ToInt32(ddlTahun.SelectedValue),
                Convert.ToInt32(ddlBulan.SelectedValue));

            if (intResult2 > 0)
            {
                LoadDataPES();
                LoadDataBulan();




                DisplayAJAXMessage(this, "Cancel Inputan Berhasil !");
                return;
            }
        }
        //private void LoadJenis()
        //{
        //    ArrayList arrJenis = new ArrayList();
        //    ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
        //    arrJenis = masterDFacade.RetrieveJenis();
        //    ddlJenis.Items.Add(new ListItem("---- Pilih ----", "0"));
        //    foreach (ISO_UpdMasterDoc jn in arrJenis)
        //    {
        //        ddlJenis.Items.Add(new ListItem(jn.DocCategory, jn.IDdoc.ToString()));
        //    }
        //}

        //protected void btnPrint_ServerClick(object sender, EventArgs e)
        //{
        //    string strError = string.Empty;
        //    string Deptm1 = ((Users)Session["Users"]).DeptID.ToString();
        //    string Deptm2 = ddlDept.SelectedValue;
        //    string Deptm = string.Empty;
        //    string jenis = ddlJenis.SelectedValue;
        //    Users users = (Users)Session["Users"];

        //    if (users.DeptID == 23 || users.DeptID == 14)
        //    { Deptm = Deptm2; } else
        //    { Deptm = Deptm1; }

        //    ISO_UPD2Facade updF = new ISO_UPD2Facade();
        //    ArrayList arrUPD = updF.RetrieveDok2(Deptm, jenis);        
        //    ISO_UpdDMD up = new ISO_UpdDMD();

        //    Session["ListOfDokumen"] = arrUPD;
        //    lstBA.DataSource = arrUPD;
        //    lstBA.DataBind();        
        //}

        //protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Users users = (Users)Session["Users"];

        //    ISO_UpdDMD ba = (ISO_UpdDMD)e.Item.DataItem;
        //    Image view = (Image)e.Item.FindControl("view");
        //    Image att = (Image)e.Item.FindControl("att");

        //    if (users.DeptID == 23 && ba.ID != 0)
        //    { att.Visible = true; } else
        //    { att.Visible = false; }

        //    view.Attributes.Add("onclick", "PreviewPDF('" + ba.ID.ToString() + "')");
        //    att.Attributes.Add("onclick", "OpenDialog('" + ba.ID.ToString() +  "')");
        //} 




        //protected void ddlDept_Change(object sender, EventArgs e)
        //{


        //}

        //protected void ddlJenis_Change(object sender, EventArgs e)
        //{
        //    ArrayList arrData = new ArrayList();
        //    ISO_UpdDMD upd = new ISO_UpdDMD();
        //    ISO_UPD2Facade updF = new ISO_UPD2Facade();

        //    Users users = (Users)Session["Users"];

        //    if (users.DeptID == 23 || users.DeptID == 14)
        //    {
        //        string Deptm = string.Empty;
        //        string jenis = string.Empty;

        //        Deptm = ddlDept.SelectedValue;
        //        jenis = ddlJenis.SelectedValue;

        //        arrData = updF.RetrieveDok2(Deptm, jenis);
        //        lstBA.DataSource = arrData;
        //        lstBA.DataBind();
        //    }
        //    else
        //    {
        //        string Deptm = string.Empty;
        //        string jenis = string.Empty;

        //        Deptm = users.DeptID.ToString();
        //        jenis = ddlJenis.SelectedValue;

        //        arrData = updF.RetrieveDok2(Deptm, jenis);
        //        lstBA.DataSource = arrData;
        //        lstBA.DataBind();

        //    }  
        //}  

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}