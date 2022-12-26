using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GRCweb1.Modul.Mtc
{
    public partial class SerahWorkOrder_New : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                Users users = (Users)Session["Users"];
                WorkOrder_New wor = new WorkOrder_New();
                WorkOrderFacade_New worf = new WorkOrderFacade_New();
                int StatusApv = worf.RetrieveUserLevel1(users.ID);
                Session["StatusApv"] = StatusApv;

                WorkOrder_New wor5 = new WorkOrder_New();
                WorkOrderFacade_New worf5 = new WorkOrderFacade_New();
                int DeptID = worf5.RetrieveDeptIDHead(users.ID);

                if (StatusApv == 1 || StatusApv == 3 && DeptID == 19 || StatusApv == 10 || StatusApv == 0 && users.DeptID == 11 || StatusApv == 4)
                {
                    LabelJudul.Visible = true;
                    LabelJudul.Text = "SERAH TERIMA PEKERJAAN";

                    PanelSatu.Visible = true; PanelDua.Visible = true; PanelTiga.Visible = true; PanelEmpat.Visible = true;
                    LabelPeminta.Visible = true; txtPeminta.Visible = true; LabelTglBuat.Visible = true; txtTglBuat.Visible = true;
                    LabelTglDisetujui.Visible = true; txtTglDisetujui.Visible = true; LabelDept.Visible = true; LabelTargetSelesai.Visible = true;
                    txtTargetSelesai.Visible = true;
                    LabelPelaksana.Visible = true; txtPelaksana.Visible = true; txtTargetSelesai.ReadOnly = false; txtPelaksana.ReadOnly = false;
                    LabelWajibisi1.Visible = true; LabelWajibisi1.Visible = false; LabelWajibisi2.Visible = false;
                    LabelFinishDate.Visible = true; txtFinishDate.Visible = true; btnApprove.Visible = true;
                    PanelAreaKerja.Visible = true; ddlArea.Visible = true; LabelAreaKerja.Visible = true;
                    txtPenerima.Visible = true; LabelPerbaikan.Visible = true; txtPerbaikan.Visible = true;

                    LoadList();
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi NoWo untuk Mencari!!!!");
            }
            else
            {
                LoadFindWO();
            }

        }

        private ArrayList LoadFindWO()
        {
            ArrayList arrWO = new ArrayList();
            try
            {
                WorkOrder_New WO2 = new WorkOrder_New();
                WorkOrderFacade_New FacadeWO2 = new WorkOrderFacade_New();
                WO2 = FacadeWO2.RetrieveByNoWithStatus("A.NoWO", txtSearch.Text);

                LabelIDWO.Text = WO2.WOID.ToString();
                txtNoWO.Text = WO2.NoWO;
                txtPeminta.Text = WO2.DeptName;
                txtTglBuat.Text = WO2.TglDibuat;
                txtTglDisetujui.Text = WO2.TglDisetujui;
                txtUraian.Text = WO2.UraianPekerjaan;
                txtPelaksana.Text = WO2.Pelaksana;
                txtFinishDate.Text = WO2.FinishDate;
                txtTargetSelesai.Text = WO2.TglTarget;
                txtFinishDate.Text = WO2.FinishDate;
                txtPenerima.Text = WO2.ToDeptName;
                txtPerbaikan.Text = WO2.UraianPerbaikan;

                int ToDept = WO2.ToDept;
                int FromDept = WO2.DeptID_Users;

                ddlArea.Items.Clear();
                ddlArea.Items.Add(new ListItem(WO2.AreaWO.Trim(), WO2.AreaWO.Trim()));

                Session["ToDept"] = ToDept;
                Session["AreaWO"] = ddlArea.SelectedItem;
                Session["FromDept"] = FromDept;
                arrWO = new ArrayList();
                return arrWO;
            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
                return arrWO;
            }
        }

        protected void LoadList()
        {
            int StApv = Convert.ToInt32(Session["StatusApv"]);

            LoadListFinishWO();

            if (noWO.Value != string.Empty)
            {
                string[] ListSPP = noWO.Value.Split(',');
                string[] ListOpenPO = ListSPP.Distinct().ToArray();
                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                LoadListFinishWO(ListOpenPO[0].ToString());
                //btnApprove.Enabled = true;
                //btnNotApprove.Enabled = true;
                btnNext.Enabled = (ListOpenPO.Count() > 1) ? true : false;
                ViewState["index"] = idx;
            }
            else
            {
                //btnApprove.Enabled = false;
                btnNext.Enabled = false;
            }
            string[] ListOpenPOx = noWO.Value.Split(',');
            string[] ListOpenPOd = ListOpenPOx.Distinct().ToArray();
            int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            btnNext.Enabled = ((idxx - 1) >= ListOpenPOd.Count()) ? false : true;
            ViewState["index"] = idxx;
            //txtAlasan.Attributes.Add("onkeyup", "onKeyUp()");
            if (Request.QueryString["NoWO"] != null)
            {
                LoadListFinishWO(Request.QueryString["NoWO"].ToString());
            }
        }

        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            string AreaWo = Session["AreaWO"].ToString();
            int ToDept = Convert.ToInt32(Session["ToDept"]);
            int DariWO = Convert.ToInt32(Session["FromDept"]);

            Users users = (Users)Session["Users"];
            WorkOrder_New woR = new WorkOrder_New();
            WorkOrderFacade_New worF = new WorkOrderFacade_New();
            int intResult = 0;

            woR.WOID = Convert.ToInt32(LabelIDWO.Text);
            woR.UserID = users.ID;
            woR.UserName = users.UserName;
            woR.ToDept = ToDept;
            woR.AreaWO = AreaWo.Trim();
            woR.DeptID_Users = DariWO;

            intResult = worF.UpdateWO_Closed(woR);

            if (intResult > -1)
            {
                int intResult2 = 0;
                intResult2 = worF.UpdateWO_Apv_L5(woR);

                Response.Redirect("SerahWorkOrder_New.aspx");
            }

        }

        protected void btnNext_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string[] ListSPP = noWO.Value.Split(',');
                string[] ListOpenPO = ListSPP.Distinct().ToArray();
                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;

                //idx = (idx > ListOpenPO.Count() - 1) ? 0 : idx;
                idx = (idx > ListOpenPO.Count() - 1) ? 0 : idx;
                btnNext.Enabled = ((idx) == ListOpenPO.Count()) ? false : true;
                try
                {
                    ViewState["index"] = idx;
                    LoadListFinishWO(ListOpenPO[idx].ToString());
                }
                catch
                {
                    LoadListFinishWO(ListOpenPO[0].ToString());
                    ViewState["index"] = 0;
                }
                btnPrev.Enabled = (idx > 0) ? true : false;
            }
            catch { }
        }


        private void LoadListFinishWO()
        {
            Users users = (Users)Session["Users"];
            string DeptID = users.DeptID.ToString();
            WorkOrder_New WO = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO = new WorkOrderFacade_New();
            ArrayList arrWO1 = new ArrayList();
            arrWO1 = FacadeWO.RetrieveWO_Finish(DeptID);
            Session["arrWO1"] = arrWO1;

            foreach (WorkOrder_New WO1 in arrWO1)
            {
                if (WO1.NoWO != "")
                {
                    noWO.Value += WO1.NoWO + ",";
                }
            }

            noWO.Value = (noWO.Value != string.Empty) ? noWO.Value.Substring(0, (noWO.Value.Length - 1)) : "0";

        }

        private void LoadListFinishWO(string NoWO)
        {
            //int StApv = Convert.ToInt32(Session["StatusApv"]);

            WorkOrder_New WO2 = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO2 = new WorkOrderFacade_New();
            WO2 = FacadeWO2.RetrieveWOFinishNoWO(NoWO);

            LabelIDWO.Text = WO2.WOID.ToString();
            txtNoWO.Text = WO2.NoWO;
            txtPeminta.Text = WO2.DeptName;
            txtTglBuat.Text = WO2.TglDibuat;
            txtTglDisetujui.Text = WO2.TglDisetujui;
            txtUraian.Text = WO2.UraianPekerjaan;
            txtPelaksana.Text = WO2.Pelaksana;
            txtFinishDate.Text = WO2.FinishDate;
            txtTargetSelesai.Text = WO2.TglTarget;
            txtFinishDate.Text = WO2.FinishDate;
            txtPenerima.Text = WO2.ToDeptName;
            txtPerbaikan.Text = WO2.UraianPerbaikan;

            int ToDept = WO2.ToDept;
            int FromDept = WO2.DeptID_Users;

            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem(WO2.AreaWO.Trim(), WO2.AreaWO.Trim()));

            Session["ToDept"] = ToDept;
            Session["AreaWO"] = ddlArea.SelectedItem;
            Session["FromDept"] = FromDept;

            WorkOrder_New WO3 = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO3 = new WorkOrderFacade_New();
            ArrayList arrWO3 = new ArrayList();
            arrWO3 = FacadeWO3.Retrieve_ListWO_Lampiran(NoWO);

            lstBA.DataSource = arrWO3;
            lstBA.DataBind();
        }

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            WorkOrder_New wrk = (WorkOrder_New)e.Item.DataItem;
            System.Web.UI.WebControls.Image view = (System.Web.UI.WebControls.Image)e.Item.FindControl("view");
            view.Attributes.Add("onclick", "PreviewWO('" + wrk.IDLampiran.ToString() + "')");
        }

        protected void lstBA_Command(object sender, RepeaterCommandEventArgs e)
        { }

        protected void btnPrev_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string[] ListSPP = noWO.Value.Split(',');
                string[] ListOpenPO = ListSPP.Distinct().ToArray();
                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;

                //idx = (idx > ListOpenPO.Count() - 1) ? 0 : idx;
                idx = (idx > ListOpenPO.Count() - 1) ? 0 : idx;
                btnNext.Enabled = ((idx) == ListOpenPO.Count()) ? false : true;
                try
                {
                    ViewState["index"] = idx;
                    LoadListFinishWO(ListOpenPO[idx].ToString());
                }
                catch
                {
                    LoadListFinishWO(ListOpenPO[0].ToString());
                    ViewState["index"] = 0;
                }
                btnPrev.Enabled = (idx > 0) ? true : false;
            }
            catch { }
        }

        private void LoadDeptUsers()
        {
            Users users = (Users)Session["Users"];
            string DeptIDUsers = users.DeptID.ToString();
            string UserName = users.UserName;
            WorkOrder_New DomainWO = new WorkOrder_New();
            WorkOrderFacade_New FacadeWO = new WorkOrderFacade_New();
            string NamaDept = FacadeWO.RetrieveNamaDept(DeptIDUsers);
            Session["DeptID"] = DeptIDUsers;
            Session["UserName"] = UserName;

        }

        public void ClearForm()
        {

        }

        private void LoadDept()
        {

        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        public void LoadGrid()
        {


        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }





        private void AutoNext()
        {
            if (btnNext.Enabled == true)
            {
                btnNext_ServerClick(null, null);
            }
            else if (btnPrev.Enabled == true)
            {
                btnPrev_ServerClick(null, null);
            }
            else
            {
                Response.Redirect("SerahWorkOrder_New.aspx");
            }
        }

    }
}