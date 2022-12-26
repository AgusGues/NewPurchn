using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.ISO
{
    public partial class UpdateMasterPES : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadDept();
                //ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
                //LoadPIC();
            }
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ///ddlBulan.Items.Add(new ListItem("--pilih bulan--", "0"));
            int n = 0;
            for (int i = 0; i < 12; i++)
            {
                n = i + 1;
                ddlBulan.Items.Add(new ListItem(Global.nBulan(n).ToString(), n.ToString().PadLeft(2, '0')));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        private void LoadTahun()
        {
            ddlTahun.Items.Clear();
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year - 1).ToString(), (DateTime.Now.Year - 1).ToString()));
            ddlTahun.Items.Add(new ListItem(DateTime.Now.Year.ToString(), DateTime.Now.Year.ToString()));
            ddlTahun.Items.Add(new ListItem((DateTime.Now.Year + 1).ToString(), (DateTime.Now.Year + 1).ToString()));
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            ArrayList arrDept = new ArrayList();
            string dpName = string.Empty;
            MasterPESS mp = new MasterPESS();
            mp.Criteria = (txtItem.Text == string.Empty) ? "" : " and ic.Description like '%" + txtItem.Text + "%' ";
            mp.Criteria += (ddlDept.SelectedIndex == 0) ? "" : " and ic.DeptID=" + ddlDept.SelectedValue.ToString();
            mp.Criteria += "and ua.RowStatus>-1";
            lstDept.DataSource = mp.RetrieveCari();
            lstDept.DataBind();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            ArrayList arrDept = new ArrayList();
            string dpName = string.Empty;
            MasterPESS mp = new MasterPESS();
            mp.Criteria = (ddlDept.SelectedIndex == 0) ? "" : " and ID=" + ddlDept.SelectedValue.ToString();
            //string forHoOnly = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("HeadOffice", "PES").ToString();
            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Input SOP' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString() + ",0";
            arrDept = mp.RetrieveDept();
            lstDept.DataSource = arrDept;
            lstDept.DataBind();
            txtTotal.Text = mp.GetTotalPes(ddlPIC.SelectedItem.Text.Trim(), ddlPesType.SelectedValue).ToString() + "%";
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListMasterSOPKPI.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "Departement :" + ddlDept.SelectedItem.Text;
            Html += "<br>PIC     : " + ddlPIC.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0\"", "border=\"1\"");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
        protected void ddlDept_SelectedChange(object sender, EventArgs e)
        {
            LoadPIC();
        }

        protected void ddlPIC_Change(object sender, EventArgs e)
        {
            btnPreview_Click(null, null);
        }
        protected void lstDept_DataBound(object sender, RepeaterItemEventArgs e)
        {
            MasterPESS mp = new MasterPESS();
            Repeater lstpic = (Repeater)e.Item.FindControl("lstPIC");
            PESMM pm = (PESMM)e.Item.DataItem;
            if (txtItem.Text != string.Empty)
            {
                if (ddlPIC.SelectedIndex == 0 && ddlDept.SelectedIndex == 0)
                {
                    mp.Criteria = "";
                }
                else if (ddlPIC.SelectedIndex > 0 && ddlDept.SelectedIndex > 0)
                {
                    mp.Criteria = " and ua.DeptID=" + ddlDept.SelectedValue.ToString();
                    mp.Criteria += " and ua.UserID=" + ddlPIC.SelectedValue.ToString();
                }
                else if (ddlPIC.SelectedIndex == 0 && ddlDept.SelectedIndex > 0)
                {
                    mp.Criteria = "and ua.DeptID=" + ddlDept.SelectedValue.ToString();
                }

                mp.Field = "PICSop2";
                lstpic.DataSource = mp.Retrieve();
                lstpic.DataBind();
            }
            else
            {
                if (ddlPIC.SelectedIndex == 0 && ddlDept.SelectedIndex == 0)
                {
                    mp.Criteria = "";
                }
                else if (ddlPIC.SelectedIndex > 0 && ddlDept.SelectedIndex > 0)
                {
                    mp.Criteria = " and ua.DeptID=" + ddlDept.SelectedValue.ToString();
                    mp.Criteria += " and ua.UserID=" + ddlPIC.SelectedValue.ToString();
                }
                else if (ddlPIC.SelectedIndex == 0 && ddlDept.SelectedIndex > 0)
                {
                    mp.Criteria = "and ua.DeptID=" + ddlDept.SelectedValue.ToString();
                }

                mp.Field = "PICSop";
                lstpic.DataSource = mp.Retrieve();
                lstpic.DataBind();
            }
        }
        protected void lstPIC_DataBound(object sender, RepeaterItemEventArgs e)
        {
            MasterPESS mp = new MasterPESS();
            ArrayList arrP = new ArrayList();
            PESMM pm = (PESMM)e.Item.DataItem;
            Repeater lstcat = (Repeater)e.Item.FindControl("lstCat");
            mp.Criteria = " ID=" + ddlPesType.SelectedValue.ToString();
            mp.Field = "PESType";
            arrP = mp.Retrieve();

            lstcat.DataSource = arrP;
            lstcat.DataBind();
        }
        protected void lstCat_DataBound(object sender, RepeaterItemEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            ArrayList arrTot = new ArrayList();
            PESMM pmp = new PESMM();
            Repeater lstSOP = (Repeater)e.Item.FindControl("lstPES");
            //Repeater lstTot = (Repeater)e.Item.FindControl("lstToBot");
            MasterPESS mp = new MasterPESS();
            Repeater lstP = (Repeater)sender;
            var data = ((RepeaterItem)e.Item.Parent.Parent).DataItem;
            PESMM pm = (PESMM)data;
            PESMM pm2 = (PESMM)e.Item.DataItem;
            if (txtItem.Text != string.Empty)
            {
                mp.Criteria = " and ic.Description like '%" + txtItem.Text + "%'";
                mp.Criteria += (ddlDept.SelectedIndex == 0) ? "" : " and ic.DeptID=" + ddlDept.SelectedValue.ToString();
                mp.Criteria += "and ua.RowStatus>-1";
                mp.Field = "CariPes";
                lstSOP.DataSource = mp.Retrieve();
                lstSOP.DataBind();
            }
            else
            {
                if (((Users)Session["Users"]).Apv == 2)
                {
                    mp.Criteria = " and ic.DeptID=" + pm.DeptID;
                    mp.Criteria += " and iu.UserID=" + pm.ISOUserID;
                    mp.Criteria += " and ic.PesType=" + ddlPesType.SelectedValue.ToString();
                    mp.Criteria += " and iu.SectionID=" + pm.BagianID.ToString();
                    mp.Criteria += " and ua.BagianID=" + pm.BagianID.ToString();
                    mp.Field = "Cat2";
                    arrData = mp.Retrieve();
                    lstSOP.DataSource = arrData;
                    lstSOP.DataBind();
                    CheckBoxList chek = ((CheckBoxList)e.Item.FindControl("checkboxlist1"));
                    string strSQL = "select isnull(iu.Penilaian2,0)Penilaian2, iu.RowStatus, isnull(ic.ApvUpdPes,0)ApvUpdPes from ISO_UserCategory as iu right join ISO_Category as ic on iu.CategoryID=ic.ID where " +
                    "iu.CategoryID=" + pmp.ID + " and iu.ID=" + pmp.IDUser + " and ic.ID=" + pmp.ID;
                    DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
                    SqlDataReader dr = da.RetrieveDataByString(strSQL);
                    string strSQLerror = da.Error;
                    int i = 0;

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            if (Convert.ToInt32(dr["Penilaian2"].ToString()) == 6)
                            {
                                ListItem li2 = chek.Items.FindByValue(dr["Penilaian2"].ToString());
                                li2.Selected = true;
                                li2.Enabled = false;
                            }
                        }
                    }
                }
                else if (((Users)Session["Users"]).Apv == 3)
                {
                    mp.Criteria = " and ic.DeptID=" + pm.DeptID;
                    mp.Criteria += " and iu.UserID=" + pm.ISOUserID;
                    mp.Criteria += " and ic.PesType=" + ddlPesType.SelectedValue.ToString();
                    mp.Criteria += " and iu.SectionID=" + pm.BagianID.ToString();
                    mp.Criteria += " and ua.BagianID=" + pm.BagianID.ToString();
                    mp.Criteria += " and ic.FeedBack='ok' ";
                    mp.Field = "Cat3";
                    arrData = mp.Retrieve();
                    lstSOP.DataSource = arrData;
                    lstSOP.DataBind();
                    CheckBoxList chek = ((CheckBoxList)e.Item.FindControl("checkboxlist1"));
                    string strSQL = "select isnull(iu.Penilaian2,0)Penilaian2, iu.RowStatus, isnull(ic.ApvUpdPes,0)ApvUpdPes from ISO_UserCategory as iu right join ISO_Category as ic on iu.CategoryID=ic.ID where " +
                    "iu.CategoryID=" + pmp.ID + " and iu.ID=" + pmp.IDUser + " and ic.ID=" + pmp.ID;
                    DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
                    SqlDataReader dr = da.RetrieveDataByString(strSQL);
                    string strSQLerror = da.Error;
                    int i = 0;

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            if (Convert.ToInt32(dr["Penilaian2"].ToString()) == 6)
                            {
                                ListItem li2 = chek.Items.FindByValue(dr["Penilaian2"].ToString());
                                li2.Selected = true;
                                li2.Enabled = false;
                            }
                        }
                    }
                }
                else if (((Users)Session["Users"]).Apv == 4)
                {
                    mp.Criteria = " and ic.DeptID=" + pm.DeptID;
                    mp.Criteria += " and iu.UserID=" + pm.ISOUserID;
                    mp.Criteria += " and ic.PesType=" + ddlPesType.SelectedValue.ToString();
                    mp.Criteria += " and iu.SectionID=" + pm.BagianID.ToString();
                    mp.Criteria += " and ua.BagianID=" + pm.BagianID.ToString();
                    mp.Criteria += " and ic.FeedBack='ok' and ua.DeptID=27 ";
                    mp.Field = "Cat4";
                    arrData = mp.Retrieve();
                    lstSOP.DataSource = arrData;
                    lstSOP.DataBind();
                    CheckBoxList chek = ((CheckBoxList)e.Item.FindControl("checkboxlist1"));
                    string strSQL = "select isnull(iu.Penilaian2,0)Penilaian2, iu.RowStatus, isnull(ic.ApvUpdPes,0)ApvUpdPes from ISO_UserCategory as iu right join ISO_Category as ic on iu.CategoryID=ic.ID where " +
                    "iu.CategoryID=" + pmp.ID + " and iu.ID=" + pmp.IDUser + " and ic.ID=" + pmp.ID;
                    DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
                    SqlDataReader dr = da.RetrieveDataByString(strSQL);
                    string strSQLerror = da.Error;
                    int i = 0;

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {

                            if (Convert.ToInt32(dr["Penilaian2"].ToString()) == 6)
                            {
                                ListItem li2 = chek.Items.FindByValue(dr["Penilaian2"].ToString());
                                li2.Selected = true;
                                li2.Enabled = false;
                            }
                        }
                    }
                }
                else
                {
                    mp.Criteria = " and ic.DeptID=" + pm.DeptID;
                    mp.Criteria += " and iu.UserID=" + pm.ISOUserID;
                    mp.Criteria += " and ic.PesType=" + ddlPesType.SelectedValue.ToString();
                    mp.Criteria += " and iu.SectionID=" + pm.BagianID.ToString();
                    mp.Criteria += " and ua.BagianID=" + pm.BagianID.ToString();
                    mp.Field = "Cat";
                    arrData = mp.Retrieve();
                    lstSOP.DataSource = arrData;
                    lstSOP.DataBind();
                }
            }
        }
        protected void lstPES_DataBound(object sender, RepeaterItemEventArgs e)
        {
            PESMM pm = (PESMM)e.Item.DataItem;
            Repeater scr = (Repeater)e.Item.FindControl("lstScr");
            MasterPESS mp = new MasterPESS();
            mp.Criteria = " and CategoryID=" + pm.ID;
            mp.Field = "Score";
            scr.DataSource = mp.Retrieve();
            scr.DataBind();
            CheckBoxList chek = ((CheckBoxList)e.Item.FindControl("checkboxlist1"));
            CheckBox chksmt = ((CheckBox)e.Item.FindControl("chksmt"));
            CheckBox chkthn = ((CheckBox)e.Item.FindControl("chkthn"));
            CheckBox chkhapus = ((CheckBox)e.Item.FindControl("chkhapus"));
            CheckBox chkbatal = ((CheckBox)e.Item.FindControl("chkbatal"));
            CheckBox chkapv = ((CheckBox)e.Item.FindControl("chkapv"));
            //string strSQL = "select ApvUpdPes from ISO_Category where ApvUpdPes=4 and ID=" + pm.ID;
            string strSQL = "select iu.Penilaian, isnull(iu.Penilaian2,0)Penilaian2, iu.RowStatus, isnull(ic.ApvUpdPes,0)ApvUpdPes, ic.FeedBack from ISO_UserCategory as iu right join ISO_Category as ic on iu.CategoryID=ic.ID where " +
            "iu.CategoryID=" + pm.ID + " and iu.ID=" + pm.IDUser + " and ic.ID=" + pm.ID;
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            SqlDataReader dr = da.RetrieveDataByString(strSQL);
            string strSQLerror = da.Error;
            string cekdept = chekdept();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["ApvUpdPes"].ToString()) == 4)
                    {
                        //ListItem li = chek.Items.FindByValue(dr["ApvUpdPes"].ToString());
                        //li.Selected = true;
                        //li.Enabled = false;
                        chkapv.Visible = true;
                        chkapv.Checked = true;
                        chkapv.Enabled = false;
                        LinkButton lnkSmpn = ((LinkButton)e.Item.FindControl("lnkSimpan"));
                        lnkSmpn.Visible = false;
                        if (cekdept == "yes")
                        {
                            lnkSmpn.Visible = true;
                        }
                    }

                    if (Convert.ToInt32(dr["Penilaian2"].ToString()) == 6 || Convert.ToInt32(dr["Penilaian"].ToString()) == 6)
                    {
                        //ListItem li2 = chek.Items.FindByValue(dr["Penilaian2"].ToString());
                        //li2.Selected = true;
                        //li2.Enabled = false;
                        chksmt.Checked = true;
                    }

                    if (Convert.ToInt32(dr["Penilaian2"].ToString()) == 12 || Convert.ToInt32(dr["Penilaian"].ToString()) == 12)
                    {
                        chkthn.Checked = true;
                    }
                    if (Convert.ToInt32(dr["RowStatus"].ToString()) == 2)
                    {
                        //ListItem li3 = chek.Items.FindByValue(dr["RowStatus"].ToString());
                        //li3.Selected = true;
                        //li3.Enabled = false;
                        chkhapus.Checked = true;
                    }

                    if (Convert.ToInt32(dr["ApvUpdPes"].ToString()) != 4 && cekdept == "yes")
                    {
                        LinkButton lnkPubl = ((LinkButton)e.Item.FindControl("LinkPublish"));
                        lnkPubl.Visible = false;
                    }

                    if (cekdept == "yes" && Convert.ToInt32(dr["ApvUpdPes"].ToString()) == 4)
                    {
                        LinkButton lnkPubl = ((LinkButton)e.Item.FindControl("LinkPublish"));
                        lnkPubl.Visible = true;
                    }

                    //if (((Users)Session["Users"]).Apv == 2 && Convert.ToInt32(dr["ApvUpdPes"].ToString()) == 4)
                    //{
                    //    LinkButton lnkSmpn = ((LinkButton)e.Item.FindControl("lnkSimpan"));
                    //    LinkButton lnkApv = ((LinkButton)e.Item.FindControl("lnkApproved"));
                    //    lnkSmpn.Visible = false;
                    //    lnkApv.Visible = false;
                    //}
                    if (dr["FeedBack"].ToString() == "Publish")
                    {
                        LinkButton lnkPubls = ((LinkButton)e.Item.FindControl("LinkPublish"));
                        lnkPubls.Visible = false;
                        LinkButton lnkSmpn = ((LinkButton)e.Item.FindControl("lnkSimpan"));
                        lnkSmpn.Visible = true;
                        if (cekdept == "yes")
                        {
                            lnkSmpn.Visible = true;
                        }
                    }
                }
            }
            TextBox Description2 = (TextBox)e.Item.FindControl("txtDescription2");
            TextBox Target2 = (TextBox)e.Item.FindControl("txtTarget");
            TextBox Bobot2 = (TextBox)e.Item.FindControl("txtBobot");
            TextBox Checking2 = (TextBox)e.Item.FindControl("txtChecking");
            if (Description2.Text != string.Empty)
            {
                Description2.ForeColor = System.Drawing.Color.Blue;
                //Target2.ForeColor = System.Drawing.Color.Green;
                //Bobot2.ForeColor = System.Drawing.Color.Green;
            }
            if (Target2.Text != string.Empty)
            {
                Target2.ForeColor = System.Drawing.Color.Blue;
            }
            if (Convert.ToDecimal(Bobot2.Text) != 0)
            {
                Bobot2.ForeColor = System.Drawing.Color.Blue;
            }
            if (Checking2.Text != string.Empty)
            {
                Checking2.ForeColor = System.Drawing.Color.Blue;
            }
            //CheckBoxList chek2 = ((CheckBoxList)e.Item.FindControl("checkboxlist1"));
            //string strSQLi = "select Penilaian from ISO_UserCategory where CategoryID=" + pm.ID + " and ID=" + pm.IDUser;
            //DataAccess dai = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            //SqlDataReader dri = dai.RetrieveDataByString(strSQLi);
            //string strSQLerrori = dai.Error;
            //int ii = 0;

            //if (dri.HasRows)
            //{
            //    while (dri.Read())
            //    {
            //        ListItem li2 = chek.Items.FindByValue(dri["Penilaian"].ToString());
            //        if (Convert.ToInt32(dri["Penilaian"].ToString()) == 6)
            //        {
            //            li2.Selected = true;
            //            li2.Enabled = false;
            //        }
            //    }
            //}

            if (((Users)Session["Users"]).Apv != 2 && ((Users)Session["Users"]).Apv != 3 && ((Users)Session["Users"]).Apv != 4)
            {
                LinkButton lnkApv = ((LinkButton)e.Item.FindControl("lnkApproved"));
                lnkApv.Visible = false;
            }

            if (((Users)Session["Users"]).Apv == 2 || ((Users)Session["Users"]).Apv == 3 || ((Users)Session["Users"]).Apv == 4)
            {
                LinkButton lnkSmpn = ((LinkButton)e.Item.FindControl("lnkSimpan"));
                lnkSmpn.Visible = false;
                chkapv.Visible = true;
                if (cekdept == "yes")
                {
                    lnkSmpn.Visible = true;
                }
            }
            if (chkapv.Checked)
            {
                LinkButton lnkApv = ((LinkButton)e.Item.FindControl("lnkApproved"));
                lnkApv.Visible = false;
                if (cekdept == "yes")
                {
                    lnkApv.Visible = true;
                }
            }
            //if (((Users)Session["Users"]).ID == 358 || ((Users)Session["Users"]).ID == 47)
            //{
            //    LinkButton lnkPublish = ((LinkButton)e.Item.FindControl("LinkPublish"));
            //    lnkPublish.Visible = true;
            //}
            //{
            //    Convert.ToInt32(chek.SelectedIndex);
            //}
            //var Nama = (DropDownList)e.Item.FindControl("ddlNama");
            //ArrayList arrNama = new ArrayList();
            //MasterPESS p = new MasterPESS();
            //p.Criteria = " and ua.DeptID=" + ddlDept.SelectedValue.ToString();
            //p.Field = "NamaSop";
            //arrNama = p.Retrieve();
            //Nama.Items.Clear();
            //Nama.Items.Add(new ListItem("--All PIC--", "0"));
            //foreach (PESMM ps in arrNama)
            //{
            //    Nama.Items.Add(new ListItem(ps.PIC, ps.ISOUserID.ToString()));
            //}

        }
        protected string chekdept()
        {
            string cekdept = string.Empty;
            string strSQL1 = "select case when deptname like 'IT%' or deptname like 'HRD%' then 'yes' else 'no' end cekdept from dept where id=" + ((Users)Session["Users"]).DeptID;
            DataAccess da1 = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            SqlDataReader dr1 = da1.RetrieveDataByString(strSQL1);
            string strSQLerror1 = da1.Error;
            if (dr1.HasRows == true)
            {
                while (dr1.Read())
                {
                    cekdept = dr1["cekdept"].ToString();
                }
            }
            return cekdept;
        }
        protected void lstscr_DataBound(object sender, RepeaterItemEventArgs e)
        {
            PESMM pm = (PESMM)e.Item.DataItem;
            //CheckBoxList chek = ((CheckBoxList)e.Item.FindControl("checkboxlist1"));
            string strSQL = "select ApvUpdScore from ISO_SOPScore where ApvUpdScore=4 and ID=" + pm.ID2;
            DataAccess da = new DataAccess(Global.ConnectionString());//selalu pakai ini biar ga lock db nya
            SqlDataReader dr = da.RetrieveDataByString(strSQL);
            string strSQLerror = da.Error;

            string cekdept = chekdept();
            int i = 0;

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    if (Convert.ToInt32(dr["ApvUpdScore"].ToString()) == 4 || cekdept == "yes")
                    {
                        LinkButton lnkUpdt = ((LinkButton)e.Item.FindControl("lnkUpdate2"));
                        lnkUpdt.Visible = false;
                        LinkButton lnkPublScore = ((LinkButton)e.Item.FindControl("LinkPublishScore"));
                        lnkPublScore.Visible = true;
                    }
                    //if (((Users)Session["Users"]).Apv != 2 || ((Users)Session["Users"]).Apv != 3)
                    //{
                    //    LinkButton lnkPublScore = ((LinkButton)e.Item.FindControl("LinkPublishScore"));
                    //    lnkPublScore.Visible = false;
                    //}
                    if (cekdept == "yes" && Convert.ToInt32(dr["ApvUpdScore"].ToString()) == 4)
                    {
                        LinkButton lnkPublScore = ((LinkButton)e.Item.FindControl("LinkPublishScore"));
                        lnkPublScore.Visible = true;
                    }
                    else if (cekdept == "yes" && Convert.ToInt32(dr["ApvUpdScore"].ToString()) == 5)
                    {
                        LinkButton lnkUpdt = ((LinkButton)e.Item.FindControl("lnkUpdate2"));
                        lnkUpdt.Visible = false;
                        LinkButton lnkPublScore = ((LinkButton)e.Item.FindControl("LinkPublishScore"));
                        lnkPublScore.Visible = false;
                    }
                }
            }
        }
        protected void lstPES_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int RowNum = e.Item.ItemIndex;
            Repeater lstSOP = (Repeater)e.Item.FindControl("lstPES");
            TextBox ID = ((TextBox)e.Item.FindControl("txtID"));
            TextBox IDUser = ((TextBox)e.Item.FindControl("txtIDUser"));
            TextBox ID2 = ((TextBox)e.Item.FindControl("txtID2"));
            string Description2 = ((TextBox)e.Item.FindControl("txtDescription2")).Text;
            string Description = ((Label)e.Item.FindControl("desk")).Text;
            string Target2 = ((TextBox)e.Item.FindControl("txtTarget")).Text;
            string Target = ((Label)e.Item.FindControl("Target")).Text;
            string Bobot2 = ((TextBox)e.Item.FindControl("txtBobot")).Text;
            string BobotNilai = ((Label)e.Item.FindControl("BobotNilai")).Text;
            string Checking2 = ((TextBox)e.Item.FindControl("txtChecking")).Text;
            string Checking = ((Label)e.Item.FindControl("Checking")).Text;
            string FeedBack = ((TextBox)e.Item.FindControl("txtFeedBack")).Text;
            CheckBoxList chkSmt = (CheckBoxList)e.Item.FindControl("checkboxlist1");
            CheckBox chksmt = ((CheckBox)e.Item.FindControl("chksmt"));
            CheckBox chkthn = ((CheckBox)e.Item.FindControl("chkthn"));
            CheckBox chkhapus = ((CheckBox)e.Item.FindControl("chkhapus"));
            CheckBox chkbatal = ((CheckBox)e.Item.FindControl("chkbatal"));
            CheckBox chkapv = ((CheckBox)e.Item.FindControl("chkapv"));
            if (e.CommandName == "baru")
            {
                panel1.Visible = true;
                lblAddItem.Text = "-";
                Repeater lstScr;
                int j = 0;
                int i = 0;
                desc2.Text = Description;
                target2.Text = Target;
                bobot2.Text = BobotNilai;
                checking2.Text = Checking;
                Repeater lstPIC;
                Repeater lstCat;
                foreach (RepeaterItem objItem in lstDept.Items)
                {
                    lstPIC = ((Repeater)(objItem.FindControl("lstPIC")));
                    foreach (RepeaterItem objDetail in lstPIC.Items)
                    {
                        lstCat = ((Repeater)(objDetail.FindControl("lstCat")));
                        foreach (RepeaterItem objCat in lstCat.Items)
                        {
                            lstSOP = (Repeater)objCat.FindControl("lstPES");
                            foreach (RepeaterItem objDetail0 in lstSOP.Items)
                            {
                                if (j == RowNum)
                                {
                                    lstScr = ((Repeater)(objDetail0.FindControl("lstScr")));
                                    foreach (RepeaterItem objScr in lstScr.Items)
                                    {
                                        Label lblScore = (Label)lstScr.Items[i].FindControl("Pencapaian");
                                        TextBox lblScoreValue = (TextBox)lstScr.Items[i].FindControl("txtScore");
                                        if (i == 0)
                                        {
                                            pentarget2.Text = lblScore.Text;
                                            penscore2.Text = lblScoreValue.Text;
                                        }
                                        if (i == 1)
                                        {
                                            pentarget4.Text = lblScore.Text;
                                            penscore4.Text = lblScoreValue.Text;
                                        }
                                        if (i == 2)
                                        {
                                            pentarget6.Text = lblScore.Text;
                                            penscore6.Text = lblScoreValue.Text;
                                        }
                                        if (i == 3)
                                        {
                                            pentarget8.Text = lblScore.Text;
                                            penscore8.Text = lblScoreValue.Text;
                                        }
                                        if (i == 4)
                                        {
                                            pentarget10.Text = lblScore.Text;
                                            penscore10.Text = lblScoreValue.Text;
                                        }
                                        i++;
                                    }
                                }
                                j++;
                            }
                        }
                    }
                }
            }
            if (e.CommandName == "simpan")
            {
                if (chkhapus.Checked)
                {
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdateUPDHapus";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                    cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                    cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                    cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                    cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                    cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                    //cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = (chksmt.Checked) ? 6 : 0;
                    //cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = (chksmt.SelectedIndex == 6) ? 2 : 0;
                    //cmd.Parameters.Add("@Penilaian3", SqlDbType.Int).Value = (chksmt.SelectedIndex == 4) ? 4 : 0;
                    if (chksmt.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0; //Semesteran
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 6; //Semesteran
                    }
                    else if (chkthn.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0; //Tahunan
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 12; //Tahunan
                    }
                    else
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0;
                    }
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }
                if (chkbatal.Checked)
                {
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdateUPDBatal";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                    cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                    cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                    cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                    cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                    cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                    //cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = (chksmt.Checked) ? 6 : 0;
                    //cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = (chksmt.SelectedIndex == 6) ? 2 : 0;
                    //cmd.Parameters.Add("@Penilaian3", SqlDbType.Int).Value = (chksmt.SelectedIndex == 4) ? 4 : 0;
                    if (chksmt.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0; //Semesteran
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 6; //Semesteran
                    }
                    else if (chkthn.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0; //Tahunan
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 12; //Tahunan
                    }
                    else
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0;
                    }
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }
                else
                {
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdateUPD";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                    cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                    cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                    cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                    cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                    cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                    //cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 6) ? 6 : 12;
                    //cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 6) ? 2 : 0;
                    //cmd.Parameters.Add("@Penilaian3", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 4) ? 4 : 0;
                    if (chksmt.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0; //Semesteran
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 6; //Semesteran
                    }
                    else if (chkthn.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0; //Tahunan
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 12; //Tahunan
                    }
                    else
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0;
                    }
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    //Response.Redirect(Request.RawUrl);
                    btnPreview_Click(null, null);
                }
            }
            else if (e.CommandName == "approved")
            {
                //int jmldicek = 0;
                CheckBoxList chkRow = (CheckBoxList)e.Item.FindControl("checkboxlist1");
                if (chkapv.Checked)
                {
                    //jmldicek = jmldicek + 1;
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDApvPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "ApvUPD";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = (chkRow.SelectedIndex == 6 || chkRow.SelectedIndex == 1 ||
                    //chkRow.SelectedIndex == 2 || chkRow.SelectedIndex == 4) ? 4 : 0;
                    //if (chkRow.SelectedIndex == 0)
                    //{
                    //    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //}
                    //else if (chkRow.SelectedIndex == 1)
                    //{
                    //    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //}
                    //else if (chkRow.SelectedIndex == 4)
                    //{
                    //    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //}
                    //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }
                else
                {
                    DisplayAJAXMessage(this, "Approved belum di ceklis");
                }

                if (chkbatal.Checked)
                {
                    //jmldicek = jmldicek + 1;
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDApvPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "ApvUPDBatal";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = (chkRow.SelectedIndex == 6 || chkRow.SelectedIndex == 1 ||
                    //chkRow.SelectedIndex == 2 || chkRow.SelectedIndex == 4) ? 4 : 0;
                    //if (chkRow.SelectedIndex == 0)
                    //{
                    //    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //}
                    //else if (chkRow.SelectedIndex == 1)
                    //{
                    //    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //}
                    //else if (chkRow.SelectedIndex == 4)
                    //{
                    //    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = 4;
                    //}
                    //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }
            }
            //else if (e.CommandName == "Add")
            //{
            //    //Repeater rptAdd = (Repeater)e.Item.FindControl("lstPES");
            //    for (int i = 0; i <= RowNum - 1; i++)
            //    {
            //        ((TextBox)e.Item.FindControl("txtDescription2")).Text = string.Empty;
            //        ((TextBox)e.Item.FindControl("txtTarget")).Text = string.Empty;
            //        ((TextBox)e.Item.FindControl("txtBobot")).Text = string.Empty;
            //        ((TextBox)e.Item.FindControl("txtChecking")).Text = string.Empty;
            //    }
            //}
            else if (e.CommandName == "publish")
            {
                CheckBoxList chkRow2 = (CheckBoxList)e.Item.FindControl("checkboxlist1");
                if (chkhapus.Checked && chkapv.Checked)
                {
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdatePublishUPDHapus";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                    cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                    cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                    cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                    cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                    cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                    if (chksmt.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 6; //Semesteran
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Semesteran
                    }
                    else if (chkthn.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 12; //Tahunan
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Tahunan
                    }
                    else
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0;
                    }
                    //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    //Response.Redirect(Request.RawUrl);
                    btnPreview_Click(null, null);
                }
                else if (chkapv.Checked)
                {
                    if (Convert.ToDecimal(Bobot2.ToString()) == 0)
                    {
                        SqlConnection con = new SqlConnection(Global.ConnectionString());
                        SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdatePublishUPD";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                        cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                        cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                        cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                        cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                        cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                        cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                        //cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 6) ? 6 : 12;
                        //cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 0) ? 2 : 0;
                        //cmd.Parameters.Add("@Penilaian3", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 4) ? 4 : 0;
                        if (chksmt.Checked)
                        {
                            cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 6; //Semesteran
                            cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Semesteran
                        }
                        else if (chkthn.Checked)
                        {
                            cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 12; //Tahunan
                            cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Tahunan
                        }
                        else
                        {
                            cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0;
                        }
                        //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                        cmd.Connection = con;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        //Response.Redirect(Request.RawUrl.ToString(), true);
                        btnPreview_Click(null, null);
                    }
                    else if (Convert.ToDecimal(Bobot2.ToString()) != 0)
                    {
                        SqlConnection con = new SqlConnection(Global.ConnectionString());
                        SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdatePublishUPDScore";
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                        cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                        cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                        cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                        cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                        cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                        cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                        //cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 6) ? 6 : 12;
                        //cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 0) ? 2 : 0;
                        //cmd.Parameters.Add("@Penilaian3", SqlDbType.Int).Value = (chkSmt.SelectedIndex == 4) ? 4 : 0;
                        if (chksmt.Checked)
                        {
                            cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 6; //Semesteran
                            cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Semesteran
                        }
                        else if (chkthn.Checked)
                        {
                            cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 12; //Tahunan
                            cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Tahunan
                        }
                        else
                        {
                            cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0;
                            cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0;
                        }
                        //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                        cmd.Connection = con;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        //Response.Redirect(Request.RawUrl.ToString(), true);
                        btnPreview_Click(null, null);
                    }
                }
                else if (chkbatal.Checked)
                {
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdatePublishUPDBatal";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                    cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                    cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                    cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                    cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                    cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                    if (chksmt.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 6; //Semesteran
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Semesteran
                    }
                    else if (chkthn.Checked)
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 12; //Tahunan
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0; //Tahunan
                    }
                    else
                    {
                        cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@Penilaian2", SqlDbType.Int).Value = 0;
                    }
                    //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }

            }
        }
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
        protected void lstscr_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int RowNum = e.Item.ItemIndex;
            Repeater lstSOP2 = (Repeater)e.Item.FindControl("lstScr");
            string TargetKe2 = ((TextBox)e.Item.FindControl("txtPencapaian")).Text;
            TextBox Score = ((TextBox)e.Item.FindControl("txtScore2"));
            TextBox ID2 = ((TextBox)e.Item.FindControl("txtID2"));
            if (e.CommandName == "update2")
            {
                if (TargetKe2.Trim() == string.Empty)
                {

                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPESScore", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdateUPD2Hapus";
                    cmd.Parameters.Add("@TargetKe2", SqlDbType.VarChar).Value = TargetKe2.ToString();
                    cmd.Parameters.Add("@ID2", SqlDbType.Int).Value = Convert.ToInt32(ID2.Text);
                    cmd.Parameters.Add("@Score", SqlDbType.Int).Value = Convert.ToInt32(Score.Text);
                    //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }
                else
                {
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPESScore", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdateUPD2";
                    cmd.Parameters.Add("@TargetKe2", SqlDbType.VarChar).Value = TargetKe2.ToString();
                    cmd.Parameters.Add("@ID2", SqlDbType.Int).Value = Convert.ToInt32(ID2.Text);
                    cmd.Parameters.Add("@Score", SqlDbType.Int).Value = Convert.ToInt32(Score.Text);
                    //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                    cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }
            }
            else if (e.CommandName == "publishscore")
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateUPDPESScore", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "UpdateUPDScore2";
                cmd.Parameters.Add("@TargetKe2", SqlDbType.VarChar).Value = TargetKe2.ToString();
                cmd.Parameters.Add("@ID2", SqlDbType.Int).Value = Convert.ToInt32(ID2.Text);
                cmd.Parameters.Add("@Score", SqlDbType.Int).Value = Convert.ToInt32(Score.Text);
                //cmd.CommandText = "update ISO_Category set Description2=@Description2, Target2=@Target2 where ID=@ID";
                cmd.Connection = con;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                //Response.Redirect(Request.RawUrl.ToString(), true);
                btnPreview_Click(null, null);
            }

        }

        protected void lbAddItem_Click(object sender, EventArgs e)
        {
            //List<MyData> dataList = new List<MyData>();
            if (lblAddItem.Text == "+")
            {
                panel1.Visible = true;
                lblAddItem.Text = "-";
            }
            else
            {
                ClearEdit();
                panel1.Visible = false;
                lblAddItem.Text = "+";
            }
        }
       
        protected void Simpan_Click(object sender, EventArgs e)
        {

            int _ddlAutoLink = 0;
            int _ddlPIC = 0;string _desc2 = "";
            string _target2 = "";decimal _bobot2 = 0;string _checking2 = "";
            string _pentarget2 = "";int _penscore2 = 0;
            string _ddlPesType = "";string _ddlDept = "";int _BagianID = 0;
            string _pentarget4 = "";int _penscore4 = 0;
            string _pentarget6 = "";int _penscore6 = 0;
            string _pentarget8 = "";int _penscore8 = 0;
            string _pentarget10 = "";int _penscore10 = 0;
            int _keterangan = 0;
            if (ddlAutoLink.SelectedValue.ToString() != "0")
            {
                MasterPESS Ma = new MasterPESS();
                _ddlAutoLink = Convert.ToInt32(ddlAutoLink.SelectedValue.ToString());
                _ddlPIC = Convert.ToInt32(ddlPIC.SelectedValue.ToString());
                _ddlDept = ddlDept.SelectedValue.ToString();
                _desc2 = Ma.CategoryDesc(_ddlAutoLink);
                _target2 = Ma.CategoryTarget(_ddlAutoLink);
                _checking2 = Ma.CategoryCheck(_ddlAutoLink);
                _bobot2 = Ma.CategoryBobot(_ddlAutoLink);
                _ddlPesType = ddlPesType.SelectedValue.ToString();
                _BagianID = Convert.ToInt32(BagianID.Text.ToString());
                _pentarget2 = ""; _penscore2 = 0;
                _pentarget4 = ""; _penscore4 = 0;
                _pentarget6 = ""; _penscore6 = 0;
                _pentarget8 = "";_penscore8 = 0;
                _pentarget10 = "";_penscore10 = 0;
                _keterangan = 0;
            }
            else
            {
                if (ddlPIC.SelectedValue.ToString() == "0") { DisplayAJAXMessage(this, "pic Harus di isi"); return; }
                if (desc2.Text == "") { DisplayAJAXMessage(this, "deskripsi Harus di isi"); return; }
                if (target2.Text == "") { DisplayAJAXMessage(this, "target Harus di isi"); return; }
                if (bobot2.Text.ToString() == "") { DisplayAJAXMessage(this, "bobot Harus di isi"); return; }
                if (checking2.Text.ToString() == "") { DisplayAJAXMessage(this, "checking Harus di isi"); return; }
                _ddlAutoLink = Convert.ToInt32(ddlAutoLink.SelectedValue.ToString()); ;
                _ddlPIC = Convert.ToInt32(ddlPIC.SelectedValue.ToString());_desc2 = desc2.Text;
                _target2 = target2.Text;_bobot2 = Convert.ToDecimal(bobot2.Text.ToString())/ 100;_checking2 = checking2.Text;
                _pentarget2 = pentarget2.Text;_penscore2 = Convert.ToInt32(penscore2.Text.ToString());
                _ddlPesType = ddlPesType.SelectedValue.ToString();_ddlDept = ddlDept.SelectedValue.ToString();_BagianID = Convert.ToInt32(BagianID.Text.ToString());
                _pentarget4 = pentarget4.Text;_penscore4 = Convert.ToInt32(penscore4.Text.ToString());
                _pentarget6 = pentarget6.Text;_penscore6 = (penscore6.Text.ToString() == string.Empty) ? 0 : Convert.ToInt32(penscore6.Text.ToString());
                _pentarget8 = pentarget8.Text;_penscore8 = (penscore8.Text.ToString() == string.Empty) ? 0 : Convert.ToInt32(penscore8.Text.ToString());
                _pentarget10 = pentarget10.Text;_penscore10 = (penscore10.Text.ToString() == string.Empty) ? 0 : Convert.ToInt32(penscore10.Text.ToString());
                _keterangan =0; if (keterangan.SelectedIndex == 0) { _keterangan = 6; }if(keterangan.SelectedIndex == 1) { _keterangan = 12; }
            }
            btnSimpan.Enabled = false;
            PESMM pmp = new PESMM();
            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd = new SqlCommand("spUpdateUPDPESInsert1", con);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "insertnew";
                cmd.Parameters.Add("@ddlAutoLink", SqlDbType.Int).Value = _ddlAutoLink;
                cmd.Parameters.Add("@ddlPIC2", SqlDbType.Int).Value = _ddlPIC;
                cmd.Parameters.Add("@desc2", SqlDbType.VarChar).Value = _desc2;
                cmd.Parameters.Add("@target2", SqlDbType.VarChar).Value = _target2;
                cmd.Parameters.Add("@bobot2", SqlDbType.Decimal).Value = _bobot2;
                cmd.Parameters.Add("@checking2", SqlDbType.VarChar).Value = _checking2;
                cmd.Parameters.Add("@pentarget2", SqlDbType.VarChar).Value = _pentarget2;
                cmd.Parameters.Add("@penscore2", SqlDbType.Int).Value = _penscore2;
                cmd.Parameters.Add("@ddlPesType", SqlDbType.VarChar).Value = _ddlPesType;
                cmd.Parameters.Add("@ddlDept", SqlDbType.VarChar).Value = _ddlDept;
                cmd.Parameters.Add("@BagianID", SqlDbType.Int).Value = _BagianID;
                cmd.Parameters.Add("@pentarget4", SqlDbType.VarChar).Value = _pentarget4;
                cmd.Parameters.Add("@penscore4", SqlDbType.Int).Value = _penscore4;
                cmd.Parameters.Add("@pentarget6", SqlDbType.VarChar).Value = _pentarget6;
                cmd.Parameters.Add("@penscore6", SqlDbType.Int).Value = _penscore6;
                cmd.Parameters.Add("@pentarget8", SqlDbType.VarChar).Value = _pentarget8;
                cmd.Parameters.Add("@penscore8", SqlDbType.Int).Value = _penscore8;
                cmd.Parameters.Add("@pentarget10", SqlDbType.VarChar).Value = _pentarget10;
                cmd.Parameters.Add("@penscore10", SqlDbType.Int).Value = _penscore10;
                cmd.Parameters.Add("@thnbln", SqlDbType.VarChar).Value = ddlTahun.SelectedValue + ddlBulan.SelectedValue;
                cmd.Parameters.Add("@Penilaian", SqlDbType.Int).Value = _keterangan;

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch { }
            btnPreview_Click(null, null);
            ClearEdit();
            btnSimpan.Enabled = true;
            LoadAutoLink();
        }
        private void ClearEdit()
        {
            desc2.Text = string.Empty;
            target2.Text = string.Empty;
            bobot2.Text = string.Empty;
            checking2.Text = string.Empty;
            pentarget2.Text = string.Empty;
            penscore2.Text = string.Empty;
            pentarget4.Text = string.Empty;
            penscore4.Text = string.Empty;
            pentarget6.Text = string.Empty;
            penscore6.Text = string.Empty;
            pentarget8.Text = string.Empty;
            penscore8.Text = string.Empty;
            pentarget10.Text = string.Empty;
            penscore10.Text = string.Empty;
        }
        private void LoadDept()
        {
            UsersFacade usersFacade = new UsersFacade();
            Users users2 = usersFacade.RetrieveByUserName(((Users)Session["Users"]).UserName);
            MasterPESS rps = new MasterPESS();
            ArrayList arrDept = new ArrayList();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("-- All Dept --", "0"));
            DeptFacade deptFacade = new DeptFacade();
            deptFacade.Criteria = " order by Alias";
            //ArrayList arrDept = new ArrayList();
            switch (users2.UnitKerjaID)
            {
                case 1:
                case 7:
                    arrDept = deptFacade.RetrieveAliasDept();
                    break;
                default:
                    arrDept = deptFacade.RetrieveAliasDept();
                    break;
            }
            Auth DeptAuth = new Auth();
            UserAuth oto = new UserAuth();
            oto.Option = "All";
            oto.Criteria = " and UserID=" + ((Users)Session["Users"]).ID.ToString();
            oto.Criteria += " and ModulName='Input SOP' ";
            DeptAuth = oto.Retrieve(true);
            string UserDept = ((Users)Session["Users"]).DeptID.ToString() + ",0";
            string[] arrDepts = (DeptAuth.AuthDept != null) ? DeptAuth.AuthDept.Split(',') : UserDept.ToString().Split(',');
            for (int i = 0; i < arrDepts.Count(); i++)
            {
                foreach (Dept dept in arrDept)
                {
                    if (int.Parse(arrDepts[i].ToString()) == dept.ID)
                    {
                        ddlDept.Items.Add(new ListItem(dept.AlisName, dept.ID.ToString()));
                    }
                }
            }
            arrDept = rps.RetrieveDept();
            string dpName = string.Empty;
            //ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }

        private void LoadAutoLink()
        {
            ArrayList arrPIC = new ArrayList();
            MasterPESS p = new MasterPESS();
            p.Criteria = " AND u.PesType= " + ddlPesType.SelectedValue.ToString() + " AND u.SectionID= " + BagianID.Text.ToString();
            p.Field = "ListAutoLink";
            arrPIC = p.Retrieve();
            ddlAutoLink.Items.Clear();
            ddlAutoLink.Items.Add(new ListItem("--None--", "0"));
            foreach (PESMM ps in arrPIC)
            {
                ddlAutoLink.Items.Add(new ListItem(ps.NameAutoLink, ps.UserCategoryId.ToString()));
            }
        }
        private void LoadPIC()
        {
            ArrayList arrPIC = new ArrayList();
            MasterPESS p = new MasterPESS();
            p.Criteria = " and ua.DeptID=" + ddlDept.SelectedValue.ToString();
            p.Field = "PICSop";
            arrPIC = p.Retrieve();
            ddlPIC.Items.Clear();
            ddlPIC.Items.Add(new ListItem("--All PIC--", "0"));
            foreach (PESMM ps in arrPIC)
            {
                ddlPIC.Items.Add(new ListItem(ps.PIC, ps.ISOUserID.ToString()));
            }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            string strError = "";
            ArrayList arrData = new ArrayList();
            for (int n = 0; n < lstDept.Items.Count; n++)
            {
                PESMM usp = new PESMM();
                TextBox Description2 = (TextBox)lstDept.Items[n].FindControl("txtDescription2");
                TextBox ID = (TextBox)lstDept.Items[n].FindControl("txtID");
                using (SqlConnection conn = new SqlConnection(Global.ConnectionString()))
                {
                    using (SqlCommand cmd3 = new SqlCommand("update ISO_Category set Description2=@Description2 where ID=@ID", conn))
                    {
                        cmd3.CommandType = CommandType.Text;
                        cmd3.Parameters.AddWithValue("@Description2", Description2);
                        cmd3.Parameters.AddWithValue("@ID", ID);

                        conn.Open();
                        cmd3.ExecuteNonQuery();
                        conn.Close();
                        //Response.Redirect(Request.RawUrl.ToString(), true);
                        btnPreview_Click(null, null);
                    }
                }
            }
            //foreach (RepeaterItem item in lstDept.Items)
            //{
            //    TextBox Description2 = ((TextBox)item.FindControl("txtDescription2"));
            //    TextBox ID = ((TextBox)item.FindControl("txtID"));
            //    using (SqlConnection conn = new SqlConnection(Global.ConnectionString()))
            //    {
            //        using (SqlCommand cmd3 = new SqlCommand("update ISO_Category set Description2=@Description2 where ID=@ID", conn))
            //        {
            //            cmd3.CommandType = CommandType.Text;
            //            cmd3.Parameters.AddWithValue("@Description2", Description2);
            //            cmd3.Parameters.AddWithValue("@ID", ID);

            //            conn.Open();
            //            cmd3.ExecuteNonQuery();
            //            conn.Close();
            //            Response.Redirect(Request.RawUrl.ToString(), true);
            //        }
            //    }
            //}
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            int jmldicek = 0;
            //RepeaterItem item = e.Item;
            //Repeater lst = (Repeater)e.FindControl("lstDept");
            //Repeater lst2 = (Repeater)lst.FindControl("lstPIC");
            //Repeater lst3 = (Repeater)lst2.FindControl("lstCat");
            //Repeater lst4 = (Repeater)lst3.FindControl("lstPES");
            //TextBox ID = ((TextBox)lst4.FindControl("txtID"));
            //TextBox IDUser = ((TextBox)lst4.FindControl("txtIDUser"));
            for (int n = 0; n < lstDept.Items.Count; n++)
            {
                var item = (sender as Button).NamingContainer as RepeaterItem;
                var rep = (RepeaterItem)item.FindControl("lstPES");
                Repeater SubListRep = (Repeater)item.FindControl("lstPES");
                //string description2 = ((TextBox)item.FindControl("txtDescription2")).Text;
                CheckBox chkRow = (CheckBox)rep.FindControl("checkboxlist1");
                TextBox ID = ((TextBox)rep.FindControl("txtID"));
                TextBox IDUser = ((TextBox)rep.FindControl("txtIDUser"));
                if (chkRow.Checked)
                {
                    jmldicek = jmldicek + 1;
                    SqlConnection con = new SqlConnection(Global.ConnectionString());
                    SqlCommand cmd = new SqlCommand("spUpdateUPDPES", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "ApprovedUPD";
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Convert.ToInt32(ID.Text);
                    cmd.Parameters.Add("@IDUser", SqlDbType.Int).Value = Convert.ToInt32(IDUser.Text);
                    cmd.Parameters.Add("@ApvUpdPes", SqlDbType.Int).Value = Convert.ToInt32(chkRow.Checked);
                    //cmd.Parameters.Add("@Description2", SqlDbType.VarChar).Value = Description2.ToString();
                    //cmd.Parameters.Add("@Target2", SqlDbType.VarChar).Value = Target2.ToString();
                    //cmd.Parameters.Add("@Bobot2", SqlDbType.Decimal).Value = Convert.ToDecimal(Bobot2.ToString()) / 100;
                    //cmd.Parameters.Add("@Checking2", SqlDbType.VarChar).Value = Checking2.ToString();
                    //cmd.Parameters.Add("@FeedBack", SqlDbType.VarChar).Value = FeedBack.ToString();
                    //cmd.Connection = con;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    //Response.Redirect(Request.RawUrl.ToString(), true);
                    btnPreview_Click(null, null);
                }

            }
        }
        protected void ddlPIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPIC.SelectedIndex > 0)
            {
                MasterPESS Ma = new MasterPESS();
                int cat = Ma.RetrieveById(ddlPIC.SelectedValue);
                BagianID.Text = cat.ToString();
                string BagianName = Ma.RetrieveByName(cat);
                lblPic.Text = ddlPIC.SelectedItem.ToString()+ " | "+ BagianName;
                LoadAutoLink();
            }
            else { lblPic.Text = ""; ddlAutoLink.Items.Clear();ddlAutoLink.Items.Add(new ListItem("--None--", "0"));}
        }
        
        protected void btnCacel_Click(object sender, EventArgs e)
        {
            ClearEdit();
            panel1.Visible = false;
            lblAddItem.Text = "+";
        }
    }

    public class MasterPESS
    {
        private ArrayList arrData = new ArrayList();
        private PESMM pm = new PESMM();
        public string Criteria { get; set; }
        public string Field { get; set; }
        public string Where { get; set; }
        private string Query()
        {
            string query = string.Empty;
            switch (this.Field)
            {
                case "ListAutoLink":
                    query =
"SELECT c.Description+ ' | '+  " +
"(SELECT q.UserName FROM iso_users q WHERE q.id = u.UserID) NameAutoLink,u.ID UserCategoryId " +
"FROM ISO_UserCategory u, ISO_Category c " +
"WHERE u.CategoryID = c.ID AND u.RowStatus > -1 " + this.Criteria +
"and u.Sarmut != '' AND c.Description != ''  " +
"ORDER BY NameAutoLink ";
                    break;
                case "PICSop":
                    query = "select ua.UserID,ua.UserName,ua.NIK,ib.BagianName,ua.DeptID,ua.BagianID from UserAccount as ua " +
                            "left join ISO_Bagian as ib on ib.ID=ua.BagianID " +
                            "where ua.RowStatus>-1 " + this.Criteria +
                    " order by ua.UserName ";
                    break;
                case "PICSop2":
                    query = "select top 1 ua.UserID,ua.UserName,ua.NIK,ib.BagianName,ua.DeptID,ua.BagianID from UserAccount as ua " +
                            "left join ISO_Bagian as ib on ib.ID=ua.BagianID " +
                            "where ua.RowStatus>-1 " + this.Criteria +
                             "order by ua.DeptID,ib.Urutan,ib.BagianName,ua.UserName ";
                    break;
                case "PESType":
                    query = "Select * from ISO_PES where " + this.Criteria + " order by ID ";
                    break;
                case "Cat":
                    query = "select iu.ID as IDUser,iu.UserID,ic.ID,ua.UserName,ic.Description,ic.Description2,ic.DeptID,ic.Target,ic.Target2,ic.Checking,ic.Checking2,CAST(ic.KodeUrutan as int)KodeUrutan, " +
                          "iu.Bobot,isnull(iu.Bobot2,0) Bobot2,iu.SectionID,iu.TypeBobot, ic.FeedBack, isnull(ic.ApvUpdPes,0)ApvUpdPes " +
                          "from ISO_Category as ic " +
                          "LEFT JOIN ISO_UserCategory as iu " +
                          "on iu.CategoryID=ic.ID and iu.PesType=ic.PesType LEFT JOIN UserAccount as ua on ua.UserID=iu.UserID " +
                          "where iu.RowStatus>-1 and ua.RowStatus>-1 and iu.UserID is not null and ua.UserName is not null " + this.Criteria +
                          " order by iu.UserID, KodeUrutan ";
                    break;
                case "Score":
                    query = "select ID, CategoryID, PesType, TargetKe, PointNilai, RowStatus, isnull(TargetKe2,0)TargetKe2, ApvUpdScore, isnull(PointNilai2,0)PointNilai2 from ISO_SOPScore where RowStatus>-1" + this.Criteria + " order by PointNilai desc";
                    break;
                //case "NamaSop":
                //    query = "select ua.UserID,ua.UserName,ua.NIK,ib.BagianName,ua.DeptID,ua.BagianID from UserAccount as ua " +
                //            "left join ISO_Bagian as ib on ib.ID=ua.BagianID " +
                //            "where ua.RowStatus>-1 " + this.Criteria +
                //             "order by ua.DeptID,ib.Urutan,ib.BagianName,ua.UserName ";
                //    break;
                case "CariPes":
                    query = "select iu.UserID,ic.ID,ua.UserName,ic.Description,ic.Description2,ic.DeptID,ic.Target,ic.Target2,ic.Checking,CAST(ic.KodeUrutan as int)KodeUrutan, " +
                          "iu.Bobot,iu.SectionID,iu.TypeBobot " +
                          "from ISO_Category as ic " +
                          "LEFT JOIN ISO_UserCategory as iu " +
                          "on iu.CategoryID=ic.ID and iu.PesType=ic.PesType LEFT JOIN UserAccount as ua on ua.UserID=iu.UserID " +
                          "where iu.RowStatus>-1 and ua.RowStatus>-1 and iu.UserID is not null and ua.UserName is not null " + this.Criteria +
                          " order by iu.UserID, KodeUrutan ";
                    break;
                case "Cat2":
                    query = "select iu.ID as IDUser,iu.UserID,ic.ID,ua.UserName,ic.Description,ic.Description2,ic.DeptID,ic.Target,ic.Target2,ic.Checking,ic.Checking2,CAST(ic.KodeUrutan as int)KodeUrutan, " +
                          "iu.Bobot,isnull(iu.Bobot2,0) Bobot2,iu.SectionID,iu.TypeBobot, ic.FeedBack, isnull(ic.ApvUpdPes,0)ApvUpdPes " +
                          "from ISO_Category as ic " +
                          "LEFT JOIN ISO_UserCategory as iu " +
                          "on iu.CategoryID=ic.ID and iu.PesType=ic.PesType LEFT JOIN UserAccount as ua on ua.UserID=iu.UserID " +
                          "where iu.RowStatus>-1 and ua.RowStatus>-1 and iu.UserID is not null and ua.UserName is not null " + this.Criteria +
                          " order by iu.UserID, KodeUrutan ";
                    break;
                case "Cat3":
                    query = "select iu.ID as IDUser,iu.UserID,ic.ID,ua.UserName,ic.Description,ic.Description2,ic.DeptID,ic.Target,ic.Target2,ic.Checking,ic.Checking2,CAST(ic.KodeUrutan as int)KodeUrutan, " +
                          "iu.Bobot,isnull(iu.Bobot2,0) Bobot2,iu.SectionID,iu.TypeBobot, ic.FeedBack, isnull(ic.ApvUpdPes,0)ApvUpdPes " +
                          "from ISO_Category as ic " +
                          "LEFT JOIN ISO_UserCategory as iu " +
                          "on iu.CategoryID=ic.ID and iu.PesType=ic.PesType LEFT JOIN UserAccount as ua on ua.UserID=iu.UserID " +
                          "where iu.RowStatus>-1 and ua.RowStatus>-1 and iu.UserID is not null and ua.UserName is not null " + this.Criteria +
                          " order by iu.UserID, KodeUrutan ";
                    break;
                case "Cat4":
                    query = "select iu.ID as IDUser,iu.UserID,ic.ID,ua.UserName,ic.Description,ic.Description2,ic.DeptID,ic.Target,ic.Target2,ic.Checking,ic.Checking2,CAST(ic.KodeUrutan as int)KodeUrutan, " +
                          "iu.Bobot,isnull(iu.Bobot2,0) Bobot2,iu.SectionID,iu.TypeBobot, ic.FeedBack, isnull(ic.ApvUpdPes,0)ApvUpdPes " +
                          "from ISO_Category as ic " +
                          "LEFT JOIN ISO_UserCategory as iu " +
                          "on iu.CategoryID=ic.ID and iu.PesType=ic.PesType LEFT JOIN UserAccount as ua on ua.UserID=iu.UserID " +
                          "where iu.RowStatus>-1 and ua.RowStatus>-1 and iu.UserID is not null and ua.UserName is not null " + this.Criteria +
                          " order by iu.UserID, KodeUrutan ";
                    break;
            }
            return query;
        }
        private PESMM GenerateObject(SqlDataReader sdr)
        {
            pm = new PESMM();
            switch (this.Field)
            {
                case "ListAutoLink":
                    pm.NameAutoLink = sdr["NameAutoLink"].ToString();
                    pm.UserCategoryId = Convert.ToInt32(sdr["UserCategoryId"].ToString());
                    break;
                case "PICSop":
                    pm.PIC = sdr["UserName"].ToString();
                    pm.ISOUserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.BagianName = sdr["BagianName"].ToString();
                    pm.NIK = sdr["NIK"].ToString();
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                    break;
                case "PICSop2":
                    pm.PIC = sdr["UserName"].ToString();
                    pm.ISOUserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.BagianName = sdr["BagianName"].ToString();
                    pm.NIK = sdr["NIK"].ToString();
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                    break;
                case "PESType":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.PESName = sdr["PESName"].ToString();
                    break;
                case "Cat":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.IDUser = Convert.ToInt32(sdr["IDUser"].ToString());
                    pm.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.Desk = sdr["Description"].ToString();
                    pm.Desk2 = sdr["Description2"].ToString();
                    pm.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100;
                    pm.BobotNilai2 = Convert.ToDecimal(sdr["Bobot2"].ToString()) * 100;
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.TypeBobot = sdr["TypeBobot"].ToString();
                    pm.Target = sdr["Target"].ToString();
                    pm.Target2 = sdr["Target2"].ToString();
                    pm.Checking = sdr["Checking"].ToString();
                    pm.Checking2 = sdr["Checking2"].ToString();
                    pm.PIC = sdr["UserName"].ToString();
                    pm.FeedBack = sdr["FeedBack"].ToString();
                    pm.ApvUpdPes = Convert.ToInt32(sdr["ApvUpdPes"].ToString());
                    break;
                case "Score":
                    pm.Pencapaian = sdr["TargetKe"].ToString();
                    pm.Score = Convert.ToDecimal(sdr["PointNilai"].ToString());
                    pm.Pencapaian2 = sdr["TargetKe2"].ToString();
                    pm.ID2 = Convert.ToInt32(sdr["ID"].ToString());
                    pm.Score2 = Convert.ToDecimal(sdr["PointNilai2"].ToString());
                    break;
                //case "NamaSop":
                //    pm.PIC = sdr["UserName"].ToString();
                //    pm.ISOUserID = Convert.ToInt32(sdr["UserID"].ToString());
                //    pm.BagianName = sdr["BagianName"].ToString();
                //    pm.NIK = sdr["NIK"].ToString();
                //    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                //    pm.BagianID = Convert.ToInt32(sdr["BagianID"].ToString());
                //    break;
                case "CariPes":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.Desk = sdr["Description"].ToString();
                    pm.Desk2 = sdr["Description2"].ToString();
                    pm.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100;
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.TypeBobot = sdr["TypeBobot"].ToString();
                    pm.Target = sdr["Target"].ToString();
                    pm.Target2 = sdr["Target2"].ToString();
                    pm.Checking = sdr["Checking"].ToString();
                    pm.PIC = sdr["UserName"].ToString();
                    break;
                case "Cat2":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.IDUser = Convert.ToInt32(sdr["IDUser"].ToString());
                    pm.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.Desk = sdr["Description"].ToString();
                    pm.Desk2 = sdr["Description2"].ToString();
                    pm.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100;
                    pm.BobotNilai2 = Convert.ToDecimal(sdr["Bobot2"].ToString()) * 100;
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.TypeBobot = sdr["TypeBobot"].ToString();
                    pm.Target = sdr["Target"].ToString();
                    pm.Target2 = sdr["Target2"].ToString();
                    pm.Checking = sdr["Checking"].ToString();
                    pm.Checking2 = sdr["Checking2"].ToString();
                    pm.PIC = sdr["UserName"].ToString();
                    pm.FeedBack = sdr["FeedBack"].ToString();
                    pm.ApvUpdPes = Convert.ToInt32(sdr["ApvUpdPes"].ToString());
                    break;
                case "Cat3":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.IDUser = Convert.ToInt32(sdr["IDUser"].ToString());
                    pm.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.Desk = sdr["Description"].ToString();
                    pm.Desk2 = sdr["Description2"].ToString();
                    pm.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100;
                    pm.BobotNilai2 = Convert.ToDecimal(sdr["Bobot2"].ToString()) * 100;
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.TypeBobot = sdr["TypeBobot"].ToString();
                    pm.Target = sdr["Target"].ToString();
                    pm.Target2 = sdr["Target2"].ToString();
                    pm.Checking = sdr["Checking"].ToString();
                    pm.Checking2 = sdr["Checking2"].ToString();
                    pm.PIC = sdr["UserName"].ToString();
                    pm.FeedBack = sdr["FeedBack"].ToString();
                    pm.ApvUpdPes = Convert.ToInt32(sdr["ApvUpdPes"].ToString());
                    break;
                case "Cat4":
                    pm.ID = Convert.ToInt32(sdr["ID"].ToString());
                    pm.IDUser = Convert.ToInt32(sdr["IDUser"].ToString());
                    pm.UserID = Convert.ToInt32(sdr["UserID"].ToString());
                    pm.Desk = sdr["Description"].ToString();
                    pm.Desk2 = sdr["Description2"].ToString();
                    pm.BobotNilai = Convert.ToDecimal(sdr["Bobot"].ToString()) * 100;
                    pm.BobotNilai2 = Convert.ToDecimal(sdr["Bobot2"].ToString()) * 100;
                    pm.DeptID = Convert.ToInt32(sdr["DeptID"].ToString());
                    pm.TypeBobot = sdr["TypeBobot"].ToString();
                    pm.Target = sdr["Target"].ToString();
                    pm.Target2 = sdr["Target2"].ToString();
                    pm.Checking = sdr["Checking"].ToString();
                    pm.Checking2 = sdr["Checking2"].ToString();
                    pm.PIC = sdr["UserName"].ToString();
                    pm.FeedBack = sdr["FeedBack"].ToString();
                    pm.ApvUpdPes = Convert.ToInt32(sdr["ApvUpdPes"].ToString());
                    break;
            }
            return pm;
        }
        public ArrayList Retrieve()
        {
            arrData = new ArrayList();
            string strSQL = this.Query();
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveDept()
        {
            arrData = new ArrayList();
            string strSQL = "Select * from Dept Where RowStatus>-1 " + this.Criteria + " order by DeptName";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new PESMM
                    {
                        DeptID = Convert.ToInt32(sdr["ID"].ToString()),
                        DeptName = sdr["DeptName"].ToString()
                    });
                }
            }
            return arrData;
        }
        public int GetTotalPes(string pic, string pestype)
        {
            int result = 0;
            string strSQL = "select cast(isnull(sum(bobot * 100),0) as int) total from iso_usercategory where RowStatus>-1 and " +
                "userid in (select UserID from useraccount where username='" + pic + "') and pestype=" + pestype;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["total"].ToString());
                }
            }
            return result;
        }
        public ArrayList RetrieveCari()
        {
            arrData = new ArrayList();
            string strSQL = "select iu.UserID,ic.ID,ua.UserName,ic.Description,ic.Description2,ic.DeptID,ic.Target,ic.Target2,ic.Checking,CAST(ic.KodeUrutan as int)KodeUrutan, " +
                          "iu.Bobot,iu.SectionID,iu.TypeBobot, d.DeptName " +
                          "from ISO_Category as ic " +
                          "LEFT JOIN ISO_UserCategory as iu " +
                          "on iu.CategoryID=ic.ID and iu.PesType=ic.PesType LEFT JOIN UserAccount as ua on ua.UserID=iu.UserID " +
                          "LEFT JOIN Dept as d on d.ID=ic.DeptID where iu.RowStatus>-1 and ua.rowstatus>-1 and iu.UserID is not null " + this.Criteria +
                          " order by iu.UserID, KodeUrutan ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                //while (sdr.Read())
                //{
                arrData.Add(new PESMM());
                //return arrData;
                //}
            }
            return arrData;
        }
        public int RetrieveById(string BagianName)
        {
            arrData = new ArrayList();
            int BagID = 0;
            string strSQL = ("select ua.BagianID from UserAccount as ua left join ISO_Bagian as ib on ib.ID=ua.BagianID where ua.RowStatus>-1  and ua.UserID = '" + BagianName + "' ");
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    BagID = Convert.ToInt32(sdr["BagianID"].ToString());
                }
            }
            return BagID;
        }
        public string RetrieveByName(int ID)
        {
            arrData = new ArrayList();
            string val = "";
            string strSQL = ("SELECT BagianName FROM ISO_Bagian WHERE RowStatus>-1 AND ID="+ID);
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    val = sdr["BagianName"].ToString();
                }
            }
            return val;
        }

        public string CategoryDesc(int ID)
        {
            arrData = new ArrayList();string val = "";
            string strSQL = ("SELECT Description FROM ISO_Category WHERE ID IN (SELECT CategoryID FROM ISO_UserCategory WHERE RowStatus>-1 AND ID="+ ID + " )");
            DataAccess da = new DataAccess(Global.ConnectionString());SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows){while (sdr.Read()){val = sdr["Description"].ToString();}}
            return val;
        }
        public string CategoryTarget(int ID)
        {
            arrData = new ArrayList(); string val = "";
            string strSQL = ("SELECT Target FROM ISO_Category WHERE ID IN (SELECT CategoryID FROM ISO_UserCategory WHERE RowStatus>-1 AND ID=" + ID + " )");
            DataAccess da = new DataAccess(Global.ConnectionString()); SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows) { while (sdr.Read()) { val = sdr["Target"].ToString(); } }
            return val;
        }
        public string CategoryCheck(int ID)
        {
            arrData = new ArrayList(); string val = "";
            string strSQL = ("SELECT Checking FROM ISO_Category WHERE ID IN (SELECT CategoryID FROM ISO_UserCategory WHERE RowStatus>-1 AND ID=" + ID + " )");
            DataAccess da = new DataAccess(Global.ConnectionString()); SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows) { while (sdr.Read()) { val = sdr["Checking"].ToString(); } }
            return val;
        }
        public decimal CategoryBobot(int ID)
        {
            arrData = new ArrayList(); decimal val = 0;
            string strSQL = ("SELECT Bobot FROM ISO_UserCategory WHERE RowStatus>-1 AND ID=" + ID);
            DataAccess da = new DataAccess(Global.ConnectionString()); SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows) { while (sdr.Read()) { val = decimal.Parse(sdr["Bobot"].ToString()); } }
            return val;
        }
    }

    public class PESMM : GRCBaseDomain
    {
        public int UserCategoryId { get; set; }
        public string NameAutoLink { get; set; }
        public int ID { get; set; }
        public int ID2 { get; set; }
        public int IDUser { get; set; }
        public string Desk { get; set; }
        public string Desk2 { get; set; }
        public string TypeBobot { get; set; }
        public int TypePes { get; set; }
        public int ISOUserID { get; set; }
        public string Target { get; set; }
        public string Target2 { get; set; }
        public string SOPNo { get; set; }
        public string SOPName { get; set; }
        public decimal BobotNilai { get; set; }
        public decimal BobotNilai2 { get; set; }
        public string PIC { get; set; }
        public string Tahun { get; set; }
        public decimal Score { get; set; }
        public decimal TotalBobot { get; set; }
        public decimal TotalNilai { get; set; }
        public string Pencapaian { get; set; }
        public string Pencapaian2 { get; set; }
        public decimal Nilai { get; set; }
        public int DeptID { get; set; }
        public string AlisName { get; set; }
        public int BagianID { get; set; }
        public string DeptName { get; set; }
        public string BagianName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PESName { get; set; }
        public string Checking { get; set; }
        public string Checking2 { get; set; }
        public string NIK { get; set; }
        public string FeedBack { get; set; }
        public int ApvUpdPes { get; set; }
        public decimal Score2 { get; set; }
    }
}
