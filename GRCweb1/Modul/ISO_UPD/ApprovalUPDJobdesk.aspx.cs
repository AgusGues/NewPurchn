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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class ApprovalUPDJobdesk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = (Users)Session["Users"];
                //LoadDept();
                LoadOpenJOBDESK();
                Users user = ((Users)Session["Users"]);
                JobDeskFacade jobdeskFacade = new JobDeskFacade();
                JobDesk smt = new JobDesk();
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = jobdeskFacade.GetUserType(UserID);
                Session["usertype"] = usertype;

                if (ID.Value != string.Empty)
                {
                    string[] ListJobdesk = ID.Value.Split(',');
                    string[] ListOpenJobdesk = ListJobdesk.Distinct().ToArray();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    LoadOpenJOBDESK(ListOpenJobdesk[0].ToString());
                    //btnApprove.Enabled = true;
                    //btnNotApprove.Enabled = true;
                    //btnNext.Enabled = (ListOpenJobdesk.Count() > 1) ? true : false;
                    ViewState["index"] = idx;
                }
                else
                {
                    //btnApprove.Enabled = false;
                    //btnNext.Enabled = false;
                }
                string[] ListOpenJOBDESKx = ID.Value.Split(',');
                string[] ListOpenJOBDESKd = ListOpenJOBDESKx.Distinct().ToArray();
                int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
                //btnNext.Enabled = ((idxx - 1) >= ListOpenJOBDESKd.Count()) ? false : true;
                ViewState["index"] = idxx;
                //txtAlasan.Attributes.Add("onkeyup", "onKeyUp()");
                if (Request.QueryString["ID"] != null)
                {
                    LoadOpenJOBDESK(Request.QueryString["ID"].ToString());
                }

            }
        }
        private void LoadDept()
        {
            ArrayList arrDept = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrDept = masterDFacade.RetrieveDept();
            ddlDept.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.namaDept, dept.idDept.ToString()));
            }
        }

        private void LoadOpenJOBDESK()
        {
            Users user = ((Users)Session["Users"]);
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskFacade = new JobDeskFacade();
            JobDesk jobdesk = new JobDesk();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string Apv = jobdeskFacade.GetApv(UserID);
            string ApvJOBDESK = jobdeskFacade.GetStatusApv(UserID);
            Session["Apv"] = Apv;
            Session["ApvJOBDESK"] = ApvJOBDESK;
            string UserInput = jobdeskFacade.GetUserI(UserID);
            string UserInput1 = jobdeskFacade.GetUserIGrid(UserID);
            Session["UserInput"] = UserInput;
            Session["UserInput1"] = UserInput1;
            arrJOBDESK = jobdeskFacade.RetrieveOpenJOBDESKHeader(UserInput, Apv);
            foreach (JobDesk jobdesk1 in arrJOBDESK)
            {
                if (jobdesk1.ID != 0)
                {

                    ID.Value += jobdesk1.ID + ",";
                }
            }
            Session.Remove("AlasanCancel");
            btnNotApprove.Attributes.Add("onclick", "return confirm_revisi();");
            btnTidakIkutRevisi.Attributes.Add("onclick", "return confirm_notrevisi();");

        }
        private void LoadOpenJOBDESK(string ID)
        {
            Users user = (Users)Session["Users"];
            JobDeskFacade jobdeskf = new JobDeskFacade();
            JobDesk jobdesk = new JobDesk();
            //ArrayList arrDept = new ArrayList();
            ddlDept.Items.Clear();
            string UserInput = Session["UserInput"].ToString();
            jobdesk = jobdeskf.RetrieveJOBDESKNum(ID);

            string dpName = string.Empty;
            //txtJOBDESK_No.Text = jobdesk.JOBDESK_No;
            txtTglSusun.Text = jobdesk.TglSusun.ToString("dd-MMM-yyyy");
            dpName = (jobdesk.DeptID == 4 || jobdesk.DeptID == 5 || jobdesk.DeptID == 18 || jobdesk.DeptID == 19) ? "Maintenance" : jobdesk.DeptID.ToString();
            if (jobdesk.DeptID == 4 || jobdesk.DeptID == 5 || jobdesk.DeptID == 18 || jobdesk.DeptID == 19)
            {
                ddlDept.Items.Add(new ListItem(dpName, jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 2)
            {
                ddlDept.Items.Add(new ListItem("Boardmill", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 3)
            {
                ddlDept.Items.Add(new ListItem("Finishing", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 6)
            {
                ddlDept.Items.Add(new ListItem("Logistik Produk Jadi", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 7)
            {
                ddlDept.Items.Add(new ListItem("Hrd & GA", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 9)
            {
                ddlDept.Items.Add(new ListItem("Quality Assurance", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 10)
            {
                ddlDept.Items.Add(new ListItem("Logistik Bahan Baku", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 11)
            {
                ddlDept.Items.Add(new ListItem("Ppic", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 12)
            {
                ddlDept.Items.Add(new ListItem("Finance", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 13)
            {
                ddlDept.Items.Add(new ListItem("Marketing", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 14)
            {
                ddlDept.Items.Add(new ListItem("IT", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 15)
            {
                ddlDept.Items.Add(new ListItem("Purchasing", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 22)
            {
                ddlDept.Items.Add(new ListItem("Project Sipil", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 23)
            {
                ddlDept.Items.Add(new ListItem("Iso", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 24)
            {
                ddlDept.Items.Add(new ListItem("Accounting", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 26)
            {
                ddlDept.Items.Add(new ListItem("Armada", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 27)
            {
                ddlDept.Items.Add(new ListItem("Manager Corporate", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 28)
            {
                ddlDept.Items.Add(new ListItem("Product Development", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.DeptID == 30)
            {
                ddlDept.Items.Add(new ListItem("Research & Development", jobdesk.DeptID.ToString()));
            }
            if (jobdesk.Status == 1)
            {
                btnApprove.Visible = false;
                btnNotApprove.Visible = false;
                btnIkutRevisi.Visible = true;
                btnTidakIkutRevisi.Visible = true;
            }
            //ddlDept.Items.Add(new ListItem(dpName, jobdesk.DeptID.ToString()));
            //ddlDept.SelectedValue = jobdesk.DeptID.ToString();
            ddlDept.Enabled = false;
            txtAtasan.Text = jobdesk.Atasan;
            txtRevisi.Text = jobdesk.Revisi.ToString();
            //txtApv.Text = jobdesk.Approval2;
            //txtJabatan.Text = jobdesk.BagianName;
            txtBagian.Text = jobdesk.BagianName.ToString();
            txtID.Text = jobdesk.ID.ToString();
            LoadDataGridTUJ(jobdesk.ID);
            LoadDataGridBawahan(jobdesk.ID);
            LoadDataGridTPJ(jobdesk.ID);
            LoadDataGridHK(jobdesk.ID);
            LoadDataGridTJ(jobdesk.ID);
            LoadDataGridWewenang(jobdesk.ID);
            LoadDataGridPendidikan(jobdesk.ID);
            LoadDataGridPengalaman(jobdesk.ID);
            LoadDataGridPengetahuan(jobdesk.ID);
            LoadDataGridKeterampilan(jobdesk.ID);
            LoadDataGridFisik(jobdesk.ID);
            LoadDataGridNonFisik(jobdesk.ID);
            LoadDataGridUsia(jobdesk.ID);
        }
        private void LoadDataGridTUJ(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESK2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESK3(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstTUJ.DataSource = arrJOBDESK;
            lstTUJ.DataBind();
        }
        private void LoadDataGridBawahan(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKBawahan(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKBawahan2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstBawahan.DataSource = arrJOBDESK;
            lstBawahan.DataBind();
        }
        private void LoadDataGridTPJ(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKTPJ(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKTPJ2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstTPJ.DataSource = arrJOBDESK;
            lstTPJ.DataBind();
        }
        private void LoadDataGridHK(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKHK(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKHK2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstHK.DataSource = arrJOBDESK;
            lstHK.DataBind();
        }
        private void LoadDataGridTJ(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKTJ(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKTJ2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstTJ.DataSource = arrJOBDESK;
            lstTJ.DataBind();
        }
        private void LoadDataGridWewenang(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKWewenang(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKWewenang2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstWewenang.DataSource = arrJOBDESK;
            lstWewenang.DataBind();
        }
        private void LoadDataGridPendidikan(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKPendidikan(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKPendidikan2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstPend.DataSource = arrJOBDESK;
            lstPend.DataBind();
        }
        private void LoadDataGridPengalaman(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKPengalaman(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKPengalaman2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstPengalaman.DataSource = arrJOBDESK;
            lstPengalaman.DataBind();
        }
        private void LoadDataGridPengetahuan(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKPengetahuan(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKPengetahuan2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstPengetahuan.DataSource = arrJOBDESK;
            lstPengetahuan.DataBind();
        }
        private void LoadDataGridKeterampilan(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKKeterampilan(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKKeterampilan2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstKeterampilan.DataSource = arrJOBDESK;
            lstKeterampilan.DataBind();
        }
        private void LoadDataGridFisik(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKFisik(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKFisik2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstFisik.DataSource = arrJOBDESK;
            lstFisik.DataBind();
        }
        private void LoadDataGridNonFisik(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKNonFisik(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKNonFisik2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstNonFisik.DataSource = arrJOBDESK;
            lstNonFisik.DataBind();
        }
        private void LoadDataGridUsia(int JOBDESKID)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            string UserID = users.ID.ToString();
            string Apv = jobdeskf.GetApv(UserID);
            if (Convert.ToInt32(Apv) == 1)
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKUsia(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            else
            {
                arrJOBDESK = jobdeskf.RetrieveJOBDESKUsia2(JOBDESKID, Convert.ToInt32(Apv), Convert.ToInt32(UserID));
            }
            lstUsia.DataSource = arrJOBDESK;
            lstUsia.DataBind();
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ApproveJOBDESK();

            LoadOpenJOBDESK();
            //LoadDataGridViewItem(LoadDataGrid());
            //LoadDept();
            Users user = ((Users)Session["Users"]);
            JobDeskFacade jobdeskFacade = new JobDeskFacade();
            JobDesk jobdesk = new JobDesk();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string usertype = jobdeskFacade.GetUserType(UserID);
            Session["usertype"] = usertype;
        }
        protected void btnNotApprove_Click(object sender, EventArgs e)
        {
            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Revisi harus diisi");
                    return;
                }
                NotApproveJOBDESK();

                LoadOpenJOBDESK();
                //LoadDataGridViewItem(LoadDataGrid());
                //LoadDept();
                Users user = ((Users)Session["Users"]);
                JobDeskFacade jobdeskFacade = new JobDeskFacade();
                JobDesk jobdesk = new JobDesk();
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = jobdeskFacade.GetUserType(UserID);
                Session["usertype"] = usertype;
            }
        }

        private void ApproveJOBDESK()
        {
            Users user = (Users)Session["Users"];
            JobDesk jobdesk = new JobDesk();
            JobDeskFacade jobdeskF = new JobDeskFacade();
            string strError = string.Empty;
            string UserID = user.ID.ToString();
            int Apv = Convert.ToInt32(user.Apv.ToString());
            //Convert.ToInt32(Session["Apv"].ToString());
            UserID = user.ID.ToString();
            string usertype = jobdeskF.GetUserType(UserID);

            jobdesk.Approval = Apv;
            jobdesk.ID = int.Parse(txtID.Text);
            jobdesk.User_ID = int.Parse(UserID);
            jobdesk.CreatedBy = user.UserName;
            jobdesk.Atasan = txtAtasan.Text;

            ArrayList arrJobDeskDetail = new ArrayList();
            JobDeskProcessFacade ProsesFacade = new JobDeskProcessFacade(jobdesk, arrJobDeskDetail);
            if (Convert.ToInt32(user.Apv.ToString()) == 3 && Convert.ToInt32(user.UserLevel.ToString()) == 2)
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDesk0", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "Approve";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //strError = ProsesFacade.Update0();
            }
            else if (Convert.ToInt32(user.Apv.ToString()) == 2 && Convert.ToInt32(user.RowStatus.ToString()) == 0)
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDesk0", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "Approve1";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else if (Convert.ToInt32(usertype.ToString()) == 4)
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDesk0", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "Approve2";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                strError = ProsesFacade.Update();
            }
            //LoadDataGridViewItem(LoadDataGrid());
            if (strError == string.Empty)
            {
                DisplayAJAXMessage(this, "Approval berhasil");
            }
            AutoNext();
        }
        private void NotApproveJOBDESK()
        {
            Users user = (Users)Session["Users"];
            JobDesk jobdesk = new JobDesk();
            Revisin ro = new Revisin();
            JobDeskFacade jobdeskF = new JobDeskFacade();
            //Revisine ro = new Revisine();
            string strError = string.Empty;
            string UserID = user.ID.ToString();
            int Apv = Convert.ToInt32(user.Apv.ToString());
            //Convert.ToInt32(Session["Apv"].ToString());
            UserID = user.ID.ToString();
            string usertype = jobdeskF.GetUserType(UserID);

            jobdesk.Approval = Apv;
            jobdesk.ID = int.Parse(txtID.Text);
            jobdesk.User_ID = int.Parse(UserID);
            jobdesk.CreatedBy = user.UserName;
            ro.AlasanRevisi = Session["AlasanCancel"].ToString();

            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd = new SqlCommand("spUpdateJobDesk1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "NotApprove";
            //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
            cmd.Parameters.Add("@AlasanCancel", SqlDbType.VarChar).Value = ro.AlasanRevisi;
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            if (strError == string.Empty)
            {
                DisplayAJAXMessage(this, "Not Approval berhasil");
            }
            AutoNext();
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private void AutoNext()
        {
            if (btnNext.Enabled == true)
            {
                btnNext_Click(null, null);
            }
            else if (btnPrev.Enabled == true)
            {
                btnPrev_Click(null, null);
            }
            else
            {
                Response.Redirect("JobDeskApproval.aspx");
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            string[] ListJOBDESK = ID.Value.Split(',');
            string[] ListOpenJOBDESK = ListJOBDESK.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenJOBDESK.Count()) ? false : true;
            LoadOpenJOBDESK(ListOpenJOBDESK[idx].ToString());
            ViewState["index"] = idx;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            string[] ListJOBDESK = ID.Value.Split(',');
            string[] ListOpenJOBDESK = ListJOBDESK.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            idx = (idx > ListOpenJOBDESK.Count() - 1) ? 0 : idx;
            btnNext.Enabled = ((idx) == ListOpenJOBDESK.Count()) ? false : true;
            try
            {
                ViewState["index"] = idx;
                LoadOpenJOBDESK(ListOpenJOBDESK[idx].ToString());
            }
            catch
            {
                LoadOpenJOBDESK(ListOpenJOBDESK[0].ToString());
                ViewState["index"] = 0;
            }
            btnPrev.Enabled = (idx > 0) ? true : false;
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

        protected void btnIkutRevisi_Click(object sender, EventArgs e)
        {
            IkutRevisiJOBDESK();

            LoadOpenJOBDESK();
            //LoadDataGridViewItem(LoadDataGrid());
            //LoadDept();
            Users user = ((Users)Session["Users"]);
            JobDeskFacade jobdeskFacade = new JobDeskFacade();
            JobDesk jobdesk = new JobDesk();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string usertype = jobdeskFacade.GetUserType(UserID);
            Session["usertype"] = usertype;
        }

        protected void btnTidakIkutRevisi_Click(object sender, EventArgs e)
        {
            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Revisi harus diisi");
                    return;
                }
                TidakIkutRevisiJOBDESK();

                LoadOpenJOBDESK();
                //LoadDataGridViewItem(LoadDataGrid());
                //LoadDept();
                Users user = ((Users)Session["Users"]);
                JobDeskFacade jobdeskFacade = new JobDeskFacade();
                JobDesk jobdesk = new JobDesk();
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = jobdeskFacade.GetUserType(UserID);
                Session["usertype"] = usertype;
            }
        }

        private void IkutRevisiJOBDESK()
        {
            Users user = ((Users)Session["Users"]);
            if (user.UnitKerjaID == 7)
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDeskDistribusi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "IkutRevisi";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=2, Approval=6, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();


                SqlConnection conn2 = new SqlConnection(@"Initial Catalog=bpasjombang;Data Source=sqljombang.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=2, Approval=6, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();
            }
            else if (user.UnitKerjaID == 1)
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDeskDistribusi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "IkutRevisi";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=2, Approval=6, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "' where Jabatan='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();

                SqlConnection conn2 = new SqlConnection(@"Initial Catalog=bpasjombang;Data Source=sqljombang.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=2, Approval=6, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDeskDistribusi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "IkutRevisi";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=2, Approval=6, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "' where Jabatan='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();

                SqlConnection conn2 = new SqlConnection(@"Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=2, Approval=6, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();
            }
            Response.Redirect("ApprovalUPDJobdesk.aspx");

        }

        private void TidakIkutRevisiJOBDESK()
        {
            Users user = ((Users)Session["Users"]);
            if (user.UnitKerjaID == 7)
            {
                Revisin ro = new Revisin();
                ro.AlasanRevisi = Session["AlasanCancel"].ToString();

                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDesk1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "TidakIkutRevisi";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                cmd.Parameters.Add("@AlasanCancel", SqlDbType.VarChar).Value = ro.AlasanRevisi;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=-2, Approval=2, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "', AlasanTidakIkutRevisi='" + ro.AlasanRevisi + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();


                SqlConnection conn2 = new SqlConnection(@"Initial Catalog=bpasjombang;Data Source=sqljombang.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=-2, Approval=2, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "', AlasanTidakIkutRevisi='" + ro.AlasanRevisi + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();

            }
            else if (user.UnitKerjaID == 1)
            {
                Revisin ro = new Revisin();
                ro.AlasanRevisi = Session["AlasanCancel"].ToString();

                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDesk1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "TidakIkutRevisi";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                cmd.Parameters.Add("@AlasanCancel", SqlDbType.VarChar).Value = ro.AlasanRevisi;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=-2, Approval=2, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "', AlasanTidakIkutRevisi='" + ro.AlasanRevisi + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();

                SqlConnection conn2 = new SqlConnection(@"Initial Catalog=bpasjombang;Data Source=sqljombang.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=-2, Approval=2, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "', AlasanTidakIkutRevisi='" + ro.AlasanRevisi + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();
            }
            else
            {
                Revisin ro = new Revisin();
                ro.AlasanRevisi = Session["AlasanCancel"].ToString();

                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDesk1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "TidakIkutRevisi";
                //cmd.Parameters.AddWithValue("@ID", txtApproved.ToString());
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                cmd.Parameters.Add("@AlasanCancel", SqlDbType.VarChar).Value = ro.AlasanRevisi;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=-2, Approval=2, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "', AlasanTidakIkutRevisi='" + ro.AlasanRevisi + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();

                SqlConnection conn2 = new SqlConnection(@"Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=-2, Approval=2, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "', AlasanTidakIkutRevisi='" + ro.AlasanRevisi + "' where ID='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();
            }
            Response.Redirect("ApprovalUPDJobdesk.aspx");
            
        }
    }
}

public class Revisin : JobDesk
{
    public string AlasanRevisi { get; set; }
    //public int Approval { get; set; }
}