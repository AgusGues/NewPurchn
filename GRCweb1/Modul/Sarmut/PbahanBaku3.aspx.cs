using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Domain;
using BusinessFacade;
using DataAccessLayer;

namespace GRCweb1.Modul.SarMut
{
    public partial class PbahanBaku3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                #region Header Line
                line1.Text = "Line 1";
                line2.Text = "Line 2";
                line3.Text = "Line 3";
                line4.Text = "Line 4";
                line5.Text = "Line 5";
                line6.Text = "Line 6";
                if (user.UnitKerjaID == 1)
                {
                    lblF1.Text = "G185";
                    lblF2.Text = "G206";
                    frmula1.Text = (75.0).ToString("N0");
                    frmula1a.Text = (79.0).ToString("N0");
                }
                else if (user.UnitKerjaID == 7)
                {
                    lblF1.Text = "G185";
                    lblF2.Text = "G206";
                    frmula1.Text = (90.0).ToString("N0");
                    frmula1a.Text = (79.0).ToString("N0");
                }
                else
                {
                    lblF1.Text = "G208";
                    lblF2.Text = "G208";
                    frmula1.Text = (90.0).ToString("N0");
                    frmula1a.Text = (90.0).ToString("N0");
                }
                frmula2.Text = (90.0).ToString("N0");
                frmula2a.Text = (79.0).ToString("N0");
                frmula3.Text = (90.0).ToString("N0");
                frmula3a.Text = (79.0).ToString("N0");
                frmula4.Text = (90.0).ToString("N0");
                frmula4a.Text = (79.0).ToString("N0");
                frmula5.Text = (90.0).ToString("N0");
                frmula5a.Text = (79.0).ToString("N0");
                frmula6.Text = (90.0).ToString("N0");
                frmula6a.Text = (79.0).ToString("N0");
                #endregion
            }
        }


        protected bool IsChecked = true;
        protected void OnCheckedChanged(object sender, EventArgs e)
        {
            this.IsChecked = (sender as CheckBox).Checked;
        }


        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            ArrayList arrItem = (Session["ListBBPemakaian"] == null) ? new ArrayList() : (ArrayList)Session["ListBBPemakaian"];
            PbahanBaku pbb = new PbahanBaku();
            pbb.ID = int.Parse(txtID.Value);
            pbb.Tgl_Prod = DateTime.Parse(txtTanggal.Text);
            arrItem.Add(pbb);
            Session["ListBBPemakaian"] = arrItem;
            lstR.DataSource = arrItem;
            lstR.DataBind();
            btnSimpan.Enabled = true;

        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("RekapPulp.aspx");
        }

        protected void jmlL1G_Change(object sender, EventArgs e)
        {
            decimal tMix = 0;
            ArrayList arrData = new ArrayList();
            for (int i = 0; i < lstR.Items.Count; i++)
            {
                PbahanBaku pbx2 = new PbahanBaku();
                Label lbltgl = (Label)lstR.Items[i].FindControl("lbltglProd");
                TextBox L1 = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1");
                TextBox L1x = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1x");
                TextBox L2 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2");
                TextBox L2x = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x");
                TextBox L3 = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3");
                TextBox L3x = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3x");
                TextBox L4 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4");
                TextBox L4x = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x");
                TextBox L5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                TextBox L5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                TextBox L6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                TextBox L6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                HiddenField hd = (HiddenField)lstR.Items[i].FindControl("txtNilaiID");
                Label lbltMix = (Label)lstR.Items[i].FindControl("tMix");
                Label lblFrmula = (Label)lstR.Items[i].FindControl("Frmula");
                Label lblKvKg = (Label)lstR.Items[i].FindControl("txtKvKg");
                Label lblKL = (Label)lstR.Items[i].FindControl("txtKL");
                TextBox SdL = (TextBox)lstR.Items[i].FindControl("txtSdL");
                TextBox SspB = (TextBox)lstR.Items[i].FindControl("txtSspB");
                TextBox AT = (TextBox)lstR.Items[i].FindControl("txtAT");
                Label lblEfis = (Label)lstR.Items[i].FindControl("txtEfis");
                Label lblKvKg2 = (Label)lstR.Items[i].FindControl("txtKvKg2");
                Label lblKvEfis = (Label)lstR.Items[i].FindControl("txtKvEfis");
                Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                Label lblKsKg0 = (Label)lstR.Items[i].FindControl("txtKsKg0");
                Label lblKsKg = (Label)lstR.Items[i].FindControl("txtKsKg");
                Label lblKsEfis = (Label)lstR.Items[i].FindControl("txtKsEfis");
                Label lblKuKg = (Label)lstR.Items[i].FindControl("txtKuKg");
                Label lblKuEfs = (Label)lstR.Items[i].FindControl("txtKuEfs");
                Label lblKgrade2Kg = (Label)lstR.Items[i].FindControl("txtKgrade2Kg");
                Label lblKgrade2Eff = (Label)lstR.Items[i].FindControl("txtKgrade2Eff");
                Label lblKgrade3Kg = (Label)lstR.Items[i].FindControl("txtKgrade3Kg");
                Label lblKgrade3Eff = (Label)lstR.Items[i].FindControl("txtKgrade3Eff");
                Label lblTEfis = (Label)lstR.Items[i].FindControl("txtTEfis");
                TextBox KV = (TextBox)lstR.Items[i].FindControl("txtKV");
                TextBox KB = (TextBox)lstR.Items[i].FindControl("txtKB");
                TextBox KS = (TextBox)lstR.Items[i].FindControl("txtKS");
                TextBox KGU = (TextBox)lstR.Items[i].FindControl("txtKGradeUtama");
                TextBox KGU2 = (TextBox)lstR.Items[i].FindControl("txtKgrade2");
                TextBox KGU3 = (TextBox)lstR.Items[i].FindControl("txtKgrade3");
                if (L1.Text != string.Empty || L2.Text != string.Empty || L3.Text != string.Empty || L4.Text != string.Empty || L5.Text != string.Empty || L6.Text != string.Empty || lbltMix.Text != string.Empty)
                {
                    if (L1.Text == "" || L1x.Text == "" || L2.Text == "" || L2x.Text == "" || L3.Text == "" || L3x.Text == "" || L4.Text == "" || L4x.Text == "" || L5.Text == "" || L5x.Text == "" || L6.Text == "" || L6x.Text == "" || lbltMix.Text == "" || lblFrmula.Text == "" || lblKL.Text == "" || SdL.Text == ""
                        || SspB.Text == "" || AT.Text == "" || lblEfis.Text == "" || lblKvKg.Text == "" || lblKvKg2.Text == "" || lblKvEfis.Text == "" || lblKBimaBK.Text == "" || lblKBimaSampah.Text == "" || lblKBimaBB.Text == "" || lblKsKg0.Text == "" || lblKsKg.Text == ""
                        || lblKsEfis.Text == "" || lblKuKg.Text == "" || lblKuEfs.Text == "" || lblKgrade2Kg.Text == "" || lblKgrade2Eff.Text == "" || lblKgrade3Kg.Text == "" || lblKgrade3Eff.Text == "" || lblTEfis.Text == "" || KV.Text == "" || KB.Text == "" || KS.Text == "" || KGU.Text == "" || KGU2.Text == "" || KGU3.Text == "")
                    {
                        L1.Text = "0";
                        L1x.Text = "0";
                        L2.Text = "0";
                        L2x.Text = "0";
                        L3.Text = "0";
                        L3x.Text = "0";
                        L4.Text = "0";
                        L4x.Text = "0";
                        L5.Text = "0";
                        L5x.Text = "0";
                        L6.Text = "0";
                        L6x.Text = "0";
                        lbltMix.Text = "0";
                        lblFrmula.Text = "0";
                        lblKL.Text = "0";
                        SdL.Text = "0";
                        SspB.Text = "0";
                        AT.Text = "0";
                        lblEfis.Text = "0";
                        lblKvKg.Text = "0";
                        lblKvKg2.Text = "0";
                        lblKvEfis.Text = "0";
                        lblKBimaBK.Text = "0";
                        lblKBimaSampah.Text = "0";
                        lblKBimaBB.Text = "0";
                        lblKsKg0.Text = "0";
                        lblKsKg.Text = "0";
                        lblKsEfis.Text = "0";
                        lblKuKg.Text = "0";
                        lblKuEfs.Text = "0";
                        lblKgrade2Kg.Text = "0";
                        lblKgrade2Eff.Text = "0";
                        lblKgrade3Kg.Text = "0";
                        lblKgrade3Eff.Text = "0";
                        lblTEfis.Text = "0";
                        KV.Text = "0";
                        KB.Text = "0";
                        KS.Text = "0";
                        KGU.Text = "0";
                        KGU2.Text = "0";
                        KGU3.Text = "0";
                    }

                    HtmlTableRow tr = (HtmlTableRow)lstR.Items[i].FindControl("Tr1");
                    lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L2.Text)) + (decimal.Parse(L2x.Text)) + (decimal.Parse(L3.Text)) + (decimal.Parse(L3x.Text)) + (decimal.Parse(L4.Text)) + (decimal.Parse(L4x.Text)) + (decimal.Parse(L5.Text)) + (decimal.Parse(L5x.Text)) + (decimal.Parse(L6.Text)) + (decimal.Parse(L6x.Text))).ToString("N0");
                    lblFrmula.Text = ((decimal.Parse(L1.Text)) * (decimal.Parse(frmula1.Text)) + (decimal.Parse(L1x.Text)) * (decimal.Parse(frmula1a.Text)) + (decimal.Parse(L2.Text)) * (decimal.Parse(frmula2.Text)) + (decimal.Parse(L2x.Text)) * (decimal.Parse(frmula2a.Text)) + (decimal.Parse(L3.Text)) * (decimal.Parse(frmula3.Text)) + (decimal.Parse(L3x.Text)) * (decimal.Parse(frmula3a.Text)) + (decimal.Parse(L4.Text)) * (decimal.Parse(frmula4.Text)) + (decimal.Parse(L4x.Text)) * (decimal.Parse(frmula4a.Text)) + (decimal.Parse(L5.Text)) * (decimal.Parse(frmula5.Text)) + (decimal.Parse(L5x.Text)) * (decimal.Parse(frmula5a.Text)) + (decimal.Parse(L6.Text)) * (decimal.Parse(frmula6.Text)) + (decimal.Parse(L6x.Text)) * (decimal.Parse(frmula6a.Text))).ToString("N0");
                    lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                    try
                    {
                        lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    //lblKsEfis.Text = ((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKuEfs.Text)) + (decimal.Parse(lblKgrade2Eff.Text)) + (decimal.Parse(lblKgrade3Eff.Text)))).ToString("N1");


                    decimal satukomasatu;
                    satukomasatu = 1.10m;
                    lblKvKg2.Text = (decimal.Parse(lblKvKg.Text) * (satukomasatu)).ToString("N0");


                    PbahanBakuFacade pBahanF = new PbahanBakuFacade();
                    PbahanBakuFacade pBahanF0 = new PbahanBakuFacade();
                    PbahanBakuFacade pBahanF0a = new PbahanBakuFacade();
                    PbahanBakuFacade pBahanF1 = new PbahanBakuFacade();
                    PbahanBakuFacade pBahanF2 = new PbahanBakuFacade();
                    PbahanBakuFacade pBahanF3 = new PbahanBakuFacade();
                    PbahanBakuFacade pBahanF4 = new PbahanBakuFacade();
                    PbahanBakuFacade pBahanAll = new PbahanBakuFacade();
                    DateTime vTgl = DateTime.Parse(lbltgl.Text);
                    //Kertas PulpVirgin
                    lblKvKg.Text = pBahanF.Retrievexx0(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //Kertas Semen Bima
                    lblKBimaBK.Text = pBahanF0.Retrievexx01(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //Sampah Bima
                    lblKBimaSampah.Text = pBahanF0a.Retrievexx0a(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //bersih Bima
                    lblKBimaBB.Text = ((decimal.Parse(lblKBimaBK.Text)) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                    //Kertas Semen0
                    lblKsKg0.Text = pBahanF1.Retrievexx02(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //Kertas Semen
                    lblKsKg.Text = ((decimal.Parse(lblKBimaBB.Text)) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                    //Kertas Grade Utama
                    lblKuKg.Text = pBahanF2.Retrievexx03(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //Kertas Grade Utama
                    //lblKgrade2Kg.Text = pBahanF2.Retrievexx03(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //Kertas Grade2
                    lblKgrade2Kg.Text = pBahanF3.Retrievexx04(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //Kertas Grade3
                    lblKgrade3Kg.Text = pBahanF4.Retrievexx05(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //All Pemakaian Jenis Kertas
                    //lblKL.Text = pBahanAll.RetrievexxAllKertas(vTgl.ToString("yyyyMMdd")).ToString("N0");

                    //string Standare = lblKvKg.Text + "," +
                    //                  lblKsKg.Text;
                }
                else
                {
                    hd.Value = "0";
                    pbx2.ID = 0;
                }

                arrData.Add(pbx2);
            }
            Session["Nilai"] = arrData;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session.Remove("ListBBPemakaian");
            btnSimpan.Enabled = false;
            //lbHitung.Enabled = false;
            clearForm();
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            GetValue();
            int intResult = 0;
            string strEvent = "Insert";
            PbahanBaku pbb2 = new PbahanBaku();
            PbahanBakuFacade pbb2x = new PbahanBakuFacade();
            pbb2x = new PbahanBakuFacade();
            if (ViewState["id"] != null)
            {
                pbb2.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            if (Session["ListBBPemakaian"] == null)
            {
                DisplayAJAXMessage(this, "Form Isian Masih Kosong");
                return;
            }
            ArrayList arrData = (ArrayList)Session["ListBBPemakaian"];
            for (int i = 0; i < lstR.Items.Count; i++)
            {
                intResult = 0;
                HtmlTableRow tr = (HtmlTableRow)lstR.Items[i].FindControl("lstR");
                Label lbl = (Label)lstR.Items[i].FindControl("lbltglProd");
                TextBox txtL1 = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1");
                TextBox txtL1x = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1x");
                TextBox txtL2 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2");
                TextBox txtL2x = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x");
                TextBox txtL3 = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3");
                TextBox txtL3x = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3x");
                TextBox txtL4 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4");
                TextBox txtL4x = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x");
                TextBox txtL5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                TextBox txtL5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                TextBox txtL6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                TextBox txtL6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                Label lbltMix = (Label)lstR.Items[i].FindControl("tMix");
                Label lblFrmula = (Label)lstR.Items[i].FindControl("Frmula");
                TextBox txtSdL = (TextBox)lstR.Items[i].FindControl("txtSdL");
                TextBox txtSspB = (TextBox)lstR.Items[i].FindControl("txtSspB");
                TextBox txtAT = (TextBox)lstR.Items[i].FindControl("txtAT");
                Label txtKL = (Label)lstR.Items[i].FindControl("txtKL");
                Label lblEfis = (Label)lstR.Items[i].FindControl("txtEfis");
                TextBox txtJenisKertasVirgin = (TextBox)lstR.Items[i].FindControl("txtJenisKertasVirgin");
                Label lblKvKg = (Label)lstR.Items[i].FindControl("txtKvKg");
                Label lblKvKg2 = (Label)lstR.Items[i].FindControl("txtKvKg2");
                Label lblKvEfis = (Label)lstR.Items[i].FindControl("txtKvEfis");
                Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                Label lblKsKg0 = (Label)lstR.Items[i].FindControl("txtKsKg0");
                Label lblKsKg = (Label)lstR.Items[i].FindControl("txtKsKg");
                Label lblKsEfis = (Label)lstR.Items[i].FindControl("txtKsEfis");
                Label lblKuKg = (Label)lstR.Items[i].FindControl("txtKuKg");
                Label lblKuEfs = (Label)lstR.Items[i].FindControl("txtKuEfs");
                Label lblKgrade2Kg = (Label)lstR.Items[i].FindControl("txtKgrade2Kg");
                Label lblKgrade2Eff = (Label)lstR.Items[i].FindControl("txtKgrade2Eff");
                Label lblKgrade3Kg = (Label)lstR.Items[i].FindControl("txtKgrade3Kg");
                Label lblKgrade3Eff = (Label)lstR.Items[i].FindControl("txtKgrade3Eff");
                Label lblTEfis = (Label)lstR.Items[i].FindControl("txtTEfis");
                TextBox txtKV = (TextBox)lstR.Items[i].FindControl("txtKV");
                TextBox txtKB = (TextBox)lstR.Items[i].FindControl("txtKB");
                TextBox txtKS = (TextBox)lstR.Items[i].FindControl("txtKS");
                TextBox txtKGradeUtama = (TextBox)lstR.Items[i].FindControl("txtKGradeUtama");
                TextBox txtKgrade2 = (TextBox)lstR.Items[i].FindControl("txtKgrade2");
                TextBox txtKgrade3 = (TextBox)lstR.Items[i].FindControl("txtKgrade3");

                //convert to string
                string L1G1 = txtL1.Text;
                string L1G1x = txtL1x.Text;
                string L2G2 = txtL2.Text;
                string L2G2x = txtL2x.Text;
                string L3G3 = txtL3.Text;
                string L3G3x = txtL3x.Text;
                string L4G4 = txtL4.Text;
                string L4G4x = txtL4x.Text;
                string L5G5 = txtL5.Text;
                string L5G5x = txtL5x.Text;
                string L6G6 = txtL6.Text;
                string L6G6x = txtL6x.Text;
                string tMix = lbltMix.Text;
                string Frmula = lblFrmula.Text;
                string SdL = txtSdL.Text;
                string SspB = txtSspB.Text;
                string AT = txtAT.Text;
                string KL = txtKL.Text;
                string Efis = lblEfis.Text;
                string JenisKertasVirgin = txtJenisKertasVirgin.Text;
                string KvKg = lblKvKg.Text;
                string KvKg2 = lblKvKg2.Text;
                string KvEfis = lblKvEfis.Text;
                string KBimaBK = lblKBimaBK.Text;
                string KBimaSampah = lblKBimaSampah.Text;
                string KBimaBB = lblKBimaBB.Text;
                string KsKg0 = lblKsKg0.Text;
                string KsKg = lblKsKg.Text;
                string KsEfis = lblKsEfis.Text;
                string KuKg = lblKuKg.Text;
                string KuEfs = lblKuEfs.Text;
                string Kgrade2Kg = lblKgrade2Kg.Text;
                string Kgrade2Eff = lblKgrade2Eff.Text;
                string Kgrade3Kg = lblKgrade3Kg.Text;
                string Kgrade3Eff = lblKgrade3Eff.Text;
                string TEfis = lblTEfis.Text;
                string KV = txtKV.Text;
                string KB = txtKB.Text;
                string KS = txtKS.Text;
                string KGradeUtama = txtKGradeUtama.Text;
                string Kgrade2 = txtKgrade2.Text;
                string Kgrade3 = txtKgrade3.Text;

                pbb2.Tgl_Prod = Convert.ToDateTime(lbl.Text);
                pbb2.Tgl_Prod2 = (lbl.Text).ToString();
                pbb2.L1 = Convert.ToDecimal(txtL1.Text);
                pbb2.L1x = Convert.ToDecimal(txtL1x.Text);
                pbb2.L2 = Convert.ToDecimal(txtL2.Text);
                pbb2.L2x = Convert.ToDecimal(txtL2x.Text);
                pbb2.L3 = Convert.ToDecimal(txtL3.Text);
                pbb2.L3x = Convert.ToDecimal(txtL3x.Text);
                pbb2.L4 = Convert.ToDecimal(txtL4.Text);
                pbb2.L4x = Convert.ToDecimal(txtL4x.Text);
                pbb2.L5 = Convert.ToDecimal(txtL5.Text);
                pbb2.L5x = Convert.ToDecimal(txtL5x.Text);
                pbb2.L6 = Convert.ToDecimal(txtL6.Text);
                pbb2.L6x = Convert.ToDecimal(txtL6x.Text);
                pbb2.TMix = Convert.ToDecimal(lbltMix.Text);
                pbb2.Formula = Convert.ToDecimal(lblFrmula.Text);
                pbb2.SdL = Convert.ToDecimal(txtSdL.Text);
                pbb2.SspB = Convert.ToDecimal(txtSspB.Text);
                pbb2.AT = Convert.ToDecimal(txtAT.Text);
                pbb2.KL = Convert.ToDecimal(txtKL.Text);
                pbb2.Efis = Convert.ToDecimal(lblEfis.Text);
                pbb2.JKertasVirgin = (txtJenisKertasVirgin.Text).ToString();
                pbb2.KvKg = Convert.ToDecimal(lblKvKg.Text);
                pbb2.KvKg2 = Convert.ToDecimal(lblKvKg2.Text);
                pbb2.KvEfis = Convert.ToDecimal(lblKvEfis.Text);
                pbb2.BkBimaKg = Convert.ToDecimal(lblKBimaBK.Text);
                pbb2.SampahBima = Convert.ToDecimal(lblKBimaSampah.Text);
                pbb2.BbBimaKg = Convert.ToDecimal(lblKBimaBB.Text);
                pbb2.KsKg0 = Convert.ToDecimal(lblKsKg0.Text);
                pbb2.KsKg = Convert.ToDecimal(lblKsKg.Text);
                pbb2.KsEfis = Convert.ToDecimal(lblKsEfis.Text);
                pbb2.KuKg = Convert.ToDecimal(lblKuKg.Text);
                pbb2.KuEfs = Convert.ToDecimal(lblKuEfs.Text);
                pbb2.Kgrade2Kg = Convert.ToDecimal(lblKgrade2Kg.Text);
                pbb2.Kgrade2Eff = Convert.ToDecimal(lblKgrade2Eff.Text);
                pbb2.Kgrade3Kg = Convert.ToDecimal(lblKgrade3Kg.Text);
                pbb2.Kgrade3Eff = Convert.ToDecimal(lblKgrade3Eff.Text);
                pbb2.TEfis = Convert.ToDecimal(lblTEfis.Text);
                pbb2.KV = Convert.ToDecimal(txtKV.Text);
                pbb2.KB = Convert.ToDecimal(txtKB.Text);
                pbb2.KS = Convert.ToDecimal(txtKS.Text);
                pbb2.KGradeUtama = Convert.ToDecimal(txtKGradeUtama.Text);
                pbb2.Kgrade2 = Convert.ToDecimal(txtKgrade2.Text);
                pbb2.Kgrade3 = Convert.ToDecimal(txtKgrade3.Text);
                pbb2.CreatedBy = ((Users)Session["Users"]).UserName;
                pbb2.LastModifiedBy = ((Users)Session["Users"]).UserName;
                if (pbb2.ID > 0)
                {
                    intResult = pbb2x.Update(pbb2);
                    InsertLog(strEvent);
                }
                else
                {
                    intResult = pbb2x.Insert(pbb2);
                    if (intResult > 0)
                    {
                        DisplayAJAXMessage(this, "Data Telah Disimpan");
                        InsertLog(strEvent);
                        btnSimpan.Enabled = false;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Simpan");
                    }
                }
            }
        }

        private void GetValue()
        {
            for (int i = 0; i < lstR.Items.Count; i++)
            {

                TextBox L1 = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1");
                TextBox L2 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2");
                TextBox L3 = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3");
                TextBox L4 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4");
                TextBox L5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                TextBox L6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                string Line1 = L1.Text;
            }

        }

        private void clearForm()
        {
            ViewState["id"] = null;
            Session.Remove("id");
            Session["ListBBPemakaian"] = null;
            ArrayList arrData = new ArrayList();
            lstR.DataSource = arrData;
            lstR.DataBind();
            txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "BahanBakuPulp";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtTanggal.Text;
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


        protected void lstR_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstR_Databound(object sender, RepeaterItemEventArgs e)
        {

        }
    }
}