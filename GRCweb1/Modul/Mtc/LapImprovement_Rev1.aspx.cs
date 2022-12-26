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

namespace GRCweb1.Modul.Mtc
{
    public partial class LapImprovement_Rev1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users user = ((Users)Session["Users"]);
                Global.link = "~/Default.aspx";
                txtUrl.Value = (Request.QueryString["p"] != null) ? Request.QueryString["P"].ToString() : "";
                LoadDept();
                btnToForm.Visible = (txtUrl.Value == "2" && ((Users)Session["Users"]).DeptID == 19) ? true : false;
                txtTanggal.Text = DateTime.Now.AddDays(-31).ToString("dd-MMM-yyyy");
                txtTanggal0.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                if (Request.QueryString["n"] != null)
                {
                    LoadImprovement();
                }

                //if (user.DeptID == 7 || user.DeptID == 19)
                //{
                //    ddlDeptName.Enabled = false;
                //}
            }
         ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            if (txtCari.Text == string.Empty) return;

            ArrayList arrP = new ArrayList();
            string where = " and (mp.projectName like '%" + txtCari.Text + "%' or mp.Nomor like '" + txtCari.Text + "') ";
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
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
                Response.Redirect("InputProjectNew_Rev1.aspx?p=1");
            }
            else if (txtUrl.Value == "2")
            {
                Response.Redirect("EstimasiMaterial_Rev1.aspx?p=2");
            }
        }
        private void LoadDept()
        {
            Users user = ((Users)Session["Users"]);
            string[] arrDeptInProject = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeptPemohon", "Project").Split(',');

            if (user.DeptID == 7 || user.DeptID == 19 || user.DeptID == 4)
            {
                RBM.Checked = true; RBK.Checked = false;
            }
            else
            {
                RBK.Checked = true; RBM.Enabled = false;
            }

            if (RBM.Checked == true)
            {
                if ((user.DeptID == 19 && user.Apv > 1 || user.DeptID == 18 && user.Apv > 1 || user.DeptID == 4 && user.Apv > 1
                    || user.DeptID == 5 && user.Apv > 1 || user.DeptID == 19) || (user.DeptID == 7))
                {
                    if (ddlStatus.SelectedValue == "4")
                    {
                        ddlBulan.Visible = true; ddlTahun.Visible = true;
                    }



                    ArrayList arrDept = new ArrayList();
                    arrDept = new DeptFacade().RetrievePerDept7(user.DeptID);
                    Session["arrDept"] = arrDept;

                    if (user.DeptID == 7)
                    {
                        ddlP.Items.Clear();
                        ddlP.Items.Add(new ListItem("HRD GA", "7"));
                    }
                    else if (user.DeptID == 19 || user.DeptID == 4)
                    {
                        ddlP.Items.Clear();
                        ddlP.Items.Add(new ListItem("MAINTENANCE", "19"));
                    }


                }
                else if (user.DeptID != 19 || user.DeptID != 18 || user.DeptID != 4 || user.DeptID != 5)
                {
                    ArrayList arrDept = new ArrayList();
                    arrDept = new DeptFacade().RetrievePerDept(user.DeptID);
                    Session["arrDept"] = arrDept;
                }

                ArrayList arrDept1 = (ArrayList)Session["arrDept"];

                ddlDeptName.Items.Clear();
                ddlDeptName.Items.Add(new ListItem("--Pilih--", "0"));

                if (arrDept1.Count > 0)
                {
                    foreach (Dept dpt in arrDept1)
                    {
                        //int pos = Array.IndexOf(arrDeptInProject, dpt.ID.ToString());
                        //if (pos != -1)
                        if (dpt.ID > 0)
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
            else if (RBK.Checked == true)
            {
                if ((user.DeptID == 19 && user.Apv > 1 || user.DeptID == 18 && user.Apv > 1 || user.DeptID == 4 && user.Apv > 1
                        || user.DeptID == 5 && user.Apv > 1 || user.DeptID == 19) || (user.DeptID == 7))
                {
                    if (ddlStatus.SelectedValue == "4")
                    {
                        ddlBulan.Visible = true; ddlTahun.Visible = true;
                    }
                    ArrayList arrDept = new ArrayList();
                    arrDept = new DeptFacade().RetrievePerDept7(user.DeptID);
                    Session["arrDept"] = arrDept;

                    //if (user.DeptID == 7)
                    //{
                    //    ddlP.Items.Clear();
                    //    ddlP.Items.Add(new ListItem("MAINTENANCE", "19"));
                    //}
                    //else if (user.DeptID == 19)
                    //{
                    //    ddlP.Items.Clear();
                    //    ddlP.Items.Add(new ListItem("HRD GA", "7"));
                    //}


                }
                else if (user.DeptID != 19 || user.DeptID != 18 || user.DeptID != 4 || user.DeptID != 5)
                {
                    ArrayList arrDept = new ArrayList();
                    arrDept = new DeptFacade().RetrievePerDept(user.DeptID);
                    Session["arrDept"] = arrDept;
                }

                ArrayList arrDept1 = (ArrayList)Session["arrDept"];

                ddlDeptName.Items.Clear();
                ddlDeptName.Items.Add(new ListItem("--Pilih--", "0"));
                if (arrDept1.Count > 0)
                {
                    foreach (Dept dpt in arrDept1)
                    {
                        //int pos = Array.IndexOf(arrDeptInProject, dpt.ID.ToString());
                        //if (pos != -1)
                        if (dpt.ID > 0)
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


        }

        private void LoadImprovement()
        {
            Users user = ((Users)Session["Users"]);
            ArrayList arrP = new ArrayList();
            string where = string.Empty;

            if (ddlTahun.SelectedValue == "" && ddlBulan.SelectedValue == "")
            {
                int Tahun = 0; int Bulan = 0; Session["Bulan"] = Bulan; Session["Tahun"] = Tahun;
            }
            else if (ddlTahun.SelectedValue != "" && ddlBulan.SelectedValue != "")
            {
                int Tahun = Convert.ToInt32(ddlTahun.SelectedValue);
                int Bulan = Convert.ToInt32(ddlBulan.SelectedValue);
                Session["Bulan"] = Bulan; Session["Tahun"] = Tahun;
            }

            int Bulan1 = Convert.ToInt32(Session["Bulan"]);
            int Tahun1 = Convert.ToInt32(Session["Tahun"]);
            string DeptPenerimaIMP = ddlP.SelectedValue;

            if (ddlStatus.SelectedIndex > 0)
            {
                switch (int.Parse(ddlStatus.SelectedValue))
                {
                    // Close
                    case 3:
                        where =
               " and (mp.Status=2 and mp.RowStatus=2 and mp.Approval=5 and mp.Release=1 and mp.ApvPM=3)"; break;

                    // Finish
                    case 21:
                        where =
               " and (mp.Status=2 and mp.RowStatus=2 and mp.Approval=3 and mp.Release=1 and mp.ApvPM=2)"; break;

                    // Release
                    case 2:
                        where =
                " and (mp.Status=2 and mp.RowStatus=2 and mp.Approval=2 and mp.Release=1 and mp.ApvPM=2)"; break;

                    // Serah Terima Mgr Peminta
                    case 22:
                        where =
               " and (mp.Status=2 and mp.RowStatus=2 and mp.Approval=4 and mp.Release=1 and mp.ApvPM=2)"; break;

                    // Open
                    case 0:
                        where =
                " and ((mp.Status=0 and mp.RowStatus=0 and mp.Approval=1) or " + // Status Admin
                " (mp.Status=0 and mp.RowStatus=1 and mp.Approval=1) or " + // Approval Manager Pembuat
                " (mp.Status=1 and mp.RowStatus=1 and mp.Approval=1 and (mp.VerUser is null or mp.VerUser=0) and mp.ApvPM=1) or  " + // Approval Head Enginering
                " (mp.Status=1 and mp.RowStatus=1 and mp.Approval=1 and mp.VerUser=1 and mp.ApvPM=1) or  " + // Approved Mgr Peminta
                " (mp.Status=1 and mp.RowStatus=1 and mp.Approval=2 and mp.VerUser=1 and mp.ApvPM=1))  "; break; // Approved Mgr MTN

                    // Tanggal Target
                    case 4:
                        where =
                " "; break;

                    default: where = " and mp.Status=" + ddlStatus.SelectedValue; break;
                }
            }

            if ((user.DeptID == 19 && user.Apv > 1
                || user.DeptID == 18 && user.Apv > 1
                || user.DeptID == 4 && user.Apv > 1
                || user.DeptID == 5 && user.Apv > 1
                || user.DeptID == 19) || (user.DeptID == 7))
            {
                if (RBM.Checked == true)
                {
                    if (user.DeptID == 19 || user.DeptID == 4)
                    {
                        where += "  and mp.DeptID=" + ddlDeptName.SelectedValue + " and mp.ToDeptID=19 ";
                    }
                    else if (user.DeptID == 7)
                    {
                        where += "  and mp.DeptID=" + ddlDeptName.SelectedValue + " and mp.ToDeptID=7 ";
                    }
                }
                else if (RBK.Checked == true)
                {
                    if (user.DeptID == 19 || user.DeptID == 4)
                    {
                        where += "  and mp.DeptID=" + ddlDeptName.SelectedValue + " and mp.ToDeptID=7 ";
                    }
                    else if (user.DeptID == 7)
                    {
                        where += "  and mp.DeptID=" + ddlDeptName.SelectedValue + " and mp.ToDeptID=19 ";
                    }
                }

                where += " and (Nomor IS NOT NULL or Nomor !='')";

                if (ddlStatus.SelectedValue == "4")
                {
                    where += " and YEAR(mp.ToDate)='" + Tahun1 + "' and MONTH(mp.ToDate)='" + Bulan1 + "' ";
                }
                else
                { where += " "; }

                where += (Request.QueryString["n"] != null) ? " and Nomor='" + Request.QueryString["n"].ToString() + "'" : "";
                where = (txtCari.Text.Substring(0, 4) != "Find") ? " and (mp.projectName like '%" + txtCari.Text + "%' or mp.Nomor like '" + txtCari.Text + "') " : where;
                where += " Order By Year(FromDate2),Month(FromDate2),Nomor,FromDate2";
            }
            else if (user.DeptID != 19)
            {
                where += (ddlDeptName.SelectedIndex == 0) ? "" : " and mp.DeptID=" + ddlDeptName.SelectedValue;
                where += " and mp.ToDeptID=" + ddlP.SelectedValue + " and (Nomor IS NOT NULL or Nomor !='')";
                if (ddlStatus.SelectedValue == "4")
                {
                    where += " and YEAR(mp.ToDate)='" + Tahun1 + "' and MONTH(mp.ToDate)='" + Bulan1 + "' ";
                }
                else
                { where += " "; }

                where += (Request.QueryString["n"] != null) ? " and Nomor='" + Request.QueryString["n"].ToString() + "'" : "";
                where = (txtCari.Text.Substring(0, 4) != "Find") ? " and (mp.projectName like '%" + txtCari.Text + "%' or mp.Nomor like '" + txtCari.Text + "') " : where;
                where += " Order By Year(FromDate2),Month(FromDate2),Nomor,FromDate2";
            }

            if (ddlStatus.SelectedValue != "-1")
            {
                string where2 = string.Empty;
                where2 =
                    //"case when mp.Approval=2 and mp.ApvPM=2  then LEFT(convert(char,mp.todate,106),11) end ToDate2,";
                    " LEFT(convert(char,mp.todate,106),11) ToDate2,";
                Session["where2"] = where2;

                //if (ddlStatus.SelectedValue == "2")
                //{
                //    where2 = 
                //    //"case when mp.Approval=2 and mp.ApvPM=2  then LEFT(convert(char,mp.todate,106),11) end ToDate2,";
                //    " LEFT(convert(char,mp.todate,106),11) ToDate2,";
                //    Session["where2"] = where2;
                //}
            }
            //Session["where2"] = where2;
            MTC_ProjectFacade_Rev1 mpp = new MTC_ProjectFacade_Rev1();
            arrP = mpp.RetrieveProject1(where, true, DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtTanggal0.Text).ToString("yyyyMMdd"));
            lstProject.DataSource = arrP;
            lstProject.DataBind();
        }

        protected void lstProjcet_DataBound(object sender, RepeaterItemEventArgs e)
        {
            string maxBiaya = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
            MTC_Project_Rev1 mpp = (MTC_Project_Rev1)e.Item.DataItem;
            MTC_ProjectFacade_Rev1 mpf = new MTC_ProjectFacade_Rev1();
            Label apv = (Label)e.Item.FindControl("txtApv");
            Label sts = (Label)e.Item.FindControl("txtSts");
            Label tgl = (Label)e.Item.FindControl("txtDate");
            //ddlDeptName.SelectedValue = mpp.DeptID.ToString();

            //if (mpp.VerPM < 0)
            //{
            //    sts.Text = "Not Approved by " + mpp.LastModifiedBy2 ;
            //}

            if (mpp.Approval == 1 && mpp.Status == 0 && mpp.RowStatus == 0)
            {
                apv.Text = "Admin";
            }
            else if (mpp.Approval != 1 && mpp.Biaya < 50000000)
            {
                apv.Text = "PM";
            }
            else if (mpp.Approval != 1 && mpp.Biaya > 50000000 && mpp.ApvDir == 1)
            {
                apv.Text = "Direksi";
            }
            else
            { apv.Text = " - "; }

            //switch (mpp.Approval)
            //{
            //    case 0: apv.Text = "Admin"; break;
            //    case 1: apv.Text = "PM"; break;
            //    case 2: apv.Text = (mpp.Biaya > decimal.Parse(maxBiaya)) ? "Direksi" : "PM"; break;
            //}

            if (mpp.Approval == 3 && mpp.Status == 2 && mpp.RowStatus == 2)
            {
                sts.Text = "Finish";
            }
            else if (mpp.Approval == 2 && mpp.Status == 2 && mpp.RowStatus == 2 && mpp.ApvDir != 1)
            {
                sts.Text = "Release By PM";
            }
            else if (mpp.Approval == 2 && mpp.Status == 2 && mpp.RowStatus == 2 && mpp.ApvDir == 1)
            {
                sts.Text = "Release By Direksi";
            }
            else if (mpp.Approval == 5 && mpp.Status == 2 && mpp.RowStatus == 2 && mpp.VerPM == 3)
            {
                sts.Text = "Close";
            }
            else if (mpp.Approval == 1 && mpp.Status == 0 && mpp.RowStatus == 0)
            {
                sts.Text = "Open - Admin";
            }
            else if (mpp.Approval == 1 && mpp.Status == 0 && mpp.RowStatus == 1 && mpp.VerPM == 0)
            {
                sts.Text = " Aproved Mgr >> PM-1";
            }
            else if (mpp.Approval == 1 && mpp.Status == 0 && mpp.RowStatus == 1 && mpp.VerPM == 1 && mpp.ToDeptID == 19)
            {
                sts.Text = " Aproved PM-1 >> Head Engineering";
            }
            else if (mpp.Approval == 1 && mpp.Status == 0 && mpp.RowStatus == 1 && mpp.VerPM == 1 && mpp.ToDeptID == 7)
            {
                sts.Text = " Aproved PM-1 >> Head GA";
            }
            else if (mpp.Approval == 1 && mpp.Status == 1 && mpp.RowStatus == 1 && (mpp.VerUser == 0) && mpp.ToDeptID == 19)
            {
                sts.Text = "Apv Head Eng >> Ver. Mgr Peminta";
            }
            else if (mpp.Approval == 1 && mpp.Status == 1 && mpp.RowStatus == 1 && (mpp.VerUser == 0) && mpp.ToDeptID == 7)
            {
                sts.Text = "Apv Head GA >> Ver. Mgr Peminta";
            }
            else if (mpp.Approval == 1 && mpp.Status == 1 && mpp.RowStatus == 1 && mpp.VerUser == 1 && mpp.ToDeptID == 19)
            {
                sts.Text = "Ver. Mgr Peminta>> MGR MTN";
            }
            else if (mpp.Approval == 1 && mpp.Status == 1 && mpp.RowStatus == 1 && mpp.VerUser == 1 && mpp.ToDeptID == 7)
            {
                sts.Text = "Ver. Mgr Peminta>> MGR HRD";
            }
            else if (mpp.Approval == 2 && mpp.Status == 1 && mpp.RowStatus == 1 && mpp.ToDeptID == 19)
            {
                sts.Text = "Apv Mgr MTN >> PM-2";
            }
            else if (mpp.Approval == 2 && mpp.Status == 1 && mpp.RowStatus == 1 && mpp.ToDeptID == 7)
            {
                sts.Text = "Apv Mgr HRD >> PM-2";
            }
            if (mpp.ToDate2 == "11 Nov 2000")
            {
                tgl.Text = "X";
            }
            else
            {
                tgl.Text = mpp.ToDate2;
            }

            if (mpp.VerPM < 0)
            {
                sts.Text = "Not Approved by " + mpp.LastModifiedBy2;
            }

            //switch (mpp.Status)
            //{
            //    case 0:case 1: sts.Text = "Open"; break;
            //    case 2:
            //        switch (mpp.RowStatus)
            //        {
            //            case 1: sts.Text = "Finish"; break;
            //            case 2: sts.Text = "Hand Over"; break;
            //            case 3: sts.Text = "Close"; break;
            //            default: sts.Text = "Release"; break;
            //        }
            //        break;
            //    case 3: sts.Text = "Close"; break;
            //    case 4: sts.Text = "Pending"; break;
            //    default: sts.Text = "Cancel"; break;
            //}

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
                MTC_ProjectFacade_Rev1 mpf = new MTC_ProjectFacade_Rev1();
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
            Response.AddHeader("content-disposition", "attachment;filename=ListImprovement_Rev1.xls");
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

        protected void ddlStatus_change(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue == "4")
            {
                ddlBulan.Visible = true; ddlTahun.Visible = true;
                LabelPeriode.Visible = true;
                LabelPeriode.Text = "Periode";
            }
            else
            {
                ddlBulan.Visible = false; ddlTahun.Visible = false;
                LabelPeriode.Visible = false;
            }
        }

        protected void RBM_CheckedChanged(object sender, EventArgs e)
        {
            Users user = ((Users)Session["Users"]);
            RBM.Checked = true; RBK.Checked = false;

            if (user.DeptID == 7)
            {
                ddlP.Items.Clear();
                ddlP.Items.Add(new ListItem("HRD GA", "7"));
            }
            else if (user.DeptID == 19)
            {
                ddlP.Items.Clear();
                ddlP.Items.Add(new ListItem("MAINTENANCE", "19"));
            }
        }

        protected void RBK_CheckedChanged(object sender, EventArgs e)
        {
            RBM.Checked = false; RBK.Checked = true;
            Users user = ((Users)Session["Users"]);

            if (user.DeptID == 7)
            {
                ddlP.Items.Clear();
                ddlP.Items.Add(new ListItem("MAINTENANCE", "19"));

                ddlDeptName.Items.Clear();
                ddlDeptName.Items.Add(new ListItem("HRD GA", "7"));
            }
            else if (user.DeptID == 19)
            {
                ddlP.Items.Clear();
                ddlP.Items.Add(new ListItem("HRD GA", "7"));

                ddlDeptName.Items.Clear();
                ddlDeptName.Items.Add(new ListItem("MAINTENANCE", "19"));
            }
        }

    }
}