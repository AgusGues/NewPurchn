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
using System.Text;
using System.Xml.Linq;
using System.Net;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GRCweb1.Modul.SarMut
{
    public partial class BM_Pulp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];

                btnLihat.Visible = false; btnNew.Visible = false; btnPreview.Visible = true;
                btnSimpan.Visible = false; btnCancel.Visible = false;
                LoadBulan();
                LoadTahun();
                Session["Nilai"] = null;
                #region Line
                line1.Text = "Line 1";
                line1a.Text = "Line 1";
                line2.Text = "Line 2";
                line2a.Text = "Line 2";
                line3.Text = "Line 3";
                line3a.Text = "Line 3";
                line4.Text = "Line 4";
                line4a.Text = "Line 4";
                line5.Text = "Line 5";
                line5a.Text = "Line 5";
                line6.Text = "Line 6";
                line6a.Text = "Line 6";
                LineC1.Text = "Line 1";
                LineC2.Text = "Line 2";
                LineC3.Text = "Line 3";
                LineC4.Text = "Line 4";
                LineC12.Text = "Line 1";
                LineC21.Text = "Line 2";
                LineC31.Text = "Line 3";
                LineC41.Text = "Line 4";
                #endregion
                #region Header Line
                if (user.UnitKerjaID == 7)
                {
                    #region Krwg
                    lblF1.Text = "G185G 189";
                    lblF1a.Text = "G185G 189";
                    lblF2.Text = "G206";
                    lblF2a.Text = "G206";
                    lblF3.Text = "G185G 189";
                    lblF3a.Text = "G185G 189";
                    lblF4.Text = "C007";
                    lblF4a.Text = "C007";
                    lblF5.Text = "G206/207";
                    lblF5a.Text = "G206/207";
                    lblF6.Text = "G185G 189";
                    lblF6a.Text = "G185G 189";
                    lblF7.Text = "G206";
                    lblF7a.Text = "G206";
                    lblF8.Text = "G185/G201";
                    lblF8a.Text = "G185/G201";
                    lblF9.Text = "C007";
                    lblF9a.Text = "C007";
                    lblF10.Text = "G206/G207";
                    lblF10a.Text = "G206/G207";
                    lblF11.Text = "G185";
                    lblF11a.Text = "G185";
                    lblF12.Text = "C007";
                    lblF12a.Text = "C007";
                    lblF13.Text = "G206";
                    lblF13a.Text = "G206";
                    lblF14.Text = "C007";
                    lblF14a.Text = "C007";
                    lblF15.Text = "G206/G207";
                    lblF16.Text = "G211/P004";
                    lblF15a.Text = "G206/G207";
                    lblF16a.Text = "G 211/P 004";
                    frmula1.Text = (90.0).ToString("N0");
                    frmula1x.Text = (90.0).ToString("N0");
                    frmula1a.Text = (79.0).ToString("N0");
                    frmula1ax.Text = (79.0).ToString("N0");
                    frmula2.Text = (90.0).ToString("N0");
                    frmula2x.Text = (90.0).ToString("N0");
                    frmula2a.Text = (85.0).ToString("N0");
                    frmula2ax.Text = (85.0).ToString("N0");
                    frmula2b.Text = (79.0).ToString("N0");
                    frmula2bx.Text = (79.0).ToString("N0");
                    frmula3.Text = (90.0).ToString("N0");
                    frmula3x.Text = (90.0).ToString("N0");
                    frmula3a.Text = (79.0).ToString("N0");
                    frmula3ax.Text = (79.0).ToString("N0");
                    frmula3a1.Text = (90.0).ToString("N0");
                    frmula3a1x.Text = (90.0).ToString("N0");
                    frmula4.Text = (85.0).ToString("N0");
                    frmula4x.Text = (85.0).ToString("N0");
                    frmula4a.Text = (79.0).ToString("N0");
                    frmula4ax.Text = (79.0).ToString("N0");
                    frmula4a1.Text = (90.0).ToString("N0");
                    frmula4a1x.Text = (90.0).ToString("N0");
                    frmula5.Text = (85.0).ToString("N0");
                    frmula5x.Text = (85.0).ToString("N0");
                    frmula5a.Text = (79.0).ToString("N0");
                    frmula5ax.Text = (79.0).ToString("N0");
                    frmula6.Text = (85.0).ToString("N0");
                    frmula6x.Text = (85.0).ToString("N0");
                    frmula6a.Text = (79.0).ToString("N0");
                    frmula6a1.Text = (90.0).ToString("N0");
                    frmula6ax.Text = (79.0).ToString("N0");
                    frmula6ax1.Text = (90.0).ToString("N0");
                    PersenKBima.Text = (3.5).ToString("N1");
                    PersenKBima0.Text = (3.5).ToString("N1");
                    txtPerKraftSmpahPlastik.Text = (14.3).ToString("N1");
                    txtPerKraftSmpahPlastik0.Text = (14.3).ToString("N1");
                    #endregion
                }
                else if (user.UnitKerjaID == 1)
                {
                    #region ctrp & jombang
                    lblfc1.Text = "G185/G211";
                    lblfc1a.Text = "G206";
                    lblfc1a0.Text = "G207";
                    lblfc1a01.Text = "P003";
                    lblfc2.Text = "G185/G211";
                    lblfc2a.Text = "G206";
                    lblfc3.Text = "G185/G211";
                    lblfc3a.Text = "G206";
                    lblfc4.Text = "G185/G211";
                    lblfc4a.Text = "G206";
                    lblfc4a1.Text = "G207";

                    lblfc1z.Text = "G185/G211";
                    lblfc1za.Text = "G206";
                    lblfc1zb.Text = "G207";
                    lblfc1zc.Text = "P003";
                    lblfc2z.Text = "G185";
                    lblfc2za.Text = "G206";

                    lblfc3z.Text = "G185/G211";
                    lblfc3za.Text = "G206";
                    lblfc4z.Text = "G185/G211";
                    lblfc4za.Text = "G206";
                    lblfc4za1.Text = "G207";
                    lblfc1x.Text = (75.0).ToString("N0");
                    lblfc1ax.Text = (66.0).ToString("N0");
                    lblfc1ax0.Text = (66.0).ToString("N0");
                    lblfc1zf1b.Text = (66.0).ToString("N0");
                    lblfc1zf1c.Text = (84.0).ToString("N0");
                    lblfc1ax01.Text = (84.0).ToString("N0");
                    lblfc2x.Text = (90.0).ToString("N0");
                    lblfc2ax.Text = (80.0).ToString("N0");
                    lblfc3x.Text = (90.0).ToString("N0");
                    lblfc3ax.Text = (80.0).ToString("N0");
                    lblfc4x.Text = (90.0).ToString("N0");
                    lblfc4ax.Text = (80.0).ToString("N0");
                    lblfc4ax1.Text = (66.0).ToString("N0");
                    lblfc1zf.Text = (75.0).ToString("N0");
                    lblfc1zf1.Text = (66.0).ToString("N0");
                    lblfc2zf.Text = (90.0).ToString("N0");
                    lblfc2zf1.Text = (80.0).ToString("N0");
                    lblfc3zf.Text = (90.0).ToString("N0");
                    lblfc3zaf1.Text = (80.0).ToString("N0");
                    lblfc4zf.Text = (90.0).ToString("N0");
                    lblfc4zaf1.Text = (80.0).ToString("N0");
                    lblfc4zaf12.Text = (90.0).ToString("N0");
                    #endregion
                }
                else
                {
                    #region jombang
                    //lblfc1.Text = "G208 8,33%";
                    //lblfc1a.Text = "G212";
                    //lblfc1a0.Text = "G212";
                    //lblfc1a01.Text = "G208 Up";
                    //lblfc1x.Text = (97.5).ToString("N1");
                    //lblfc1ax.Text = (97.5).ToString("N1");
                    //lblfc1ax0.Text = (90.0).ToString("N0");
                    //lblfc1ax01.Text = (90.0).ToString("N0");

                    lblfc1.Text = "G211 ui";
                    lblfc1a.Text = "G211 ui";
                    lblfc1a0.Text = "G215 ul";
                    lblfc1a01.Text = "P004 ul";
                    lblfc1x.Text = (90).ToString("N0");
                    lblfc1ax.Text = (97.5).ToString("N1");
                    lblfc1ax0.Text = (97.5).ToString("N1");
                    lblfc1ax01.Text = (97.5).ToString("N1");

                    //lblfc1zf1b.Text = (66.0).ToString("N0");
                    //lblfc1zf1c.Text = (84.0).ToString("N0");
                    //lblfc2x.Text = (90.0).ToString("N0");
                    //lblfc2ax.Text = (80.0).ToString("N0");
                    //lblfc3x.Text = (90.0).ToString("N0");
                    //lblfc3ax.Text = (80.0).ToString("N0");
                    //lblfc4x.Text = (90.0).ToString("N0");
                    //lblfc4ax.Text = (80.0).ToString("N0");
                    //lblfc4ax1.Text = (66.0).ToString("N0");
                    //lblfc1zf1b.Text = (66.0).ToString("N0");
                    //lblfc1zf1c.Text = (84.0).ToString("N0");
                    //lblfc2x.Text = (90.0).ToString("N0");
                    //lblfc2ax.Text = (80.0).ToString("N0");
                    //lblfc3x.Text = (90.0).ToString("N0");
                    //lblfc3ax.Text = (80.0).ToString("N0");
                    //lblfc4x.Text = (90.0).ToString("N0");
                    //lblfc4ax.Text = (80.0).ToString("N0");
                    //lblfc4ax1.Text = (66.0).ToString("N0");
                    //lblfc1zf.Text = (97.5).ToString("N1");
                    //lblfc1zf1.Text = (97.5).ToString("N1");
                    //lblfc1zf1b.Text = (90.0).ToString("N0");
                    //lblfc1zf1c.Text = (90.0).ToString("N0");
                    //lblfc2zf.Text = (90.0).ToString("N0");
                    //lblfc2zf1.Text = (80.0).ToString("N0");
                    //lblfc3zf.Text = (90.0).ToString("N0");
                    //lblfc3zaf1.Text = (80.0).ToString("N0");
                    //lblfc4zf.Text = (90.0).ToString("N0");
                    //lblfc4zaf1.Text = (80.0).ToString("N0");
                    //lblfc4zaf12.Text = (66.0).ToString("N0");
                    //lblfc1z.Text = "G208 8,33%";
                    //lblfc1za.Text = "G212";
                    //lblfc1zb.Text = "G212";
                    //lblfc1zc.Text = "G208 Up";
                    lblfc1z.Text = "G211 ui";
                    lblfc1za.Text = "G211 ui";
                    lblfc1zb.Text = "G215 ul";
                    lblfc1zc.Text = "P00 ul";
                    lblfc1zf.Text = (90).ToString("N0");
                    lblfc1zf1.Text = (97.5).ToString("N1");
                    lblfc1zf1b.Text = (97.5).ToString("N1");
                    lblfc1zf1c.Text = (97.5).ToString("N1");
                    #endregion
                }
                #region hide
                //else
                //{
                //    lblF1.Text = "G208";
                //    lblF2.Text = "G208";
                //    frmula1.Text = (90.0).ToString("N0");
                //    frmula1a.Text = (90.0).ToString("N0");
                //}
                #endregion

                #endregion
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(btnExport);
        }

        private void LoadTahun()
        {
            PakaiFacade pd = new PakaiFacade();
            pd.GetTahun(ddlTahun);
            ddlTahun.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void LoadBulan()
        {
            ddlBulan.Items.Clear();
            //ddlBulan.Items.Add(new ListItem("--Pilih Bulan--", "0"));
            for (int i = 1; i < 13; i++)
            {
                ddlBulan.Items.Add(new ListItem(Global.nBulan(i).ToString(), i.ToString()));
            }
            ddlBulan.SelectedValue = DateTime.Now.Month.ToString();
        }

        protected void lstR_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void lstR_Databound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void C1_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        protected void C1_Databound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void C2_Command(object sender, RepeaterCommandEventArgs e)
        {
            #region Proses
            int RowNum = e.Item.ItemIndex;
            Image Simpan = (Image)C2.Items[RowNum].FindControl("simpan");
            Image img = (Image)C2.Items[RowNum].FindControl("edit");
            TextBox txtjmlL1G1 = (TextBox)C2.Items[RowNum].FindControl("txtjmlL1G1");
            TextBox txtjmlL1G1x = (TextBox)C2.Items[RowNum].FindControl("txtjmlL1G1x");
            TextBox txtjmlL1G1x1 = (TextBox)C2.Items[RowNum].FindControl("txtjmlL1G1x1");
            TextBox txtjmlL1G1x2 = (TextBox)C2.Items[RowNum].FindControl("txtjmlL1G1x12");
            TextBox txtjmlL2G2 = (TextBox)C2.Items[RowNum].FindControl("txtjmlL2G2");
            TextBox txtjmlL2G2x = (TextBox)C2.Items[RowNum].FindControl("txtjmlL2G2x");
            TextBox txtjmlL3G3 = (TextBox)C2.Items[RowNum].FindControl("txtjmlL3G3");
            TextBox txtjmlL3G3x = (TextBox)C2.Items[RowNum].FindControl("txtjmlL3G3x");
            TextBox txtjmlL4G4 = (TextBox)C2.Items[RowNum].FindControl("txtjmlL4G4");
            TextBox txtjmlL4G4x = (TextBox)C2.Items[RowNum].FindControl("txtjmlL4G4x");
            TextBox txtjmlL4G4x1 = (TextBox)C2.Items[RowNum].FindControl("txtjmlL4G4x1");
            TextBox txtSdL = (TextBox)C2.Items[RowNum].FindControl("txtSdL");
            TextBox txtSspB = (TextBox)C2.Items[RowNum].FindControl("txtSspB");
            TextBox txtAT = (TextBox)C2.Items[RowNum].FindControl("txtAT");
            TextBox txtJenisKertasVirgin = (TextBox)C2.Items[RowNum].FindControl("txtJenisKertasVirgin");
            TextBox txtKV = (TextBox)C2.Items[RowNum].FindControl("txtKV");
            TextBox txtKS = (TextBox)C2.Items[RowNum].FindControl("txtKS");
            TextBox txtKGradeUtama = (TextBox)C2.Items[RowNum].FindControl("txtKGradeUtama");
            TextBox txtKgrade2 = (TextBox)C2.Items[RowNum].FindControl("txtKgrade2");
            TextBox txtKgrade3 = (TextBox)C2.Items[RowNum].FindControl("txtKgrade3");
            Label tMix = (Label)C2.Items[RowNum].FindControl("tMix");
            Label Frmula = (Label)C2.Items[RowNum].FindControl("Frmula");
            Label txtKL = (Label)C2.Items[RowNum].FindControl("txtKL");
            Label txtEfis = (Label)C2.Items[RowNum].FindControl("txtEfis");
            Label txtTEfis = (Label)C2.Items[RowNum].FindControl("txtTEfis");
            Label txtKsKg = (Label)C2.Items[RowNum].FindControl("txtKsKg");
            Label txtKsEfis = (Label)C2.Items[RowNum].FindControl("txtKsEfis");
            Label txtKvEfis = (Label)C2.Items[RowNum].FindControl("txtKvEfis");
            Label txtKgrade2Eff = (Label)C2.Items[RowNum].FindControl("txtKgrade2Eff");
            Label txtKuEfs = (Label)C2.Items[RowNum].FindControl("txtKuEfs");
            //Label txtKgKraftPlastikBrsh = (Label)lstR2.Items[RowNum].FindControl("txtKgKraftPlastikBrsh");
            Label txtKgrade3Eff = (Label)C2.Items[RowNum].FindControl("txtKgrade3Eff");
            Label txtKvKg2 = (Label)C2.Items[RowNum].FindControl("txtKvKg2");
            Label TglProd = (Label)C2.Items[RowNum].FindControl("lbltglProd");
            Label txtKvKg = (Label)C2.Items[RowNum].FindControl("txtKvKg");
            //TextBox txtKBimaBK = (TextBox)lstR2.Items[RowNum].FindControl("txtKBimaBK");
            //TextBox txtKsKg0 = (TextBox)lstR2.Items[RowNum].FindControl("txtKsKg0");
            Label txtKuKg = (Label)C2.Items[RowNum].FindControl("txtKuKg");
            Label txtKgrade2Kg = (Label)C2.Items[RowNum].FindControl("txtKgrade2Kg");
            //TextBox txtKgKraftPlastikktr = (TextBox)lstR2.Items[RowNum].FindControl("txtKgKraftPlastikktr");
            Label txtKgrade3Kg = (Label)C2.Items[RowNum].FindControl("txtKgrade3Kg");

            Label lbl1 = (Label)C2.Items[RowNum].FindControl("txtL1G");
            Label lbl1x = (Label)C2.Items[RowNum].FindControl("txtL1Gx");


            switch (e.CommandName)
            {
                case "Edit":
                    txtjmlL1G1.Visible = true;
                    txtjmlL1G1.ReadOnly = false;
                    txtjmlL1G1x.Visible = true;
                    txtjmlL1G1x.ReadOnly = false;
                    txtjmlL1G1x1.Visible = true;
                    txtjmlL1G1x1.ReadOnly = false;
                    txtjmlL1G1x2.Visible = true;
                    txtjmlL1G1x2.ReadOnly = false;
                    txtjmlL2G2.Visible = true;
                    txtjmlL2G2.ReadOnly = false;
                    txtjmlL2G2x.Visible = true;
                    txtjmlL2G2x.ReadOnly = false;
                    txtjmlL3G3.Visible = true;
                    txtjmlL3G3.ReadOnly = false;
                    txtjmlL3G3x.Visible = true;
                    txtjmlL3G3x.ReadOnly = false;
                    txtjmlL4G4.Visible = true;
                    txtjmlL4G4.ReadOnly = false;
                    txtjmlL4G4x.Visible = true;
                    txtjmlL4G4x.ReadOnly = false;
                    txtjmlL4G4x1.Visible = true;
                    txtjmlL4G4x1.ReadOnly = false;
                    txtSdL.Visible = true;
                    txtSdL.ReadOnly = false;
                    txtSspB.Visible = true;
                    txtSspB.ReadOnly = false;
                    txtAT.Visible = true;
                    txtAT.ReadOnly = false;
                    txtJenisKertasVirgin.Visible = true;
                    txtJenisKertasVirgin.ReadOnly = false;
                    txtKV.Visible = true;
                    txtKV.ReadOnly = false;
                    txtKS.Visible = true;
                    txtKS.ReadOnly = false;
                    txtKGradeUtama.Visible = true;
                    txtKGradeUtama.ReadOnly = false;
                    txtKgrade2.Visible = true;
                    txtKgrade2.ReadOnly = false;
                    txtKgrade3.Visible = true;
                    txtKgrade3.ReadOnly = false;
                    //txtKvKg.Visible = true;
                    //txtKvKg.ReadOnly = false;
                    //txtKBimaBK.Visible = true;
                    //txtKBimaBK.ReadOnly = false;
                    //txtKsKg0.Visible = true;
                    //txtKsKg0.ReadOnly = false;
                    //txtKuKg.Visible = true;
                    //txtKuKg.ReadOnly = false;
                    //txtKgrade2Kg.Visible = true;
                    //txtKgrade2Kg.ReadOnly = false;
                    //txtKgKraftPlastikktr.Visible = true;
                    //txtKgKraftPlastikktr.ReadOnly = false;
                    //txtKgrade3Kg.Visible = true;
                    //txtKgrade3Kg.ReadOnly = false;
                    lbl1.Visible = false;
                    lbl1x.Visible = false;
                    Simpan.Visible = true;
                    btnCalc.Visible = true;
                    img.Visible = false;
                    break;
                case "refreshx":
                    PakaiBahanBaku pBahanBx = new PakaiBahanBaku();
                    PakaiBahanBakuFacade pBahanFx = new PakaiBahanBakuFacade();
                    DateTime vTgl = DateTime.Parse(TglProd.Text);
                    txtKvKg.Text = pBahanFx.Retrievexx0(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //txtKBimaBK.Text = pBahanFx.Retrievexx01(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKsKg.Text = pBahanFx.Retrievexx02(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKuKg.Text = pBahanFx.Retrievexx03(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKgrade2Kg.Text = pBahanFx.Retrievexx04(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKgrade3Kg.Text = pBahanFx.Retrievexx05(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    //txtKgKraftPlastikktr.Text = pBahanFx.Retrievexx06(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    break;
                case "Save":
                    txtjmlL1G1.Visible = true;
                    txtjmlL1G1.ReadOnly = true;
                    txtjmlL1G1x.Visible = true;
                    txtjmlL1G1x.ReadOnly = true;
                    txtjmlL1G1x1.Visible = true;
                    txtjmlL1G1x1.ReadOnly = true;
                    txtjmlL1G1x2.Visible = true;
                    txtjmlL1G1x2.ReadOnly = true;
                    txtjmlL2G2.Visible = true;
                    txtjmlL2G2.ReadOnly = true;
                    txtjmlL2G2x.Visible = true;
                    txtjmlL2G2x.ReadOnly = true;
                    txtjmlL3G3.Visible = true;
                    txtjmlL3G3.ReadOnly = true;
                    txtjmlL3G3x.Visible = true;
                    txtjmlL3G3x.ReadOnly = true;
                    txtjmlL4G4.Visible = true;
                    txtjmlL4G4.ReadOnly = true;
                    txtjmlL4G4x.Visible = true;
                    txtjmlL4G4x.ReadOnly = true;
                    txtjmlL4G4x1.Visible = true;
                    txtjmlL4G4x1.ReadOnly = true;
                    txtSdL.Visible = true;
                    txtSdL.ReadOnly = true;
                    txtSspB.Visible = true;
                    txtSspB.ReadOnly = true;
                    txtAT.Visible = true;
                    txtAT.ReadOnly = true;
                    txtJenisKertasVirgin.Visible = true;
                    txtJenisKertasVirgin.ReadOnly = true;
                    txtKV.Visible = true;
                    txtKV.ReadOnly = true;
                    txtKS.Visible = true;
                    txtKS.ReadOnly = true;
                    txtKGradeUtama.Visible = true;
                    txtKGradeUtama.ReadOnly = true;
                    txtKgrade2.Visible = true;
                    txtKgrade2.ReadOnly = true;
                    txtKgrade3.Visible = true;
                    txtKgrade3.ReadOnly = true;
                    //txtKvKg.Visible = true;
                    //txtKvKg.ReadOnly = true;
                    //txtKBimaBK.Visible = true;
                    //txtKBimaBK.ReadOnly = true;
                    //txtKsKg0.Visible = true;
                    //txtKsKg0.ReadOnly = true;
                    //txtKuKg.Visible = true;
                    //txtKuKg.ReadOnly = true;
                    //txtKgrade2Kg.Visible = true;
                    //txtKgrade2Kg.ReadOnly = true;
                    //txtKgKraftPlastikktr.Visible = true;
                    //txtKgKraftPlastikktr.ReadOnly = true;
                    //txtKgrade3Kg.Visible = true;
                    //txtKgrade3Kg.ReadOnly = true;
                    lbl1.Visible = true;
                    lbl1x.Visible = true;
                    Simpan.Visible = false;
                    btnCalc.Visible = false;
                    img.Visible = true;

                    int Result = 0;
                    Users user = (Users)Session["Users"];
                    PakaiBahanBaku pBahanB = new PakaiBahanBaku();
                    string strError = string.Empty;
                    string UserID = user.ID.ToString();

                    pBahanB.ID = int.Parse(txtjmlL1G1.ToolTip.ToString());
                    pBahanB.L1 = decimal.Parse(txtjmlL1G1.Text);
                    pBahanB.L1x = decimal.Parse(txtjmlL1G1x.Text);
                    pBahanB.L1x1 = decimal.Parse(txtjmlL1G1x1.Text);
                    pBahanB.L1x2 = decimal.Parse(txtjmlL1G1x2.Text);
                    pBahanB.L2 = decimal.Parse(txtjmlL2G2.Text);
                    pBahanB.L2x = decimal.Parse(txtjmlL2G2x.Text);
                    pBahanB.L3 = decimal.Parse(txtjmlL3G3.Text);
                    pBahanB.L3x = decimal.Parse(txtjmlL3G3x.Text);
                    pBahanB.L4 = decimal.Parse(txtjmlL4G4.Text);
                    pBahanB.L4x = decimal.Parse(txtjmlL4G4x.Text);
                    pBahanB.L4x1 = decimal.Parse(txtjmlL4G4x1.Text);
                    pBahanB.SdL = decimal.Parse(txtSdL.Text);
                    pBahanB.SspB = decimal.Parse(txtSspB.Text);
                    pBahanB.AT = decimal.Parse(txtAT.Text);
                    pBahanB.JKertasVirgin = txtJenisKertasVirgin.Text;
                    pBahanB.Kv = decimal.Parse(txtKV.Text);
                    pBahanB.Ks = decimal.Parse(txtKS.Text);
                    pBahanB.KGradeUtama = decimal.Parse(txtKGradeUtama.Text);
                    pBahanB.Kgrade2 = decimal.Parse(txtKgrade2.Text);
                    pBahanB.Kgrade3 = decimal.Parse(txtKgrade3.Text);
                    pBahanB.TMix = decimal.Parse(tMix.Text);
                    pBahanB.Formula = decimal.Parse(Frmula.Text.Replace(",", "."));
                    pBahanB.KL = decimal.Parse(txtKL.Text.Replace(",", "."));
                    pBahanB.Efis = decimal.Parse(txtEfis.Text.Replace(",", "."));
                    pBahanB.TEfis = decimal.Parse(txtTEfis.Text.Replace(",", "."));
                    pBahanB.KsKg = decimal.Parse(txtKsKg.Text.Replace(",", "."));
                    pBahanB.KsEfis = decimal.Parse(txtKsEfis.Text.Replace(",", "."));
                    pBahanB.KvEfis = decimal.Parse(txtKvEfis.Text.Replace(",", "."));
                    pBahanB.Kgrade2Eff = decimal.Parse(txtKgrade2Eff.Text.Replace(",", "."));
                    pBahanB.KuEfs = decimal.Parse(txtKuEfs.Text.Replace(",", "."));
                    pBahanB.Kgrade3Eff = decimal.Parse(txtKgrade3Eff.Text.Replace(",", "."));
                    pBahanB.KvKg2 = decimal.Parse(txtKvKg2.Text.Replace(",", "."));
                    //pBahanB.UserID = int.Parse(UserID);
                    pBahanB.LastModifiedBy = user.UserName;
                    string strSQL;
                    strSQL = " update BM_Pulp set L1=" + txtjmlL1G1.Text + ",L1x=" + txtjmlL1G1x.Text + ",L1x1=" + txtjmlL1G1x1.Text + ",L1x2=" + txtjmlL1G1x2.Text + ",L2=" + txtjmlL2G2.Text + ",L2x=" + txtjmlL2G2x.Text + ", " +
                             " L3 =" + txtjmlL3G3.Text + ",L3x =" + txtjmlL3G3x.Text + ",L4 =" + txtjmlL4G4.Text + ",L4x =" + txtjmlL4G4x.Text + ",L4x1 =" + txtjmlL4G4x1.Text + ", " +
                             " KuKg='" + txtKuKg.Text.Replace(".", "") + "',SdL='" + txtSdL.Text.Replace(".", "") + "',SspB='" + txtSspB.Text.Replace(".", "") + "',AT='" + txtAT.Text.Replace(".", "") + "',JKertasVirgin='" + txtJenisKertasVirgin.Text + "', " +
                             " Kgrade3Kg='" + txtKgrade3Kg.Text.Replace(".", "") + "',Kgrade2Kg='" + txtKgrade2Kg.Text.Replace(".", "") + "',KvKg='" + txtKvKg.Text.Replace(".", "") + "',KvKg2='" + txtKvKg2.Text.Replace(".", "") + "',Kv = " + txtKV.Text + ",Ks=" + txtKS.Text + ",KGradeUtama=" + txtKGradeUtama.Text + ",Efis =" + txtEfis.Text.Replace(",", ".") + ",KsEfis =" + txtKsEfis.Text.Replace(",", ".") + ",TEfis =" + txtTEfis.Text.Replace(",", ".") + ",KsKg= " + txtKsKg.Text.Replace(".", "") + ", " +
                             " Kgrade3Eff=" + txtKgrade3Eff.Text.Replace(",", ".") + ",KuEfs=" + txtKuEfs.Text.Replace(",", ".") + ",Kgrade2Eff=" + txtKgrade2Eff.Text.Replace(",", ".") + ",KvEfis=" + txtKvEfis.Text.Replace(",", ".") + ",Kgrade2 = " + txtKgrade2.Text + ",Kgrade3= " + txtKgrade3.Text + ",TMix=" + tMix.Text + ",Formula=" + Frmula.Text.Replace(".", "") + ",KL=" + txtKL.Text.Replace(".", "") + ", " +
                             " LastModifiedBy = '" + ((Users)Session["Users"]).UserName + "',LastModifiedTime = GETDATE() where ID = " + txtjmlL1G1.ToolTip.ToString() + " ";
                    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                    if (dataAccess.Error != string.Empty)
                    {
                        Result = -1;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Edit Berhasil ...!!!");
                    }
                    break;

            }
            #endregion
        }

        protected void C2_Databound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void lstR2_Command(object sender, RepeaterCommandEventArgs e)
        {
            #region Proses
            int RowNum = e.Item.ItemIndex;
            Image Simpan = (Image)lstR2.Items[RowNum].FindControl("simpan");
            Image img = (Image)lstR2.Items[RowNum].FindControl("edit");
            TextBox txtjmlL1G1 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL1G1");
            TextBox txtjmlL1G1x = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL1G1x");
            TextBox txtjmlL2G2 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL2G2");
            TextBox txtjmlL2G2x = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL2G2x");
            TextBox txtjmlL2G2x1 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL2G2x1");
            TextBox txtjmlL3G3 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL3G3");
            TextBox txtjmlL3G3x = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL3G3x");
            TextBox txtjmlL4G4 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL4G4");
            TextBox txtjmlL4G4x = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL4G4x");
            TextBox txtjmlL4G4x1 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL4G4x1");
            TextBox txtjmlL5G5 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL5G5");
            TextBox txtjmlL5G5x = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL5G5x");
            TextBox txtjmlL5G5x1 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL5G5x1");
            TextBox txtjmlL6G6 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL6G6");
            TextBox txtjmlL6G6x = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL6G6x");
            TextBox txtjmlL6G6x1 = (TextBox)lstR2.Items[RowNum].FindControl("txtjmlL6G6x1");
            TextBox txtSdL = (TextBox)lstR2.Items[RowNum].FindControl("txtSdL");
            TextBox txtSspB = (TextBox)lstR2.Items[RowNum].FindControl("txtSspB");
            TextBox txtAT = (TextBox)lstR2.Items[RowNum].FindControl("txtAT");
            TextBox txtJenisKertasVirgin = (TextBox)lstR2.Items[RowNum].FindControl("txtJenisKertasVirgin");
            TextBox txtKV = (TextBox)lstR2.Items[RowNum].FindControl("txtKV");
            TextBox txtKB = (TextBox)lstR2.Items[RowNum].FindControl("txtKB");
            TextBox txtKS = (TextBox)lstR2.Items[RowNum].FindControl("txtKS");
            TextBox txtKGradeUtama = (TextBox)lstR2.Items[RowNum].FindControl("txtKGradeUtama");
            TextBox txtKgrade2 = (TextBox)lstR2.Items[RowNum].FindControl("txtKgrade2");
            TextBox txtKgrade3 = (TextBox)lstR2.Items[RowNum].FindControl("txtKgrade3");
            Label tMix = (Label)lstR2.Items[RowNum].FindControl("tMix");
            Label Frmula = (Label)lstR2.Items[RowNum].FindControl("Frmula");
            Label txtKL = (Label)lstR2.Items[RowNum].FindControl("txtKL");
            Label txtEfis = (Label)lstR2.Items[RowNum].FindControl("txtEfis");
            Label txtTEfis = (Label)lstR2.Items[RowNum].FindControl("txtTEfis");
            Label txtKBimaBB = (Label)lstR2.Items[RowNum].FindControl("txtKBimaBB");
            Label txtKBimaSampah = (Label)lstR2.Items[RowNum].FindControl("txtKBimaSampah");
            Label txtKsKg = (Label)lstR2.Items[RowNum].FindControl("txtKsKg");
            Label txtKsEfis = (Label)lstR2.Items[RowNum].FindControl("txtKsEfis");
            Label txtKvEfis = (Label)lstR2.Items[RowNum].FindControl("txtKvEfis");
            Label txtKgrade2Eff = (Label)lstR2.Items[RowNum].FindControl("txtKgrade2Eff");
            Label txtKuEfs = (Label)lstR2.Items[RowNum].FindControl("txtKuEfs");
            Label txtKgKraftPlastikBrsh = (Label)lstR2.Items[RowNum].FindControl("txtKgKraftPlastikBrsh");
            Label txtKgrade3Eff = (Label)lstR2.Items[RowNum].FindControl("txtKgrade3Eff");
            Label txtKvKg2 = (Label)lstR2.Items[RowNum].FindControl("txtKvKg2");
            Label txtKvKg = (Label)lstR2.Items[RowNum].FindControl("txtKvKg");
            //TextBox txtKvKg = (TextBox)lstR2.Items[RowNum].FindControl("txtKvKg");
            Label TglProd = (Label)lstR2.Items[RowNum].FindControl("lbltglProd");
            Label txtKBimaBK = (Label)lstR2.Items[RowNum].FindControl("txtKBimaBK");
            Label txtKsKg0 = (Label)lstR2.Items[RowNum].FindControl("txtKsKg0");
            Label txtKuKg = (Label)lstR2.Items[RowNum].FindControl("txtKuKg");
            Label txtKgrade2Kg = (Label)lstR2.Items[RowNum].FindControl("txtKgrade2Kg");
            Label txtKgKraftPlastikktr = (Label)lstR2.Items[RowNum].FindControl("txtKgKraftPlastikktr"); 
            Label txtKgrade3Kg = (Label)lstR2.Items[RowNum].FindControl("txtKgrade3Kg");
            Label txtKgKraftPlastikSmph = (Label)lstR2.Items[RowNum].FindControl("txtKgKraftPlastikSmph");
            Label lbl1 = (Label)lstR2.Items[RowNum].FindControl("txtL1G");
            Label lbl1x = (Label)lstR2.Items[RowNum].FindControl("txtL1Gx");


            switch (e.CommandName)
            {
                case "Edit":
                    txtjmlL1G1.Visible = true;
                    txtjmlL1G1.ReadOnly = false;
                    txtjmlL1G1x.Visible = true;
                    txtjmlL1G1x.ReadOnly = false;
                    txtjmlL2G2.Visible = true;
                    txtjmlL2G2.ReadOnly = false;
                    txtjmlL2G2x.Visible = true;
                    txtjmlL2G2x.ReadOnly = false;
                    txtjmlL2G2x1.Visible = true;
                    txtjmlL2G2x1.ReadOnly = false;
                    txtjmlL3G3.Visible = true;
                    txtjmlL3G3.ReadOnly = false;
                    txtjmlL3G3x.Visible = true;
                    txtjmlL3G3x.ReadOnly = false;
                    txtjmlL4G4.Visible = true;
                    txtjmlL4G4.ReadOnly = false;
                    txtjmlL4G4x.Visible = true;
                    txtjmlL4G4x.ReadOnly = false;
                    txtjmlL4G4x1.Visible = true;
                    txtjmlL4G4x1.ReadOnly = false;
                    txtjmlL5G5.Visible = true;
                    txtjmlL5G5.ReadOnly = false;
                    txtjmlL5G5x.Visible = true;
                    txtjmlL5G5x.ReadOnly = false;
                    txtjmlL5G5x1.Visible = true;
                    txtjmlL5G5x1.ReadOnly = false;
                    txtjmlL6G6.Visible = true;
                    txtjmlL6G6.ReadOnly = false;
                    txtjmlL6G6x.Visible = true;
                    txtjmlL6G6x.ReadOnly = false;
                    txtjmlL6G6x1.Visible = true;
                    txtjmlL6G6x1.ReadOnly = false;
                    txtSdL.Visible = true;
                    txtSdL.ReadOnly = false;
                    txtSspB.Visible = true;
                    txtSspB.ReadOnly = false;
                    txtAT.Visible = true;
                    txtAT.ReadOnly = false;
                    txtJenisKertasVirgin.Visible = true;
                    txtJenisKertasVirgin.ReadOnly = false;
                    txtKV.Visible = true;
                    txtKV.ReadOnly = false;
                    txtKB.Visible = true;
                    txtKB.ReadOnly = false;
                    txtKS.Visible = true;
                    txtKS.ReadOnly = false;
                    txtKGradeUtama.Visible = true;
                    txtKGradeUtama.ReadOnly = false;
                    txtKgrade2.Visible = true;
                    txtKgrade2.ReadOnly = false;
                    txtKgrade3.Visible = true;
                    txtKgrade3.ReadOnly = false;
                    //txtKgKraftPlastikSmph.Visible = true;
                    //txtKgKraftPlastikSmph.ReadOnly = false;
                    //txtKvKg.Visible = true;
                    //txtKvKg.ReadOnly = false;
                    //txtKBimaBK.Visible = true;
                    //txtKBimaBK.ReadOnly = false;
                    //txtKsKg0.Visible = true;
                    //txtKsKg0.ReadOnly = false;
                    //txtKuKg.Visible = true;
                    //txtKuKg.ReadOnly = false;
                    //txtKgrade2Kg.Visible = true;
                    //txtKgrade2Kg.ReadOnly = false;
                    //txtKgKraftPlastikktr.Visible = true;
                    //txtKgKraftPlastikktr.ReadOnly = false;
                    //txtKgrade3Kg.Visible = true;
                    //txtKgrade3Kg.ReadOnly = false;
                    lbl1.Visible = false;
                    lbl1x.Visible = false;
                    Simpan.Visible = true;
                    btnCalc.Visible = true;
                    img.Visible = false;
                    break;
                case "refresh":

                    PakaiBahanBaku pBahanBx = new PakaiBahanBaku();
                    PakaiBahanBakuFacade pBahanFx = new PakaiBahanBakuFacade();
                    DateTime vTgl = DateTime.Parse(TglProd.Text);
                    txtKvKg.Text = pBahanFx.Retrievexx0(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKBimaBK.Text = pBahanFx.Retrievexx01(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKsKg0.Text = pBahanFx.Retrievexx02(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKuKg.Text = pBahanFx.Retrievexx03(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKgrade2Kg.Text = pBahanFx.Retrievexx04(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKgrade3Kg.Text = pBahanFx.Retrievexx05(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    txtKgKraftPlastikktr.Text = pBahanFx.Retrievexx06(vTgl.ToString("yyyyMMdd")).ToString("N0");
                    break;
                case "Save":
                    txtjmlL1G1.Visible = true;
                    txtjmlL1G1.ReadOnly = true;
                    txtjmlL1G1x.Visible = true;
                    txtjmlL1G1x.ReadOnly = true;
                    txtjmlL2G2.Visible = true;
                    txtjmlL2G2.ReadOnly = true;
                    txtjmlL2G2x.Visible = true;
                    txtjmlL2G2x.ReadOnly = true;
                    txtjmlL2G2x1.Visible = true;
                    txtjmlL2G2x1.ReadOnly = true;
                    txtjmlL3G3.Visible = true;
                    txtjmlL3G3.ReadOnly = true;
                    txtjmlL3G3x.Visible = true;
                    txtjmlL3G3x.ReadOnly = true;
                    txtjmlL4G4.Visible = true;
                    txtjmlL4G4.ReadOnly = true;
                    txtjmlL4G4x.Visible = true;
                    txtjmlL4G4x.ReadOnly = true;
                    txtjmlL4G4x1.Visible = true;
                    txtjmlL4G4x1.ReadOnly = true;
                    txtjmlL5G5.Visible = true;
                    txtjmlL5G5.ReadOnly = true;
                    txtjmlL5G5x.Visible = true;
                    txtjmlL5G5x.ReadOnly = true;
                    txtjmlL5G5x1.Visible = true;
                    txtjmlL5G5x1.ReadOnly = true;
                    txtjmlL6G6.Visible = true;
                    txtjmlL6G6.ReadOnly = true;
                    txtjmlL6G6x.Visible = true;
                    txtjmlL6G6x.ReadOnly = true;
                    txtjmlL6G6x1.Visible = true;
                    txtjmlL6G6x1.ReadOnly = true;
                    txtSdL.Visible = true;
                    txtSdL.ReadOnly = true;
                    txtSspB.Visible = true;
                    txtSspB.ReadOnly = true;
                    txtAT.Visible = true;
                    txtAT.ReadOnly = true;
                    txtJenisKertasVirgin.Visible = true;
                    txtJenisKertasVirgin.ReadOnly = true;
                    txtKV.Visible = true;
                    txtKV.ReadOnly = true;
                    txtKB.Visible = true;
                    txtKB.ReadOnly = true;
                    txtKS.Visible = true;
                    txtKS.ReadOnly = true;
                    txtKGradeUtama.Visible = true;
                    txtKGradeUtama.ReadOnly = true;
                    txtKgrade2.Visible = true;
                    txtKgrade2.ReadOnly = true;
                    txtKgrade3.Visible = true;
                    txtKgrade3.ReadOnly = true;
                    //txtKvKg.Visible = true;
                    //txtKvKg.ReadOnly = true;
                    //txtKBimaBK.Visible = true;
                    //txtKBimaBK.ReadOnly = true;
                    //txtKsKg0.Visible = true;
                    //txtKsKg0.ReadOnly = true;
                    //txtKuKg.Visible = true;
                    //txtKuKg.ReadOnly = true;
                    //txtKgrade2Kg.Visible = true;
                    //txtKgrade2Kg.ReadOnly = true;
                    //txtKgKraftPlastikktr.Visible = true;
                    //txtKgKraftPlastikktr.ReadOnly = true;
                    //txtKgrade3Kg.Visible = true;
                    //txtKgrade3Kg.ReadOnly = true;
                    lbl1.Visible = true;
                    lbl1x.Visible = true;
                    Simpan.Visible = false;
                    btnCalc.Visible = false;
                    img.Visible = true;

                    int Result = 0;
                    Users user = (Users)Session["Users"];
                    PakaiBahanBaku pBahanB = new PakaiBahanBaku();
                    string strError = string.Empty;
                    string UserID = user.ID.ToString();

                    pBahanB.ID = int.Parse(txtjmlL1G1.ToolTip.ToString());
                    pBahanB.L1 = decimal.Parse(txtjmlL1G1.Text);
                    pBahanB.L1x = decimal.Parse(txtjmlL1G1x.Text);
                    pBahanB.L2 = decimal.Parse(txtjmlL2G2.Text);
                    pBahanB.L2x = decimal.Parse(txtjmlL2G2x.Text);
                    pBahanB.L2x1 = decimal.Parse(txtjmlL2G2x1.Text);
                    pBahanB.L3 = decimal.Parse(txtjmlL3G3.Text);
                    pBahanB.L3x = decimal.Parse(txtjmlL3G3x.Text);
                    pBahanB.L4 = decimal.Parse(txtjmlL4G4.Text);
                    pBahanB.L4x = decimal.Parse(txtjmlL4G4x.Text);
                    pBahanB.L4x1 = decimal.Parse(txtjmlL4G4x1.Text);
                    pBahanB.L5 = decimal.Parse(txtjmlL5G5.Text);
                    pBahanB.L5x = decimal.Parse(txtjmlL5G5x.Text);
                    pBahanB.L5x1 = decimal.Parse(txtjmlL5G5x1.Text);
                    pBahanB.L6 = decimal.Parse(txtjmlL6G6.Text);
                    pBahanB.L6x = decimal.Parse(txtjmlL6G6x.Text);
                    pBahanB.L6x1 = decimal.Parse(txtjmlL6G6x1.Text);
                    pBahanB.SdL = decimal.Parse(txtSdL.Text);
                    pBahanB.SspB = decimal.Parse(txtSspB.Text);
                    pBahanB.AT = decimal.Parse(txtAT.Text);
                    pBahanB.JKertasVirgin = txtJenisKertasVirgin.Text;
                    pBahanB.Kv = decimal.Parse(txtKV.Text);
                    pBahanB.Kb = decimal.Parse(txtKB.Text);
                    pBahanB.Ks = decimal.Parse(txtKS.Text);
                    pBahanB.KGradeUtama = decimal.Parse(txtKGradeUtama.Text);
                    pBahanB.Kgrade2 = decimal.Parse(txtKgrade2.Text);
                    pBahanB.Kgrade3 = decimal.Parse(txtKgrade3.Text);
                    pBahanB.TMix = decimal.Parse(tMix.Text);
                    pBahanB.Formula = decimal.Parse(Frmula.Text.Replace(",", "."));
                    pBahanB.KL = decimal.Parse(txtKL.Text.Replace(",", "."));
                    pBahanB.Efis = decimal.Parse(txtEfis.Text.Replace(",", "."));
                    pBahanB.TEfis = decimal.Parse(txtTEfis.Text.Replace(",", "."));
                    pBahanB.BbBimaKg = decimal.Parse(txtKBimaBB.Text.Replace(",", "."));
                    pBahanB.SampahBima = decimal.Parse(txtKBimaSampah.Text.Replace(",", "."));
                    pBahanB.KsKg = decimal.Parse(txtKsKg.Text.Replace(",", "."));
                    pBahanB.KsEfis = decimal.Parse(txtKsEfis.Text.Replace(",", "."));
                    pBahanB.KvEfis = decimal.Parse(txtKvEfis.Text.Replace(",", "."));
                    pBahanB.Kgrade2Eff = decimal.Parse(txtKgrade2Eff.Text.Replace(",", "."));
                    pBahanB.KuEfs = decimal.Parse(txtKuEfs.Text.Replace(",", "."));
                    pBahanB.KraftBkKg = decimal.Parse(txtKgKraftPlastikBrsh.Text.Replace(",", "."));
                    pBahanB.Kgrade3Eff = decimal.Parse(txtKgrade3Eff.Text.Replace(",", "."));
                    pBahanB.KvKg2 = decimal.Parse(txtKvKg2.Text.Replace(",", "."));
                    pBahanB.SampahKraft = decimal.Parse(txtKgKraftPlastikBrsh.Text.Replace(",", "."));
                    //pBahanB.UserID = int.Parse(UserID);
                    //test
                    pBahanB.LastModifiedBy = user.UserName;
                    string strSQL;
                    strSQL = " update BM_Pulp set SampahKraft=" + txtKgKraftPlastikSmph.Text + ", L1=" + txtjmlL1G1.Text + ",L1x=" + txtjmlL1G1x.Text + ",L2=" + txtjmlL2G2.Text + ",L2x=" + txtjmlL2G2x.Text + ", " +
                             " L2x1=" + txtjmlL2G2x1.Text + ",L3 =" + txtjmlL3G3.Text + ",L3x =" + txtjmlL3G3x.Text + ",L4 =" + txtjmlL4G4.Text + ",L4x =" + txtjmlL4G4x.Text + ", " +
                             " L4x1 =" + txtjmlL4G4x1.Text + ",L5=" + txtjmlL5G5.Text + ",L5x=" + txtjmlL5G5x.Text + ",L5x1=" + txtjmlL5G5x1.Text + ",L6=" + txtjmlL6G6.Text + ", " +
                             " L6x = " + txtjmlL6G6x.Text + ",L6x1 = " + txtjmlL6G6x1.Text + ",SdL='" + txtSdL.Text.Replace(".", "") + "',SspB='" + txtSspB.Text.Replace(".", "") + "',AT='" + 
                             txtAT.Text.Replace(".", "") + "',JKertasVirgin='" + txtJenisKertasVirgin.Text + "', " +
                             " KraftBkKg='" + Decimal.Parse(txtKgKraftPlastikktr.Text).ToString().Replace(",", ".") + "',Kgrade3Kg='" + Decimal.Parse(txtKgrade3Kg.Text).ToString().Replace(",", ".") + "',Kgrade2Kg='" +
                             Decimal.Parse(txtKgrade2Kg.Text).ToString().Replace(",", ".") + "',KuKg='" +
                             Decimal.Parse(txtKuKg.Text).ToString().Replace(",", ".") + "',KsKg0='" + Decimal.Parse(txtKsKg0.Text).ToString().Replace(",", ".") + 
                             "',BkBimaKg='" + Decimal.Parse(txtKBimaBK.Text).ToString().Replace(",", ".") + "',KvKg='" + Decimal.Parse(txtKvKg.Text).ToString().Replace(",", ".") + "',KvKg2='" +
                             Decimal.Parse(txtKvKg2.Text).ToString().Replace(",", ".") + "',Kv = " + txtKV.Text + ",Kb=" + txtKB.Text + ",Ks=" + txtKS.Text + ",KGradeUtama=" + txtKGradeUtama.Text + 
                             ",Efis =" + Decimal.Parse(txtEfis.Text).ToString().Replace(",", ".") + ",KsEfis =" +
                             Decimal.Parse(txtKsEfis.Text).ToString().Replace(",", ".") + ",TEfis =" + Decimal.Parse(txtTEfis.Text).ToString().Replace(",", ".") + 
                             ",SampahBima= " + Decimal.Parse(txtKBimaSampah.Text).ToString().Replace(",", "") + ",KsKg= " +
                             Decimal.Parse(txtKsKg.Text).ToString().Replace(".", "") + ", " +" Kgrade3Eff=" + Decimal.Parse(txtKgrade3Eff.Text).ToString().Replace(",", ".") + 
                             ",KraftBbKg='" + Decimal.Parse(txtKgKraftPlastikBrsh.Text).ToString().Replace(",", "") + "',KuEfs=" +
                             Decimal.Parse(txtKuEfs.Text).ToString().Replace(",", ".") + ",Kgrade2Eff=" + Decimal.Parse(txtKgrade2Eff.Text).ToString().Replace(",", ".") + 
                             ",KvEfis=" + Decimal.Parse(txtKvEfis.Text).ToString().Replace(",", ".") + ",Kgrade2 = " + txtKgrade2.Text + ",Kgrade3= " + 
                             txtKgrade3.Text + ",TMix=" + tMix.Text + ",Formula=" + Frmula.Text.Replace(".", "") + ",KL=" + txtKL.Text.Replace(".", "") + ",BbBimaKg=" + txtKBimaBB.Text.Replace(".", "") + ", " +
                             " LastModifiedBy = '" + ((Users)Session["Users"]).UserName + "',LastModifiedTime = GETDATE() where ID = " + txtjmlL1G1.ToolTip.ToString() + " ";
                    DataAccess dataAccess = new DataAccess(Global.ConnectionString());
                    SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(strSQL);
                    if (dataAccess.Error != string.Empty)
                    {
                        Result = -1;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Edit Berhasil ...!!!");
                    }
                    break;

            }
            #endregion
        }

        protected void lstR2_Databound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void btnSimpan2_Click(object sender, EventArgs e)
        {
            #region Proses Updated
            //GetValue();
            //int intResult = 0;
            ////string strEvent = "Insert";
            //PakaiBahanBaku pbb = new PakaiBahanBaku();
            //PakaiBahanBakuFacade pbb0 = new PakaiBahanBakuFacade();
            //pbb0 = new PakaiBahanBakuFacade();
            ////if (ViewState["id"] != null)
            ////{
            ////    pbb.ID = int.Parse(ViewState["id"].ToString());
            ////    strEvent = "Edit";
            ////}

            //ArrayList arrData = (ArrayList)Session["ListBBPemakaian"];
            //for (int i = 0; i < lstR2.Items.Count; i++)
            //{
            //    intResult = 0;
            //    HtmlTableRow tr = (HtmlTableRow)lstR2.Items[i].FindControl("lstR2");
            //    Label lbl = (Label)lstR2.Items[i].FindControl("lbltglProd");
            //    TextBox txtL1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL1G1");
            //    TextBox txtL1x = (TextBox)lstR2.Items[i].FindControl("txtjmlL1G1x");
            //    TextBox txtL2 = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2");
            //    TextBox txtL2x = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2x");
            //    TextBox txtL2x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2x1");
            //    TextBox txtL3 = (TextBox)lstR2.Items[i].FindControl("txtjmlL3G3");
            //    TextBox txtL3x = (TextBox)lstR2.Items[i].FindControl("txtjmlL3G3x");
            //    TextBox txtL4 = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4");
            //    TextBox txtL4x = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4x");
            //    TextBox txtL4x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4x1");
            //    TextBox txtL5 = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5");
            //    TextBox txtL5x = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5x");
            //    TextBox txtL5x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5x1");
            //    TextBox txtL6 = (TextBox)lstR2.Items[i].FindControl("txtjmlL6G6");
            //    TextBox txtL6x = (TextBox)lstR2.Items[i].FindControl("txtjmlL6G6x");
            //    Label lbltMix = (Label)lstR2.Items[i].FindControl("tMix");
            //    Label lblFrmula = (Label)lstR2.Items[i].FindControl("Frmula");
            //    TextBox txtSdL = (TextBox)lstR2.Items[i].FindControl("txtSdL");
            //    TextBox txtSspB = (TextBox)lstR2.Items[i].FindControl("txtSspB");
            //    TextBox txtAT = (TextBox)lstR2.Items[i].FindControl("txtAT");
            //    Label txtKL = (Label)lstR2.Items[i].FindControl("txtKL");
            //    Label lblEfis = (Label)lstR2.Items[i].FindControl("txtEfis");
            //    TextBox txtJenisKertasVirgin = (TextBox)lstR2.Items[i].FindControl("txtJenisKertasVirgin");
            //    Label lblKvKg = (Label)lstR2.Items[i].FindControl("txtKvKg");
            //    Label lblKvKg2 = (Label)lstR2.Items[i].FindControl("txtKvKg2");
            //    Label lblKvEfis = (Label)lstR2.Items[i].FindControl("txtKvEfis");
            //    Label lblKBimaBK = (Label)lstR2.Items[i].FindControl("txtKBimaBK");
            //    Label lblKBimaSampah = (Label)lstR2.Items[i].FindControl("txtKBimaSampah");
            //    Label lblKBimaBB = (Label)lstR2.Items[i].FindControl("txtKBimaBB");
            //    Label lblKsKg0 = (Label)lstR2.Items[i].FindControl("txtKsKg0");
            //    Label lblKsKg = (Label)lstR2.Items[i].FindControl("txtKsKg");
            //    Label lblKsEfis = (Label)lstR2.Items[i].FindControl("txtKsEfis");
            //    Label lblKuKg = (Label)lstR2.Items[i].FindControl("txtKuKg");
            //    Label lblKuEfs = (Label)lstR2.Items[i].FindControl("txtKuEfs");
            //    Label lblKgrade2Kg = (Label)lstR2.Items[i].FindControl("txtKgrade2Kg");
            //    Label lblKgrade2Eff = (Label)lstR2.Items[i].FindControl("txtKgrade2Eff");
            //    Label lblKgKraftPlastikktr = (Label)lstR2.Items[i].FindControl("txtKgKraftPlastikktr");
            //    Label lblKgKraftPlastikSmph = (Label)lstR2.Items[i].FindControl("txtKgKraftPlastikSmph");
            //    Label lblKgKraftPlastikBrsh = (Label)lstR2.Items[i].FindControl("txtKgKraftPlastikBrsh");
            //    Label lblKgrade3Kg = (Label)lstR2.Items[i].FindControl("txtKgrade3Kg");
            //    Label lblKgrade3Kg2 = (Label)lstR2.Items[i].FindControl("txtKgrade3Kg2");
            //    Label lblKgrade3Eff = (Label)lstR2.Items[i].FindControl("txtKgrade3Eff");
            //    Label lblTEfis = (Label)lstR2.Items[i].FindControl("txtTEfis");
            //    TextBox txtKV = (TextBox)lstR2.Items[i].FindControl("txtKV");
            //    TextBox txtKB = (TextBox)lstR2.Items[i].FindControl("txtKB");
            //    TextBox txtKS = (TextBox)lstR2.Items[i].FindControl("txtKS");
            //    TextBox txtKGradeUtama = (TextBox)lstR2.Items[i].FindControl("txtKGradeUtama");
            //    TextBox txtKgrade2 = (TextBox)lstR2.Items[i].FindControl("txtKgrade2");
            //    TextBox txtKgrade3 = (TextBox)lstR2.Items[i].FindControl("txtKgrade3");

            //    //convert to string
            //    string L1G1 = txtL1.Text;
            //    string L1G1x = txtL1x.Text;
            //    string L2G2 = txtL2.Text;
            //    string L2G2x = txtL2x.Text;
            //    string L2G2x1 = txtL2x1.Text;
            //    string L3G3 = txtL3.Text;
            //    string L3G3x = txtL3x.Text;
            //    string L4G4 = txtL4.Text;
            //    string L4G4x = txtL4x.Text;
            //    string L4G4x1 = txtL4x1.Text;
            //    string L5G5 = txtL5.Text;
            //    string L5G5x = txtL5x.Text;
            //    string L5G5x1 = txtL5x1.Text;
            //    string L6G6 = txtL6.Text;
            //    string L6G6x = txtL6x.Text;
            //    string tMix = lbltMix.Text;
            //    string Frmula = lblFrmula.Text;
            //    string SdL = txtSdL.Text;
            //    string SspB = txtSspB.Text;
            //    string AT = txtAT.Text;
            //    string KL = txtKL.Text;
            //    string Efis = lblEfis.Text;
            //    string JenisKertasVirgin = txtJenisKertasVirgin.Text;
            //    string KvKg = lblKvKg.Text;
            //    string KvKg2 = lblKvKg2.Text;
            //    string KvEfis = lblKvEfis.Text;
            //    string KBimaBK = lblKBimaBK.Text;
            //    string KBimaSampah = lblKBimaSampah.Text;
            //    string KBimaBB = lblKBimaBB.Text;
            //    string KsKg0 = lblKsKg0.Text;
            //    string KsKg = lblKsKg.Text;
            //    string KsEfis = lblKsEfis.Text;
            //    string KuKg = lblKuKg.Text;
            //    string KuEfs = lblKuEfs.Text;
            //    string Kgrade2Kg = lblKgrade2Kg.Text;
            //    string Kgrade2Eff = lblKgrade2Eff.Text;
            //    string KgKraftPlastikktr = lblKgKraftPlastikktr.Text;
            //    string KgKraftPlastikSmph = lblKgKraftPlastikSmph.Text;
            //    string KgKraftPlastikBrsh = lblKgKraftPlastikBrsh.Text;
            //    string Kgrade3Kg = lblKgrade3Kg.Text;
            //    string Kgrade3Kg2 = lblKgrade3Kg2.Text;
            //    string Kgrade3Eff = lblKgrade3Eff.Text;
            //    string TEfis = lblTEfis.Text;
            //    string KV = txtKV.Text;
            //    string KB = txtKB.Text;
            //    string KS = txtKS.Text;
            //    string KGradeUtama = txtKGradeUtama.Text;
            //    string Kgrade2 = txtKgrade2.Text;
            //    string Kgrade3 = txtKgrade3.Text;

            //    //pbb.Tgl_Prod = Convert.ToDateTime(lbl.Text);
            //    //pbb.Tgl_Prod2 = (lbl.Text).ToString();
            //    pbb.L1 = Convert.ToDecimal(txtL1.Text);
            //    pbb.L1x = Convert.ToDecimal(txtL1x.Text);
            //    pbb.L2 = Convert.ToDecimal(txtL2.Text);
            //    pbb.L2x = Convert.ToDecimal(txtL2x.Text);
            //    pbb.L2x1 = Convert.ToDecimal(txtL2x1.Text);
            //    pbb.L3 = Convert.ToDecimal(txtL3.Text);
            //    pbb.L3x = Convert.ToDecimal(txtL3x.Text);
            //    pbb.L4 = Convert.ToDecimal(txtL4.Text);
            //    pbb.L4x = Convert.ToDecimal(txtL4x.Text);
            //    pbb.L4x1 = Convert.ToDecimal(txtL4x1.Text);
            //    pbb.L5 = Convert.ToDecimal(txtL5.Text);
            //    pbb.L5x = Convert.ToDecimal(txtL5x.Text);
            //    pbb.L5x1 = Convert.ToDecimal(txtL5x1.Text);
            //    pbb.L6 = Convert.ToDecimal(txtL6.Text);
            //    pbb.L6x = Convert.ToDecimal(txtL6x.Text);
            //    //pbb.TMix = Convert.ToDecimal(lbltMix.Text);
            //    //pbb.Formula = Convert.ToDecimal(lblFrmula.Text);
            //    //pbb.SdL = Convert.ToDecimal(txtSdL.Text);
            //    //pbb.SspB = Convert.ToDecimal(txtSspB.Text);
            //    //pbb.AT = Convert.ToDecimal(txtAT.Text);
            //    //pbb.KL = Convert.ToDecimal(txtKL.Text);
            //    //pbb.Efis = Convert.ToDecimal(lblEfis.Text);
            //    //pbb.JKertasVirgin = (txtJenisKertasVirgin.Text).ToString();
            //    //pbb.KvKg = Convert.ToDecimal(lblKvKg.Text);
            //    //pbb.KvKg2 = Convert.ToDecimal(lblKvKg2.Text);
            //    //pbb.KvEfis = Convert.ToDecimal(lblKvEfis.Text);
            //    //pbb.BkBimaKg = Convert.ToDecimal(lblKBimaBK.Text);
            //    //pbb.SampahBima = Convert.ToDecimal(lblKBimaSampah.Text);
            //    //pbb.BbBimaKg = Convert.ToDecimal(lblKBimaBB.Text);
            //    //pbb.KsKg0 = Convert.ToDecimal(lblKsKg0.Text);
            //    //pbb.KsKg = Convert.ToDecimal(lblKsKg.Text);
            //    //pbb.KsEfis = Convert.ToDecimal(lblKsEfis.Text);
            //    //pbb.KuKg = Convert.ToDecimal(lblKuKg.Text);
            //    //pbb.KuEfs = Convert.ToDecimal(lblKuEfs.Text);
            //    //pbb.Kgrade2Kg = Convert.ToDecimal(lblKgrade2Kg.Text);
            //    //pbb.Kgrade2Eff = Convert.ToDecimal(lblKgrade2Eff.Text);
            //    //pbb.KraftBkKg = Convert.ToDecimal(lblKgKraftPlastikktr.Text);
            //    //pbb.SampahKraft = Convert.ToDecimal(lblKgKraftPlastikSmph.Text);
            //    //pbb.KraftBbKg = Convert.ToDecimal(lblKgKraftPlastikBrsh.Text);
            //    //pbb.Kgrade3Kg = Convert.ToDecimal(lblKgrade3Kg.Text);
            //    //pbb.Kgrade3xKg = Convert.ToDecimal(lblKgrade3Kg2.Text);
            //    //pbb.Kgrade3Eff = Convert.ToDecimal(lblKgrade3Eff.Text);
            //    //pbb.TEfis = Convert.ToDecimal(lblTEfis.Text);
            //    //pbb.Kv = Convert.ToDecimal(txtKV.Text);
            //    //pbb.Kb = Convert.ToDecimal(txtKB.Text);
            //    //pbb.Ks = Convert.ToDecimal(txtKS.Text);
            //    //pbb.KGradeUtama = Convert.ToDecimal(txtKGradeUtama.Text);
            //    //pbb.Kgrade2 = Convert.ToDecimal(txtKgrade2.Text);
            //    //pbb.Kgrade3 = Convert.ToDecimal(txtKgrade3.Text);
            //    //pbb.CreatedBy = ((Users)Session["Users"]).UserName;
            //    //pbb.LastModifiedBy = ((Users)Session["Users"]).UserName;
            //    //int intResult = 0;
            //    //if (pbb.ID > 0)
            //    //{
            //        intResult = pbb0.Update(pbb);
            //        DisplayAJAXMessage(this, "Data Telah Updated");
            //    //}
            //    //if (pbb.ID > 0)
            //    //{
            //        //intResult = pbb0.Update(pbb);
            //        //DisplayAJAXMessage(this, "Data Telah Updated");
            //        ////InsertLog(strEvent);
            //    //}
            //    //else
            //    //{
            //    //    DisplayAJAXMessage(this, "Gagal Update");
            //    //}
            //}
            #endregion
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            if (user.UnitKerjaID == 1)
            {
                #region Proses Simpan ctrp
                GetValue();
                int intResult = 0;
                string strEvent = "Insert";
                PakaiBahanBaku pbb = new PakaiBahanBaku();
                PakaiBahanBakuFacade pbb0 = new PakaiBahanBakuFacade();
                pbb0 = new PakaiBahanBakuFacade();
                if (ViewState["id"] != null)
                {
                    pbb.ID = int.Parse(ViewState["id"].ToString());
                    strEvent = "Edit";
                }

                ArrayList arrData = (ArrayList)Session["ListBBPemakaian"];
                for (int i = 0; i < C1.Items.Count; i++)
                {
                    intResult = 0;
                    HtmlTableRow tr = (HtmlTableRow)C1.Items[i].FindControl("Tr1c");
                    Label lbl = (Label)C1.Items[i].FindControl("lbltglProd");
                    TextBox txtL1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1");
                    TextBox txtL1x = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x");
                    TextBox txtL1x1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x1");
                    TextBox txtL1x12 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x12");
                    TextBox txtL2 = (TextBox)C1.Items[i].FindControl("txtjmlL2G2");
                    TextBox txtL2x = (TextBox)C1.Items[i].FindControl("txtjmlL2G2x");
                    //TextBox txtL2x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x1");
                    TextBox txtL3 = (TextBox)C1.Items[i].FindControl("txtjmlL3G3");
                    TextBox txtL3x = (TextBox)C1.Items[i].FindControl("txtjmlL3G3x");
                    TextBox txtL4 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4");
                    TextBox txtL4x = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x");
                    TextBox txtL4x1 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x1");
                    //TextBox txtL5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                    //TextBox txtL5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                    //TextBox txtL5x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x1");
                    //TextBox txtL6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                    //TextBox txtL6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                    Label lbltMix = (Label)C1.Items[i].FindControl("tMix");
                    Label lblFrmula = (Label)C1.Items[i].FindControl("Frmula");
                    TextBox txtSdL = (TextBox)C1.Items[i].FindControl("txtSdL");
                    TextBox txtSspB = (TextBox)C1.Items[i].FindControl("txtSspB");
                    TextBox txtAT = (TextBox)C1.Items[i].FindControl("txtAT");
                    Label txtKL = (Label)C1.Items[i].FindControl("txtKL");
                    Label lblEfis = (Label)C1.Items[i].FindControl("txtEfis");
                    TextBox txtJenisKertasVirgin = (TextBox)C1.Items[i].FindControl("txtJenisKertasVirgin");
                    Label lblKvKg = (Label)C1.Items[i].FindControl("txtKvKg");
                    Label lblKvKg2 = (Label)C1.Items[i].FindControl("txtKvKg2");
                    Label lblKvEfis = (Label)C1.Items[i].FindControl("txtKvEfis");
                    //Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                    //Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                    //Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                    //Label lblKsKg0 = (Label)lstR.Items[i].FindControl("txtKsKg0");
                    Label lblKsKg = (Label)C1.Items[i].FindControl("txtKsKg");
                    Label lblKsEfis = (Label)C1.Items[i].FindControl("txtKsEfis");
                    Label lblKuKg = (Label)C1.Items[i].FindControl("txtKuKg");
                    Label lblKuEfs = (Label)C1.Items[i].FindControl("txtKuEfs");
                    Label lblKgrade2Kg = (Label)C1.Items[i].FindControl("txtKgrade2Kg");
                    Label lblKgrade2Eff = (Label)C1.Items[i].FindControl("txtKgrade2Eff");
                    //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                    //Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                    //Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                    Label lblKgrade3Kg = (Label)C1.Items[i].FindControl("txtKgrade3Kg");
                    //Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
                    Label lblKgrade3Eff = (Label)C1.Items[i].FindControl("txtKgrade3Eff");
                    Label lblTEfis = (Label)C1.Items[i].FindControl("txtTEfis");
                    TextBox txtKV = (TextBox)C1.Items[i].FindControl("txtKV");
                    //TextBox txtKB = (TextBox)lstR.Items[i].FindControl("txtKB");
                    TextBox txtKS = (TextBox)C1.Items[i].FindControl("txtKS");
                    TextBox txtKGradeUtama = (TextBox)C1.Items[i].FindControl("txtKGradeUtama");
                    TextBox txtKgrade2 = (TextBox)C1.Items[i].FindControl("txtKgrade2");
                    TextBox txtKgrade3 = (TextBox)C1.Items[i].FindControl("txtKgrade3");

                    //convert to string
                    string L1G1 = txtL1.Text;
                    string L1G1x = txtL1x.Text;
                    string L1G1x1 = txtL1x1.Text;
                    string L1G1x12 = txtL1x12.Text;
                    string L2G2 = txtL2.Text;
                    string L2G2x = txtL2x.Text;
                    //string L2G2x1 = txtL2x1.Text;
                    string L3G3 = txtL3.Text;
                    string L3G3x = txtL3x.Text;
                    string L4G4 = txtL4.Text;
                    string L4G4x = txtL4x.Text;
                    string L4G4x1 = txtL4x1.Text;
                    //string L5G5 = txtL5.Text;
                    //string L5G5x = txtL5x.Text;
                    //string L5G5x1 = txtL5x1.Text;
                    //string L6G6 = txtL6.Text;
                    //string L6G6x = txtL6x.Text;
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
                    //string KBimaBK = lblKBimaBK.Text;
                    //string KBimaSampah = lblKBimaSampah.Text;
                    //string KBimaBB = lblKBimaBB.Text;
                    //string KsKg0 = lblKsKg0.Text;
                    string KsKg = lblKsKg.Text;
                    string KsEfis = lblKsEfis.Text;
                    string KuKg = lblKuKg.Text;
                    string KuEfs = lblKuEfs.Text;
                    string Kgrade2Kg = lblKgrade2Kg.Text;
                    string Kgrade2Eff = lblKgrade2Eff.Text;
                    //string KgKraftPlastikktr = lblKgKraftPlastikktr.Text;
                    //string KgKraftPlastikSmph = lblKgKraftPlastikSmph.Text;
                    //string KgKraftPlastikBrsh = lblKgKraftPlastikBrsh.Text;
                    string Kgrade3Kg = lblKgrade3Kg.Text;
                    //string Kgrade3Kg2 = lblKgrade3Kg2.Text;
                    string Kgrade3Eff = lblKgrade3Eff.Text;
                    string TEfis = lblTEfis.Text;
                    string KV = txtKV.Text;
                    //string KB = txtKB.Text;
                    string KS = txtKS.Text;
                    string KGradeUtama = txtKGradeUtama.Text;
                    string Kgrade2 = txtKgrade2.Text;
                    string Kgrade3 = txtKgrade3.Text;

                    pbb.Tgl_Prod = Convert.ToDateTime(lbl.Text);
                    pbb.Tgl_Prod2 = (lbl.Text).ToString();
                    pbb.L1 = Convert.ToDecimal(txtL1.Text);
                    pbb.L1x = Convert.ToDecimal(txtL1x.Text);
                    pbb.L1x1 = Convert.ToDecimal(txtL1x1.Text);
                    pbb.L1x2 = Convert.ToDecimal(txtL1x12.Text);
                    pbb.L2 = Convert.ToDecimal(txtL2.Text);
                    pbb.L2x = Convert.ToDecimal(txtL2x.Text);
                    //pbb.L2x1 = Convert.ToDecimal(txtL2x1.Text);
                    pbb.L3 = Convert.ToDecimal(txtL3.Text);
                    pbb.L3x = Convert.ToDecimal(txtL3x.Text);
                    pbb.L4 = Convert.ToDecimal(txtL4.Text);
                    pbb.L4x = Convert.ToDecimal(txtL4x.Text);
                    pbb.L4x1 = Convert.ToDecimal(txtL4x1.Text);
                    //pbb.L5 = Convert.ToDecimal(txtL5.Text);
                    //pbb.L5x = Convert.ToDecimal(txtL5x.Text);
                    //pbb.L5x1 = Convert.ToDecimal(txtL5x1.Text);
                    //pbb.L6 = Convert.ToDecimal(txtL6.Text);
                    //pbb.L6x = Convert.ToDecimal(txtL6x.Text);
                    pbb.TMix = Convert.ToDecimal(lbltMix.Text);
                    pbb.Formula = Convert.ToDecimal(lblFrmula.Text);
                    pbb.SdL = Convert.ToDecimal(txtSdL.Text);
                    pbb.SspB = Convert.ToDecimal(txtSspB.Text);
                    pbb.AT = Convert.ToDecimal(txtAT.Text);
                    pbb.KL = Convert.ToDecimal(txtKL.Text);
                    pbb.Efis = Convert.ToDecimal(lblEfis.Text);
                    pbb.JKertasVirgin = (txtJenisKertasVirgin.Text).ToString();
                    pbb.KvKg = Convert.ToDecimal(lblKvKg.Text);
                    pbb.KvKg2 = Convert.ToDecimal(lblKvKg2.Text);
                    pbb.KvEfis = Convert.ToDecimal(lblKvEfis.Text);
                    //pbb.BkBimaKg = Convert.ToDecimal(lblKBimaBK.Text);
                    //pbb.SampahBima = Convert.ToDecimal(lblKBimaSampah.Text);
                    //pbb.BbBimaKg = Convert.ToDecimal(lblKBimaBB.Text);
                    //pbb.KsKg0 = Convert.ToDecimal(lblKsKg0.Text);
                    pbb.KsKg = Convert.ToDecimal(lblKsKg.Text);
                    pbb.KsEfis = Convert.ToDecimal(lblKsEfis.Text);
                    pbb.KuKg = Convert.ToDecimal(lblKuKg.Text);
                    pbb.KuEfs = Convert.ToDecimal(lblKuEfs.Text);
                    pbb.Kgrade2Kg = Convert.ToDecimal(lblKgrade2Kg.Text);
                    pbb.Kgrade2Eff = Convert.ToDecimal(lblKgrade2Eff.Text);
                    //pbb.KraftBkKg = Convert.ToDecimal(lblKgKraftPlastikktr.Text);
                    //pbb.SampahKraft = Convert.ToDecimal(lblKgKraftPlastikSmph.Text);
                    //pbb.KraftBbKg = Convert.ToDecimal(lblKgKraftPlastikBrsh.Text);
                    pbb.Kgrade3Kg = Convert.ToDecimal(lblKgrade3Kg.Text);
                    //pbb.Kgrade3xKg = Convert.ToDecimal(lblKgrade3Kg2.Text);
                    pbb.Kgrade3Eff = Convert.ToDecimal(lblKgrade3Eff.Text);
                    pbb.TEfis = Convert.ToDecimal(lblTEfis.Text);
                    pbb.Kv = Convert.ToDecimal(txtKV.Text);
                    //pbb.Kb = Convert.ToDecimal(txtKB.Text);
                    pbb.Ks = Convert.ToDecimal(txtKS.Text);
                    pbb.KGradeUtama = Convert.ToDecimal(txtKGradeUtama.Text);
                    pbb.Kgrade2 = Convert.ToDecimal(txtKgrade2.Text);
                    pbb.Kgrade3 = Convert.ToDecimal(txtKgrade3.Text);
                    pbb.CreatedBy = ((Users)Session["Users"]).UserName;
                    pbb.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    if (pbb.ID > 0)
                    {
                        intResult = pbb0.Update(pbb);
                        InsertLog(strEvent);
                    }
                    else
                    {
                        intResult = pbb0.InsertCtrp(pbb);
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
                #endregion
            }
            else if (user.UnitKerjaID == 7)
            {
                #region Proses Simpan krwg
                GetValue();
                int intResult = 0;
                string strEvent = "Insert";
                PakaiBahanBaku pbb = new PakaiBahanBaku();
                PakaiBahanBakuFacade pbb0 = new PakaiBahanBakuFacade();
                pbb0 = new PakaiBahanBakuFacade();
                if (ViewState["id"] != null)
                {
                    pbb.ID = int.Parse(ViewState["id"].ToString());
                    strEvent = "Edit";
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
                    TextBox txtL2x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x1");
                    TextBox txtL3 = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3");
                    TextBox txtL3x = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3x");
                    TextBox txtL4 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4");
                    TextBox txtL4x = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x");
                    TextBox txtL4x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x1");
                    TextBox txtL5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                    TextBox txtL5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                    TextBox txtL5x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x1");
                    TextBox txtL6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                    TextBox txtL6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                    TextBox txtL6x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x1");
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
                    Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                    Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                    Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                    Label lblKgrade3Kg = (Label)lstR.Items[i].FindControl("txtKgrade3Kg");
                    Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
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
                    string L2G2x1 = txtL2x1.Text;
                    string L3G3 = txtL3.Text;
                    string L3G3x = txtL3x.Text;
                    string L4G4 = txtL4.Text;
                    string L4G4x = txtL4x.Text;
                    string L4G4x1 = txtL4x1.Text;
                    string L5G5 = txtL5.Text;
                    string L5G5x = txtL5x.Text;
                    string L5G5x1 = txtL5x1.Text;
                    string L6G6 = txtL6.Text;
                    string L6G6x = txtL6x.Text;
                    string L6G6x1 = txtL6x1.Text;
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
                    string KgKraftPlastikktr = lblKgKraftPlastikktr.Text;
                    string KgKraftPlastikSmph = lblKgKraftPlastikSmph.Text;
                    string KgKraftPlastikBrsh = lblKgKraftPlastikBrsh.Text;
                    string Kgrade3Kg = lblKgrade3Kg.Text;
                    string Kgrade3Kg2 = lblKgrade3Kg2.Text;
                    string Kgrade3Eff = lblKgrade3Eff.Text;
                    string TEfis = lblTEfis.Text;
                    string KV = txtKV.Text;
                    string KB = txtKB.Text;
                    string KS = txtKS.Text;
                    string KGradeUtama = txtKGradeUtama.Text;
                    string Kgrade2 = txtKgrade2.Text;
                    string Kgrade3 = txtKgrade3.Text;

                    pbb.Tgl_Prod = Convert.ToDateTime(lbl.Text);
                    pbb.Tgl_Prod2 = (lbl.Text).ToString();
                    pbb.L1 = Convert.ToDecimal(txtL1.Text);
                    pbb.L1x = Convert.ToDecimal(txtL1x.Text);
                    pbb.L2 = Convert.ToDecimal(txtL2.Text);
                    pbb.L2x = Convert.ToDecimal(txtL2x.Text);
                    pbb.L2x1 = Convert.ToDecimal(txtL2x1.Text);
                    pbb.L3 = Convert.ToDecimal(txtL3.Text);
                    pbb.L3x = Convert.ToDecimal(txtL3x.Text);
                    pbb.L4 = Convert.ToDecimal(txtL4.Text);
                    pbb.L4x = Convert.ToDecimal(txtL4x.Text);
                    pbb.L4x1 = Convert.ToDecimal(txtL4x1.Text);
                    pbb.L5 = Convert.ToDecimal(txtL5.Text);
                    pbb.L5x = Convert.ToDecimal(txtL5x.Text);
                    pbb.L5x1 = Convert.ToDecimal(txtL5x1.Text);
                    pbb.L6 = Convert.ToDecimal(txtL6.Text);
                    pbb.L6x = Convert.ToDecimal(txtL6x.Text);
                    pbb.L6x1 = Convert.ToDecimal(txtL6x1.Text);
                    pbb.TMix = Convert.ToDecimal(lbltMix.Text);
                    pbb.Formula = Convert.ToDecimal(lblFrmula.Text);
                    pbb.SdL = Convert.ToDecimal(txtSdL.Text);
                    pbb.SspB = Convert.ToDecimal(txtSspB.Text);
                    pbb.AT = Convert.ToDecimal(txtAT.Text);
                    pbb.KL = Convert.ToDecimal(txtKL.Text);
                    pbb.Efis = Convert.ToDecimal(lblEfis.Text);
                    pbb.JKertasVirgin = (txtJenisKertasVirgin.Text).ToString();
                    pbb.KvKg = Convert.ToDecimal(lblKvKg.Text);
                    pbb.KvKg2 = Convert.ToDecimal(lblKvKg2.Text);
                    pbb.KvEfis = Convert.ToDecimal(lblKvEfis.Text);
                    pbb.BkBimaKg = Convert.ToDecimal(lblKBimaBK.Text);
                    pbb.SampahBima = Convert.ToDecimal(lblKBimaSampah.Text);
                    pbb.BbBimaKg = Convert.ToDecimal(lblKBimaBB.Text);
                    pbb.KsKg0 = Convert.ToDecimal(lblKsKg0.Text);
                    pbb.KsKg = Convert.ToDecimal(lblKsKg.Text);
                    pbb.KsEfis = Convert.ToDecimal(lblKsEfis.Text);
                    pbb.KuKg = Convert.ToDecimal(lblKuKg.Text);
                    pbb.KuEfs = Convert.ToDecimal(lblKuEfs.Text);
                    pbb.Kgrade2Kg = Convert.ToDecimal(lblKgrade2Kg.Text);
                    pbb.Kgrade2Eff = Convert.ToDecimal(lblKgrade2Eff.Text);
                    pbb.KraftBkKg = Convert.ToDecimal(lblKgKraftPlastikktr.Text);
                    pbb.SampahKraft = Convert.ToDecimal(lblKgKraftPlastikSmph.Text);
                    pbb.KraftBbKg = Convert.ToDecimal(lblKgKraftPlastikBrsh.Text);
                    pbb.Kgrade3Kg = Convert.ToDecimal(lblKgrade3Kg.Text);
                    pbb.Kgrade3xKg = Convert.ToDecimal(lblKgrade3Kg2.Text);
                    pbb.Kgrade3Eff = Convert.ToDecimal(lblKgrade3Eff.Text);
                    pbb.TEfis = Convert.ToDecimal(lblTEfis.Text);
                    pbb.Kv = Convert.ToDecimal(txtKV.Text);
                    pbb.Kb = Convert.ToDecimal(txtKB.Text);
                    pbb.Ks = Convert.ToDecimal(txtKS.Text);
                    pbb.KGradeUtama = Convert.ToDecimal(txtKGradeUtama.Text);
                    pbb.Kgrade2 = Convert.ToDecimal(txtKgrade2.Text);
                    pbb.Kgrade3 = Convert.ToDecimal(txtKgrade3.Text);
                    pbb.CreatedBy = ((Users)Session["Users"]).UserName;
                    pbb.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    if (pbb.ID > 0)
                    {
                        intResult = pbb0.Update(pbb);
                        InsertLog(strEvent);
                    }
                    else
                    {
                        intResult = pbb0.Insert(pbb);
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
                #endregion
            }
            else
            {
                #region Proses Simpan Jombang
                GetValue();
                int intResult = 0;
                string strEvent = "Insert";
                PakaiBahanBaku pbb = new PakaiBahanBaku();
                PakaiBahanBakuFacade pbb0 = new PakaiBahanBakuFacade();
                pbb0 = new PakaiBahanBakuFacade();
                if (ViewState["id"] != null)
                {
                    pbb.ID = int.Parse(ViewState["id"].ToString());
                    strEvent = "Edit";
                }

                ArrayList arrData = (ArrayList)Session["ListBBPemakaian"];
                for (int i = 0; i < C1.Items.Count; i++)
                {
                    intResult = 0;
                    HtmlTableRow tr = (HtmlTableRow)C1.Items[i].FindControl("Tr1c");
                    Label lbl = (Label)C1.Items[i].FindControl("lbltglProd");
                    TextBox txtL1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1");
                    TextBox txtL1x = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x");
                    TextBox txtL1x1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x1");
                    TextBox txtL1x12 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x12");
                    //TextBox txtL2 = (TextBox)C1.Items[i].FindControl("txtjmlL2G2");
                    //TextBox txtL2x = (TextBox)C1.Items[i].FindControl("txtjmlL2G2x");
                    //TextBox txtL2x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x1");
                    //TextBox txtL3 = (TextBox)C1.Items[i].FindControl("txtjmlL3G3");
                    //TextBox txtL3x = (TextBox)C1.Items[i].FindControl("txtjmlL3G3x");
                    //TextBox txtL4 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4");
                    //TextBox txtL4x = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x");
                    //TextBox txtL4x1 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x1");
                    //TextBox txtL5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                    //TextBox txtL5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                    //TextBox txtL5x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x1");
                    //TextBox txtL6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                    //TextBox txtL6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                    Label lbltMix = (Label)C1.Items[i].FindControl("tMix");
                    Label lblFrmula = (Label)C1.Items[i].FindControl("Frmula");
                    TextBox txtSdL = (TextBox)C1.Items[i].FindControl("txtSdL");
                    TextBox txtSspB = (TextBox)C1.Items[i].FindControl("txtSspB");
                    TextBox txtAT = (TextBox)C1.Items[i].FindControl("txtAT");
                    Label txtKL = (Label)C1.Items[i].FindControl("txtKL");
                    Label lblEfis = (Label)C1.Items[i].FindControl("txtEfis");
                    TextBox txtJenisKertasVirgin = (TextBox)C1.Items[i].FindControl("txtJenisKertasVirgin");
                    Label lblKvKg = (Label)C1.Items[i].FindControl("txtKvKg");
                    Label lblKvKg2 = (Label)C1.Items[i].FindControl("txtKvKg2");
                    Label lblKvEfis = (Label)C1.Items[i].FindControl("txtKvEfis");
                    //Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                    //Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                    //Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                    //Label lblKsKg0 = (Label)lstR.Items[i].FindControl("txtKsKg0");
                    Label lblKsKg = (Label)C1.Items[i].FindControl("txtKsKg");
                    Label lblKsEfis = (Label)C1.Items[i].FindControl("txtKsEfis");
                    Label lblKuKg = (Label)C1.Items[i].FindControl("txtKuKg");
                    Label lblKuEfs = (Label)C1.Items[i].FindControl("txtKuEfs");
                    Label lblKgrade2Kg = (Label)C1.Items[i].FindControl("txtKgrade2Kg");
                    Label lblKgrade2Eff = (Label)C1.Items[i].FindControl("txtKgrade2Eff");
                    //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                    //Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                    //Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                    Label lblKgrade3Kg = (Label)C1.Items[i].FindControl("txtKgrade3Kg");
                    //Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
                    Label lblKgrade3Eff = (Label)C1.Items[i].FindControl("txtKgrade3Eff");
                    Label lblTEfis = (Label)C1.Items[i].FindControl("txtTEfis");
                    TextBox txtKV = (TextBox)C1.Items[i].FindControl("txtKV");
                    //TextBox txtKB = (TextBox)lstR.Items[i].FindControl("txtKB");
                    TextBox txtKS = (TextBox)C1.Items[i].FindControl("txtKS");
                    TextBox txtKGradeUtama = (TextBox)C1.Items[i].FindControl("txtKGradeUtama");
                    TextBox txtKgrade2 = (TextBox)C1.Items[i].FindControl("txtKgrade2");
                    TextBox txtKgrade3 = (TextBox)C1.Items[i].FindControl("txtKgrade3");

                    //convert to string
                    string L1G1 = txtL1.Text;
                    string L1G1x = txtL1x.Text;
                    string L1G1x1 = txtL1x1.Text;
                    string L1G1x12 = txtL1x12.Text;
                    //string L2G2 = txtL2.Text;
                    //string L2G2x = txtL2x.Text;
                    //string L2G2x1 = txtL2x1.Text;
                    //string L3G3 = txtL3.Text;
                    //string L3G3x = txtL3x.Text;
                    //string L4G4 = txtL4.Text;
                    //string L4G4x = txtL4x.Text;
                    //string L4G4x1 = txtL4x1.Text;
                    //string L5G5 = txtL5.Text;
                    //string L5G5x = txtL5x.Text;
                    //string L5G5x1 = txtL5x1.Text;
                    //string L6G6 = txtL6.Text;
                    //string L6G6x = txtL6x.Text;
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
                    //string KBimaBK = lblKBimaBK.Text;
                    //string KBimaSampah = lblKBimaSampah.Text;
                    //string KBimaBB = lblKBimaBB.Text;
                    //string KsKg0 = lblKsKg0.Text;
                    string KsKg = lblKsKg.Text;
                    string KsEfis = lblKsEfis.Text;
                    string KuKg = lblKuKg.Text;
                    string KuEfs = lblKuEfs.Text;
                    string Kgrade2Kg = lblKgrade2Kg.Text;
                    string Kgrade2Eff = lblKgrade2Eff.Text;
                    //string KgKraftPlastikktr = lblKgKraftPlastikktr.Text;
                    //string KgKraftPlastikSmph = lblKgKraftPlastikSmph.Text;
                    //string KgKraftPlastikBrsh = lblKgKraftPlastikBrsh.Text;
                    string Kgrade3Kg = lblKgrade3Kg.Text;
                    //string Kgrade3Kg2 = lblKgrade3Kg2.Text;
                    string Kgrade3Eff = lblKgrade3Eff.Text;
                    string TEfis = lblTEfis.Text;
                    string KV = txtKV.Text;
                    //string KB = txtKB.Text;
                    string KS = txtKS.Text;
                    string KGradeUtama = txtKGradeUtama.Text;
                    string Kgrade2 = txtKgrade2.Text;
                    string Kgrade3 = txtKgrade3.Text;

                    pbb.Tgl_Prod = Convert.ToDateTime(lbl.Text);
                    pbb.Tgl_Prod2 = (lbl.Text).ToString();
                    pbb.L1 = Convert.ToDecimal(txtL1.Text);
                    pbb.L1x = Convert.ToDecimal(txtL1x.Text);
                    pbb.L1x1 = Convert.ToDecimal(txtL1x1.Text);
                    pbb.L1x2 = Convert.ToDecimal(txtL1x12.Text);
                    //pbb.L2 = Convert.ToDecimal(txtL2.Text);
                    //pbb.L2x = Convert.ToDecimal(txtL2x.Text);
                    //pbb.L2x1 = Convert.ToDecimal(txtL2x1.Text);
                    //pbb.L3 = Convert.ToDecimal(txtL3.Text);
                    //pbb.L3x = Convert.ToDecimal(txtL3x.Text);
                    //pbb.L4 = Convert.ToDecimal(txtL4.Text);
                    //pbb.L4x = Convert.ToDecimal(txtL4x.Text);
                    //pbb.L4x1 = Convert.ToDecimal(txtL4x1.Text);
                    //pbb.L5 = Convert.ToDecimal(txtL5.Text);
                    //pbb.L5x = Convert.ToDecimal(txtL5x.Text);
                    //pbb.L5x1 = Convert.ToDecimal(txtL5x1.Text);
                    //pbb.L6 = Convert.ToDecimal(txtL6.Text);
                    //pbb.L6x = Convert.ToDecimal(txtL6x.Text);
                    pbb.TMix = Convert.ToDecimal(lbltMix.Text);
                    pbb.Formula = Convert.ToDecimal(lblFrmula.Text);
                    pbb.SdL = Convert.ToDecimal(txtSdL.Text);
                    pbb.SspB = Convert.ToDecimal(txtSspB.Text);
                    pbb.AT = Convert.ToDecimal(txtAT.Text);
                    pbb.KL = Convert.ToDecimal(txtKL.Text);
                    pbb.Efis = Convert.ToDecimal(lblEfis.Text);
                    pbb.JKertasVirgin = (txtJenisKertasVirgin.Text).ToString();
                    pbb.KvKg = Convert.ToDecimal(lblKvKg.Text);
                    pbb.KvKg2 = Convert.ToDecimal(lblKvKg2.Text);
                    pbb.KvEfis = Convert.ToDecimal(lblKvEfis.Text);
                    //pbb.BkBimaKg = Convert.ToDecimal(lblKBimaBK.Text);
                    //pbb.SampahBima = Convert.ToDecimal(lblKBimaSampah.Text);
                    //pbb.BbBimaKg = Convert.ToDecimal(lblKBimaBB.Text);
                    //pbb.KsKg0 = Convert.ToDecimal(lblKsKg0.Text);
                    pbb.KsKg = Convert.ToDecimal(lblKsKg.Text);
                    pbb.KsEfis = Convert.ToDecimal(lblKsEfis.Text);
                    pbb.KuKg = Convert.ToDecimal(lblKuKg.Text);
                    pbb.KuEfs = Convert.ToDecimal(lblKuEfs.Text);
                    pbb.Kgrade2Kg = Convert.ToDecimal(lblKgrade2Kg.Text);
                    pbb.Kgrade2Eff = Convert.ToDecimal(lblKgrade2Eff.Text);
                    //pbb.KraftBkKg = Convert.ToDecimal(lblKgKraftPlastikktr.Text);
                    //pbb.SampahKraft = Convert.ToDecimal(lblKgKraftPlastikSmph.Text);
                    //pbb.KraftBbKg = Convert.ToDecimal(lblKgKraftPlastikBrsh.Text);
                    pbb.Kgrade3Kg = Convert.ToDecimal(lblKgrade3Kg.Text);
                    //pbb.Kgrade3xKg = Convert.ToDecimal(lblKgrade3Kg2.Text);
                    pbb.Kgrade3Eff = Convert.ToDecimal(lblKgrade3Eff.Text);
                    pbb.TEfis = Convert.ToDecimal(lblTEfis.Text);
                    pbb.Kv = Convert.ToDecimal(txtKV.Text);
                    //pbb.Kb = Convert.ToDecimal(txtKB.Text);
                    pbb.Ks = Convert.ToDecimal(txtKS.Text);
                    pbb.KGradeUtama = Convert.ToDecimal(txtKGradeUtama.Text);
                    pbb.Kgrade2 = Convert.ToDecimal(txtKgrade2.Text);
                    pbb.Kgrade3 = Convert.ToDecimal(txtKgrade3.Text);
                    pbb.CreatedBy = ((Users)Session["Users"]).UserName;
                    pbb.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    if (pbb.ID > 0)
                    {
                        intResult = pbb0.Update(pbb);
                        InsertLog(strEvent);
                    }
                    else
                    {
                        intResult = pbb0.InsertJombang(pbb);
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
                #endregion
            }

        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "BahanBakuPulp";
            eventLog.EventName = eventName;
            //eventLog.DocumentNo = txtTanggal.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
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

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            // priview();  
            //btnSimpan.Enabled = true;
            //Session["FlagLap"] = 0;
            LoadPreview();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            ArrayList arrData = new ArrayList();
            for (int i = 0; i < lstR2.Items.Count; i++)
            {
                TextBox L1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL1G1");
                TextBox L1x = (TextBox)lstR2.Items[i].FindControl("txtjmlL1G1x");
                TextBox L2 = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2");
                TextBox L2x = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2x");
                TextBox L2x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2x1");
                TextBox L3 = (TextBox)lstR2.Items[i].FindControl("txtjmlL3G3");
                TextBox L3x = (TextBox)lstR2.Items[i].FindControl("txtjmlL3G3x");
                TextBox L4 = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4");
                TextBox L4x = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4x");
                TextBox L4x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4x1");
                TextBox L5 = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5");
                TextBox L5x = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5x");
                TextBox L5x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5x1");
                TextBox L6 = (TextBox)lstR2.Items[i].FindControl("txtjmlL6G6");
                TextBox L6x = (TextBox)lstR2.Items[i].FindControl("txtjmlL6G6x");

                TextBox SdL = (TextBox)lstR2.Items[i].FindControl("txtSdL");
                TextBox SspB = (TextBox)lstR2.Items[i].FindControl("txtSspB");
                TextBox AT = (TextBox)lstR2.Items[i].FindControl("txtAT");
                TextBox JenisKertasVirgin = (TextBox)lstR2.Items[i].FindControl("txtJenisKertasVirgin");
                TextBox KV = (TextBox)lstR2.Items[i].FindControl("txtKV");
                TextBox KB = (TextBox)lstR2.Items[i].FindControl("txtKB");
                TextBox KS = (TextBox)lstR2.Items[i].FindControl("txtKS");
                TextBox KGU = (TextBox)lstR2.Items[i].FindControl("txtKGradeUtama");
                TextBox KGU2 = (TextBox)lstR2.Items[i].FindControl("txtKgrade2");
                TextBox KGU3 = (TextBox)lstR2.Items[i].FindControl("txtKgrade3");

                L1.ReadOnly = false;
                L1x.ReadOnly = false;
                L2.ReadOnly = false;
                L2x.ReadOnly = false;
                L2x1.ReadOnly = false;
                L3.ReadOnly = false;
                L3x.ReadOnly = false;
                L4.ReadOnly = false;
                L4x.ReadOnly = false;
                L4x1.ReadOnly = false;
                L5.ReadOnly = false;
                L5x.ReadOnly = false;
                L5x1.ReadOnly = false;
                L6.ReadOnly = false;
                L6x.ReadOnly = false;
                SdL.ReadOnly = false;
                SspB.ReadOnly = false;
                AT.ReadOnly = false;
                JenisKertasVirgin.ReadOnly = false;
                KV.ReadOnly = false;
                KB.ReadOnly = false;
                KS.ReadOnly = false;
                KGU.ReadOnly = false;
                KGU2.ReadOnly = false;
                KGU3.ReadOnly = false;

                HtmlTableRow tr = (HtmlTableRow)lstR2.Items[i].FindControl("Tr1x");
            }
            //btnEdit.Visible = false;
            //btnSimpan2.Visible = true;
            //btnSimpan2.Enabled = true;
            btnCalc.Visible = true;

        }

        protected void btnCalc_Click(object sender, EventArgs e)
        {

            #region #1
            string TahunR = DateTime.Now.Year.ToString();
            string BulanR = DateTime.Now.Month.ToString();
            int HariR = DateTime.Now.Day;
            if (HariR < 10)
            { string HariR0 = "0" + HariR.ToString(); Session["HariR"] = HariR0; }
            else
            { string HariR0 = HariR.ToString(); Session["HariR"] = HariR0; }

            if (BulanR == "1") { string BulanString = "Januari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "2") { string BulanString = "Februari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "3") { string BulanString = "Maret"; Session["BulanS"] = BulanString; }
            else if (BulanR == "4") { string BulanString = "April"; Session["BulanS"] = BulanString; }
            else if (BulanR == "5") { string BulanString = "Mei"; Session["BulanS"] = BulanString; }
            else if (BulanR == "6") { string BulanString = "Juni"; Session["BulanS"] = BulanString; }
            else if (BulanR == "7") { string BulanString = "Juli"; Session["BulanS"] = BulanString; }
            else if (BulanR == "8") { string BulanString = "Agustus"; Session["BulanS"] = BulanString; }
            else if (BulanR == "9") { string BulanString = "September"; Session["BulanS"] = BulanString; }
            else if (BulanR == "10") { string BulanString = "Oktober"; Session["BulanS"] = BulanString; }
            else if (BulanR == "11") { string BulanString = "November"; Session["BulanS"] = BulanString; }
            else if (BulanR == "12") { string BulanString = "Desember"; Session["BulanS"] = BulanString; }
            string BulanAlias = Session["BulanS"].ToString();
            string HariAlias = Session["HariR"].ToString();
            string TglAliasR = HariAlias + "-" + BulanAlias + "-" + TahunR; Session["Tgl"] = TglAliasR;

            Users user = (Users)Session["Users"];
            int Bulan1 = int.Parse(ddlBulan.SelectedValue);
            string Tahun = ddlTahun.SelectedValue.ToString();
            if (Bulan1 < 10)
            {
                string Bulan2 = "0" + Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }
            else
            {
                string Bulan2 = Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }

            string Periode = Tahun + Session["Bulan2"].ToString() + "01";
            string PeriodeBulanTahun = Tahun + Session["Bulan2"].ToString();
            Session["PeriodeBulanTahun"] = PeriodeBulanTahun;

            PakaiBahanBaku PakaiBB = new PakaiBahanBaku();
            PakaiBahanBakuFacade pakaiBBF = new PakaiBahanBakuFacade();
            #endregion
            #region rules#1
            int CekInput = pakaiBBF.RetrieveInputan(PeriodeBulanTahun);
            Session["Cek"] = CekInput;
            if (CekInput > 0)
            {
                if (user.UnitKerjaID == 1)
                {
                    #region  Prosess AdaRecord
                    decimal tMix = 0;
                    ArrayList arrData = new ArrayList();
                    for (int i = 0; i < C2.Items.Count; i++)
                    {
                        PakaiBahanBaku pbk = new PakaiBahanBaku();
                        HiddenField hd = (HiddenField)C2.Items[i].FindControl("txtNilaiID");
                        Label lbltgl = (Label)C2.Items[i].FindControl("lbltglProd");
                        TextBox L1 = (TextBox)C2.Items[i].FindControl("txtjmlL1G1");
                        TextBox L1x = (TextBox)C2.Items[i].FindControl("txtjmlL1G1x");
                        TextBox L1x1 = (TextBox)C2.Items[i].FindControl("txtjmlL1G1x1");
                        TextBox L1x2 = (TextBox)C2.Items[i].FindControl("txtjmlL1G1x12");
                        TextBox L2 = (TextBox)C2.Items[i].FindControl("txtjmlL2G2");
                        TextBox L2x = (TextBox)C2.Items[i].FindControl("txtjmlL2G2x");
                        TextBox L2x1 = (TextBox)C2.Items[i].FindControl("txtjmlL2G2x1");
                        TextBox L3 = (TextBox)C2.Items[i].FindControl("txtjmlL3G3");
                        TextBox L3x = (TextBox)C2.Items[i].FindControl("txtjmlL3G3x");
                        TextBox L4 = (TextBox)C2.Items[i].FindControl("txtjmlL4G4");
                        TextBox L4x = (TextBox)C2.Items[i].FindControl("txtjmlL4G4x");
                        TextBox L4x1 = (TextBox)C2.Items[i].FindControl("txtjmlL4G4x1");
                        Label lbltMix = (Label)C2.Items[i].FindControl("tMix");
                        Label lblFrmula = (Label)C2.Items[i].FindControl("Frmula");
                        Label lblKL = (Label)C2.Items[i].FindControl("txtKL");
                        TextBox SdL = (TextBox)C2.Items[i].FindControl("txtSdL");
                        TextBox SspB = (TextBox)C2.Items[i].FindControl("txtSspB");
                        TextBox AT = (TextBox)C2.Items[i].FindControl("txtAT");
                        Label lblEfis = (Label)C2.Items[i].FindControl("txtEfis");
                        Label lblKvKg = (Label)C2.Items[i].FindControl("txtKvKg");
                        Label lblKvKg2 = (Label)C2.Items[i].FindControl("txtKvKg2");
                        Label lblKvEfis = (Label)C2.Items[i].FindControl("txtKvEfis");
                        TextBox KV = (TextBox)C2.Items[i].FindControl("txtKV");
                        TextBox KS = (TextBox)C2.Items[i].FindControl("txtKS");
                        TextBox KGU = (TextBox)C2.Items[i].FindControl("txtKGradeUtama");
                        TextBox KGU2 = (TextBox)C2.Items[i].FindControl("txtKgrade2");
                        TextBox KGU3 = (TextBox)C2.Items[i].FindControl("txtKgrade3");
                        Label lblKsKg = (Label)C2.Items[i].FindControl("txtKsKg");
                        Label lblKsEfis = (Label)C2.Items[i].FindControl("txtKsEfis");
                        Label lblKuKg = (Label)C2.Items[i].FindControl("txtKuKg");
                        Label lblKuEfs = (Label)C2.Items[i].FindControl("txtKuEfs");
                        Label lblKgrade2Kg = (Label)C2.Items[i].FindControl("txtKgrade2Kg");
                        Label lblKgrade2Eff = (Label)C2.Items[i].FindControl("txtKgrade2Eff");
                        Label lblKgrade3Kg = (Label)C2.Items[i].FindControl("txtKgrade3Kg");
                        Label lblKgrade3Eff = (Label)C2.Items[i].FindControl("txtKgrade3Eff");
                        Label lblTEfis = (Label)C2.Items[i].FindControl("txtTEfis");
                        HtmlTableRow tr = (HtmlTableRow)C2.Items[i].FindControl("Tr1c2");
                        lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L1x1.Text)) + (decimal.Parse(L1x2.Text)) + (decimal.Parse(L2.Text)) + (decimal.Parse(L2x.Text)) + (decimal.Parse(L3.Text)) + (decimal.Parse(L3x.Text)) + (decimal.Parse(L4.Text)) + (decimal.Parse(L4x.Text)) + (decimal.Parse(L4x1.Text))).ToString("N0");
                        lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(lblfc1zf.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(lblfc1zf1.Text))) + ((decimal.Parse(L1x1.Text)) * (decimal.Parse(lblfc1zf1b.Text))) + ((decimal.Parse(L1x2.Text)) * (decimal.Parse(lblfc1zf1c.Text))) + ((decimal.Parse(L2.Text)) * (decimal.Parse(lblfc2zf.Text))) + ((decimal.Parse(L2x.Text)) * (decimal.Parse(lblfc2zf1.Text)))
                                        + (decimal.Parse(L3.Text)) * (decimal.Parse(lblfc3zf.Text)) + (decimal.Parse(L3x.Text)) * (decimal.Parse(lblfc3zaf1.Text)) + (decimal.Parse(L4.Text)) * (decimal.Parse(lblfc4zf.Text)) + (decimal.Parse(L4x.Text)) * (decimal.Parse(lblfc4zaf1.Text)) + (decimal.Parse(L4x1.Text)) * (decimal.Parse(lblfc4zaf12.Text))).ToString("N0");
                        lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                        #region KV
                        decimal satukomasatu;
                        decimal satukomalima;
                        decimal satukomadua;
                        satukomasatu = 1.10m;
                        satukomalima = 1.15m;
                        satukomadua = 1.20m;
                        //perubahan formula 0-100*1.5
                        if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 100)
                        {
                            lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                        }
                        //else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                        //}
                        #endregion
                        //lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                        lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                        //lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima0.Text))) / 100).ToString("N0");
                        //lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                        //lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                        //lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                        //lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                        #region #1
                        try
                        {

                            lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #2
                        try
                        {

                            lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #3
                        try
                        {

                            lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #4
                        try
                        {

                            lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #5
                        try
                        {

                            lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region#6
                        try
                        {


                            lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text))))) - (decimal.Parse(lblKgrade2Eff.Text))).ToString("N2");

                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion

                        #region hide2
                        //}
                        //else
                        //{
                        //    hd.Value = "0";
                        //    pbk.ID = 0;
                        //}
                        #endregion
                    }
                    #endregion
                }
                else if (user.UnitKerjaID == 7)
                {
                    #region  Prosess AdaRecord
                    decimal tMix = 0;
                    ArrayList arrData = new ArrayList();
                    for (int i = 0; i < lstR2.Items.Count; i++)
                    {
                        PakaiBahanBaku pbk = new PakaiBahanBaku();
                        HiddenField hd = (HiddenField)lstR2.Items[i].FindControl("txtNilaiID");
                        Label lbltgl = (Label)lstR2.Items[i].FindControl("lbltglProd");
                        TextBox L1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL1G1");
                        TextBox L1x = (TextBox)lstR2.Items[i].FindControl("txtjmlL1G1x");
                        TextBox L2 = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2");
                        TextBox L2x = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2x");
                        TextBox L2x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL2G2x1");
                        TextBox L3 = (TextBox)lstR2.Items[i].FindControl("txtjmlL3G3");
                        TextBox L3x = (TextBox)lstR2.Items[i].FindControl("txtjmlL3G3x");
                        TextBox L4 = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4");
                        TextBox L4x = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4x");
                        TextBox L4x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL4G4x1");
                        TextBox L5 = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5");
                        TextBox L5x = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5x");
                        TextBox L5x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL5G5x1");
                        TextBox L6 = (TextBox)lstR2.Items[i].FindControl("txtjmlL6G6");
                        TextBox L6x = (TextBox)lstR2.Items[i].FindControl("txtjmlL6G6x");
                        TextBox L6x1 = (TextBox)lstR2.Items[i].FindControl("txtjmlL6G6x1");
                        Label lbltMix = (Label)lstR2.Items[i].FindControl("tMix");
                        Label lblFrmula = (Label)lstR2.Items[i].FindControl("Frmula");
                        Label lblKL = (Label)lstR2.Items[i].FindControl("txtKL");
                        TextBox SdL = (TextBox)lstR2.Items[i].FindControl("txtSdL");
                        TextBox SspB = (TextBox)lstR2.Items[i].FindControl("txtSspB");
                        TextBox AT = (TextBox)lstR2.Items[i].FindControl("txtAT");
                        Label lblEfis = (Label)lstR2.Items[i].FindControl("txtEfis");
                        Label lblKvKg = (Label)lstR2.Items[i].FindControl("txtKvKg");
                        Label lblKvKg2 = (Label)lstR2.Items[i].FindControl("txtKvKg2");
                        Label lblKvEfis = (Label)lstR2.Items[i].FindControl("txtKvEfis");
                        TextBox KV = (TextBox)lstR2.Items[i].FindControl("txtKV");
                        TextBox KB = (TextBox)lstR2.Items[i].FindControl("txtKB");
                        TextBox KS = (TextBox)lstR2.Items[i].FindControl("txtKS");
                        TextBox KGU = (TextBox)lstR2.Items[i].FindControl("txtKGradeUtama");
                        TextBox KGU2 = (TextBox)lstR2.Items[i].FindControl("txtKgrade2");
                        TextBox KGU3 = (TextBox)lstR2.Items[i].FindControl("txtKgrade3");
                        Label lblKBimaSampah = (Label)lstR2.Items[i].FindControl("txtKBimaSampah");
                        Label lblKBimaBK = (Label)lstR2.Items[i].FindControl("txtKBimaBK");
                        Label lblKBimaBB = (Label)lstR2.Items[i].FindControl("txtKBimaBB");
                        Label lblKsKg = (Label)lstR2.Items[i].FindControl("txtKsKg");
                        Label lblKsKg0 = (Label)lstR2.Items[i].FindControl("txtKsKg0");
                        Label lblKsEfis = (Label)lstR2.Items[i].FindControl("txtKsEfis");
                        Label lblKuKg = (Label)lstR2.Items[i].FindControl("txtKuKg");
                        Label lblKuEfs = (Label)lstR2.Items[i].FindControl("txtKuEfs");
                        Label lblKgrade2Kg = (Label)lstR2.Items[i].FindControl("txtKgrade2Kg");
                        Label lblKgrade2Eff = (Label)lstR2.Items[i].FindControl("txtKgrade2Eff");
                        Label PerKraftSmpahPlastik = (Label)lstR2.Items[i].FindControl("txtPerKraftSmpahPlastik");
                        Label lblKgKraftPlastikSmph = (Label)lstR2.Items[i].FindControl("txtKgKraftPlastikSmph");
                        //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                        Label lblKgKraftPlastikBrsh = (Label)lstR2.Items[i].FindControl("txtKgKraftPlastikBrsh");
                        Label lblKgKraftPlastikktr = (Label)lstR2.Items[i].FindControl("txtKgKraftPlastikktr");
                        Label lblKgrade3Kg = (Label)lstR2.Items[i].FindControl("txtKgrade3Kg");
                        Label lblKgrade3Kg2 = (Label)lstR2.Items[i].FindControl("txtKgrade3Kg2");
                        Label lblKgrade3Eff = (Label)lstR2.Items[i].FindControl("txtKgrade3Eff");
                        Label lblTEfis = (Label)lstR2.Items[i].FindControl("txtTEfis");

                        HtmlTableRow tr = (HtmlTableRow)lstR2.Items[i].FindControl("Tr1x");
                        lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L2.Text)) + (decimal.Parse(L2x.Text)) + (decimal.Parse(L2x1.Text)) + (decimal.Parse(L3.Text)) + (decimal.Parse(L3x.Text)) + (decimal.Parse(L4.Text)) + (decimal.Parse(L4x.Text)) + (decimal.Parse(L4x1.Text)) + (decimal.Parse(L5.Text)) + (decimal.Parse(L5x.Text)) + (decimal.Parse(L5x1.Text)) + (decimal.Parse(L6.Text)) + (decimal.Parse(L6x.Text)) + (decimal.Parse(L6x1.Text))).ToString("N0");
                        lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(frmula1x.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(frmula1ax.Text))) + ((decimal.Parse(L2.Text)) * (decimal.Parse(frmula2x.Text))) + ((decimal.Parse(L2x.Text)) * (decimal.Parse(frmula2ax.Text))) + ((decimal.Parse(L2x1.Text)) * (decimal.Parse(frmula2bx.Text)))
                                        + (decimal.Parse(L3.Text)) * (decimal.Parse(frmula3x.Text)) + (decimal.Parse(L3x.Text)) * (decimal.Parse(frmula3ax.Text)) + (decimal.Parse(L4.Text)) * (decimal.Parse(frmula3a1x.Text)) + (decimal.Parse(L4x.Text)) * (decimal.Parse(frmula4x.Text)) + (decimal.Parse(L4x1.Text)) * (decimal.Parse(frmula4ax.Text)) + (decimal.Parse(L5.Text)) * (decimal.Parse(frmula4a1x.Text)) + (decimal.Parse(L5x.Text)) * (decimal.Parse(frmula5x.Text)) + +(decimal.Parse(L5x1.Text)) * (decimal.Parse(frmula5ax.Text))
                                        + (decimal.Parse(L6.Text)) * (decimal.Parse(frmula6x.Text)) + (decimal.Parse(L6x.Text)) * (decimal.Parse(frmula6ax.Text)) + (decimal.Parse(L6x1.Text)) * (decimal.Parse(frmula6ax1.Text))).ToString("N0");
                        lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                        #region KV
                        decimal satukomasatu;
                        decimal satukomalima;
                        decimal satukomadua;
                        satukomasatu = 1.10m;
                        satukomalima = 1.15m;
                        satukomadua = 1.20m;
                        if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 100)
                        {
                            lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                        }
                        //else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                        //}
                        #endregion
                        lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                        lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                        lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima0.Text))) / 100).ToString("N0");
                        lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                        lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                        lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                        lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                        #region #1
                        try
                        {

                            lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #2
                        try
                        {

                            lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #3
                        try
                        {

                            lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #4
                        try
                        {

                            lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg2.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #5
                        try
                        {

                            lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg2.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region#6
                        try
                        {

                            //lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text)))) - (decimal.Parse(lblKgrade2Eff.Text)))).ToString("N2");
                            lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text))))) - (decimal.Parse(lblKgrade2Eff.Text))).ToString("N2");

                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion

                        #region hide2
                        //}
                        //else
                        //{
                        //    hd.Value = "0";
                        //    pbk.ID = 0;
                        //}
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region  Prosess AdaRecord
                    decimal tMix = 0;
                    ArrayList arrData = new ArrayList();
                    for (int i = 0; i < C2.Items.Count; i++)
                    {
                        PakaiBahanBaku pbk = new PakaiBahanBaku();
                        HiddenField hd = (HiddenField)C2.Items[i].FindControl("txtNilaiID");
                        Label lbltgl = (Label)C2.Items[i].FindControl("lbltglProd");
                        TextBox L1 = (TextBox)C2.Items[i].FindControl("txtjmlL1G1");
                        TextBox L1x = (TextBox)C2.Items[i].FindControl("txtjmlL1G1x");
                        TextBox L1x1 = (TextBox)C2.Items[i].FindControl("txtjmlL1G1x1");
                        TextBox L1x2 = (TextBox)C2.Items[i].FindControl("txtjmlL1G1x12");
                        //TextBox L2 = (TextBox)C2.Items[i].FindControl("txtjmlL2G2");
                        //TextBox L2x = (TextBox)C2.Items[i].FindControl("txtjmlL2G2x");
                        //TextBox L2x1 = (TextBox)C2.Items[i].FindControl("txtjmlL2G2x1");
                        //TextBox L3 = (TextBox)C2.Items[i].FindControl("txtjmlL3G3");
                        //TextBox L3x = (TextBox)C2.Items[i].FindControl("txtjmlL3G3x");
                        //TextBox L4 = (TextBox)C2.Items[i].FindControl("txtjmlL4G4");
                        //TextBox L4x = (TextBox)C2.Items[i].FindControl("txtjmlL4G4x");
                        //TextBox L4x1 = (TextBox)C2.Items[i].FindControl("txtjmlL4G4x1");
                        Label lbltMix = (Label)C2.Items[i].FindControl("tMix");
                        Label lblFrmula = (Label)C2.Items[i].FindControl("Frmula");
                        Label lblKL = (Label)C2.Items[i].FindControl("txtKL");
                        TextBox SdL = (TextBox)C2.Items[i].FindControl("txtSdL");
                        TextBox SspB = (TextBox)C2.Items[i].FindControl("txtSspB");
                        TextBox AT = (TextBox)C2.Items[i].FindControl("txtAT");
                        Label lblEfis = (Label)C2.Items[i].FindControl("txtEfis");
                        Label lblKvKg = (Label)C2.Items[i].FindControl("txtKvKg");
                        Label lblKvKg2 = (Label)C2.Items[i].FindControl("txtKvKg2");
                        Label lblKvEfis = (Label)C2.Items[i].FindControl("txtKvEfis");
                        TextBox KV = (TextBox)C2.Items[i].FindControl("txtKV");
                        TextBox KS = (TextBox)C2.Items[i].FindControl("txtKS");
                        TextBox KGU = (TextBox)C2.Items[i].FindControl("txtKGradeUtama");
                        TextBox KGU2 = (TextBox)C2.Items[i].FindControl("txtKgrade2");
                        TextBox KGU3 = (TextBox)C2.Items[i].FindControl("txtKgrade3");
                        Label lblKsKg = (Label)C2.Items[i].FindControl("txtKsKg");
                        Label lblKsEfis = (Label)C2.Items[i].FindControl("txtKsEfis");
                        Label lblKuKg = (Label)C2.Items[i].FindControl("txtKuKg");
                        Label lblKuEfs = (Label)C2.Items[i].FindControl("txtKuEfs");
                        Label lblKgrade2Kg = (Label)C2.Items[i].FindControl("txtKgrade2Kg");
                        Label lblKgrade2Eff = (Label)C2.Items[i].FindControl("txtKgrade2Eff");
                        Label lblKgrade3Kg = (Label)C2.Items[i].FindControl("txtKgrade3Kg");
                        Label lblKgrade3Eff = (Label)C2.Items[i].FindControl("txtKgrade3Eff");
                        Label lblTEfis = (Label)C2.Items[i].FindControl("txtTEfis");
                        HtmlTableRow tr = (HtmlTableRow)C2.Items[i].FindControl("Tr1c2");
                        lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L1x1.Text)) + (decimal.Parse(L1x2.Text))).ToString("N0");
                        lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(lblfc1zf.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(lblfc1zf1.Text))) + ((decimal.Parse(L1x1.Text)) * (decimal.Parse(lblfc1zf1b.Text))) + ((decimal.Parse(L1x2.Text)) * (decimal.Parse(lblfc1zf1c.Text)))).ToString("N0");
                        lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                        #region KV
                        decimal satukomasatu;
                        decimal satukomalima;
                        decimal satukomadua;
                        satukomasatu = 1.10m;
                        satukomalima = 1.15m;
                        satukomadua = 1.20m;
                        if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 100)
                        {
                            lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                        }
                        //else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                        //}
                        #endregion
                        //lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                        lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                        //lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima0.Text))) / 100).ToString("N0");
                        //lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                        //lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                        //lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                        //lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                        #region #1
                        try
                        {

                            lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #2
                        try
                        {

                            lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #3
                        try
                        {

                            lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #4
                        try
                        {

                            lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #5
                        try
                        {

                            lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region#6
                        try
                        {


                            lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text))))) - (decimal.Parse(lblKgrade2Eff.Text))).ToString("N2");

                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion

                        #region hide2
                        //}
                        //else
                        //{
                        //    hd.Value = "0";
                        //    pbk.ID = 0;
                        //}
                        #endregion
                    }
                    #endregion
                }

            }
            else if (CekInput == 0)
            {
                if (user.UnitKerjaID == 1)
                {
                    #region  Prosess noRecord Ctrp
                    decimal tMix = 0;
                    ArrayList arrData = new ArrayList();
                    for (int i = 0; i < C1.Items.Count; i++)
                    {
                        PakaiBahanBaku pbk = new PakaiBahanBaku();
                        HiddenField hd = (HiddenField)C1.Items[i].FindControl("txtNilaiID");
                        Label lbltgl = (Label)C1.Items[i].FindControl("lbltglProd");
                        TextBox L1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1");
                        TextBox L1x = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x");
                        TextBox L1x1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x1");
                        TextBox L1x2 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x12");
                        TextBox L2 = (TextBox)C1.Items[i].FindControl("txtjmlL2G2");
                        TextBox L2x = (TextBox)C1.Items[i].FindControl("txtjmlL2G2x");
                        //TextBox L2x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x1");
                        TextBox L3 = (TextBox)C1.Items[i].FindControl("txtjmlL3G3");
                        TextBox L3x = (TextBox)C1.Items[i].FindControl("txtjmlL3G3x");
                        TextBox L4 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4");
                        TextBox L4x = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x");
                        TextBox L4x1 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x1");
                        //TextBox L5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                        //TextBox L5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                        //TextBox L5x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x1");
                        //TextBox L6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                        //TextBox L6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                        Label lbltMix = (Label)C1.Items[i].FindControl("tMix");
                        Label lblFrmula = (Label)C1.Items[i].FindControl("Frmula");
                        Label lblKL = (Label)C1.Items[i].FindControl("txtKL");
                        TextBox SdL = (TextBox)C1.Items[i].FindControl("txtSdL");
                        TextBox SspB = (TextBox)C1.Items[i].FindControl("txtSspB");
                        TextBox AT = (TextBox)C1.Items[i].FindControl("txtAT");
                        Label lblEfis = (Label)C1.Items[i].FindControl("txtEfis");
                        Label lblKvKg = (Label)C1.Items[i].FindControl("txtKvKg");
                        Label lblKvKg2 = (Label)C1.Items[i].FindControl("txtKvKg2");
                        Label lblKvEfis = (Label)C1.Items[i].FindControl("txtKvEfis");
                        TextBox KV = (TextBox)C1.Items[i].FindControl("txtKV");
                        //TextBox KB = (TextBox)lstR.Items[i].FindControl("txtKB");
                        TextBox KS = (TextBox)C1.Items[i].FindControl("txtKS");
                        TextBox KGU = (TextBox)C1.Items[i].FindControl("txtKGradeUtama");
                        TextBox KGU2 = (TextBox)C1.Items[i].FindControl("txtKgrade2");
                        TextBox KGU3 = (TextBox)C1.Items[i].FindControl("txtKgrade3");
                        //Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                        //Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                        //Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                        Label lblKsKg = (Label)C1.Items[i].FindControl("txtKsKg");
                        //Label lblKsKg0 = (Label)C1.Items[i].FindControl("txtKsKg0");
                        Label lblKsEfis = (Label)C1.Items[i].FindControl("txtKsEfis");
                        Label lblKuKg = (Label)C1.Items[i].FindControl("txtKuKg");
                        Label lblKuEfs = (Label)C1.Items[i].FindControl("txtKuEfs");
                        Label lblKgrade2Kg = (Label)C1.Items[i].FindControl("txtKgrade2Kg");
                        Label lblKgrade2Eff = (Label)C1.Items[i].FindControl("txtKgrade2Eff");
                        //Label PerKraftSmpahPlastik = (Label)lstR.Items[i].FindControl("txtPerKraftSmpahPlastik");
                        //Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                        //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                        //Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                        //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                        Label lblKgrade3Kg = (Label)C1.Items[i].FindControl("txtKgrade3Kg");
                        //Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
                        Label lblKgrade3Eff = (Label)C1.Items[i].FindControl("txtKgrade3Eff");
                        Label lblTEfis = (Label)C1.Items[i].FindControl("txtTEfis");

                        HtmlTableRow tr = (HtmlTableRow)C1.Items[i].FindControl("Tr1");
                        lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L1x1.Text)) + (decimal.Parse(L1x2.Text)) + (decimal.Parse(L2.Text)) + (decimal.Parse(L2x.Text)) + (decimal.Parse(L3.Text)) + (decimal.Parse(L3x.Text)) + (decimal.Parse(L4.Text)) + (decimal.Parse(L4x.Text)) + (decimal.Parse(L4x1.Text))).ToString("N0");

                        lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(lblfc1x.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(lblfc1ax.Text))) + ((decimal.Parse(L1x1.Text)) * (decimal.Parse(lblfc1ax0.Text))) + ((decimal.Parse(L1x2.Text)) * (decimal.Parse(lblfc1ax01.Text))) + ((decimal.Parse(L2.Text)) * (decimal.Parse(lblfc2x.Text))) + ((decimal.Parse(L2x.Text)) * (decimal.Parse(lblfc2ax.Text)))
                                        + (decimal.Parse(L3.Text)) * (decimal.Parse(lblfc3x.Text)) + (decimal.Parse(L3x.Text)) * (decimal.Parse(lblfc3ax.Text)) + (decimal.Parse(L4.Text)) * (decimal.Parse(lblfc4x.Text)) + (decimal.Parse(L4x.Text)) * (decimal.Parse(lblfc4ax.Text)) + (decimal.Parse(L4x1.Text)) * (decimal.Parse(lblfc4ax1.Text))).ToString("N0");
                        lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                        #region KV
                        decimal satukomasatu;
                        decimal satukomalima;
                        decimal satukomadua;
                        satukomasatu = 1.10m;
                        satukomalima = 1.15m;
                        satukomadua = 1.20m;
                        if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 100)
                        {
                            lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                        }
                        //else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                        //}
                        #endregion
                        //lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                        lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                        //lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima.Text))) / 100).ToString("N0");
                        //lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                        //lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                        //lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                        //lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                        #region #1
                        try
                        {

                            lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #2
                        try
                        {

                            lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #3
                        try
                        {

                            lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #4
                        try
                        {

                            lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #5
                        try
                        {

                            lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region#6
                        try
                        {

                            //lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text)))) - (decimal.Parse(lblKgrade2Eff.Text)))).ToString("N2");
                            lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text))))) - (decimal.Parse(lblKgrade2Eff.Text))).ToString("N2");

                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion

                        #region hide2
                        //}
                        //else
                        //{
                        //    hd.Value = "0";
                        //    pbk.ID = 0;
                        //}
                        #endregion
                    }
                    #endregion
                }
                else if (user.UnitKerjaID == 7)
                {
                    #region  Prosess noRecord Krwg
                    decimal tMix = 0;
                    ArrayList arrData = new ArrayList();
                    for (int i = 0; i < lstR.Items.Count; i++)
                    {
                        PakaiBahanBaku pbk = new PakaiBahanBaku();
                        HiddenField hd = (HiddenField)lstR.Items[i].FindControl("txtNilaiID");
                        Label lbltgl = (Label)lstR.Items[i].FindControl("lbltglProd");
                        TextBox L1 = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1");
                        TextBox L1x = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1x");
                        TextBox L2 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2");
                        TextBox L2x = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x");
                        TextBox L2x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x1");
                        TextBox L3 = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3");
                        TextBox L3x = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3x");
                        TextBox L4 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4");
                        TextBox L4x = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x");
                        TextBox L4x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x1");
                        TextBox L5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                        TextBox L5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                        TextBox L5x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x1");
                        TextBox L6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                        TextBox L6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                        TextBox L6x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x1");
                        Label lbltMix = (Label)lstR.Items[i].FindControl("tMix");
                        Label lblFrmula = (Label)lstR.Items[i].FindControl("Frmula");
                        Label lblKL = (Label)lstR.Items[i].FindControl("txtKL");
                        TextBox SdL = (TextBox)lstR.Items[i].FindControl("txtSdL");
                        TextBox SspB = (TextBox)lstR.Items[i].FindControl("txtSspB");
                        TextBox AT = (TextBox)lstR.Items[i].FindControl("txtAT");
                        Label lblEfis = (Label)lstR.Items[i].FindControl("txtEfis");
                        Label lblKvKg = (Label)lstR.Items[i].FindControl("txtKvKg");
                        Label lblKvKg2 = (Label)lstR.Items[i].FindControl("txtKvKg2");
                        Label lblKvEfis = (Label)lstR.Items[i].FindControl("txtKvEfis");
                        TextBox KV = (TextBox)lstR.Items[i].FindControl("txtKV");
                        TextBox KB = (TextBox)lstR.Items[i].FindControl("txtKB");
                        TextBox KS = (TextBox)lstR.Items[i].FindControl("txtKS");
                        TextBox KGU = (TextBox)lstR.Items[i].FindControl("txtKGradeUtama");
                        TextBox KGU2 = (TextBox)lstR.Items[i].FindControl("txtKgrade2");
                        TextBox KGU3 = (TextBox)lstR.Items[i].FindControl("txtKgrade3");
                        Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                        Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                        Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                        Label lblKsKg = (Label)lstR.Items[i].FindControl("txtKsKg");
                        Label lblKsKg0 = (Label)lstR.Items[i].FindControl("txtKsKg0");
                        Label lblKsEfis = (Label)lstR.Items[i].FindControl("txtKsEfis");
                        Label lblKuKg = (Label)lstR.Items[i].FindControl("txtKuKg");
                        Label lblKuEfs = (Label)lstR.Items[i].FindControl("txtKuEfs");
                        Label lblKgrade2Kg = (Label)lstR.Items[i].FindControl("txtKgrade2Kg");
                        Label lblKgrade2Eff = (Label)lstR.Items[i].FindControl("txtKgrade2Eff");
                        Label PerKraftSmpahPlastik = (Label)lstR.Items[i].FindControl("txtPerKraftSmpahPlastik");
                        Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                        //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                        Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                        Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                        Label lblKgrade3Kg = (Label)lstR.Items[i].FindControl("txtKgrade3Kg");
                        Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
                        Label lblKgrade3Eff = (Label)lstR.Items[i].FindControl("txtKgrade3Eff");
                        Label lblTEfis = (Label)lstR.Items[i].FindControl("txtTEfis");

                        HtmlTableRow tr = (HtmlTableRow)lstR.Items[i].FindControl("Tr1");
                        lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L2.Text)) + (decimal.Parse(L2x.Text)) + (decimal.Parse(L2x1.Text)) + (decimal.Parse(L3.Text)) + (decimal.Parse(L3x.Text)) + (decimal.Parse(L4.Text)) + (decimal.Parse(L4x.Text)) + (decimal.Parse(L4x1.Text)) + (decimal.Parse(L5.Text)) + (decimal.Parse(L5x.Text)) + (decimal.Parse(L5x1.Text)) + (decimal.Parse(L6.Text)) + (decimal.Parse(L6x.Text)) + (decimal.Parse(L6x1.Text))).ToString("N0");
                        lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(frmula1.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(frmula1a.Text))) + ((decimal.Parse(L2.Text)) * (decimal.Parse(frmula2.Text))) + ((decimal.Parse(L2x.Text)) * (decimal.Parse(frmula2a.Text))) + ((decimal.Parse(L2x1.Text)) * (decimal.Parse(frmula2b.Text)))
                                        + (decimal.Parse(L3.Text)) * (decimal.Parse(frmula3.Text)) + (decimal.Parse(L3x.Text)) * (decimal.Parse(frmula3a.Text)) + (decimal.Parse(L4.Text)) * (decimal.Parse(frmula3a1.Text)) + (decimal.Parse(L4x.Text)) * (decimal.Parse(frmula4.Text)) + (decimal.Parse(L4x1.Text)) * (decimal.Parse(frmula4a.Text)) + (decimal.Parse(L5.Text)) * (decimal.Parse(frmula4a1.Text)) + (decimal.Parse(L5x.Text)) * (decimal.Parse(frmula5.Text)) + +(decimal.Parse(L5x1.Text)) * (decimal.Parse(frmula5a.Text))
                                        + (decimal.Parse(L6.Text)) * (decimal.Parse(frmula6.Text)) + (decimal.Parse(L6x.Text)) * (decimal.Parse(frmula6a.Text)) + (decimal.Parse(L6x1.Text)) * (decimal.Parse(frmula6a1.Text))).ToString("N0");
                        lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                        #region KV
                        decimal satukomasatu;
                        decimal satukomalima;
                        decimal satukomadua;
                        satukomasatu = 1.10m;
                        satukomalima = 1.15m;
                        satukomadua = 1.20m;
                        if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 100)
                        {
                            lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                        }
                        //else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                        //}
                        #endregion
                        lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                        lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                        lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima.Text))) / 100).ToString("N0");
                        lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                        lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                        lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                        lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                        #region #1
                        try
                        {

                            lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #2
                        try
                        {

                            lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #3
                        try
                        {

                            lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #4
                        try
                        {

                            lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg2.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #5
                        try
                        {

                            lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg2.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region#6
                        try
                        {

                            //lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text)))) - (decimal.Parse(lblKgrade2Eff.Text)))).ToString("N2");
                            lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text))))) - (decimal.Parse(lblKgrade2Eff.Text))).ToString("N2");

                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion

                        #region hide2
                        //}
                        //else
                        //{
                        //    hd.Value = "0";
                        //    pbk.ID = 0;
                        //}
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    #region  Prosess noRecord Jombang
                    decimal tMix = 0;
                    ArrayList arrData = new ArrayList();
                    for (int i = 0; i < C1.Items.Count; i++)
                    {
                        PakaiBahanBaku pbk = new PakaiBahanBaku();
                        HiddenField hd = (HiddenField)C1.Items[i].FindControl("txtNilaiID");
                        Label lbltgl = (Label)C1.Items[i].FindControl("lbltglProd");
                        TextBox L1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1");
                        TextBox L1x = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x");
                        TextBox L1x1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x1");
                        TextBox L1x2 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x12");
                        TextBox L2 = (TextBox)C1.Items[i].FindControl("txtjmlL2G2");
                        TextBox L2x = (TextBox)C1.Items[i].FindControl("txtjmlL2G2x");
                        //TextBox L2x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x1");
                        TextBox L3 = (TextBox)C1.Items[i].FindControl("txtjmlL3G3");
                        TextBox L3x = (TextBox)C1.Items[i].FindControl("txtjmlL3G3x");
                        TextBox L4 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4");
                        TextBox L4x = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x");
                        TextBox L4x1 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x1");
                        //TextBox L5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                        //TextBox L5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                        //TextBox L5x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x1");
                        //TextBox L6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                        //TextBox L6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                        Label lbltMix = (Label)C1.Items[i].FindControl("tMix");
                        Label lblFrmula = (Label)C1.Items[i].FindControl("Frmula");
                        Label lblKL = (Label)C1.Items[i].FindControl("txtKL");
                        TextBox SdL = (TextBox)C1.Items[i].FindControl("txtSdL");
                        TextBox SspB = (TextBox)C1.Items[i].FindControl("txtSspB");
                        TextBox AT = (TextBox)C1.Items[i].FindControl("txtAT");
                        Label lblEfis = (Label)C1.Items[i].FindControl("txtEfis");
                        Label lblKvKg = (Label)C1.Items[i].FindControl("txtKvKg");
                        Label lblKvKg2 = (Label)C1.Items[i].FindControl("txtKvKg2");
                        Label lblKvEfis = (Label)C1.Items[i].FindControl("txtKvEfis");
                        TextBox KV = (TextBox)C1.Items[i].FindControl("txtKV");
                        //TextBox KB = (TextBox)lstR.Items[i].FindControl("txtKB");
                        TextBox KS = (TextBox)C1.Items[i].FindControl("txtKS");
                        TextBox KGU = (TextBox)C1.Items[i].FindControl("txtKGradeUtama");
                        TextBox KGU2 = (TextBox)C1.Items[i].FindControl("txtKgrade2");
                        TextBox KGU3 = (TextBox)C1.Items[i].FindControl("txtKgrade3");
                        //Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                        //Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                        //Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                        Label lblKsKg = (Label)C1.Items[i].FindControl("txtKsKg");
                        //Label lblKsKg0 = (Label)C1.Items[i].FindControl("txtKsKg0");
                        Label lblKsEfis = (Label)C1.Items[i].FindControl("txtKsEfis");
                        Label lblKuKg = (Label)C1.Items[i].FindControl("txtKuKg");
                        Label lblKuEfs = (Label)C1.Items[i].FindControl("txtKuEfs");
                        Label lblKgrade2Kg = (Label)C1.Items[i].FindControl("txtKgrade2Kg");
                        Label lblKgrade2Eff = (Label)C1.Items[i].FindControl("txtKgrade2Eff");
                        //Label PerKraftSmpahPlastik = (Label)lstR.Items[i].FindControl("txtPerKraftSmpahPlastik");
                        //Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                        //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                        //Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                        //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                        Label lblKgrade3Kg = (Label)C1.Items[i].FindControl("txtKgrade3Kg");
                        //Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
                        Label lblKgrade3Eff = (Label)C1.Items[i].FindControl("txtKgrade3Eff");
                        Label lblTEfis = (Label)C1.Items[i].FindControl("txtTEfis");

                        HtmlTableRow tr = (HtmlTableRow)C1.Items[i].FindControl("Tr1");
                        lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L1x1.Text)) + (decimal.Parse(L1x2.Text))).ToString("N0");

                        lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(lblfc1x.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(lblfc1ax.Text))) + ((decimal.Parse(L1x1.Text)) * (decimal.Parse(lblfc1ax0.Text))) + ((decimal.Parse(L1x2.Text)) * (decimal.Parse(lblfc1ax01.Text)))).ToString("N0");

                        lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                        #region KV
                        decimal satukomasatu;
                        decimal satukomalima;
                        decimal satukomadua;
                        satukomasatu = 1.10m;
                        satukomalima = 1.15m;
                        satukomadua = 1.20m;
                        if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 100)
                        {
                            lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                        }
                        //else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                        //}
                        //else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                        //{
                        //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                        //}
                        #endregion
                        //lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                        lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                        //lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima.Text))) / 100).ToString("N0");
                        //lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                        //lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                        //lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                        //lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                        #region #1
                        try
                        {

                            lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #2
                        try
                        {

                            lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #3
                        try
                        {

                            lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #4
                        try
                        {

                            lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region #5
                        try
                        {

                            lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N1");
                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion
                        #region#6
                        try
                        {

                            //lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text)))) - (decimal.Parse(lblKgrade2Eff.Text)))).ToString("N2");
                            lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text))))) - (decimal.Parse(lblKgrade2Eff.Text))).ToString("N2");

                        }
                        catch (DivideByZeroException)
                        {

                        }
                        #endregion

                        #region hide2
                        //}
                        //else
                        //{
                        //    hd.Value = "0";
                        //    pbk.ID = 0;
                        //}
                        #endregion
                    }
                    #endregion
                }

            }
            #endregion

            btnSimpan.Enabled = true;
        }

        protected void jmlL1G_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
            {
                #region  Prosess ctrp & Jombang
                decimal tMix = 0;
                ArrayList arrData = new ArrayList();
                for (int i = 0; i < C1.Items.Count; i++)
                {
                    PakaiBahanBaku pbk = new PakaiBahanBaku();
                    HiddenField hd = (HiddenField)C1.Items[i].FindControl("txtNilaiID");
                    Label lbltgl = (Label)C1.Items[i].FindControl("lbltglProd");
                    TextBox L1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1");
                    TextBox L1x = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x");
                    TextBox L1x1 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x1");
                    TextBox L1x2 = (TextBox)C1.Items[i].FindControl("txtjmlL1G1x12");
                    TextBox L2 = (TextBox)C1.Items[i].FindControl("txtjmlL2G2");
                    TextBox L2x = (TextBox)C1.Items[i].FindControl("txtjmlL2G2x");
                    TextBox L3 = (TextBox)C1.Items[i].FindControl("txtjmlL3G3");
                    TextBox L3x = (TextBox)C1.Items[i].FindControl("txtjmlL3G3x");
                    TextBox L4 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4");
                    TextBox L4x = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x");
                    TextBox L4x1 = (TextBox)C1.Items[i].FindControl("txtjmlL4G4x1");
                    Label lbltMix = (Label)C1.Items[i].FindControl("tMix");
                    Label lblFrmula = (Label)C1.Items[i].FindControl("Frmula");
                    Label lblKL = (Label)C1.Items[i].FindControl("txtKL");
                    TextBox SdL = (TextBox)C1.Items[i].FindControl("txtSdL");
                    TextBox SspB = (TextBox)C1.Items[i].FindControl("txtSspB");
                    TextBox AT = (TextBox)C1.Items[i].FindControl("txtAT");
                    Label lblEfis = (Label)C1.Items[i].FindControl("txtEfis");
                    //Label lblKvKg = (Label)lstR.Items[i].FindControl("txtKvKg");
                    Label lblKvKg2 = (Label)C1.Items[i].FindControl("txtKvKg2");
                    Label lblKvEfis = (Label)C1.Items[i].FindControl("txtKvEfis");
                    TextBox KV = (TextBox)C1.Items[i].FindControl("txtKV");
                    TextBox KB = (TextBox)C1.Items[i].FindControl("txtKB");
                    TextBox KS = (TextBox)C1.Items[i].FindControl("txtKS");
                    TextBox KGU = (TextBox)C1.Items[i].FindControl("txtKGradeUtama");
                    TextBox KGU2 = (TextBox)C1.Items[i].FindControl("txtKgrade2");
                    TextBox KGU3 = (TextBox)C1.Items[i].FindControl("txtKgrade3");
                    //Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                    //Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                    //Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                    //Label lblKsKg = (Label)lstR.Items[i].FindControl("txtKsKg");
                    //Label lblKsKg0 = (Label)lstR.Items[i].FindControl("txtKsKg0");
                    Label lblKsEfis = (Label)C1.Items[i].FindControl("txtKsEfis");
                    //Label lblKuKg = (Label)lstR.Items[i].FindControl("txtKuKg");
                    Label lblKuEfs = (Label)C1.Items[i].FindControl("txtKuEfs");
                    //Label lblKgrade2Kg = (Label)lstR.Items[i].FindControl("txtKgrade2Kg");
                    Label lblKgrade2Eff = (Label)C1.Items[i].FindControl("txtKgrade2Eff");
                    //Label PerKraftSmpahPlastik = (Label)lstR.Items[i].FindControl("txtPerKraftSmpahPlastik");
                    //Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                    ////Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                    //Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                    //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                    //Label lblKgrade3Kg = (Label)lstR.Items[i].FindControl("txtKgrade3Kg");
                    //Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
                    Label lblKgrade3Eff = (Label)C1.Items[i].FindControl("txtKgrade3Eff");
                    Label lblTEfis = (Label)C1.Items[i].FindControl("txtTEfis");
                    //if (L1.Text != string.Empty || L1x.Text != string.Empty || L2.Text != string.Empty || L2x.Text != string.Empty || L2x1.Text != string.Empty ||
                    //    L3.Text != string.Empty || L3x.Text != string.Empty || L4.Text != string.Empty || L4x.Text != string.Empty || L4x1.Text != string.Empty
                    //    || L5.Text != string.Empty || L5x.Text != string.Empty || L5x1.Text != string.Empty || L6.Text != string.Empty || L6x.Text != string.Empty)
                    //{
                    if (L1.Text == "" || L1x.Text == "" || L1x1.Text == "" || L1x2.Text == "" || L2.Text == "" || L2x.Text == "" || L3.Text == "" || L3x.Text == "" || L4.Text == "" || L4x.Text == "" || L4x1.Text == "" || lbltMix.Text == "" || lblFrmula.Text == ""
                        || SdL.Text == "" || SspB.Text == "" || AT.Text == "" || lblKL.Text == "" || lblEfis.Text == "" || lblKvKg2.Text == "" || lblKvEfis.Text == "" || lblKsEfis.Text == "" || lblKuEfs.Text == ""
                        || lblKgrade3Eff.Text == "" || lblTEfis.Text == "" || KV.Text == "" || KS.Text == "" || KGU.Text == "" || KGU2.Text == "" || KGU3.Text == "")
                    {
                        L1.Text = "0";
                        L1x.Text = "0";
                        L1x1.Text = "0";
                        L1x2.Text = "0";
                        L2.Text = "0";
                        L2x.Text = "0";
                        L3.Text = "0";
                        L3x.Text = "0";
                        L4.Text = "0";
                        L4x.Text = "0";
                        L4x1.Text = "0";
                        lblFrmula.Text = "0";
                        lbltMix.Text = "0";
                        lblKL.Text = "0";
                        SdL.Text = "0";
                        SspB.Text = "0";
                        AT.Text = "0";
                        lblKvKg2.Text = "0";
                        lblKvEfis.Text = "0";
                        lblEfis.Text = "0";
                        KV.Text = "0";
                        //KB.Text = "0";
                        KS.Text = "0";
                        KGU.Text = "0";
                        KGU2.Text = "0";
                        KGU3.Text = "0";
                        //lblKBimaSampah.Text = "0";
                        //lblKBimaBB.Text = "0";
                        //lblKsKg.Text = "0";
                        lblKuEfs.Text = "0";
                        lblKsEfis.Text = "0";
                        lblKgrade2Eff.Text = "0";
                        //lblKgKraftPlastikSmph.Text = "0";
                        //lblKgKraftPlastikBrsh.Text = "0";
                        //lblKgrade3Kg2.Text = "0";
                        lblKgrade3Eff.Text = "0";
                        lblTEfis.Text = "0";
                        //lblKsKg0.Text = "0";
                    }

                    HtmlTableRow tr = (HtmlTableRow)C1.Items[i].FindControl("Tr1");
                    lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L2.Text)) + (decimal.Parse(L2x.Text)) + (decimal.Parse(L3.Text)) + (decimal.Parse(L3x.Text)) + (decimal.Parse(L4.Text)) + (decimal.Parse(L4x.Text))).ToString("N0");
                    //lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(frmula1.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(frmula1a.Text))) + ((decimal.Parse(L2.Text)) * (decimal.Parse(frmula2.Text))) + ((decimal.Parse(L2x.Text)) * (decimal.Parse(frmula2a.Text))) + ((decimal.Parse(L2x1.Text)) * (decimal.Parse(frmula2b.Text)))
                    //                + (decimal.Parse(L3.Text)) * (decimal.Parse(frmula3.Text)) + (decimal.Parse(L3x.Text)) * (decimal.Parse(frmula3a.Text)) + (decimal.Parse(L4.Text)) * (decimal.Parse(frmula3a1.Text)) + (decimal.Parse(L4x.Text)) * (decimal.Parse(frmula4.Text)) + (decimal.Parse(L4x1.Text)) * (decimal.Parse(frmula4a.Text)) + (decimal.Parse(L5.Text)) * (decimal.Parse(frmula4a1.Text)) + (decimal.Parse(L5x.Text)) * (decimal.Parse(frmula5.Text)) + +(decimal.Parse(L5x1.Text)) * (decimal.Parse(frmula5a.Text))
                    //                + (decimal.Parse(L6.Text)) * (decimal.Parse(frmula6.Text)) + (decimal.Parse(L6x.Text)) * (decimal.Parse(frmula6a.Text))).ToString("N0");
                    //lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                    #region KV
                    //decimal satukomasatu;
                    //decimal satukomalima;
                    //decimal satukomadua;
                    //satukomasatu = 1.10m;
                    //satukomalima = 1.15m;
                    //satukomadua = 1.20m;
                    //if ((Convert.ToInt32(KV.Text)) >= 20 && (Convert.ToInt32(KV.Text)) <= 40)
                    //{
                    //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                    //}
                    //else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                    //{
                    //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                    //}
                    //else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                    //{
                    //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                    //}
                    //else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                    //{
                    //    lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                    //}
                    #endregion
                    //lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                    //lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                    //lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima.Text))) / 100).ToString("N0");
                    //lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                    //lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                    //lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                    //lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                    #region #1
                    try
                    {

                        // lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #2
                    try
                    {

                        //lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #3
                    try
                    {

                        //lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #4
                    try
                    {

                        // lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg2.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #5
                    try
                    {

                        //lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg2.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region#6
                    try
                    {

                        //lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text)))) - (decimal.Parse(lblKgrade2Eff.Text)))).ToString("N3");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                }
                #endregion
            }
            else if (user.UnitKerjaID == 7)
            {
                #region  Prosess Krwg
                decimal tMix = 0;
                ArrayList arrData = new ArrayList();
                for (int i = 0; i < lstR.Items.Count; i++)
                {
                    PakaiBahanBaku pbk = new PakaiBahanBaku();
                    HiddenField hd = (HiddenField)lstR.Items[i].FindControl("txtNilaiID");
                    Label lbltgl = (Label)lstR.Items[i].FindControl("lbltglProd");
                    TextBox L1 = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1");
                    TextBox L1x = (TextBox)lstR.Items[i].FindControl("txtjmlL1G1x");
                    TextBox L2 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2");
                    TextBox L2x = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x");
                    TextBox L2x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL2G2x1");
                    TextBox L3 = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3");
                    TextBox L3x = (TextBox)lstR.Items[i].FindControl("txtjmlL3G3x");
                    TextBox L4 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4");
                    TextBox L4x = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x");
                    TextBox L4x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL4G4x1");
                    TextBox L5 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5");
                    TextBox L5x = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x");
                    TextBox L5x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL5G5x1");
                    TextBox L6 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6");
                    TextBox L6x = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x");
                    TextBox L6x1 = (TextBox)lstR.Items[i].FindControl("txtjmlL6G6x1");
                    Label lbltMix = (Label)lstR.Items[i].FindControl("tMix");
                    Label lblFrmula = (Label)lstR.Items[i].FindControl("Frmula");
                    Label lblKL = (Label)lstR.Items[i].FindControl("txtKL");
                    TextBox SdL = (TextBox)lstR.Items[i].FindControl("txtSdL");
                    TextBox SspB = (TextBox)lstR.Items[i].FindControl("txtSspB");
                    TextBox AT = (TextBox)lstR.Items[i].FindControl("txtAT");
                    Label lblEfis = (Label)lstR.Items[i].FindControl("txtEfis");
                    Label lblKvKg = (Label)lstR.Items[i].FindControl("txtKvKg");
                    Label lblKvKg2 = (Label)lstR.Items[i].FindControl("txtKvKg2");
                    Label lblKvEfis = (Label)lstR.Items[i].FindControl("txtKvEfis");
                    TextBox KV = (TextBox)lstR.Items[i].FindControl("txtKV");
                    TextBox KB = (TextBox)lstR.Items[i].FindControl("txtKB");
                    TextBox KS = (TextBox)lstR.Items[i].FindControl("txtKS");
                    TextBox KGU = (TextBox)lstR.Items[i].FindControl("txtKGradeUtama");
                    TextBox KGU2 = (TextBox)lstR.Items[i].FindControl("txtKgrade2");
                    TextBox KGU3 = (TextBox)lstR.Items[i].FindControl("txtKgrade3");
                    Label lblKBimaSampah = (Label)lstR.Items[i].FindControl("txtKBimaSampah");
                    Label lblKBimaBK = (Label)lstR.Items[i].FindControl("txtKBimaBK");
                    Label lblKBimaBB = (Label)lstR.Items[i].FindControl("txtKBimaBB");
                    Label lblKsKg = (Label)lstR.Items[i].FindControl("txtKsKg");
                    Label lblKsKg0 = (Label)lstR.Items[i].FindControl("txtKsKg0");
                    Label lblKsEfis = (Label)lstR.Items[i].FindControl("txtKsEfis");
                    Label lblKuKg = (Label)lstR.Items[i].FindControl("txtKuKg");
                    Label lblKuEfs = (Label)lstR.Items[i].FindControl("txtKuEfs");
                    Label lblKgrade2Kg = (Label)lstR.Items[i].FindControl("txtKgrade2Kg");
                    Label lblKgrade2Eff = (Label)lstR.Items[i].FindControl("txtKgrade2Eff");
                    Label PerKraftSmpahPlastik = (Label)lstR.Items[i].FindControl("txtPerKraftSmpahPlastik");
                    Label lblKgKraftPlastikSmph = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikSmph");
                    //Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                    Label lblKgKraftPlastikBrsh = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikBrsh");
                    Label lblKgKraftPlastikktr = (Label)lstR.Items[i].FindControl("txtKgKraftPlastikktr");
                    Label lblKgrade3Kg = (Label)lstR.Items[i].FindControl("txtKgrade3Kg");
                    Label lblKgrade3Kg2 = (Label)lstR.Items[i].FindControl("txtKgrade3Kg2");
                    Label lblKgrade3Eff = (Label)lstR.Items[i].FindControl("txtKgrade3Eff");
                    Label lblTEfis = (Label)lstR.Items[i].FindControl("txtTEfis");
                    //if (L1.Text != string.Empty || L1x.Text != string.Empty || L2.Text != string.Empty || L2x.Text != string.Empty || L2x1.Text != string.Empty ||
                    //    L3.Text != string.Empty || L3x.Text != string.Empty || L4.Text != string.Empty || L4x.Text != string.Empty || L4x1.Text != string.Empty
                    //    || L5.Text != string.Empty || L5x.Text != string.Empty || L5x1.Text != string.Empty || L6.Text != string.Empty || L6x.Text != string.Empty)
                    //{
                    if (L1.Text == "" || L1x.Text == "" || L2.Text == "" || L2x.Text == "" || L2x1.Text == "" || L3.Text == "" || L3x.Text == "" || L4.Text == "" || L4x.Text == "" || L4x1.Text == ""
                        || L5.Text == "" || L5x.Text == "" || L5x1.Text == "" || lblFrmula.Text == "" || lblKL.Text == "" || SdL.Text == ""
                        || SspB.Text == "" || AT.Text == "" || lblEfis.Text == "" || lblKvKg2.Text == "" || lblKvEfis.Text == "" || KV.Text == "" || KB.Text == "" || KS.Text == "" || KGU.Text == "" || KGU2.Text == "" || KGU3.Text == "" || lblKBimaSampah.Text == "" || lblKBimaBB.Text == ""
                        || lblKsKg.Text == "" || lblKuEfs.Text == "" || lblKsEfis.Text == "" || lblKgrade2Eff.Text == "" || lblKgKraftPlastikSmph.Text == "" || lblKgKraftPlastikBrsh.Text == "" || lblKgrade3Kg2.Text == "" || lblKgrade3Eff.Text == "" || lblTEfis.Text == "")
                    {
                        L1.Text = "0";
                        L1x.Text = "0";
                        L2.Text = "0";
                        L2x.Text = "0";
                        L2x1.Text = "0";
                        L3.Text = "0";
                        L3x.Text = "0";
                        L4.Text = "0";
                        L4x.Text = "0";
                        L4x1.Text = "0";
                        L5.Text = "0";
                        L5x.Text = "0";
                        L5x1.Text = "0";
                        L6.Text = "0";
                        L6x.Text = "0";
                        L6x1.Text = "0";
                        lblFrmula.Text = "0";
                        lbltMix.Text = "0";
                        lblKL.Text = "0";
                        SdL.Text = "0";
                        SspB.Text = "0";
                        AT.Text = "0";
                        lblKvKg2.Text = "0";
                        lblKvEfis.Text = "0";
                        lblEfis.Text = "0";
                        KV.Text = "0";
                        KB.Text = "0";
                        KS.Text = "0";
                        KGU.Text = "0";
                        KGU2.Text = "0";
                        KGU3.Text = "0";
                        lblKBimaSampah.Text = "0";
                        lblKBimaBB.Text = "0";
                        lblKsKg.Text = "0";
                        lblKuEfs.Text = "0";
                        lblKsEfis.Text = "0";
                        lblKgrade2Eff.Text = "0";
                        lblKgKraftPlastikSmph.Text = "0";
                        lblKgKraftPlastikBrsh.Text = "0";
                        lblKgrade3Kg2.Text = "0";
                        lblKgrade3Eff.Text = "0";
                        lblTEfis.Text = "0";
                        //lblKsKg0.Text = "0";
                    }

                    HtmlTableRow tr = (HtmlTableRow)lstR.Items[i].FindControl("Tr1");
                    lbltMix.Text = (decimal.Parse(L1.Text) + (decimal.Parse(L1x.Text)) + (decimal.Parse(L2.Text)) + (decimal.Parse(L2x.Text)) + (decimal.Parse(L2x1.Text)) + (decimal.Parse(L3.Text)) + (decimal.Parse(L3x.Text)) + (decimal.Parse(L4.Text)) + (decimal.Parse(L4x.Text)) + (decimal.Parse(L4x1.Text)) + (decimal.Parse(L5.Text)) + (decimal.Parse(L5x.Text)) + (decimal.Parse(L5x1.Text)) + (decimal.Parse(L6.Text)) + (decimal.Parse(L6x.Text)) + (decimal.Parse(L6x1.Text))).ToString("N0");
                    lblFrmula.Text = (((decimal.Parse(L1.Text)) * (decimal.Parse(frmula1.Text))) + ((decimal.Parse(L1x.Text)) * (decimal.Parse(frmula1a.Text))) + ((decimal.Parse(L2.Text)) * (decimal.Parse(frmula2.Text))) + ((decimal.Parse(L2x.Text)) * (decimal.Parse(frmula2a.Text))) + ((decimal.Parse(L2x1.Text)) * (decimal.Parse(frmula2b.Text)))
                                    + (decimal.Parse(L3.Text)) * (decimal.Parse(frmula3.Text)) + (decimal.Parse(L3x.Text)) * (decimal.Parse(frmula3a.Text)) + (decimal.Parse(L4.Text)) * (decimal.Parse(frmula3a1.Text)) + (decimal.Parse(L4x.Text)) * (decimal.Parse(frmula4.Text)) + (decimal.Parse(L4x1.Text)) * (decimal.Parse(frmula4a.Text)) + (decimal.Parse(L5.Text)) * (decimal.Parse(frmula4a1.Text)) + (decimal.Parse(L5x.Text)) * (decimal.Parse(frmula5.Text)) + +(decimal.Parse(L5x1.Text)) * (decimal.Parse(frmula5a.Text))
                                    + (decimal.Parse(L6.Text)) * (decimal.Parse(frmula6.Text)) + (decimal.Parse(L6x.Text)) * (decimal.Parse(frmula6a.Text)) + (decimal.Parse(L6x1.Text)) * (decimal.Parse(frmula6a1.Text))).ToString("N0");
                    lblKL.Text = ((decimal.Parse(SdL.Text)) - (decimal.Parse(SspB.Text)) - (decimal.Parse(AT.Text))).ToString("N0");
                    #region KV
                    decimal satukomasatu;
                    decimal satukomalima;
                    decimal satukomadua;
                    satukomasatu = 1.10m;
                    satukomalima = 1.15m;
                    satukomadua = 1.20m;
                    if ((Convert.ToInt32(KV.Text)) >= 20 && (Convert.ToInt32(KV.Text)) <= 40)
                    {
                        lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomasatu)).ToString("N0");
                    }
                    else if ((Convert.ToInt32(KV.Text)) >= 50 && (Convert.ToInt32(KV.Text)) <= 60)
                    {
                        lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomalima)).ToString("N0");
                    }
                    else if ((Convert.ToInt32(KV.Text)) >= 70 && (Convert.ToInt32(KV.Text)) <= 100)
                    {
                        lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text)) * (satukomadua)).ToString("N0");
                    }
                    else if ((Convert.ToInt32(KV.Text)) >= 0 && (Convert.ToInt32(KV.Text)) <= 10)
                    {
                        lblKvKg2.Text = ((decimal.Parse(lblKvKg.Text))).ToString("N0");
                    }
                    #endregion
                    lblKBimaBB.Text = (decimal.Parse(lblKBimaBK.Text) - (decimal.Parse(lblKBimaSampah.Text))).ToString("N0");
                    lblKvEfis.Text = ((decimal.Parse(KV.Text) / (100)) * (-10)).ToString("N1");
                    lblKBimaSampah.Text = (((decimal.Parse(lblKBimaBK.Text)) * (decimal.Parse(PersenKBima.Text))) / 100).ToString("N0");
                    lblKgKraftPlastikSmph.Text = (((decimal.Parse(lblKgKraftPlastikktr.Text)) * (decimal.Parse(txtPerKraftSmpahPlastik.Text))) / 100).ToString("N0");
                    lblKsKg.Text = (decimal.Parse(lblKBimaBB.Text) + (decimal.Parse(lblKsKg0.Text))).ToString("N0");
                    lblKgKraftPlastikBrsh.Text = ((decimal.Parse(lblKgKraftPlastikktr.Text)) - (decimal.Parse(lblKgKraftPlastikSmph.Text))).ToString("N0");
                    lblKgrade3Kg2.Text = (decimal.Parse(lblKgKraftPlastikBrsh.Text) + (decimal.Parse(lblKgrade3Kg.Text))).ToString("N0");
                    #region #1
                    try
                    {

                        lblEfis.Text = ((decimal.Parse(lblKL.Text) - decimal.Parse(lblFrmula.Text)) / decimal.Parse(lblFrmula.Text) * 100).ToString("N1");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #2
                    try
                    {

                        lblKuEfs.Text = ((decimal.Parse(lblKuKg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #3
                    try
                    {

                        lblKgrade2Eff.Text = ((decimal.Parse(lblKgrade2Kg.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #4
                    try
                    {

                        lblKgrade3Eff.Text = ((decimal.Parse(lblKgrade3Kg2.Text)) / (decimal.Parse(lblKL.Text))).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region #5
                    try
                    {

                        lblTEfis.Text = (((decimal.Parse(lblKvKg2.Text)) + (decimal.Parse(lblKsKg.Text)) + (decimal.Parse(lblKuKg.Text)) + (decimal.Parse(lblKgrade2Kg.Text)) + (decimal.Parse(lblKgrade3Kg2.Text)) - (decimal.Parse(lblFrmula.Text))) / (decimal.Parse(lblFrmula.Text)) * (100)).ToString("N2");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                    #region#6
                    try
                    {

                        lblKsEfis.Text = ((((decimal.Parse(lblEfis.Text)) - ((decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKvEfis.Text)) + (decimal.Parse(lblKgrade3Eff.Text)) + (decimal.Parse(lblKuEfs.Text)))) - (decimal.Parse(lblKgrade2Eff.Text)))).ToString("N3");
                    }
                    catch (DivideByZeroException)
                    {

                    }
                    #endregion
                }
                #endregion
            }
            btnCalc.Visible = true;
            btnSimpan.Visible = true;
            btnSimpan.Enabled = false;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
            {
                #region Ctrp & Jombang
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                //LoadData();
                Response.AddHeader("content-disposition", "attachment;filename=PemantauanPemakaianKertas.xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string Html = "<b>REPORT EFFESIENSI BAHAN BAKU PULP</b>";
                Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
                string HtmlEnd = "";
                Div6.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("xx\">", "\">\'");
                Contents = Contents.Replace("border=\"0", "border=\"1");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
                #endregion
            }
            else if (user.UnitKerjaID == 7)
            {
                #region Karawang
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.BufferOutput = true;
                //LoadData();
                Response.AddHeader("content-disposition", "attachment;filename=PemantauanPemakaianKertas.xls");
                Response.Charset = "utf-8";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                string Html = "<b>REPORT EFFESIENSI BAHAN BAKU PULP</b>";
                Html += "<br>Periode : " + ddlBulan.SelectedItem.Text + "  " + ddlTahun.SelectedValue.ToString();
                string HtmlEnd = "";
                Div3.RenderControl(hw);
                string Contents = sw.ToString();
                Contents = Contents.Replace("xx\">", "\">\'");
                Contents = Contents.Replace("border=\"0", "border=\"1");
                Response.Write(Html + Contents + HtmlEnd);
                Response.Flush();
                Response.End();
                #endregion
            }
        }




        protected void LoadPreview()
        {
            //string FLagLap = Session["FlagLap"].ToString();

            string TahunR = DateTime.Now.Year.ToString();
            string BulanR = DateTime.Now.Month.ToString();
            int HariR = DateTime.Now.Day;
            if (HariR < 10)
            { string HariR0 = "0" + HariR.ToString(); Session["HariR"] = HariR0; }
            else
            { string HariR0 = HariR.ToString(); Session["HariR"] = HariR0; }

            if (BulanR == "1") { string BulanString = "Januari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "2") { string BulanString = "Februari"; Session["BulanS"] = BulanString; }
            else if (BulanR == "3") { string BulanString = "Maret"; Session["BulanS"] = BulanString; }
            else if (BulanR == "4") { string BulanString = "April"; Session["BulanS"] = BulanString; }
            else if (BulanR == "5") { string BulanString = "Mei"; Session["BulanS"] = BulanString; }
            else if (BulanR == "6") { string BulanString = "Juni"; Session["BulanS"] = BulanString; }
            else if (BulanR == "7") { string BulanString = "Juli"; Session["BulanS"] = BulanString; }
            else if (BulanR == "8") { string BulanString = "Agustus"; Session["BulanS"] = BulanString; }
            else if (BulanR == "9") { string BulanString = "September"; Session["BulanS"] = BulanString; }
            else if (BulanR == "10") { string BulanString = "Oktober"; Session["BulanS"] = BulanString; }
            else if (BulanR == "11") { string BulanString = "November"; Session["BulanS"] = BulanString; }
            else if (BulanR == "12") { string BulanString = "Desember"; Session["BulanS"] = BulanString; }
            string BulanAlias = Session["BulanS"].ToString();
            string HariAlias = Session["HariR"].ToString();
            string TglAliasR = HariAlias + "-" + BulanAlias + "-" + TahunR; Session["Tgl"] = TglAliasR;

            Users user = (Users)Session["Users"];
            int Bulan1 = int.Parse(ddlBulan.SelectedValue);
            string Tahun = ddlTahun.SelectedValue.ToString();
            if (Bulan1 < 10)
            {
                string Bulan2 = "0" + Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }
            else
            {
                string Bulan2 = Bulan1.ToString(); Session["Bulan2"] = Bulan2;
            }

            string Periode = Tahun + Session["Bulan2"].ToString() + "01";
            string PeriodeBulanTahun = Tahun + Session["Bulan2"].ToString();
            Session["PeriodeBulanTahun"] = PeriodeBulanTahun;

            PakaiBahanBaku PakaiBB = new PakaiBahanBaku();
            PakaiBahanBakuFacade pakaiBBF = new PakaiBahanBakuFacade();
            #region rules#1
            int CekInput = pakaiBBF.RetrieveInputan(PeriodeBulanTahun);
            Session["Cek"] = CekInput;
            #region sudah input
            if (CekInput > 0)
            {
                ArrayList arrData2 = new ArrayList();
                PakaiBahanBakuFacade Fbm2 = new PakaiBahanBakuFacade();
                if (user.UnitKerjaID == 1)
                {
                    PanelKarawang.Visible = false; PanelKarawang2.Visible = false; btnExport.Visible = true; btnEdit.Visible = false; btnSimpan2.Visible = false; PanelCiteureup.Visible = false; PanelCiteureup2.Visible = true;
                    btnCalc.Visible = false; btnSimpan.Visible = false;
                    arrData2 = Fbm2.RetrieveReportAdaInputCtrp(PeriodeBulanTahun); Session["arrData2"] = arrData2;
                    C2.DataSource = arrData2;
                    C2.DataBind();
                    RepeaterCeXport.DataSource = arrData2;
                    RepeaterCeXport.DataBind();

                }
                else if (user.UnitKerjaID == 7)
                {
                    PanelKarawang.Visible = false; PanelKarawang2.Visible = true; btnExport.Visible = true; btnEdit.Visible = false; btnSimpan2.Visible = false;
                    btnCalc.Visible = false; btnSimpan.Visible = false;
                    arrData2 = Fbm2.RetrieveReportAdaInputKRwg(PeriodeBulanTahun); Session["arrData2"] = arrData2;
                    lstR2.DataSource = arrData2;
                    lstR2.DataBind();
                    rptExport.DataSource = arrData2;
                    rptExport.DataBind();
                }
                else
                {
                    PanelKarawang.Visible = false; PanelKarawang2.Visible = false; btnExport.Visible = true; btnEdit.Visible = false; btnSimpan2.Visible = false; PanelCiteureup.Visible = false; PanelCiteureup2.Visible = true;
                    btnCalc.Visible = false; btnSimpan.Visible = false;
                    arrData2 = Fbm2.RetrieveReportAdaInputJmbg(PeriodeBulanTahun); Session["arrData2"] = arrData2;
                    C2.DataSource = arrData2;
                    C2.DataBind();
                    RepeaterCeXport.DataSource = arrData2;
                    RepeaterCeXport.DataBind();
                }
            }
            #endregion
            #region belum input
            else if (CekInput == 0)
            {
                ArrayList arrData = new ArrayList();
                PakaiBahanBakuFacade Fbm = new PakaiBahanBakuFacade();

                if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
                {
                    PanelKarawang.Visible = false; PanelKarawang2.Visible = false; btnExport.Visible = false; btnCalc.Visible = false; btnSimpan.Visible = false; btnEdit.Visible = false; PanelCiteureup.Visible = true; PanelCiteureup2.Visible = false;
                    arrData = Fbm.RetrieveReportCtrp(Periode, PeriodeBulanTahun); Session["Data"] = arrData;
                    C1.DataSource = arrData;
                    C1.DataBind();

                }
                else if (user.UnitKerjaID == 7)
                {
                    PanelKarawang.Visible = true; PanelKarawang2.Visible = false; btnExport.Visible = false; btnCalc.Visible = false; btnSimpan.Visible = false; btnEdit.Visible = false;
                    arrData = Fbm.RetrieveReportKarawang(Periode, PeriodeBulanTahun); Session["Data"] = arrData;
                    lstR.DataSource = arrData;
                    lstR.DataBind();
                }
            }
            #endregion
            #endregion

            #region update sarmut Effesiensi Pemakaian Kertas
            string Queryx = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = Queryx;
            SqlDataReader sdr = zl.Retrieve();

            #region update sarmut Boardmill
            string sarmutPrs = "Effesiensi Pemakaian Kertas";
            string strDept = string.Empty;
            int deptid = getDeptID("BOARD");
            #endregion
            #region #1
            decimal actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (user.UnitKerjaID == 1 || user.UnitKerjaID == 13)
            {
                zl.CustomQuery = " select isnull(cast((((sum(KvKg2) + sum(KsKg) + sum(KuKg) + sum(Kgrade2Kg)+ sum(Kgrade3Kg)) -sum(Formula)) / (sum(Formula)) * 100)as decimal(18,2)),0)actual from BM_Pulp " +
                             " where LEFT(convert(char,Tgl_Prod,112),6)='" + PeriodeBulanTahun + "' and RowStatus>-1 ";
            }
            else if (user.UnitKerjaID == 7)
            {
                zl.CustomQuery = " select isnull(cast((((sum(KvKg2) + sum(KsKg) + sum(KuKg) + sum(Kgrade2Kg)+ sum(Kgrade3xKg)) -sum(Formula)) / (sum(Formula)) * 100)as decimal(18,2)),0)actual from BM_Pulp " +
                             " where LEFT(convert(char,Tgl_Prod,112),6)='" + PeriodeBulanTahun + "' and RowStatus>-1 ";
            }

            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #2
            ZetroView zl1 = new ZetroView();
            zl1.QueryType = Operation.CUSTOM;
            zl1.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedValue + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "update SPD_TransPrs set actual=" + actual.ToString().Replace(",", ".") + " where Approval=0 and tahun=@tahun and bulan=@bulan " +
                " and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "')";
            SqlDataReader sdr1 = zl1.Retrieve();
            #endregion
            #region #3
            //hitung average untuk spd_perusahaan
            actual = 0;
            zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @tahun int,@bulan int,@thnbln nvarchar(6) set @tahun=" + ddlTahun.SelectedValue +
                " set @bulan=" + ddlBulan.SelectedValue + " " +
                "set @thnbln=cast(@tahun as char(4)) + REPLACE(STR(@bulan, 2), SPACE(1), '0')  " +
                "select isnull(cast(avg(actual) as decimal(18,2)),0) actual from SPD_TransPrs where Approval=0 and tahun=@tahun and bulan=@bulan " +
                " and SarmutPID in ( " +
                "select ID from SPD_Perusahaan where deptid=" + deptid + " and rowstatus>-1 and SarMutPerusahaan='" + sarmutPrs + "') and actual>0";
            sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    actual = Decimal.Parse(sdr["actual"].ToString());
                }
            }
            #endregion
            #region #4
            //zl1 = new ZetroView();
            //zl1.QueryType = Operation.CUSTOM;

            //zl1.CustomQuery =
            //    "update SPD_TransPrs set actual= " + actual.ToString().Replace(",", ".") + " where approval=0 and Tahun =" + ddlTahun.SelectedValue +
            //     " and Bulan=" + ddlBulan.SelectedIndex +
            //     " and SarmutPID in (select ID from SPD_Perusahaan where SarMutPerusahaan ='" + sarmutPrs + "') ";
            //sdr1 = zl1.Retrieve();
            #endregion
            #endregion

        }

        protected int getDeptID(string deptName)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select id from spd_dept where dept like '%" + deptName + "%'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }


    }

    
}
public class PakaiBahanBakuFacade
{
    public string strError = string.Empty;
    private ArrayList arrData = new ArrayList();
    private List<SqlParameter> sqlListParam;
    private PakaiBahanBaku objpakaiBB = new PakaiBahanBaku();

    public PakaiBahanBakuFacade()
        : base()
    {

    }
    public string Criteria { get; set; }
    public string Field { get; set; }
    public string Where { get; set; }

    public int Insert(object objDomain)
    {
        try
        {
            objpakaiBB = (PakaiBahanBaku)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@Tgl_Prod", objpakaiBB.Tgl_Prod));
            sqlListParam.Add(new SqlParameter("@Tgl_Prod2", objpakaiBB.Tgl_Prod2));
            sqlListParam.Add(new SqlParameter("@L1", objpakaiBB.L1));
            sqlListParam.Add(new SqlParameter("@L1x", objpakaiBB.L1x));
            sqlListParam.Add(new SqlParameter("@L2", objpakaiBB.L2));
            sqlListParam.Add(new SqlParameter("@L2x", objpakaiBB.L2x));
            sqlListParam.Add(new SqlParameter("@L2x1", objpakaiBB.L2x1));
            sqlListParam.Add(new SqlParameter("@L3", objpakaiBB.L3));
            sqlListParam.Add(new SqlParameter("@L3x", objpakaiBB.L3x));
            //sqlListParam.Add(new SqlParameter("@L3x1", objpakaiBB.L3x1));
            sqlListParam.Add(new SqlParameter("@L4", objpakaiBB.L4));
            sqlListParam.Add(new SqlParameter("@L4x", objpakaiBB.L4x));
            sqlListParam.Add(new SqlParameter("@L4x1", objpakaiBB.L4x1));
            sqlListParam.Add(new SqlParameter("@L5", objpakaiBB.L5));
            sqlListParam.Add(new SqlParameter("@L5x", objpakaiBB.L5x));
            sqlListParam.Add(new SqlParameter("@L5x1", objpakaiBB.L5x1));
            sqlListParam.Add(new SqlParameter("@L6", objpakaiBB.L6));
            sqlListParam.Add(new SqlParameter("@L6x", objpakaiBB.L6x));
            sqlListParam.Add(new SqlParameter("@L6x1", objpakaiBB.L6x1));
            sqlListParam.Add(new SqlParameter("@TMix", objpakaiBB.TMix));
            sqlListParam.Add(new SqlParameter("@Formula", objpakaiBB.Formula));
            sqlListParam.Add(new SqlParameter("@SdL", objpakaiBB.SdL));
            sqlListParam.Add(new SqlParameter("@SspB", objpakaiBB.SspB));
            sqlListParam.Add(new SqlParameter("@AT", objpakaiBB.AT));
            sqlListParam.Add(new SqlParameter("@KL", objpakaiBB.KL));
            sqlListParam.Add(new SqlParameter("@Efis", objpakaiBB.Efis));
            sqlListParam.Add(new SqlParameter("@JKertasVirgin", objpakaiBB.JKertasVirgin));
            sqlListParam.Add(new SqlParameter("@KvKg", objpakaiBB.KvKg));
            sqlListParam.Add(new SqlParameter("@KvKg2", objpakaiBB.KvKg2));
            sqlListParam.Add(new SqlParameter("@KvEfis", objpakaiBB.KvEfis));
            sqlListParam.Add(new SqlParameter("@BkBimaKg", objpakaiBB.BkBimaKg));
            sqlListParam.Add(new SqlParameter("@SampahBima", objpakaiBB.SampahBima));
            sqlListParam.Add(new SqlParameter("@BbBimaKg", objpakaiBB.BbBimaKg));
            sqlListParam.Add(new SqlParameter("@KsKg0", objpakaiBB.KsKg0));
            sqlListParam.Add(new SqlParameter("@KsKg", objpakaiBB.KsKg));
            sqlListParam.Add(new SqlParameter("@KsEfis", objpakaiBB.KsEfis));
            sqlListParam.Add(new SqlParameter("@KuKg", objpakaiBB.KuKg));
            sqlListParam.Add(new SqlParameter("@KuEfs", objpakaiBB.KuEfs));
            sqlListParam.Add(new SqlParameter("@Kgrade2Kg", objpakaiBB.Kgrade2Kg));
            sqlListParam.Add(new SqlParameter("@Kgrade2Eff", objpakaiBB.Kgrade2Eff));
            sqlListParam.Add(new SqlParameter("@KraftBkKg", objpakaiBB.KraftBkKg));
            sqlListParam.Add(new SqlParameter("@SampahKraft", objpakaiBB.SampahKraft));
            sqlListParam.Add(new SqlParameter("@KraftBbKg", objpakaiBB.KraftBbKg));
            sqlListParam.Add(new SqlParameter("@Kgrade3Kg", objpakaiBB.Kgrade3Kg));
            sqlListParam.Add(new SqlParameter("@Kgrade3xKg", objpakaiBB.Kgrade3xKg));
            sqlListParam.Add(new SqlParameter("@Kgrade3Eff", objpakaiBB.Kgrade3Eff));
            sqlListParam.Add(new SqlParameter("@TEfis", objpakaiBB.TEfis));
            sqlListParam.Add(new SqlParameter("@KV", objpakaiBB.Kv));
            sqlListParam.Add(new SqlParameter("@KB", objpakaiBB.Kb));
            sqlListParam.Add(new SqlParameter("@KS", objpakaiBB.Ks));
            sqlListParam.Add(new SqlParameter("@KGradeUtama", objpakaiBB.KGradeUtama));
            sqlListParam.Add(new SqlParameter("@Kgrade2", objpakaiBB.Kgrade2));
            sqlListParam.Add(new SqlParameter("@Kgrade3", objpakaiBB.Kgrade3));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objpakaiBB.CreatedBy));
            //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objpakaiBB.LastModifiedBy));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "InsertPBB2_SPNew");
            strError = da.Error;
            return result;
        }

        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    public int InsertCtrp(object objDomain)
    {
        try
        {
            objpakaiBB = (PakaiBahanBaku)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@Tgl_Prod", objpakaiBB.Tgl_Prod));
            sqlListParam.Add(new SqlParameter("@Tgl_Prod2", objpakaiBB.Tgl_Prod2));
            sqlListParam.Add(new SqlParameter("@L1", objpakaiBB.L1));
            sqlListParam.Add(new SqlParameter("@L1x", objpakaiBB.L1x));
            sqlListParam.Add(new SqlParameter("@L1x1", objpakaiBB.L1x1));
            sqlListParam.Add(new SqlParameter("@L1x2", objpakaiBB.L1x2));
            sqlListParam.Add(new SqlParameter("@L2", objpakaiBB.L2));
            sqlListParam.Add(new SqlParameter("@L2x", objpakaiBB.L2x));
            //sqlListParam.Add(new SqlParameter("@L2x1", objpakaiBB.L2x1));
            sqlListParam.Add(new SqlParameter("@L3", objpakaiBB.L3));
            sqlListParam.Add(new SqlParameter("@L3x", objpakaiBB.L3x));
            sqlListParam.Add(new SqlParameter("@L4", objpakaiBB.L4));
            sqlListParam.Add(new SqlParameter("@L4x", objpakaiBB.L4x));
            sqlListParam.Add(new SqlParameter("@L4x1", objpakaiBB.L4x1));
            //sqlListParam.Add(new SqlParameter("@L5", objpakaiBB.L5));
            //sqlListParam.Add(new SqlParameter("@L5x", objpakaiBB.L5x));
            //sqlListParam.Add(new SqlParameter("@L5x1", objpakaiBB.L5x1));
            //sqlListParam.Add(new SqlParameter("@L6", objpakaiBB.L6));
            //sqlListParam.Add(new SqlParameter("@L6x", objpakaiBB.L6x));
            sqlListParam.Add(new SqlParameter("@TMix", objpakaiBB.TMix));
            sqlListParam.Add(new SqlParameter("@Formula", objpakaiBB.Formula));
            sqlListParam.Add(new SqlParameter("@SdL", objpakaiBB.SdL));
            sqlListParam.Add(new SqlParameter("@SspB", objpakaiBB.SspB));
            sqlListParam.Add(new SqlParameter("@AT", objpakaiBB.AT));
            sqlListParam.Add(new SqlParameter("@KL", objpakaiBB.KL));
            sqlListParam.Add(new SqlParameter("@Efis", objpakaiBB.Efis));
            sqlListParam.Add(new SqlParameter("@JKertasVirgin", objpakaiBB.JKertasVirgin));
            sqlListParam.Add(new SqlParameter("@KvKg", objpakaiBB.KvKg));
            sqlListParam.Add(new SqlParameter("@KvKg2", objpakaiBB.KvKg2));
            sqlListParam.Add(new SqlParameter("@KvEfis", objpakaiBB.KvEfis));
            //sqlListParam.Add(new SqlParameter("@BkBimaKg", objpakaiBB.BkBimaKg));
            //sqlListParam.Add(new SqlParameter("@SampahBima", objpakaiBB.SampahBima));
            //sqlListParam.Add(new SqlParameter("@BbBimaKg", objpakaiBB.BbBimaKg));
            //sqlListParam.Add(new SqlParameter("@KsKg0", objpakaiBB.KsKg0));
            sqlListParam.Add(new SqlParameter("@KsKg", objpakaiBB.KsKg));
            sqlListParam.Add(new SqlParameter("@KsEfis", objpakaiBB.KsEfis));
            sqlListParam.Add(new SqlParameter("@KuKg", objpakaiBB.KuKg));
            sqlListParam.Add(new SqlParameter("@KuEfs", objpakaiBB.KuEfs));
            //sqlListParam.Add(new SqlParameter("@KraftBkKg", objpakaiBB.KraftBkKg));
            //sqlListParam.Add(new SqlParameter("@SampahKraft", objpakaiBB.SampahKraft));
            //sqlListParam.Add(new SqlParameter("@KraftBbKg", objpakaiBB.KraftBbKg));
            sqlListParam.Add(new SqlParameter("@Kgrade2Kg", objpakaiBB.Kgrade2Kg));
            sqlListParam.Add(new SqlParameter("@Kgrade2Eff", objpakaiBB.Kgrade2Eff));
            sqlListParam.Add(new SqlParameter("@Kgrade3Kg", objpakaiBB.Kgrade3Kg));
            //sqlListParam.Add(new SqlParameter("@Kgrade3xKg", objpakaiBB.Kgrade3xKg));
            sqlListParam.Add(new SqlParameter("@Kgrade3Eff", objpakaiBB.Kgrade3Eff));
            sqlListParam.Add(new SqlParameter("@TEfis", objpakaiBB.TEfis));
            sqlListParam.Add(new SqlParameter("@KV", objpakaiBB.Kv));
            //sqlListParam.Add(new SqlParameter("@KB", objpakaiBB.Kb));
            sqlListParam.Add(new SqlParameter("@KS", objpakaiBB.Ks));
            sqlListParam.Add(new SqlParameter("@KGradeUtama", objpakaiBB.KGradeUtama));
            sqlListParam.Add(new SqlParameter("@Kgrade2", objpakaiBB.Kgrade2));
            sqlListParam.Add(new SqlParameter("@Kgrade3", objpakaiBB.Kgrade3));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objpakaiBB.CreatedBy));
            //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objpakaiBB.LastModifiedBy));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "InsertPBB2_SPNew");
            strError = da.Error;
            return result;
        }

        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }


    public int InsertJombang(object objDomain)
    {
        try
        {
            objpakaiBB = (PakaiBahanBaku)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@Tgl_Prod", objpakaiBB.Tgl_Prod));
            sqlListParam.Add(new SqlParameter("@Tgl_Prod2", objpakaiBB.Tgl_Prod2));
            sqlListParam.Add(new SqlParameter("@L1", objpakaiBB.L1));
            sqlListParam.Add(new SqlParameter("@L1x", objpakaiBB.L1x));
            sqlListParam.Add(new SqlParameter("@L1x1", objpakaiBB.L1x1));
            sqlListParam.Add(new SqlParameter("@L1x2", objpakaiBB.L1x2));
            //sqlListParam.Add(new SqlParameter("@L2", objpakaiBB.L2));
            //sqlListParam.Add(new SqlParameter("@L2x", objpakaiBB.L2x));
            //sqlListParam.Add(new SqlParameter("@L2x1", objpakaiBB.L2x1));
            //sqlListParam.Add(new SqlParameter("@L3", objpakaiBB.L3));
            //sqlListParam.Add(new SqlParameter("@L3x", objpakaiBB.L3x));
            //sqlListParam.Add(new SqlParameter("@L4", objpakaiBB.L4));
            //sqlListParam.Add(new SqlParameter("@L4x", objpakaiBB.L4x));
            //sqlListParam.Add(new SqlParameter("@L4x1", objpakaiBB.L4x1));
            //sqlListParam.Add(new SqlParameter("@L5", objpakaiBB.L5));
            //sqlListParam.Add(new SqlParameter("@L5x", objpakaiBB.L5x));
            //sqlListParam.Add(new SqlParameter("@L5x1", objpakaiBB.L5x1));
            //sqlListParam.Add(new SqlParameter("@L6", objpakaiBB.L6));
            //sqlListParam.Add(new SqlParameter("@L6x", objpakaiBB.L6x));
            sqlListParam.Add(new SqlParameter("@TMix", objpakaiBB.TMix));
            sqlListParam.Add(new SqlParameter("@Formula", objpakaiBB.Formula));
            sqlListParam.Add(new SqlParameter("@SdL", objpakaiBB.SdL));
            sqlListParam.Add(new SqlParameter("@SspB", objpakaiBB.SspB));
            sqlListParam.Add(new SqlParameter("@AT", objpakaiBB.AT));
            sqlListParam.Add(new SqlParameter("@KL", objpakaiBB.KL));
            sqlListParam.Add(new SqlParameter("@Efis", objpakaiBB.Efis));
            sqlListParam.Add(new SqlParameter("@JKertasVirgin", objpakaiBB.JKertasVirgin));
            sqlListParam.Add(new SqlParameter("@KvKg", objpakaiBB.KvKg));
            sqlListParam.Add(new SqlParameter("@KvKg2", objpakaiBB.KvKg2));
            sqlListParam.Add(new SqlParameter("@KvEfis", objpakaiBB.KvEfis));
            //sqlListParam.Add(new SqlParameter("@BkBimaKg", objpakaiBB.BkBimaKg));
            //sqlListParam.Add(new SqlParameter("@SampahBima", objpakaiBB.SampahBima));
            //sqlListParam.Add(new SqlParameter("@BbBimaKg", objpakaiBB.BbBimaKg));
            //sqlListParam.Add(new SqlParameter("@KsKg0", objpakaiBB.KsKg0));
            sqlListParam.Add(new SqlParameter("@KsKg", objpakaiBB.KsKg));
            sqlListParam.Add(new SqlParameter("@KsEfis", objpakaiBB.KsEfis));
            sqlListParam.Add(new SqlParameter("@KuKg", objpakaiBB.KuKg));
            sqlListParam.Add(new SqlParameter("@KuEfs", objpakaiBB.KuEfs));
            //sqlListParam.Add(new SqlParameter("@KraftBkKg", objpakaiBB.KraftBkKg));
            //sqlListParam.Add(new SqlParameter("@SampahKraft", objpakaiBB.SampahKraft));
            //sqlListParam.Add(new SqlParameter("@KraftBbKg", objpakaiBB.KraftBbKg));
            sqlListParam.Add(new SqlParameter("@Kgrade2Kg", objpakaiBB.Kgrade2Kg));
            sqlListParam.Add(new SqlParameter("@Kgrade2Eff", objpakaiBB.Kgrade2Eff));
            sqlListParam.Add(new SqlParameter("@Kgrade3Kg", objpakaiBB.Kgrade3Kg));
            //sqlListParam.Add(new SqlParameter("@Kgrade3xKg", objpakaiBB.Kgrade3xKg));
            sqlListParam.Add(new SqlParameter("@Kgrade3Eff", objpakaiBB.Kgrade3Eff));
            sqlListParam.Add(new SqlParameter("@TEfis", objpakaiBB.TEfis));
            sqlListParam.Add(new SqlParameter("@KV", objpakaiBB.Kv));
            //sqlListParam.Add(new SqlParameter("@KB", objpakaiBB.Kb));
            sqlListParam.Add(new SqlParameter("@KS", objpakaiBB.Ks));
            sqlListParam.Add(new SqlParameter("@KGradeUtama", objpakaiBB.KGradeUtama));
            sqlListParam.Add(new SqlParameter("@Kgrade2", objpakaiBB.Kgrade2));
            sqlListParam.Add(new SqlParameter("@Kgrade3", objpakaiBB.Kgrade3));
            sqlListParam.Add(new SqlParameter("@CreatedBy", objpakaiBB.CreatedBy));
            //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objpakaiBB.LastModifiedBy));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "InsertPBB2_SPNew");
            strError = da.Error;
            return result;
        }

        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }

    public int Update(object objDomain)
    {
        try
        {
            objpakaiBB = (PakaiBahanBaku)objDomain;
            sqlListParam = new List<SqlParameter>();
            sqlListParam.Add(new SqlParameter("@ID", objpakaiBB.ID));
            //sqlListParam.Add(new SqlParameter("@Tgl_Prod", objpakaiBB.Tgl_Prod));
            //sqlListParam.Add(new SqlParameter("@Tgl_Prod2", objpakaiBB.Tgl_Prod2));
            sqlListParam.Add(new SqlParameter("@L1", objpakaiBB.L1));
            sqlListParam.Add(new SqlParameter("@L1x", objpakaiBB.L1x));
            sqlListParam.Add(new SqlParameter("@L2", objpakaiBB.L2));
            sqlListParam.Add(new SqlParameter("@L2x", objpakaiBB.L2x));
            sqlListParam.Add(new SqlParameter("@L2x1", objpakaiBB.L2x1));
            sqlListParam.Add(new SqlParameter("@L3", objpakaiBB.L3));
            sqlListParam.Add(new SqlParameter("@L3x", objpakaiBB.L3x));
            sqlListParam.Add(new SqlParameter("@L4", objpakaiBB.L4));
            sqlListParam.Add(new SqlParameter("@L4x", objpakaiBB.L4x));
            sqlListParam.Add(new SqlParameter("@L4x1", objpakaiBB.L4x1));
            sqlListParam.Add(new SqlParameter("@L5", objpakaiBB.L5));
            sqlListParam.Add(new SqlParameter("@L5x", objpakaiBB.L5x));
            sqlListParam.Add(new SqlParameter("@L5x1", objpakaiBB.L5x1));
            sqlListParam.Add(new SqlParameter("@L6", objpakaiBB.L6));
            sqlListParam.Add(new SqlParameter("@L6x", objpakaiBB.L6x));
            //sqlListParam.Add(new SqlParameter("@TMix", objpakaiBB.TMix));
            //sqlListParam.Add(new SqlParameter("@Formula", objpakaiBB.Formula));
            //sqlListParam.Add(new SqlParameter("@SdL", objpakaiBB.SdL));
            //sqlListParam.Add(new SqlParameter("@SspB", objpakaiBB.SspB));
            //sqlListParam.Add(new SqlParameter("@AT", objpakaiBB.AT));
            //sqlListParam.Add(new SqlParameter("@KL", objpakaiBB.KL));
            //sqlListParam.Add(new SqlParameter("@Efis", objpakaiBB.Efis));
            //sqlListParam.Add(new SqlParameter("@JKertasVirgin", objpakaiBB.JKertasVirgin));
            //sqlListParam.Add(new SqlParameter("@KvKg", objpakaiBB.KvKg));
            //sqlListParam.Add(new SqlParameter("@KvKg2", objpakaiBB.KvKg2));
            //sqlListParam.Add(new SqlParameter("@KvEfis", objpakaiBB.KvEfis));
            //sqlListParam.Add(new SqlParameter("@BkBimaKg", objpakaiBB.BkBimaKg));
            //sqlListParam.Add(new SqlParameter("@SampahBima", objpakaiBB.SampahBima));
            //sqlListParam.Add(new SqlParameter("@BbBimaKg", objpakaiBB.BbBimaKg));
            //sqlListParam.Add(new SqlParameter("@KsKg0", objpakaiBB.KsKg0));
            //sqlListParam.Add(new SqlParameter("@KsKg", objpakaiBB.KsKg));
            //sqlListParam.Add(new SqlParameter("@KsEfis", objpakaiBB.KsEfis));
            //sqlListParam.Add(new SqlParameter("@KuKg", objpakaiBB.KuKg));
            //sqlListParam.Add(new SqlParameter("@KuEfs", objpakaiBB.KuEfs));
            //sqlListParam.Add(new SqlParameter("@KraftBkKg", objpakaiBB.KraftBkKg));
            //sqlListParam.Add(new SqlParameter("@SampahKraft", objpakaiBB.SampahKraft));
            //sqlListParam.Add(new SqlParameter("@KraftBbKg", objpakaiBB.KraftBbKg));
            //sqlListParam.Add(new SqlParameter("@Kgrade2Kg", objpakaiBB.Kgrade2Kg));
            //sqlListParam.Add(new SqlParameter("@Kgrade2Eff", objpakaiBB.Kgrade2Eff));
            //sqlListParam.Add(new SqlParameter("@Kgrade3Kg", objpakaiBB.Kgrade3Kg));
            //sqlListParam.Add(new SqlParameter("@Kgrade3xKg", objpakaiBB.Kgrade3xKg));
            //sqlListParam.Add(new SqlParameter("@Kgrade3Eff", objpakaiBB.Kgrade3Eff));
            //sqlListParam.Add(new SqlParameter("@TEfis", objpakaiBB.TEfis));
            //sqlListParam.Add(new SqlParameter("@KV", objpakaiBB.Kv));
            //sqlListParam.Add(new SqlParameter("@KB", objpakaiBB.Kb));
            //sqlListParam.Add(new SqlParameter("@KS", objpakaiBB.Ks));
            //sqlListParam.Add(new SqlParameter("@KGradeUtama", objpakaiBB.KGradeUtama));
            //sqlListParam.Add(new SqlParameter("@Kgrade2", objpakaiBB.Kgrade2));
            //sqlListParam.Add(new SqlParameter("@Kgrade3", objpakaiBB.Kgrade3));
            ////sqlListParam.Add(new SqlParameter("@CreatedBy", objpakaiBB.CreatedBy));
            //sqlListParam.Add(new SqlParameter("@LastModifiedBy", objpakaiBB.LastModifiedBy));
            DataAccess da = new DataAccess(Global.ConnectionString());
            int result = da.ProcessData(sqlListParam, "UpdatePBB2_SPNew");
            strError = da.Error;
            return result;
        }

        catch (Exception ex)
        {
            strError = ex.Message;
            return -1;
        }
    }


    public int RetrieveInputan(string Periode)
    {
        string StrSql =
        " select SUM(Total)Total from (select COUNT(ID)Total from BM_Pulp" +
        " where LEFT(convert(char,Tgl_Prod,112),6)='" + Periode + "' and Rowstatus>-1 union all select '0'Total ) as xx ";
        DataAccess dataAccess = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = dataAccess.RetrieveDataByString(StrSql);
        strError = dataAccess.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToInt32(sqlDataReader["Total"]);
            }
        }

        return 0;
    }


    private string PulpReportCtrp(string Periode, string Periode2)
    {
        string result = " declare @date datetime set @date =  '" + Periode + "'; " +
                        " with DaysInMonth as (select @date as Date " +
                        " union all  " +
                        " select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +

                        " PulpVirgin As (select C.Quantity,A.PakaiDate from Pakai A " +
                                     " INNER JOIN PakaiDetail C ON A.ID=C.PakaiID " +
                                     " where LEFT(convert(char,A.pakaidate,112),6)='" + Periode2 + "' and C.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C.ItemID and JID=1 and RowStatus>-1) " +
                                     " and A.Status=3  )," +

                        " KertasBima As (select C0.Quantity,A0.PakaiDate from Pakai A0 " +
                                     " INNER JOIN PakaiDetail C0 ON A0.ID=C0.PakaiID " +
                                     " where LEFT(convert(char,A0.pakaidate,112),6)='" + Periode2 + "' and C0.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C0.ItemID and JID=2 and RowStatus>-1) " +
                                     " and A0.Status=3  )," +

                        " KertasSemen As (select C01.Quantity,A01.PakaiDate from Pakai A01 " +
                                     " INNER JOIN PakaiDetail C01 ON A01.ID=C01.PakaiID " +
                                     " where LEFT(convert(char,A01.pakaidate,112),6)='" + Periode2 + "' and C01.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C01.ItemID and JID=3 and RowStatus>-1) " +
                                     " and A01.Status=3  )," +

                        " GradeUtama As (select C02.Quantity,A02.PakaiDate from Pakai A02 " +
                                     " INNER JOIN PakaiDetail C02 ON A02.ID=C02.PakaiID " +
                                     " where LEFT(convert(char,A02.pakaidate,112),6)='" + Periode2 + "' and C02.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C02.ItemID and JID=4 and RowStatus>-1) " +
                                     " and A02.Status=3  )," +

                        " Grade2 As (select C03.Quantity,A03.PakaiDate from Pakai A03 " +
                                     " INNER JOIN PakaiDetail C03 ON A03.ID=C03.PakaiID " +
                                     " where LEFT(convert(char,A03.pakaidate,112),6)='" + Periode2 + "' and C03.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C03.ItemID and JID=5 and RowStatus>-1) " +
                                     " and A03.Status=3  )," +

                        " Grade3 As (select C04.Quantity,A04.PakaiDate from Pakai A04 " +
                                     " INNER JOIN PakaiDetail C04 ON A04.ID=C04.PakaiID " +
                                     " where LEFT(convert(char,A04.pakaidate,112),6)='" + Periode2 + "' and C04.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C04.ItemID and JID=6 and RowStatus>-1) " +
                                     " and A04.Status=3  ), " +

                        " KraftPlastik As (select C05.Quantity,A05.PakaiDate from Pakai A05 " +
                                     " INNER JOIN PakaiDetail C05 ON A05.ID=C05.PakaiID " +
                                     " where LEFT(convert(char,A05.pakaidate,112),6)='" + Periode2 + "' and C05.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C05.ItemID and JID=7 and RowStatus>-1) " +
                                     " and A05.Status=3  ) " +

                        " select Tgl_Prod,Tgl_Prod2,sum(QtyPulpVirgin)QtyPulpVirgin,sum(QtyKertasBima)QtyKertasBima,sum(QtyKertasSemen)QtyKertasSemen,sum(QtyGradeUtama)QtyGradeUtama, " +
                        " sum(QtyGrade2)QtyGrade2,sum(QtyGrade3)QtyGrade3,sum(QtyKraftPlastik)QtyKraftPlastik " +
                        " from ( " +
                        " select Tgl_Prod,Tgl_Prod2,sum(QtyPulpVirgin)QtyPulpVirgin,sum(QtyKertasBima)QtyKertasBima,sum(QtyKertasSemen)QtyKertasSemen,sum(QtyGradeUtama)QtyGradeUtama, " +
                        " sum(QtyGrade2)QtyGrade2,sum(QtyGrade3)QtyGrade3,sum(QtyKraftPlastik)QtyKraftPlastik " +
                        " from ( " +
                        " select Tgl_Prod,Tgl_Prod2,QtyPulpVirgin,QtyKertasBima,QtyKertasSemen,QtyGradeUtama,QtyGrade2,QtyGrade3,QtyKraftPlastik " +
                        " from ( " +
                        " select LEFT(convert(char,A.date,106),11)Tgl_Prod,A.Date Tgl_Prod2, " +
                        " isnull(sum(((C.Quantity))),0) 'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A " +
                        " left Join PulpVirgin C on C.PakaiDate=A.Date " +
                        " where month(date) = month(@Date)  group by A.Date,C.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A0.date,106),11)Tgl_Prod,A0.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',isnull(sum(((C0.Quantity))),0) 'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A0 " +
                        " left Join KertasBima C0 on C0.PakaiDate=A0.Date " +
                        " where month(date) = month(@Date)  group by A0.Date,C0.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A01.date,106),11)Tgl_Prod,A01.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',isnull(sum(((C01.Quantity))),0) 'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A01 " +
                        " left Join KertasSemen C01 on C01.PakaiDate=A01.Date " +
                        " where month(date) = month(@Date)  group by A01.Date,C01.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A02.date,106),11)Tgl_Prod,A02.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',isnull(sum(((C02.Quantity))),0)'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A02 " +
                        " left Join GradeUtama C02 on C02.PakaiDate=A02.Date " +
                        " where month(date) = month(@Date)  group by A02.Date,C02.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A03.date,106),11)Tgl_Prod,A03.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',isnull(sum(((C03.Quantity))),0)'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A03 " +
                        " left Join Grade2 C03 on C03.PakaiDate=A03.Date " +
                        " where month(date) = month(@Date)  group by A03.Date,C03.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A04.date,106),11)Tgl_Prod,A04.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',isnull(sum(((C04.Quantity))),0)'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A04 " +
                        " left Join Grade3 C04 on C04.PakaiDate=A04.Date " +
                        " where month(date) = month(@Date)  group by A04.Date,C04.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A05.date,106),11)Tgl_Prod,A05.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',isnull(sum(((C05.Quantity))),0)'QtyKraftPlastik' " +
                        " from DaysInMonth A05 " +
                        " left Join KraftPlastik C05 on C05.PakaiDate=A05.Date " +
                        " where month(date) = month(@Date)  group by A05.Date,C05.Quantity " +

                        " ) as xx group by Tgl_Prod,Tgl_Prod2,QtyPulpVirgin,QtyKertasBima,QtyKertasSemen,QtyGradeUtama,QtyGrade2,QtyGrade3,QtyKraftPlastik) " +
                        " as xx1  group by Tgl_Prod,Tgl_Prod2,QtyPulpVirgin,QtyKertasBima,QtyKertasSemen,QtyGradeUtama,QtyGrade2,QtyGrade3,QtyKraftPlastik) as xx2 group by Tgl_Prod,Tgl_Prod2 ";
        return result;
    }

    private string PulpReportKrwg(string Periode, string Periode2)
    {
        string result = " declare @date datetime set @date =  '" + Periode + "'; " +
                        " with DaysInMonth as (select @date as Date " +
                        " union all  " +
                        " select dateadd(dd,1,Date)  from DaysInMonth  where month(date) = month(@Date)), " +

                        " PulpVirgin As (select C.Quantity,A.PakaiDate from Pakai A " +
                                     " INNER JOIN PakaiDetail C ON A.ID=C.PakaiID " +
                                     " where LEFT(convert(char,A.pakaidate,112),6)='" + Periode2 + "' and C.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C.ItemID and JID=1 and RowStatus>-1) " +
                                     " and A.Status=3  )," +

                        " KertasBima As (select C0.Quantity,A0.PakaiDate from Pakai A0 " +
                                     " INNER JOIN PakaiDetail C0 ON A0.ID=C0.PakaiID " +
                                     " where LEFT(convert(char,A0.pakaidate,112),6)='" + Periode2 + "' and C0.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C0.ItemID and JID=2 and RowStatus>-1) " +
                                     " and A0.Status=3  )," +

                        " KertasSemen As (select C01.Quantity,A01.PakaiDate from Pakai A01 " +
                                     " INNER JOIN PakaiDetail C01 ON A01.ID=C01.PakaiID " +
                                     " where LEFT(convert(char,A01.pakaidate,112),6)='" + Periode2 + "' and C01.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C01.ItemID and JID=3 and RowStatus>-1) " +
                                     " and A01.Status=3  )," +

                        " GradeUtama As (select C02.Quantity,A02.PakaiDate from Pakai A02 " +
                                     " INNER JOIN PakaiDetail C02 ON A02.ID=C02.PakaiID " +
                                     " where LEFT(convert(char,A02.pakaidate,112),6)='" + Periode2 + "' and C02.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C02.ItemID and JID=4 and RowStatus>-1) " +
                                     " and A02.Status=3  )," +

                        " Grade2 As (select C03.Quantity,A03.PakaiDate from Pakai A03 " +
                                     " INNER JOIN PakaiDetail C03 ON A03.ID=C03.PakaiID " +
                                     " where LEFT(convert(char,A03.pakaidate,112),6)='" + Periode2 + "' and C03.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C03.ItemID and JID=5 and RowStatus>-1) " +
                                     " and A03.Status=3  )," +

                        " Grade3 As (select C04.Quantity,A04.PakaiDate from Pakai A04 " +
                                     " INNER JOIN PakaiDetail C04 ON A04.ID=C04.PakaiID " +
                                     " where LEFT(convert(char,A04.pakaidate,112),6)='" + Periode2 + "' and C04.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C04.ItemID and JID=6 and RowStatus>-1) " +
                                     " and A04.Status=3  ), " +

                        " KraftPlastik As (select C05.Quantity,A05.PakaiDate from Pakai A05 " +
                                     " INNER JOIN PakaiDetail C05 ON A05.ID=C05.PakaiID " +
                                     " where LEFT(convert(char,A05.pakaidate,112),6)='" + Periode2 + "' and C05.ItemID  " +
                                     " in (select ItemID from Pulp_Formula where ItemID=C05.ItemID and JID=7 and RowStatus>-1) " +
                                     " and A05.Status=3  ) " +

                        " select Tgl_Prod,Tgl_Prod2,sum(QtyPulpVirgin)QtyPulpVirgin,sum(QtyKertasBima)QtyKertasBima,sum(QtyKertasSemen)QtyKertasSemen,sum(QtyGradeUtama)QtyGradeUtama, " +
                        " sum(QtyGrade2)QtyGrade2,sum(QtyGrade3)QtyGrade3,sum(QtyKraftPlastik)QtyKraftPlastik " +
                        " from ( " +
                        " select Tgl_Prod,Tgl_Prod2,sum(QtyPulpVirgin)QtyPulpVirgin,sum(QtyKertasBima)QtyKertasBima,sum(QtyKertasSemen)QtyKertasSemen,sum(QtyGradeUtama)QtyGradeUtama, " +
                        " sum(QtyGrade2)QtyGrade2,sum(QtyGrade3)QtyGrade3,sum(QtyKraftPlastik)QtyKraftPlastik " +
                        " from ( " +
                        " select Tgl_Prod,Tgl_Prod2,QtyPulpVirgin,QtyKertasBima,QtyKertasSemen,QtyGradeUtama,QtyGrade2,QtyGrade3,QtyKraftPlastik " +
                        " from ( " +
                        " select LEFT(convert(char,A.date,106),11)Tgl_Prod,A.Date Tgl_Prod2, " +
                        " isnull(sum(((C.Quantity))),0) 'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A " +
                        " left Join PulpVirgin C on C.PakaiDate=A.Date " +
                        " where month(date) = month(@Date)  group by A.Date,C.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A0.date,106),11)Tgl_Prod,A0.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',isnull(sum(((C0.Quantity))),0) 'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A0 " +
                        " left Join KertasBima C0 on C0.PakaiDate=A0.Date " +
                        " where month(date) = month(@Date)  group by A0.Date,C0.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A01.date,106),11)Tgl_Prod,A01.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',isnull(sum(((C01.Quantity))),0) 'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A01 " +
                        " left Join KertasSemen C01 on C01.PakaiDate=A01.Date " +
                        " where month(date) = month(@Date)  group by A01.Date,C01.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A02.date,106),11)Tgl_Prod,A02.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',isnull(sum(((C02.Quantity))),0)'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A02 " +
                        " left Join GradeUtama C02 on C02.PakaiDate=A02.Date " +
                        " where month(date) = month(@Date)  group by A02.Date,C02.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A03.date,106),11)Tgl_Prod,A03.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',isnull(sum(((C03.Quantity))),0)'QtyGrade2',0'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A03 " +
                        " left Join Grade2 C03 on C03.PakaiDate=A03.Date " +
                        " where month(date) = month(@Date)  group by A03.Date,C03.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A04.date,106),11)Tgl_Prod,A04.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',isnull(sum(((C04.Quantity))),0)'QtyGrade3',0'QtyKraftPlastik' " +
                        " from DaysInMonth A04 " +
                        " left Join Grade3 C04 on C04.PakaiDate=A04.Date " +
                        " where month(date) = month(@Date)  group by A04.Date,C04.Quantity " +

                        " union all " +

                        " select LEFT(convert(char,A05.date,106),11)Tgl_Prod,A05.Date Tgl_Prod2, " +
                        " 0'QtyPulpVirgin',0'QtyKertasBima',0'QtyKertasSemen',0'QtyGradeUtama',0'QtyGrade2',0'QtyGrade3',isnull(sum(((C05.Quantity))),0)'QtyKraftPlastik' " +
                        " from DaysInMonth A05 " +
                        " left Join KraftPlastik C05 on C05.PakaiDate=A05.Date " +
                        " where month(date) = month(@Date)  group by A05.Date,C05.Quantity " +

                        " ) as xx group by Tgl_Prod,Tgl_Prod2,QtyPulpVirgin,QtyKertasBima,QtyKertasSemen,QtyGradeUtama,QtyGrade2,QtyGrade3,QtyKraftPlastik) " +
                        " as xx1  group by Tgl_Prod,Tgl_Prod2,QtyPulpVirgin,QtyKertasBima,QtyKertasSemen,QtyGradeUtama,QtyGrade2,QtyGrade3,QtyKraftPlastik) as xx2 group by Tgl_Prod,Tgl_Prod2 ";
        return result;
    }

    public decimal Retrievexx0(string tgl)
    {
        string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                        " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                        " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=1 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                        " ) as pakaiData ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
        strError = da.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["Actual"]);
            }
        }
        return 0;

    }

    public decimal Retrievexx01(string tgl)
    {
        string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                        " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                        " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=2 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                        " ) as pakaiData ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
        strError = da.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["Actual"]);
            }
        }
        return 0;

    }
    public decimal Retrievexx02(string tgl)
    {
        string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                        " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                        " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=3 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                        " ) as pakaiData ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
        strError = da.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["Actual"]);
            }
        }
        return 0;

    }
    public decimal Retrievexx03(string tgl)
    {
        string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                        " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                        " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=4 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                        " ) as pakaiData ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
        strError = da.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["Actual"]);
            }
        }
        return 0;

    }
    public decimal Retrievexx04(string tgl)
    {
        string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                        " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                        " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=5 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                        " ) as pakaiData ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
        strError = da.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["Actual"]);
            }
        }
        return 0;

    }
    public decimal Retrievexx05(string tgl)
    {
        string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                        " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                        " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=6 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                        " ) as pakaiData ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
        strError = da.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["Actual"]);
            }
        }
        return 0;

    }
    public decimal Retrievexx06(string tgl)
    {
        string strSQL = " select isnull(sum (pakaiData.Quantity),0) as Actual  From ( " +
                        " select A.PakaiNo,A.PakaiDate,B.ItemID,B.Quantity from Pakai A, PakaiDetail B  where B.PakaiID=A.ID and B.ItemID in " +
                        " (select ItemID from Pulp_Formula where ItemID=B.ItemID and JID=7 and RowStatus>-1) and A.Status=3 and B.RowStatus>-1 and A.PakaiDate='" + tgl + "' and A.DeptID=2  " +
                        " ) as pakaiData ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sqlDataReader = da.RetrieveDataByString(strSQL);
        strError = da.Error;

        if (sqlDataReader.HasRows)
        {
            while (sqlDataReader.Read())
            {
                return Convert.ToDecimal(sqlDataReader["Actual"]);
            }
        }
        return 0;

    }
    public ArrayList RetrieveReportKarawang(string Periode, string Periode2)
    {
        arrData = new ArrayList();
        string strsql = this.PulpReportKrwg(Periode, Periode2);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.PulpReportKrwg(Periode, Periode2));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectKrwg(sdr));
            }
        }
        return arrData;
    }

    public ArrayList RetrieveReportCtrp(string Periode, string Periode2)
    {
        arrData = new ArrayList();
        string strsql = this.PulpReportCtrp(Periode, Periode2);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.PulpReportCtrp(Periode, Periode2));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectKrwg(sdr));
            }
        }
        return arrData;
    }

    private string ReportAdaCtrp(string Periode)
    {
        string result = " select 'A'Urt, ID,Tgl_Prod2,L1,L1x,L1x1,L1x2,L2,L2x,L3,L3x,L4,L4x,L4x1,L5,L5x,L6,L6x,TMix, " +
                        " Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin, " +
                        " KvKg,KvKg2,KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff," +
                        " Kgrade3Kg,Kgrade3Eff," +
                        " TEfis,Kv,kb,ks,KGradeUtama,Kgrade2,Kgrade3 " +
                        " from BM_Pulp where LEFT(convert(char,Tgl_Prod,112),6)='" + Periode + "' and RowStatus>-1 " +
                        " union all " +
                        " select 'B'Urt,''ID,'TOTAL'TglProd2, sum(L1)L1,sum(L1x)L1x,sum(L1x1)L1x1,sum(L1x2)L1x2,sum(L2)L2,sum(L2x)L2x,sum(L3)L3,sum(L3x)L3x, " +
                        " sum(L4)L4,sum(L4x)L4x,sum(L4x1)L4x1,sum(L5)L5,sum(L5x)L5x,sum(L6)L6,sum(L6x)L6x,sum(TMix)TMix,sum(Formula)Formula, " +
                        " sum(SdL)SdL,sum(SspB)SspB,sum(AT)[AT],sum(KL)KL,((sum(KL)-sum(Formula))/sum(Formula)*100)Efis,'' , sum(KvKg)KvKg ,sum(KvKg2)KvKg2, " +
                        " AVG(KvEfis)KvEfis,sum(BkBimaKg)BkBimaKg,sum(SampahBima)SampahBima,sum(BbBimaKg)BbBimaKg,sum(KsKg0)KsKg0,sum(KsKg)KsKg,AVG(KsEfis)KsEfis,sum(KuKg)KuKg, " +
                        " Avg(KuEfs)KuEfis,sum(Kgrade2Kg)Kgrade2Kg,Avg(Kgrade2Eff)Kgrade2Eff, " +
                        " sum(Kgrade3Kg)Kgrade3Kg,avg(Kgrade3Eff)Kgrade3Eff,(((sum(Kvkg2)+ sum(KsKg)+sum(KuKg)+sum(Kgrade2Kg)+sum(Kgrade3Kg))-sum(Formula))/sum(Formula)*100)Tefis,'0','0','0' " +
                        " ,'0','0','0' from BM_Pulp where LEFT(convert(char,Tgl_Prod,112),6)='" + Periode + "' and RowStatus>-1 order by urt,ID ";
        return result;
    }

    public ArrayList RetrieveReportAdaInputCtrp(string Periode)
    {
        arrData = new ArrayList();
        string strsql = this.ReportAdaCtrp(Periode);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.ReportAdaCtrp(Periode));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectCtrp0(sdr));
            }
        }
        return arrData;
    }

    private string ReportAdaJmbg(string Periode)
    {
        string result = " select 'A'Urt, ID,Tgl_Prod2,L1,L1x,L1x1,L1x2,TMix, " +
                        " Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin, " +
                        " KvKg,KvKg2,KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff," +
                        " Kgrade3Kg,Kgrade3Eff," +
                        " TEfis,Kv,kb,ks,KGradeUtama,Kgrade2,Kgrade3 " +
                        " from BM_Pulp where LEFT(convert(char,Tgl_Prod,112),6)='" + Periode + "' and RowStatus>-1 " +
                        " union all " +
                        " select 'B'Urt,''ID,'TOTAL'TglProd2, sum(L1)L1,sum(L1x)L1x,sum(L1x1)L1x1,sum(L1x2)L1x2 " +
                        " ,sum(TMix)TMix,sum(Formula)Formula, " +
                        " sum(SdL)SdL,sum(SspB)SspB,sum(AT)[AT],sum(KL)KL,((sum(KL)-sum(Formula))/sum(Formula)*100)Efis,'' , sum(KvKg)KvKg ,sum(KvKg2)KvKg2, " +
                        " AVG(KvEfis)KvEfis,sum(BkBimaKg)BkBimaKg,sum(SampahBima)SampahBima,sum(BbBimaKg)BbBimaKg,sum(KsKg0)KsKg0,sum(KsKg)KsKg,AVG(KsEfis)KsEfis,sum(KuKg)KuKg, " +
                        " Avg(KuEfs)KuEfis,sum(Kgrade2Kg)Kgrade2Kg,Avg(Kgrade2Eff)Kgrade2Eff, " +
                        " sum(Kgrade3Kg)Kgrade3Kg,avg(Kgrade3Eff)Kgrade3Eff,(((sum(Kvkg2)+ sum(KsKg)+sum(KuKg)+sum(Kgrade2Kg))-sum(Formula))/sum(Formula)*100)Tefis,'0','0','0' " +
                        " ,'0','0','0' from BM_Pulp where LEFT(convert(char,Tgl_Prod,112),6)='" + Periode + "' and RowStatus>-1 order by urt,ID ";
        return result;
    }

    public ArrayList RetrieveReportAdaInputJmbg(string Periode)
    {
        arrData = new ArrayList();
        string strsql = this.ReportAdaJmbg(Periode);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.ReportAdaJmbg(Periode));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectJmbg0(sdr));
            }
        }
        return arrData;
    }


    private string ReportAdaKrwg(string Periode)
    {
        string result = " select 'A'Urt, ID,Tgl_Prod2,L1,L1x,L2,L2x,L2x1,L3,L3x,L4,L4x,L4x1,L5,L5x,L5x1,L6,L6x,L6x1,TMix, " +
                        " Formula,SdL,SspB,AT,KL,Efis,JKertasVirgin," +
                        " KvKg,KvKg2,KvEfis,BkBimaKg,SampahBima,BbBimaKg,KsKg0,KsKg,KsEfis,KuKg,KuEfs,Kgrade2Kg,Kgrade2Eff,KraftBkKg, " +
                        " SampahKraft,KraftBbKg,Kgrade3Kg,Kgrade3xKg,Kgrade3Eff," +
                        " TEfis,Kv,kb,ks,KGradeUtama,Kgrade2,Kgrade3 " +
                        " from BM_Pulp where LEFT(convert(char,Tgl_Prod,112),6)='" + Periode + "'  " +
                        " union all " +
                        " select 'B'Urt,''ID,'TOTAL'TglProd2, sum(L1)L1,sum(L1x)L1x,sum(L2)L2,sum(L2x)L2x,sum(L2x1)L2x1,sum(L3)L3,sum(L3x)L3x, " +
                        " sum(L4)L4,sum(L4x)L4x,sum(L4x1)L4x1,sum(L5)L5,sum(L5x)L5x,sum(L5x1)L5x1,sum(L6)L6,sum(L6x)L6x,sum(L6x1)L6x,sum(TMix)TMix,sum(Formula)Formula, " +
                        " sum(SdL)SdL,sum(SspB)SspB,sum(AT)[AT],sum(KL)KL,((sum(KL)-sum(Formula))/sum(Formula)*100)Efis,'' , sum(KvKg)KvKg ,sum(KvKg2)KvKg2, " +
                        " AVG(KvEfis)KvEfis,sum(BkBimaKg)BkBimaKg,sum(SampahBima)SampahBima,sum(BbBimaKg)BbBimaKg,sum(KsKg0)KsKg0,sum(KsKg)KsKg,AVG(KsEfis)KsEfis,sum(KuKg)KuKg, " +
                        " Avg(KuEfs)KuEfis,sum(Kgrade2Kg)Kgrade2Kg,Avg(Kgrade2Eff)Kgrade2Eff,sum(KraftBkKg)KraftBkKg,sum(SampahKraft)SampahKraft,sum(KraftBbKg)KraftBbKg, " +
                        " sum(Kgrade3Kg)Kgrade3Kg,sum(Kgrade3xKg)Kgrade3xKg,avg(Kgrade3Eff)Kgrade3Eff,(((sum(Kvkg2)+ sum(KsKg)+sum(KuKg)+sum(Kgrade2Kg)+sum(Kgrade3xKg))-sum(Formula))/sum(Formula)*100)Tefis,'0','0','0' " +
                        " ,'0','0','0' from BM_Pulp where LEFT(convert(char,Tgl_Prod,112),6)='" + Periode + "' and RowStatus>-1 order by urt,ID ";
        return result;
    }


    public ArrayList RetrieveReportAdaInputKRwg(string Periode)
    {
        arrData = new ArrayList();
        string strsql = this.ReportAdaKrwg(Periode);
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(this.ReportAdaKrwg(Periode));
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                arrData.Add(GenerateObjectKrwg0(sdr));
            }
        }
        return arrData;
    }






    private PakaiBahanBaku GenerateObjectKrwg(SqlDataReader sdr)
    {
        PakaiBahanBaku pkb = new PakaiBahanBaku();
        pkb.Tgl_Prod2 = sdr["Tgl_Prod2"].ToString();
        pkb.Tgl_Prod = Convert.ToDateTime(sdr["Tgl_Prod"]);
        pkb.QtyPulpVirgin = Convert.ToDecimal(sdr["QtyPulpVirgin"]);
        pkb.QtyKertasSemen = Convert.ToDecimal(sdr["QtyKertasSemen"]);
        pkb.QtyKertasBima = Convert.ToDecimal(sdr["QtyKertasBima"]);
        pkb.QtyGradeUtama = Convert.ToDecimal(sdr["QtyGradeUtama"]);
        pkb.QtyGrade2 = Convert.ToDecimal(sdr["QtyGrade2"]);
        pkb.QtyGrade3 = Convert.ToDecimal(sdr["QtyGrade3"]);
        pkb.QtyKraftPlastik = Convert.ToDecimal(sdr["QtyKraftPlastik"]);
        return pkb;
    }

    private PakaiBahanBaku GenerateObjectKrwg0(SqlDataReader sdr)
    {
        PakaiBahanBaku pkb = new PakaiBahanBaku();
        pkb.ID = Convert.ToInt32(sdr["ID"]);
        pkb.Tgl_Prod2 = sdr["Tgl_Prod2"].ToString();
        //pkb.Tgl_Prod = Convert.ToDateTime(sdr["Tgl_Prod"]);
        pkb.L1 = Convert.ToDecimal(sdr["L1"]);
        pkb.L1x = Convert.ToDecimal(sdr["L1x"]);
        pkb.L2 = Convert.ToDecimal(sdr["L2"]);
        pkb.L2x = Convert.ToDecimal(sdr["L2x"]);
        pkb.L2x1 = Convert.ToDecimal(sdr["L2x1"]);
        pkb.L3 = Convert.ToDecimal(sdr["L3"]);
        pkb.L3x = Convert.ToDecimal(sdr["L3x"]);
        pkb.L4 = Convert.ToDecimal(sdr["L4"]);
        pkb.L4x = Convert.ToDecimal(sdr["L4x"]);
        pkb.L4x1 = Convert.ToDecimal(sdr["L4x1"]);
        pkb.L5 = Convert.ToDecimal(sdr["L5"]);
        pkb.L5x = Convert.ToDecimal(sdr["L5x"]);
        pkb.L5x1 = Convert.ToDecimal(sdr["L5x1"]);
        pkb.L6 = Convert.ToDecimal(sdr["L6"]);
        pkb.L6x = Convert.ToDecimal(sdr["L6x"]);
        pkb.L6x1 = Convert.ToDecimal(sdr["L6x1"]);
        pkb.TMix = Convert.ToDecimal(sdr["TMix"]);
        pkb.Formula = Convert.ToDecimal(sdr["Formula"]);
        pkb.SdL = Convert.ToDecimal(sdr["SdL"]);
        pkb.SspB = Convert.ToDecimal(sdr["SspB"]);
        pkb.AT = Convert.ToDecimal(sdr["AT"]);
        pkb.KL = Convert.ToDecimal(sdr["KL"]);
        pkb.Efis = Convert.ToDecimal(sdr["Efis"]);
        pkb.JKertasVirgin = sdr["JKertasVirgin"].ToString();
        pkb.KvKg = Convert.ToDecimal(sdr["KvKg"]);
        pkb.KvKg2 = Convert.ToDecimal(sdr["KvKg2"]);
        pkb.KvEfis = Convert.ToDecimal(sdr["KvEfis"]);
        pkb.BkBimaKg = Convert.ToDecimal(sdr["BkBimaKg"]);
        pkb.SampahBima = Convert.ToDecimal(sdr["SampahBima"]);
        pkb.BbBimaKg = Convert.ToDecimal(sdr["BbBimaKg"]);
        pkb.KsKg0 = Convert.ToDecimal(sdr["KsKg0"]);
        pkb.KsKg = Convert.ToDecimal(sdr["KsKg"]);
        pkb.KsEfis = Convert.ToDecimal(sdr["KsEfis"]);
        pkb.KuKg = Convert.ToDecimal(sdr["KuKg"]);
        pkb.KuEfs = Convert.ToDecimal(sdr["KuEfs"]);
        pkb.Kgrade2Kg = Convert.ToDecimal(sdr["Kgrade2Kg"]);
        pkb.Kgrade2Eff = Convert.ToDecimal(sdr["Kgrade2Eff"]);
        pkb.KraftBkKg = Convert.ToDecimal(sdr["KraftBkKg"]);
        pkb.SampahKraft = Convert.ToDecimal(sdr["SampahKraft"]);
        pkb.KraftBkKg = Convert.ToDecimal(sdr["KraftBkKg"]);
        pkb.KraftBbKg = Convert.ToDecimal(sdr["KraftBbKg"]);
        pkb.Kgrade3Kg = Convert.ToDecimal(sdr["Kgrade3Kg"]);
        pkb.Kgrade3xKg = Convert.ToDecimal(sdr["Kgrade3xKg"]);
        pkb.Kgrade3Eff = Convert.ToDecimal(sdr["Kgrade3Eff"]);
        pkb.TEfis = Convert.ToDecimal(sdr["TEfis"]);
        pkb.Kv = Convert.ToDecimal(sdr["Kv"]);
        pkb.Kb = Convert.ToDecimal(sdr["Kb"]);
        pkb.Ks = Convert.ToDecimal(sdr["Ks"]);
        pkb.KGradeUtama = Convert.ToDecimal(sdr["KGradeUtama"]);
        pkb.Kgrade2 = Convert.ToDecimal(sdr["Kgrade2"]);
        pkb.Kgrade3 = Convert.ToDecimal(sdr["Kgrade3"]);
        return pkb;
    }

    private PakaiBahanBaku GenerateObjectCtrp0(SqlDataReader sdr)
    {
        PakaiBahanBaku pkb = new PakaiBahanBaku();
        pkb.ID = Convert.ToInt32(sdr["ID"]);
        pkb.Tgl_Prod2 = sdr["Tgl_Prod2"].ToString();
        //pkb.Tgl_Prod = Convert.ToDateTime(sdr["Tgl_Prod"]);
        pkb.L1 = Convert.ToDecimal(sdr["L1"]);
        pkb.L1x = Convert.ToDecimal(sdr["L1x"]);
        pkb.L1x1 = Convert.ToDecimal(sdr["L1x1"]);
        pkb.L1x2 = Convert.ToDecimal(sdr["L1x2"]);
        pkb.L2 = Convert.ToDecimal(sdr["L2"]);
        pkb.L2x = Convert.ToDecimal(sdr["L2x"]);
        pkb.L3 = Convert.ToDecimal(sdr["L3"]);
        pkb.L3x = Convert.ToDecimal(sdr["L3x"]);
        pkb.L4 = Convert.ToDecimal(sdr["L4"]);
        pkb.L4x = Convert.ToDecimal(sdr["L4x"]);
        pkb.L4x1 = Convert.ToDecimal(sdr["L4x1"]);
        pkb.TMix = Convert.ToDecimal(sdr["TMix"]);
        pkb.Formula = Convert.ToDecimal(sdr["Formula"]);
        pkb.SdL = Convert.ToDecimal(sdr["SdL"]);
        pkb.SspB = Convert.ToDecimal(sdr["SspB"]);
        pkb.AT = Convert.ToDecimal(sdr["AT"]);
        pkb.KL = Convert.ToDecimal(sdr["KL"]);
        pkb.Efis = Convert.ToDecimal(sdr["Efis"]);
        pkb.JKertasVirgin = sdr["JKertasVirgin"].ToString();
        pkb.KvKg = Convert.ToDecimal(sdr["KvKg"]);
        pkb.KvKg2 = Convert.ToDecimal(sdr["KvKg2"]);
        pkb.KvEfis = Convert.ToDecimal(sdr["KvEfis"]);
        //pkb.BkBimaKg = Convert.ToDecimal(sdr["BkBimaKg"]);
        //pkb.SampahBima = Convert.ToDecimal(sdr["SampahBima"]);
        //pkb.BbBimaKg = Convert.ToDecimal(sdr["BbBimaKg"]);
        //pkb.KsKg0 = Convert.ToDecimal(sdr["KsKg0"]);
        pkb.KsKg = Convert.ToDecimal(sdr["KsKg"]);
        pkb.KsEfis = Convert.ToDecimal(sdr["KsEfis"]);
        pkb.KuKg = Convert.ToDecimal(sdr["KuKg"]);
        pkb.KuEfs = Convert.ToDecimal(sdr["KuEfs"]);
        pkb.Kgrade2Kg = Convert.ToDecimal(sdr["Kgrade2Kg"]);
        pkb.Kgrade2Eff = Convert.ToDecimal(sdr["Kgrade2Eff"]);
        //pkb.KraftBkKg = Convert.ToDecimal(sdr["KraftBkKg"]);
        //pkb.SampahKraft = Convert.ToDecimal(sdr["SampahKraft"]);
        //pkb.KraftBkKg = Convert.ToDecimal(sdr["KraftBkKg"]);
        //pkb.KraftBbKg = Convert.ToDecimal(sdr["KraftBbKg"]);
        pkb.Kgrade3Kg = Convert.ToDecimal(sdr["Kgrade3Kg"]);
        ////pkb.Kgrade3xKg = Convert.ToDecimal(sdr["Kgrade3xKg"]);
        pkb.Kgrade3Eff = Convert.ToDecimal(sdr["Kgrade3Eff"]);
        pkb.TEfis = Convert.ToDecimal(sdr["TEfis"]);
        pkb.Kv = Convert.ToDecimal(sdr["Kv"]);
        //pkb.Kb = Convert.ToDecimal(sdr["Kb"]);
        pkb.Ks = Convert.ToDecimal(sdr["Ks"]);
        pkb.KGradeUtama = Convert.ToDecimal(sdr["KGradeUtama"]);
        pkb.Kgrade2 = Convert.ToDecimal(sdr["Kgrade2"]);
        pkb.Kgrade3 = Convert.ToDecimal(sdr["Kgrade3"]);
        return pkb;
    }

    private PakaiBahanBaku GenerateObjectJmbg0(SqlDataReader sdr)
    {
        PakaiBahanBaku pkb = new PakaiBahanBaku();
        pkb.ID = Convert.ToInt32(sdr["ID"]);
        pkb.Tgl_Prod2 = sdr["Tgl_Prod2"].ToString();
        //pkb.Tgl_Prod = Convert.ToDateTime(sdr["Tgl_Prod"]);
        pkb.L1 = Convert.ToDecimal(sdr["L1"]);
        pkb.L1x = Convert.ToDecimal(sdr["L1x"]);
        pkb.L1x1 = Convert.ToDecimal(sdr["L1x1"]);
        pkb.L1x2 = Convert.ToDecimal(sdr["L1x2"]);
        //pkb.L2 = Convert.ToDecimal(sdr["L2"]);
        //pkb.L2x = Convert.ToDecimal(sdr["L2x"]);
        //pkb.L3 = Convert.ToDecimal(sdr["L3"]);
        //pkb.L3x = Convert.ToDecimal(sdr["L3x"]);
        //pkb.L4 = Convert.ToDecimal(sdr["L4"]);
        //pkb.L4x = Convert.ToDecimal(sdr["L4x"]);
        //pkb.L4x1 = Convert.ToDecimal(sdr["L4x1"]);
        pkb.TMix = Convert.ToDecimal(sdr["TMix"]);
        pkb.Formula = Convert.ToDecimal(sdr["Formula"]);
        pkb.SdL = Convert.ToDecimal(sdr["SdL"]);
        pkb.SspB = Convert.ToDecimal(sdr["SspB"]);
        pkb.AT = Convert.ToDecimal(sdr["AT"]);
        pkb.KL = Convert.ToDecimal(sdr["KL"]);
        pkb.Efis = Convert.ToDecimal(sdr["Efis"]);
        pkb.JKertasVirgin = sdr["JKertasVirgin"].ToString();
        pkb.KvKg = Convert.ToDecimal(sdr["KvKg"]);
        pkb.KvKg2 = Convert.ToDecimal(sdr["KvKg2"]);
        pkb.KvEfis = Convert.ToDecimal(sdr["KvEfis"]);
        //pkb.BkBimaKg = Convert.ToDecimal(sdr["BkBimaKg"]);
        //pkb.SampahBima = Convert.ToDecimal(sdr["SampahBima"]);
        //pkb.BbBimaKg = Convert.ToDecimal(sdr["BbBimaKg"]);
        //pkb.KsKg0 = Convert.ToDecimal(sdr["KsKg0"]);
        pkb.KsKg = Convert.ToDecimal(sdr["KsKg"]);
        pkb.KsEfis = Convert.ToDecimal(sdr["KsEfis"]);
        pkb.KuKg = Convert.ToDecimal(sdr["KuKg"]);
        pkb.KuEfs = Convert.ToDecimal(sdr["KuEfs"]);
        pkb.Kgrade2Kg = Convert.ToDecimal(sdr["Kgrade2Kg"]);
        pkb.Kgrade2Eff = Convert.ToDecimal(sdr["Kgrade2Eff"]);
        //pkb.KraftBkKg = Convert.ToDecimal(sdr["KraftBkKg"]);
        //pkb.SampahKraft = Convert.ToDecimal(sdr["SampahKraft"]);
        //pkb.KraftBkKg = Convert.ToDecimal(sdr["KraftBkKg"]);
        //pkb.KraftBbKg = Convert.ToDecimal(sdr["KraftBbKg"]);
        pkb.Kgrade3Kg = Convert.ToDecimal(sdr["Kgrade3Kg"]);
        ////pkb.Kgrade3xKg = Convert.ToDecimal(sdr["Kgrade3xKg"]);
        pkb.Kgrade3Eff = Convert.ToDecimal(sdr["Kgrade3Eff"]);
        pkb.TEfis = Convert.ToDecimal(sdr["TEfis"]);
        pkb.Kv = Convert.ToDecimal(sdr["Kv"]);
        //pkb.Kb = Convert.ToDecimal(sdr["Kb"]);
        pkb.Ks = Convert.ToDecimal(sdr["Ks"]);
        pkb.KGradeUtama = Convert.ToDecimal(sdr["KGradeUtama"]);
        pkb.Kgrade2 = Convert.ToDecimal(sdr["Kgrade2"]);
        pkb.Kgrade3 = Convert.ToDecimal(sdr["Kgrade3"]);
        return pkb;
    }

}


public class PakaiBahanBaku : GRCBaseDomain
{
    public int UnitKerjaID { get; set; }
    public int ID { get; set; }
    public int PlantID { get; set; }

    // public DateTime LastModifiedTime { get; set; }

    public DateTime Tanggal2 { get; set; }
    public string Tanggal { get; set; }
    public DateTime Tgl_Prod { get; set; }
    public string Tgl_Prod2 { get; set; }
    public decimal QtyPulpVirgin { get; set; }
    public decimal QtyKertasSemen { get; set; }
    public decimal QtyKertasBima { get; set; }
    public decimal QtyGradeUtama { get; set; }
    public decimal QtyGrade2 { get; set; }
    public decimal QtyGrade3 { get; set; }
    public decimal QtyKraftPlastik { get; set; }

    public decimal L1 { get; set; }
    public decimal L1x { get; set; }
    public decimal L1x1 { get; set; }
    public decimal L1x2 { get; set; }
    public decimal L2 { get; set; }
    public decimal L2x { get; set; }
    public decimal L2x1 { get; set; }
    public decimal L3 { get; set; }
    public decimal L3x { get; set; }
    public decimal L4 { get; set; }
    public decimal L4x { get; set; }
    public decimal L4x1 { get; set; }
    public decimal L5 { get; set; }
    public decimal L5x { get; set; }
    public decimal L5x1 { get; set; }
    public decimal L6 { get; set; }
    public decimal L6x { get; set; }
    public decimal L6x1 { get; set; }
    public decimal TMix { get; set; }
    public decimal Formula { get; set; }
    public decimal SdL { get; set; }
    public decimal SspB { get; set; }
    public decimal AT { get; set; }
    public decimal KL { get; set; }
    public decimal TSpb { get; set; }
    public decimal Efis { get; set; }
    public string JKertasVirgin { get; set; }
    public decimal KvKg { get; set; }
    public decimal KvKg2 { get; set; }
    public decimal KvEfis { get; set; }
    public decimal BkBimaKg { get; set; }
    public decimal SampahBima { get; set; }
    public decimal BbBimaKg { get; set; }
    public decimal KsKg0 { get; set; }
    public decimal KsKg { get; set; }
    public decimal KsEfis { get; set; }
    public decimal KuKg { get; set; }
    public decimal KuEfs { get; set; }
    public decimal Kgrade2Kg { get; set; }
    public decimal Kgrade2Eff { get; set; }
    public decimal KraftBkKg { get; set; }
    public decimal SampahKraft { get; set; }
    public decimal KraftBbKg { get; set; }
    public decimal Kgrade3Kg { get; set; }
    public decimal Kgrade3xKg { get; set; }
    public decimal Kgrade3Eff { get; set; }
    public decimal TEfis { get; set; }
    public decimal Kv { get; set; }
    public decimal Kb { get; set; }
    public decimal Ks { get; set; }
    public decimal KK { get; set; }
    public decimal KGradeUtama { get; set; }
    public decimal Kgrade2 { get; set; }
    public decimal Kgrade3 { get; set; }
}
