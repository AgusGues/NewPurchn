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
using DefectFacade;
using Factory;
using DataAccessLayer;
using System.IO;
using BasicFrame.WebControls;

namespace GRCweb1.Modul.Boardmill
{
    public partial class InputBendingStrength : System.Web.UI.Page
    {
        BendingStrength bendingStrength = new BendingStrength();
        BendingStrengthFacade bendingStrengFacade = new BendingStrengthFacade();
        string PilhanFormula = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["ListRountineTest"] = null;
                Session["ListProductionTest"] = null;
                Global.link = "~/Default.aspx";
                txtTanggalInput.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtTanggalInput2.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadFormula();
                LoadFormula1();
                LoadGroupProduksi();
                LoadJenisProduksi();
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
            "MyScript", myScript, true);
        }

        private string ValidateTextRountineTest()
        {
            string strmessage = string.Empty;

            if (ddlFormula1.SelectedIndex == 0)
                return "Formula harus dipilih";
            else if (txtBK.Text == string.Empty)
                return "BK tidak boleh kosong";
            else if (txtT.Text == string.Empty)
                return "T tidak boleh kosong";
            else if (txtL.Text == string.Empty)
                return "L tidak boleh kosong";
            else if (txtP.Text == string.Empty)
                return "P tidak boleh kosong";
            else if (txtBA.Text == string.Empty)
                return "BA tidak boleh kosong";
            else if (txtBB.Text == string.Empty)
                return "BB tidak boleh kosong";
            else if (txtBK2.Text == string.Empty)
                return "BK tidak boleh kosong";
            else if (txtLBC.Text == string.Empty)
                return "LB 'C' tidak boleh kosong";
            else if (txtLBL.Text == string.Empty)
                return "LB 'L' tidak boleh kosong";
            else if (txtLKC.Text == string.Empty)
                return "LK 'C' tidak boleh kosong";
            else if (txtLKL.Text == string.Empty)
                return "LK 'L' tidak boleh kosong";
            //if (Convert.ToDateTime(txtTanggalInput2.Text).ToString("dd-MM-yyyy") != bendingstrng_facade.cekIDFormulaProduksiTanggal(ddlFormula1.SelectedValue))
            //    return "Prod. Date Harus sama dengan Tanggal Formula yang digunakan";
            if (Convert.ToDateTime(txtTanggalInput2.Text).ToString("dd-MM-yyyy") != bendingStrengFacade.cekIDFormulaProduksiTanggal(ddlFormula1.SelectedValue))
                return "Prod. Date Harus sama dengan Tanggal Formula yang digunakan";
            return strmessage;

        }



        private string ValidateTextProductionTest()
        {
            string strmessage = string.Empty;

            if (ddlFormula.SelectedIndex == 0)
                return "Formula harus dipilih";
            else if (ddlGroupProduksi.SelectedIndex == 0)
                return "Group Produksi Tidak boleh kosong";
            else if (ddlJenisProduksi.SelectedIndex == 0)
                return "Jenis Produksi tidak boleh kosong";
            else if (txtThicknessC.Text == string.Empty)
                return "Thickness C tidak boleh kosong";
            else if (txtThicknessL.Text == string.Empty)
                return "Thickness L tidak boleh kosong";
            else if (txtPeakLoadC.Text == string.Empty)
                return "Peak Load C tidak boleh kosong";
            else if (txtPeakloadL.Text == string.Empty)
                return "Peak Load L tidak boleh kosong";
            else if (txtPeakElongationC.Text == string.Empty)
                return "Peak Elongation C  tidak boleh kosong";
            else if (txtPeakElongationL.Text == string.Empty)
                return "Peak Elongation L boleh kosong";
            else if (txtBendingStrengthC.Text == string.Empty)
                return "BendingStrength C tidak boleh kosong";
            else if (txtBendingStrengthL.Text == string.Empty)
                return "BendingStrength L tidak boleh kosong";
            else if (txtAreaUncerCurveC.Text == string.Empty)
                return "Area Under Curve C tidak boleh kosong";
            else if (txtAreaUnderCurveL.Text == string.Empty)
                return "Area Under Curve L tidak boleh kosong";


            return strmessage;

        }

        private void clearProductionTest()
        {
            txtThicknessC.Text = "";
            txtThicknessL.Text = "";
            txtPeakLoadC.Text = "";
            txtPeakloadL.Text = "";
            txtPeakElongationC.Text = "";
            txtPeakElongationL.Text = "";
            txtBendingStrengthC.Text = "";
            txtBendingStrengthL.Text = "";
            txtAreaUncerCurveC.Text = "";
            txtAreaUnderCurveL.Text = "";


        }

        private void clearRoutineTest()
        {

            txtBK.Text = "";
            txtT.Text = "";
            txtL.Text = "";
            txtP.Text = "";
            txtBA.Text = "";
            txtBB.Text = "";
            txtBK2.Text = "";
            txtLBC.Text = "";
            txtLBL.Text = "";
            txtLKC.Text = "";
            txtLKL.Text = "";
        }

        private void LoadFormula()
        {
            ArrayList arrFormula = new ArrayList();
            BendingStrengthFacade bendingSt = new BendingStrengthFacade();
            arrFormula = bendingSt.RetrieveFormula1();

            ddlFormula.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (BendingStrength Bs in arrFormula)
            {
                ddlFormula.Items.Add(new ListItem(Bs.FormulaName, Bs.FormulaID.ToString()));
            }
        }

        private void LoadFormula1()
        {
            ArrayList arrFormula = new ArrayList();
            BendingStrengthFacade bendingSt = new BendingStrengthFacade();
            arrFormula = bendingSt.RetrieveFormula();

            ddlFormula1.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (BendingStrength Bs in arrFormula)
            {
                ddlFormula1.Items.Add(new ListItem(Bs.Formula + "\t" + "\t" + "\t" + "\t" + Bs.Tanggal.ToString() + "\t" + "\t" + Bs.GroupProduksi + "\t" + "\t" + Bs.JenisProduksi, Bs.FormulaID.ToString()));
                PilhanFormula = Bs.Formula;
            }


        }

        private void LoadGroupProduksi()
        {
            ArrayList arrGroupProduksi = new ArrayList();
            BendingStrengthFacade bendingSt = new BendingStrengthFacade();
            arrGroupProduksi = bendingSt.RetrieveGroupProduksi();

            ddlGroupProduksi.Items.Add(new ListItem("-- Pilih --", "0"));

            foreach (BendingStrength Bs in arrGroupProduksi)
            {
                ddlGroupProduksi.Items.Add(new ListItem(Bs.Group, Bs.FormulaID.ToString()));
            }
        }

        private void LoadJenisProduksi()
        {
            ArrayList arrGroupProduksi = new ArrayList();
            BendingStrengthFacade bendingSt = new BendingStrengthFacade();
            arrGroupProduksi = bendingSt.RetrieveJenisProduksi();

            ddlJenisProduksi.Items.Add(new ListItem("-- Pilih --", "0"));

            foreach (BendingStrength Bs in arrGroupProduksi)
            {
                ddlJenisProduksi.Items.Add(new ListItem(Bs.Kode.ToString()));
            }
        }



        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int proses = 0;


            string strValidate = ValidateTextRountineTest();

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";



            bendingStrength.IdProd = int.Parse(ddlFormula1.SelectedValue);
            bendingStrength.ProdDate = Convert.ToDateTime(txtTanggalInput2.Text);

            //bendingStrength.Formula = bendingstrng_facade.cekIDFormulaProduksi(ddlFormula1.SelectedValue);

            bendingStrength.Formula = bendingStrengFacade.cekIDFormulaProduksi(ddlFormula1.SelectedValue);

            bendingStrength.BK = decimal.Parse(txtBK.Text);
            bendingStrength.T = decimal.Parse(txtT.Text);
            bendingStrength.L = decimal.Parse(txtL.Text);
            bendingStrength.P = decimal.Parse(txtP.Text);
            bendingStrength.BA = decimal.Parse(txtBA.Text);
            bendingStrength.BB = decimal.Parse(txtBB.Text);
            bendingStrength.BK2 = decimal.Parse(txtBK2.Text);
            bendingStrength.LBC = decimal.Parse(txtLBC.Text);
            bendingStrength.LBL = decimal.Parse(txtLBL.Text);
            bendingStrength.LKC = decimal.Parse(txtLKC.Text);
            bendingStrength.LKL = decimal.Parse(txtLKL.Text);
            bendingStrength.CreateBy = ((Users)Session["Users"]).UserName;


            //Insert Biasa
            int intResult = 0;
            intResult = bendingStrengFacade.Insert(bendingStrength);


            if (intResult > 0)
            {
                bendingStrength.IdProd = int.Parse(ddlFormula1.SelectedValue);
                bendingStrength.Chek = -1;
                intResult = bendingStrengFacade.Update(bendingStrength);
            }

            clearRoutineTest();

            //Insert RollBack

            //TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            //transManager.BeginTransaction();
            //AbstractTransactionFacadeF absTrans = new BendingStrange_Facade(bendingStrength);
            //int intResult = absTrans.Insert(transManager);

            //if (absTrans.Error != string.Empty)
            //{
            //    transManager.RollbackTransaction();
            //    return;
            //}

            //proses = intResult;

            //if (proses > 0)
            //{

            //    bendingStrength.IdProd = int.Parse(ddlFormula1.SelectedValue);
            //    bendingStrength.Chek = -1;

            //    absTrans = new BendingStrange_Facade(bendingStrength);
            //    absTrans.Update(transManager);

            //    if (absTrans.Error != string.Empty)
            //    {
            //        transManager.RollbackTransaction();
            //        return;
            //    }
            //}

        }

        protected void btnUpdate1_ServerClick(object sender, EventArgs e)
        {
            string strValidate = ValidateTextProductionTest();

            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }

            string strEvent = "Insert";


            BendingStrength bendingStrength = new BendingStrength();
            BendingStrengthFacade bendingStrengFacade = new BendingStrengthFacade();
            bendingStrength.ProdDate = Convert.ToDateTime(txtTanggalInput.Text);
            bendingStrength.Formula = ddlFormula.SelectedItem.Text.ToString();
            bendingStrength.GroupProduksi = ddlGroupProduksi.SelectedItem.Text.ToString();
            bendingStrength.JenisProduksi = ddlJenisProduksi.SelectedItem.Text.ToString();


            bendingStrength.ThicknessC = decimal.Parse(txtThicknessC.Text);
            bendingStrength.ThicknessL = decimal.Parse(txtThicknessL.Text);
            bendingStrength.PeakLoadC = decimal.Parse(txtPeakLoadC.Text);
            bendingStrength.PeakLoadL = decimal.Parse(txtPeakloadL.Text);
            bendingStrength.PeakElongationC = decimal.Parse(txtPeakElongationC.Text);
            bendingStrength.PeakElongationL = decimal.Parse(txtPeakElongationL.Text);
            bendingStrength.BendingStrengthC = decimal.Parse(txtBendingStrengthC.Text);
            bendingStrength.BendingStrengthL = decimal.Parse(txtBendingStrengthL.Text);
            bendingStrength.AreaUnderCurveC = decimal.Parse(txtAreaUncerCurveC.Text);
            bendingStrength.AreaUnderCurveL = decimal.Parse(txtAreaUnderCurveL.Text);
            bendingStrength.CreateBy = ((Users)Session["Users"]).UserName;

            int intResult = 0;
            intResult = bendingStrengFacade.InsertProduction(bendingStrength);
            clearProductionTest();
        }

        protected void txtTanggalInput_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtTanggalInput2_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnListRountineTest_ServerClick(object sender, EventArgs e)
        {
            Session["ListRountineTest"] = null;
            //Session[""] = null;
            Response.Redirect("ListRountineTest.aspx?xXx=>" + (((Users)Session["Users"]).DeptID));
        }

        protected void btnProductionTest_ServerClick(object sender, EventArgs e)
        {
            Session["ListProductionTest"] = null;
            Response.Redirect("ListProductionTest.aspx?xXx=>" + (((Users)Session["Users"]).DeptID));
        }
    }
}