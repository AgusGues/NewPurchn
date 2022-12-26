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
using System.Data.SqlClient;
using System.Configuration;

namespace GRCweb1.Modul.Boardmill
{
    public partial class InputScrab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTanggalInput.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {

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

            Scrub scrub = new Scrub();
            ScrubFacade scrubFacade = new ScrubFacade();
            scrub.TglInput = Convert.ToDateTime(txtTanggalInput.Text);
            scrub.Typescrab = int.Parse(ddlItemType.SelectedValue);
            scrub.Jumlah = decimal.Parse(txtJumlah.Text);
            scrub.Kg = decimal.Parse(txtKg.Text);
            scrub.M3 = decimal.Parse(txtM3.Text);
            scrub.Createdby = ((Users)Session["Users"]).UserName;
            scrub.Keterangan = txtKeterangan.Text;

            

            if (rdPalet.Checked == true)
            {
                scrub.Typeinput = 1;
            }
            else if (rdBin.Checked == true)
            {
                scrub.Typeinput = 2;
            }
            else
            {
                scrub.Typeinput = 0;
            }

            int intResult = 0;
            intResult = scrubFacade.Insert(scrub);
            clearText();
            
            LoadDataGridScrub(LoadGridScrub());

        }

        private void LoadDataGridScrub(ArrayList arrScrub)
        {
            this.GridView1.DataSource = arrScrub;
            this.GridView1.DataBind();
        }

        private ArrayList LoadGridScrub()
        {
            ArrayList arrScrub = new ArrayList();
            ScrubFacade scrubFacade = new ScrubFacade();
            arrScrub = scrubFacade.RetrieveScrab();
            if (arrScrub.Count > 0)
            {
                return arrScrub;
            }

            arrScrub.Add(new Scrub());
            return arrScrub;
        }

        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            clearText();
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }

        protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlLine_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtPrefix_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txNama_TextChanged(object sender, EventArgs e)
        {

        }
        protected void txNama_DataBinding(object sender, EventArgs e)
        {

        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private string ValidateText()
        {
            string strmessage = string.Empty;

            if (ddlItemType.SelectedIndex == 0)
                return "Tipe Scrab tidak boleh kosong";

            else if (txtJumlah.Text == string.Empty)
                return "Jumlah tidak boleh kosong";

            else if (txtKg.Text == string.Empty)
                return "Berat harus di isi";

            else if (txtM3.Text == string.Empty)
                return "Data Harus di isi";

            

            return strmessage;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void clearText()
        {
            txtJumlah.Text = "";
            txtKg.Text = "";
            txtM3.Text = "";
            txtKeterangan.Text = "";
            txtJumlah.Focus();
        }

        private void clearCalculasi()
        {
            txtJumlah.Text = "";
            txtKg.Text = "";
            txtM3.Text = "";
        }


        private void kalkulasi()
        {




        }


        protected void txtJumlah_TextChanged(object sender, EventArgs e)
        {
            int nomorUnit = ((Users)Session["Users"]).UnitKerjaID;
            decimal pembagi = 1596;


            ScrubFacade scrubFacade = new ScrubFacade();
            Scrub scrub = scrubFacade.RetrieveByUnitKerjaID(nomorUnit);

            decimal nilaipalet = scrub.BeratPalet;
            decimal nilaibin = scrub.BeratBin;



            decimal nilaim3 = (nilaibin / pembagi);
            decimal nilaim3palet = (nilaipalet / pembagi);



            if (rdPalet.Checked == true)
            {
                decimal hasilKg = decimal.Parse(txtJumlah.Text) * nilaipalet;
                decimal hasilM3 = decimal.Parse(txtJumlah.Text) * nilaim3palet;
                txtKg.Text = hasilKg.ToString();
                txtM3.Text = hasilM3.ToString();
            }

            else if (rdBin.Checked == true)
            {
                decimal hasilKg = decimal.Parse(txtJumlah.Text) * nilaibin;
                decimal hasilM3 = decimal.Parse(txtJumlah.Text) * nilaim3;
                txtKg.Text = hasilKg.ToString();
                txtM3.Text = hasilM3.ToString();
            }
        }
        protected void txtTanggalInput_TextChanged(object sender, EventArgs e)
        {

        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void rdPalet_CheckedChanged(object sender, EventArgs e)
        {
            txtJumlah.Text = "";
            txtKg.Text = "";
            txtM3.Text = "";
        }
        protected void rdBin_CheckedChanged(object sender, EventArgs e)
        {
            txtJumlah.Text = "";
            txtKg.Text = "";
            txtM3.Text = "";
        }
    }
}