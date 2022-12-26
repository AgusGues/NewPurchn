using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;
using System.Collections;

namespace GRCweb1.Modul.ISO_UPD
{
    public partial class FormUpdateMasterTanggal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadDataGridViewItem(LoadDataGrid());
                clearForm();
            }
        }

        private void LoadDataGridViewItem(ArrayList arrUPD)
        {
            this.GridView1.DataSource = arrUPD;
            this.GridView1.DataBind();
        }

        private ArrayList LoadDataGrid()
        {
            ArrayList arrUPD = new ArrayList();
            ISO_UpdFacade updF = new ISO_UpdFacade();
            arrUPD = updF.RetrieveMaster();
            if (arrUPD.Count > 0)
            {
                return arrUPD;
            }

            arrUPD.Add(new ISO_Upd());
            return arrUPD;
        }

        private void clearForm()
        {
            ViewState["id"] = null;
            txtK.Text = string.Empty;
            txtT.Text = DateTime.Now.Date.ToString("dd-MMM-yyyy");
            txtR.Text = string.Empty;
            txtNoF.Text = string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text == "1")
                {
                    e.Row.Cells[1].Text = "Pedoman Mutu";
                }
                if (e.Row.Cells[1].Text == "2")
                {
                    e.Row.Cells[1].Text = "Instruksi Kerja";
                }
                if (e.Row.Cells[1].Text == "3")
                {
                    e.Row.Cells[1].Text = "Form";
                }
                if (e.Row.Cells[1].Text == "4")
                {
                    e.Row.Cells[1].Text = "Prosedur";
                }
                if (e.Row.Cells[1].Text == "5")
                {
                    e.Row.Cells[1].Text = "Rencana Mutu";
                }
                if (e.Row.Cells[1].Text == "6")
                {
                    e.Row.Cells[1].Text = "Standar";
                }
                if (e.Row.Cells[1].Text == "9")
                {
                    e.Row.Cells[1].Text = "Bagan Alir";
                }
            }

        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;

            LoadDataGridViewItem(LoadDataGrid());
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ISO_Upd upd1 = new ISO_Upd();
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];

                ViewState["id"] = int.Parse(row.Cells[0].Text);
                txtK.Text = CekString(row.Cells[1].Text);
                txtT.Text = CekString(row.Cells[2].Text);
                txtR.Text = CekString(row.Cells[3].Text);
                txtNoF.Text = CekString(row.Cells[4].Text);
            }
        }

        private string CekString(string strValue)
        {
            if (strValue == "&nbsp;")
                return string.Empty;

            return strValue;
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int intResult = 0;
            Users users = new Users();
            ISO_Upd upd1 = new ISO_Upd();
            ISO_DMDFacade dmdF = new ISO_DMDFacade();
            if (ViewState["id"] != null)
            {
                upd1.ID = int.Parse(ViewState["id"].ToString());
            }

            upd1.RevNo = txtR.Text;
            upd1.Tanggal = DateTime.Parse(txtT.Text);
            upd1.FormNO = txtNoF.Text;
            upd1.idCategory = txtK.Text;
            if (upd1.idCategory == "Pedoman Mutu")
                upd1.idCategory = "1";
            if (upd1.idCategory == "Instruksi Kerja")
                upd1.idCategory = "2";
            if (upd1.idCategory == "Form")
                upd1.idCategory = "3";
            if (upd1.idCategory == "Prosedure")
                upd1.idCategory = "4";
            if (upd1.idCategory == "Rencana Mutu")
                upd1.idCategory = "5";
            if (upd1.idCategory == "Standard")
                upd1.idCategory = "6";


            intResult = dmdF.UpdateMasterRev(upd1);


            clearForm();
            LoadDataGridViewItem(LoadDataGrid());

        }
    }
}