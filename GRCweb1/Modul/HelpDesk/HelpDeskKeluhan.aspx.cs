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

namespace GRCweb1.Modul.HelpDesk
{
    public partial class HelpDeskKeluhan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDataGridHelpDeskKeluhan(LoadGridHelpDeskKeluhan());
                clearForm();
                LoadDept();
                LoadHelpDeskCategory();
                btnDelete.Disabled = true;
                //btnPerbaikan.Disabled = true;
                txtAnalisa.Enabled = false;
                txtPerbaikan.Enabled = false;
                txtDeptID.Enabled = false;
                //txtTglPerbaikan.Enabled = false;

                if (((Users)Session["Users"]).DeptID == 14)
                {
                    //GridView1.Columns[10].Visible = true;
                    ddlPenyelesaian.Enabled = true;
                    ddlStatus.Enabled = true;
                    txtTglPerbaikan.Enabled = true;



                }
                //else if (((Users)Session["Users"]).UserName == "beny")
                //{
                //    //GridView1.Columns[10].Visible = true;
                //    ddlPenyelesaian.Enabled = true;
                //    ddlStatus.Enabled = true;
                //    txtTglPerbaikan.Enabled = true;

                //}
                else
                {
                    ddlPenyelesaian.Enabled = false;
                    ddlStatus.Enabled = false;
                    txtTglPerbaikan.Enabled = false;
                }

            }
        }

        private void LoadDataGridHelpDeskKeluhan(ArrayList arrHelpDeskKeluhan)
        {

            this.GridView1.DataSource = arrHelpDeskKeluhan;
            this.GridView1.DataBind();
        }


        private ArrayList LoadGridHelpDeskKeluhan()
        {

            //if (((Users)Session["Users"]).UserName == "razib")
            //Users usr = (Users)Session["Users"];
            {
                ArrayList arrHelpDeskKeluhan = new ArrayList();
                HelpDeskKeluhanFacade helpDeskKeluhanFacade = new HelpDeskKeluhanFacade();
                arrHelpDeskKeluhan = helpDeskKeluhanFacade.Retrieve();
                //arrHelpDeskKeluhan = helpDeskKeluhanFacade.RetrieveByUser(usr.UserName.ToString());
                if (arrHelpDeskKeluhan.Count > 0)
                {
                    return arrHelpDeskKeluhan;
                }

                arrHelpDeskKeluhan.Add(new HelpDeskKeluhan());
                return arrHelpDeskKeluhan;
            }
            //else
            //{
            //    Users usr = (Users)Session["Users"];               
            //    ArrayList arrHelpDeskKeluhan = new ArrayList();
            //    HelpDeskKeluhanFacade helpDeskKeluhanFacade = new HelpDeskKeluhanFacade();
            //    //arrHelpDeskKeluhan = helpDeskKeluhanFacade.Retrieve();
            //    arrHelpDeskKeluhan = helpDeskKeluhanFacade.RetrieveByUser(usr.UserName.ToString());
            //    if (arrHelpDeskKeluhan.Count > 0)
            //    {
            //        return arrHelpDeskKeluhan;
            //    }

            //    arrHelpDeskKeluhan.Add(new HelpDeskKeluhan());
            //    return arrHelpDeskKeluhan;
            //}
        }

        private void LoadDept()
        {
            ddlDept.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlDept.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                ddlDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));

                //txtStatus.Enabled = false;
            }
        }

        private void SelectDept(string strDept)
        {
            ddlDept.ClearSelection();
            //ddlCity.ClearSelection() ini buat apa 
            foreach (ListItem item in ddlDept.Items)
            {
                if (item.Text == strDept)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        private void SelectKategory(string strHelpDeskCategory)
        {
            //ddlDept.ClearSelection();
            ddlHelpDeskCategory.ClearSelection();
            foreach (ListItem item in ddlHelpDeskCategory.Items)
            {
                if (item.Text == strHelpDeskCategory)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                DeptFacade deptFacade = new DeptFacade();
                Dept dept = deptFacade.RetrieveById(int.Parse(ddlDept.SelectedValue));
                if (deptFacade.Error == string.Empty && dept.ID > 0)
                {
                    txtDeptID.Text = dept.DeptCode;

                    //txtCariNamaBrg.Focus();
                }
            }
        }


        private void LoadHelpDeskCategory()
        {
            ArrayList arrHelpDeskCategory = new ArrayList();
            HelpDeskKeluhanFacade helpDeskKeluhanFacade = new HelpDeskKeluhanFacade();
            arrHelpDeskCategory = helpDeskKeluhanFacade.RetrieveHelpDeskCategory();
            ddlHelpDeskCategory.Items.Add(new ListItem("-- Pilih Kategori --", "0"));
            foreach (HelpDeskCategory helpDeskCategory in arrHelpDeskCategory)
            {
                ddlHelpDeskCategory.Items.Add(new ListItem(helpDeskCategory.HelpCategory, helpDeskCategory.ID.ToString()));
            }
        }

        private void SelectHelpDeskCategory(string strHelpDeskCategory)
        {
            ddlHelpDeskCategory.ClearSelection();
            foreach (ListItem item in ddlHelpDeskCategory.Items)
            {
                if (item.Text == strHelpDeskCategory)
                {
                    item.Selected = true;
                    return;
                }
            }

        }


        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrHelpDeskKeluhan = new ArrayList();
            HelpDeskKeluhanFacade helpDeskKeluhanFacade = new HelpDeskKeluhanFacade();
            if (txtSearch.Text == string.Empty)
                arrHelpDeskKeluhan = helpDeskKeluhanFacade.Retrieve();
            else
                arrHelpDeskKeluhan = helpDeskKeluhanFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrHelpDeskKeluhan.Count > 0)
            {
                return arrHelpDeskKeluhan;
            }

            arrHelpDeskKeluhan.Add(new SuppPurch());
            return arrHelpDeskKeluhan;
        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtHelpDeskNo.Text = string.Empty;
            txtDeptID.Text = string.Empty;
            txtKeluhan.Text = string.Empty;
            txtHelpTgl.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            txtTglPerbaikan.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = (((Users)Session["Users"]).UserName);
            ddlHelpDeskCategory.SelectedIndex = 0;
            ddlDept.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            //txtStatus.Text = "Open";
            //ddlStatus.SelectedIndex = 0;
            btnUpdate.Disabled = false;
            txtPIC.Text = string.Empty;
            txtAnalisa.Text = string.Empty;
            btnDelete.Disabled = true;
            txtPerbaikan.Text = string.Empty;


        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {

            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";

            Domain.HelpDeskKeluhan helpDeskKeluhan = new Domain.HelpDeskKeluhan();
            HelpDeskKeluhanFacade helpDeskKeluhanFacade = new HelpDeskKeluhanFacade();
            if (ViewState["id"] != null)
            {
                helpDeskKeluhan.ID = int.Parse(ViewState["id"].ToString());
                helpDeskKeluhan.HelpDeskNo = txtHelpDeskNo.Text;
                strEvent = "Edit";
            }
            else
            {
                helpDeskKeluhanFacade = new HelpDeskKeluhanFacade();
                int noUrut = helpDeskKeluhanFacade.CountHelpKeluhan();
                noUrut = noUrut + 1;
                //            supplier.SupplierCode = "C" + noUrut.ToString().PadLeft(4, '0');
                helpDeskKeluhan.HelpDeskNo = "CHLPD" + noUrut.ToString().PadLeft(4, '0');
            }

            helpDeskKeluhan.HelpTgl = DateTime.Parse(txtHelpTgl.Text);
            helpDeskKeluhan.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
            helpDeskKeluhan.DeptName = ddlDept.SelectedItem.ToString();
            helpDeskKeluhan.HelpDeskCategoryID = int.Parse(ddlHelpDeskCategory.SelectedValue.ToString());
            helpDeskKeluhan.Status = int.Parse(ddlStatus.SelectedIndex.ToString());
            helpDeskKeluhan.Keluhan = txtKeluhan.Text;
            helpDeskKeluhan.Perbaikan = txtPerbaikan.Text;
            helpDeskKeluhan.TglPerbaikan = DateTime.Parse(txtTglPerbaikan.Text);
            helpDeskKeluhan.Analisa = txtAnalisa.Text;
            helpDeskKeluhan.KategoriPenyelesaianID = int.Parse(ddlPenyelesaian.SelectedIndex.ToString());
            helpDeskKeluhan.PIC = txtPIC.Text;
            helpDeskKeluhan.CreatedBy = ((Users)Session["Users"]).UserName;
            helpDeskKeluhan.LastModifiedBy = ((Users)Session["Users"]).UserName;

            int intResult = 0;
            if (helpDeskKeluhan.ID > 0)
            {
                //DisplayAJAXMessage(this, "Tidak bisa di edit, Harus hubungi IT ..");
                //return;
                intResult = helpDeskKeluhanFacade.Update(helpDeskKeluhan);
            }
            else
            {
                intResult = helpDeskKeluhanFacade.Insert(helpDeskKeluhan);

                if (helpDeskKeluhanFacade.Error == string.Empty)
                {
                    if (intResult > 0)
                    {

                        txtHelpDeskNo.Text = helpDeskKeluhan.HelpDeskNo;
                        DisplayAJAXMessage(this, "Data Keluhan Sudah Tersimpan");
                    }
                }
            }

            if (helpDeskKeluhanFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridHelpDeskKeluhan(LoadGridHelpDeskKeluhan());
                txtHelpDeskNo.Enabled = false;

                //txtSupplierCode.Enabled = false;
                //txtSupplierName.Enabled = true;
                //txtAlamat.Enabled = true;
                //clearForm();
                txtAnalisa.Enabled = false;
                txtPerbaikan.Enabled = false;
                InsertLog(strEvent);
            }
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            Domain.HelpDeskKeluhan helpDeskKeluhan = new Domain.HelpDeskKeluhan();
            HelpDeskKeluhanFacade HelpDeskKeluhanFacade = new HelpDeskKeluhanFacade();

            helpDeskKeluhan.ID = int.Parse(ViewState["id"].ToString());
            helpDeskKeluhan.LastModifiedBy = Global.UserLogin.UserName;
            //int intResult = SuppPurchFacade.Delete(suppPurch);
            int intResult = HelpDeskKeluhanFacade.Delete(helpDeskKeluhan);
            if (HelpDeskKeluhanFacade.Error == string.Empty && intResult > 0)
            {
                LoadDataGridHelpDeskKeluhan(LoadGridHelpDeskKeluhan());
                //clearForm();

                InsertLog("Delete");
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  e.Row.Cells[1].Text = DateTime.Parse(e.Row.Cells[1].Text).ToString("dd-MMM-yyyy");

                if (e.Row.Cells[9].Text == "0")
                {
                    e.Row.Cells[9].Text = "Open";
                }
                if (e.Row.Cells[9].Text == "1")
                {
                    e.Row.Cells[9].Text = "OnProgress";
                }
                if (e.Row.Cells[9].Text == "2")
                {
                    e.Row.Cells[9].Text = "Solved";
                }
                if (e.Row.Cells[9].Text == "3")
                {
                    e.Row.Cells[9].Text = "Close";
                }
                //next what status
                if (e.Row.Cells[8].Text == "0")
                {
                    e.Row.Cells[8].Text = "Kategori";
                }
                if (e.Row.Cells[8].Text == "1")
                {
                    e.Row.Cells[8].Text = "Hardware";
                }
                if (e.Row.Cells[8].Text == "2")
                {
                    e.Row.Cells[8].Text = "Software";
                }

                //(e.Row.Cells[10].Enabled)=(((Users)Session["Users"]).UserName=="razib")?true:false;
                if (((Users)Session["Users"]).DeptID == 14)
                {
                    GridView1.Columns[11].Visible = true;

                }
                else
                {
                    GridView1.Columns[11].Visible = false;
                }


            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtHelpDeskNo.Text = cekString(row.Cells[1].Text);
                txtHelpTgl.Text = cekString(row.Cells[2].Text);
                txtPIC.Text = cekString(row.Cells[3].Text);
                LoadDept();
                SelectDept(cekString(row.Cells[4].Text));
                txtKeluhan.Text = cekString(row.Cells[5].Text);
                ddlHelpDeskCategory.Items.Clear();
                LoadHelpDeskCategory();
                SelectKategory(cekString(row.Cells[8].Text));
                txtPerbaikan.Text = cekString(row.Cells[7].Text);
                txtAnalisa.Text = cekString(row.Cells[6].Text);
                txtTglPerbaikan.Text = cekString(row.Cells[10].Text);
                // ddlStatus.SelectedIndex = 0;
                //btnPerbaikan.Disabled = false;
                btnDelete.Disabled = true;
                txtPerbaikan.Enabled = true;
                txtAnalisa.Enabled = true;



            }
        }
        private string cekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            //LoadDataGridHelpDeskKeluhan(LoadGridHelpDeskKeluhan());
            LoadDataGridHelpDeskKeluhan(LoadGridByCriteria());
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
            txtAnalisa.Enabled = false;
            txtPerbaikan.Enabled = false;

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGridHelpDeskKeluhan(LoadGridHelpDeskKeluhan());
            else
                LoadDataGridHelpDeskKeluhan(LoadGridHelpDeskKeluhan());
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "HelpDesk HelpDeskKeluhan";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtHelpDeskNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
                clearForm();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private string ValidateText()
        {
            if (ddlDept.SelectedIndex == 0)
                return "Pilih Departemen";
            else if (txtPIC.Text == string.Empty)
                return "PIC harus diisi";
            else if (ddlHelpDeskCategory.SelectedIndex == 0)
                return "Pilih Kategori";
            else if (txtKeluhan.Text == string.Empty)
                return "Keluhan tidak boleh kosong";
            return string.Empty;

        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlStatus.SelectedValue == "Open")
            //{

            //}
        }

    }
}