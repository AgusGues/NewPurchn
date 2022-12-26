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
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
//using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.Master
{
    public partial class HargaSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                LoadDataSup();
                LoadHargaItem();
            }
        }

        private void LoadDataSup()
        {
            HargaSupplierFacade Facade = new HargaSupplierFacade();
            ArrayList arrdata = Facade.LoadDataSup();
            if (Facade.Error == string.Empty)
            {
                GridView2.DataSource = arrdata;
                GridView2.DataBind();
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            HargaSupplierFacade Facade = new HargaSupplierFacade();
            ArrayList arrdata = new ArrayList();
            if (txtSearch1.Text!="") { arrdata = Facade.LoadDataSupW(txtSearch1.Text); } else { arrdata = Facade.LoadDataSup(); }
            if (Facade.Error == string.Empty)
            {
                GridView2.DataSource = arrdata;
                GridView2.DataBind();
            }
        }

        private void LoadHargaItem()
        {
            HargaSupplierFacade Facade = new HargaSupplierFacade();
            ArrayList arrdata = Facade.Retrieve2();
            if (Facade.Error == string.Empty)
            {
                Session["ListData"] = arrdata;
                GridView1.DataSource = arrdata;
                GridView1.DataBind();
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView2.Rows[index];
                int sup = int.Parse(row.Cells[1].Text);
                Session["SupParam"] = sup;
                int supid = 0;
                if (row.Cells[0].Text.Trim().ToUpper() == "KARAWANG")
                    supid = 7;
                if (row.Cells[0].Text.Trim().ToUpper() == "CITEUREUP")
                    supid = 1;
                if (row.Cells[0].Text.Trim().ToUpper() == "JOMBANG")
                    supid = 13;
                Session["SupPID"] = supid;
                if (sup > 0)
                {
                    LoadHargaItem();
                    HargaSupplierFacade Facade = new HargaSupplierFacade();
                    ArrayList arrDataD = Facade.WhereHargaSup(sup);
                    if (arrDataD.Count > 0)
                    {
                        LoadDetail(arrDataD);
                    }
                }
                else { LoadHargaItem(); }
                lblSupplier.Text = row.Cells[3].Text + "  (Plant : " + row.Cells[0].Text + ")";
            }
        }

        private void LoadDetail(ArrayList arrDataD)
        {
            ArrayList arrData = (ArrayList)Session["ListData"];
            foreach (Domain.HargaSupplier wheredata in arrDataD)//where data
            {
                int i = 0;
                foreach (Domain.HargaSupplier alldata in arrData)//all data
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                    if (alldata.IdHarga == wheredata.IdHargaD) { cb.Checked = true; }
                    i = i + 1;
                }
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (Session["SupParam"].ToString() == ""){DisplayAJAXMessage(this, "Pilih dulu Suppliernya");return;}
            HargaSupplierFacade facade = new HargaSupplierFacade();
            int intResult = 0;
            Domain.HargaSupplier domain = new Domain.HargaSupplier();
            domain.SupplierId = int.Parse(Session["SupParam"].ToString());
            domain.Plan = int.Parse(Session["SupPID"].ToString());
            intResult = facade.Delete(domain);
            if (facade.Error == string.Empty)
            {
                ArrayList arrdata = (ArrayList)Session["ListData"];
                int i = 0;
                foreach (Domain.HargaSupplier checkdata in arrdata)
                {
                    CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");
                    if (cb.Checked)
                    {
                        domain.IdHarga = checkdata.IdHarga;
                        intResult = facade.Insert(domain);
                        if (facade.Error != string.Empty)
                        {
                            break;
                        }
                    }
                    i = i + 1;
                }
                InsertLog("Edit");
            }
        }

        private void clearForm()
        {
            LoadDataSup();
            LoadHargaItem();
            Session["SupParam"] = "";
        }

        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadHargaItem();
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Master Harga Supplier";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = Session["SupParam"].ToString();
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
    }
}