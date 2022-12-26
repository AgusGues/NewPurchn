using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;
using Factory;

namespace GRCweb1.Modul.RMM
{
    public partial class ApvRMM2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = ((Users)Session["Users"]);
                RMMFacade rmmFacade = new RMMFacade();
                Domain.RMM smt = new Domain.RMM();
                string UserID = string.Empty;
                UserID = user.ID.ToString();
                string usertype = rmmFacade.GetUserType(UserID);
                Session["usertype"] = usertype;
                LoadDept();
                LoadOpenRMM();
                LoadDataRMM();
                btnApprove.Enabled = false;
                btnUnApprove.Enabled = false;
            }
            if (Request.QueryString["rmmNo"] != null)
            {
                LoadOpenRMM(Request.QueryString["rmmNo"].ToString());
            }
            if ((Session["usertype"].ToString().Trim() == "head iso") || (Session["usertype"].ToString().Trim() == "Makrufspp") || (Session["usertype"].ToString().Trim() == "iso"))
            {
                IsoOnly.Visible = true;
            }
            else
            {
                IsoOnly.Visible = false;
            }
        }

        private void LoadDept()
        {
            ddlDept.Items.Clear();
            ArrayList arrDept = new ArrayList();
            RMM_Dept sarmutdept = new RMM_Dept();
            RMM_DeptFacade deptFacade = new RMM_DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDept.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (RMM_Dept dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.Departemen.ToUpper().Trim(), dept.ID.ToString()));
            }
            Users user = ((Users)Session["Users"]);
            //sarmutdept = deptFacade.GetUserDept(user.ID);
            //ddlDept.SelectedValue = sarmutdept.ID.ToString();
        }

        private void LoadOpenRMM(string RMMNum)
        {
            Users user = (Users)Session["Users"];
            RMMFacade rmmf = new RMMFacade();
            Domain.RMM rmm = new Domain.RMM();
            string UserInput = Session["UserInput"].ToString();
            rmm = rmmf.RetrieveRMMNum(RMMNum, UserInput);


            //txtRMM_No.Text = rmm.RMM_No;
            //txtDate.Text = rmm.Tgl_RMM.ToString("dd-MMM-yyyy");
            //txtDeptF.Text = rmm.DeptName;
            //txtApv.Text = rmm.Approval;
            //txtAktivitas.Text = rmm.Aktivitas;
            //txtID.Text = rmm.ID.ToString();

        }

        private void LoadOpenRMM()
        {
            Users user = ((Users)Session["Users"]);
            ArrayList arrRMM = new ArrayList();
            RMMFacade rmmFacade = new RMMFacade();
            Domain.RMM rmm = new Domain.RMM();
            string UserID = string.Empty;
            UserID = user.ID.ToString();
            string Apv = rmmFacade.GetApv(UserID);
            string ApvRMM = rmmFacade.GetStatusApv(UserID);
            Session["Apv"] = Apv;
            Session["ApvRMM"] = ApvRMM;
            string UserInput = rmmFacade.GetUserI(UserID);
            string UserInput1 = rmmFacade.GetUserIGrid(UserID);
            Session["UserInput"] = UserInput;
            Session["UserInput1"] = UserInput1;
            arrRMM = rmmFacade.RetrieveOpenRMMHeader(UserInput, Apv);
            foreach (Domain.RMM rmm1 in arrRMM)
            {
                if (rmm1.RMM_No != string.Empty)
                {

                    rMMNO.Value += rmm1.RMM_No + ",";
                }
            }

        }

        private void ListRMMDept()
        {
            Users users = (Users)Session["Users"];
            string strSQuery = string.Empty;
            RMMFacade rmf = new RMMFacade();
            Domain.RMM rm = new Domain.RMM();
            string UserHead = users.ID.ToString();
            ArrayList arrData = new ArrayList();
            arrData = rmf.RetrieveDept(ddlDept.SelectedValue, UserHead);
            lstAppRMM.DataSource = arrData;
            lstAppRMM.DataBind();
        }

        private ArrayList LoadDataRMM()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrRMM = new ArrayList();
            RMMFacade rmmf = new RMMFacade();
            string UserInput1 = Session["UserInput1"].ToString();
            string UserHead = users.ID.ToString();
            arrRMM = rmmf.RetrieveRMM(UserHead, UserInput1);
            lstAppRMM.DataSource = arrRMM;
            lstAppRMM.DataBind();
            return arrRMM;
        }

        protected void lstAppRMM_DataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        protected void chk_CheckedChangePrs(object sender, EventArgs e)
        {
            Domain.RMM rmm = new Domain.RMM();
            CheckBox chk = (CheckBox)sender;
            if (chk.Checked == true)
            {
                btnApprove.Enabled = true; btnUnApprove.Enabled = true;

            }
            else if (chk.Checked == false)
            {
                btnApprove.Enabled = false; btnUnApprove.Enabled = false;
            }
            //foreach (RepeaterItem ex in lstAppRMM.Controls)
            //{
            //    foreach (Control C in ex.Controls)
            //    {
            //        CheckBox cb = C as CheckBox;
            //        if ((cb != null) && (cb != sender))
            //            cb.Checked = false;

            //    }
            //}
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lstAppRMM.Items.Count; i++)
            {
                CheckBox chk = (CheckBox)lstAppRMM.Items[i].FindControl("chkprs");
                if (chk.Checked == true)
                {
                    //int intResult = 0;
                    Users user = (Users)Session["Users"];
                    Domain.RMM rmm = new Domain.RMM();
                    RMMFacade rmmF = new RMMFacade();
                    string strError = string.Empty;
                    string UserID = user.ID.ToString();
                    int Apv = Convert.ToInt32(Session["Apv"].ToString());

                    rmm.Apv = Apv;
                    rmm.ID = int.Parse(chk.ToolTip.ToString());
                    rmm.User_ID = int.Parse(UserID);
                    rmm.LastModifiedBy = user.UserName;
                    rmm.DeptID = ((Users)Session["Users"]).DeptID;

                    RMMProsesFacade ProsesFacade = new RMMProsesFacade(rmm);
                    strError = ProsesFacade.Update1();
                    LoadOpenRMM();
                    if (strError == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Approval berhasil");
                    }

                }
            }
            LoadOpenRMM();
            LoadDataRMM();
            btnApprove.Enabled = false;
            btnUnApprove.Enabled = false;
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListRMMDept();
        }

        protected void chk_CheckedChange(object sender, EventArgs e)
        {
            int i = 0;
            string transID = string.Empty;
            foreach (RepeaterItem objDetail in lstAppRMM.Items)
            {
                CheckBox chk = (CheckBox)lstAppRMM.Items[i].FindControl("chkprs");
                chk.Checked = chkAll.Checked;
                transID = chk.ToolTip;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                //if (chk.Checked == true)
                //    zl.CustomQuery = "update spd_trans set checked=1 where id=" + transID;
                //else
                //    zl.CustomQuery = "update spd_trans set checked=0 where id=" + transID;
                SqlDataReader sdr = zl.Retrieve();
                i++;

                btnApprove.Enabled = true;
                btnUnApprove.Enabled = true;
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}