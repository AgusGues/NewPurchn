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
using System.IO;
using Domain;
using BusinessFacade;

namespace GRCweb1.Modul.MTC
{
    public partial class LapImprovement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtUrl.Value = (Request.QueryString["p"] != null) ? Request.QueryString["P"].ToString() : "";
                LoadDept();
                btnToForm.Visible = (txtUrl.Value == "2" && ((Users)Session["Users"]).DeptID == 19) ? true : false;
                if (Request.QueryString["n"] != null)
                {
                    LoadImprovement();
                }
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            if (txtCari.Text == string.Empty) return;

            ArrayList arrP = new ArrayList();
            string where = " and (mp.projectName like '%" + txtCari.Text + "%' or mp.Nomor like '" + txtCari.Text + "') ";
            MTC_ProjectFacade mpp = new MTC_ProjectFacade();
            //arrP = mpp.RetrieveProject(where, true);
            //lstProject.DataSource = arrP;
            //lstProject.DataBind();
            LoadImprovement();
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            LoadImprovement();
        }
        protected void btnToForm_Click(object sender, EventArgs e)
        {
            if (txtUrl.Value == "1")
            {
                Response.Redirect("InputProjectNew.aspx?p=1");
            }
            else if (txtUrl.Value == "2")
            {
                Response.Redirect("EstimasiMaterial.aspx?p=2");
            }
        }
        private void LoadDept()
        {
            string[] arrDeptInProject = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeptPemohon", "Project").Split(',');
            ArrayList arrDept = new ArrayList();

            arrDept = new DeptFacade().Retrieve();
            ddlDeptName.Items.Clear();
            ddlDeptName.Items.Add(new ListItem("--Pilih--", "0"));
            if (arrDept.Count > 0)
            {
                foreach (Dept dpt in arrDept)
                {
                    int pos = Array.IndexOf(arrDeptInProject, dpt.ID.ToString());
                    if (pos != -1)
                    {
                        ddlDeptName.Items.Add(new ListItem(dpt.DeptName, dpt.ID.ToString()));
                    }
                    else if (arrDeptInProject.Contains("All"))
                    {
                        ddlDeptName.Items.Add(new ListItem(dpt.DeptName, dpt.ID.ToString()));
                    }
                }
            }
        }
        private void LoadImprovement()
        {
            ArrayList arrP = new ArrayList();
            string where = string.Empty;
            if (ddlStatus.SelectedIndex > 0)
            {
                switch (int.Parse(ddlStatus.SelectedValue))
                {
                    case 21: where = " and mp.Status=2 and mp.RowStatus >=1"; break;
                    case 2: where = "and mp.Status=2 And mp.Approval=2 and mp.RowStatus!=1"; break;
                    case 0: where = " and mp.Approval=0"; break;
                    default: where = " and mp.Status=" + ddlStatus.SelectedValue; break;
                }
            }
            //string where = (ddlStatus.SelectedIndex == 0) ? "" : " and mp.Status =" + ddlStatus.SelectedValue;
            where += (ddlDeptName.SelectedIndex == 0) ? "" : " and mp.DeptID=" + ddlDeptName.SelectedValue;
            where += " and (Nomor IS NOT NULL or Nomor !='')";
            where += (Request.QueryString["n"] != null) ? " and Nomor='" + Request.QueryString["n"].ToString() + "'" : "";
            where = (txtCari.Text.Substring(0, 4) != "Find") ? " and (mp.projectName like '%" + txtCari.Text + "%' or mp.Nomor like '" + txtCari.Text + "') " : where;
            where += " Order By Year(FromDate),Month(FromDate),Nomor,FromDate";
            MTC_ProjectFacade mpp = new MTC_ProjectFacade();
            arrP = mpp.RetrieveProject(where, true);
            lstProject.DataSource = arrP;
            lstProject.DataBind();
        }
        protected void lstProjcet_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string maxBiaya = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "Engineering");
            MTC_Project mpp = (MTC_Project)e.Item.DataItem;
            MTC_ProjectFacade mpf = new MTC_ProjectFacade();
            Label apv = (Label)e.Item.FindControl("txtApv");
            Label sts = (Label)e.Item.FindControl("txtSts");
            ddlDeptName.SelectedValue = mpp.DeptID.ToString();
            switch (mpp.Approval)
            {
                case 0: apv.Text = "Admin"; break;
                case 1: apv.Text = "PM"; break;
                case 2: apv.Text = (mpp.Biaya > decimal.Parse(maxBiaya)) ? "Direksi" : "PM"; break;
            }
            switch (mpp.Status)
            {
                case 0: case 1: sts.Text = "Open"; break;
                case 2:
                    switch (mpp.RowStatus)
                    {
                        case 1: sts.Text = "Finish"; break;
                        case 2: sts.Text = "Hand Over"; break;
                        case 3: sts.Text = "Close"; break;
                        default: sts.Text = "Release"; break;
                    }
                    break;
                case 3: sts.Text = "Close"; break;
                case 4: sts.Text = "Pending"; break;
                default: sts.Text = "Cancel"; break;
            }
            if (txtUrl.Value == "2")
            {
                ArrayList arrData = mpf.RetrieveEstimasiMaterial(mpp.ID);
                Repeater lstMat = (Repeater)e.Item.FindControl("lstEstimasi");

                lstMat.DataSource = arrData;
                lstMat.DataBind();
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("brs");
                tr.Attributes.Add("class", " baris Line3");
            }
            Label biayaAktual = (Label)e.Item.FindControl("txtAktual");
            biayaAktual.Text = Math.Round(mpf.GetTotalBiayaPakai(mpp.ID, "Harga"), 0, MidpointRounding.AwayFromZero).ToString("###,###,##");

        }
        protected void lstProject_Command(object sender, RepeaterCommandEventArgs e)
        {
            if (txtUrl.Value == "3")
            {
                LinkButton lb = (LinkButton)e.Item.FindControl("lnk");
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("brs");
                Repeater lstMat = (Repeater)e.Item.FindControl("lstEstimasi");
                string attrb = tr.Attributes["class"].ToString();
                if (tr.Attributes["class"].ToString() == " baris Line3")
                {
                    lstMat.Visible = false;
                    tr.Attributes.Add("class", " baris EvenRows");
                }
                else
                {
                    lstMat.Visible = true;
                    tr.Attributes.Add("class", " baris Line3");
                }
                int ID = int.Parse(lb.CommandArgument.ToString());
                MTC_ProjectFacade mpf = new MTC_ProjectFacade();
                ArrayList arrData = mpf.RetrieveEstimasiMaterial(ID);
                lstMat.DataSource = arrData;
                lstMat.DataBind();
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem rpt in lstProject.Items)
            {
                ((Label)rpt.FindControl("dsc")).Visible = true;
                ((LinkButton)rpt.FindControl("lnk")).Visible = false;
            }
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Buffer = true;
            Response.BufferOutput = true;
            Response.AddHeader("content-disposition", "attachment;filename=ListImprovement.xls");
            Response.Charset = "utf-8";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            string Html = "<b>REKAP PES</b>";
            Html += "<br>Status Imporvement : " + ddlStatus.SelectedItem.Text;
            Html += "<br>Departement : " + ddlDeptName.SelectedItem.Text;
            string HtmlEnd = "";
            //lstForPrint.RenderControl(hw);
            lst.RenderControl(hw);
            string Contents = sw.ToString();
            Contents = Contents.Replace("border=\"0", "border=\"1");
            Response.Write(Html + Contents + HtmlEnd);
            Response.Flush();
            Response.End();
        }
    }
}