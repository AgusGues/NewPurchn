using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Web.Services;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.IO;

namespace GRCweb1.Modul.Budgeting
{
    public partial class MasterBudMaskerFin1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadBulan();
                LoadTahun();
                LoadBudget();
                txtMS.Text = "0";
                txtST.Text = "0";
                txtSTB8.Text = "0";
                txtMSKTL.Text = "0";
                LID.Text = "0";
            }
        }
        private int InsertData()
        {
            int result = 0;
            string strSQL = "if (select count(ID) from MaterialSTMSBudgetFIN where tahun=" + ddlTahun.SelectedValue + " and bulan=" + ddlBulan.SelectedValue + ")=0 begin" +
                " insert MaterialSTMSBudgetFIN(Tahun, Bulan, Bagian, Jabatan, JmlKary, StdST, StdMS, QtyST, QtyMS, Aproval, RowStatus," +
                " CreatedBy, CreatedTime, LastModifiedBy, LastModifiedTime,StdSTB8,StdMSKT,QtySTB8,QtyMSKT)values(" + ddlTahun.SelectedValue + "," + ddlBulan.SelectedValue +
                ",'-','-',0,0,0," + txtST.Text + "," + txtMS.Text + ",0,0,'" + ((Users)Session["Users"]).UserName + "',getdate(),'" +
                ((Users)Session["Users"]).UserName + "',getdate(),0,0," + txtSTB8.Text + "," + txtMSKTL.Text + ") end";
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error != string.Empty)
                result = -1;
            return result;
        }
        private int EditData(string ID)
        {
            int result = 0;
            string strSQL = "update MaterialSTMSBudgetFIN set qtyst=" + txtST.Text + ",qtyms=" + txtMS.Text + ",QtySTB8=" + txtSTB8.Text + ",QtyMSKT=" + txtMSKTL.Text + ",lastmodifiedby='" +
                ((Users)Session["Users"]).UserName + "',LastModifiedTime=getdate() where ID=" + LID.Text;
            DataAccess dataAccess = new DataAccess(Global.ConnectionString());
            SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
            if (dataAccess.Error != string.Empty)
                result = -1;
            return result;
        }
        private void LoadBudget()
        {
            string strSQL = "select ID,Tahun,Bulan,QtyST Sarung_Tangan,QtyMS Masker,QtySTB8 B8,QtyMSKT TL from MaterialSTMSBudgetFIN  where rowstatus>-1 order by tahun desc,bulan desc";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            ddlBulan.Items.Add(new ListItem("--Pilih--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }
        private void LoadTahun()
        {
            PakaiFacade pd = new PakaiFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                //try
                //{
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                LID.Text = row.Cells[0].Text;
                txtST.Text = row.Cells[3].Text;
                txtMS.Text = row.Cells[5].Text;
                ddlBulan.SelectedValue = row.Cells[2].Text;
                ddlTahun.SelectedValue = row.Cells[1].Text;

                //}
                //catch
                //{
                //}
            }
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (LID.Text != "0")
            {
                EditData(LID.Text);
                DisplayAJAXMessage(this, "Data Berhasil di Update!");
            }
            else
            {
                InsertData();
                DisplayAJAXMessage(this, "Data Berhasil di Simpan!");
            }
            LoadBulan();
            LoadTahun();
            LoadBudget();
            txtMS.Text = "0";
            txtMSKTL.Text = "0";
            txtST.Text = "0";
            txtSTB8.Text = "0";
            LID.Text = "0";

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            LoadBulan();
            LoadTahun();
            LoadBudget();
            txtMS.Text = "0";
            txtST.Text = "0";
            txtMSKTL.Text = "0";
            txtSTB8.Text = "0";
            LID.Text = "0";
        }

    }
}