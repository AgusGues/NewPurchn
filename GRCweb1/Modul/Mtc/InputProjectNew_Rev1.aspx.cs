using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessFacade;
using Domain;
using DataAccessLayer;

namespace GRCweb1.Modul.MTC
{
    public partial class InputProjectNew_Rev1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users user = (Users)Session["Users"];
                Global.link = "~/default.aspx";
                Session["StProject"] = "";
                Session["ProjectID"] = "";
                Session["SubPj"] = "";
                Session["NewProject"] = "yes";
                Session["Cancel"] = "";
                Session["Search"] = "";
                btnDelete.Visible = false;
                txtTglProject.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadProjectGroup();
                LoadDept();
                LoadProdLine();
                string minBiaya = new Inifiles(Server.MapPath("~/App_Data/PurcnConfig.ini")).Read("EstimasiLevel", "EngineeringNew");
                LoadUnit();
                ddlStatus.Enabled = false;
                ddlStatus.SelectedValue = (txtProjectID.Text == string.Empty) ? "0" : ddlStatus.SelectedValue;
                txtMinEstimasi.Text = minBiaya;
                LoadSasaran();

                LoadDataHead();

            }
        }

        private void LoadDataHead()
        {
            Users user = (Users)Session["Users"];
            MTC_ProjectFacade_Rev1 mtcF01 = new MTC_ProjectFacade_Rev1();
            ddlHead.Items.Clear();
            ddlHead.Items.Add(new ListItem("--- Silahkan Pilih Nama Head ---", "0"));
            ArrayList arrHead = mtcF01.GetHead(user.DeptID);
            foreach (MTC_Project_Rev1 head in arrHead)
            {
                if (arrHead.Count == 1)
                {
                    ddlHead.Items.Clear();
                    ddlHead.Items.Add(new ListItem(head.NamaHead, head.NamaHead));
                }
                else if (arrHead.Count > 0)
                {
                    ddlHead.Items.Add(new ListItem(head.NamaHead, head.NamaHead));
                }
            }

            if (arrHead.Count > 0)
            {
                trHead.Visible = true;
            }
            else
            {
                trHead.Visible = false;
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm(); InfoLabelSave.Visible = false;
            LoadProjectGroup();
            LoadDept();
            LoadProdLine(); LoadSasaran();
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            //if (txtNoImprovement.Text == string.Empty)
            //{
            //    DisplayAJAXMessage(this, "Nomor Improvement blm di isi");
            //    return;
            //}
            Users users = (Users)Session["Users"];


            if (ddlHead.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, "Nama Head Belum Dipilih !!");
                return;
            }


            if (ddlDept.SelectedValue == "0")
            {
                DisplayAJAXMessage(this, "Department Belum Dipilih !!");
                return;
            }
            if (txtProjectName.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Permintaan improvment belum diisi !!");
                return;
            }
            if (txtDetailSasaran.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Detail sasaran improvment blm di isi");
                return;
            }

            //penambahan agus 10-05-2022
            if (txtKondisiSebelum.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Kondisi Sebelumnya belum di isi");
            }
            if (txtKondisiyangdiharapkan.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Kondisi yang diharapkan belum di isi");
            }



            Session["Model"] = "";
            int result = 0;
            MTC_Project_Rev1 objP = new MTC_Project_Rev1();
            MTC_ProjectFacade_Rev1 mtc = new MTC_ProjectFacade_Rev1();
            //MTC_Project objP1 = new MTC_Project();
            //MTC_ProjectFacade mtc1 = new MTC_ProjectFacade();

            objP.NamaHead = ddlHead.SelectedItem.ToString();
            Session["NamaHead"] = objP.NamaHead;

            //Penambahan agus 10-05-2022
            objP.KondisiSebelum = txtKondisiSebelum.Text;
            Session["KondisiSebelum"] = objP.KondisiSebelum;

            objP.KondisiYangDiharapkan = txtKondisiyangdiharapkan.Text;
            Session["KondisiYangDiHarapkan"] = objP.KondisiYangDiharapkan;
            //Penambahan agus 10-05-2022

            objP.ProjectDate = DateTime.Parse(txtTglProject.Text);
            Session["ProjectDate"] = objP.ProjectDate;
            objP.NamaProject = txtProjectName.Text.ToUpper();
            Session["NamaProject"] = objP.NamaProject;
            objP.Sasaran = ddlSasaran.SelectedValue.ToString();
            Session["Sasaran"] = objP.Sasaran;
            //objP.ProdLine = int.Parse(ddlProdLine.SelectedValue.ToString());
            objP.ProdLine = int.Parse(ddlProdLine.Text);

            Session["ProdLine"] = objP.ProdLine;
            objP.DeptID = Int32.Parse(txtID.Text);
            Session["DeptID"] = objP.DeptID;
            objP.GroupID = int.Parse(ddlProjectGroup.SelectedValue.ToString());
            Session["GroupID"] = objP.GroupID;
            //objP.FinishDate = DateTime.Parse(txtTglFinish.Text);
            objP.FinishDate = Convert.ToDateTime("11/11/2000 4:00:00");
            Session["FinishDate"] = objP.FinishDate;
            //objP.Biaya = (txtEstimasiBiaya.Text == string.Empty) ? 0 : decimal.Parse(txtEstimasiBiaya.Text);
            objP.Quantity = int.Parse(txtQtyProject.Text);
            Session["Quantity"] = objP.Quantity;
            objP.UOMID = int.Parse(ddlUnit.SelectedValue);
            Session["UOMID"] = objP.UOMID;
            //objP.Approval = int.Parse(ddlApproval.SelectedValue.ToString());
            objP.Approval = 1;
            objP.DetailSasaran = txtDetailSasaran.Text.Trim();
            Session["DetailSasaran"] = objP.DetailSasaran;
            Session["Approval"] = objP.Approval;
            objP.Zona = ddlArea.SelectedItem.ToString().Trim();
            if (Convert.ToInt32(ddlArea.SelectedValue) > 0)
            {
                Session["Zona"] = ddlArea.SelectedItem.ToString().Trim();
            }
            else { Session["Zona"] = ""; }

            int Bln = DateTime.Now.Month;

            if (Bln < 10)
            {
                string Bulan = "0" + Bln;
                Session["Bulan1"] = Bulan;
            }
            else
            {
                string Bulan = Convert.ToInt32(Bln).ToString();
                Session["Bulan1"] = Bulan;
            }

            string Bulan1 = Session["Bulan1"].ToString();

            string Thn = DateTime.Now.Year.ToString();
            string Tahun = Thn.Substring(2, 2);
            objP = mtc.cekNomorImprovment(Bln, Thn);

            if (objP.IDno > 0)
            {
                int Count1 = objP.Count;

                if (Count1 < 10)
                {
                    int CountNumber = 1 + Count1;
                    string CountNo = Convert.ToInt32(CountNumber).ToString();
                    Session["CountNo"] = CountNo;
                    Session["CountNumber"] = CountNumber;
                }
                else
                {
                    int CountNumber = 1 + Count1;
                    string CountNo = Convert.ToInt32(CountNumber).ToString();
                    Session["CountNo"] = CountNo;
                    Session["CountNumber"] = CountNumber;
                }

                int CountNumber1 = Convert.ToInt32(Session["CountNumber"]);

                if (CountNumber1 < 10)
                {
                    string CountNo1 = "0" + Convert.ToInt32(Session["CountNo"]).ToString();
                    Session["CountNo1"] = CountNo1;
                }
                else
                {
                    string CountNo1 = Convert.ToInt32(Session["CountNo"]).ToString();
                    Session["CountNo1"] = CountNo1;
                }

                string CountNo11 = Session["CountNo1"].ToString();
                string No = "I" + CountNo11 + Bulan1 + Tahun;
                Session["No"] = No;

                int ID = objP.IDno;
                objP.IDno = ID;
                objP.Count = int.Parse(Session["CountNumber"].ToString());
                result = mtc.UpdateNo(objP);
            }
            else
            {
                string No = "I" + "01" + Bulan1 + Tahun;
                Session["No"] = No;

                objP.Bulan = int.Parse(Bulan1);
                objP.Tahun = int.Parse(Thn);
                result = mtc.insertNo(objP);
            }

            if (txtProjectID.Text != string.Empty)
            {
                objP.ID = int.Parse(txtProjectID.Text);
                objP.LastModifiedBy = ((Users)Session["Users"]).UserName;
                Session["Model"] = "new";
                result = mtc.Update(objP);
            }
            else
            {
                objP.ProjectDate = Convert.ToDateTime(Session["ProjectDate"]);
                objP.Sasaran = Session["Sasaran"].ToString();
                objP.ProdLine = Convert.ToInt32(Session["ProdLine"].ToString());
                objP.DeptID = Convert.ToInt32(Session["DeptID"].ToString());
                objP.GroupID = Convert.ToInt32(Session["GroupID"].ToString());
                objP.FinishDate = Convert.ToDateTime(Session["FinishDate"]);
                objP.Quantity = Convert.ToInt32(Session["Quantity"].ToString());
                objP.UOMID = Convert.ToInt32(Session["UOMID"].ToString());
                objP.Approval = Convert.ToInt32(Session["Approval"].ToString());
                objP.NamaProject = Session["NamaProject"].ToString();
                objP.Nomor = Session["No"].ToString();
                objP.CreatedBy = ((Users)Session["Users"]).UserName;
                objP.DetailSasaran = Session["DetailSasaran"].ToString();
                objP.Zona = Session["Zona"].ToString();
                objP.ToDept = Convert.ToInt32(ddlDept.SelectedValue);
                objP.NamaHead = Session["NamaHead"].ToString();
                objP.KondisiSebelum = Session["KondisiSebelum"].ToString();
                objP.KondisiYangDiharapkan = Session["KondisiYangDiHarapkan"].ToString();

                result = mtc.InsertNew(objP);
                Session["ID"] = result;

                if (result > -1)
                {
                    InfoLabelSave.Visible = true; InfoLabelSave.Text = "Inputan Berhasil DiSimpan !!";
                    string No = Session["No"].ToString();
                    txtNoImprovement.Text = No;


                }
            }

            if (result >= 0)
            {
                //btnSearch_Click(null, null); 
            }
        }
        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {

        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("LapImprovement.aspx?p=1");
        }
        private void clearForm()
        {
            foreach (object c in Div1.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = string.Empty;
                }
                if (c is DropDownList)
                {
                    ((DropDownList)c).SelectedIndex = 0;
                }
                ddlUnit.SelectedValue = "35";
                txtQtyProject.Text = "1";
            }
            txtTglProject.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
        private void LoadProjectGroup()
        {
            ArrayList arrAset = new ArrayList();
            AssetManagementFacade ast = new AssetManagementFacade();
            arrAset = ast.Retrieve("AM_Group", " order by ID");
            ddlProjectGroup.Items.Clear();
            ddlProjectGroup.Items.Add(new ListItem("--Pilih--", "0"));
            if (ast.Error == string.Empty && arrAset.Count > 0)
            {
                foreach (AssetManagement am in arrAset)
                {
                    ddlProjectGroup.Items.Add(new ListItem(am.NamaGroup, am.ID.ToString()));
                }
            }
        }
        private void LoadDept()
        {
            //string[] arrDeptInProject = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DeptPemohon", "Project").Split(new string[] { "," }, StringSplitOptions.None);
            //ArrayList arrDept = new ArrayList();
            //arrDept = new DeptFacade().Retrieve();
            //ddlDeptName.Items.Clear();
            //ddlDeptName.Items.Add(new ListItem("--Pilih--", "0"));
            //if (arrDept.Count > 0)
            //{
            //    foreach (Dept dpt in arrDept)
            //    {
            //        if (arrDeptInProject.Contains(dpt.ID.ToString()))
            //        {
            //            ddlDeptName.Items.Add(new ListItem(dpt.DeptName, dpt.ID.ToString()));
            //        }
            //        else if (arrDeptInProject.Contains("All"))
            //        {
            //            ddlDeptName.Items.Add(new ListItem(dpt.DeptName, dpt.ID.ToString()));
            //        }
            //    }
            //}

            Users users = (Users)Session["Users"];
            int DeptID = users.DeptID;
            Dept deptName = new Dept();
            DeptFacade deptF = new DeptFacade();
            deptName = deptF.RetriveDeptName(DeptID);
            txtDeptName.Text = deptName.DeptName;
            txtID.Text = users.DeptID.ToString();



        }

        private void LoadArea(string ID, int DeptID)
        {
            MTC_ProjectFacade_Rev1 mtcF = new MTC_ProjectFacade_Rev1();
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem("--Pilih --", "0"));
            ArrayList arrArea = mtcF.GetArea(ID, DeptID);
            foreach (MTC_Project_Rev1 mtc in arrArea)
            {
                ddlArea.Items.Add(new ListItem(mtc.AreaName.ToString(), mtc.IDarea.ToString()));
            }

        }

        private void LoadProdLine()
        {
            ddlProdLine.Items.Clear();
            ddlProdLine.Items.Add(new ListItem("--Pilih --", "0"));
            ArrayList arrDepts = new DeptFacade().GetZona();

            ddlProdLine.Items.Add(new ListItem("WTP", "98"));
            ddlProdLine.Items.Add(new ListItem("Material Preparation", "97"));
            foreach (Plant pl in arrDepts)
            {
                ddlProdLine.Items.Add(new ListItem(pl.ZonaName.ToString(), pl.ID.ToString()));
            }

            ddlProdLine.Items.Add(new ListItem("General", "99"));
        }
        protected void ddlProdLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProdLine.SelectedItem.ToString().Trim() == "Zona 1"
                || ddlProdLine.SelectedItem.ToString().Trim() == "Zona 2"
                || ddlProdLine.SelectedItem.ToString().Trim() == "Zona 3")
            {
                LoadArea(ddlProdLine.SelectedValue, 2);
            }
            else if (ddlProdLine.SelectedItem.ToString().Trim() == "Zona 4")
            {
                LoadArea(ddlProdLine.SelectedValue, 3);
            }
            else { ddlArea.Items.Clear(); ddlArea.Items.Add(new ListItem("--Pilih Line --", "0")); }
        }
        protected void AutoComSelect(object sender, EventArgs e)
        {
            txtSubProjectName.Enabled = true;
        }
        protected void txtProjectName_TextChanged(object sender, EventArgs e)
        {

        }
        private void LoadGridProject()
        {
            ArrayList arrP = new ArrayList();
            Session["StProject"] = " and Status=" + ddlStatus.SelectedValue.ToString();
            Session["ProjectID"] = (txtProjectID.Text == string.Empty) ? string.Empty : " and ID=" + txtProjectID.Text;
            Session["SubPj"] = " and (SubProject is null or SubProject ='') ";
            arrP = new MTC_ProjectFacade().Retrieve();
            lstProject.DataSource = arrP;
            lstProject.DataSource = arrP;

            //MTC_Project mtcDomain = new MTC_Project();
            //MTC_ProjectFacade mtcFacade = new MTC_ProjectFacade();
            //mtcDomain = mtcFacade.RetrieveGrid(Convert.ToInt32(Session["ID"]));
            //lstProject.DataSource = mtcDomain;
            //lstProject.DataBind();
        }
        protected void lstProject_DataBound(object source, RepeaterItemEventArgs e)
        {
            Session["Jml"] = "";
            DataRowView dr = e.Item.DataItem as DataRowView;
            var dPj = e.Item.FindControl("dtlProject") as Repeater;
            if (dPj != null)
            {
                ArrayList arrDp = new ArrayList();
                Label grp = ((Label)e.Item.FindControl("Label1"));
                Label dpt = (Label)e.Item.FindControl("dptName");
                Image img = (Image)e.Item.FindControl("lstDel");
                Label byn = (Label)e.Item.FindControl("lblBiaya");
                MTC_Project mt = e.Item.DataItem as MTC_Project;
                Dept dept = new DeptFacade().RetrieveById(mt.DeptID);
                AssetManagementFacade am = new AssetManagementFacade();
                string amName = am.GetGroupName(mt.GroupID).ToString();
                grp.Text = dept.DeptName;
                arrDp = new MTC_ProjectFacade().RetrieveByCriteria(mt.NamaProject);
                if (arrDp.Count > 0)
                {
                    Session["SubPj"] = " and (SubProject is not null or SubProject !='') ";
                    decimal biaya = new MTC_ProjectFacade().GetTotalBiaya(mt.NamaProject);
                    byn.Text = biaya.ToString("###,##0.00");
                    img.Visible = false;
                    dPj.DataSource = arrDp;
                    dPj.DataBind();
                }
                else
                {
                    Session["SubPj"] = " and (SubProject is null or SubProject='') ";
                    decimal biaya = new MTC_ProjectFacade().GetTotalBiaya(mt.NamaProject);
                    byn.Text = biaya.ToString("###,##0.00");
                    img.Visible = (mt.Progress == -1) ? false : true;
                }
                Label app = (Label)e.Item.FindControl("app");
                switch (mt.Approval)
                {
                    //case 1:
                    //    app.Text = "Plant Manager";
                    //    app.Text = "Open";
                    //    break;
                    //case 2:
                    //    app.Text = "Direksi";
                    //    break;
                    default:
                        app.Text = "Open";
                        break;
                }
                Label sts = (Label)e.Item.FindControl("sts");
                switch (mt.Progress)
                {
                    case 0:
                    case 1: sts.Text = "Open"; break;
                    case 2: sts.Text = "Release"; break;
                    case 21: sts.Text = "Finish"; break;
                    case 3: sts.Text = "Close"; break;
                    case 4: sts.Text = "Pending"; break;
                    default: sts.Text = "Cancel"; break;
                }
                ((Image)e.Item.FindControl("lstEdit")).Visible = (mt.Approval < 2 && mt.RowStatus == 0) ? true : false;
                ((Image)e.Item.FindControl("lstDel")).Visible = (mt.Progress == 0) ? true : false;

            }
        }
        protected void dtlProject_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("grpName");
            if (lbl != null)
            {
                MTC_Project mt = e.Item.DataItem as MTC_Project;
                Dept dept = new DeptFacade().RetrieveById(mt.DeptID);
                AssetManagementFacade am = new AssetManagementFacade();
                string amName = am.GetGroupName(mt.GroupID).ToString();
                lbl.Text = dept.DeptName;
            }
        }
        protected void lstSarmut_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            DataRow dr = (DataRow)e.Item.DataItem;
            switch (e.CommandName)
            {

                case "edit":
                    MTC_Project objP = new MTC_ProjectFacade().RetrieveByID(int.Parse(e.CommandArgument.ToString()));
                    txtTglProject.Text = objP.ProjectDate.ToString("dd-MMM-yyyy");
                    ddlProjectGroup.SelectedValue = objP.GroupID.ToString();
                    txtProjectName.Text = objP.NamaProject;
                    txtSubProjectName.Enabled = false;
                    txtProjectName.Enabled = true;
                    txtSubProjectName.Text = objP.SubProjectName.ToString();
                    ddlProdLine.SelectedValue = objP.ProdLine.ToString();
                    //ddlDeptName.SelectedValue = objP.DeptID.ToString();
                    //txtTglFinish.Text = objP.FinishDate.ToString("dd-MMM-yyyy");
                    txtEstimasiBiaya.Text = objP.Biaya.ToString("###,##0.00");
                    ddlStatus.SelectedValue = objP.Progress.ToString();
                    txtProjectID.Text = objP.ID.ToString();
                    txtNoImprovement.Text = objP.Nomor.ToUpper();
                    //ddlApproval.SelectedValue = objP.Approval.ToString();

                    break;
                case "del":
                    MTC_Project ojbP = new MTC_Project();
                    ojbP.ID = int.Parse(e.CommandArgument.ToString());
                    ojbP.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    int result = new MTC_ProjectFacade().Delete(ojbP);
                    if (result > 0)
                    {
                        e.Item.Visible = false;
                        btnNew_ServerClick(null, null);
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Tidak bisa dihapus");
                        return;
                    }
                    break;
            }
        }
        protected void dtlProject_ItemCommand(object sender, RepeaterCommandEventArgs e)
        { }
        //protected void dtlProject_ItemCommand(object sender, RepeaterCommandEventArgs e)
        //{
        //    switch (e.CommandName)
        //    {
        //        case "edit":
        //            MTC_Project objP = new MTC_ProjectFacade().RetrieveByID(int.Parse(e.CommandArgument.ToString()));
        //            txtTglProject.Text = objP.ProjectDate.ToString("dd-MMM-yyyy");
        //            ddlProjectGroup.SelectedValue = objP.GroupID.ToString();
        //            txtProjectName.Text = objP.NamaProject;
        //            txtProjectName.Enabled = false;
        //            txtSubProjectName.Text = objP.SubProjectName.ToString();
        //            txtSubProjectName.Enabled = true;
        //            ddlProdLine.SelectedValue = objP.ProdLine.ToString();
        //            //ddlDeptName.SelectedValue = (ddlDeptName.SelectedValue!=objP.DeptID.ToString())?"0":objP.DeptID.ToString();
        //            //txtTglFinish.Text = objP.FinishDate.ToString("dd-MMM-yyyy");
        //            txtEstimasiBiaya.Text = objP.Biaya.ToString("###,##0.00");
        //            ddlStatus.SelectedValue = objP.Progress.ToString();
        //            txtProjectID.Text = objP.ID.ToString();
        //            txtNoImprovement.Text = objP.Nomor.ToUpper();
        //            break;
        //        case "del":
        //            MTC_Project ojbP = new MTC_Project();
        //            ojbP.ID = int.Parse(e.CommandArgument.ToString());
        //            ojbP.LastModifiedBy = ((Users)Session["Users"]).UserName;
        //            int result = new MTC_ProjectFacade().Delete(ojbP);
        //            if (result > 0)
        //            {
        //                LoadGridProject();
        //            }
        //            else
        //            {
        //                DisplayAJAXMessage(this, "Tidak bisa dihapus");
        //                return;
        //            }
        //            break;
        //    }
        //}
        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ArrayList arrP = new ArrayList();
            Session["Cancel"] = "no";
            Session["SubPj"] = "";
            Session["StProject"] = " and Status >-1 ";
            Session["Search"] = " and " + ddlSearch.SelectedValue + " like '%" + txtSearch.Text + "%' ";
            arrP = new MTC_ProjectFacade().Retrieve();
            lstProject.DataSource = arrP;
            lstProject.DataBind();
        }
        private void LoadUnit()
        {
            ArrayList arrUom = new UOMFacade().Retrieve();
            ddlUnit.Items.Clear();
            ddlUnit.Items.Add(new ListItem(" ", "0"));
            foreach (UOM unit in arrUom)
            {
                ddlUnit.Items.Add(new ListItem(unit.UOMCode, unit.ID.ToString()));
            }
            ddlUnit.SelectedValue = "35";
        }
        private void LoadSasaran()
        {
            string[] sasaran = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Sasaran", "EngineeringNew").Split(',');
            if (sasaran.Count() > 0)
            {
                ddlSasaran.Items.Clear();
                for (int i = 0; i < sasaran.Count(); i++)
                {
                    ddlSasaran.Items.Add(new ListItem(sasaran[i].ToString(), sasaran[i].ToString()));
                }
            }
        }
        protected void txtEstimasiBiaya_Change(object sender, EventArgs e)
        {
            //string EstimasiLevel = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EstimasiLevel", "Engineering");
            //if (decimal.Parse(txtEstimasiBiaya.Text) > decimal.Parse(EstimasiLevel.ToString()))
            //{
            //    ddlApproval.SelectedValue = "2";
            //}
            //else
            //{
            //    ddlApproval.SelectedValue = "1";
            //}
        }
    }
}