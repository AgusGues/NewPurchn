using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using Factory;
using System.Data;

namespace GRCweb1.Modul.Factory
{
    public partial class FCLokasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadLokasi();
            }
        }
        protected void LoadLokasi()
        {

            ArrayList arrLokasi = new ArrayList();
            FC_LokasiFacade pf = new FC_LokasiFacade();
            if (txtSearch.Text != string.Empty)
                arrLokasi = pf.RetrieveByLokasi1(txtSearch.Text);
            else
                arrLokasi = pf.RetrieveByLokTypeID2(int.Parse(ddlProses.SelectedValue));
            GridView1.DataSource = arrLokasi;
            GridView1.DataBind();
        }
        protected void clearform()
        {
            txtLokasi.Text = string.Empty;
            txtID.Text = "0";
            btnUpdate.Disabled = true;
            btnDelete.Enabled = false;
            LoadLokasi();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                txtID.Text = row.Cells[0].Text;
                txtLokasi.Text = row.Cells[1].Text;
                btnUpdate.Disabled = false;
                btnDelete.Enabled = true;
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadLokasi();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            FC_Lokasi Lokasi = new FC_Lokasi();
            FC_LokasiFacade LokasiF = new FC_LokasiFacade();

            if (int.Parse(txtID.Text) == 0)
            {
                if (txtLokasi.Text != string.Empty)
                {
                    int ada = LokasiF.check(txtLokasi.Text);
                    if (ada == 0)
                    {
                        Lokasi.LokTypeID = int.Parse(ddlProses.SelectedValue);
                        Lokasi.Lokasi = txtLokasi.Text;
                        LokasiF.Insert(Lokasi);
                    }
                    else
                        DisplayAJAXMessage(this, "Data Lokasi : " + txtLokasi.Text + " sudah tersedia");
                }
            }
            else
            {
                if (txtLokasi.Text != string.Empty)
                {
                    Lokasi.Lokasi = txtLokasi.Text;
                    Lokasi.ID = int.Parse(txtID.Text);
                    //Lokasi.Status = 0;
                    LokasiF.Update(Lokasi);
                }
            }
            clearform();
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrLokasi = new ArrayList();
            FC_LokasiFacade pf = new FC_LokasiFacade();
            arrLokasi = pf.RetrieveByLokasi1(txtSearch.Text);
            GridView1.DataSource = arrLokasi;
            GridView1.DataBind();
            clearform();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
            txtID.Text = "0";
            btnUpdate.Disabled = false;
            btnDelete.Enabled = false;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            FC_Lokasi Lokasi = new FC_Lokasi();
            FC_LokasiFacade LokasiF = new FC_LokasiFacade();
            if (int.Parse(txtID.Text) != 0)
            {
                Lokasi.ID = int.Parse(txtID.Text);
                LokasiF.Delete(Lokasi);
            }
            clearform();
        }
        protected void ddlProses_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadLokasi();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "ConfirmIt()", true);
            //if (inpHide.Value != string.Empty)
            //{
            //    int DiagResult = int.Parse(inpHide.Value);

            //    if (DiagResult == 1)
            //    {

            //        //Do Somthing

            //        DisplayAJAXMessage(this, "You Have Selected Ok");
            //        inpHide.Value = string.Empty;
            //    }

            //    else if (DiagResult == 0)
            //    {

            //        //Do somthing

            //        DisplayAJAXMessage(this, "You have Selected Cancel");
            //        inpHide.Value = string.Empty;
            //    }
            //}
            //else
            //    inpHide.Value = string.Empty;
        }
    }
}