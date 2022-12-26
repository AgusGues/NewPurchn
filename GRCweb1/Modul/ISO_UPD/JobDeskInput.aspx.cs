using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
//using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using DataAccessLayer;
using System.Data.SqlClient;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class JobDeskInput : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];
                JobDesk jobdesk = new JobDesk();
                //txtTglSusun.SelectedDate = DateTime.Now;
                txtTglSusun.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDept();
                LoadBawahan();
                LoadTUJabatan();
                LoadTPJabatan();
                LoadHK();
                LoadTJ();
                LoadWewenang();
                LoadPendidikan();
                LoadPengalaman();
                LoadUsia();
                LoadPengetahuan();
                LoadKeterampilan();
                LoadFisik();
                LoadNonFisik();
                Session["id"] = null;
                Session["where"] = string.Empty;
                if (Request.QueryString["ID"] != null)
                {
                    //clearForm();
                    LoadJOBDESK(Request.QueryString["ID"].ToString());
                    //btnUpdate.Disabled = true;
                    //lbAddItem.Enabled = true;
                }

            }
        }
        public int newdata = 0;
        private void LoadJOBDESK(string strID)
        {
            Session["ListOfJOBDESKDetail"] = null;
            Users users = (Users)Session["Users"];
            JobDeskFacade jobDESKFacade = new JobDeskFacade();
            JobDesk jobdesk = new JobDesk();
            jobdesk = jobDESKFacade.RetrieveByNo(strID);

            if (jobDESKFacade.Error == string.Empty && jobdesk.ID > 0)
            {
                ddlDept.Visible = false;
                txtDept.Visible = true;
                txtDept.Text = jobdesk.DeptID.ToString();
                if (Convert.ToInt32(txtDept.Text) == 2)
                {
                    txtDept.Text = "Boardmill";
                }
                else if (Convert.ToInt32(txtDept.Text) == 3)
                {
                    txtDept.Text = "Finishing";
                }
                else if (Convert.ToInt32(txtDept.Text) == 4 || Convert.ToInt32(txtDept.Text) == 5 || Convert.ToInt32(txtDept.Text) == 18 || Convert.ToInt32(txtDept.Text) == 19)
                {
                    txtDept.Text = "Maintenance";
                }
                else if (Convert.ToInt32(txtDept.Text) == 6)
                {
                    txtDept.Text = "Logistik Produk Jadi";
                }
                else if (Convert.ToInt32(txtDept.Text) == 7)
                {
                    txtDept.Text = "Hrd & GA";
                }
                else if (Convert.ToInt32(txtDept.Text) == 9)
                {
                    txtDept.Text = "Quality Assurance";
                }
                else if (Convert.ToInt32(txtDept.Text) == 10)
                {
                    txtDept.Text = "Logistik Bahan Baku";
                }
                else if (Convert.ToInt32(txtDept.Text) == 11)
                {
                    txtDept.Text = "Ppic";
                }
                else if (Convert.ToInt32(txtDept.Text) == 12)
                {
                    txtDept.Text = "Finance";
                }
                else if (Convert.ToInt32(txtDept.Text) == 13)
                {
                    txtDept.Text = "Marketing";
                }
                else if (Convert.ToInt32(txtDept.Text) == 14)
                {
                    txtDept.Text = "IT";
                }
                else if (Convert.ToInt32(txtDept.Text) == 15)
                {
                    txtDept.Text = "Purchasing";
                }
                else if (Convert.ToInt32(txtDept.Text) == 22)
                {
                    txtDept.Text = "Project Sipil";
                }
                else if (Convert.ToInt32(txtDept.Text) == 23)
                {
                    txtDept.Text = "Iso";
                }
                else if (Convert.ToInt32(txtDept.Text) == 24)
                {
                    txtDept.Text = "Accounting";
                }
                else if (Convert.ToInt32(txtDept.Text) == 26)
                {
                    txtDept.Text = "Armada";
                }
                else if (Convert.ToInt32(txtDept.Text) == 27)
                {
                    txtDept.Text = "Manager Coporate";
                }
                else if (Convert.ToInt32(txtDept.Text) == 28)
                {
                    txtDept.Text = "Product Development";
                }
                else if (Convert.ToInt32(txtDept.Text) == 30)
                {
                    txtDept.Text = "Research & Development";
                }
                ddlBagian.Visible = false;
                txtBagian.Text = jobdesk.BagianName.ToString();
                txtBagian.Visible = true;
                Session["id"] = jobdesk.ID;
                txtAtasan.Text = jobdesk.Atasan;
                txtTglSusun.Text = jobdesk.TglSusun.ToString("dd-MMM-yyyy");
                txtRevisi.Text = jobdesk.Revisi.ToString();
                txtID.Text = Session["id"].ToString();
                btnSimpan.Enabled = false;

                if (txtID.Text != string.Empty && jobdesk.Approval == 0)
                {
                    btnUpdate.Visible = true;
                }
                if (users.DeptID == 23 || users.DeptID == 7 && users.UserLevel > 0)
                {
                    btnLaporan.Visible = true;
                    btnUpdate.Visible = false;
                }
                Session["JOBDESKHeader"] = jobdesk;

                JobDeskDetailFacade jobDeskFacadeBawahan = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetail = jobDeskFacadeBawahan.RetrieveByID(jobdesk.ID);
                if (jobDeskFacadeBawahan.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetail;
                    if (arrJobDeskDetail.Count <= 0) arrJobDeskDetail = new ArrayList();
                    //GridView1.DataSource = arrJobDeskDetail;
                    //GridView1.DataBind();
                    lstBawahan.DataSource = arrJobDeskDetail;
                    lstBawahan.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeTUJ = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailTUJ = jobDeskFacadeTUJ.RetrieveByIDTUJ(jobdesk.ID);
                if (jobDeskFacadeTUJ.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailTUJ;
                    if (arrJobDeskDetailTUJ.Count <= 0) arrJobDeskDetailTUJ = new ArrayList();
                    lstTUJ.DataSource = arrJobDeskDetailTUJ;
                    lstTUJ.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeTPJ = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailTPJ = jobDeskFacadeTPJ.RetrieveByIDTPJ(jobdesk.ID);
                if (jobDeskFacadeTPJ.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailTPJ;
                    if (arrJobDeskDetailTPJ.Count <= 0) arrJobDeskDetailTPJ = new ArrayList();
                    lstTPJ.DataSource = arrJobDeskDetailTPJ;
                    lstTPJ.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeHK = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailHK = jobDeskFacadeHK.RetrieveByIDHK(jobdesk.ID);
                if (jobDeskFacadeHK.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailHK;
                    if (arrJobDeskDetailHK.Count <= 0) arrJobDeskDetailHK = new ArrayList();
                    lstHK.DataSource = arrJobDeskDetailHK;
                    lstHK.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeTJ = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailTJ = jobDeskFacadeTJ.RetrieveByIDTJ(jobdesk.ID);
                if (jobDeskFacadeTJ.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailTJ;
                    if (arrJobDeskDetailTJ.Count <= 0) arrJobDeskDetailTJ = new ArrayList();
                    lstTJ.DataSource = arrJobDeskDetailTJ;
                    lstTJ.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeW = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailW = jobDeskFacadeW.RetrieveByIDW(jobdesk.ID);
                if (jobDeskFacadeW.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailW;
                    if (arrJobDeskDetailW.Count <= 0) arrJobDeskDetailW = new ArrayList();
                    lstWewenang.DataSource = arrJobDeskDetailW;
                    lstWewenang.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadePend = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailPend = jobDeskFacadePend.RetrieveByIDPend(jobdesk.ID);
                if (jobDeskFacadePend.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailPend;
                    if (arrJobDeskDetailPend.Count <= 0) arrJobDeskDetailPend = new ArrayList();
                    lstPend.DataSource = arrJobDeskDetailPend;
                    lstPend.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadePeng = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailPeng = jobDeskFacadePeng.RetrieveByIDPeng(jobdesk.ID);
                if (jobDeskFacadePeng.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailPeng;
                    if (arrJobDeskDetailPeng.Count <= 0) arrJobDeskDetailPeng = new ArrayList();
                    lstPengalaman.DataSource = arrJobDeskDetailPeng;
                    lstPengalaman.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadePengt = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailPengt = jobDeskFacadePengt.RetrieveByIDPengt(jobdesk.ID);
                if (jobDeskFacadePengt.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailPengt;
                    if (arrJobDeskDetailPengt.Count <= 0) arrJobDeskDetailPengt = new ArrayList();
                    lstPengetahuan.DataSource = arrJobDeskDetailPengt;
                    lstPengetahuan.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeKet = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailKet = jobDeskFacadeKet.RetrieveByIDKet(jobdesk.ID);
                if (jobDeskFacadeKet.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailKet;
                    if (arrJobDeskDetailKet.Count <= 0) arrJobDeskDetailKet = new ArrayList();
                    lstKeterampilan.DataSource = arrJobDeskDetailKet;
                    lstKeterampilan.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeFisik = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailFisik = jobDeskFacadeFisik.RetrieveByIDFisik(jobdesk.ID);
                if (jobDeskFacadeFisik.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailFisik;
                    if (arrJobDeskDetailKet.Count <= 0) arrJobDeskDetailFisik = new ArrayList();
                    lstFisik.DataSource = arrJobDeskDetailFisik;
                    lstFisik.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeNonFisik = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailNonFisik = jobDeskFacadeNonFisik.RetrieveByIDNonFisik(jobdesk.ID);
                if (jobDeskFacadeNonFisik.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailNonFisik;
                    if (arrJobDeskDetailNonFisik.Count <= 0) arrJobDeskDetailNonFisik = new ArrayList();
                    lstNonFisik.DataSource = arrJobDeskDetailNonFisik;
                    lstNonFisik.DataBind();
                }
                JobDeskDetailFacade jobDeskFacadeUsia = new JobDeskDetailFacade();
                ArrayList arrJobDeskDetailUsia = jobDeskFacadeUsia.RetrieveByIDUsia(jobdesk.ID);
                if (jobDeskFacadeUsia.Error == string.Empty)
                {
                    Session["ID"] = jobdesk.ID;
                    Session["ListOfJOBDESKDetail"] = arrJobDeskDetailUsia;
                    if (arrJobDeskDetailUsia.Count <= 0) arrJobDeskDetailUsia = new ArrayList();
                    lstUsia.DataSource = arrJobDeskDetailUsia;
                    lstUsia.DataBind();
                }
            }
        }
        //private void LoadOpenJOBDESK()
        //{
        //    Users user = ((Users)Session["Users"]);
        //    ArrayList arrJOBDESK = new ArrayList();
        //    JobDeskFacade jobdeskFacade = new JobDeskFacade();
        //    JobDesk jobdesk = new JobDesk();
        //    string UserID = string.Empty;
        //    UserID = user.ID.ToString();
        //    string Apv = jobdeskFacade.GetApv(UserID);
        //    string ApvJOBDESK = jobdeskFacade.GetStatusApv(UserID);
        //    Session["Apv"] = Apv;
        //    Session["ApvJOBDESK"] = ApvJOBDESK;
        //    string UserInput = jobdeskFacade.GetUserI(UserID);
        //    string UserInput1 = jobdeskFacade.GetUserIGrid(UserID);
        //    Session["UserInput"] = UserInput;
        //    Session["UserInput1"] = UserInput1;
        //    arrJOBDESK = jobdeskFacade.RetrieveOpenJOBDESKHeader(UserInput, Apv);
        //    foreach (JobDesk jobdesk1 in arrJOBDESK)
        //    {
        //        if (jobdesk1.ID != 0)
        //        {

        //            ID.Value += jobdesk1.ID + ",";
        //        }
        //    }
        //    Session.Remove("AlasanCancel");
        //    //btnNotApprove.Attributes.Add("onclick", "return confirm_revisi();");

        //}
        //private void LoadOpenJOBDESK(string ID)
        //{
        //    Users user = (Users)Session["Users"];
        //    JobDeskFacade jobdeskf = new JobDeskFacade();
        //    JobDesk jobdesk = new JobDesk();
        //    string UserInput = Session["UserInput"].ToString();
        //    jobdesk = jobdeskf.RetrieveJOBDESKNum(ID);


        //    //txtJOBDESK_No.Text = jobdesk.JOBDESK_No;
        //    txtTglSusun.Text = jobdesk.TglSusun.ToString("dd-MMM-yyyy");
        //    ddlDept.SelectedValue = jobdesk.DeptName;
        //    txtAtasan.Text = jobdesk.Atasan;
        //    //txtApv.Text = jobdesk.Approval2;
        //    //txtJabatan.Text = jobdesk.BagianName;
        //    ddlBagian.SelectedValue = jobdesk.BagianName;
        //    txtID.Text = jobdesk.ID.ToString();
        //    LoadDataGrid(jobdesk.ID);
        //}

        //private void LoadDataGrid(int JOBDESKID)
        //{
        //    Users users = (Users)Session["Users"];
        //    ArrayList arrJOBDESK = new ArrayList();
        //    JobDeskFacade jobdeskf = new JobDeskFacade();
        //    string UserID = users.ID.ToString();
        //    string Apv = jobdeskf.GetApv(UserID);
        //    if (Convert.ToInt32(Apv) == 1)
        //    {
        //        arrJOBDESK = jobdeskf.RetrieveJOBDESK2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
        //    }
        //    else
        //    {
        //        arrJOBDESK = jobdeskf.RetrieveJOBDESK3(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
        //    }
        //    lstTUJ.DataSource = arrJOBDESK;
        //    lstTUJ.DataBind();
        //}

        private void LoadDept()
        {
            //Users users = (Users)Session["Users"];
            DeptFacade deptFacade = new DeptFacade();
            UsersFacade usersFacade = new UsersFacade();
            ArrayList arrDept = new ArrayList();

            deptFacade.Criteria = " and ID in(SELECT DeptID FROM Users WHERE UserName='" + ((Users)Session["Users"]).UserName + "') ";
            deptFacade.Criteria += " order by Alias";
            arrDept = deptFacade.RetrieveAliasDept();
            string UserDept = ((Users)Session["Users"]).DeptID.ToString() + ",0";
            //ddlDept.Items.Clear();
            List<ListItem> lte = new List<ListItem>();
            if (deptFacade.Error == string.Empty)
            {
                ddlDept.Items.Add(new ListItem("-- Pilih Dept --", "0"));
                foreach (Dept dept in arrDept)
                {
                    ddlDept.Items.Add(new ListItem(dept.AlisName, dept.ID.ToString()));
                }
            }
            foreach (ListItem lt in ddlDept.Items)
            {
                lte.Add(lt);
            }
            List<ListItem> sorted = lte.OrderBy(b => b.Text).ToList();
            ddlDept.Items.Clear();
            foreach (ListItem l in sorted)
            {
                ddlDept.Items.Add(l);
            }
            //ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }
        private void LoadBawahan()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT TOP 5 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstBawahan.DataSource = arrData;
            lstBawahan.DataBind();
        }
        private void LoadTUJabatan()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT TOP 3 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstTUJ.DataSource = arrData;
            lstTUJ.DataBind();
        }
        private void LoadTPJabatan()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 20 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstTPJ.DataSource = arrData;
            lstTPJ.DataBind();
        }
        private void LoadHK()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 5 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstHK.DataSource = arrData;
            lstHK.DataBind();
        }
        private void LoadTJ()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 10 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstTJ.DataSource = arrData;
            lstTJ.DataBind();
        }
        private void LoadWewenang()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 10 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstWewenang.DataSource = arrData;
            lstWewenang.DataBind();
        }
        private void LoadPendidikan()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 3 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstPend.DataSource = arrData;
            lstPend.DataBind();
        }
        private void LoadPengalaman()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 3 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstPengalaman.DataSource = arrData;
            lstPengalaman.DataBind();
        }
        private void LoadUsia()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 1 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstUsia.DataSource = arrData;
            lstUsia.DataBind();
        }
        private void LoadPengetahuan()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 5 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstPengetahuan.DataSource = arrData;
            lstPengetahuan.DataBind();
        }
        private void LoadKeterampilan()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 10 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstKeterampilan.DataSource = arrData;
            lstKeterampilan.DataBind();
        }
        private void LoadFisik()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 10 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstFisik.DataSource = arrData;
            lstFisik.DataBind();
        }
        private void LoadNonFisik()
        {
            ArrayList arrData = new ArrayList();
            newdata = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "SELECT Top 10 * FROM users order by id desc";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new JobDesk
                    {
                        TypeJobDesk = sdr["UserID"].ToString()
                    });
                }
            }
            lstNonFisik.DataSource = arrData;
            lstNonFisik.DataBind();
        }
        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBagian();
        }
        private void LoadBagian()
        {
            ArrayList arrUser = new ArrayList();
            JobDeskFacade usFac = new JobDeskFacade();
            //usFac.Criteria = " DeptID=" + ddlDept.SelectedValue.ToString();
            usFac.Criteria = " DeptID=" + Convert.ToInt32(ddlDept.SelectedValue.ToString());
            arrUser = usFac.RetriveJabatan();
            //ddlBagian.Items.Clear();
            List<ListItem> lte = new List<ListItem>();
            if (ddlDept.SelectedIndex > 0)
            {
                ddlBagian.Items.Add(new ListItem("--Pilih Jabatan--", string.Empty));
                foreach (JobDesk user in arrUser)
                {
                    ddlBagian.Items.Add(new ListItem(user.BagianName, user.BagianName));
                }
            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();

            JobDesk jobdesk = new JobDesk();

            int jobdeskID = 0;
            jobdesk.DeptID = Convert.ToInt32(ddlDept.SelectedValue.ToString());
            jobdesk.BagianName = ddlBagian.SelectedValue.ToString();
            jobdesk.Atasan = txtAtasan.Text;
            jobdesk.TglSusun = DateTime.Parse(txtTglSusun.Text);
            jobdesk.Revisi = Convert.ToInt32(txtRevisi.Text);
            jobdesk.CreatedBy = users.UserName;
            //ZetroView zl = new ZetroView();
            //zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "insert into JobDesk (JOBDESK_No, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, ApproveDate1, RowStatus, Pendidikan, Pengalaman, AlasanCancel)" +
            //    "values ('', " + jobdesk.DeptID + ", " + jobdesk.BagianName + ", " + jobdesk.Atasan + ", " + jobdesk.TglSusun + ", " + jobdesk.Revisi + ", 0, 0, " + users.UserName + ", " + jobdesk.TglSusun + ", " + users.UserName + ", " + jobdesk.TglSusun + ", " + jobdesk.TglSusun + ", 0, '', '', '')";
            //SqlDataReader sdr = zl.Retrieve();

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new JobDeskFacade(jobdesk);
            int intresult = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            jobdeskID = intresult;
            if (jobdeskID > 0)
            {
                JobdeskDetail jobdeskDetail = new JobdeskDetail();
                int i = 0;
                foreach (RepeaterItem objItem in lstBawahan.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtBawahan = (TextBox)lstBawahan.Items[i].FindControl("txtBawahan");

                    if (txtBawahan.Text != string.Empty)
                    {
                        jobdeskDetail.Bawahan = txtBawahan.Text;
                    }
                    //ZetroView zl1 = new ZetroView();
                    //zl1.QueryType = Operation.CUSTOM;
                    //zl1.CustomQuery = "insert into JobDeskBawahan (JOBDESKID, Bawahan, RowStatus) " +
                    //    "values (" + jobdeskDetail.JOBDESKID + ", " + txtBawahan.Text + ", 0)";
                    ////zl.CustomQuery = "exec spd_updateTransPrs " + pattern.Replace(decimal.Parse(txtActual.Text).ToString(), ".") + "," + lblTercapai.ToolTip;
                    //SqlDataReader sdr1 = zl1.Retrieve();
                    absTrans = new JobDeskBawahanFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    i = i + 1;
                }

                int j = 0;
                foreach (RepeaterItem objItem in lstTUJ.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtTUJabatan = (TextBox)lstTUJ.Items[j].FindControl("txtTUJabatan");

                    if (txtTUJabatan.Text != string.Empty)
                    {
                        jobdeskDetail.TUJabatan = txtTUJabatan.Text;
                    }
                    absTrans = new JobDeskTUJabatanFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    j = j + 1;
                }

                int k = 0;
                foreach (RepeaterItem objItem in lstTPJ.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtTPJabatan = (TextBox)lstTPJ.Items[k].FindControl("txtTPJabatan");

                    if (txtTPJabatan.Text != string.Empty)
                    {
                        jobdeskDetail.TPJabatan = txtTPJabatan.Text;
                    }
                    absTrans = new JobDeskTPJabatanFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    k = k + 1;
                }

                int l = 0;
                foreach (RepeaterItem objItem in lstHK.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtHK = (TextBox)lstHK.Items[l].FindControl("txtHK");

                    if (txtHK.Text != string.Empty)
                    {
                        jobdeskDetail.HubunganKerja = txtHK.Text;
                    }
                    absTrans = new JobDeskHubunganKerjaFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    l = l + 1;
                }

                int m = 0;
                foreach (RepeaterItem objItem in lstTJ.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtTanggungJawab = (TextBox)lstTJ.Items[m].FindControl("txtTanggungJawab");

                    if (txtTanggungJawab.Text != string.Empty)
                    {
                        jobdeskDetail.TanggungJawab = txtTanggungJawab.Text;
                    }
                    absTrans = new JobDeskTanggungJawabFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    m = m + 1;
                }

                int n = 0;
                foreach (RepeaterItem objItem in lstWewenang.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtWewenang = (TextBox)lstWewenang.Items[n].FindControl("txtWewenang");

                    if (txtWewenang.Text != string.Empty)
                    {
                        jobdeskDetail.Wewenang = txtWewenang.Text;
                    }
                    absTrans = new JobDeskWewenangFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    n = n + 1;
                }

                int o = 0;
                foreach (RepeaterItem objItem in lstPend.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtPendidikan = (TextBox)lstPend.Items[o].FindControl("txtPendidikan");

                    if (txtPendidikan.Text != string.Empty)
                    {
                        jobdeskDetail.Pendidikan = txtPendidikan.Text;
                    }
                    absTrans = new JobDeskPendidikanFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    o = o + 1;
                }

                int p = 0;
                foreach (RepeaterItem objItem in lstPengalaman.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtPengalaman = (TextBox)lstPengalaman.Items[p].FindControl("txtPengalaman");

                    if (txtPengalaman.Text != string.Empty)
                    {
                        jobdeskDetail.Pengalaman = txtPengalaman.Text;
                    }
                    absTrans = new JobDeskPengalamanFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    p = p + 1;
                }

                int q = 0;
                foreach (RepeaterItem objItem in lstPengetahuan.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtPengetahuan = (TextBox)lstPengetahuan.Items[q].FindControl("txtPengetahuan");

                    if (txtPengetahuan.Text != string.Empty)
                    {
                        jobdeskDetail.Pengetahuan = txtPengetahuan.Text;
                    }
                    absTrans = new JobDeskPengetahuanFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    q = q + 1;
                }

                int r = 0;
                foreach (RepeaterItem objItem in lstKeterampilan.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtKeterampilan = (TextBox)lstKeterampilan.Items[r].FindControl("txtKeterampilan");

                    if (txtKeterampilan.Text != string.Empty)
                    {
                        jobdeskDetail.Keterampilan = txtKeterampilan.Text;
                    }
                    absTrans = new JobDeskKeterampilanFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    r = r + 1;
                }

                int s = 0;
                foreach (RepeaterItem objItem in lstFisik.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtFisik = (TextBox)lstFisik.Items[s].FindControl("txtFisik");

                    if (txtFisik.Text != string.Empty)
                    {
                        jobdeskDetail.Fisik = txtFisik.Text;
                    }
                    absTrans = new JobDeskFisikFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    s = s + 1;
                }

                int t = 0;
                foreach (RepeaterItem objItem in lstNonFisik.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtNonFisik = (TextBox)lstNonFisik.Items[t].FindControl("txtNonFisik");

                    if (txtNonFisik.Text != string.Empty)
                    {
                        jobdeskDetail.NonFisik = txtNonFisik.Text;
                    }
                    absTrans = new JobDeskNonFisikFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    t = t + 1;
                }

                int u = 0;
                foreach (RepeaterItem objItem in lstUsia.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    jobdeskDetail.JOBDESKID = jobdeskID;
                    TextBox txtUsia = (TextBox)lstUsia.Items[u].FindControl("txtUsia");

                    if (txtUsia.Text != string.Empty)
                    {
                        jobdeskDetail.Usia = txtUsia.Text;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Data Usia harus di isi !");
                        return;
                    }
                    absTrans = new JobDeskUsiaFacade(jobdeskDetail);
                    intresult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    u = u + 1;
                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            DisplayAJAXMessage(this, "Berhasil Disimpan");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            JobdeskDetail jobdesk = new JobdeskDetail();

            int jobdeskID = 0;
            jobdesk.ID = Convert.ToInt32(txtID.Text);
            //jobdesk.DeptID = Convert.ToInt32(ddlDept.SelectedValue.ToString());
            //jobdesk.BagianName = ddlBagian.SelectedValue.ToString();
            jobdesk.Atasan = txtAtasan.Text;
            jobdesk.TglSusun = DateTime.Parse(txtTglSusun.Text);
            jobdesk.Revisi = Convert.ToInt32(txtRevisi.Text);
            jobdesk.CreatedBy = users.UserName;
            //ZetroView zl = new ZetroView();
            //zl.QueryType = Operation.CUSTOM;
            //zl.CustomQuery = "insert into JobDesk (JOBDESK_No, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, ApproveDate1, RowStatus, Pendidikan, Pengalaman, AlasanCancel)" +
            //    "values ('', " + jobdesk.DeptID + ", " + jobdesk.BagianName + ", " + jobdesk.Atasan + ", " + jobdesk.TglSusun + ", " + jobdesk.Revisi + ", 0, 0, " + users.UserName + ", " + jobdesk.TglSusun + ", " + users.UserName + ", " + jobdesk.TglSusun + ", " + jobdesk.TglSusun + ", 0, '', '', '')";
            //SqlDataReader sdr = zl.Retrieve();

            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacade absTrans = new JobDeskDetailFacade(jobdesk);
            int intresult = absTrans.Update(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            jobdeskID = intresult;
            if (txtID.Text != string.Empty)
            {
                JobdeskDetail jobdeskDetail = new JobdeskDetail();
                int i = 0;
                foreach (RepeaterItem objItem in lstBawahan.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtBawahan = (TextBox)lstBawahan.Items[i].FindControl("txtBawahan");
                    TextBox IDBawahan = (TextBox)lstBawahan.Items[i].FindControl("IDBawahan");

                    if (txtBawahan.Text != string.Empty)
                    {
                        jobdeskDetail.Bawahan = txtBawahan.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDBawahan.Text);
                    }

                    absTrans = new JobDeskBawahanFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    i = i + 1;
                }

                int j = 0;
                foreach (RepeaterItem objItem in lstTUJ.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtTUJabatan = (TextBox)lstTUJ.Items[j].FindControl("txtTUJabatan");
                    TextBox IDTUJabatan = (TextBox)lstTUJ.Items[j].FindControl("IDTUJabatan");

                    if (txtTUJabatan.Text != string.Empty)
                    {
                        jobdeskDetail.TUJabatan = txtTUJabatan.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDTUJabatan.Text);
                    }
                    absTrans = new JobDeskTUJabatanFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    j = j + 1;
                }

                int k = 0;
                foreach (RepeaterItem objItem in lstTPJ.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtTPJabatan = (TextBox)lstTPJ.Items[k].FindControl("txtTPJabatan");
                    TextBox IDTPJabatan = (TextBox)lstTPJ.Items[k].FindControl("IDTPJabatan");

                    if (txtTPJabatan.Text != string.Empty)
                    {
                        jobdeskDetail.TPJabatan = txtTPJabatan.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDTPJabatan.Text);
                    }
                    absTrans = new JobDeskTPJabatanFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    k = k + 1;
                }

                int l = 0;
                foreach (RepeaterItem objItem in lstHK.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtHK = (TextBox)lstHK.Items[l].FindControl("txtHK");
                    TextBox IDHK = (TextBox)lstHK.Items[l].FindControl("IDHK");

                    if (txtHK.Text != string.Empty)
                    {
                        jobdeskDetail.HubunganKerja = txtHK.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDHK.Text);
                    }
                    absTrans = new JobDeskHubunganKerjaFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    l = l + 1;
                }

                int m = 0;
                foreach (RepeaterItem objItem in lstTJ.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtTanggungJawab = (TextBox)lstTJ.Items[m].FindControl("txtTanggungJawab");
                    TextBox IDTanggungJawab = (TextBox)lstTJ.Items[m].FindControl("IDTanggungJawab");

                    if (txtTanggungJawab.Text != string.Empty)
                    {
                        jobdeskDetail.TanggungJawab = txtTanggungJawab.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDTanggungJawab.Text);
                    }
                    absTrans = new JobDeskTanggungJawabFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    m = m + 1;
                }

                int n = 0;
                foreach (RepeaterItem objItem in lstWewenang.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtWewenang = (TextBox)lstWewenang.Items[n].FindControl("txtWewenang");
                    TextBox IDWewenang = (TextBox)lstWewenang.Items[n].FindControl("IDWewenang");

                    if (txtWewenang.Text != string.Empty)
                    {
                        jobdeskDetail.Wewenang = txtWewenang.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDWewenang.Text);
                    }
                    absTrans = new JobDeskWewenangFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    n = n + 1;
                }

                int o = 0;
                foreach (RepeaterItem objItem in lstPend.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtPendidikan = (TextBox)lstPend.Items[o].FindControl("txtPendidikan");
                    TextBox IDPendidikan = (TextBox)lstPend.Items[o].FindControl("IDPendidikan");

                    if (txtPendidikan.Text != string.Empty)
                    {
                        jobdeskDetail.Pendidikan = txtPendidikan.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDPendidikan.Text);
                    }
                    absTrans = new JobDeskPendidikanFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    o = o + 1;
                }

                int p = 0;
                foreach (RepeaterItem objItem in lstPengalaman.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtPengalaman = (TextBox)lstPengalaman.Items[p].FindControl("txtPengalaman");
                    TextBox IDPengalaman = (TextBox)lstPengalaman.Items[p].FindControl("IDPengalaman");

                    if (txtPengalaman.Text != string.Empty)
                    {
                        jobdeskDetail.Pengalaman = txtPengalaman.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDPengalaman.Text);
                    }
                    absTrans = new JobDeskPengalamanFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    p = p + 1;
                }

                int q = 0;
                foreach (RepeaterItem objItem in lstPengetahuan.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtPengetahuan = (TextBox)lstPengetahuan.Items[q].FindControl("txtPengetahuan");
                    TextBox IDPengetahuan = (TextBox)lstPengetahuan.Items[q].FindControl("IDPengetahuan");

                    if (txtPengetahuan.Text != string.Empty)
                    {
                        jobdeskDetail.Pengetahuan = txtPengetahuan.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDPengetahuan.Text);
                    }
                    absTrans = new JobDeskPengetahuanFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    q = q + 1;
                }

                int r = 0;
                foreach (RepeaterItem objItem in lstKeterampilan.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtKeterampilan = (TextBox)lstKeterampilan.Items[r].FindControl("txtKeterampilan");
                    TextBox IDKeterampilan = (TextBox)lstKeterampilan.Items[r].FindControl("IDKeterampilan");

                    if (txtKeterampilan.Text != string.Empty)
                    {
                        jobdeskDetail.Keterampilan = txtKeterampilan.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDKeterampilan.Text);
                    }
                    absTrans = new JobDeskKeterampilanFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    r = r + 1;
                }

                int s = 0;
                foreach (RepeaterItem objItem in lstFisik.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtFisik = (TextBox)lstFisik.Items[s].FindControl("txtFisik");
                    TextBox IDFisik = (TextBox)lstFisik.Items[s].FindControl("IDFisik");

                    if (txtFisik.Text != string.Empty)
                    {
                        jobdeskDetail.Fisik = txtFisik.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDFisik.Text);
                    }
                    absTrans = new JobDeskFisikFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    s = s + 1;
                }

                int t = 0;
                foreach (RepeaterItem objItem in lstNonFisik.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtNonFisik = (TextBox)lstNonFisik.Items[t].FindControl("txtNonFisik");
                    TextBox IDNonFisik = (TextBox)lstNonFisik.Items[t].FindControl("IDNonFisik");

                    if (txtNonFisik.Text != string.Empty)
                    {
                        jobdeskDetail.NonFisik = txtNonFisik.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDNonFisik.Text);
                    }
                    absTrans = new JobDeskNonFisikFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    t = t + 1;
                }

                int u = 0;
                foreach (RepeaterItem objItem in lstUsia.Items)
                {
                    jobdeskDetail = new JobdeskDetail();
                    TextBox txtUsia = (TextBox)lstUsia.Items[u].FindControl("txtUsia");
                    TextBox IDUsia = (TextBox)lstUsia.Items[u].FindControl("IDUsia");

                    if (txtUsia.Text != string.Empty)
                    {
                        jobdeskDetail.Usia = txtUsia.Text;
                        jobdeskDetail.ID = Convert.ToInt32(IDUsia.Text);
                    }
                    absTrans = new JobDeskUsiaFacade(jobdeskDetail);
                    intresult = absTrans.Update(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    u = u + 1;
                }
            }

            transManager.CommitTransaction();
            transManager.CloseConnection();
            DisplayAJAXMessage(this, "Data Berhasil Diupdate");
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        //protected void lstPrs_DataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Users users = (Users)Session["Users"];
        //    JobDesk ba = (JobDesk)e.Item.DataItem;
        //    TextBox txtTarget = (TextBox)e.Item.FindControl("txtTarget");
        //}
        protected void btnLaporan_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("LapUPDJobDesk.aspx");
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListJobDesk"] = null;
            Session["ID"] = null;

            Response.Redirect("ListUPDJobDesk.aspx");
        }

        protected void btnBaru_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("JobDeskInput.aspx");
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            string strQuery = "SELECT ID, DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, RowStatus " +
                                "FROM JobDesk WHERE RowStatus>-1 AND " +
                                "ID='" + txtID.Text.Trim() + "' ";
            string strQuery1 = "SELECT jd.ID, jdd.Bawahan FROM JobDesk as jd inner join JobDeskBawahan as jdd on jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 and jdd.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery2 = "SELECT jd.ID, jdd.TujuanUmumJabatan FROM JobDesk as jd inner join JobDeskTUJabatan as jdd on jd.ID=jdd.JOBDESKID WHERE jd.RowStatus>-1 and jdd.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery3 = "SELECT jd.ID, jdp.TugasPokokJabatan FROM JobDesk as jd inner join JobDeskTPJabatan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery4 = "SELECT jd.ID, jdp.HubunganKerja FROM JobDesk as jd inner join JobDeskHK as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery5 = "SELECT jd.ID, jdp.TanggungJawab FROM JobDesk as jd inner join JobDeskTJ as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery6 = "SELECT jd.ID, jdp.Wewenang FROM JobDesk as jd inner join JobDeskWewenang as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery7 = "SELECT jd.ID, jdp.Pendidikan FROM JobDesk as jd inner join JobDeskPendidikan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery8 = "SELECT jd.ID, jdp.Pengalaman FROM JobDesk as jd inner join JobDeskPengalaman as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery9 = "SELECT jd.ID, jdp.Pengetahuan FROM JobDesk as jd inner join JobDeskPengetahuan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery10 = "SELECT jd.ID, jdp.Keterampilan FROM JobDesk as jd inner join JobDeskKeterampilan as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery11 = "SELECT jd.ID, jdp.Fisik FROM JobDesk as jd inner join JobDeskFisik as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery12 = "SELECT jd.ID, jdp.NonFisik FROM JobDesk as jd inner join JobDeskNonFisik as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";
            string strQuery13 = "SELECT jd.ID, jdp.Usia FROM JobDesk as jd inner join JobDeskUsia as jdp on jd.ID=jdp.JOBDESKID WHERE jd.RowStatus>-1 and jdp.RowStatus>-1 AND jd.ID='" + txtID.Text.Trim() + "' ";

            if (txtID.Text.Trim() != string.Empty)
            {
                Session["Query"] = strQuery;
                Session["Query1"] = strQuery1;
                Session["Query2"] = strQuery2;
                Session["Query3"] = strQuery3;
                Session["Query4"] = strQuery4;
                Session["Query5"] = strQuery5;
                Session["Query6"] = strQuery6;
                Session["Query7"] = strQuery7;
                Session["Query8"] = strQuery8;
                Session["Query9"] = strQuery9;
                Session["Query10"] = strQuery10;
                Session["Query11"] = strQuery11;
                Session["Query12"] = strQuery12;
                Session["Query13"] = strQuery13;
                Cetak(this);
            }
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('Report.aspx?IdReport=jobdesk', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 780px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}