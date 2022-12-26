using BusinessFacade;
using DataAccessLayer;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.TPP
{
    public partial class ApvTPPHSE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.link = "~/Default.aspx";
                {
                    LoadOpenTPP();
                    LoadDataGridViewItem(LoadDataGrid());
                    LoadDept();
                    Users user = ((Users)Session["Users"]);
                    NewSuppPurch sp = new NewSuppPurch();
                    TPP_Facade tppFacade = new TPP_Facade();
                    TPP tpp = new TPP();
                    string UserID = string.Empty;
                    UserID = user.ID.ToString();
                    string usertype = tppFacade.GetUserType(UserID);
                    Session["usertype"] = usertype;
                    //btnNotApprove.Visible = false;
                    //btnUpdate.Enabled = false;
                    if (user.Apv > 2)
                    {
                        btnNotApprove.Visible = true;
                    }
                    else
                    {
                        btnNotApprove.Visible = false;
                    }
                }

                if (noTPP.Value != string.Empty)
                {
                    Users user = (Users)Session["Users"];
                    string[] ListTPP = noTPP.Value.Split(',');
                    string[] ListOpenTPP = ListTPP.Distinct().ToArray();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    LoadOpenTPP(ListOpenTPP[0].ToString());
                    btnApprove.Enabled = true;
                    //btnNotApprove.Enabled = true;
                    //btnNotApprove.Enabled = true;
                    btnNext.Enabled = (ListOpenTPP.Count() > 1) ? true : false;
                    ViewState["index"] = idx;
                }
                else
                {
                    btnApprove.Enabled = false;
                    btnNotApprove.Enabled = false;
                    btnNext.Enabled = false;
                }
                string[] ListOpenTPPx = noTPP.Value.Split(',');
                string[] ListOpenTPPd = ListOpenTPPx.Distinct().ToArray();
                int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
                btnNext.Enabled = ((idxx - 1) >= ListOpenTPPd.Count()) ? false : true;
                ViewState["index"] = idxx;
                //txtAlasan.Attributes.Add("onkeyup", "onKeyUp()");
                if (Request.QueryString["UpdNo"] != null)
                {
                    LoadOpenTPP(Request.QueryString["UpdNo"].ToString());
                }
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            string[] ListTPP = noTPP.Value.Split(',');
            string[] ListOpenTPP = ListTPP.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenTPP.Count()) ? false : true;
            LoadOpenTPP(ListOpenTPP[idx].ToString());
            ViewState["index"] = idx;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            string[] ListTPP = noTPP.Value.Split(',');
            string[] ListOpenTPP = ListTPP.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            idx = (idx > ListOpenTPP.Count() - 1) ? 0 : idx;
            btnNext.Enabled = ((idx) == ListOpenTPP.Count()) ? false : true;
            try
            {
                ViewState["index"] = idx;
                LoadOpenTPP(ListOpenTPP[idx].ToString());
            }
            catch
            {
                LoadOpenTPP(ListOpenTPP[0].ToString());
                ViewState["index"] = 0;
            }
            btnPrev.Enabled = (idx > 0) ? true : false;
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            //if (user.Apv == 5 || user.Apv == 3)
            //{
            ApproveTPP();
            LoadOpenTPP();
            LoadDataGridViewItem(LoadDataGrid());
            LoadDept();
            //Users user = ((Users)Session["Users"]);
            TPP_Facade tppFacade = new TPP_Facade();
            TPP tpp = new TPP();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string usertype = tppFacade.GetUserType(UserID);
            Session["usertype"] = usertype;
            //AutoNext();
            /*}
            else if (user.Apv == 2)
            {
                ApproveTPP();

                LoadOpenTPP();
                LoadDataGridViewItem(LoadDataGrid());
                LoadDept();
                //Users user = ((Users)Session["Users"]);
                TPP_Facade tppFacade = new TPP_Facade();
                TPP tpp = new TPP();
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = tppFacade.GetUserType(UserID);
                Session["usertype"] = usertype;

                SqlConnection con = new SqlConnection(Global.ConnectionString());
                UsersFacade usersFacade = new UsersFacade();
                Users users3 = usersFacade.RetrieveTppApproved(int.Parse(txtID.Text));

                string emailaddress = string.Empty;
                string emailaddress1 = string.Empty;
                string tmpID = string.Empty;
                tmpID = txtID.Text.ToString();

                //            string strSQL = @"select * from Users where ID in (select user_ID from TPPEmail where Dept_ID 
                //                        in (select Dept_ID from tpp where id='" + tmpID + "')) order by id desc";
                string strSQL = "select * from Users where ID " +
                    "in (select userID from ISO_Dept where UserGroupID=100 and TppDept " + "in (select Dept_ID from tpp where id='" + tmpID + "')) order by id desc";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader dr = da.RetrieveDataByString(strSQL);
                string strSQLerror = da.Error;
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        emailaddress = emailaddress + dr["usrmail"].ToString() + ",";
                    }
                }

                emailaddress1 = emailaddress.Substring(0, (emailaddress.Length) - 1);
                KirimEmailApproved(emailaddress1);

                con.Close();
                //refresh current page
                //Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                DisplayAJAXMessage(this, "Approved berhasil");
                AutoNext();
            }*/
        }

        protected void btnNotApprove_Click(object sender, EventArgs e)
        {
            //NotApproveTPP();

            LoadOpenTPP();
            LoadDataGridViewItem(LoadDataGrid());
            LoadDept();
            Users user = ((Users)Session["Users"]);
            TPP_Facade tppFacade = new TPP_Facade();
            Domain.TPP tpp = new Domain.TPP();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string usertype = tppFacade.GetUserType(UserID);
            Session["usertype"] = usertype;
            tpp.ID = int.Parse(txtID.Text);

            SqlConnection con = new SqlConnection(Global.ConnectionString());
            SqlCommand cmd = new SqlCommand("TPP_UpdateNotApv", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = int.Parse(txtID.Text);

            con.Open();
            cmd.ExecuteNonQuery();

            UsersFacade usersFacade = new UsersFacade();
            Users users3 = usersFacade.RetrieveTppNoteApproved(int.Parse(txtID.Text));

            string emailaddress = string.Empty;
            string emailaddress1 = string.Empty;
            string tmpID = string.Empty;
            tmpID = txtID.Text.ToString();

            string strSQL = @"select * from Users where ID in (select user_ID from TPPEmail where Dept_ID 
                        in (select Dept_ID from tpp where id='" + tmpID + "')) order by id desc";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader dr = da.RetrieveDataByString(strSQL);
            string strSQLerror = da.Error;
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    emailaddress = emailaddress + dr["usrmail"].ToString() + ",";
                }
            }

            /*foreach (var email in users3.ToString())
            {
                emailaddress = emailaddress + users3.UsrMail.Trim() + ",";
            }*/

            emailaddress1 = emailaddress.Substring(0, (emailaddress.Length) - 1);
            KirimEmail(emailaddress1);

            con.Close();
            //refresh current page
            //Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
            DisplayAJAXMessage(this, "Not Approval berhasil");
            AutoNext();
        }

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{           
        //    string idTPP = Session["idTPP"].ToString();
        //    Response.Redirect("FormTPP.aspx?ID="+idTPP+"");
        //}

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
                Response.Redirect("ApvTPPHSE.aspx");
            }
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            //LoadOpenUPD(txtCari.Text);
        }

        private void LoadOpenTPP()
        {
            Users user = ((Users)Session["Users"]);
            ArrayList arrTPP = new ArrayList();
            TPP_Facade tppFacade = new TPP_Facade();
            Domain.TPP tpp = new Domain.TPP();
            int userhse = user.ID;
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string Apv = tppFacade.GetApv(UserID);
            string ApvTPP = tppFacade.GetStatusApv(UserID);
            Session["Apv"] = Apv;
            Session["ApvTPP"] = ApvTPP;
            string UserInput = tppFacade.GetUserI(UserID);
            string UserInput1 = tppFacade.GetUserIGrid(UserID);
            Session["UserInput"] = UserInput;
            Session["UserInput1"] = UserInput1;
            arrTPP = tppFacade.RetrieveForOpenTPPHSE(userhse);
            foreach (Domain.TPP tpp1 in arrTPP)
            {
                if (tpp1.Laporan_No != string.Empty)
                {

                    noTPP.Value += tpp1.Laporan_No + ",";
                }
            }

        }

        private void LoadOpenTPP(string TPPNum)
        {
            Users user = (Users)Session["Users"];
            TPP_Facade tppF = new TPP_Facade();
            Domain.TPP tpp = new Domain.TPP();
            string UserInput = Session["UserInput"].ToString();
            tpp = tppF.RetrieveTPPNum(TPPNum, UserInput);



            //if (txtCari.Text != "Find by Nomor UPD" && sp.updNo == string.Empty)
            //{
            //    sp = new ISO_Upd();
            //    DisplayAJAXMessage(this, "UPD tidak di temukan");
            //    txtCari.Text = string.Empty;
            //    return;
            //}

            txtNoPO.Text = tpp.Laporan_No;
            txtDate.Text = tpp.TPP_Date.ToString("dd-MMM-yyyy");
            //string Dept_ID = tpp.Dept_ID.ToString();
            //tpp.Dept_ID = int.Parse(txtDeptF.Text);
            txtDeptF.Text = tpp.DeptFrom;
            txtApv.Text = tpp.Approval;
            txtTidakS.Text = tpp.Uraian;
            txtID.Text = tpp.ID.ToString();
            if (txtNoPO.Text.Trim() != string.Empty)
                FormData(txtNoPO.Text.Trim());
            //btnUpdate.Enabled = false;

        }

        protected void lstSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListOfSPPDetail"] = null;
            Session["NoSPP"] = null;
            string[] ListTPP = noTPP.Value.Split(',');
            string[] ListOpenTPP = ListTPP.Distinct().ToArray();
            string spp = string.Join(",", ListOpenTPP);
            string sp = new EncryptPasswordFacade().EncryptToString(spp);
            Response.Redirect("ListSPPApproval.aspx?approve=" + (((Users)Session["Users"]).GroupID) + "&sp=" + sp);

        }

        private void ApproveTPP()
        {
            Users user = (Users)Session["Users"];
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppF = new TPP_Facade();
            string strError = string.Empty;
            string UserID = user.ID.ToString();
            int Apv = Convert.ToInt32(Session["Apv"].ToString());
            if ((chkya.Checked == true && chkno.Checked == false) || (chkno.Checked == true && chkya.Checked == false))
            {
                tpp.Apv = Apv;
                tpp.ID = int.Parse(txtID.Text);
                tpp.User_ID = int.Parse(UserID);
                tpp.CreatedBy = user.UserName;
                tpp.rekomendasi = txtRekomHSE.Text;
                if (chkya.Checked)
                {
                    tpp.Ceklis = 1;
                }
                else if (chkno.Checked)
                {
                    tpp.Ceklis = 2;
                }

                else
                {
                    tpp.Ceklis = 0;
                }
                TPP_ProcessFacade ProcessFacade = new TPP_ProcessFacade(tpp);
                strError = ProcessFacade.Update2();
                LoadDataGridViewItem(LoadDataGrid());
                if (strError == string.Empty)
                {
                    DisplayAJAXMessage(this, "Approval berhasil");
                }
                AutoNext();
            }
            else
            {
                DisplayAJAXMessage(this, "Pilih Rekomendasi");
            }
        }

        private ArrayList LoadDataGrid()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrTPP = new ArrayList();
            TPP_Facade tppF = new TPP_Facade();
            string UserInput1 = Session["UserInput1"].ToString();
            string UserHead = users.ID.ToString();
            int userid = users.ID;
            arrTPP = tppF.RetrieveTPPHSE(userid);
            GridView1.DataSource = arrTPP;
            GridView1.DataBind();

            return arrTPP;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadDataGridViewItem(LoadDataGrid());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ApvTPP = Session["ApvTPP"].ToString();
            Users user = (Users)Session["Users"];
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                FormData(GridView1.Rows[index].Cells[1].Text);
                GridViewRow row = GridView1.Rows[index];
                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtNoPO.Text = CekString(row.Cells[1].Text);
                txtDate.Text = CekString(row.Cells[3].Text);
                txtDeptF.Text = CekString(row.Cells[2].Text);
                txtTidakS.Text = CekString(row.Cells[4].Text);
                txtID.Text = CekString(row.Cells[0].Text);
                string idTPP = txtID.Text;
                Session["idTPP"] = idTPP;
                txtApv.Text = CekString(row.Cells[5].Text);

                txtTidakS.ReadOnly = true;
                btnNext.Enabled = false;
                //if (ApvTPP == "1")
                //{
                //    btnUpdate.Enabled = true;
                //}
                //if (user.Apv > 2)
                //{
                //    btnNotApprove.Enabled = true;
                //}

            }
        }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        private void LoadDataGridViewItem(ArrayList arrTPP)
        {
            this.GridView1.DataSource = arrTPP;
            this.GridView1.DataBind();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void clearForm()
        {
            //txtAlasan2.Text = string.Empty;
            btnApprove.Enabled = true;
            btnNotApprove.Enabled = true;

            LoadDept();
            TxtLaporanNo.Text = string.Empty;
            txtTPP_Date.SelectedDate = DateTime.Now;
            //txtPIC.Text = string.Empty;
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            Users user = ((Users)Session["Users"]);
            tppdept = deptFacade.GetUserDept(user.ID);
            ddlDeptName.SelectedValue = tppdept.ID.ToString();
            Rb1.Checked = false;
            Rb1a.Checked = false;
            Rb1b.Checked = false;
            Rb1c.Checked = false;
            Rb2.Checked = false;
            Rb3.Checked = false;
            Rb4.Checked = false;
            Rb5.Checked = false;
            Rb6.Checked = false;
            txtKeterangan.Text = string.Empty;
            txtUTSesuai.Text = string.Empty;
            txtITSesuai.Text = string.Empty;
            chkLingkungan.Checked = false;
            chkManusia.Checked = false;
            chkMaterial.Checked = false;
            chkMesin.Checked = false;
            chkMetode.Checked = false;
            txtLingkungan.Text = string.Empty;
            txtManusia.Text = string.Empty;
            txtMaterial.Text = string.Empty;
            txtMesin.Text = string.Empty;
            txtMetode.Text = string.Empty;
            txtLingkungan.Enabled = false;
            txtManusia.Enabled = false;
            txtMaterial.Enabled = false;
            txtMesin.Enabled = false;
            txtMetode.Enabled = false;
            LoadPencegahan("0");
            LoadPerbaikan("0");
            PanelColour();
            txtKlausulNo.Text = string.Empty;
            if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
            {
                PanelStatus.Enabled = true;
                chkClose.Enabled = true;
                txtDateTKasus.Enabled = true;
                chksolved.Enabled = true;
            }
            else
            {
                PanelStatus.Enabled = false;
                chkClose.Enabled = false;
                txtDateTKasus.Enabled = false;
                chksolved.Enabled = false;
            }
            LSpv.Text = " ";
            LMgr.Text = " ";
            LPMgr.Text = " ";
            LMR.Text = " ";
            LAuditor.Text = " ";
        }
        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            TPP_Dept tppdept = new TPP_Dept();
            TPP_DeptFacade deptFacade = new TPP_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (TPP_Dept dept in arrDept)
            {
                ddlDeptName.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            tppdept = deptFacade.GetUserDept(user.ID);
            ddlDeptName.SelectedValue = tppdept.ID.ToString();
        }
        protected void LoadPencegahan(string laporan_no)
        {
            ArrayList arrPencegahan = new ArrayList();
            TPP_TindakanFacade cegahF = new TPP_TindakanFacade();
            arrPencegahan = cegahF.RetrieveByNo(laporan_no, "Pencegahan");
            GridPencegahan.DataSource = arrPencegahan;
            GridPencegahan.DataBind();
        }
        protected void LoadPerbaikan(string laporan_no)
        {
            ArrayList arrPerbaikan = new ArrayList();
            TPP_TindakanFacade PerbaikanF = new TPP_TindakanFacade();
            arrPerbaikan = PerbaikanF.RetrieveByNo(laporan_no, "Perbaikan");
            GridPerbaikan.DataSource = arrPerbaikan;
            GridPerbaikan.DataBind();
        }
        protected void FormData(string laporan_no)
        {
            //try
            //{

            ArrayList arrtpp = new ArrayList();
            Domain.TPP tpp = new Domain.TPP();
            TPP_Facade tppf = new TPP_Facade();
            tpp = tppf.RetrieveByNo(laporan_no);
            arrtpp.Add(tpp);
            clearForm();
            TxtLaporanNo.Text = tpp.Laporan_No;
            txtTPP_Date.SelectedDate = tpp.TPP_Date;
            for (int i = ddlDeptName.Items.Count - 1; i > 0; i--)
            {
                if (ddlDeptName.Items[i].Text.Contains(tpp.Departemen))
                {
                    ddlDeptName.Items[i].Selected = true;
                    break;
                }
            }
            ddlDeptName.SelectedItem.Text = tpp.Departemen;
            string masalah = tpp.Asal_Masalah.Trim();
            if (tpp.Asal_Masalah.Trim() == "Audit External")
                Rb1.Checked = true;
            if (tpp.Asal_Masalah.Trim() == "Audit Internal")
                Rb2.Checked = true;

            Rb1a.Checked = false;
            Rb1b.Checked = false;
            Rb1c.Checked = false;
            if (tpp.Asal_M_ID1 == 1)
                Rb1a.Checked = true;
            if (tpp.Asal_M_ID1 == 2)
                Rb1b.Checked = true;
            if (tpp.Asal_M_ID1 == 3)
                Rb1c.Checked = true;
            if (tpp.Asal_Masalah.Trim() == "NCR Proses")
                Rb3.Checked = true;
            if (tpp.Asal_Masalah.Trim() == "NCR Customer Complain")
                Rb4.Checked = true;
            if (tpp.Asal_Masalah.Trim() == "Kecelakaan Kerja")
                Rb5.Checked = true;
            if (tpp.Asal_Masalah.Trim() == "Lain-lain")
                Rb6.Checked = true;
            txtUTSesuai.Text = tpp.Uraian;
            txtITSesuai.Enabled = true;
            txtITSesuai.Text = tpp.Ketidaksesuaian;
            LoadPencegahan(laporan_no);
            LoadPerbaikan(laporan_no);
            txtKeterangan.Text = string.Empty;
            txtKlausulNo.Text = string.Empty;
            if (Rb6.Checked == true || Rb4.Checked == true)
            {
                txtKeterangan.Text = tpp.Keterangan;
                txtKlausulNo.Text = string.Empty;
            }
            if (Rb1.Checked == true || Rb2.Checked == true)
            {
                txtKeterangan.Text = string.Empty;
                txtKlausulNo.Text = tpp.Keterangan;
            }
            ArrayList arrPenyebab = new ArrayList();
            TPP_Penyebab_Detail_Facade penyebabdetailF = new TPP_Penyebab_Detail_Facade();
            arrPenyebab = penyebabdetailF.RetrieveByNo(laporan_no);
            foreach (TPP_Penyebab_Detail Pdetail in arrPenyebab)
            {
                if (Pdetail.Penyebab.Trim() == "Lingkungan")
                {
                    chkLingkungan.Checked = true;
                    txtLingkungan.Enabled = true;
                    txtLingkungan.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Manusia")
                {
                    chkManusia.Checked = true;
                    txtManusia.Enabled = true;
                    txtManusia.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Material")
                {
                    chkMaterial.Checked = true;
                    txtMaterial.Enabled = true;
                    txtMaterial.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Mesin")
                {
                    chkMesin.Checked = true;
                    txtMesin.Enabled = true;
                    txtMesin.Text = Pdetail.Uraian;
                }
                if (Pdetail.Penyebab.Trim() == "Metode")
                {
                    chkMetode.Checked = true;
                    txtMetode.Enabled = true;
                    txtMetode.Text = Pdetail.Uraian;
                }
            }
            PanelColour();
            if (Session["usertype"].ToString().Trim().ToUpper() == "ISO")
            {
                PanelStatus.Enabled = true;
                chkClose.Enabled = true;
                txtDateTKasus.Enabled = true;
                chksolved.Enabled = true;
                txtDateSolved0.Enabled = true;
                txtDueDate0.Enabled = true;
            }
            else
            {
                PanelStatus.Enabled = false;
                chkClose.Enabled = false;
                txtDateTKasus.Enabled = false;
                chksolved.Enabled = false;
                txtDateSolved0.Enabled = false;
                txtDueDate0.Enabled = false;
            }
            if (tpp.Closed == 1)
            {
                chkClose.Checked = true;
                txtDateTKasus.SelectedDate = tpp.Close_Date;
            }
            else
            {
                chkClose.Checked = false;
                txtDateTKasus.SelectedValue = string.Empty;
            }
            if (tpp.Solved == 1)
            {
                chksolved.Checked = true;
                txtDateSolved0.SelectedDate = tpp.Solve_Date;
                txtDueDate0.SelectedDate = tpp.Due_Date;
            }
            else
            {
                chksolved.Checked = false;
                txtDateSolved0.SelectedValue = string.Empty;
                txtDueDate0.SelectedValue = string.Empty;
            }
            if (tpp.Approval.Trim().ToUpper() == "ADMIN")
            {
                LSpv.Text = "Open";
                LMgr.Text = "Open";
                LMgr0.Text = "Open";
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LPMgr.Text = " ";
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                }
                else
                {
                    LPMgr.Text = "Open";
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                }
            }
            if (tpp.Approval.Trim().ToUpper() == "HEAD/KASIE")
            {
                LSpv.Text = "Approved";
                LMgr.Text = "Open";
                LMgr0.Text = "Open";
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LPMgr.Text = " ";
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                }
                else
                {
                    LPMgr.Text = "Open";
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                }
            }
            if (tpp.Approval.Trim().ToUpper() == "MANAGER")
            {
                LSpv.Text = "Approved";
                LMgr0.Text = "Approved";
                LMgr.Text = "Approved";
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LPMgr.Text = " ";
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                }
                else
                {
                    LPMgr.Text = "Open";
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                }
            }
            if (tpp.Approval.Trim().ToUpper() == "PLANT MANAGER")
            {
                LSpv.Text = "Approved";
                LMgr.Text = "Approved";
                LMgr0.Text = "Approved";
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LPMgr.Text = " ";
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                }
                else
                {
                    LPMgr.Text = "Approved";
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                }
            }
            if (tpp.Verified > 0 && tpp.Notverified == 0)
            {
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LAuditor.Text = "Approved";
                    LAuditor0.Text = "Approved";
                    LMR.Text = "Approved";
                }
                else
                {
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                    LMR.Text = "Approved";
                }
            }
            else
            {
                if (tpp.Asal_M_ID == 1 || tpp.Asal_M_ID == 2)
                {
                    LAuditor.Text = "Open";
                    LAuditor0.Text = "Open";
                    LMR.Text = "Open";
                }
                else
                {
                    LAuditor.Text = " ";
                    LAuditor0.Text = " ";
                    LMR.Text = "Open";
                }
            }
        }

        protected void PanelColour()
        {
            Session["masalah"] = " ";
            if (Rb1.Checked == true)
            {
                PanelRB1.BackColor = System.Drawing.Color.Blue;
                PanelRB1.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Audit External";
            }
            else
            {
                PanelRB1.BackColor = System.Drawing.Color.White;
                PanelRB1.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb1a.Checked == true)
            {
                PanelRb1a.BackColor = System.Drawing.Color.Blue;
                PanelRb1a.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                PanelRb1a.BackColor = System.Drawing.Color.White;
                PanelRb1a.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb1b.Checked == true)
            {
                PanelRb1b.BackColor = System.Drawing.Color.Blue;
                PanelRb1b.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                PanelRb1b.BackColor = System.Drawing.Color.White;
                PanelRb1b.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb1c.Checked == true)
            {
                PanelRb1c.BackColor = System.Drawing.Color.Blue;
                PanelRb1c.ForeColor = System.Drawing.Color.White;
            }
            else
            {
                PanelRb1c.BackColor = System.Drawing.Color.White;
                PanelRb1c.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb2.Checked == true)
            {
                PanelRB2.BackColor = System.Drawing.Color.Blue;
                PanelRB2.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Audit Internal";
            }
            else
            {
                PanelRB2.BackColor = System.Drawing.Color.White;
                PanelRB2.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb3.Checked == true)
            {
                PanelRb3.BackColor = System.Drawing.Color.Blue;
                PanelRb3.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "NCR Proses";
            }
            else
            {
                PanelRb3.BackColor = System.Drawing.Color.White;
                PanelRb3.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb4.Checked == true)
            {
                PanelRB4.BackColor = System.Drawing.Color.Blue;
                PanelRB4.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "NCR Customer Complain";
            }
            else
            {
                PanelRB4.BackColor = System.Drawing.Color.White;
                PanelRB4.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb5.Checked == true)
            {
                PanelRb5.BackColor = System.Drawing.Color.Blue;
                PanelRb5.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Kecelakaan Kerja";
            }
            else
            {
                PanelRb5.BackColor = System.Drawing.Color.White;
                PanelRb5.ForeColor = System.Drawing.Color.Black;
            }
            if (Rb6.Checked == true)
            {
                PanelRb6.BackColor = System.Drawing.Color.Blue;
                PanelRb6.ForeColor = System.Drawing.Color.White;
                Session["masalah"] = "Lain-lain";
            }
            else
            {
                PanelRb6.BackColor = System.Drawing.Color.White;
                PanelRb6.ForeColor = System.Drawing.Color.Black;
            }

            if (chkLingkungan.Checked == true)
            {
                //chkLingkungan.BackColor = System.Drawing.Color.Green;

                chkLingkungan.ForeColor = System.Drawing.Color.White;
                Panel14.BackImageUrl = ResolveUrl("~/images/ellipse_L.png");
            }
            else
            {
                //chkLingkungan.BackColor = System.Drawing.Color.White;
                chkLingkungan.ForeColor = System.Drawing.Color.Black;
                Panel14.BackImageUrl = string.Empty;
            }
            if (chkManusia.Checked == true)
            {
                //chkManusia.BackColor = System.Drawing.Color.Green;
                chkManusia.ForeColor = System.Drawing.Color.White;
                Panel13.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                //chkManusia.BackColor = System.Drawing.Color.White;
                chkManusia.ForeColor = System.Drawing.Color.Black;
                Panel13.BackImageUrl = string.Empty;
            }

            if (chkMesin.Checked == true)
            {
                //chkMesin.BackColor = System.Drawing.Color.Green;
                chkMesin.ForeColor = System.Drawing.Color.White;
                Panel15.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                //chkMesin.BackColor = System.Drawing.Color.White;
                chkMesin.ForeColor = System.Drawing.Color.Black;
                Panel15.BackImageUrl = string.Empty;
            }
            if (chkMaterial.Checked == true)
            {
                //chkMaterial.BackColor = System.Drawing.Color.Green;
                chkMaterial.ForeColor = System.Drawing.Color.White;
                Panel17.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                //chkMaterial.BackColor = System.Drawing.Color.White;
                chkMaterial.ForeColor = System.Drawing.Color.Black;
                Panel17.BackImageUrl = string.Empty;
            }
            if (chkMetode.Checked == true)
            {
                //chkMetode.BackColor = System.Drawing.Color.Green;
                chkMetode.ForeColor = System.Drawing.Color.White;
                Panel16.BackImageUrl = ResolveUrl("~/images/ellipse.png");
            }
            else
            {
                //chkMetode.BackColor = System.Drawing.Color.White;
                chkMetode.ForeColor = System.Drawing.Color.Black;
                Panel16.BackImageUrl = string.Empty;
            }
            if (Rb1.Checked == true || Rb2.Checked == true)
                PanelAudite.Visible = true;
            else
                PanelAudite.Visible = false;
            if (Rb6.Checked == true || Rb4.Checked == true)
                txtKeterangan.Visible = true;
            else
                txtKeterangan.Visible = false;
        }
        //}
        //catch { }
        protected void chkList_CheckedChanged(object sender, EventArgs e)
        {
            if (chkList.Checked == true)
                Panel3.Visible = true;
            else
                Panel3.Visible = false;
        }
        protected void chkDetail_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDetail.Checked == true)
                PanelForm.Visible = true;
            else
                PanelForm.Visible = false;
        }

        public void KirimEmail(string Email)
        {
            try
            {
                Depo depo = new Depo();
                DepoFacade depof = new DepoFacade();
                depo = depof.RetrieveById(((Users)Session["Users"]).UnitKerjaID);
                MailMessage msg = new MailMessage();
                EmailReportFacade emailFacade = new EmailReportFacade();
                //string strCC = "iso_krwg@grcboard.com";
                msg.From = new MailAddress("system_support@grcboard.com");
                msg.To.Add(Email);
                msg.CC.Add("iso_krwg@grcboard.com");
                //msg.To.Add("iso_krwg@grcboard.com");
                //msg.Bcc.Add("iswan@grcboard.com");
                msg.Subject = "Reminder Penolakan Approval TPP";
                msg.Body += "Yth. Bpk/ Ibu, \n\r";
                msg.Body += "Mohon dicek terkait penolakan TPP: \n\r";
                msg.Body += "No TPP        : " + txtNoPO.Text + "agar segera diperbaiki dan dikoordinasikan dengan Plant Manager / Corporate Manager. \n\r";
                msg.Body += "Batas Waktu koordinasi dan perbaikan maksimal 3 hari setelah notifikasi ini diterima. \n\r\n\r\n\r";
                //msg.Body += "Date        : " + txtTglMulai.Text + "\n\r";
                //msg.Body += "Target Date : " + TglTarget.ToString("dd-MMM-yyyy") + "\n\r";
                //msg.Body += "PIC         : " + ddlPIC.SelectedItem.Text + "\n\r";
                //msg.Body += (Session["AlasanCancel"] != null) ? "Status : Task di Cancel -> " + Session["AlasanCancel"].ToString() + "\n\r" : "";
                string plant = ""; string plant1 = "";
                switch (depo.ID) { case 1: plant = "ctrp"; plant1 = "123.123.123.129"; break; case 7: plant = "krwg"; plant1 = "10.0.0.252"; break; default: plant1 = ""; plant = "purchasing"; break; }
                //msg.Body += (plant1 == "") ? "" : "Silahkan Klik : http://" + plant1 + ":212" + "\n\r";
                //msg.Body += "Silahkan Klik : http://" + plant + ".grcboard.com" + "\n\r\n\r";
                msg.Body += "Terimakasih, " + "\n\r";
                msg.Body += "Salam GRCBOARD " + "\n\r\n\r\n\r";
                //msg.Body += "Regard's, " + "\n\r";
                //msg.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
                //msg.Body += emailFacade.mailFooter();
                SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                smt.Host = emailFacade.mailSmtp();
                smt.Port = emailFacade.mailPort();
                smt.EnableSsl = true;
                smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smt.UseDefaultCredentials = false;
                smt.Credentials = new System.Net.NetworkCredential("sodikin@grcboard.com", "grc123!@#");
                //bypas certificate
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smt.Send(msg);
                clearForm();
                resultMailSucc.Visible = true;
                resultMailSucc.Text = "Email terkirim";
                resultMailFail.Visible = false;
                resultMailFail.Text = string.Empty;
            }
            catch (Exception ex)
            {
                resultMailSucc.Visible = true;
                resultMailSucc.Text = "Email gagal terkirim " + ex.Message;
                resultMailFail.Visible = false;
                resultMailFail.Text = string.Empty;
            }
        }

        public void KirimEmailApproved(string Email)
        {
            try
            {
                Depo depo = new Depo();
                DepoFacade depof = new DepoFacade();
                depo = depof.RetrieveById(((Users)Session["Users"]).UnitKerjaID);
                MailMessage msg = new MailMessage();
                EmailReportFacade emailFacade = new EmailReportFacade();
                //string strCC = "iso_krwg@grcboard.com";
                msg.From = new MailAddress("system_support@grcboard.com");
                msg.To.Add(Email);
                //msg.CC.Add("iso_krwg@grcboard.com");
                //msg.To.Add("ardiyoga@grcboard.com");
                //msg.Bcc.Add("iswan@grcboard.com");
                msg.Subject = "Reminder Approval TPP";
                msg.Body += "Yth. Bpk/ Ibu, \n\r";
                msg.Body += "Mohon segera di Approved TPP: \n\r";
                msg.Body += "No TPP        : " + txtNoPO.Text + " \n\r\n\r\n\r";
                //msg.Body += "Date        : " + txtTglMulai.Text + "\n\r";
                //msg.Body += "Target Date : " + TglTarget.ToString("dd-MMM-yyyy") + "\n\r";
                //msg.Body += "PIC         : " + ddlPIC.SelectedItem.Text + "\n\r";
                //msg.Body += (Session["AlasanCancel"] != null) ? "Status : Task di Cancel -> " + Session["AlasanCancel"].ToString() + "\n\r" : "";
                string plant = ""; string plant1 = "";
                switch (depo.ID) { case 1: plant = "ctrp"; plant1 = "123.123.123.129"; break; case 7: plant = "krwg"; plant1 = "10.0.0.252"; break; default: plant1 = ""; plant = "purchasing"; break; }
                //msg.Body += (plant1 == "") ? "" : "Silahkan Klik : http://" + plant1 + ":212" + "\n\r";
                //msg.Body += "Silahkan Klik : http://" + plant + ".grcboard.com" + "\n\r\n\r";
                msg.Body += "Terimakasih, " + "\n\r";
                msg.Body += "Salam GRCBOARD " + "\n\r\n\r\n\r";
                //msg.Body += "Regard's, " + "\n\r";
                //msg.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
                //msg.Body += emailFacade.mailFooter();
                SmtpClient smt = new SmtpClient(emailFacade.mailSmtp());
                smt.Host = emailFacade.mailSmtp();
                smt.Port = emailFacade.mailPort();
                smt.EnableSsl = true;
                smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smt.UseDefaultCredentials = false;
                smt.Credentials = new System.Net.NetworkCredential("sodikin@grcboard.com", "grc123!@#");
                //bypas certificate
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smt.Send(msg);
                clearForm();
                resultMailSucc.Visible = true;
                resultMailSucc.Text = "Email terkirim";
                resultMailFail.Visible = false;
                resultMailFail.Text = string.Empty;
            }
            catch (Exception ex)
            {
                resultMailSucc.Visible = true;
                resultMailSucc.Text = "Email gagal terkirim " + ex.Message;
                resultMailFail.Visible = false;
                resultMailFail.Text = string.Empty;
            }
        }
        protected void btnApprove_Click1(object sender, EventArgs e)
        {

        }
    }
}