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

namespace GRCweb1.Modul.ISO
{
    public partial class MasterTaskCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadDept();
                KategoriID.SelectedValue = "3";
                loadDataGrid();
            }
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Deskripsi.Text = string.Empty;
            IdKat.Text = string.Empty;
            KategoriID.Text = "Task";
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            ISO_Catagory Kat = new ISO_Catagory();
            ISO_CategoryFacade catFacade = new ISO_CategoryFacade();
            if (ddlDept.SelectedValue == "0")
            {
                txtMsg.InnerHtml = "Departement harus diisi";
                txtMsg.Attributes.Add("color", "red");
                return;
            }
            Kat.Task = KategoriID.SelectedItem.ToString();
            Kat.Desk = Deskripsi.Text;
            Kat.DeptID = int.Parse(ddlDept.SelectedValue.ToString());
            Kat.Bobots = 0;
            Kat.PlantID = ((Users)Session["Users"]).UnitKerjaID;
            Kat.PesType = int.Parse(KategoriID.SelectedValue.ToString());
            Kat.Target = txtTarget.Text;
            Kat.Checking = txtChecking.Text;

            if (IdKat.Text == string.Empty)
            {
                int result = catFacade.Insert(Kat);
                if (catFacade.Error == string.Empty && result > 0)
                {
                    loadDataGrid();
                    InsertLog("Insert");
                }
                else
                {
                    txtMsg.InnerHtml = catFacade.Error;
                }
            }
            else
            {
                Kat.RowStatus = 0;
                Kat.ID = int.Parse(IdKat.Text);
                int result = catFacade.Update(Kat);
                if (catFacade.Error == string.Empty && result > 0)
                {
                    loadDataGrid();
                    InsertLog("Update");
                }
                else
                {
                    txtMsg.InnerHtml = catFacade.Error;
                }
            }
            txtTarget.Text = string.Empty;
            Deskripsi.Text = string.Empty;
            txtChecking.Text = string.Empty;
            IdKat.Text = string.Empty;
        }
        protected void Kategori_Change(object sender, EventArgs e)
        {
            loadDataGrid();
        }
        protected void ddlDept_Change(object sender, EventArgs e)
        {
            loadDataGrid();
        }
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Task Category";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = Deskripsi.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            if (eventLogFacade.Error == string.Empty)
            {
                Deskripsi.Text = string.Empty;
                IdKat.Text = string.Empty;
            }
        }
        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            ISO_Catagory Kat = new ISO_Catagory();
            ISO_CategoryFacade catFacade = new ISO_CategoryFacade();

            if (IdKat.Text == string.Empty) return;

            Kat.Task = KategoriID.Text;
            Kat.Desk = Deskripsi.Text;
            Kat.Bobots = int.Parse(txtTarget.Text);
            Kat.RowStatus = -1;
            Kat.ID = int.Parse(IdKat.Text);
            int result = catFacade.Update(Kat);
            if (catFacade.Error == string.Empty && result > 0)
            {
                loadDataGrid();
                InsertLog("Delete");
            }
            else
            {
                txtMsg.InnerHtml = catFacade.Error;
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }
        protected void LoadGrid(ArrayList arrUsr)
        {
            GridView1.DataSource = arrUsr;
            GridView1.DataBind();
        }
        protected void lstCat_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Image img = (Image)e.Item.FindControl("edt");
            img.Visible = (((Users)Session["Users"]).DeptID.ToString() == "14" || ((Users)Session["Users"]).DeptID.ToString() == "7") ? true : false;
        }
        protected void lstCat_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string ID = e.CommandArgument.ToString();
            ISO_CategoryFacade iso = new ISO_CategoryFacade();
            iso.Criteria = "and ID=" + ID;
            ISO_Catagory ic = iso.RetrieveDetail();
            switch (e.CommandName.ToString())
            {
                case "edit":
                    IdKat.Text = ic.ID.ToString();
                    ddlDept.SelectedValue = ic.DeptID.ToString();
                    KategoriID.SelectedValue = ic.PesType.ToString();
                    Deskripsi.Text = ic.Desk.ToString();
                    txtTarget.Text = ic.Target.ToString();
                    txtChecking.Text = ic.Checking.ToString();
                    break;
            }
        }
        private void loadDataGrid()
        {
            ISO_CategoryFacade Kat = new ISO_CategoryFacade();
            ArrayList arrCat = new ArrayList();
            Kat.Criteria = (ddlDept.SelectedValue == "0" || KategoriID.SelectedValue == "2") ? "" : " and DeptID=" + ddlDept.SelectedValue;
            Kat.Criteria += (KategoriID.SelectedValue == string.Empty) ? "" : " and PesType=" + KategoriID.SelectedValue;
            Kat.OrderBy = " Order by CAST(KodeUrutan as int),PesType,ID";
            arrCat = Kat.Retrieve();
            GridView1.DataSource = arrCat;
            GridView1.DataBind();
            lstCat.DataSource = arrCat;
            lstCat.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                IdKat.Text = row.Cells[0].Text;
                Deskripsi.Text = row.Cells[2].Text;
                KategoriID.Text = row.Cells[1].Text;
                txtTarget.Text = row.Cells[3].Text;
            }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            //ListUserISO.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

        }
        private void LoadDept()
        {
            ArrayList arrData = new ArrayList();
            arrData = new DeptFacade().Retrieve();
            ddlDept.Items.Clear();
            ddlDept.Items.Add(new ListItem("--Pilih--", "0"));
            foreach (Dept dept in arrData)
            {
                ddlDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
            }
            ddlDept.SelectedValue = ((Users)Session["Users"]).DeptID.ToString();
        }

    }
}