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
    public partial class JobDeskDistribusi : System.Web.UI.Page
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
                    ViewState["index"] = idx;
                }
                else
                {
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
            arrJOBDESK = jobdeskFacade.RetrieveOpenJobDeskDistribusi();
            foreach (JobDesk jobdesk1 in arrJOBDESK)
            {
                if (jobdesk1.ID != 0)
                {

                    ID.Value += jobdesk1.ID + ",";
                }
            }
            //Session.Remove("AlasanCancel");
            //btnNotApprove.Attributes.Add("onclick", "return confirm_revisi();");

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
            if (jobdesk.Status == 2)
            {
                btnDistribusi.Visible = false;
                btnAktivasi.Visible = true;
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
            LoadDataGridTUJDistribusi(jobdesk.ID);
            LoadDataGridBawahanDistribusi(jobdesk.ID);
            LoadDataGridTPJDistribusi(jobdesk.ID);
            LoadDataGridHKDistribusi(jobdesk.ID);
            LoadDataGridTJDistribusi(jobdesk.ID);
            LoadDataGridWewenangDistribusi(jobdesk.ID);
            LoadDataGridPendidikanDistribusi(jobdesk.ID);
            LoadDataGridPengalamanDistribusi(jobdesk.ID);
            LoadDataGridUsiaDistribusi(jobdesk.ID);
            LoadDataGridPengetahuanDistribusi(jobdesk.ID);
            LoadDataGridKeterampilanDistribusi(jobdesk.ID);
            LoadDataGridFisikDistribusi(jobdesk.ID);
            LoadDataGridNonFisikDistribusi(jobdesk.ID);
        }
        private void LoadDataGridTUJDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKTUJDistribusi(JOBDESKID);
            lstTUJ.DataSource = arrJOBDESK;
            lstTUJ.DataBind();
        }
        private void LoadDataGridBawahanDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKBawahanDistribusi(JOBDESKID);
            lstBawahan.DataSource = arrJOBDESK;
            lstBawahan.DataBind();
        }
        private void LoadDataGridTPJDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKTPJDistribusi(JOBDESKID);
            lstTPJ.DataSource = arrJOBDESK;
            lstTPJ.DataBind();
        }
        private void LoadDataGridHKDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKHKDistribusi(JOBDESKID);
            lstHK.DataSource = arrJOBDESK;
            lstHK.DataBind();
        }
        private void LoadDataGridTJDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKTJDistribusi(JOBDESKID);
            lstTJ.DataSource = arrJOBDESK;
            lstTJ.DataBind();
        }
        private void LoadDataGridWewenangDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKWewenangDistribusi(JOBDESKID);
            lstWewenang.DataSource = arrJOBDESK;
            lstWewenang.DataBind();
        }
        private void LoadDataGridPendidikanDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKPendidikanDistribusi(JOBDESKID);
            lstPend.DataSource = arrJOBDESK;
            lstPend.DataBind();
        }
        private void LoadDataGridPengalamanDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKPengalamanDistribusi(JOBDESKID);
            lstPengalaman.DataSource = arrJOBDESK;
            lstPengalaman.DataBind();
        }
        private void LoadDataGridUsiaDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKUsiaDistribusi(JOBDESKID);
            lstUsia.DataSource = arrJOBDESK;
            lstUsia.DataBind();
        }
        private void LoadDataGridPengetahuanDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKPengetahuanDistribusi(JOBDESKID);
            lstPengetahuan.DataSource = arrJOBDESK;
            lstPengetahuan.DataBind();
        }
        private void LoadDataGridKeterampilanDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKKeterampilanDistribusi(JOBDESKID);
            lstKeterampilan.DataSource = arrJOBDESK;
            lstKeterampilan.DataBind();
        }
        private void LoadDataGridFisikDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKFisikDistribusi(JOBDESKID);
            lstFisik.DataSource = arrJOBDESK;
            lstFisik.DataBind();
        }
        private void LoadDataGridNonFisikDistribusi(int JOBDESKID)
        {
            ArrayList arrJOBDESK = new ArrayList();
            JobDeskFacade jobdeskf = new JobDeskFacade();
            arrJOBDESK = jobdeskf.RetrieveJOBDESKNonFisikDistribusi(JOBDESKID);
            lstNonFisik.DataSource = arrJOBDESK;
            lstNonFisik.DataBind();
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

        protected void btnDistribusi_Click(object sender, EventArgs e)
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

            if (user.UnitKerjaID == 7)
            {
                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "INSERT INTO JobDesk (DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, ApproveDate1, RowStatus, AlasanCancel) " +
                                   "SELECT DeptID, Jabatan, Atasan, TglSusun, Revisi, 1, 1, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, ApproveDate1, RowStatus, AlasanCancel " +
                                   "FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDesk WHERE ID='" + int.Parse(txtID.Text) + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();

                int ID = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "SELECT TOP 1 ID FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDesk ORDER BY ID desc";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ID = Int32.Parse(sdr["ID"].ToString());
                    }
                }

                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd2.CommandType = CommandType.Text;
                cmdd2.CommandText = "insert into Jobdeskbawahan (jobdeskid, bawahan, rowstatus) " +
                                    "SELECT '" + ID + "', Bawahan, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskBawahan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd2.ExecuteNonQuery();
                cmdd2.Dispose();

                SqlCommand cmdd3 = conn.CreateCommand();
                cmdd3.CommandType = CommandType.Text;
                cmdd3.CommandText = "insert into JobDeskTUJabatan (JOBDESKID, TujuanUmumJabatan, RowStatus) " +
                                    "SELECT '" + ID + "', TujuanUmumJabatan, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskTUJabatan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd3.ExecuteNonQuery();
                cmdd3.Dispose();

                SqlCommand cmdd4 = conn.CreateCommand();
                cmdd4.CommandType = CommandType.Text;
                cmdd4.CommandText = "insert into JobDeskTPJabatan (JOBDESKID, TugasPokokJabatan, RowStatus) " +
                                    "SELECT '" + ID + "', TugasPokokJabatan, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskTPJabatan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd4.ExecuteNonQuery();
                cmdd4.Dispose();

                SqlCommand cmdd5 = conn.CreateCommand();
                cmdd5.CommandType = CommandType.Text;
                cmdd5.CommandText = "insert into JobDeskHK (JOBDESKID, HubunganKerja, RowStatus) " +
                                    "SELECT '" + ID + "', HubunganKerja, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskHK WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd5.ExecuteNonQuery();
                cmdd5.Dispose();

                SqlCommand cmdd6 = conn.CreateCommand();
                cmdd6.CommandType = CommandType.Text;
                cmdd6.CommandText = "insert into JobDeskTJ (JOBDESKID, TanggungJawab, RowStatus) " +
                                    "SELECT '" + ID + "', TanggungJawab, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskTJ WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd6.ExecuteNonQuery();
                cmdd6.Dispose();

                SqlCommand cmdd7 = conn.CreateCommand();
                cmdd7.CommandType = CommandType.Text;
                cmdd7.CommandText = "insert into JobDeskWewenang (JOBDESKID, Wewenang, RowStatus) " +
                                    "SELECT '" + ID + "', Wewenang, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskWewenang WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd7.ExecuteNonQuery();
                cmdd7.Dispose();

                SqlCommand cmdd8 = conn.CreateCommand();
                cmdd8.CommandType = CommandType.Text;
                cmdd8.CommandText = "insert into JobDeskPendidikan (JOBDESKID, Pendidikan, RowStatus) " +
                                    "SELECT '" + ID + "', Pendidikan, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskPendidikan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd8.ExecuteNonQuery();
                cmdd8.Dispose();

                SqlCommand cmdd9 = conn.CreateCommand();
                cmdd9.CommandType = CommandType.Text;
                cmdd9.CommandText = "insert into JobDeskPengalaman (JOBDESKID, Pengalaman, RowStatus) " +
                                    "SELECT '" + ID + "', Pengalaman, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskPengalaman WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd9.ExecuteNonQuery();
                cmdd9.Dispose();

                SqlCommand cmdd10 = conn.CreateCommand();
                cmdd10.CommandType = CommandType.Text;
                cmdd10.CommandText = "insert into JobDeskUsia (JOBDESKID, Usia, RowStatus) " +
                                    "SELECT '" + ID + "', Usia, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskUsia WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd10.ExecuteNonQuery();
                cmdd10.Dispose();

                SqlCommand cmdd11 = conn.CreateCommand();
                cmdd11.CommandType = CommandType.Text;
                cmdd11.CommandText = "insert into JobDeskPengetahuan (JOBDESKID, Pengetahuan, RowStatus) " +
                                    "SELECT '" + ID + "', Pengetahuan, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskPengetahuan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd11.ExecuteNonQuery();
                cmdd11.Dispose();

                SqlCommand cmdd12 = conn.CreateCommand();
                cmdd12.CommandType = CommandType.Text;
                cmdd12.CommandText = "insert into JobDeskKeterampilan (JOBDESKID, Keterampilan, RowStatus) " +
                                    "SELECT '" + ID + "', Keterampilan, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskKeterampilan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd12.ExecuteNonQuery();
                cmdd12.Dispose();

                SqlCommand cmdd13 = conn.CreateCommand();
                cmdd13.CommandType = CommandType.Text;
                cmdd13.CommandText = "insert into JobDeskFisik (JOBDESKID, Fisik, RowStatus) " +
                                    "SELECT '" + ID + "', Fisik, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskFisik WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd13.ExecuteNonQuery();
                cmdd13.Dispose();

                SqlCommand cmdd14 = conn.CreateCommand();
                cmdd14.CommandType = CommandType.Text;
                cmdd14.CommandText = "insert into JobDeskNonFisik (JOBDESKID, NonFisik, RowStatus) " +
                                    "SELECT '" + ID + "', NonFisik, RowStatus FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDeskNonFisik WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd14.ExecuteNonQuery();
                cmdd14.Dispose();

                conn.Close();


                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = "UPDATE JobDesk SET Approval=6 WHERE ID='" + int.Parse(txtID.Text) + "' ";
                SqlDataReader sdr1 = zl1.Retrieve();
            }
            else
            {
                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "INSERT INTO JobDesk (DeptID, Jabatan, Atasan, TglSusun, Revisi, Status, Approval, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, ApproveDate1, RowStatus, AlasanCancel) " +
                                   "SELECT DeptID, Jabatan, Atasan, TglSusun, Revisi, 1, 1, CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime, ApproveDate1, RowStatus, AlasanCancel " +
                                   "FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDesk WHERE ID='" + int.Parse(txtID.Text) + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();

                int ID = 0;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "SELECT TOP 1 ID FROM [sqlkrwg.grcboard.com].bpaskrwg.dbo.JobDesk ORDER BY ID desc";
                SqlDataReader sdr = zl.Retrieve();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ID = Int32.Parse(sdr["ID"].ToString());
                    }
                }

                SqlCommand cmdd2 = conn.CreateCommand();
                cmdd2.CommandType = CommandType.Text;
                cmdd2.CommandText = "insert into Jobdeskbawahan (jobdeskid, bawahan, rowstatus) " +
                                    "SELECT '" + ID + "', Bawahan, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskBawahan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd2.ExecuteNonQuery();
                cmdd2.Dispose();

                SqlCommand cmdd3 = conn.CreateCommand();
                cmdd3.CommandType = CommandType.Text;
                cmdd3.CommandText = "insert into JobDeskTUJabatan (JOBDESKID, TujuanUmumJabatan, RowStatus) " +
                                    "SELECT '" + ID + "', TujuanUmumJabatan, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskTUJabatan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd3.ExecuteNonQuery();
                cmdd3.Dispose();

                SqlCommand cmdd4 = conn.CreateCommand();
                cmdd4.CommandType = CommandType.Text;
                cmdd4.CommandText = "insert into JobDeskTPJabatan (JOBDESKID, TugasPokokJabatan, RowStatus) " +
                                    "SELECT '" + ID + "', TugasPokokJabatan, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskTPJabatan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd4.ExecuteNonQuery();
                cmdd4.Dispose();

                SqlCommand cmdd5 = conn.CreateCommand();
                cmdd5.CommandType = CommandType.Text;
                cmdd5.CommandText = "insert into JobDeskHK (JOBDESKID, HubunganKerja, RowStatus) " +
                                    "SELECT '" + ID + "', HubunganKerja, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskHK WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd5.ExecuteNonQuery();
                cmdd5.Dispose();

                SqlCommand cmdd6 = conn.CreateCommand();
                cmdd6.CommandType = CommandType.Text;
                cmdd6.CommandText = "insert into JobDeskTJ (JOBDESKID, TanggungJawab, RowStatus) " +
                                    "SELECT '" + ID + "', TanggungJawab, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskTJ WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd6.ExecuteNonQuery();
                cmdd6.Dispose();

                SqlCommand cmdd7 = conn.CreateCommand();
                cmdd7.CommandType = CommandType.Text;
                cmdd7.CommandText = "insert into JobDeskWewenang (JOBDESKID, Wewenang, RowStatus) " +
                                    "SELECT '" + ID + "', Wewenang, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskWewenang WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd7.ExecuteNonQuery();
                cmdd7.Dispose();

                SqlCommand cmdd8 = conn.CreateCommand();
                cmdd8.CommandType = CommandType.Text;
                cmdd8.CommandText = "insert into JobDeskPendidikan (JOBDESKID, Pendidikan, RowStatus) " +
                                    "SELECT '" + ID + "', Pendidikan, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskPendidikan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd8.ExecuteNonQuery();
                cmdd8.Dispose();

                SqlCommand cmdd9 = conn.CreateCommand();
                cmdd9.CommandType = CommandType.Text;
                cmdd9.CommandText = "insert into JobDeskPengalaman (JOBDESKID, Pengalaman, RowStatus) " +
                                    "SELECT '" + ID + "', Pengalaman, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskPengalaman WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd9.ExecuteNonQuery();
                cmdd9.Dispose();

                SqlCommand cmdd10 = conn.CreateCommand();
                cmdd10.CommandType = CommandType.Text;
                cmdd10.CommandText = "insert into JobDeskUsia (JOBDESKID, Usia, RowStatus) " +
                                    "SELECT '" + ID + "', Usia, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskUsia WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd10.ExecuteNonQuery();
                cmdd10.Dispose();

                SqlCommand cmdd11 = conn.CreateCommand();
                cmdd11.CommandType = CommandType.Text;
                cmdd11.CommandText = "insert into JobDeskPengetahuan (JOBDESKID, Pengetahuan, RowStatus) " +
                                    "SELECT '" + ID + "', Pengetahuan, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskPengetahuan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd11.ExecuteNonQuery();
                cmdd11.Dispose();

                SqlCommand cmdd12 = conn.CreateCommand();
                cmdd12.CommandType = CommandType.Text;
                cmdd12.CommandText = "insert into JobDeskKeterampilan (JOBDESKID, Keterampilan, RowStatus) " +
                                    "SELECT '" + ID + "', Keterampilan, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskKeterampilan WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd12.ExecuteNonQuery();
                cmdd12.Dispose();

                SqlCommand cmdd13 = conn.CreateCommand();
                cmdd13.CommandType = CommandType.Text;
                cmdd13.CommandText = "insert into JobDeskFisik (JOBDESKID, Fisik, RowStatus) " +
                                    "SELECT '" + ID + "', Fisik, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskFisik WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd13.ExecuteNonQuery();
                cmdd13.Dispose();

                SqlCommand cmdd14 = conn.CreateCommand();
                cmdd14.CommandType = CommandType.Text;
                cmdd14.CommandText = "insert into JobDeskNonFisik (JOBDESKID, Fisik, RowStatus) " +
                                    "SELECT '" + ID + "', NonFisik, RowStatus FROM [sqlctrp.grcboard.com].bpasctrp.dbo.JobDeskNonFisik WHERE JOBDESKID='" + int.Parse(txtID.Text) + "' ";
                cmdd14.ExecuteNonQuery();
                cmdd14.Dispose();

                conn.Close();


                ZetroView zl1 = new ZetroView();
                zl1.QueryType = Operation.CUSTOM;
                zl1.CustomQuery = "UPDATE JobDesk SET Approval=6 WHERE ID='" + int.Parse(txtID.Text) + "' ";
                SqlDataReader sdr1 = zl1.Retrieve();

            }

            //JobdeskDetail jobdeskDetail = new JobdeskDetail();
            //int i = 0;
            //foreach (RepeaterItem objItem in lstBawahan.Items)
            //{
            //    jobdeskDetail = new JobdeskDetail();
            //    jobdeskDetail.JOBDESKID = jobdeskID;
            //    TextBox txtBawahan = (TextBox)lstBawahan.Items[i].FindControl("txtBawahan");

            //    if (txtBawahan.Text != string.Empty)
            //    {
            //        jobdeskDetail.Bawahan = txtBawahan.Text;
            //    }
            //    absTrans = new JobDeskBawahanFacade(jobdeskDetail);
            //    intresult = absTrans.Insert(transManager);
            //    if (absTrans.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return;
            //    }
            //    i = i + 1;
            //}

            DisplayAJAXMessage(this, "Berhasil Disimpan");
            AutoNext();
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
                Response.Redirect("JobDeskDistribusi.aspx");
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void btnAktivasi_Click(object sender, EventArgs e)
        {
            AktivasiJOBDESK();

            LoadOpenJOBDESK();
            Users user = ((Users)Session["Users"]);
            JobDeskFacade jobdeskFacade = new JobDeskFacade();
            JobDesk jobdesk = new JobDesk();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string usertype = jobdeskFacade.GetUserType(UserID);
            Session["usertype"] = usertype;
        }

        private void AktivasiJOBDESK()
        {
            Users user = ((Users)Session["Users"]);
            if (user.UnitKerjaID == 7)
            {
                SqlConnection con = new SqlConnection(Global.ConnectionString());
                SqlCommand cmd = new SqlCommand("spUpdateJobDeskDistribusi", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "Aktivasi";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpasctrp;Data Source=sqlctrp.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=3, Approval=7, LastModifiedTime=GETDATE(), " +
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
                cmd.Parameters.Add("@tampil", SqlDbType.VarChar).Value = "Aktivasi";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtID.Text;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.VarChar).Value = user.UserName;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SqlConnection conn = new SqlConnection(@"Initial Catalog=bpaskrwg;Data Source=sqlkrwg.grcboard.com;User ID=sa;Password=Passw0rd");
                conn.Open();
                SqlCommand cmdd = conn.CreateCommand();
                cmdd.CommandType = CommandType.Text;
                cmdd.CommandText = "UPDATE JobDesk set Status=3, Approval=7, LastModifiedTime=GETDATE(), " +
                                   "LastModifiedBy='" + user.UserName + "' where Jabatan='" + txtBagian.Text + "' ";
                cmdd.ExecuteNonQuery();
                cmdd.Dispose();
                conn.Close();
            }

            Response.Redirect("JobDeskApproval.aspx");
        }
    }
}