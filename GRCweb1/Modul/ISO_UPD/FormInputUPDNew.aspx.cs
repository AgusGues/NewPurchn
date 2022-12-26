using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Drawing;
using System.IO;
using System.Data;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormInputUPDNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                Global.link = "~/Default.aspx";
                Session["UpdId"] = null;
                Users users = (Users)Session["Users"];

                PanelAwalMenu.Visible = true;
                PanelKD.Visible = true;
                PanelKD.Enabled = false;

                LoadJDoc();

                btnSave.Enabled = false;
                btnsave2.Enabled = false;

                string DeptID = string.Empty;

                if (users.DeptID == 31 || users.DeptID == 30)
                {
                    PilihanDept.Visible = true; LoadSubDept();
                }
                else
                {
                    PilihanDept.Visible = false;
                }

                if (users.DeptID == 19)
                {
                    DeptID = ("4");
                }
                else
                {
                    DeptID = users.DeptID.ToString();
                    AutoCompleteExtender5.ContextKey = DeptID;
                    AutoCompleteExtender4.ContextKey = DeptID;
                }

                LoadDataGridUPD(LoadDataUPD());
            }
        }
        private void LoadSubDept()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrSub = new ArrayList();
            ISO_UpdFacade UPDFacadeSub = new ISO_UpdFacade();
            arrSub = UPDFacadeSub.Retrieve_SubDept(users.DeptID);
            ddlLabelDept.Items.Clear();
            ddlLabelDept.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_Upd masterD in arrSub)
            {
                ddlLabelDept.Items.Add(new ListItem(masterD.SubDept, masterD.SubDept));
            }


        }
        private ArrayList LoadDataUPD()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade UPDFacade = new ISO_UpdFacade();
            arrUPD = UPDFacade.Retrieve_ListUPD(users.DeptID);
            arrUPD.Add(new ISO_Upd());
            return arrUPD;
        }

        private void LoadDataGridUPD(ArrayList arrUPD)
        {
            this.GridView1.DataSource = arrUPD;
            this.GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Users users = (Users)Session["Users"];
            GridView1.PageIndex = e.NewPageIndex;
            LoadDataGridUPD(LoadDataUPD());
        }

        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Users users = (Users)Session["Users"];
            GridView3.PageIndex = e.NewPageIndex;
            LoadDataGridUPDKhusus(LoadDataUPDKhusus());
        }
        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView3.Rows[index];
                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtPDK1.Text = CekString(row.Cells[2].Text);

                txtPDK3.Focus();
            }
        }

        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Users users = (Users)Session["Users"];
            GridView4.PageIndex = e.NewPageIndex;
            LoadDataGridUPDBiasa(LoadDataUPDBiasa());
        }
        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView4.Rows[index];
                ViewState["id"] = int.Parse(row.Cells[0].Text);

                txtPDB1.Text = CekString(row.Cells[1].Text);
                txtPDB3.Text = CekString(row.Cells[2].Text);

                txtPDB2.Focus();
            }
        }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        { }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        { }

        private void LoadJDoc()
        {
            ArrayList arrTypeD = new ArrayList();
            ISO_UpdTypeDocFacade typeDFacade = new ISO_UpdTypeDocFacade();
            Users users = (Users)Session["Users"];
            if (users.DeptID == 22)
            {
                arrTypeD = typeDFacade.RetrieveProject();
                ddlJDoc.Items.Clear();
                ddlJDoc.Items.Add(new ListItem("-- Pilih Jenis Dokumen --", "0"));
                foreach (ISO_UpdTypeDoc typeD in arrTypeD)
                {
                    ddlJDoc.Items.Add(new ListItem(typeD.DocTypeName, typeD.ID.ToString()));
                }
            }
            else
            {
                arrTypeD = typeDFacade.Retrieve();
                ddlJDoc.Items.Clear();
                ddlJDoc.Items.Add(new ListItem("-- Pilih Jenis Dokumen --", "0"));
                foreach (ISO_UpdTypeDoc typeD in arrTypeD)
                {
                    ddlJDoc.Items.Add(new ListItem(typeD.DocTypeName, typeD.ID.ToString()));
                }
            }
        }

        private void LoadUPD(string UpdNo)
        {
            ISO_UpdFacade updFacade = new ISO_UpdFacade();
            int cekStatusDetail = 0;
            DateTime tglSelesai = DateTime.MaxValue;

            ArrayList arrUPD = new ArrayList();
            arrUPD = updFacade.RetrieveByNo(UpdNo);

            if (arrUPD.Count > 0)
            {
                foreach (ISO_Upd upd in arrUPD)
                {
                    btnSave.Enabled = false;
                    Session["id"] = upd.ID;
                    ViewState["UpdNo"] = upd.updNo;
                    txtUpdNo.Text = upd.updNo;
                }
            }

            if (cekStatusDetail > 0)
            {
            }

            else

                this.GridView1.DataSource = arrUPD;
            this.GridView1.DataBind();
            Session["ListOfTask"] = arrUPD;
        }

        private void clearForm()
        {
            Session["id"] = null;
            ddlJDoc.SelectedIndex = 0;
            txtUpdNo.Text = string.Empty;
            btnSave.Enabled = true;
            ArrayList arrList = new ArrayList();
            arrList.Add(new ISO_Upd());

            GridView1.DataSource = arrList;
            GridView1.DataBind();
        }

        private void LoadDCat1()
        {

            ArrayList arrMasterD = new ArrayList();
            ISO_UpdMasterDocFacade masterDFacade = new ISO_UpdMasterDocFacade();
            arrMasterD = masterDFacade.Retrieve1();
            ddlKD1.Items.Clear();
            ddlKD1.Items.Add(new ListItem("---- Pilih ----", "0"));
            foreach (ISO_UpdMasterDoc masterD in arrMasterD)
            {
                ddlKD1.Items.Add(new ListItem(masterD.DocCategory, masterD.ID.ToString()));
            }


        }

        private void LoadSection()
        {
            DeptFacade deptFacade = new DeptFacade();
            ArrayList arrDept = deptFacade.RetrieveByUserID(((Users)Session["Users"]).ID);
        }

        private void LoadCategory(int pestype)
        {
            CategoryFacade catFacade = new CategoryFacade();
            ArrayList arrCategory = catFacade.RetrieveByPesType(pestype);
        }

        private void LoadDeptCode(string deptCode)
        {
            Users users = (Users)Session["Users"];
            int depoid = users.UnitKerjaID;

            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();

            int noid = Convert.ToInt32(ddlJDoc.SelectedValue);

            ISO_Dept isoDept = new ISO_Dept();
            ISO_DeptFacade isoDeptFacade = new ISO_DeptFacade();

            ISO_Upd uPD = new ISO_Upd();
            ISO_UpdFacade uPDFacade = new ISO_UpdFacade();
            uPDFacade = new ISO_UpdFacade();
        }

        //protected void btnSearch_ServerClick(object sender, EventArgs e)
        //{
        //    ISO_UpdFacade updFacade = new ISO_UpdFacade();

        //    ArrayList arrUPD = new ArrayList();
        //    arrUPD = updFacade.RetrieveByNo(txtSearch.Text);
        //    if (arrUPD.Count > 0)
        //    {
        //        foreach (ISO_Upd upd in arrUPD)
        //        {
        //            Session["id"] = upd.ID;
        //            btnSave.Enabled = false;

        //            ViewState["NoUPD"] = upd.updNo;

        //            txtUpdNo.Text = upd.updNo;               
        //            int cekStatusDetail = 0;               

        //            LoadSection();                
        //        }
        //    }

        //    this.GridView1.DataSource = arrUPD;
        //    this.GridView1.DataBind();
        //}

        private void SelectSection(string strDepo)
        {
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[5].Text == "1")
                {
                    e.Row.Cells[5].Text = "Pedoman Mutu";
                }
                if (e.Row.Cells[5].Text == "2")
                {
                    e.Row.Cells[5].Text = "Instruksi Kerja";
                }
                if (e.Row.Cells[5].Text == "3")
                {
                    e.Row.Cells[5].Text = "Form";
                }
                if (e.Row.Cells[5].Text == "4")
                {
                    e.Row.Cells[5].Text = "Prosedur";
                }
                if (e.Row.Cells[5].Text == "5")
                {
                    e.Row.Cells[5].Text = "Standar";
                }
                if (e.Row.Cells[5].Text == "6")
                {
                    e.Row.Cells[5].Text = "Bagan Alir";
                }
                if (e.Row.Cells[5].Text == "7")
                {
                    e.Row.Cells[5].Text = "Struktur Org";
                }
                if (e.Row.Cells[5].Text == "8")
                {
                    e.Row.Cells[5].Text = "JobDesc";
                }

                if (e.Row.Cells[4].Text == "2")
                {
                    e.Row.Cells[4].Text = "Boardmill";
                }
                if (e.Row.Cells[4].Text == "3")
                {
                    e.Row.Cells[4].Text = "Finishing";
                }
                if (e.Row.Cells[4].Text == "6")
                {
                    e.Row.Cells[4].Text = "Log. Produk Jadi";
                }
                if (e.Row.Cells[4].Text == "7")
                {
                    e.Row.Cells[4].Text = "HRD";
                }
                if (e.Row.Cells[4].Text == "9")
                {
                    e.Row.Cells[4].Text = "QA";
                }
                if (e.Row.Cells[4].Text == "10")
                {
                    e.Row.Cells[4].Text = "Log. Bahan Baku";
                }
                if (e.Row.Cells[4].Text == "11")
                {
                    e.Row.Cells[4].Text = "PPIC";
                }
                if (e.Row.Cells[4].Text == "14")
                {
                    e.Row.Cells[4].Text = "EDP(ELECTRONONIC DATA PROCESSING)";
                }
                if (e.Row.Cells[4].Text == "15")
                {
                    e.Row.Cells[4].Text = "Purchasing";
                }
                if (e.Row.Cells[4].Text == "23")
                {
                    e.Row.Cells[4].Text = "ISO";
                }
                if (e.Row.Cells[4].Text == "26")
                {
                    e.Row.Cells[4].Text = "Delivery";
                }
                if (e.Row.Cells[4].Text == "13")
                {
                    e.Row.Cells[4].Text = "Marketing";
                }
            }
        }

        private void SelectCategory(string strDepo)
        {
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);
            string strDeptID = dept.DeptID.ToString();
            Response.Redirect("ListTask.aspx?TaskDept=" + strDeptID + "&TipeTask=UnSolved" + "&FormTask=1");
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            if (Session["ArrImgLampiran"] != null)
            {
                int z = 0;
                ArrayList arrImgLampiran = new ArrayList();
                arrImgLampiran = (ArrayList)Session["ArrImgLampiran"];
                while (z != arrImgLampiran.Count)
                {
                    File.Delete(this.Server.MapPath("~\\Resource_Web\\Lampiran_iso\\" + arrImgLampiran[z]));
                    z++;
                }

                Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
            }

            if (Session["id"] != null)
            {
                ISO_Upd upd = new ISO_Upd();
                upd.ID = (int)Session["id"];
                ISO_UpdProcessFacade updProcessFacade = new ISO_UpdProcessFacade(upd, new ISO_UpdDocNo(), new ArrayList());
                string strError = updProcessFacade.CancelTask();
                if (strError == string.Empty)
                    clearForm();
                else
                {
                    DisplayAJAXMessage(this, "Task tidak dapat di cancel");
                }

            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
            PanelKD.Visible = true;
            PanelKD.Enabled = false;
            btnSave.Enabled = false;
            btnsave2.Enabled = false;
            PanelPDB.Visible = false;
            ddlKD1.Items.Clear();
            PanelPilihan.Visible = false;
            txtKD2.Text = string.Empty;
            txtKD3.Text = string.Empty;
            RB11.Checked = false;
            RB22.Checked = false;

            if (Session["ArrImgLampiran"] != null)
            {
                int z = 0;
                ArrayList arrImgLampiran = new ArrayList();
                arrImgLampiran = (ArrayList)Session["ArrImgLampiran"];
                while (z != arrImgLampiran.Count)
                {
                    File.Delete(this.Server.MapPath("~\\Resource_Web\\Lampiran_iso\\" + arrImgLampiran[z]));
                    z++;
                }

                Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
            }
            LoadDataGridUPD(LoadDataUPD());
        }

        protected void btnSave_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrImgLampiran = new ArrayList();
            Users users = (Users)Session["Users"];

            if (users.DeptID == 30 || users.DeptID == 31)
            {
                if (ddlLabelDept.SelectedValue == "0")
                {
                    DisplayAJAXMessage(this, "Pilih Sub Department harus di pilih !! "); return;
                }
            }

            if (ddlJDoc.SelectedIndex == 0)
            { DisplayAJAXMessage(this, "Pilih Doc Type "); return; }

            if (ddlKD1.SelectedIndex == 0)
            { DisplayAJAXMessage(this, " Kategori dokumen belum di pilih "); return; }

            if (txtKD2.Text == string.Empty)
            { DisplayAJAXMessage(this, " Nama Dokumen harus diisi "); return; }

            if (txtKD3.Text == string.Empty)
            { DisplayAJAXMessage(this, " Alasan harus diisi "); return; }

            ISO_Upd upd = new ISO_Upd();
            ISO_UPDdetail updDetail = new ISO_UPDdetail();
            //Users users = (Users)Session["Users"];
            ISO_UpdFacade updFacade = new ISO_UpdFacade();
            if (ViewState["id"] != null)
            {
                upd.ID = int.Parse(ViewState["id"].ToString());
            }

            int JenisDoc = int.Parse(ddlJDoc.SelectedValue); // Jenis Dokumen : 1. Baru 2. Rubah 3. Musnah
            string CategoryUPD = ddlKD1.SelectedValue; // Kategori Dokumen : 1.PM 2.IK 3.Form 6.Standard
            ISO_Upd upd2 = new ISO_Upd();
            ISO_UpdFacade updFacade2 = new ISO_UpdFacade();
            string Tipe = updFacade2.CekTipe(CategoryUPD);

            upd.TypE = Tipe;
            upd.JenisDoc = JenisDoc;
            upd.Categoryupd = CategoryUPD;
            upd.JenisDoc = int.Parse(ddlJDoc.SelectedValue);
            upd.UpdName = txtKD2.Text;

            if (users.DeptID == 5 || users.DeptID == 18 || users.DeptID == 19)
            {
                upd.DeptID = 4;
            }
            else
            {
                upd.DeptID = users.DeptID;
            }
            //upd.DeptID = users.DeptID;
            if (users.DeptID == 30 || users.DeptID == 31)
            {
                string NamaSub = ddlLabelDept.SelectedItem.ToString().Trim();
                //ISO_Upd updHead = new ISO_Upd();
                ISO_UpdFacade updFacadeHead = new ISO_UpdFacade();
                int HeadID = updFacadeHead.RetrieveHeadID(NamaSub);
                upd.HeadID = HeadID;
            }
            else
            {
                upd.HeadID = 0;
            }

            upd.Pic = users.UserName;
            upd.TglPengajuan = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            upd.Alasan = txtKD3.Text;
            upd.CreatedBy = ((Users)Session["Users"]).UserName;
            upd.LastModifiedBy = ((Users)Session["Users"]).UserName;
            upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;

            ArrayList arrUpdDetail = new ArrayList();

            if (int.Parse(ddlJDoc.SelectedValue) == 4)
            {
                if (Session["ListOfUpdDetail"] != null)
                {
                    arrUpdDetail = (ArrayList)Session["ListOfUpdDetail"];
                }
            }

            else
            {
                if (int.Parse(ddlJDoc.SelectedValue) == 1)
                {
                    upd.Type = int.Parse(Tipe);
                    upd.Alasan = txtKD3.Text;
                    upd.DokumenBaru = string.Empty;
                    upd.DokumenLama = string.Empty;
                    upd.NoDokumen = string.Empty;
                    upd.NamaDokumen = txtKD2.Text;
                    upd.NoRevisi = string.Empty;
                    upd.NoDokumen = txtUpdNo.Text;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    arrUpdDetail.Add(upd);
                }

                else if (int.Parse(ddlJDoc.SelectedValue) == 2)
                {
                    upd.Alasan = txtPDB2.Text;
                    upd.IsiDokumen = string.Empty;
                    upd.NoDokumen = string.Empty;
                    upd.NamaDokumen = string.Empty;
                    upd.NoRevisi = string.Empty;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    arrUpdDetail.Add(upd);
                }

                else if (int.Parse(ddlJDoc.SelectedValue) == 3)
                {
                    //upd.Alasan = txtKet1.Text;
                    upd.DokumenBaru = string.Empty;
                    upd.DokumenLama = string.Empty;
                    upd.NoDokumen = string.Empty;
                    upd.NamaDokumen = string.Empty;
                    upd.NoRevisi = string.Empty;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    arrUpdDetail.Add(upd);
                }
                else if (int.Parse(ddlJDoc.SelectedValue) == 6)
                {
                    //upd.Alasan = txtKet1.Text;
                    upd.DokumenBaru = string.Empty;
                    upd.DokumenLama = string.Empty;
                    upd.NoDokumen = string.Empty;
                    upd.NamaDokumen = string.Empty;
                    upd.NoRevisi = string.Empty;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    arrUpdDetail.Add(upd);
                }
            }

            int intdocType = int.Parse(ddlJDoc.SelectedValue);

            ISO_UpdDocNoFacade docNoFacade = new ISO_UpdDocNoFacade();
            ISO_UpdDocNo docNo = new ISO_UpdDocNo();
            ISO_UpdDocNo docno = docNoFacade.RetrieveByDocTypeID(intdocType);

            if (docNoFacade.Error == string.Empty)
            {
                if (docno.ID > 0)
                {
                    docno.DocNo = docno.DocNo + 1;
                    if (docno.DocType == 1)
                        docno.DocTypE = "UDB";
                    if (docno.DocType == 2)
                        docno.DocTypE = "UPD";
                    if (docno.DocType == 3)
                        docno.DocTypE = "UHD";

                    docno.DeptID = users.DeptID;
                    docno.Tahun = DateTime.Now.Year;

                    if (((Users)Session["Users"]).UnitKerjaID == 1)
                        docno.Plant = "C";
                    if (((Users)Session["Users"]).UnitKerjaID == 7)
                        docno.Plant = "K";

                    ISO_Upd docNo2 = new ISO_Upd();
                    ISO_UpdFacade docNoFacade2 = new ISO_UpdFacade();
                    string DeptCode = docNoFacade2.CekCDeptCode(((Users)Session["Users"]).DeptID.ToString());

                    docno.DeptCode = DeptCode;
                }

                else
                {
                    docno.DocNo = 1;
                    if (docno.DocType == 4)
                        docno.DeptCode = docno.DeptCode;

                    //HO ikut C dulu
                    if (((Users)Session["Users"]).UnitKerjaID == 1)
                        docno.Plant = "C";

                    if (((Users)Session["Users"]).UnitKerjaID == 7)
                        docno.Plant = "K";
                }
            }

            ISO_UpdProcessFacade updProcessFacade = new ISO_UpdProcessFacade(upd, docno, arrUpdDetail);
            updProcessFacade.arrImgProcessFacade((ArrayList)Session["ArrImgLampiran"]);
            string strError = string.Empty;
            strError = updProcessFacade.Insert();

            int intID = 0;
            intID = upd.updID;
            Session["UPDide"] = upd.updID;

            if (strError == string.Empty)
            {
                LoadDataGridUPD(LoadDataUPD());

                Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
                txtUpdNo.Text = updProcessFacade.UpdNo;
                upd.NoDokumen = txtUpdNo.Text;
                btnSave.Enabled = false;

                //if (intID > 0)
                //{              
                //    PanelUpload.Visible = true;
                //    PanelAwalMenu.Visible = false;
                //    PanelKD.Visible = false;
                //    PanelPilihan.Visible = false;
                //}  
            }
        }

        protected void btnSave2_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrImgLampiran = new ArrayList();

            if (ddlJDoc.SelectedIndex == 0)
            { DisplayAJAXMessage(this, "Pilih Doc Type "); return; }

            if (ddlJDoc.SelectedValue == "2" && RB11.Checked == true)
            {
                if (txtPDB2.Text == string.Empty)
                { DisplayAJAXMessage(this, " Alasan harus diisi "); return; }
            }
            else if (ddlJDoc.SelectedValue == "2" && RB22.Checked == true)
            {
                if (txtPDK3.Text == string.Empty)
                { DisplayAJAXMessage(this, " Alasan harus diisi "); return; }
            }

            ISO_Upd upd = new ISO_Upd();
            ISO_UPDdetail updDetail = new ISO_UPDdetail();
            ISO_UpdDMD updM = new ISO_UpdDMD();
            Users users = (Users)Session["Users"];
            ISO_UpdFacade updFacade = new ISO_UpdFacade();

            if (ViewState["id"] != null)
            {
                upd.ID = int.Parse(ViewState["id"].ToString());
            }

            upd.JenisDoc = int.Parse(ddlJDoc.SelectedValue);

            if (ddlJDoc.SelectedValue == "2" && RB11.Checked == true || ddlJDoc.SelectedValue == "3" && RB11.Checked == true)
            {
                ISO_Upd upd2 = new ISO_Upd();
                ISO_UpdFacade updFacade2 = new ISO_UpdFacade();
                upd2 = updFacade2.RetrieveUPDBiasa(txtPDB1.Text.Trim());

                Session["IDmaster"] = upd2.IDmaster;
                Session["Tipe"] = upd2.Type;
                Session["Alasan"] = txtPDB2.Text;
                Session["CategoryUPD"] = upd2.CategoryUPd.ToString();
                Session["NoDokumen"] = upd2.NoDokumen;
                Session["RevisiNo"] = upd2.RevisiNo;
            }
            else if (ddlJDoc.SelectedValue == "2" && RB22.Checked == true || ddlJDoc.SelectedValue == "3" && RB22.Checked == true)
            {
                ISO_Upd upd3 = new ISO_Upd();
                ISO_UpdFacade updFacade3 = new ISO_UpdFacade();
                upd3 = updFacade3.RetrieveUPDKhusus(txtPDK1.Text.Trim());
                Session["IDmaster"] = upd3.IDmaster;
                Session["Tipe"] = upd3.Type;
                Session["Alasan"] = txtPDK3.Text;
                Session["CategoryUPD"] = upd3.CategoryUPd.ToString();
                Session["NoDokumen"] = upd3.NoDokumen;
                Session["RevisiNo"] = upd3.RevisiNo;
            }

            upd.CategoryUPd = Session["CategoryUPD"].ToString();
            upd.NoDokumen = Session["NoDokumen"].ToString();
            upd.UpdName = txtPDB3.Text;
            upd.DeptID = users.DeptID;
            upd.Pic = users.UserName;
            upd.TglPengajuan = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            upd.UpdImage = (Session["linkFile"] != null) ? Session["linkFile"].ToString() : "";
            upd.CreatedBy = ((Users)Session["Users"]).UserName;
            upd.LastModifiedBy = ((Users)Session["Users"]).UserName;
            upd.NoDokumen = txtPDB1.Text;
            upd.IDmaster = Session["IDmaster"].ToString();
            upd.DeptA = ((Users)Session["Users"]).DeptID.ToString();
            upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;
            upd.type = Convert.ToInt32(Session["Tipe"]);
            upd.RevisiNo = Session["RevisiNo"].ToString();

            ArrayList arrUpdDetail = new ArrayList();

            if (int.Parse(ddlJDoc.SelectedValue) == 4)
            {
                if (Session["ListOfUpdDetail"] != null)
                {
                    arrUpdDetail = (ArrayList)Session["ListOfUpdDetail"];
                }
            }

            else
            {
                if (int.Parse(ddlJDoc.SelectedValue) == 2 && RB11.Checked == true || int.Parse(ddlJDoc.SelectedValue) == 3 && RB11.Checked == true)
                {
                    upd.Alasan = txtPDB2.Text;
                    upd.IsiDokumen = string.Empty;
                    upd.NoDokumen = string.Empty;
                    upd.NamaDokumen = txtPDB3.Text;
                    upd.NoRevisi = string.Empty;
                    upd.CategoryUPd = Session["CategoryUPD"].ToString();
                    upd.NoDokumen = txtPDB1.Text;
                    upd.IDmaster = Session["IDmaster"].ToString();
                    upd.UpdName = txtPDB3.Text;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;
                    arrUpdDetail.Add(upd);
                }
                else if (int.Parse(ddlJDoc.SelectedValue) == 2 && RB22.Checked == true || int.Parse(ddlJDoc.SelectedValue) == 3 && RB22.Checked == true)
                {
                    upd.Alasan = txtPDK3.Text;
                    upd.IsiDokumen = string.Empty;
                    upd.NoDokumen = string.Empty;
                    upd.NamaDokumen = txtPDK1.Text;
                    upd.NoRevisi = string.Empty;
                    upd.CategoryUPd = Session["CategoryUPD"].ToString();
                    upd.NoDokumen = Session["NoDokumen"].ToString();
                    upd.IDmaster = Session["IDmaster"].ToString();
                    upd.UpdName = txtPDK1.Text;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;
                    arrUpdDetail.Add(upd);
                }

                //else if (int.Parse(ddlJDoc.SelectedValue) == 3)
                //{
                //    upd.Alasan = txtPDK3.Text;
                //    upd.IsiDokumen = string.Empty;
                //    upd.NoDokumen = string.Empty;
                //    upd.NamaDokumen = txtPDK1.Text;
                //    upd.NoRevisi = string.Empty;
                //    upd.CategoryUPd = Session["CategoryUPD"].ToString();
                //    upd.NoDokumen = Session["NoDokumen"].ToString();
                //    upd.IDmaster = Session["IDmaster"].ToString();
                //    upd.UpdName = txtPDK1.Text;
                //    upd.UserID = ((Users)Session["Users"]).ID;
                //    upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;
                //    arrUpdDetail.Add(upd);
                //}

                else if (int.Parse(ddlJDoc.SelectedValue) == 5)
                {
                    //upd.Alasan = txtKet1.Text;
                    upd.IsiDokumen = string.Empty;
                    upd.NoDokumen = string.Empty;
                    //upd.NamaDokumen = txtK.Text;
                    //upd.UpdName = txtK.Text;
                    upd.NoRevisi = string.Empty;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    //upd.CategoryUPd = IDK1.Text;
                    //upd.NoDokumen = IDK.Text;
                    upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;
                    //upd.IDmaster = ID22.Text;
                    //upd.DeptA = IDdept.Text;

                    arrUpdDetail.Add(upd);
                }

                else if (int.Parse(ddlJDoc.SelectedValue) == 6)
                {
                    //upd.Alasan = txtKet1.Text;
                    upd.IsiDokumen = string.Empty;
                    upd.NoDokumen = string.Empty;
                    //upd.NamaDokumen = txtK.Text;
                    upd.NoRevisi = string.Empty;
                    //upd.CategoryUPd = IDK1.Text;
                    //upd.NoDokumen = IDK.Text;
                    //upd.IDmaster = ID22.Text;
                    //upd.DeptA = IDdept.Text;
                    upd.UserID = ((Users)Session["Users"]).ID;
                    //upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;
                    //upd.UpdName = txtK.Text;               

                    arrUpdDetail.Add(upd);
                }

                else if (int.Parse(ddlJDoc.SelectedValue) == 7)
                {
                    //upd.Alasan = txtKet1.Text;
                    //upd.IsiDokumen = string.Empty;
                    //upd.NoDokumen = string.Empty;                
                    //upd.NamaDokumen = txtOP.Text;
                    //upd.NoRevisi = string.Empty;
                    //upd.CategoryUPd = IDK1.Text;
                    //upd.NoDokumen = IDK.Text;
                    //upd.IDmaster = ID22.Text;
                    //upd.DeptA = IDdept.Text;
                    //upd.UserID = ((Users)Session["Users"]).ID;
                    //upd.PlanID = ((Users)Session["Users"]).UnitKerjaID;
                    //upd.UpdName = txtOP.Text;

                    arrUpdDetail.Add(upd);
                }

            }

            int intdocType = int.Parse(ddlJDoc.SelectedValue);

            ISO_UpdDocNoFacade docNoFacade = new ISO_UpdDocNoFacade();
            ISO_UpdDocNo docNo = new ISO_UpdDocNo();
            ISO_UpdDocNo docno = docNoFacade.RetrieveByDocTypeID(intdocType);

            if (docNoFacade.Error == string.Empty)
            {
                if (docno.ID > 0)
                {
                    docno.DocNo = docno.DocNo + 1;
                    if (docno.DocType == 1)
                        docno.DocTypE = "UDB";
                    if (docno.DocType == 2)
                        docno.DocTypE = "UPD";
                    if (docno.DocType == 3)
                        docno.DocTypE = "UHD";
                    if (docno.DocType == 5)
                        docno.DocTypE = "UPD";
                    if (docno.DocType == 6)
                        docno.DocTypE = "UHD";
                    if (docno.DocType == 7)
                        docno.DocTypE = "UPD";

                    docno.DeptID = users.DeptID;
                    docno.Tahun = DateTime.Now.Year;
                    if (((Users)Session["Users"]).UnitKerjaID == 1)
                        docno.Plant = "C";
                    if (((Users)Session["Users"]).UnitKerjaID == 7)
                        docno.Plant = "K";

                    ISO_Upd docNo2 = new ISO_Upd();
                    ISO_UpdFacade docNoFacade2 = new ISO_UpdFacade();
                    string DeptCode = docNoFacade2.CekCDeptCode(((Users)Session["Users"]).DeptID.ToString());

                    docno.DeptCode = DeptCode;

                }

                else
                {
                    docno.DocNo = 1;
                    docno.Tahun = DateTime.Now.Year;
                    docno.DeptCode = docno.DeptCode;

                    //HO ikut C dulu
                    if (((Users)Session["Users"]).UnitKerjaID == 1)
                        docno.Plant = "C";

                    if (((Users)Session["Users"]).UnitKerjaID == 7)
                        docno.Plant = "K";
                }
            }

            ISO_UpdProcessFacade updProcessFacade = new ISO_UpdProcessFacade(upd, docno, arrUpdDetail);
            updProcessFacade.arrImgProcessFacade((ArrayList)Session["ArrImgLampiran"]);

            string strError = string.Empty;
            strError = updProcessFacade.Insert();

            if (strError == string.Empty)
            {
                Session["TempIdLampiran"] = null;
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;

                int intID = 0;
                txtUpdNo.Text = updProcessFacade.UpdNo;
                upd.NoDokumen = txtUpdNo.Text;
                intID = upd.updID;
                Session["UPDide"] = upd.updID;

                btnSave.Enabled = false;
                btnsave2.Enabled = false;

                LoadDataGridUPD(LoadDataUPD());
                LoadDataGridUPDBiasa(LoadDataUPDBiasa());
                LoadDataGridUPDKhusus(LoadDataUPDKhusus());
                //if (intID > 0)
                //{

                //    PanelUpload.Visible = true;
                //    PanelAwalMenu.Visible = false;
                //    PanelPDK.Visible = false;
                //    PanelPilihan.Visible = false;
                //    PanelPDB.Visible = false;

                //    LoadDataGridUPD(LoadDataUPD());
                //}            

            }
        }

        protected void btnListSolved_ServerClick(object sender, EventArgs e)
        {
            DeptFacade deptFacade = new DeptFacade();
            Dept dept = deptFacade.RetrieveDeptByUserID(((Users)Session["Users"]).ID);

            string strDeptID = dept.DeptID.ToString();
            Response.Redirect("ListTask.aspx?TaskDept=" + strDeptID + "&TipeTask=Solved" + "&FormTask=1");
        }

        protected void btnLampiran_ServerClick(object sender, EventArgs e)
        {
            if (Session["TempIdLampiran"] == null || Session["ArrImgLampiran"] == null)
            {
                Session["TempIdLampiran"] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
            }
        }

        protected void ButtonP_ServerClick(object sender, EventArgs e)
        {
            if (Session["TempIdLampiran"] == null || Session["ArrImgLampiran"] == null)
            {
                Session["TempIdLampiran"] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                Session["ArrImgLampiran"] = null;
                Session["i"] = null;
            }
        }

        protected void btnL_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("ListUPD.aspx");

        }

        private void LoadDataGrid(ArrayList arrUPD)
        {
            this.GridView1.DataSource = arrUPD;
            this.GridView1.DataBind();
        }

        private ArrayList ListUPD(string deptID)
        {
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            arrUPD = updF.RetrieveStatus0(deptID);
            if (arrUPD.Count > 0)
            {
                return arrUPD;
            }

            arrUPD.Add(new ISO_Upd());
            return arrUPD;
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Input UPD";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtUpdNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;
            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            return string.Empty;
        }

        protected void ddlBagian_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCategory(2);
        }



        protected void UPDTextChange(object sender, EventArgs e)
        {
            if (txtPDK1.Text != string.Empty)
            {
                LoadMasterNama(txtPDK1.Text);
            }
        }

        //protected void txtPDB2Change(object sender, EventArgs e)
        //{
        //    btnsave2.Enabled = true;
        //    btnsave2.Focus();
        //}

        //protected void txtKD3Change(object sender, EventArgs e)
        //{
        //    btnSave.Enabled = true;
        //    btnSave.Focus();
        //}

        //protected void txtPDK3Change(object sender, EventArgs e)
        //{
        //    btnsave2.Enabled = true;
        //    btnsave2.Focus();
        //}

        protected void txtPDB1Change(object sender, EventArgs e)
        {
            if (txtPDB1.Text != string.Empty)
            {
                LoadMasterNamaWithNomor(txtPDB1.Text);

                //txtPDB1.Focus();            
            }
            txtPDB2.Focus();
        }

        private void LoadMaster(string strUPDM)
        {
            ISO_UpdDMD UPDm = new ISO_UpdDMD();
            ISO_DMDFacade UPDfacade = new ISO_DMDFacade();
            UPDm = UPDfacade.RetrieveByUPDno(strUPDM);
        }

        private void LoadMasterNama(string strNama)
        {
            ISO_UpdDMD UPDmn = new ISO_UpdDMD();
            ISO_DMDFacade UPDfacadef = new ISO_DMDFacade();
            UPDmn = UPDfacadef.RetrieveByNama(strNama);
        }

        private void LoadMasterNamaWithNomor(string strNomor)
        {
            ISO_UpdDMD UPDNnomor = new ISO_UpdDMD();
            ISO_DMDFacade UPDFnomor = new ISO_DMDFacade();
            UPDNnomor = UPDFnomor.RetrieveByNamaWithNomor(strNomor);
            txtPDB3.Text = UPDNnomor.DocName;

            txtPDB2.Focus();

        }

        protected void ddlKD1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtKD2.Focus();
        }

        protected void ddlJDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISO_UpdDMD dmd = new ISO_UpdDMD();
            Users user = (Users)Session["Users"];
            if (ddlJDoc.SelectedIndex > 0)
            {
                if (ddlJDoc.SelectedIndex == 1)
                {
                    PanelPilihan.Visible = false;
                    PanelPDB.Visible = false;
                    PanelPDK.Visible = false;
                    PanelKD.Enabled = true;
                    PanelKD.Visible = true;
                    LoadDCat1();
                    txtKD2.Focus();

                    btnSave.Enabled = true;
                    btnSave.Focus();
                }
                else if (ddlJDoc.SelectedIndex == 2)
                {
                    PanelKD.Visible = false;
                    PanelPilihan.Visible = true;
                    PanelPDK.Visible = true;
                    PanelPDK.Enabled = false;

                    btnsave2.Enabled = true;
                    btnsave2.Focus();
                }
                else if (ddlJDoc.SelectedIndex == 3)
                {
                    PanelKD.Visible = false;
                    PanelPilihan.Visible = true;
                    PanelPDK.Visible = true;
                    PanelPDK.Enabled = false;

                    btnsave2.Enabled = true;
                    btnsave2.Focus();
                }
            }
        }

        protected void txtN_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txtKet1_TextChanged(object sender, EventArgs e)
        {
        }

        protected void RB11_CheckedChanged(object sender, EventArgs e)
        {
            PanelPDB.Visible = true;
            PanelPDB.Enabled = true;
            PanelPDK.Visible = false;
            RB22.Checked = false;

            txtPDB1.Focus();

            txtPDB1.Text = string.Empty;
            txtPDB3.Text = string.Empty;


            LoadDataGridUPDBiasa(LoadDataUPDBiasa());

        }

        protected void RB22_CheckedChanged(object sender, EventArgs e)
        {
            PanelPDK.Enabled = true;
            PanelPDK.Visible = true;
            PanelPDB.Visible = false;
            RB11.Checked = false;

            txtPDK1.Focus();

            txtPDK1.Text = string.Empty;
            txtPDK3.Text = string.Empty;

            LoadDataGridUPDKhusus(LoadDataUPDKhusus());
        }

        private ArrayList LoadDataUPDKhusus()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrUPD1 = new ArrayList();
            ISO_UpdFacade UPDFacade1 = new ISO_UpdFacade();
            arrUPD1 = UPDFacade1.RetrieveUPDKhusus(users.DeptID);
            arrUPD1.Add(new ISO_Upd());
            return arrUPD1;
        }

        private ArrayList LoadDataUPDBiasa()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrUPD2 = new ArrayList();
            ISO_UpdFacade UPDFacade2 = new ISO_UpdFacade();
            arrUPD2 = UPDFacade2.RetrieveUPDBiasa(users.DeptID);
            arrUPD2.Add(new ISO_Upd());
            return arrUPD2;
        }

        private void LoadDataGridUPDKhusus(ArrayList arrUPD1)
        {
            this.GridView3.DataSource = arrUPD1;
            this.GridView3.DataBind();
        }

        private void LoadDataGridUPDBiasa(ArrayList arrUPD2)
        {
            this.GridView4.DataSource = arrUPD2;
            this.GridView4.DataBind();
        }


        //protected void Button_Click(object sender, EventArgs e)
        //{   
        //    Users user = (Users)Session["Users"];
        //    int userDeptID = user.DeptID;
        //    string NamaDept = string.Empty;
        //    ISO_Upd upd1 = new ISO_Upd();
        //    ISO_DMDFacade updFacade = new ISO_DMDFacade();    

        //    if (userDeptID != 0)
        //    {
        //        if (userDeptID == 23) { NamaDept = "ISO"; }
        //        else if (userDeptID == 14) { NamaDept = "IT"; }
        //        else if (userDeptID == 7) { NamaDept = "HRD"; }
        //        else if (userDeptID == 15) { NamaDept = "Purchasing"; }
        //        else if (userDeptID == 26) { NamaDept = "Delivery"; }
        //        else if (userDeptID == 13) { NamaDept = "Marketing"; }
        //        else if (userDeptID == 11) { NamaDept = "PPIC"; }
        //        else if (userDeptID == 6) { NamaDept = "LogistikBJ"; }
        //        else if (userDeptID == 10) { NamaDept = "LogistikBB"; }
        //        else if (userDeptID == 3) { NamaDept = "Finishing"; }
        //        else if (userDeptID == 2) { NamaDept = "BM"; }
        //        else if (userDeptID == 19 || userDeptID == 4 || userDeptID == 5 || userDeptID == 18) { NamaDept = "Maintenance"; }
        //        else { NamaDept = "QA"; }
        //    }

        //    if (FileUpload1.HasFile)
        //    {
        //        string fileName = FileUpload1.FileName;
        //        FileUpload1.PostedFile.SaveAs(Server.MapPath("~/App_Data/UploadUPD/" + NamaDept + "/") + fileName);
        //        Session["linkFile"] = Server.MapPath("~/App_Data/UploadUPD/" + NamaDept + "/") + fileName.ToString();

        //        upd1.UPDid = Convert.ToInt32(Session["UPDide"]);
        //        upd1.NamaFile = fileName;
        //        upd1.CreatedBy = user.UserName;

        //        int intResult = 0;
        //        intResult = updFacade.InsertLampiran(upd1); 
        //    }

        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("File");
        //    dt.Columns.Add("Size");        

        //    foreach (string strfile in Directory.GetFiles(Server.MapPath("~/App_Data/UploadUPD/" + NamaDept + "")))
        //    {
        //        FileInfo fi = new FileInfo(strfile);
        //        dt.Rows.Add(fi.Name, fi.Length.ToString());                
        //    }

        //    GridView2.DataSource = dt;
        //    GridView2.DataBind();
        //}  

        protected void Button_Click(object sender, EventArgs e)
        {
            int JDoc = Convert.ToInt32(Session["JenisDoc"]);
            if (JDoc == 1)
            {
                btnsave2.Enabled = false;
                btnSave.Enabled = true;
                btnSave.Focus();
            }
            else if (JDoc == 2 || JDoc == 3)
            {
                btnSave.Enabled = false;
                btnsave2.Enabled = true;
                btnsave2.Focus();
            }
            else
            {
                btnSave.Enabled = false;
                btnsave2.Enabled = false;
            }


            Users user = (Users)Session["Users"];
            int userDeptID = user.DeptID;
            string NamaDept = string.Empty;
            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade updFacade = new ISO_DMDFacade();

            if (userDeptID != 0)
            {
                if (userDeptID == 23) { NamaDept = "ISO"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 14) { NamaDept = "IT"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 7) { NamaDept = "HRD"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 15) { NamaDept = "Purchasing"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 26) { NamaDept = "Delivery"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 13) { NamaDept = "Marketing"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 11) { NamaDept = "PPIC"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 6) { NamaDept = "LogistikBJ"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 10) { NamaDept = "LogistikBB"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 3) { NamaDept = "Finishing"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 2) { NamaDept = "BM"; Session["NamaDept"] = NamaDept; }
                else if (userDeptID == 19 || userDeptID == 4 || userDeptID == 5 || userDeptID == 18) { NamaDept = "Maintenance"; Session["NamaDept"] = NamaDept; }
                else { NamaDept = "QA"; Session["NamaDept"] = NamaDept; }
            }
            string NamaDept1 = Session["NamaDept"].ToString();

            if (FileUpload1.HasFile)
            {


                string fileName = FileUpload1.FileName;

                string ext = Path.GetExtension(fileName);
                if (ext.ToLower() != ".xls" && ext.ToLower() != ".xlsx" && ext.ToLower() != ".doc" && ext.ToLower() != ".docx")
                {
                    DisplayAJAXMessage(this, " Format yg di izin kan : xls,xlsx,doc,docx "); return;
                }

                string dirPath = Path.Combine(@"D:\", "DATA LAMPIRAN PURCHN");
                if (!System.IO.Directory.Exists(dirPath))
                {
                    System.IO.Directory.CreateDirectory(dirPath);
                }

                string dirPath1 = Path.Combine(@"D:\DATA LAMPIRAN PURCHN\", "UPD");
                if (!System.IO.Directory.Exists(dirPath1))
                {
                    System.IO.Directory.CreateDirectory(dirPath1);
                }

                string dirPath2 = Path.Combine(@"D:\DATA LAMPIRAN PURCHN\UPD\", NamaDept1);
                Session["dirPath2"] = dirPath2;
                if (!System.IO.Directory.Exists(dirPath2))
                {
                    System.IO.Directory.CreateDirectory(dirPath2);
                }

                FileUpload1.PostedFile.SaveAs(Path.Combine(dirPath2, fileName));
                Session["linkFile"] = fileName;

                upd1.UPDid = Convert.ToInt32(Session["UPDide"]);
                upd1.NamaFile = fileName;
                upd1.CreatedBy = user.UserName;
            }

            string NamaFileLampiran = Session["linkFile"].ToString();

            DataTable dt = new DataTable();
            dt.Columns.Add("File");
            dt.Columns.Add("Size");

            string dirPath3 = Session["dirPath2"].ToString();
            FileInfo fi = new FileInfo(Path.Combine(dirPath3, NamaFileLampiran));

            int Bytes = Convert.ToInt32(fi.Length);
            decimal KiloBytes = Bytes / 1000;
            decimal MB1 = KiloBytes / 1024;
            decimal MB = System.Math.Round(MB1, 3);

            dt.Rows.Add(fi.Name, KiloBytes + " " + "Kb" + "/" + " " + MB + " " + "Mb");

            GridView2.DataSource = dt;
            GridView2.DataBind();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}