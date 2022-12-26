using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessFacade;
using Domain;
using Factory;
using Cogs;
using DataAccessLayer;
using DefectFacade;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;


namespace GRCweb1.Modul.Factory
{
    public partial class PenyerahanPotong : System.Web.UI.Page
    {
        private BSAuto bsauto = new BSAuto();
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
            culture.DateTimeFormat.LongTimePattern = "";
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtDate.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                txtDateSerah.Text = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                //LoadFormula();
                //LoadDataGridTerima();
                txtnopalet.Focus();
                LoadDataJemur();
                LoadCutterGroup();
                PartnoTransit();
                txtnopalet.Attributes.Add("onKeyPress", "doClick('" + txtPartnoOK.ClientID + "','" + btnTansfer0.ClientID + "',event)");
                txtPartnoOK.Attributes.Add("onKeyPress", "doClick('" + txtQtyOK.ClientID + "','',event)");
                txtQtyOK.Attributes.Add("onKeyPress", "doClick('" + txtPartnoBPF.ClientID + "','',event)");
                txtPartnoBPF.Attributes.Add("onKeyPress", "doClick('" + txtQtyBPF.ClientID + "','',event)");
                txtQtyBPF.Attributes.Add("onKeyPress", "doClick('" + txtPartnoBPU.ClientID + "','',event)");
                txtPartnoBPU.Attributes.Add("onKeyPress", "doClick('" + txtQtyBPU.ClientID + "','',event)");
                txtQtyBPU.Attributes.Add("onKeyPress", "doClick('" + txtPartnoBPS.ClientID + "','',event)");
                txtPartnoBPS.Attributes.Add("onKeyPress", "doClick('" + txtQtyBPS.ClientID + "','',event)");
                txtQtyBPS.Attributes.Add("onKeyPress", "doClick('','" + Button1.ClientID + "',event)");
                LoadJenis();
                RBList.SelectedValue = "INT";
                //TextBox txtqtyju = (TextBox)GridView1.Rows[0].FindControl("txtQtyJU");
                //txtqtyju.Attributes.Add("onKeyPress", "doClick('','" + Button1.ClientID + "',event)");
            }
        }
        private void LoadJenis()
        {
            ArrayList arrGroupM = new ArrayList();
            T3_GroupsFacade groupFacade = new T3_GroupsFacade();
            arrGroupM = groupFacade.RetrieveJenis();
            try
            {

                foreach (T3_Groups groups in arrGroupM)
                {
                    RBList.Items.Add(new ListItem(groups.Groups, groups.Groups));
                }
            }
            catch { }
        }
        private void LoadCutterGroup()
        {
            ddlCutter.Items.Clear();
            ArrayList arrcutterGroupFacade = new ArrayList();
            MasterGroupCutterFacade cutterGroupFacade = new MasterGroupCutterFacade();
            arrcutterGroupFacade = cutterGroupFacade.Retrieve();
            ddlCutter.Items.Clear();
            ddlCutter.Items.Add(new ListItem("-- ALL --", "0"));
            foreach (MasterGroupCutter cutterGroup in arrcutterGroupFacade)
            {
                ddlCutter.Items.Add(new ListItem(cutterGroup.GroupCutCode, cutterGroup.ID.ToString()));
            }
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        //private void LoadFormula()
        //{
        //    ddlFormula.Items.Clear();
        //    ArrayList arrFormula = new ArrayList();
        //    FormulaFacade formulaFacade = new FormulaFacade();
        //    arrFormula = formulaFacade.Retrieve1();
        //    ddlFormula.Items.Add(new ListItem(" ", "0"));
        //    foreach (Formula formula in arrFormula)
        //    {
        //        ddlFormula.Items.Add(new ListItem(formula.FormulaCode, formula.ID.ToString()));
        //    }
        //}
        private void clearform()
        {
            LoadDataJemur();
            LoadDataGridSerah(Convert.ToDateTime(txtDateSerah.Text), "serah");
            Session["arrpelarian"] = null;
            ArrayList arrPelarian = new ArrayList();
            T1_Jemur jemur = new T1_Jemur();
            if (Session["arrpelarian"] != null)
                arrPelarian = (ArrayList)Session["arrpelarian"];
            jemur.ItemID0 = 0;
            arrPelarian.Add(jemur);
            Session["arrpelarian"] = arrPelarian;
            GridView1.DataSource = arrPelarian;
            GridView1.DataBind();
            txtPartnoOK.Text = string.Empty;
            txtlokOK.Text = string.Empty;
            txtQtyOK.Text = string.Empty;

            txtPartnoPOK.Text = string.Empty;
            txtlokPOK.Text = string.Empty;
            txtQtyPOK.Text = string.Empty;

            txtPartnoOK1.Text = string.Empty;
            txtlokOK1.Text = string.Empty;
            txtQtyOK1.Text = string.Empty;

            txtPartnoKW.Text = string.Empty;
            txtlokKW.Text = string.Empty;
            txtQtyKW.Text = string.Empty;

            txtPartnoKW1.Text = string.Empty;
            txtlokKW1.Text = string.Empty;
            txtQtyKW1.Text = string.Empty;

            txtPartnoBPF.Text = string.Empty;
            txtlokBPF.Text = string.Empty;
            txtQtyBPF.Text = string.Empty;

            txtPartnoPBP.Text = string.Empty;
            txtlokPBP.Text = string.Empty;
            txtQtyPBP.Text = string.Empty;

            txtPartnoBPF0.Text = string.Empty;
            txtlokBPF0.Text = string.Empty;
            txtQtyBPF0.Text = string.Empty;

            txtPartnoBPU.Text = string.Empty;
            txtlokBPU.Text = string.Empty;
            txtQtyBPU.Text = string.Empty;

            txtPartnoBPU0.Text = string.Empty;
            txtlokBPU0.Text = string.Empty;
            txtQtyBPU0.Text = string.Empty;

            txtPartnoBPS.Text = string.Empty;
            txtQtyBPS.Text = string.Empty;

            LID.Text = "0";
            LTglProd.Text = "0";
            LJenis.Text = "0";
            LLokasi.Text = "0";
            LPartno.Text = "0";
            LPalet.Text = "0";
            LTglJemur.Text = "0";
            LRak.Text = "0";
            LQtyIn.Text = "0";
            LQtyOut.Text = "0";
            txtSerah.Text = "0";
            txtnopalet.Focus();
        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            txtnopalet.Text = string.Empty;
            ChkTglProduksi0.Checked = false;
            LoadDataJemur();
            LoadDataGridSerah(Convert.ToDateTime(txtDateSerah.Text), "serah");
            IsiPartno(0);
            if (RB1000.Checked == true)
                txtPartnoOK1.Focus();
            else
                txtPartnoOK.Focus();
            txtPartnoOK.Attributes.Add("onfocus", "this.select();");
            txtPartnoOK1.Attributes.Add("onfocus", "this.select();");
            if (ChkOven.Checked == true)
                EditOven();
            //LoadDataGridTerima();
        }
        private void LoadDataJemur()
        {
            try
            {
                ArrayList arrjemur = new ArrayList();
                T1_JemurFacade Jemur = new T1_JemurFacade();
                string criteria = string.Empty;
                string tglProd = string.Empty;
                if (ChkTglProduksi0.Checked == false)
                    tglProd = " and convert(varchar,A.TglProduksi,112) >'201301201' and convert(varchar,A.TglProduksi,112) = '" + Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd") + "' ";
                if (RB1000.Checked == true)
                    arrjemur = Jemur.RetrieveforSerah2(tglProd, criteria, "1020");
                if (RB9Mili.Checked == true)
                {
                    if (RBLisPlank2.Checked == true && RBLisflank.Checked == true && RBSource2.Checked == true)
                    {
                        if (RBKali4.Checked == true)
                            arrjemur = Jemur.RetrieveforSerah3(tglProd, criteria, RBKali4.Text.Substring(0, 4));
                        if (RBKali6.Checked == true)
                            arrjemur = Jemur.RetrieveforSerah3(tglProd, criteria, RBKali6.Text.Substring(0, 4));
                        if (RBKali12.Checked == true)
                            arrjemur = Jemur.RetrieveforSerah3(tglProd, criteria, RBKali12.Text.Substring(0, 4));
                    }
                    else
                        arrjemur = Jemur.RetrieveforSerah2(tglProd, criteria, "listplank");
                }
                if (RB4Mili.Checked == true)
                    arrjemur = Jemur.RetrieveforSerah2(tglProd, criteria, " ");
                Session["arrjemur"] = arrjemur;
                GridViewTerimaBP.Visible = false;
                GridViewokbp.Visible = true;
                GridViewokbp.DataSource = arrjemur;
                GridViewokbp.DataBind();
            }
            catch { }
        }

        private void LoadDataGridSerah(DateTime tgl, string listBy)
        {
            ArrayList arrSerah = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();
            string criteria = string.Empty;
            arrSerah = Serah.RetrieveByTglProduksiAll(tgl.ToString("yyyyMMdd"), "", " ");
            Session["arrSerah"] = arrSerah;
            GridViewSerah.DataSource = arrSerah;
            GridViewSerah.DataBind();
        }
        protected void btnTambah_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrPelarian = new ArrayList();
            T1_Jemur jemur = new T1_Jemur();
            int tlari = 0;
            tlari = GridView1.Rows.Count;
            for (int i = 0; i < tlari; i++)
            {
                T1_Jemur lari = new T1_Jemur();
                TextBox txtpartno = (TextBox)GridView1.Rows[i].FindControl("txtPartno");
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                lari.Partno = txtpartno.Text;
                lari.QtyIn = Convert.ToInt32(txtqtyju.Text);
                arrPelarian.Add(lari);
            }
            jemur.Partno = LPartno.Text;
            arrPelarian.Add(jemur);
            Session["arrpelarian"] = arrPelarian;
            GridView1.DataSource = arrPelarian;
            GridView1.DataBind();
        }
        //protected void btnTambahPatNo_ServerClick(object sender, EventArgs e)
        //{
        //    ArrayList arrPOK = new ArrayList();
        //    T1_Jemur POK = new T1_Jemur();
        //    Session["arrPOK"] = null;
        //    int tPOK = 0;
        //    tPOK = GridViewPOK.Rows.Count;
        //    for (int i = 0; i < tPOK; i++)
        //    {
        //        T1_Jemur lPOK = new T1_Jemur();
        //        TextBox txtpartnoPOK = (TextBox)GridViewPOK.Rows[i].FindControl("txtPartnoPOK");
        //        TextBox txtlokPOK = (TextBox)GridViewPOK.Rows[i].FindControl("txtlokPOK");
        //        TextBox txtqtyPOK = (TextBox)GridViewPOK.Rows[i].FindControl("txtQtyPOK");
        //        lPOK.Partno = txtpartnoPOK.Text;
        //        lPOK.Lokasi = txtlokPOK.Text;
        //        lPOK.QtyIn = Convert.ToInt32(txtqtyPOK.Text);
        //        arrPOK.Add(lPOK);
        //    }
        //    Session["arrPOK"] = arrPOK;
        //    string sisi = string.Empty;
        //    if (RBSuperFlank.Checked == true)
        //        sisi = "B1";
        //    else
        //        sisi = "SE";
        //    if (txtPartnoAsal.Text.Trim() == string.Empty)
        //        return;
        //    if (RBKali13.Checked == true)
        //    {
        //        POK.Partno = txtPartnoAsal.Text.Substring(0, 3) + "-3-09001002440" + sisi;
        //        POK.Lokasi = "D99";
        //        arrPOK.Add(POK);
        //        Session["arrPOK"] = arrPOK;
        //    }
        //    if (RBKali14.Checked == true)
        //    {
        //        POK.Partno = txtPartnoAsal.Text.Substring(0, 3) + "-3-09002002440" + sisi;
        //        POK.Lokasi = "D99";
        //        arrPOK.Add(POK);
        //        Session["arrPOK"] = arrPOK;
        //    }
        //    if (RBKali15.Checked == true)
        //    {
        //        POK.Partno = txtPartnoAsal.Text.Substring(0, 3) + "-3-09003002440" + sisi;
        //        POK.Lokasi = "D99";
        //        arrPOK.Add(POK);
        //        Session["arrPOK"] = arrPOK;
        //    }
        //    arrPOK = (ArrayList)Session["arrPOK"];
        //    GridViewPOK.DataSource = arrPOK;
        //    GridViewPOK.DataBind();
        //}
        protected void btnTerima_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridTerima();
        }
        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            if (RB4Mili.Checked == true)
                Simpan();
            if (RB1000.Checked == true)
            {
                if (Convert.ToInt32(txtQtyOK.Text) > 0 && Convert.ToInt32(txtQtyOK1.Text) > 0)
                    Simetris(txtPartnoOK.Text, txtlokOK1.Text, txtQtyOK.Text, txtPartnoOK1.Text, txtlokOK1.Text, txtQtyOK1.Text, LDestID.Text, 2);
                if (Convert.ToInt32(txtQtyKW.Text) > 0 && Convert.ToInt32(txtQtyKW1.Text) > 0)
                    Simetris(txtPartnoKW.Text, txtlokKW1.Text, txtQtyKW.Text, txtPartnoKW1.Text, txtlokKW1.Text, txtQtyKW1.Text, LDestID.Text, 2);
                if (Convert.ToInt32(txtQtyOK.Text) > 0 && Convert.ToInt32(txtQtyOK1.Text) > 0)
                    Simetris(txtPartnoBPF.Text, txtlokBPF0.Text, txtQtyBPF.Text, txtPartnoBPF0.Text, txtlokBPF0.Text, txtQtyBPF0.Text, LDestID.Text, 2);
                if (Convert.ToInt32(txtQtyBPU.Text) > 0 && Convert.ToInt32(txtQtyBPU0.Text) > 0)
                    Simetris(txtPartnoBPU.Text, txtlokBPU0.Text, txtQtyBPU.Text, txtPartnoBPU0.Text, txtlokBPU0.Text, txtQtyBPU0.Text, LDestID.Text, 2);
                Simpan();
            }

            if (RB9Mili.Checked == true)
            {
                if (RBLisflank.Checked == true)
                {
                    string test = txtQtyKW.Text;
                    Simpan();
                    SimpanListPlank();
                }
                if (RBSuperPanel.Checked == true)
                {
                    if (txtlokBPU.Text.Trim() == "I99" || Convert.ToInt32(txtQtyBPF.Text) > 0)
                    {
                        SimpanPanel();
                        if (txtlokBPU.Text.Trim() == "I99")
                            SimpanListPlankPanel();
                        if (Convert.ToInt32(txtQtyBPF.Text) > 0)
                            if (RadioButton1.Checked == true)
                                SimpanListPlankPanelR1();
                            else
                                Simpan();
                    }
                    else
                        Simpan();
                }
            }
            txtnopalet.Text = string.Empty;
            clearform();
        }
        protected void btnTansfer1_ServerClick(object sender, EventArgs e)
        {
            //lvl2LisPlankT1();
            clearform();
        }
        protected void IsiPartno(int DataIndex)
        {
            //clearform();

            if (txtnopalet.Text != string.Empty)
            {
                try
                {

                    if (GridViewokbp.Rows.Count <= DataIndex)
                        DataIndex = 0;
                    if (GridViewokbp.Rows[DataIndex].Cells[0].Text == "0")
                    {
                        return;
                    }
                    LID.Text = GridViewokbp.Rows[DataIndex].Cells[0].Text;
                    LDestID.Text = GridViewokbp.Rows[DataIndex].Cells[1].Text;
                    LTglProd.Text = GridViewokbp.Rows[DataIndex].Cells[2].Text;
                    LJenis.Text = GridViewokbp.Rows[DataIndex].Cells[3].Text;
                    LLokasi.Text = GridViewokbp.Rows[DataIndex].Cells[4].Text;
                    LPartno.Text = GridViewokbp.Rows[DataIndex].Cells[5].Text;
                    LPalet.Text = GridViewokbp.Rows[DataIndex].Cells[6].Text;
                    LTglJemur.Text = GridViewokbp.Rows[DataIndex].Cells[7].Text;
                    LRak.Text = GridViewokbp.Rows[DataIndex].Cells[8].Text;
                    LQtyIn.Text = GridViewokbp.Rows[DataIndex].Cells[9].Text;
                    LQtyOut.Text = GridViewokbp.Rows[DataIndex].Cells[10].Text;
                    txtSerah.Text = GridViewokbp.Rows[DataIndex].Cells[11].Text;
                    ArrayList arrPelarian = new ArrayList();
                    T1_Jemur lari = new T1_Jemur();
                    Session["arrpelarian"] = null;
                    lari.Partno = GridViewokbp.Rows[DataIndex].Cells[5].Text;
                    arrPelarian.Add(lari);
                    Session["arrpelarian"] = arrPelarian;
                    GridView1.DataSource = arrPelarian;
                    GridView1.DataBind();
                    if (LRak.Text.Trim().ToUpper() == "00")
                    {
                        ddlOven.Visible = true;
                        Loven.Visible = true;
                    }
                    else
                    {
                        ddlOven.Visible = false;
                        Loven.Visible = false;
                    }
                    int panjang = 0;
                    string strTebal = LPartno.Text.Substring(6, 3);
                    if (Session["arrjemur"] != null)
                    {
                        ArrayList arrcuring = new ArrayList();
                        arrcuring = (ArrayList)Session["arrjemur"];
                        if (arrcuring.Count > 0)
                        {
                            T1_Jemur jemur = (T1_Jemur)arrcuring[DataIndex];

                            string kode = string.Empty;
                            if (jemur.Partno != string.Empty)
                            {
                                //if (jemur.Partno.Substring(0, 3) == "CLB" || jemur.Partno.Substring(0, 3) == "INT" 
                                //    || jemur.Partno.Substring(0, 3) == "INP" || jemur.Partno.Substring(0, 3) == "EXT"
                                //    || jemur.Partno.Substring(0, 3) == "TBP" || jemur.Partno.Substring(0, 3) == "LOT"
                                //    || jemur.Partno.Substring(0, 3) == "SUB" || jemur.Partno.Substring(0, 3) == "GRD"
                                //    || jemur.Partno.Substring(0, 3) == "PNK" || jemur.Partno.Substring(0, 3) == "GRC")
                                //{
                                kode = jemur.Partno.Substring(0, 3);
                                //}
                                //else
                                //{
                                //    if (jemur.Partno.Substring(0, 1) == "I")
                                //    {
                                //        kode = "INP";
                                //    }
                                //    if (jemur.Partno.Substring(0, 1) == "G")
                                //    {
                                //        kode = "INT";
                                //    }
                                //    if (jemur.Partno.Substring(0, 1) == "E")
                                //    {
                                //        kode = "EXT";
                                //    }
                                //    if (jemur.Partno.Substring(0, 1) == "LOT")
                                //    {
                                //        kode = "LOT";
                                //    }
                                //}
                                if (kode.Trim() == string.Empty)
                                    kode = jemur.Partno.Substring(0, 3);
                                string A = jemur.Partno.Substring(4, 3);
                                string T = jemur.Partno.Substring(6, 3).PadLeft(3, '0');
                                string B = jemur.Partno.Substring(9, 4);
                                string C = jemur.Partno.Substring(13, 4);
                                panjang = Convert.ToInt32(C);
                                int tebal = Convert.ToInt32(T) / 10;
                                int lebar = Convert.ToInt32(B);
                                string kodeKW = string.Empty;
                                if (RBList.SelectedValue == string.Empty)
                                    kodeKW = kode;
                                else
                                    kodeKW = RBList.SelectedValue;
                                switch (jemur.Formula.Trim().Length)
                                {
                                    case 3:
                                        {
                                            if (RBWetCut.Checked == true)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString() + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            else
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                        }
                                    case 4:
                                        {

                                            if (lebar == 1230)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (lebar == 1200)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (Convert.ToInt32(C) > 2460)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (jemur.Partno.Substring(4, 1) == "1")
                                            {
                                                lebar = Convert.ToInt32(jemur.Partno.Substring(9, 4));
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');

                                                if (lebar == 1220 && Convert.ToInt32(C) == 2440)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    if (kode != "PNK")
                                                        txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    else
                                                        txtPartnoKW.Text = kode + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }

                                                if (lebar == 1230 && Convert.ToInt32(C) == 2440)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }
                                                if (lebar == 1230 && Convert.ToInt32(C) == 3600)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }
                                                if (lebar == 1000 && Convert.ToInt32(C) == 2000)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtlokBPF0.Text = "E99";
                                                    break;
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }

                                        }
                                    case 5:
                                        {
                                            if (lebar == 1230)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (lebar == 1200)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (Convert.ToInt32(C) > 2460)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (jemur.Partno.Substring(4, 1) == "1")
                                            {
                                                lebar = Convert.ToInt32(jemur.Partno.Substring(9, 4));
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoKW.Text = RBList.SelectedValue + "-MN-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');

                                                if (lebar == 1220 && Convert.ToInt32(C) == 2440)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    if (kode != "PNK")
                                                        txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    else
                                                        txtPartnoKW.Text = kode + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }

                                                if (lebar == 1230 && Convert.ToInt32(C) == 2440)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }
                                                if (lebar == 1230 && Convert.ToInt32(C) == 3600)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }
                                                if (lebar == 1000 && Convert.ToInt32(C) == 2000)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtlokBPF0.Text = "E99";
                                                    break;
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }

                                        }
                                    case 6:
                                        {

                                            if (lebar == 1230)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (lebar == 1200)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (Convert.ToInt32(C) > 2460)
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }
                                            if (jemur.Partno.Substring(4, 1) == "1")
                                            {
                                                lebar = Convert.ToInt32(jemur.Partno.Substring(9, 4));
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');

                                                if (lebar == 1220 && Convert.ToInt32(C) == 2440)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    if (kode != "PNK")
                                                        txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    else
                                                        txtPartnoKW.Text = kode + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }

                                                if (lebar == 1230 && Convert.ToInt32(C) == 2440)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }
                                                if (lebar == 1230 && Convert.ToInt32(C) == 3600)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";

                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 10).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    break;
                                                }
                                                if (lebar == 1000 && Convert.ToInt32(C) == 2000)
                                                {
                                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0') + "SE";
                                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                    txtlokBPF0.Text = "E99";
                                                    break;
                                                }
                                                break;
                                            }
                                            else
                                            {
                                                txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoKW.Text = kodeKW + "-M-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                                txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString().PadLeft(4, '0');
                                                break;
                                            }

                                        }
                                }
                                txtlokOK.Text = "H99";
                                txtlokKW.Text = "H99";
                                txtlokBPF.Text = "B99";
                                txtlokBPU.Text = "C99";
                                Users users = (Users)Session["Users"];
                                if (users.UnitKerjaID == 1)
                                {
                                    txtlokBPF0.Text = "FN02";
                                    txtlokBPU0.Text = "FN01";
                                }
                                else
                                {
                                    txtlokBPF0.Text = "D99";
                                    txtlokBPU0.Text = "D99";
                                }
                                txtQtyOK.Text = "0";
                                txtQtyKW.Text = "0";
                                txtQtyBPF.Text = "0";
                                txtQtyBPU.Text = "0";
                                txtQtyBPU0.Text = "0";
                                txtQtyBPS.Text = "0";
                                txtQtyOK1.Text = "0";
                                txtQtyKW1.Text = "0";
                                txtQtyBPF0.Text = "0";
                                if (lebar == 1230 && Convert.ToInt32(C) == 2440)
                                {
                                    RBKali16.Checked = true;
                                    RBKali17.Checked = false;
                                    RBKali18.Checked = false;
                                    RBKali4.Checked = false;
                                    RBKali6.Checked = false;
                                    RBKali12.Checked = false; RBKali20.Checked = false;
                                }
                                if (lebar == 1230 && Convert.ToInt32(C) == 3000)
                                {
                                    RBKali16.Checked = false;
                                    RBKali17.Checked = false;
                                    RBKali18.Checked = false;
                                    RBKali4.Checked = false;
                                    RBKali6.Checked = false;
                                    RBKali12.Checked = false;
                                    RBKali20.Checked = true;
                                }
                                if (lebar == 1244 && Convert.ToInt32(C) == 3600)
                                {
                                    RBKali16.Checked = false;
                                    RBKali17.Checked = true;
                                    RBKali18.Checked = false;
                                    RBKali4.Checked = false;
                                    RBKali6.Checked = false;
                                    RBKali12.Checked = false;
                                    RBKali20.Checked = false;
                                }
                                //if (lebar == 1240 && Convert.ToInt32(C) == 2460)
                                //{
                                //    RBKali16.Checked = false;
                                //    RBKali17.Checked = false;
                                //    RBKali18.Checked = true ;
                                //    RBKali4.Checked = false;
                                //    RBKali6.Checked = false;
                                //    RBKali12.Checked = false;
                                //}
                                if (RBKali16.Checked == true && RBLisflank.Checked == true)
                                {
                                    if (lebar == 1230 && Convert.ToInt32(C) == 2440)
                                    {
                                        txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali16.Text.Substring(0, 4) + "2440";
                                        txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali16.Text.Substring(0, 4) + "2440SE";
                                        txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali16.Text.Substring(0, 4) + "2440";
                                        txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali16.Text.Substring(0, 4) + "2440SE";
                                        txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali16.Text.Substring(0, 4) + "2440SE";
                                        if (users.UnitKerjaID == 1)
                                        {
                                            txtlokKW1.Text = "AA13";
                                            txtlokBPF0.Text = "AA13";
                                            txtlokBPU0.Text = "AA13";
                                        }
                                        else
                                        {
                                            txtlokKW1.Text = "E18";
                                            txtlokBPF0.Text = "E18";
                                            txtlokBPU0.Text = "E18";
                                        }
                                        txtlokOK.Text = "H99";
                                        txtlokKW.Text = "I99";
                                        txtlokBPF.Text = "B99";
                                        txtlokBPU.Text = "C99";
                                    }
                                    else
                                    {
                                        RBKali16.Checked = false;
                                        RBKali4.Checked = true;
                                    }
                                }
                                if (RBKali20.Checked == true && RBLisflank.Checked == true)
                                {
                                    if (lebar == 1230 && Convert.ToInt32(C) == 3000)
                                    {
                                        txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali20.Text.Substring(0, 4) + "3000";
                                        txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali20.Text.Substring(0, 4) + "3000SE";
                                        txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali20.Text.Substring(0, 4) + "3000";
                                        txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali20.Text.Substring(0, 4) + "3000SE";
                                        txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali20.Text.Substring(0, 4) + "3000SE";
                                        if (users.UnitKerjaID == 1)
                                        {
                                            txtlokKW1.Text = "AA13";
                                            txtlokBPF0.Text = "AA13";
                                            txtlokBPU0.Text = "AA13";
                                        }
                                        else
                                        {
                                            txtlokKW1.Text = "E18";
                                            txtlokBPF0.Text = "E18";
                                            txtlokBPU0.Text = "E18";
                                        }
                                        txtlokOK.Text = "H99";
                                        txtlokKW.Text = "I99";
                                        txtlokBPF.Text = "B99";
                                        txtlokBPU.Text = "C99";
                                    }
                                    else
                                    {
                                        RBKali20.Checked = false;
                                        RBKali4.Checked = true;
                                    }
                                }
                                if (RBKali21.Checked == true && RBLisflank.Checked == true)
                                {
                                    if (lebar == 1230 && Convert.ToInt32(C) == 3600)
                                    {
                                        txtPartnoOK.Text = kode + "-1-" + strTebal + RBKali20.Text.Substring(0, 4) + "3600";
                                        txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali20.Text.Substring(0, 4) + "3600";
                                        txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali20.Text.Substring(0, 4) + "3600SE";
                                        txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali20.Text.Substring(0, 4) + "3600";
                                        txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali20.Text.Substring(0, 4) + "3600SE";
                                        txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali20.Text.Substring(0, 4) + "3600SE";
                                        if (users.UnitKerjaID == 1)
                                        {
                                            txtlokKW1.Text = "AA13";
                                            txtlokBPF0.Text = "AA13";
                                            txtlokBPU0.Text = "AA13";
                                        }
                                        else
                                        {
                                            txtlokKW1.Text = "E18";
                                            txtlokBPF0.Text = "E18";
                                            txtlokBPU0.Text = "E18";
                                        }
                                        txtlokOK.Text = "H99";
                                        txtlokKW.Text = "I99";
                                        txtlokBPF.Text = "B99";
                                        txtlokBPU.Text = "C99";
                                    }
                                    else
                                    {
                                        RBKali20.Checked = false;
                                        RBKali4.Checked = true;
                                    }
                                }
                                if (RBKali17.Checked == true && RBLisflank.Checked == true)
                                {
                                    if (lebar == 1244 && Convert.ToInt32(C) == 3600)
                                    {
                                        txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali17.Text.Substring(0, 4) + "3600";
                                        txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali17.Text.Substring(0, 4) + "3600SE";
                                        txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali17.Text.Substring(0, 4) + "3600";
                                        txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali17.Text.Substring(0, 4) + "3600SE";
                                        txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali17.Text.Substring(0, 4) + "3600SE";
                                        if (users.UnitKerjaID == 1)
                                        {
                                            txtlokKW1.Text = "AA13";
                                            txtlokBPF0.Text = "AA13";
                                            txtlokBPU0.Text = "AA13";
                                        }
                                        else
                                        {
                                            txtlokKW1.Text = "E18";
                                            txtlokBPF0.Text = "E18";
                                            txtlokBPU0.Text = "E18";
                                        }
                                        txtlokOK.Text = "H99";
                                        txtlokKW.Text = "I99";
                                        txtlokBPF.Text = "B99";
                                        txtlokBPU.Text = "C99";
                                    }
                                    else
                                    {
                                        RBKali17.Checked = false;
                                        RBKali4.Checked = true;
                                    }
                                }
                                if (RBKali18.Checked == true && RBLisflank.Checked == true)
                                {
                                    if (lebar == 1240 && Convert.ToInt32(C) == 2460)
                                    {
                                        txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali18.Text.Substring(0, 4) + "2460";
                                        txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali18.Text.Substring(0, 4) + "2460SE";
                                        txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali18.Text.Substring(0, 4) + "2460";
                                        txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali18.Text.Substring(0, 4) + "2460SE";
                                        txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali18.Text.Substring(0, 4) + "2460SE";
                                        if (users.UnitKerjaID == 1)
                                        {
                                            txtlokKW1.Text = "AA13";
                                            txtlokBPF0.Text = "AA13";
                                            txtlokBPU0.Text = "AA13";
                                        }
                                        else
                                        {
                                            txtlokKW1.Text = "E18";
                                            txtlokBPF0.Text = "E18";
                                            txtlokBPU0.Text = "E18";
                                        }
                                        txtlokOK.Text = "H99";
                                        txtlokKW.Text = "I99";
                                        txtlokBPF.Text = "B99";
                                        txtlokBPU.Text = "C99";
                                    }
                                    else
                                    {
                                        RBKali18.Checked = false;
                                        RBKali4.Checked = true;
                                    }
                                }
                                if (RBKali2.Checked == true && RB1000.Checked == true)
                                {
                                    txtPartnoOK.Text = kode + "-3-" + "04010202020SE";
                                    txtPartnoOK1.Text = kode + "-3-" + "04010001000SE";
                                    txtlokOK1.Text = "D99";
                                    txtPartnoKW.Text = RBList.SelectedValue + "-M-" + "04010202020SE";
                                    txtPartnoKW1.Text = RBList.SelectedValue + "-M-" + "04010001000SE";
                                    txtPartnoBPF.Text = kode + "-P-" + "04010202020SE";
                                    txtPartnoBPF0.Text = kode + "-P-" + "04010001000SE";
                                    txtPartnoBPU0.Text = kode + "-P-" + "04010001000SE";
                                    txtlokOK1.Text = "D99";
                                    txtlokKW1.Text = "D99";
                                    txtlokBPF0.Text = "D99";
                                    txtlokBPU0.Text = "D99";
                                    txtlokOK.Text = "I99";
                                    txtlokKW.Text = "I99";
                                    txtlokBPF.Text = "B99";
                                    txtlokBPU.Text = "C99";

                                }
                                //RBKali4.Text = "1213 X 2440";
                                //RBKali6.Text = "1221 X 2440";
                                //RBKali12.Text = "1233 X 2440";
                                if (RBKali4.Checked == true && RBLisflank.Checked == true)
                                {
                                    txtPartnoKW.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                    txtPartnoKW1.Text = kode + "-1-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString() + "SE";
                                    txtPartnoBPF0.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString() + "SE";
                                    txtPartnoBPU0.Text = kode + "-P-" + jemur.Partno.Substring(6, 3).PadLeft(3, '0') + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString().PadLeft(4, '0') + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString() + "SE";
                                    if (users.UnitKerjaID == 1)
                                    {
                                        txtlokKW1.Text = "AA13";
                                        txtlokBPF0.Text = "AA13";
                                        txtlokBPU0.Text = "AA13";
                                    }
                                    else
                                    {
                                        txtlokKW1.Text = "E16";
                                        txtlokBPF0.Text = "E16";
                                        txtlokBPU0.Text = "E16";
                                    }
                                    txtlokOK.Text = "H99";
                                    txtlokKW.Text = "I99";
                                    txtlokBPF.Text = "B99";
                                    txtlokBPU.Text = "C99";
                                    txtPartnoOK.Text = LPartno.Text;
                                }
                                if (RBKali6.Checked == true && RBLisflank.Checked == true)
                                {
                                    txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440";
                                    txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440";
                                    txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440SE";
                                    txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440SE";
                                    txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440SE";
                                    if (users.UnitKerjaID == 1)
                                    {
                                        txtlokKW1.Text = "AA13";
                                        txtlokBPF0.Text = "AA13";
                                        txtlokBPU0.Text = "AA13";
                                    }
                                    else
                                    {
                                        txtlokKW1.Text = "E17";
                                        txtlokBPF0.Text = "E17";
                                        txtlokBPU0.Text = "E17";
                                    }
                                    txtlokOK.Text = "H99";
                                    txtlokKW.Text = "I99";
                                    txtlokBPF.Text = "B99";
                                    txtlokBPU.Text = "C99";
                                }
                                if (RBKali12.Checked == true && RBLisflank.Checked == true)
                                {
                                    txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440";
                                    txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440SE";
                                    txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440";
                                    txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440SE";
                                    txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440SE";
                                    if (users.UnitKerjaID == 1)
                                    {
                                        txtlokKW1.Text = "AA13";
                                        txtlokBPF0.Text = "AA13";
                                        txtlokBPU0.Text = "AA13";
                                    }
                                    else
                                    {
                                        txtlokKW1.Text = "E18";
                                        txtlokBPF0.Text = "E18";
                                        txtlokBPU0.Text = "E18";
                                    }
                                    txtlokOK.Text = "H99";
                                    txtlokKW.Text = "I99";
                                    txtlokBPF.Text = "B99";
                                    txtlokBPU.Text = "C99";
                                }
                                if (RBKali19.Checked == true && RBLisflank.Checked == true)
                                {
                                    txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali19.Text.Substring(0, 4) + "2440";
                                    txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali19.Text.Substring(0, 4) + "2440SE";
                                    txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali19.Text.Substring(0, 4) + "2440";
                                    txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali19.Text.Substring(0, 4) + "2440SE";
                                    txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali19.Text.Substring(0, 4) + "2440SE";
                                    if (users.UnitKerjaID == 1)
                                    {
                                        txtlokKW1.Text = "AA13";
                                        txtlokBPF0.Text = "AA13";
                                        txtlokBPU0.Text = "AA13";
                                    }
                                    else
                                    {
                                        txtlokKW1.Text = "E18";
                                        txtlokBPF0.Text = "E18";
                                        txtlokBPU0.Text = "E18";
                                    }
                                    txtlokOK.Text = "H99";
                                    txtlokKW.Text = "I99";
                                    txtlokBPF.Text = "B99";
                                    txtlokBPU.Text = "C99";
                                }

                                if (RB4Mili.Checked == true)
                                {
                                    txtPartnoOK1.Text = txtPartnoOK.Text;
                                    txtPartnoKW1.Text = txtPartnoKW.Text;
                                    txtPartnoBPF0.Text = txtPartnoBPF.Text;
                                    txtPartnoBPU0.Text = txtPartnoBPU.Text;
                                    //txtlokOK1.Text = "FN02";
                                    //txtlokKW1.Text = "E18";
                                    //if (panjang > 2460 && tebal < 10)
                                    //{
                                    //    txtlokOK.Text = "I99";
                                    //    //txtPartnoOK.Text = LPartno.Text;
                                    //    //txtPartnoOK1.Text = txtPartnoOK.Text;
                                    //    txtPartnoOK.Text = txtPartnoBPF.Text;
                                    //    txtPartnoOK1.Text = txtPartnoBPF.Text;
                                    //}
                                    //else
                                    //    txtlokOK.Text = "H99";
                                    txtlokKW.Text = "H99";
                                    txtlokBPF.Text = "B99";
                                    txtlokBPU.Text = "C99";
                                    if (users.UnitKerjaID != 1)
                                    {
                                        txtlokBPF0.Text = "A99";
                                        txtlokBPU0.Text = "E14";
                                        if (lebar == 1000 && Convert.ToInt32(C) == 2000)
                                            txtlokBPF0.Text = "E99";
                                    }
                                    txtQtyOK.Text = "0";
                                    txtQtyKW.Text = "0";
                                    txtQtyBPF.Text = "0";
                                    txtQtyBPU.Text = "0";
                                    txtQtyBPU0.Text = "0";
                                    txtQtyBPS.Text = "0";
                                    txtQtyOK1.Text = "0";
                                    txtQtyKW1.Text = "0";
                                    txtQtyBPF0.Text = "0";
                                }
                                if (RBLisflank.Checked == true)
                                {
                                    IsiPartnoLisplankLvl2(kode);
                                    txtQtyOK.Text = "0";
                                    txtQtyKW.Text = txtSerah.Text;
                                    txtQtyBPF.Text = txtQtyPBP.Text;
                                }
                                if (RBSuperPanel.Checked == true)
                                {
                                    IsiPartnoLisplankLvl2(kode);
                                    txtPartnoBPF0.Text = txtPartnoBPF.Text;
                                    txtQtyOK.Text = txtQtyPOK.Text;
                                    txtQtyBPF.Text = txtQtyPBP.Text;
                                }
                                if (RB1000.Checked == true)
                                    txtPartnoOK1.Focus();
                                else
                                    txtPartnoOK.Focus();
                                btnTansfer.Disabled = false;
                            }
                            else
                                clearform();
                        }
                        //txtQtyOK.Text = txtSerah.Text;
                        txtQtyPOK.Text = txtSerah.Text;
                        txtQtyAsal.Text = txtSerah.Text;
                        if (RB9Mili.Checked == true && RBLisflank.Checked == true && RBLisPlank2.Checked == true)
                        {
                            if (RBKali13.Checked == true)
                            {
                                //LoadDataGridTerima();
                                //ClearPotong2();
                                try
                                {
                                    string kode = string.Empty;
                                    if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                                        kode = LPartno.Text.Substring(0, 3);
                                    else
                                        kode = "INT";
                                    IsiPartnoLisplankLvl2(kode);
                                    RBKali4.Checked = false;
                                    RBKali6.Checked = false;
                                    RBKali12.Checked = true;
                                    txtnopalet.Text = string.Empty;

                                    //LoadDataGridTerima();
                                    IsiPartno1(DataIndex);
                                }
                                catch { }
                            }
                            if (RBKali14.Checked == true)
                            {
                                //LoadDataGridTerima();
                                //ClearPotong2();
                                string kode = string.Empty;
                                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                                    kode = LPartno.Text.Substring(0, 3);
                                else
                                    kode = "INT";
                                IsiPartnoLisplankLvl2(kode);
                                RBKali4.Checked = false;
                                RBKali6.Checked = true;
                                RBKali12.Checked = false;
                                txtnopalet.Text = string.Empty;

                                //LoadDataGridTerima();
                                IsiPartno1(DataIndex);
                            }
                            if (RBKali15.Checked == true)
                            {
                                // LoadDataGridTerima();
                                //ClearPotong2();
                                string kode = string.Empty;
                                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                                    kode = LPartno.Text.Substring(0, 3);
                                else
                                    kode = "INT";
                                IsiPartnoLisplankLvl2(kode);
                                RBKali4.Checked = true;
                                RBKali6.Checked = false;
                                RBKali12.Checked = false;
                                txtnopalet.Text = string.Empty;

                                //LoadDataGridTerima();
                                IsiPartno1(DataIndex);
                            }
                        }
                    }
                    AutoBST1ALL();
                    ArrayList arrbsauto = new ArrayList();
                    arrbsauto = (ArrayList)Session["arrbsauto"];
                    GridBSAuto.DataSource = arrbsauto;
                    GridBSAuto.DataBind();
                }
                catch { }
            }
        }
        protected void IsiPartno1(int DataIndex)
        {
            clearform();

            //if (txtnopalet.Text != string.Empty)
            //{
            ////try
            ////{
            if (GridViewokbp.Rows.Count <= DataIndex)
                DataIndex = 0;
            if (GridViewokbp.Rows[DataIndex].Cells[0].Text == "0")
            {
                return;
            }
            LID.Text = GridViewokbp.Rows[DataIndex].Cells[0].Text;
            LDestID.Text = GridViewokbp.Rows[DataIndex].Cells[1].Text;
            LTglProd.Text = GridViewokbp.Rows[DataIndex].Cells[2].Text;
            LJenis.Text = GridViewokbp.Rows[DataIndex].Cells[3].Text;
            LLokasi.Text = GridViewokbp.Rows[DataIndex].Cells[4].Text;
            LPartno.Text = GridViewokbp.Rows[DataIndex].Cells[5].Text;
            LPalet.Text = GridViewokbp.Rows[DataIndex].Cells[6].Text;
            LTglJemur.Text = GridViewokbp.Rows[DataIndex].Cells[7].Text;
            LRak.Text = GridViewokbp.Rows[DataIndex].Cells[8].Text;
            LQtyIn.Text = GridViewokbp.Rows[DataIndex].Cells[9].Text;
            LQtyOut.Text = GridViewokbp.Rows[DataIndex].Cells[10].Text;
            txtSerah.Text = GridViewokbp.Rows[DataIndex].Cells[11].Text;
            ArrayList arrPelarian = new ArrayList();
            T1_Jemur lari = new T1_Jemur();
            Session["arrpelarian"] = null;
            lari.Partno = GridViewokbp.Rows[DataIndex].Cells[5].Text;
            arrPelarian.Add(lari);
            Session["arrpelarian"] = arrPelarian;
            GridView1.DataSource = arrPelarian;
            GridView1.DataBind();
            string strTebal = LPartno.Text.Substring(6, 3);
            if (Session["arrjemur"] != null)
            {
                ArrayList arrcuring = new ArrayList();
                arrcuring = (ArrayList)Session["arrjemur"];
                if (arrcuring.Count > 0)
                {
                    T1_Jemur jemur = (T1_Jemur)arrcuring[DataIndex];

                    string kode = string.Empty;
                    if (jemur.Partno != string.Empty)
                    {
                        if (jemur.Partno.Substring(0, 3) == "CLB" || jemur.Partno.Substring(0, 3) == "INT"
                        || jemur.Partno.Substring(0, 3) == "INP" || jemur.Partno.Substring(0, 3) == "EXT"
                        || jemur.Partno.Substring(0, 3) == "TBP" || jemur.Partno.Substring(0, 3) == "LOT"
                        || jemur.Partno.Substring(0, 3) == "SUB" || jemur.Partno.Substring(0, 3) == "GRD"
                        || jemur.Partno.Substring(0, 3) == "PNK" || jemur.Partno.Substring(0, 3) == "GRC")
                        {
                            kode = jemur.Partno.Substring(0, 3);
                        }
                        else
                        {
                            if (jemur.Partno.Substring(0, 1) == "I")
                            {
                                kode = "INP";
                            }
                            if (jemur.Partno.Substring(0, 1) == "G")
                            {
                                kode = "INT";
                            }
                            if (jemur.Partno.Substring(0, 1) == "E")
                            {
                                kode = "EXT";
                            }
                        }
                        switch (jemur.Formula.Trim().Length)
                        {
                            case 3:
                                {
                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                    txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                    txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                    break;
                                }
                            case 4:
                                {
                                    string A = jemur.Partno.Substring(4, 3);
                                    string B = jemur.Partno.Substring(8, 4);
                                    string C = jemur.Partno.Substring(13, 4);

                                    if (jemur.Partno.Substring(4, 1) == "1")
                                    {
                                        txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                        txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                        txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                        txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                        txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(9, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                        break;
                                    }
                                    else
                                    {
                                        txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                        txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                        txtPartnoBPF.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4)) - 20).ToString() + "SE";
                                        txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                        txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(4, 3) + (Convert.ToInt32(jemur.Partno.Substring(8, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(13, 4))).ToString();
                                        break;
                                    }
                                }
                            case 5:
                                {
                                    txtPartnoOK.Text = kode + "-3-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(10, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(15, 4)) - 20).ToString() + "SE";
                                    txtPartnoKW.Text = RBList.SelectedValue + "-M-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(10, 4)) - 20).ToString() + (Convert.ToInt32(jemur.Partno.Substring(15, 4)) - 20).ToString() + "SE";

                                    txtPartnoBPU.Text = kode + "-P-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(10, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(15, 4))).ToString();
                                    txtPartnoBPS.Text = kode + "-1-" + jemur.Partno.Substring(6, 3) + (Convert.ToInt32(jemur.Partno.Substring(10, 4))).ToString() + (Convert.ToInt32(jemur.Partno.Substring(15, 4))).ToString();
                                    break;
                                }
                        }
                        txtlokOK.Text = "H99";
                        txtlokKW.Text = "I99";
                        txtlokBPF.Text = "B99";
                        txtlokBPU.Text = "C99";
                        Users users = (Users)Session["Users"];
                        if (users.UnitKerjaID == 1)
                        {
                            txtlokBPF0.Text = "FN02";
                            txtlokBPU0.Text = "FN01";
                        }
                        else
                        {
                            txtlokBPF0.Text = "D99";
                            txtlokBPU0.Text = "D99";
                        }
                        txtQtyOK.Text = "0";
                        txtQtyKW.Text = "0";
                        txtQtyBPF.Text = "0";
                        txtQtyBPU.Text = "0";
                        txtQtyBPU0.Text = "0";
                        txtQtyBPS.Text = "0";
                        txtQtyOK1.Text = "0";
                        txtQtyKW1.Text = "0";
                        txtQtyBPF0.Text = "0";

                        if (RBKali2.Checked == true && RB1000.Checked == true)
                        {
                            txtPartnoOK.Text = kode + "-3-" + "04010202020SE";
                            txtPartnoOK1.Text = kode + "-3-" + "04010001000SE";
                            txtlokOK1.Text = "D99";
                            txtPartnoKW.Text = RBList.SelectedValue + "-M-" + "04010202020SE";
                            txtPartnoKW1.Text = RBList.SelectedValue + "-M-" + "04010001000SE";
                            txtPartnoBPF.Text = kode + "-P-" + "04010202020SE";
                            txtPartnoBPF0.Text = kode + "-P-" + "04010001000SE";
                            txtPartnoBPU0.Text = kode + "-P-" + "04010001000SE";
                            txtlokOK1.Text = "D99";
                            txtlokKW1.Text = "D99";
                            txtlokBPF0.Text = "D99";
                            txtlokBPU0.Text = "D99";
                            txtlokOK.Text = "I99";
                            txtlokKW.Text = "I99";
                            txtlokBPF.Text = "B99";
                            txtlokBPU.Text = "C99";

                        }
                        //RBKali4.Text = "1213 X 2440";
                        //RBKali6.Text = "1221 X 2440";
                        //RBKali12.Text = "1233 X 2440";
                        if (RBKali4.Checked == true && RBLisflank.Checked == true)
                        {
                            txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali4.Text.Substring(0, 4) + "2440";
                            txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali4.Text.Substring(0, 4) + "2440";
                            txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali4.Text.Substring(0, 4) + "2440SE";
                            txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali4.Text.Substring(0, 4) + "2440SE";
                            txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali4.Text.Substring(0, 4) + "2440SE";
                            if (users.UnitKerjaID == 1)
                            {
                                txtlokKW1.Text = "AA13";
                                txtlokBPF0.Text = "AA13";
                                txtlokBPU0.Text = "AA13";
                            }
                            else
                            {
                                txtlokKW1.Text = "E16";
                                txtlokBPF0.Text = "E16";
                                txtlokBPU0.Text = "E16";
                            }
                            txtlokOK.Text = "H99";
                            txtlokKW.Text = "I99";
                            txtlokBPF.Text = "B99";
                            txtlokBPU.Text = "C99";
                        }
                        if (RBKali6.Checked == true && RBLisflank.Checked == true)
                        {
                            txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440";
                            txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440";
                            txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440SE";
                            txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440SE";
                            txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali6.Text.Substring(0, 4) + "2440SE";
                            if (users.UnitKerjaID == 1)
                            {
                                txtlokKW1.Text = "AA13";
                                txtlokBPF0.Text = "AA13";
                                txtlokBPU0.Text = "AA13";
                            }
                            else
                            {
                                txtlokKW1.Text = "E17";
                                txtlokBPF0.Text = "E17";
                                txtlokBPU0.Text = "E17";
                            }
                            txtlokOK.Text = "H99";
                            txtlokKW.Text = "I99";
                            txtlokBPF.Text = "B99";
                            txtlokBPU.Text = "C99";
                        }
                        if (RBKali12.Checked == true && RBLisflank.Checked == true)
                        {
                            txtPartnoKW.Text = kode + "-1-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440";
                            txtPartnoBPF.Text = kode + "-P-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440SE";
                            txtPartnoKW1.Text = kode + "-1-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440";
                            txtPartnoBPF0.Text = kode + "-P-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440SE";
                            txtPartnoBPU0.Text = kode + "-P-" + strTebal + RBKali12.Text.Substring(0, 4) + "2440SE";
                            if (users.UnitKerjaID == 1)
                            {
                                txtlokKW1.Text = "AA13";
                                txtlokBPF0.Text = "AA13";
                                txtlokBPU0.Text = "AA13";
                            }
                            else
                            {
                                txtlokKW1.Text = "E18";
                                txtlokBPF0.Text = "E18";
                                txtlokBPU0.Text = "E18";
                            }
                            txtlokOK.Text = "H99";
                            txtlokKW.Text = "I99";
                            txtlokBPF.Text = "B99";
                            txtlokBPU.Text = "C99";
                        }
                        if (RB4Mili.Checked == true)
                        {
                            txtPartnoOK1.Text = txtPartnoOK.Text;
                            txtPartnoKW1.Text = txtPartnoKW.Text;
                            txtPartnoBPF0.Text = txtPartnoBPF.Text;
                            txtPartnoBPU0.Text = txtPartnoBPU.Text;
                            //txtlokOK1.Text = "FN02";
                            //txtlokKW1.Text = "E18";

                            txtlokOK.Text = "H99";
                            txtlokKW.Text = "I99";
                            txtlokBPF.Text = "B99";
                            txtlokBPU.Text = "C99";
                        }
                        if (RBLisflank.Checked == true)
                        {
                            IsiPartnoLisplankLvl2(kode);
                            txtQtyKW.Text = txtSerah.Text;
                            txtQtyBPF.Text = txtQtyPBP.Text;
                        }
                        if (RBSuperPanel.Checked == true)
                        {
                            IsiPartnoLisplankLvl2(kode);
                            txtPartnoBPF0.Text = txtPartnoBPF.Text;
                            txtQtyOK.Text = txtQtyPOK.Text;
                            txtQtyBPF.Text = txtQtyPBP.Text;
                        }

                        if (RB1000.Checked == true)
                            txtPartnoOK1.Focus();
                        else
                            txtPartnoOK.Focus();
                        btnTansfer.Disabled = false;

                    }
                    else
                        clearform();
                }
            }


            //}
            //catch { }
            //}
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void GridViewSerah_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSerah.PageIndex = e.NewPageIndex;
            LoadDataGridSerah(Convert.ToDateTime(txtDateSerah.Text), "serah");
            ArrayList arrjemur = (ArrayList)Session["arrSerah"];
            if (arrjemur.Count == 0)
                DisplayAJAXMessage(this, "Data jemur belum tersedia");
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataJemur();
        }
        protected void txtLokasiBP_TextChanged(object sender, EventArgs e)
        {
            string text = string.Empty;
            for (int i = 0; i <= GridViewokbp.Rows.Count - 1; i++)
            {
                TextBox txtLokasiBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtLokasiBP");
                TextBox txtQtyBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtQtyBP");
                DropDownList ddlLokasi = (DropDownList)GridViewokbp.Rows[i].FindControl("txtLokasiBP");
                if (txtLokasiBP.Text != string.Empty && txtQtyBP.Text == string.Empty)
                    text = txtLokasiBP.Text;
                break;
            }
            for (int i = 0; i <= GridViewokbp.Rows.Count - 1; i++)
            {
                TextBox txtLokasiBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtLokasiBP");
                TextBox txtQtyBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtQtyBP");
                if (txtLokasiBP.Text == string.Empty)
                {
                    txtLokasiBP.Text = text;
                }
            }
            for (int i = 0; i <= GridViewokbp.Rows.Count - 1; i++)
            {
                TextBox txtLokasiBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtLokasiBP");
                TextBox txtQtyBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtQtyBP");
                if (txtLokasiBP.Text != string.Empty)
                {
                    if (txtQtyBP.Text == string.Empty)
                    {
                        txtQtyBP.Focus();
                        break;
                    }
                }
            }
        }
        protected void txtQtyBP_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i <= GridViewokbp.Rows.Count - 1; i++)
            {
                TextBox txtQtyOK = (TextBox)GridViewokbp.Rows[i].FindControl("txtQtyOK");
                TextBox txtLokasiBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtLokasiBP");
                TextBox txtQtyBP = (TextBox)GridViewokbp.Rows[i].FindControl("txtQtyBP");
                if (i == GridViewokbp.Rows.Count - 1)
                    btnTansfer.Focus();

                if (txtLokasiBP.Text != string.Empty)
                {
                    if (txtQtyOK.Text == string.Empty)
                    {
                        txtQtyOK.Focus();
                        break;
                    }
                    if (txtQtyBP.Text == string.Empty)
                    {
                        txtQtyBP.Focus();
                        break;
                    }
                }
            }
        }
        protected void LoadPelarian()
        {
            ArrayList arrcuring = new ArrayList();
            T1_JemurFacade Jemur = new T1_JemurFacade();
            arrcuring = Jemur.RetrieveforJmrLg(Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd"), "", "A.ID");
            GridView1.DataSource = arrcuring;
            GridView1.DataBind();
        }
        protected void GridViewokbp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "pilih")
            {
                txtnopalet.Text = GridViewokbp.Rows[rowindex].Cells[6].Text;
                LID.Text = GridViewokbp.Rows[rowindex].Cells[0].Text;
                LDestID.Text = GridViewokbp.Rows[rowindex].Cells[1].Text;
                LTglProd.Text = GridViewokbp.Rows[rowindex].Cells[2].Text;
                LJenis.Text = GridViewokbp.Rows[rowindex].Cells[3].Text;
                LLokasi.Text = GridViewokbp.Rows[rowindex].Cells[4].Text;
                LPartno.Text = GridViewokbp.Rows[rowindex].Cells[5].Text;
                LPalet.Text = GridViewokbp.Rows[rowindex].Cells[6].Text;
                LTglJemur.Text = GridViewokbp.Rows[rowindex].Cells[7].Text;
                LRak.Text = GridViewokbp.Rows[rowindex].Cells[8].Text;
                LQtyIn.Text = GridViewokbp.Rows[rowindex].Cells[9].Text;
                LQtyOut.Text = GridViewokbp.Rows[rowindex].Cells[10].Text;
                txtSerah.Text = GridViewokbp.Rows[rowindex].Cells[11].Text;
                txtnopalet.Text = GridViewokbp.Rows[rowindex].Cells[6].Text;
                IsiPartno(rowindex);
                ChkTglProduksi0.Checked = false;
                txtDate.Text = Convert.ToDateTime(LTglProd.Text).ToString("dd-MM-yyyy");
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox txtLari = (TextBox)GridView1.Rows[0].FindControl("txtPartNo");
            txtLari.Text = txtLari.Text.Trim();
            GridView1.Rows[0].FindControl("txtQtyJU").Focus();
        }
        protected void txtPartNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //int rowindex = Convert.ToInt32(GridView1.GridViewCommandEventArgs e.CommandArgument.ToString());
                GridViewRow row = (GridViewRow)((Control)sender).NamingContainer;
                int index = row.RowIndex;
                GridView1.Rows[index].FindControl("txtQtyJU").Focus();
            }
            catch { }
        }
        protected void txtQtyJU_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
                int i = 0;
                int totlari = 0;
                foreach (T1_Jemur pelarian in arrPelarian)

                {
                    TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                    totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                    i++;
                }
                if (txtQtyBPF.Text == string.Empty)
                    txtQtyBPF.Text = "0";
                if (RBSuperPanel.Checked == true)
                    txtQtyOK.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                else
                    txtQtyKW.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyOK.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                txtQtyPOK.Text = txtQtyOK.Text;
                btnTansfer.Focus();
                if (RBSuperPanel.Checked == true)
                {
                    int kali = pengali();
                    if (txtQtyAsal.Text == string.Empty)
                        txtQtyAsal.Text = "0";
                    if (txtQtyPOK.Text == string.Empty)
                        txtQtyPOK.Text = "0";
                    if (txtQtyPBP.Text == string.Empty)
                        txtQtyPBP.Text = "0";
                    if (txtQtyPBP.Text != string.Empty && txtQtyPOK.Text != string.Empty)
                    {
                        txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyKW.Text) - Convert.ToUInt32(txtQtyBPF.Text)
                            - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                        txtQtyOK.Text = txtQtyPOK.Text;
                        //txtQtyBPF.Text = txtQtyPBP.Text;
                    }
                }
                if (RBLisflank.Checked == true)
                {
                    int kali = pengali();

                    //txtSerah.Text = txtQtyAsal.Text;
                    if (RBLisPlank2.Checked == true)
                    {
                        txtQtyPOK.Text = (((Convert.ToInt32(txtSerah.Text) - totlari) * kali) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                        txtQtyKW.Text = (Convert.ToInt32(txtSerah.Text) - totlari).ToString();
                        txtQtyBPF.Text = "0";
                    }
                    else
                    {
                        txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                        txtQtyKW.Text = txtQtyPOK.Text;
                        txtQtyBPF.Text = txtQtyPBP.Text;
                    }
                    btnTansfer2.Focus();
                }
                AutoBST1ALL();
                ArrayList arrbsauto = new ArrayList();
                arrbsauto = (ArrayList)Session["arrbsauto"];
                GridBSAuto.DataSource = arrbsauto;
                GridBSAuto.DataBind();
            }
            catch { }
        }
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void GridViewokbp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int value = int.Parse(e.Row.Cells[13].Text);
                if (value >= 1)
                {
                    e.Row.Cells[0].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[1].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[2].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[3].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[4].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[5].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[6].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[7].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[8].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[9].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[10].BackColor = Color.FromName("#FF3300");
                    e.Row.Cells[11].BackColor = Color.FromName("#FF3300");
                }
            }
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            int checktrans = 0;
            if (LQtyIn.Text.Trim() != string.Empty && txtQtyOK.Text.Trim() != string.Empty && txtQtyBPF.Text.Trim() != string.Empty
                && txtQtyBPU.Text.Trim() != string.Empty && txtQtyBPS.Text.Trim() != string.Empty)
            {
                checktrans = Convert.ToInt32(LQtyIn.Text) - (Convert.ToInt32(txtQtyOK.Text) + Convert.ToInt32(txtQtyBPF.Text) +
                Convert.ToInt32(txtQtyBPU.Text) + Convert.ToInt32(txtQtyBPS.Text));
            }
            if (checktrans == 0)
                btnTansfer.Focus();

        }
        protected void RB1000_CheckedChanged(object sender, EventArgs e)
        {
            if (RB1000.Checked == true)
            {
                PanelTahapIII0.Visible = true;
                PanelTahapIII1.Visible = true;
                RBCut1.Visible = false;
                RBCut2.Visible = false;
                RBKali2.Visible = true;
                RBKali2.Checked = true;
                RBKali4.Checked = false;
                RBKali6.Checked = false;
                RBKali12.Checked = false;
                RBKali4.Visible = false;
                RBKali19.Visible = false;
                RBKali6.Visible = false;
                RBKali12.Visible = false;
                RBSuperPanel.Visible = false;
                RBLisflank.Visible = false;
                Panel1.Visible = true;
                txtlokOK.Text = "H99";
                txtlokKW.Text = "I99";
                txtPartnoOK1.Visible = true;
                txtPartnoKW1.Visible = true;
                txtPartnoBPF0.Visible = true;
                txtPartnoBPU0.Visible = true;
                txtlokOK1.Visible = true;
                txtQtyOK1.Visible = true;
                txtQtyKW1.Visible = true;
                txtlokKW1.Visible = true;

                txtQtyBPF0.Visible = true;
                txtQtyBPU0.Visible = true;
                //txtPartnoKW1.Visible = true;
                //txtlokKW1.Visible = true;
                //txtQtyKW1.Visible = true;
                LoadDataJemur();
                IsiPartno(0);
                PCut2H.Visible = false;
                PCut2D.Visible = false;
                LPartno1.Text = "PartNo OK";
                LPartno2.Text = "PartNo KW";
                RBCut1.Checked = true;
                RBCut2.Checked = false;
                Panel4.Visible = true;
                PCut2D.Visible = false;
                PCut3D.Visible = false;
                ChkTglProduksi0.Enabled = true;
                PanelPartnoOK.Visible = true;
                PanelPartnoKW.Visible = true;
                PanelInput.Visible = false;
                btnTansfer1.Visible = true;
                RBSource1.Checked = true;
                RBSource1.Visible = true;
                RBSource2.Visible = false;
                RBSource2.Checked = false;
            }
        }
        protected void RB9Mili_CheckedChanged(object sender, EventArgs e)
        {
            if (RB9Mili.Checked == true)
            {
                Panel9.Visible = true;
                PanelTahapIII0.Visible = false;
                PanelTahapIII1.Visible = false;
                PanelProduksi.Visible = true;
                RBCut1.Visible = true;
                RBCut2.Visible = true;
                RBKali2.Visible = false;
                RBKali2.Checked = false;
                RBKali4.Checked = true;
                RBKali4.Visible = true;
                RBKali6.Visible = true;
                RBKali12.Visible = true;
                RBKali19.Visible = true;
                RBSuperPanel.Visible = true;
                RBLisflank.Visible = true;
                Panel1.Visible = true;
                txtlokOK.Text = "H99";
                txtlokKW.Text = "I99";
                txtPartnoOK1.Visible = false;
                txtPartnoKW1.Visible = false;
                txtPartnoBPF0.Visible = false;
                txtPartnoBPU0.Visible = false;
                txtlokOK1.Visible = false;
                txtQtyOK1.Visible = false;
                txtQtyKW1.Visible = false;
                txtQtyBPF0.Visible = false;
                txtQtyBPU0.Visible = false;
                //txtPartnoKW1.Visible = false;
                //txtlokKW1.Visible = false;
                //txtQtyKW1.Visible = false;
                LoadDataJemur();
                IsiPartno(0);
                PCut2H.Visible = false;
                LPartno1.Text = "SuperPanel";
                LPartno2.Text = "ListPlank";
                RBCut1.Checked = true;
                RBCut2.Checked = false;
                Panel4.Visible = true;
                PCut2D.Visible = false;
                ChkTglProduksi0.Enabled = true;
                PCut3D.Visible = false;
                //RBSuperPanel.Checked = true;
                if (RBSuperPanel.Checked == true)
                {
                    PanelPartnoOK.Visible = false;
                    PanelPartnoKW.Visible = false;
                    PCut2H.Visible = false;
                    PanelPartnoBPUnFinish0.Visible = true;
                }
                else
                {
                    PanelPartnoOK.Visible = false;
                    PanelPartnoKW.Visible = false;
                    PCut2H.Visible = true;
                    PanelPartnoBPUnFinish0.Visible = false;
                }
                PanelPartnoOK.Visible = false;
                PanelPartnoKW.Visible = true;
                PanelPartnoBPFinish.Visible = false;
                PanelPartnoBPUnFinish.Visible = true;
                PanelSample.Visible = true;
                PanelInput.Visible = true;
                btnTansfer1.Visible = true;
                RBSource1.Checked = true;
                RBSource1.Visible = true;
                RBSource2.Visible = false;
                RBSource2.Checked = false;
            }
        }
        protected void RB4Mili_CheckedChanged(object sender, EventArgs e)
        {
            if (RB4Mili.Checked == true && RBCut1.Checked == true)
            {
                Panel9.Visible = true;
                PanelTahapIII0.Visible = false;
                PanelTahapIII1.Visible = false;
                RBCut1.Visible = true;
                RBCut2.Visible = true;
                RBKali2.Visible = true;
                RBKali2.Checked = true;
                RBKali4.Checked = false;
                RBKali6.Checked = false;
                RBKali12.Checked = false;
                RBKali4.Visible = false;
                RBKali6.Visible = false;
                RBKali12.Visible = false;
                RBKali19.Visible = false;
                RBSuperPanel.Visible = false;
                RBLisflank.Visible = false;
                RBSuperPanel.Checked = true;
                RBLisflank.Checked = false;
                Panel1.Visible = false;
                txtlokOK.Text = "H99";
                txtlokKW.Text = "I99";
                txtPartnoOK1.Visible = false;
                txtPartnoKW1.Visible = false;
                txtPartnoBPF0.Visible = false;
                txtPartnoBPU0.Visible = true;
                txtlokOK1.Visible = false;
                txtlokKW1.Visible = false;
                txtQtyOK1.Visible = false;
                txtQtyKW1.Visible = false;
                txtQtyBPF0.Visible = false;
                txtQtyBPU0.Visible = false;
                //txtPartnoKW1.Visible = true;
                //txtlokKW1.Visible = true;
                //txtQtyKW1.Visible = true;
                LoadDataJemur();
                IsiPartno(0);
                PCut2H.Visible = false;
                PCut2D.Visible = false;
                LPartno1.Text = "PartNo OK";
                LPartno2.Text = "PartNo KW";
                RBCut1.Checked = true;
                RBCut2.Checked = false;
                Panel4.Visible = true;
                PCut2D.Visible = false;
                PCut3D.Visible = false;
                ChkTglProduksi0.Enabled = true;
                PanelPartnoOK.Visible = true;
                PanelPartnoBPFinish.Visible = true;
                PanelPartnoBPUnFinish.Visible = true;
                PanelSample.Visible = true;
                PanelPartnoKW.Visible = true;
                PanelInput.Visible = false;
                btnTansfer1.Visible = true;
                PanelLevel.Visible = false;
                RBLisPlank2.Checked = true;
                PanelOpt.Visible = false;
                RBLisPlank1.Checked = false;
                RBSource1.Checked = true;
                RBSource1.Visible = true;
                RBSource2.Visible = false;
                RBSource2.Checked = false;
            }
            if (RB4Mili.Checked == true && RBCut1.Checked == false)
            {
                PCut3D.Visible = true;
                PCut2D.Visible = false;
                Panel4.Visible = false;
                RBCut1.Visible = true;
                RBCut2.Visible = true;
                LoadDataGridTerimaBP();
                ChkTglProduksi0.Checked = true;
                ChkTglProduksi0.Enabled = false;
                RBSource1.Checked = true;
                RBSource1.Visible = true;
                RBSource2.Visible = false;
                RBSource2.Checked = false;
            }
        }
        protected void RBKali2_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali2.Checked == true)
                IsiPartno(0);
        }
        protected void RBKali4_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali4.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
                PanelPartnoOK.Visible = true;
                PanelPartnoKW.Visible = true;
                PanelPartnoBPFinish.Visible = true;
                PanelPartnoBPUnFinish.Visible = true;
                PanelSample.Visible = true;
            }
        }
        protected void RBKali19_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali19.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
                PanelPartnoOK.Visible = true;
                PanelPartnoKW.Visible = true;
                PanelPartnoBPFinish.Visible = true;
                PanelPartnoBPUnFinish.Visible = true;
                PanelSample.Visible = true;
            }
        }
        protected void RBKali6_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali6.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
            }
        }
        protected void RBKali12_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali12.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
            }
        }
        protected void RBCut1_CheckedChanged(object sender, EventArgs e)
        {
            if (RBCut1.Checked == true)
            {
                PanelProduksi.Visible = true;
                if (RB9Mili.Checked == true)
                {
                    Panel4.Visible = true;
                    PCut2D.Visible = false;
                    PCut3D.Visible = false;
                    ChkTglProduksi0.Enabled = true;
                    LoadDataJemur();
                }
                if (RB4Mili.Checked == true)
                {
                    RBCut1.Visible = true;
                    RBCut2.Visible = true;
                    RBKali2.Visible = true;
                    RBKali2.Checked = true;
                    RBKali4.Checked = false;
                    RBKali6.Checked = false;
                    RBKali12.Checked = false;
                    RBKali4.Visible = false;
                    RBKali6.Visible = false;
                    RBKali12.Visible = false;
                    RBKali19.Visible = false;
                    RBSuperPanel.Visible = false;
                    RBLisflank.Visible = false;
                    txtlokOK.Text = "I99";
                    txtlokKW.Text = "I99";
                    txtPartnoOK1.Visible = true;
                    //txtPartnoKW1.Visible = true;
                    //txtPartnoBPF0.Visible = true;
                    //txtPartnoBPU0.Visible = true;
                    txtlokOK1.Visible = true;
                    txtQtyOK1.Visible = true;
                    //txtQtyKW1.Visible = true;
                    //txtQtyBPF0.Visible = true;
                    //txtQtyBPU0.Visible = true;
                    //txtPartnoKW1.Visible = true;
                    //txtlokKW1.Visible = true;
                    //txtQtyKW1.Visible = true;
                    LoadDataJemur();
                    IsiPartno(0);
                    PCut2H.Visible = false;
                    PCut2D.Visible = false;
                    LPartno1.Text = "PartNo OK";
                    LPartno2.Text = "PartNo KW";
                    RBCut1.Checked = true;
                    RBCut2.Checked = false;
                    Panel4.Visible = true;
                    PCut2D.Visible = false;
                    PCut3D.Visible = false;
                    ChkTglProduksi0.Enabled = true;
                }
            }
            else
            {
                Panel4.Visible = false;
            }

        }
        protected void RBCut2_CheckedChanged(object sender, EventArgs e)
        {
            if (RBCut2.Checked == true)
            {
                PanelProduksi.Visible = false;
                //if (RB9Mili.Checked == true)
                //{
                //    PCut3D.Visible = false;
                //    PCut2D.Visible = true;
                //    Panel4.Visible = false;
                //    LoadDataGridTerima();
                //}
                //if (RB4Mili.Checked == true)
                //{
                PCut3D.Visible = true;
                PCut2D.Visible = false;
                Panel4.Visible = false;
                RBCut1.Visible = true;
                RBCut2.Visible = true;
                LoadDataGridTerimaBP();
                ChkTglProduksi0.Checked = true;
                ChkTglProduksi0.Enabled = false;
                LoadDataGridViewSimetris();
                GridViewSimetris.Visible = true;
                GridViewAsimetris.Visible = false;
                //}
            }
            else
            {
                PCut2D.Visible = false;
            }
        }
        private void LoadDataGridTerima()
        {
            try
            {
                ArrayList arrTerima = new ArrayList();
                T3_RekapFacade rekap = new T3_RekapFacade();
                string criteria = string.Empty;
                string tglproduksi = DateTime.Parse(txtDate.Text).ToString("yyyyMMdd");
                string likepartno = string.Empty;
                string sumber = string.Empty;
                if (RBPSOK.Checked == true)
                    sumber = "-3-";
                else
                    sumber = "-P-";
                if (RBKali13.Checked == true)
                    likepartno = sumber + "09012332440";
                if (RBKali14.Checked == true)
                    likepartno = sumber + "09012152440";
                if (RBKali15.Checked == true)
                    likepartno = sumber + "09012102440";
                string strtglproduksi = "AND CONVERT(char(8), A.tglproduksi, 112) = '";
                string palet = string.Empty;
                if (txtnopalet.Text != string.Empty)
                    palet = " and A.palet='" + txtnopalet.Text + "' ";
                if (ChkTglProduksi0.Checked == false)
                    arrTerima = rekap.RetrieveByTglTerimaLisplankL2(strtglproduksi + tglproduksi + "'", likepartno, palet);
                else
                    arrTerima = rekap.RetrieveByTglTerimaLisplankL2(" ", likepartno, palet);
                GridViewTerima.DataSource = arrTerima;
                GridViewTerima.DataBind();
            }
            catch { }
        }
        private void LoadDataGridTerimaBP()
        {
            ArrayList arrTerima = new ArrayList();
            T3_RekapFacade rekap = new T3_RekapFacade();
            string criteria = string.Empty;
            string tglproduksi = DateTime.Parse(txtDate.Text).ToString("yyyyMMdd");
            string likepartno = string.Empty;
            string sumber = string.Empty;

            string strtglproduksi = "AND CONVERT(char(8), A.tglproduksi, 112) = '";
            string palet = string.Empty;
            if (txtnopalet.Text != string.Empty)
                palet = " and A.palet='" + txtnopalet.Text + "' ";
            if (ChkTglProduksi0.Checked == false)
                arrTerima = rekap.RetrieveByTglTerimaBP(strtglproduksi + tglproduksi + "'", txtPartnoAsal0.Text, txtlokAsal0.Text);
            else
                arrTerima = rekap.RetrieveByTglTerimaBP(" ", txtPartnoAsal0.Text, txtlokAsal0.Text);
            //foreach (T3_Rekap t3rekap in arrTerima)
            //{

            //}
            GridViewokbp.Visible = false;
            GridViewTerimaBP.Visible = true;
            GridViewTerimaBP.DataSource = arrTerima;
            GridViewTerimaBP.DataBind();
        }
        private void IsiPartnoLisplankLvl2(string kode)
        {
            try
            {
                string sumber = string.Empty;
                //PanelInput1.Visible = true ;
                Users users = (Users)Session["Users"];
                txtQtyOK.Text = "0";
                txtQtyKW.Text = "0";
                txtQtyBPF.Text = "0";
                txtQtyBPU.Text = "0";
                txtQtyBPU0.Text = "0";
                txtQtyBPS.Text = "0";
                txtQtyOK1.Text = "0";
                txtQtyKW1.Text = "0";
                txtQtyBPF0.Text = "0";
                if (RBPSOK.Checked == true)
                    sumber = "-3-";
                else
                    sumber = "-P-";
                txtQtyAsal.Text = "0";
                string sisi = string.Empty;
                if (RBSuperFlank.Checked == true)
                    sisi = "B1";
                else
                    sisi = "SE";
                //txtPartnoPOK1.Visible = true;
                //txtlokPOK1.Visible = true;
                //txtQtyPOK1.Visible = true;
                //txtPartnoPBP1.Visible = true;
                //txtlokPBP1.Visible = true;
                //txtQtyPBP1.Visible = true;

                //RBKali4.Text = "1213 X 2440";
                //RBKali6.Text = "1221 X 2440";
                //RBKali12.Text = "1233 X 2440";
                if (RBLisflank.Checked == true)
                {
                    if (RBLisPlank2.Checked == true)
                    {
                        if (RBKali13.Checked == true)
                        {
                            txtPartnoPOK.Text = kode + "-3-" + LPartno.Text.Substring(6, 3) + "01002440" + sisi;
                            txtPartnoAsal.Text = kode + sumber + "090" + RBKali12.Text.Substring(0, 4) + "2440SE";
                            txtPartnoPBP.Text = kode + "-P-" + LPartno.Text.Substring(6, 3) + "01002440" + sisi;
                            if (users.UnitKerjaID == 1)
                            {
                                txtlokPOK.Text = "AA13";
                                txtlokAsal.Text = "AA13";
                                txtlokPBP.Text = "AA13";
                            }
                            else
                            {
                                txtlokPOK.Text = "F32";
                                txtlokAsal.Text = "E18";
                                txtlokPBP.Text = "F32";
                            }
                        }
                        if (RBKali14.Checked == true)
                        {
                            txtPartnoPOK.Text = kode + "-3-" + LPartno.Text.Substring(6, 3) + "02002440" + sisi;

                            txtPartnoAsal.Text = kode + sumber + "090" + RBKali6.Text.Substring(0, 4) + "2440SE";
                            txtPartnoPBP.Text = kode + "-P-" + LPartno.Text.Substring(6, 3) + "02002440" + sisi;
                            if (users.UnitKerjaID == 1)
                            {
                                txtlokPOK.Text = "AA13";
                                txtlokAsal.Text = "AA13";
                                txtlokPBP.Text = "AA13";
                            }
                            else
                            {
                                txtlokPOK.Text = "F34";
                                txtlokAsal.Text = "E17";
                                txtlokPBP.Text = "F34";
                            }
                        }
                        if (RBKali15.Checked == true)
                        {
                            txtPartnoPOK.Text = kode + "-3-" + LPartno.Text.Substring(6, 3) + "03002440" + sisi;
                            txtPartnoAsal.Text = kode + sumber + "090" + RBKali4.Text.Substring(0, 4) + "2440" + sisi;
                            txtPartnoPBP.Text = kode + "-P-" + LPartno.Text.Substring(6, 3) + "03002440" + sisi;

                            if (users.UnitKerjaID == 1)
                            {
                                txtlokPOK.Text = "AA13";
                                txtlokAsal.Text = "AA13";
                                txtlokPBP.Text = "AA13";
                            }
                            else
                            {
                                txtlokPOK.Text = "F36";
                                txtlokAsal.Text = "E16";
                                txtlokPBP.Text = "F36";
                            }
                        }
                        int kali = pengali();

                        if (txtQtyAsal.Text == string.Empty)
                            txtQtyAsal.Text = "0";
                        if (txtQtyPOK.Text == string.Empty)
                            txtQtyPOK.Text = "0";
                        if (txtQtyPBP.Text == string.Empty)
                            txtQtyPBP.Text = "0";
                        if (txtQtyPBP.Text != string.Empty && txtQtyPOK.Text != string.Empty)
                        {
                            txtQtyAsal.Text = txtSerah.Text;
                            txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text) * kali) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                        }
                    }

                    else
                    {
                        PanelInput1.Visible = false;
                        if (txtQtyPBP.Text == string.Empty)
                            txtQtyPBP.Text = "0";
                        txtQtyKW.Text = txtSerah.Text;
                        txtPartnoPOK.Text = txtPartnoKW.Text;
                        txtlokPOK.Text = txtlokKW.Text;
                        txtPartnoPBP.Text = txtPartnoBPF.Text;
                        txtQtyKW.Text = txtQtyPOK.Text;
                        txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                        txtlokAsal.Text = "I99";
                        txtlokPBP.Text = "AA13";
                        txtQtyOK.Text = "0";
                        txtQtyBPF.Text = txtQtyPBP.Text;
                        //txtPartnoPOK1.Visible = false;
                        //txtlokPOK1.Visible = false;
                        //txtQtyPOK1.Visible = false;
                        //txtPartnoPBP1.Visible = false;
                        //txtlokPBP1.Visible = false;
                        //txtQtyPBP1.Visible = false;
                    }
                }
                if (RBSuperPanel.Checked == true)
                {
                    PanelInput1.Visible = false;
                    if (txtQtyPBP.Text == string.Empty)
                        txtQtyPBP.Text = "0";
                    txtPartnoPOK.Text = txtPartnoOK.Text;
                    txtlokPOK.Text = txtlokOK.Text;
                    txtPartnoPBP.Text = txtPartnoBPF.Text;
                    txtQtyKW.Text = "0";
                    txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                    txtlokAsal.Text = "H99";
                    txtlokPBP.Text = "FN02";
                    txtQtyOK.Text = txtQtyPOK.Text;
                    txtQtyBPF.Text = txtQtyPBP.Text;
                    //txtPartnoPOK1.Visible = false;
                    //txtlokPOK1.Visible = false;
                    //txtQtyPOK1.Visible = false;
                    //txtPartnoPBP1.Visible = false;
                    //txtlokPBP1.Visible = false;
                    //txtQtyPBP1.Visible = false;
                }
            }
            catch { }
        }
        protected void RBKali13_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali13.Checked == true)
            {
                //LoadDataGridTerima();
                //ClearPotong2();
                try
                {
                    string kode = string.Empty;
                    if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                        kode = LPartno.Text.Substring(0, 3);
                    else
                        kode = "INT";
                    IsiPartnoLisplankLvl2(kode);
                    RBKali4.Checked = false;
                    RBKali6.Checked = false;
                    RBKali12.Checked = true;
                    txtnopalet.Text = string.Empty;

                    //LoadDataGridTerima();
                    IsiPartno1(0);
                }
                catch { }
            }
        }
        protected void RBKali14_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali14.Checked == true)
            {
                //LoadDataGridTerima();
                //ClearPotong2();
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                RBKali4.Checked = false;
                RBKali6.Checked = true;
                RBKali12.Checked = false;
                txtnopalet.Text = string.Empty;

                //LoadDataGridTerima();
                IsiPartno1(0);
            }
        }
        protected void RBKali15_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali15.Checked == true)
            {
                // LoadDataGridTerima();
                //ClearPotong2();
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                RBKali4.Checked = true;
                RBKali6.Checked = false;
                RBKali12.Checked = false;
                txtnopalet.Text = string.Empty;

                //LoadDataGridTerima();
                IsiPartno1(0);
            }
        }
        protected void RBPSOK_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPSOK.Checked == true)
            {
                //LoadDataGridTerima();
                ClearPotong2();
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
            }
        }
        protected void RBPSBP_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPSBP.Checked == true)
            {
                //LoadDataGridTerima();
                ClearPotong2();
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
            }
        }
        protected void txtNoPalet_TextChanged(object sender, EventArgs e)

        {
            if (ChkOven.Checked != true)
            {
                if (RB1000.Checked == true)
                {
                    LoadDataJemur();
                    IsiPartno(0);
                    if (GridViewokbp.Rows[0].Cells[4].Text == "&nbsp;")
                    {
                        DisplayAJAXMessage(this, "Data produksi palet : " + txtnopalet.Text + " tidak ditemukan");
                        txtnopalet.Text = string.Empty;
                        LoadDataJemur();
                    }
                }
                if (RB9Mili.Checked == true)
                {
                    if (RBCut1.Checked == true)
                    {
                        LoadDataJemur();
                        IsiPartno(0);
                        if (GridViewokbp.Rows[0].Cells[4].Text == "&nbsp;")
                        {
                            DisplayAJAXMessage(this, "Data produksi palet : " + txtnopalet.Text + " tidak ditemukan");
                            txtnopalet.Text = string.Empty;
                            LoadDataJemur();
                        }
                    }
                    if (RBCut2.Checked == true)
                    {
                        ClearPotong2();
                        // LoadDataGridTerima();
                        IsiPartnoLisplankLvl2(GridViewTerima.Rows[0].Cells[7].Text.Substring(0, 3));
                        if (GridViewTerima.Rows[0].Cells[4].Text == "&nbsp;")
                        {
                            DisplayAJAXMessage(this, "Data produksi palet : " + txtnopalet.Text + " tidak ditemukan");
                            txtnopalet.Text = string.Empty;
                            //LoadDataGridTerima();
                        }
                    }
                }
                if (RB4Mili.Checked == true)
                {
                    LoadDataJemur();
                    IsiPartno(0);
                    if (GridViewokbp.Rows[0].Cells[4].Text == "&nbsp;")
                    {
                        DisplayAJAXMessage(this, "Data produksi palet : " + txtnopalet.Text + " tidak ditemukan");
                        txtnopalet.Text = string.Empty;
                        LoadDataJemur();
                    }
                }
            }
            else
            {
                EditOven();
            }
        }
        protected void ChkMutasi_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
                TextBox txtQtyMutasi = (TextBox)GridViewTerima.Rows[row.RowIndex].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewTerima.Rows[row.RowIndex].FindControl("chkMutasi");
                int jumlah = 0;
                int lastindex = 0;
                if (chkMutasi.Checked == true)
                {
                    if (txtQtyAsal.Text != string.Empty)
                        jumlah = int.Parse(txtQtyAsal.Text);
                    txtQtyMutasi.Text = GridViewTerima.Rows[row.RowIndex].Cells[9].Text;
                    jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                    txtPartnoAsal.Text = GridViewTerima.Rows[row.RowIndex].Cells[7].Text;
                    txtlokAsal.Text = GridViewTerima.Rows[row.RowIndex].Cells[8].Text;
                    lastindex = row.RowIndex;
                }
                else
                {
                    //jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                    txtQtyMutasi.Text = "0";
                    for (int i = 0; i <= GridViewTerima.Rows.Count - 1; i++)
                    {
                        txtQtyMutasi = (TextBox)GridViewTerima.Rows[i].FindControl("txtQtyMutasi");
                        chkMutasi = (CheckBox)GridViewTerima.Rows[i].FindControl("chkMutasi");
                        if (chkMutasi.Checked == false)
                            txtQtyMutasi.Text = string.Empty;
                        if (txtQtyMutasi.Text != string.Empty)
                        {
                            jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                        }
                    }
                }
                GridViewTerima.SelectedIndex = lastindex;
                txtQtyMutasi = (TextBox)GridViewTerima.Rows[lastindex].FindControl("txtQtyMutasi");
                txtQtyMutasi.Focus();
                txtQtyAsal.Text = jumlah.ToString();
                int kali = pengali();
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                txtQtyPOK.Text = txtQtyPOK.Text.Trim();
                if (RBSuperPanel.Checked == false)
                    txtQtyPOK.Text = ((Convert.ToInt32(txtQtyAsal.Text) * kali) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                else
                    txtQtyPOK.Text = ((Convert.ToInt32(txtQtyAsal.Text)) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                IsiPartnoLisplankLvl2(GridViewTerima.Rows[0].Cells[7].Text.Substring(0, 3));
            }
            catch { }

        }
        protected void txtQtyMutasi_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi = (TextBox)GridViewTerima.Rows[row.RowIndex].FindControl("txtQtyMutasi");
            CheckBox chkMutasi = (CheckBox)GridViewTerima.Rows[row.RowIndex].FindControl("chkMutasi");
            int jumlah = 0;
            int lastindex = 0;
            //txtQtyMutasi.Text = "0";
            for (int i = 0; i <= GridViewTerima.Rows.Count - 1; i++)
            {
                txtQtyMutasi = (TextBox)GridViewTerima.Rows[i].FindControl("txtQtyMutasi");
                chkMutasi = (CheckBox)GridViewTerima.Rows[i].FindControl("chkMutasi");
                if (chkMutasi.Checked == false)
                    txtQtyMutasi.Text = string.Empty;
                else
                {
                    if (txtQtyMutasi.Text != string.Empty)
                    {
                        jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                        lastindex = i;
                        if (Convert.ToInt32(GridViewTerima.Rows[i].Cells[9].Text) < Convert.ToInt32(txtQtyMutasi.Text))
                            txtQtyMutasi.Text = string.Empty;
                    }
                }
            }
            GridViewTerima.SelectedIndex = lastindex;
            txtQtyMutasi = (TextBox)GridViewTerima.Rows[lastindex].FindControl("txtQtyMutasi");
            txtQtyMutasi.Focus();
            txtQtyAsal.Text = jumlah.ToString();
        }
        protected int pengali()
        {
            int hasil = 0;
            if (RBKali13.Checked == true)
                hasil = 12;
            if (RBKali14.Checked == true)
                hasil = 6;
            if (RBKali15.Checked == true)
                hasil = 4;
            return hasil;
        }
        protected void ClearPotong2()
        {
            //txtPartnoAsal.Text = "";
            ArrayList arrPOK = new ArrayList();
            T1_Jemur POK = new T1_Jemur();
            Session["arrPOK"] = null;

            //txtPartnoPOK.Text = "";
            txtPartnoPBP.Text = "0";
            //txtPartnoPBS.Text = "";
            //txtlokAsal.Text = "";
            //txtlokPOK.Text = "";
            txtlokPBP.Text = "0";
            txtQtyAsal.Text = "0";
            //txtlokPBS.Text = "";
            //txtQtyPOK.Text = "0";
            txtQtyPBP.Text = "0";
            //txtQtyPBS.Text = "0";
        }
        protected void ClearPotong3()
        {
            txtPartnoAsal0.Text = "0";
            txtPartnoPOK0.Text = "0";
            //txtPartnoPBS0.Text = "";
            txtlokAsal0.Text = "0";
            txtlokPOK0.Text = "0";
            txtQtyAsal0.Text = "0";
            txtQtyPOK0.Text = "0";
        }
        protected void ClearAutoBS()
        {
            LCQtyBS1.Text = string.Empty;
            LCPartnoBS1.Text = string.Empty;
            LCQtyBS2.Text = string.Empty;
            LCPartnoBS2.Text = string.Empty;
            LCQtyBS3.Text = string.Empty;
            LCPartnoBS3.Text = string.Empty;
            LCQtyBS4.Text = string.Empty;
            LCPartnoBS4.Text = string.Empty;
        }
        protected void txtQtyPBP_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            int i = 0;
            int totlari = 0;
            foreach (T1_Jemur pelarian in arrPelarian)
            {
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                i++;
            }
            if (RBLisflank.Checked == true)
            {
                int kali = pengali();
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                if (txtQtyPBP.Text != string.Empty && txtQtyPOK.Text != string.Empty)
                {
                    if (RBSimetris.Checked == true)
                    {
                        if (RBLisPlank2.Checked == true)
                        {
                            txtQtyPOK.Text = (((Convert.ToInt32(txtSerah.Text) - totlari) * kali) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                            txtQtyKW.Text = (Convert.ToInt32(txtSerah.Text) - totlari).ToString();
                            txtQtyBPF.Text = "0";
                        }
                        else
                        {
                            txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text) - totlari).ToString();
                            txtQtyKW.Text = txtQtyPOK.Text;
                            txtQtyBPF.Text = txtQtyPBP.Text;
                        }

                    }
                    //else
                    //    txtQtyPOK.Text = ((Convert.ToInt32(txtQtyAsal.Text)) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                }
            }
            if (RBSuperPanel.Checked == true)
            {
                int kali = pengali();
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                if (txtQtyPBP.Text != string.Empty && txtQtyPOK.Text != string.Empty)
                {

                    txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text) - Convert.ToUInt32(txtQtyKW.Text) - totlari).ToString();
                    txtQtyOK.Text = txtQtyPOK.Text;
                    txtQtyBPF.Text = txtQtyPBP.Text;
                }
            }
        }
        protected void txtQtyAsal_TextChanged(object sender, EventArgs e)
        {
            if (RBLisflank.Checked == true)
            {
                //LoadDataGridTerima();
                int kali = pengali();
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                //if (txtQtyPBS.Text == string.Empty)
                //    txtQtyPBS.Text = "0";
                txtQtyPOK.Text = ((Convert.ToInt32(txtQtyAsal.Text) * kali) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                txtQtyPBP.Focus();
                txtSerah.Text = txtQtyAsal.Text;
                if (Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) < Convert.ToInt32(txtSerah.Text))
                    txtSerah.Text = (Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text)).ToString();
                //int jumlah = Convert.ToInt32(txtQtyAsal.Text);
                //int total = 0;
                //for (int i = 0; i <= GridViewTerima.Rows.Count - 1; i++)
                //{
                //    TextBox txtQtyMutasi = (TextBox)GridViewTerima.Rows[i].FindControl("txtQtyMutasi");
                //    CheckBox chkMutasi = (CheckBox)GridViewTerima.Rows[i].FindControl("chkMutasi");

                //    if (Convert.ToInt32(GridViewTerima.Rows[i].Cells[9].Text) < jumlah)
                //    {
                //        chkMutasi.Checked = true;
                //        txtQtyMutasi.Text = (Convert.ToInt32(GridViewTerima.Rows[i].Cells[9].Text)).ToString();
                //        jumlah = jumlah - (Convert.ToInt32(GridViewTerima.Rows[i].Cells[9].Text));
                //        total = total + (Convert.ToInt32(GridViewTerima.Rows[i].Cells[9].Text));
                //    }
                //    else
                //    {
                //        chkMutasi.Checked = true;
                //        txtQtyMutasi.Text = jumlah.ToString();
                //        total = total + jumlah;

                //        break;
                //    }

                //}
                //txtQtyAsal.Text = total.ToString();
            }
            if (RBSuperPanel.Checked == true)
            {
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                txtQtyPBP.Focus();
                txtQtyAsal.Text = txtQtyPOK.Text;
                txtSerah.Text = txtQtyAsal.Text;
                if (Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) < Convert.ToInt32(txtSerah.Text))
                    txtSerah.Text = (Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text)).ToString();
            }
        }
        protected void txtQtyPOK_TextChanged(object sender, EventArgs e)
        {
            if (RBLisflank.Checked == true)
            {
                int kali = pengali();
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                if ((Convert.ToInt32(txtQtyAsal.Text) * kali) - Convert.ToUInt32(txtQtyPOK.Text) > 0)
                    txtQtyPBP.Text = ((Convert.ToInt32(txtQtyAsal.Text) * kali) - Convert.ToUInt32(txtQtyPOK.Text)).ToString();
                else
                    txtQtyPBP.Text = "0";
            }
            if (RBSuperPanel.Checked == true)
            {
                int kali = pengali();
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                if ((Convert.ToInt32(txtSerah.Text)) < Convert.ToUInt32(txtQtyPOK.Text))
                    txtQtyPOK.Text = txtSerah.Text;
                if ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPOK.Text) > 0)
                    txtQtyPBP.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPOK.Text) - Convert.ToUInt32(txtQtyKW.Text)).ToString();
                else
                    txtQtyPBP.Text = "0";
                txtQtyAsal.Text = txtSerah.Text;
                txtQtyOK.Text = txtQtyPOK.Text;
                txtQtyBPF.Text = txtQtyPBP.Text;
            }
        }
        protected void txtDateSerah_TextChanged(object sender, EventArgs e)
        {
            if (RBSimetris.Checked == true)
                LoadDataGridViewSimetris();
            else
                LoadDataGridViewASimetris();
            LoadDataGridSerah(Convert.ToDateTime(txtDateSerah.Text), "serah");

        }
        protected void ChkTglProduksi0_CheckedChanged(object sender, EventArgs e)
        {
            txtnopalet.Text = string.Empty;
            if (RBCut1.Checked == true)
            {
                LoadDataJemur();
                IsiPartno(0);
            }
            if (RBCut2.Checked == true)
            {
                //LoadDataGridTerima();
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
            }
            if (RB4Mili.Checked == true)
            {
                LoadDataJemur();
                IsiPartno(0);
            }
        }
        protected void PartnoTransit()
        {
            Users users = (Users)Session["Users"];
            if (users.UnitKerjaID == 1)
            {
                RBKali4.Text = "1209 X 2440";
                RBKali6.Text = "1215 X 2440";
                RBKali12.Text = "1197 X 2440";
            }
            else
            {
                RBKali4.Text = "= Partno Asal";
                RBKali6.Text = "1221 X 2440";
                RBKali12.Text = "1233 X 2440";
            }
        }
        protected void Simpan()
        {
            string test = txtQtyKW.Text;
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            T1_JemurFacade jemurFacade = new T1_JemurFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            Users users = (Users)Session["Users"];
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToDateTime(txtDateSerah.Text).Year;
            int Bulan = Convert.ToDateTime(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            btnTansfer.Disabled = true;
            #region validasi data
            int i = 0;
            int totlari = 0;
            int checktrans = 0;
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtPartnoOK.Text == string.Empty) { btnTansfer.Disabled = false; return; }
            foreach (T1_Jemur pelarian in arrPelarian)
            {
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                i++;
            }
            if (RB4Mili.Checked == false)
            {
                txtPartnoOK.Text = txtPartnoPOK.Text;
                //txtPartnoKW.Text = txtPartnoPOK.Text;
                txtPartnoBPF.Text = txtPartnoPBP.Text;
            }
            if (txtPartnoOK.Text.IndexOf("-S-") != -1 || txtPartnoKW.Text.IndexOf("-S-") != -1 || txtPartnoBPF.Text.IndexOf("-S-") != -1)
            {
                DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                return;
            }
            if (txtQtyOK.Text.Trim() == string.Empty)
                txtQtyOK.Text = "0";
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtQtyBPU.Text.Trim() == string.Empty)
                txtQtyBPU.Text = "0";
            if (txtQtyBPS.Text.Trim() == string.Empty)
                txtQtyBPS.Text = "0";

            if (LQtyIn.Text.Trim() != string.Empty && txtQtyOK.Text.Trim() != string.Empty && txtQtyBPF.Text.Trim() != string.Empty
                && txtQtyBPU.Text.Trim() != string.Empty && txtQtyBPS.Text.Trim() != string.Empty)
            {
                if (RBLisflank.Checked == true)
                    //txtQtyKW.Text = "0";
                    checktrans = Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - (Convert.ToInt32(txtQtyOK.Text) +
                        Convert.ToInt32(txtQtyBPF.Text) + Convert.ToInt32(txtQtyBPU.Text) + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (RB1000.Checked == true)
                    checktrans = Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) -
                        (Convert.ToInt32(txtQtyKW1.Text) / 2) - ((Convert.ToInt32(txtQtyOK1.Text) / 2)
                        + (Convert.ToInt32(txtQtyBPF0.Text) / 2) + (Convert.ToInt32(txtQtyBPU0.Text) / 2)
                        + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (checktrans != 0)
                {
                    DisplayAJAXMessage(this, "Quantity Serah Error");
                    if (RB1000.Checked == true)
                        txtPartnoOK1.Focus();
                    else
                        txtPartnoOK.Focus();
                    jemurFacade.UpdateFail(Convert.ToInt32(LID.Text));
                    LoadDataJemur();
                    btnTansfer.Disabled = false;
                    return;
                }
                #endregion validasi data
                else
                {
                    #region rekam data serah
                    T1_Jemur jemur = new T1_Jemur();
                    FC_Items items = new FC_Items();
                    FC_Items items1 = new FC_Items();
                    FC_ItemsFacade itemsF = new FC_ItemsFacade();
                    ArrayList arrserah = new ArrayList();
                    FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
                    jemur = jemurFacade.RetrieveJemurByID(LID.Text);
                    items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                    if (items.ID == 0)
                    {
                        items = new FC_Items();
                        items.ItemTypeID = 3;
                        items.Kode = txtPartnoOK.Text.Substring(0, 3);
                        items.Tebal = decimal.Parse(txtPartnoOK.Text.Substring(6, 3)) / 10;
                        items.Lebar = int.Parse(txtPartnoOK.Text.Substring(9, 4));
                        items.Panjang = int.Parse(txtPartnoOK.Text.Substring(13, 4));
                        items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                        items.Partno = txtPartnoOK.Text;
                        items.GroupID = 0;
                        itemsF.Insert(items);
                        items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                    }
                    items1 = itemsfacade.RetrieveByPartNo(LPartno.Text);
                    jemur.ItemID0 = dest.GetPartnoID(LPartno.Text);
                    jemur.LokasiID0 = dest.GetLokID(LLokasi.Text);
                    jemur.CreatedBy = users.UserName;
                    jemur.Oven = ddlOven.SelectedItem.Text;
                    jemur.TglJemur = Convert.ToDateTime(txtDateSerah.Text);
                    int ItemIDDest = items1.ID;
                    if (txtPartnoOK.Text != string.Empty && txtQtyOK.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyOK.Text) > 0)
                        {
                            T1_Serah serah = new T1_Serah();
                            serah.PartnoSer = txtPartnoOK.Text;
                            serah.PartnoDest = LPartno.Text;
                            serah.ItemIDSer = items.ID;
                            if (serah.ItemIDSer == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, txtPartnoOK.Text + " belum tersedia!");
                                return;
                            }
                            serah.QtyIn = int.Parse(txtQtyOK.Text);
                            serah.LokasiID = dest.GetLokID(txtlokOK.Text);
                            if (serah.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokOK.Text + "belum tersedia!");
                                return;
                            }
                            if (txtlokOK1.Text == string.Empty)
                                txtlokOK1.Text = txtlokOK.Text;
                            serah.LokasiSer = txtlokOK1.Text;
                            serah.DestID = jemur.DestID;
                            serah.ItemIDDest = ItemIDDest;
                            serah.HPP = jemur.HPP * ((items.Lebar * items.Panjang) / jemur.LuasA);
                            serah.JemurID = jemur.ID;
                            serah.CreatedBy = users.UserName;
                            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah.SFrom = "jemur";
                            serah.Oven = ddlOven.SelectedValue;
                            arrserah.Add(serah);
                        }
                    }
                    if (txtPartnoKW.Text != string.Empty && txtQtyKW.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyKW.Text) > 0)
                        {
                            T1_Serah serah = new T1_Serah();
                            items = itemsfacade.RetrieveByPartNo(txtPartnoKW.Text);
                            serah.ItemIDSer = items.ID;
                            if (serah.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoKW.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoKW.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoKW.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoKW.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoKW.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoKW.Text);
                            }
                            serah.PartnoDest = LPartno.Text;
                            serah.QtyIn = int.Parse(txtQtyKW.Text);
                            serah.LokasiID = dest.GetLokID(txtlokKW.Text);
                            if (serah.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokKW.Text + " belum tersedia!");
                                return;
                            }
                            serah.PartnoSer = txtPartnoKW.Text;
                            serah.LokasiSer = txtlokKW1.Text;
                            serah.DestID = jemur.DestID;
                            serah.ItemIDDest = ItemIDDest;
                            serah.HPP = jemur.HPP * ((items.Lebar * items.Panjang) / jemur.LuasA);
                            serah.JemurID = jemur.ID;
                            serah.CreatedBy = users.UserName;
                            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah.SFrom = "jemur";
                            serah.Oven = ddlOven.SelectedValue;
                            arrserah.Add(serah);
                        }
                    }
                    if (txtPartnoBPF.Text != string.Empty && txtQtyBPF.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPF.Text) > 0)
                        {
                            T1_Serah serah1 = new T1_Serah();
                            serah1.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPF.Text);
                            if (serah1.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPF.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPF.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPF.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPF.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPF.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPF.Text);
                            }
                            serah1.PartnoDest = LPartno.Text;
                            serah1.QtyIn = int.Parse(txtQtyBPF.Text);
                            serah1.LokasiID = dest.GetLokID(txtlokBPF.Text);
                            if (serah1.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi BP Finish belum tersedia!");
                                return;
                            }
                            serah1.PartnoSer = txtPartnoBPF.Text;
                            serah1.LokasiSer = txtlokBPF0.Text;
                            serah1.DestID = jemur.DestID;
                            serah1.ItemIDDest = ItemIDDest;
                            serah1.HPP = jemur.HPP;
                            serah1.JemurID = jemur.ID;
                            serah1.CreatedBy = users.UserName;
                            serah1.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah1.SFrom = "jemur";
                            serah1.Oven = ddlOven.SelectedValue;
                            arrserah.Add(serah1);
                        }
                    }
                    if (txtPartnoBPU.Text != string.Empty && txtQtyBPU.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPU.Text) > 0)
                        {
                            T1_Serah serah2 = new T1_Serah();
                            serah2.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPU.Text);
                            if (serah2.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPU.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPU.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPU.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPU.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPU.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPU.Text);
                            }
                            serah2.PartnoDest = LPartno.Text;
                            serah2.PartnoSer = txtPartnoBPU0.Text;
                            serah2.QtyIn = int.Parse(txtQtyBPU.Text);
                            serah2.LokasiID = dest.GetLokID(txtlokBPU.Text);
                            if (serah2.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPU.Text + " belum tersedia!");
                                return;
                            }
                            serah2.LokasiSer = txtlokBPU0.Text;
                            serah2.DestID = jemur.DestID;
                            serah2.ItemIDDest = ItemIDDest;
                            serah2.HPP = jemur.HPP;
                            serah2.JemurID = jemur.ID;
                            serah2.CreatedBy = users.UserName;
                            serah2.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah2.SFrom = "jemur";
                            serah2.Oven = ddlOven.SelectedValue;
                            arrserah.Add(serah2);
                        }
                    }
                    if (txtPartnoBPS.Text != string.Empty && txtQtyBPS.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPS.Text) > 0)
                        {
                            T1_Serah serah3 = new T1_Serah();
                            serah3.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPS.Text);
                            if (serah3.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 1;
                                items.Kode = txtPartnoBPS.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPS.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPS.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPS.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPS.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPS.Text);
                            }
                            serah3.PartnoDest = LPartno.Text;
                            serah3.PartnoSer = txtPartnoBPS.Text;
                            serah3.QtyIn = int.Parse(txtQtyBPS.Text);
                            serah3.LokasiID = dest.GetLokID(txtlokBPS.Text);
                            if (serah3.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPS.Text + " tersedia!");
                                return;
                            }
                            serah3.DestID = jemur.DestID;
                            serah3.ItemIDDest = ItemIDDest;
                            serah3.HPP = jemur.HPP;
                            serah3.JemurID = jemur.ID;
                            serah3.CreatedBy = users.UserName;
                            serah3.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah3.SFrom = "jemur";
                            serah3.Oven = ddlOven.SelectedValue;
                            arrserah.Add(serah3);
                        }
                    }
                    if (Session["arrbsauto"] != null)
                    {
                        ArrayList arrbsauto = new ArrayList();
                        arrbsauto = (ArrayList)Session["arrbsauto"];
                        foreach (BSAuto autobs in arrbsauto)
                        {
                            if (autobs.Qty > 0)
                            {
                                T1_Serah serah4 = new T1_Serah();
                                serah4.ItemIDSer = serahFacade.GetPartnoID(autobs.Partno);
                                serah4.PartnoDest = LPartno.Text;
                                serah4.PartnoSer = autobs.Partno;
                                serah4.QtyIn = autobs.Qty;
                                serah4.LokasiID = dest.GetLokID(autobs.Lokasi);
                                if (serah4.LokasiID == 0)
                                {
                                    btnTansfer.Disabled = false;
                                    DisplayAJAXMessage(this, "Lokasi : " + autobs.Lokasi + " tersedia!");
                                    return;
                                }
                                serah4.LokasiSer = autobs.Lokasi;
                                serah4.DestID = jemur.DestID;
                                serah4.ItemIDDest = ItemIDDest;
                                serah4.HPP = jemur.HPP;
                                serah4.JemurID = jemur.ID;
                                serah4.CreatedBy = users.UserName;
                                serah4.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                                serah4.SFrom = "jemur";
                                serah4.Oven = ddlOven.SelectedValue;
                                arrserah.Add(serah4);
                            }
                        }
                    }
                    #endregion rekam data serah
                    #region pelarian
                    i = 0;
                    foreach (T1_Jemur pelarian in arrPelarian)
                    {
                        T1_Serah serah4 = new T1_Serah();
                        TextBox txtloklari = (TextBox)GridView1.Rows[i].FindControl("txtLokasi");
                        TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                        TextBox txtPartNo = (TextBox)GridView1.Rows[i].FindControl("txtPartNo");
                        if (txtPartNo.Text.IndexOf("-S-") != -1)
                        {
                            DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                            return;
                        }
                        int itemidser = 0;
                        //dest.GetPartnoID(LPartno.Text);
                        if (Convert.ToInt32(txtqtyju.Text) > 0)
                        {
                            itemidser = serahFacade.GetPartnoID(txtPartNo.Text);
                            serah4.PartnoDest = LPartno.Text;
                            serah4.ItemIDSer = itemidser;
                            serah4.QtyIn = Convert.ToInt32(txtqtyju.Text);
                            serah4.LokasiID = dest.GetLokID(txtloklari.Text);
                            if (serah4.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtloklari.Text + " belum tersedia!");
                                return;
                            }
                            serah4.DestID = jemur.DestID;
                            serah4.ItemIDDest = itemidser;
                            serah4.HPP = jemur.HPP;
                            serah4.JemurID = jemur.ID;
                            serah4.CreatedBy = users.UserName;
                            serah4.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah4.SFrom = "lari";
                            arrserah.Add(serah4);
                            if (serah4.ItemIDSer == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "PartNo Pelarian :" + txtDateSerah.Text + " belum tersedia!");
                                return;
                            }
                            i++;
                        }
                    }
                    #endregion pelarian
                    T1_SerahProcessFacade T1SerahProcessFacade = new T1_SerahProcessFacade(jemur, arrserah);
                    string strError = string.Empty;
                    if (RB4Mili.Checked == true)
                        strError = T1SerahProcessFacade.Insert4mili();
                    if (RB1000.Checked == true)
                        strError = T1SerahProcessFacade.Insert1020();
                    if (RB9Mili.Checked == true)
                        strError = T1SerahProcessFacade.InsertLisplank();
                    ddlOven.Visible = false;
                    Loven.Visible = false;
                    if (strError == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Data tersimpan");
                    }
                    else
                    {
                        btnTansfer.Disabled = false;
                        DisplayAJAXMessage(this, "Rekam data error, coba lagi !");
                        return;
                    }
                }
            }
        }
        protected void SimpanPanel()
        {
            string test = txtQtyKW.Text;
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            T1_JemurFacade jemurFacade = new T1_JemurFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            Users users = (Users)Session["Users"];
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToDateTime(txtDateSerah.Text).Year;
            int Bulan = Convert.ToDateTime(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            btnTansfer.Disabled = true;
            #region validasi data
            int i = 0;
            int totlari = 0;
            int checktrans = 0;
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtPartnoOK.Text == string.Empty) { btnTansfer.Disabled = false; return; }
            foreach (T1_Jemur pelarian in arrPelarian)
            {
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                i++;
            }
            if (RB4Mili.Checked == false)
            {
                txtPartnoOK.Text = txtPartnoPOK.Text;
                txtPartnoKW.Text = txtPartnoPOK.Text;
                txtPartnoBPF.Text = txtPartnoPBP.Text;
            }
            if (txtPartnoOK.Text.IndexOf("-S-") != -1 || txtPartnoKW.Text.IndexOf("-S-") != -1 || txtPartnoBPF.Text.IndexOf("-S-") != -1)
            {
                DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                return;
            }
            if (txtQtyOK.Text.Trim() == string.Empty)
                txtQtyOK.Text = "0";
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtQtyBPU.Text.Trim() == string.Empty)
                txtQtyBPU.Text = "0";
            if (txtQtyBPS.Text.Trim() == string.Empty)
                txtQtyBPS.Text = "0";

            if (LQtyIn.Text.Trim() != string.Empty && txtQtyOK.Text.Trim() != string.Empty && txtQtyBPF.Text.Trim() != string.Empty
                && txtQtyBPU.Text.Trim() != string.Empty && txtQtyBPS.Text.Trim() != string.Empty)
            {
                if (RBLisflank.Checked == true)
                    //txtQtyKW.Text = "0";
                    checktrans = Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - (Convert.ToInt32(txtQtyOK.Text) + Convert.ToInt32(txtQtyBPF.Text) +
                    Convert.ToInt32(txtQtyBPU.Text) + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (RB1000.Checked == true)
                    checktrans = Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) -
                        (Convert.ToInt32(txtQtyKW1.Text) / 2) - ((Convert.ToInt32(txtQtyOK1.Text) / 2)
                        + (Convert.ToInt32(txtQtyBPF0.Text) / 2) + (Convert.ToInt32(txtQtyBPU0.Text) / 2)
                        + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (checktrans != 0)
                {
                    DisplayAJAXMessage(this, "Quantity Serah Error");
                    if (RB1000.Checked == true)
                        txtPartnoOK1.Focus();
                    else
                        txtPartnoOK.Focus();
                    jemurFacade.UpdateFail(Convert.ToInt32(LID.Text));
                    LoadDataJemur();
                    btnTansfer.Disabled = false;
                    return;
                }
                #endregion validasi data
                else
                {
                    #region rekam data serah
                    T1_Jemur jemur = new T1_Jemur();
                    FC_Items items = new FC_Items();
                    FC_Items items1 = new FC_Items();
                    FC_ItemsFacade itemsF = new FC_ItemsFacade();
                    ArrayList arrserah = new ArrayList();
                    FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
                    jemur = jemurFacade.RetrieveJemurByID(LID.Text);
                    items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                    if (items.ID == 0)
                    {
                        items = new FC_Items();
                        items.ItemTypeID = 3;
                        items.Kode = txtPartnoOK.Text.Substring(0, 3);
                        items.Tebal = decimal.Parse(txtPartnoOK.Text.Substring(6, 3)) / 10;
                        items.Lebar = int.Parse(txtPartnoOK.Text.Substring(9, 4));
                        items.Panjang = int.Parse(txtPartnoOK.Text.Substring(13, 4));
                        items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                        items.Partno = txtPartnoOK.Text;
                        items.GroupID = 0;
                        itemsF.Insert(items);
                        items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                    }
                    items1 = itemsfacade.RetrieveByPartNo(LPartno.Text);
                    jemur.ItemID0 = dest.GetPartnoID(LPartno.Text);
                    jemur.LokasiID0 = dest.GetLokID(LLokasi.Text);
                    jemur.CreatedBy = users.UserName;
                    jemur.Oven = ddlOven.SelectedItem.Text;
                    jemur.TglJemur = Convert.ToDateTime(txtDateSerah.Text);
                    int ItemIDDest = items1.ID;
                    if (txtPartnoOK.Text != string.Empty && txtQtyOK.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyOK.Text) > 0)
                        {
                            T1_Serah serah = new T1_Serah();
                            serah.PartnoSer = txtPartnoOK.Text;
                            serah.PartnoDest = LPartno.Text;
                            serah.ItemIDSer = items.ID;
                            if (serah.ItemIDSer == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, txtPartnoOK.Text + " belum tersedia!");
                                return;
                            }
                            serah.QtyIn = int.Parse(txtQtyOK.Text);
                            serah.LokasiID = dest.GetLokID(txtlokOK.Text);
                            if (serah.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokOK.Text + "belum tersedia!");
                                return;
                            }
                            if (txtlokOK1.Text == string.Empty)
                                txtlokOK1.Text = txtlokOK.Text;
                            serah.LokasiSer = txtlokOK1.Text;
                            serah.DestID = jemur.DestID;
                            serah.ItemIDDest = ItemIDDest;
                            serah.HPP = jemur.HPP * ((items.Lebar * items.Panjang) / jemur.LuasA);
                            serah.JemurID = jemur.ID;
                            serah.CreatedBy = users.UserName;
                            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah.SFrom = "jemur";
                            arrserah.Add(serah);
                        }
                    }
                    if (txtPartnoKW.Text != string.Empty && txtQtyKW.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyKW.Text) > 0)
                        {
                            T1_Serah serah = new T1_Serah();
                            items = itemsfacade.RetrieveByPartNo(txtPartnoKW.Text);
                            serah.ItemIDSer = items.ID;
                            if (serah.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoKW.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoKW.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoKW.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoKW.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoKW.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoKW.Text);
                            }
                            serah.PartnoDest = LPartno.Text;
                            serah.QtyIn = int.Parse(txtQtyKW.Text);
                            serah.LokasiID = dest.GetLokID(txtlokKW.Text);
                            if (serah.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokKW.Text + " belum tersedia!");
                                return;
                            }
                            serah.PartnoSer = txtPartnoKW.Text;
                            serah.LokasiSer = txtlokKW1.Text;
                            serah.DestID = jemur.DestID;
                            serah.ItemIDDest = ItemIDDest;
                            serah.HPP = jemur.HPP * ((items.Lebar * items.Panjang) / jemur.LuasA);
                            serah.JemurID = jemur.ID;
                            serah.CreatedBy = users.UserName;
                            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah.SFrom = "jemur";
                            arrserah.Add(serah);
                        }
                    }
                    if (txtPartnoBPF.Text != string.Empty && txtQtyBPF.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPF.Text) > 0)
                        {
                            T1_Serah serah1 = new T1_Serah();
                            serah1.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPF.Text);
                            if (serah1.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPF.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPF.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPF.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPF.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPF.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPF.Text);
                            }
                            serah1.PartnoDest = LPartno.Text;
                            serah1.QtyIn = int.Parse(txtQtyBPF.Text);
                            serah1.QtyOut = int.Parse(txtQtyBPF.Text);
                            serah1.LokasiID = dest.GetLokID(txtlokBPF.Text);
                            if (serah1.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi BP Finish belum tersedia!");
                                return;
                            }
                            serah1.PartnoSer = txtPartnoBPF.Text;
                            serah1.LokasiSer = txtlokBPF.Text;
                            serah1.DestID = jemur.DestID;
                            serah1.ItemIDDest = ItemIDDest;
                            serah1.HPP = jemur.HPP;
                            serah1.JemurID = jemur.ID;
                            serah1.CreatedBy = users.UserName;
                            serah1.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah1.SFrom = "jemur";
                            arrserah.Add(serah1);
                        }
                    }
                    if (txtPartnoBPU.Text != string.Empty && txtQtyBPU.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPU.Text) > 0)
                        {
                            T1_Serah serah2 = new T1_Serah();
                            serah2.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPU.Text);
                            if (serah2.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPU.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPU.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPU.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPU.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPU.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPU.Text);
                            }
                            serah2.PartnoDest = LPartno.Text;
                            serah2.PartnoSer = txtPartnoBPU0.Text;
                            serah2.QtyIn = int.Parse(txtQtyBPU.Text);
                            serah2.LokasiID = dest.GetLokID(txtlokBPU.Text);
                            if (serah2.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPU.Text + " belum tersedia!");
                                return;
                            }
                            serah2.LokasiSer = txtlokBPU0.Text;
                            serah2.DestID = jemur.DestID;
                            serah2.ItemIDDest = ItemIDDest;
                            serah2.HPP = jemur.HPP;
                            serah2.JemurID = jemur.ID;
                            serah2.CreatedBy = users.UserName;
                            serah2.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah2.SFrom = "jemur";
                            arrserah.Add(serah2);
                        }
                    }
                    if (txtPartnoBPS.Text != string.Empty && txtQtyBPS.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPS.Text) > 0)
                        {
                            T1_Serah serah3 = new T1_Serah();
                            serah3.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPS.Text);
                            if (serah3.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 1;
                                items.Kode = txtPartnoBPS.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPS.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPS.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPS.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPS.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPS.Text);
                            }
                            serah3.PartnoDest = LPartno.Text;
                            serah3.PartnoSer = txtPartnoBPS.Text;
                            serah3.QtyIn = int.Parse(txtQtyBPS.Text);
                            serah3.LokasiID = dest.GetLokID(txtlokBPS.Text);
                            if (serah3.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPS.Text + " tersedia!");
                                return;
                            }
                            serah3.DestID = jemur.DestID;
                            serah3.ItemIDDest = ItemIDDest;
                            serah3.HPP = jemur.HPP;
                            serah3.JemurID = jemur.ID;
                            serah3.CreatedBy = users.UserName;
                            serah3.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah3.SFrom = "jemur";
                            arrserah.Add(serah3);
                        }
                    }
                    #endregion rekam data serah
                    #region pelarian
                    i = 0;
                    foreach (T1_Jemur pelarian in arrPelarian)
                    {
                        T1_Serah serah4 = new T1_Serah();
                        TextBox txtloklari = (TextBox)GridView1.Rows[i].FindControl("txtLokasi");
                        TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                        TextBox txtPartNo = (TextBox)GridView1.Rows[i].FindControl("txtPartNo");
                        if (txtPartNo.Text.IndexOf("-S-") != -1)
                        {
                            DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                            return;
                        }
                        int itemidser = 0;
                        //dest.GetPartnoID(LPartno.Text);
                        if (Convert.ToInt32(txtqtyju.Text) > 0)
                        {
                            itemidser = serahFacade.GetPartnoID(txtPartNo.Text);
                            serah4.PartnoDest = LPartno.Text;
                            serah4.ItemIDSer = itemidser;
                            serah4.QtyIn = Convert.ToInt32(txtqtyju.Text);
                            serah4.LokasiID = dest.GetLokID(txtloklari.Text);
                            if (serah4.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtloklari.Text + " belum tersedia!");
                                return;
                            }
                            serah4.DestID = jemur.DestID;
                            serah4.ItemIDDest = itemidser;
                            serah4.HPP = jemur.HPP;
                            serah4.JemurID = jemur.ID;
                            serah4.CreatedBy = users.UserName;
                            serah4.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah4.SFrom = "lari";
                            arrserah.Add(serah4);
                            if (serah4.ItemIDSer == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "PartNo Pelarian :" + txtDateSerah.Text + " belum tersedia!");
                                return;
                            }
                            i++;
                        }
                    }
                    #endregion pelarian
                    T1_SerahProcessFacade T1SerahProcessFacade = new T1_SerahProcessFacade(jemur, arrserah);
                    string strError = string.Empty;

                    if (RB9Mili.Checked == true)
                        strError = T1SerahProcessFacade.InsertLisplank();
                    ddlOven.Visible = false;
                    Loven.Visible = false;
                    if (strError == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Data tersimpan");
                    }
                    else
                    {
                        btnTansfer.Disabled = false;
                        DisplayAJAXMessage(this, "Rekam data error, coba lagi !");
                        return;
                    }
                }
            }
        }
        protected void SimpanListPlank()
        {
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            T1_JemurFacade jemurFacade = new T1_JemurFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            Users users = (Users)Session["Users"];
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToDateTime(txtDateSerah.Text).Year;
            int Bulan = Convert.ToDateTime(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            btnTansfer.Disabled = true;
            #region validasi data
            int i = 0;
            int totlari = 0;
            int checktrans = 0;
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtPartnoOK.Text == string.Empty) { btnTansfer.Disabled = false; return; }
            foreach (T1_Jemur pelarian in arrPelarian)
            {
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                i++;
            }
            if (RB4Mili.Checked == false)
            {
                txtPartnoOK.Text = txtPartnoPOK.Text;
                txtPartnoKW.Text = txtPartnoPOK.Text;
                txtPartnoBPF.Text = txtPartnoPBP.Text;
            }
            if (txtPartnoOK.Text.IndexOf("-S-") != -1 || txtPartnoKW.Text.IndexOf("-S-") != -1 || txtPartnoBPF.Text.IndexOf("-S-") != -1)
            {
                DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                return;
            }
            if (txtQtyOK.Text.Trim() == string.Empty)
                txtQtyOK.Text = "0";
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtQtyBPU.Text.Trim() == string.Empty)
                txtQtyBPU.Text = "0";
            if (txtQtyBPS.Text.Trim() == string.Empty)
                txtQtyBPS.Text = "0";

            if (LQtyIn.Text.Trim() != string.Empty && txtQtyOK.Text.Trim() != string.Empty && txtQtyBPF.Text.Trim() != string.Empty
                && txtQtyBPU.Text.Trim() != string.Empty && txtQtyBPS.Text.Trim() != string.Empty)
            {
                checktrans = Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - (Convert.ToInt32(txtQtyOK.Text) + Convert.ToInt32(txtQtyBPF.Text) +
                Convert.ToInt32(txtQtyBPU.Text) + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (RB1000.Checked == true)
                    checktrans = Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) -
                        (Convert.ToInt32(txtQtyKW1.Text) / 2) - ((Convert.ToInt32(txtQtyOK1.Text) / 2)
                        + (Convert.ToInt32(txtQtyBPF0.Text) / 2) + (Convert.ToInt32(txtQtyBPU0.Text) / 2)
                        + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (checktrans != 0)
                {
                    DisplayAJAXMessage(this, "Quantity Serah Error");
                    if (RB1000.Checked == true)
                        txtPartnoOK1.Focus();
                    else
                        txtPartnoOK.Focus();
                    jemurFacade.UpdateFail(Convert.ToInt32(LID.Text));
                    LoadDataJemur();
                    btnTansfer.Disabled = false;
                    return;
                }
                #endregion validasi data
                else
                {
                    #region rekam data serah
                    T1_Jemur jemur = new T1_Jemur();
                    FC_Items items = new FC_Items();
                    FC_Items items1 = new FC_Items();
                    FC_ItemsFacade itemsF = new FC_ItemsFacade();
                    ArrayList arrserah = new ArrayList();
                    FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
                    jemur = jemurFacade.RetrieveJemurByID(LID.Text);
                    items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                    if (items.ID == 0)
                    {
                        items = new FC_Items();
                        items.ItemTypeID = 3;
                        items.Kode = txtPartnoOK.Text.Substring(0, 3);
                        items.Tebal = decimal.Parse(txtPartnoOK.Text.Substring(6, 3)) / 10;
                        items.Lebar = int.Parse(txtPartnoOK.Text.Substring(9, 4));
                        items.Panjang = int.Parse(txtPartnoOK.Text.Substring(13, 4));
                        items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                        items.Partno = txtPartnoOK.Text;
                        items.GroupID = 0;
                        itemsF.Insert(items);
                        items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                    }
                    items1 = itemsfacade.RetrieveByPartNo(LPartno.Text);
                    jemur.ItemID0 = dest.GetPartnoID(LPartno.Text);
                    jemur.LokasiID0 = dest.GetLokID(LLokasi.Text);
                    jemur.CreatedBy = users.UserName;
                    int ItemIDDest = items1.ID;
                    if (txtPartnoOK.Text != string.Empty && txtQtyOK.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyOK.Text) > 0)
                        {
                            T1_Serah serah = new T1_Serah();
                            serah.PartnoSer = txtPartnoOK.Text;
                            serah.PartnoDest = LPartno.Text;
                            serah.ItemIDSer = items.ID;
                            if (serah.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoOK.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoOK.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoOK.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoOK.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoOK.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                            }
                            serah.QtyIn = int.Parse(txtQtyOK.Text);
                            serah.LokasiID = dest.GetLokID(txtlokOK.Text);
                            if (serah.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokOK.Text + "belum tersedia!");
                                return;
                            }
                            serah.LokasiSer = txtlokOK1.Text;
                            serah.DestID = jemur.DestID;
                            serah.ItemIDDest = ItemIDDest;
                            serah.HPP = jemur.HPP * ((items.Lebar * items.Panjang) / jemur.LuasA);
                            serah.JemurID = jemur.ID;
                            serah.CreatedBy = users.UserName;
                            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah.SFrom = "listplank";
                            arrserah.Add(serah);
                        }
                    }
                    if (txtPartnoKW.Text != string.Empty && txtQtyKW.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyKW.Text) > 0)
                        {
                            T1_Serah serah = new T1_Serah();
                            items = itemsfacade.RetrieveByPartNo(txtPartnoKW.Text);
                            serah.ItemIDSer = items.ID;
                            if (serah.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoKW.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoKW.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoKW.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoKW.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoKW.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoKW.Text);
                            }
                            serah.PartnoDest = LPartno.Text;
                            serah.QtyIn = int.Parse(txtQtyKW.Text);
                            serah.LokasiID = dest.GetLokID(txtlokKW.Text);
                            if (serah.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokKW.Text + " belum tersedia!");
                                return;
                            }
                            serah.PartnoSer = txtPartnoKW.Text;
                            serah.LokasiSer = txtlokKW1.Text;
                            serah.DestID = jemur.DestID;
                            serah.ItemIDDest = ItemIDDest;
                            serah.HPP = jemur.HPP * ((items.Lebar * items.Panjang) / jemur.LuasA);
                            serah.JemurID = jemur.ID;
                            serah.CreatedBy = users.UserName;
                            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah.SFrom = "listplank";
                            arrserah.Add(serah);
                        }
                    }
                    if (txtPartnoBPF.Text != string.Empty && txtQtyBPF.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPF.Text) > 0)
                        {
                            T1_Serah serah1 = new T1_Serah();
                            serah1.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPF.Text);
                            if (serah1.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPF.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPF.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPF.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPF.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoKW.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPF.Text);
                            }
                            serah1.PartnoDest = LPartno.Text;
                            serah1.QtyIn = int.Parse(txtQtyBPF.Text);
                            serah1.LokasiID = dest.GetLokID(txtlokBPF.Text);
                            if (serah1.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi BP Finish belum tersedia!");
                                return;
                            }
                            serah1.PartnoSer = txtPartnoBPF.Text;
                            serah1.LokasiSer = txtlokBPF0.Text;
                            serah1.DestID = jemur.DestID;
                            serah1.ItemIDDest = ItemIDDest;
                            serah1.HPP = jemur.HPP;
                            serah1.JemurID = jemur.ID;
                            serah1.CreatedBy = users.UserName;
                            serah1.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah1.SFrom = "listplank";
                            arrserah.Add(serah1);
                        }
                    }
                    if (txtPartnoBPU.Text != string.Empty && txtQtyBPU.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPU.Text) > 0)
                        {
                            T1_Serah serah2 = new T1_Serah();
                            serah2.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPU.Text);
                            if (serah2.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPU.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPU.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPU.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPU.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPU.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPU.Text);
                            }
                            serah2.PartnoDest = LPartno.Text;
                            serah2.PartnoSer = txtPartnoBPU0.Text;
                            serah2.QtyIn = int.Parse(txtQtyBPU.Text);
                            serah2.LokasiID = dest.GetLokID(txtlokBPU.Text);
                            if (serah2.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPU.Text + " belum tersedia!");
                                return;
                            }
                            serah2.LokasiSer = txtlokBPU0.Text;
                            serah2.DestID = jemur.DestID;
                            serah2.ItemIDDest = ItemIDDest;
                            serah2.HPP = jemur.HPP;
                            serah2.JemurID = jemur.ID;
                            serah2.CreatedBy = users.UserName;
                            serah2.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah2.SFrom = "listplank";
                            arrserah.Add(serah2);
                        }
                    }
                    if (txtPartnoBPS.Text != string.Empty && txtQtyBPS.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPS.Text) > 0)
                        {
                            T1_Serah serah3 = new T1_Serah();
                            serah3.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPS.Text);
                            if (serah3.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 1;
                                items.Kode = txtPartnoBPS.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPS.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPS.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPS.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPS.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPS.Text);
                            }
                            serah3.PartnoDest = LPartno.Text;
                            serah3.PartnoSer = txtPartnoBPS.Text;
                            serah3.QtyIn = int.Parse(txtQtyBPS.Text);
                            serah3.LokasiID = dest.GetLokID(txtlokBPS.Text);
                            if (serah3.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPS.Text + " tersedia!");
                                return;
                            }
                            serah3.DestID = jemur.DestID;
                            serah3.ItemIDDest = ItemIDDest;
                            serah3.HPP = jemur.HPP;
                            serah3.JemurID = jemur.ID;
                            serah3.CreatedBy = users.UserName;
                            serah3.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah3.SFrom = "listplank";
                            arrserah.Add(serah3);
                        }
                    }
                    #endregion rekam data serah
                    #region pelarian
                    i = 0;
                    foreach (T1_Jemur pelarian in arrPelarian)
                    {
                        T1_Serah serah4 = new T1_Serah();
                        TextBox txtloklari = (TextBox)GridView1.Rows[i].FindControl("txtLokasi");
                        TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                        TextBox txtPartNo = (TextBox)GridView1.Rows[i].FindControl("txtPartNo");
                        if (txtPartNo.Text.IndexOf("-S-") != -1)
                        {
                            DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                            return;
                        }
                        int itemidser = 0;
                        //dest.GetPartnoID(LPartno.Text);
                        if (Convert.ToInt32(txtqtyju.Text) > 0)
                        {
                            itemidser = serahFacade.GetPartnoID(txtPartNo.Text);
                            serah4.PartnoDest = LPartno.Text;
                            serah4.ItemIDSer = itemidser;
                            serah4.QtyIn = Convert.ToInt32(txtqtyju.Text);
                            serah4.LokasiID = dest.GetLokID(txtloklari.Text);
                            if (serah4.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtloklari.Text + " belum tersedia!");
                                return;
                            }
                            serah4.DestID = jemur.DestID;
                            serah4.ItemIDDest = itemidser;
                            serah4.HPP = jemur.HPP;
                            serah4.JemurID = jemur.ID;
                            serah4.CreatedBy = users.UserName;
                            serah4.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah4.SFrom = "lari";
                            arrserah.Add(serah4);
                            if (serah4.ItemIDSer == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "PartNo Pelarian :" + txtDateSerah.Text + " belum tersedia!");
                                return;
                            }
                            i++;
                        }
                    }
                    #endregion pelarian
                    T1_SerahProcessFacade T1SerahProcessFacade = new T1_SerahProcessFacade(jemur, arrserah);
                    string strError = string.Empty;

                    if (RB9Mili.Checked == true && Convert.ToInt32(txtQtyKW.Text) > 0)
                        strError = T1SerahProcessFacade.InsertLisplank1();

                    if (strError == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Data tersimpan");
                        //clearform();
                        //btnTansfer.Disabled = false;
                    }
                    else
                    {
                        btnTansfer.Disabled = false;
                        DisplayAJAXMessage(this, "Rekam data error, coba lagi !");
                        return;
                    }
                }
            }
        }
        protected void SimpanListPlankPanel()
        {
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            T1_JemurFacade jemurFacade = new T1_JemurFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            Users users = (Users)Session["Users"];
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToDateTime(txtDateSerah.Text).Year;
            int Bulan = Convert.ToDateTime(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            btnTansfer.Disabled = true;
            #region validasi data
            int i = 0;
            int totlari = 0;
            int checktrans = 0;
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtPartnoOK.Text == string.Empty) { btnTansfer.Disabled = false; return; }
            foreach (T1_Jemur pelarian in arrPelarian)
            {
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                i++;
            }
            if (RB4Mili.Checked == false)
            {
                txtPartnoOK.Text = txtPartnoPOK.Text;
                txtPartnoKW.Text = txtPartnoPOK.Text;
                txtPartnoBPF.Text = txtPartnoPBP.Text;
            }
            if (txtPartnoOK.Text.IndexOf("-S-") != -1 || txtPartnoKW.Text.IndexOf("-S-") != -1 || txtPartnoBPF.Text.IndexOf("-S-") != -1)
            {
                DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                return;
            }
            if (txtQtyOK.Text.Trim() == string.Empty)
                txtQtyOK.Text = "0";
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtQtyBPU.Text.Trim() == string.Empty)
                txtQtyBPU.Text = "0";
            if (txtQtyBPS.Text.Trim() == string.Empty)
                txtQtyBPS.Text = "0";

            if (LQtyIn.Text.Trim() != string.Empty && txtQtyOK.Text.Trim() != string.Empty && txtQtyBPF.Text.Trim() != string.Empty
                && txtQtyBPU.Text.Trim() != string.Empty && txtQtyBPS.Text.Trim() != string.Empty)
            {
                checktrans = Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - (Convert.ToInt32(txtQtyOK.Text) + Convert.ToInt32(txtQtyBPF.Text) +
                Convert.ToInt32(txtQtyBPU.Text) + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (RB1000.Checked == true)
                    checktrans = Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) -
                        (Convert.ToInt32(txtQtyKW1.Text) / 2) - ((Convert.ToInt32(txtQtyOK1.Text) / 2)
                        + (Convert.ToInt32(txtQtyBPF0.Text) / 2) + (Convert.ToInt32(txtQtyBPU0.Text) / 2)
                        + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (checktrans != 0)
                {
                    DisplayAJAXMessage(this, "Quantity Serah Error");
                    if (RB1000.Checked == true)
                        txtPartnoOK1.Focus();
                    else
                        txtPartnoOK.Focus();
                    jemurFacade.UpdateFail(Convert.ToInt32(LID.Text));
                    LoadDataJemur();
                    btnTansfer.Disabled = false;
                    return;
                }
                #endregion validasi data
                else
                {
                    #region rekam data serah
                    T1_Jemur jemur = new T1_Jemur();
                    FC_Items items = new FC_Items();
                    FC_Items items1 = new FC_Items();
                    FC_ItemsFacade itemsF = new FC_ItemsFacade();
                    ArrayList arrserah = new ArrayList();
                    FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
                    jemur = jemurFacade.RetrieveJemurByID(LID.Text);

                    items1 = itemsfacade.RetrieveByPartNo(LPartno.Text);
                    int ItemIDDest = items1.ID;
                    if (txtPartnoBPU.Text != string.Empty && txtQtyBPU.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPU.Text) > 0)
                        {
                            T1_Serah serah2 = new T1_Serah();
                            serah2.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPU.Text);
                            if (serah2.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPU.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPU.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPU.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPU.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPU.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPU.Text);
                            }
                            serah2.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPU.Text);
                            serah2.PartnoDest = LPartno.Text;
                            serah2.PartnoSer = txtPartnoBPU0.Text;
                            serah2.QtyIn = int.Parse(txtQtyBPU.Text);
                            serah2.LokasiID = dest.GetLokID(txtlokBPU.Text);
                            if (serah2.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPU.Text + " belum tersedia!");
                                return;
                            }
                            serah2.LokasiSer = txtlokBPU0.Text;
                            serah2.DestID = jemur.DestID;
                            serah2.ItemIDDest = ItemIDDest;
                            serah2.HPP = jemur.HPP;
                            serah2.JemurID = jemur.ID;
                            serah2.CreatedBy = users.UserName;
                            serah2.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah2.SFrom = "listplank";
                            arrserah.Add(serah2);
                        }
                    }

                    #endregion rekam data serah

                    T1_SerahProcessFacade T1SerahProcessFacade = new T1_SerahProcessFacade(jemur, arrserah);
                    string strError = string.Empty;
                    if (RB9Mili.Checked == true)
                        strError = T1SerahProcessFacade.InsertLisplank1();
                }
            }
        }
        protected void SimpanListPlankPanelR1()
        {
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            T1_JemurFacade jemurFacade = new T1_JemurFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            Users users = (Users)Session["Users"];
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToDateTime(txtDateSerah.Text).Year;
            int Bulan = Convert.ToDateTime(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            btnTansfer.Disabled = true;
            #region validasi data
            int i = 0;
            int totlari = 0;
            int checktrans = 0;
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtPartnoOK.Text == string.Empty) { btnTansfer.Disabled = false; return; }
            foreach (T1_Jemur pelarian in arrPelarian)
            {
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                i++;
            }
            if (RB4Mili.Checked == false)
            {
                txtPartnoOK.Text = txtPartnoPOK.Text;
                txtPartnoKW.Text = txtPartnoPOK.Text;
                txtPartnoBPF.Text = txtPartnoPBP.Text;
            }
            if (txtPartnoOK.Text.IndexOf("-S-") != -1 || txtPartnoKW.Text.IndexOf("-S-") != -1 || txtPartnoBPF.Text.IndexOf("-S-") != -1)
            {
                DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                return;
            }
            if (txtQtyOK.Text.Trim() == string.Empty)
                txtQtyOK.Text = "0";
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtQtyBPU.Text.Trim() == string.Empty)
                txtQtyBPU.Text = "0";
            if (txtQtyBPS.Text.Trim() == string.Empty)
                txtQtyBPS.Text = "0";

            if (LQtyIn.Text.Trim() != string.Empty && txtQtyOK.Text.Trim() != string.Empty && txtQtyBPF.Text.Trim() != string.Empty
                && txtQtyBPU.Text.Trim() != string.Empty && txtQtyBPS.Text.Trim() != string.Empty)
            {
                checktrans = Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - (Convert.ToInt32(txtQtyOK.Text) + Convert.ToInt32(txtQtyBPF.Text) +
                Convert.ToInt32(txtQtyBPU.Text) + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (RB1000.Checked == true)
                    checktrans = Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) -
                        (Convert.ToInt32(txtQtyKW1.Text) / 2) - ((Convert.ToInt32(txtQtyOK1.Text) / 2)
                        + (Convert.ToInt32(txtQtyBPF.Text) / 2) + (Convert.ToInt32(txtQtyBPU.Text) / 2)
                        + Convert.ToInt32(txtQtyBPS.Text) + totlari);
                if (checktrans != 0)
                {
                    DisplayAJAXMessage(this, "Quantity Serah Error");
                    if (RB1000.Checked == true)
                        txtPartnoOK1.Focus();
                    else
                        txtPartnoOK.Focus();
                    jemurFacade.UpdateFail(Convert.ToInt32(LID.Text));
                    LoadDataJemur();
                    btnTansfer.Disabled = false;
                    return;
                }
                #endregion validasi data
                else
                {
                    #region rekam data serah
                    T1_Jemur jemur = new T1_Jemur();
                    FC_Items items = new FC_Items();
                    FC_Items items1 = new FC_Items();
                    FC_ItemsFacade itemsF = new FC_ItemsFacade();
                    ArrayList arrserah = new ArrayList();
                    FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
                    jemur = jemurFacade.RetrieveJemurByID(LID.Text);

                    items1 = itemsfacade.RetrieveByPartNo(LPartno.Text);
                    int ItemIDDest = items1.ID;
                    if (txtPartnoBPF.Text != string.Empty && txtQtyBPF.Text != string.Empty)
                    {
                        if (Convert.ToInt32(txtQtyBPF.Text) > 0)
                        {
                            T1_Serah serah2 = new T1_Serah();
                            serah2.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPF.Text);
                            if (serah2.ItemIDSer == 0)
                            {
                                items = new FC_Items();
                                items.ItemTypeID = 3;
                                items.Kode = txtPartnoBPF.Text.Substring(0, 3);
                                items.Tebal = decimal.Parse(txtPartnoBPF.Text.Substring(6, 3)) / 10;
                                items.Lebar = int.Parse(txtPartnoBPF.Text.Substring(9, 4));
                                items.Panjang = int.Parse(txtPartnoBPF.Text.Substring(13, 4));
                                items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                                items.Partno = txtPartnoBPF.Text;
                                items.GroupID = 0;
                                itemsF.Insert(items);
                                items = itemsfacade.RetrieveByPartNo(txtPartnoBPF.Text);
                            }
                            serah2.ItemIDSer = serahFacade.GetPartnoID(txtPartnoBPF.Text);
                            serah2.PartnoDest = LPartno.Text;
                            serah2.PartnoSer = txtPartnoBPF.Text;
                            serah2.QtyIn = int.Parse(txtQtyBPF.Text);
                            serah2.LokasiID = dest.GetLokID(txtlokBPF.Text);
                            if (serah2.LokasiID == 0)
                            {
                                btnTansfer.Disabled = false;
                                DisplayAJAXMessage(this, "Lokasi : " + txtlokBPU.Text + " belum tersedia!");
                                return;
                            }
                            serah2.LokasiSer = txtlokBPF.Text;
                            serah2.DestID = jemur.DestID;
                            serah2.ItemIDDest = ItemIDDest;
                            serah2.HPP = jemur.HPP;
                            serah2.JemurID = jemur.ID;
                            serah2.CreatedBy = users.UserName;
                            serah2.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
                            serah2.SFrom = "listplank";
                            arrserah.Add(serah2);
                        }
                    }

                    #endregion rekam data serah

                    T1_SerahProcessFacade T1SerahProcessFacade = new T1_SerahProcessFacade(jemur, arrserah);
                    string strError = string.Empty;
                    if (RB9Mili.Checked == true)
                        strError = T1SerahProcessFacade.InsertLisplankR1();
                }
            }
        }
        protected int cekpartno(string partno)
        {
            int cek = 0;
            FC_Items itemsT = new FC_Items();
            FC_ItemsFacade itemsTF = new FC_ItemsFacade();
            itemsT = itemsTF.RetrieveByPartNo(partno);
            if (itemsT.ID == 0)
            {
                return 1;
            }

            return cek;
        }
        protected int ceklok(string lokasi)
        {
            int cek = 0;

            FC_Lokasi lokT = new FC_Lokasi();
            FC_LokasiFacade lokTF = new FC_LokasiFacade();
            lokT = lokTF.RetrieveByLokasi(lokasi);
            if (lokT.ID == 0)
            {
                return 1;
            }
            return cek;
        }
        protected void Simetrislvl2LisPlankNew()
        {
            string strError = string.Empty;
            int intresult = 0;
            Users users = (Users)Session["Users"];
            ArrayList arrrekapK = new ArrayList();
            ArrayList arrrekapT = new ArrayList();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            T3_Simetris simetris = new T3_Simetris();

            if (txtQtyAsal.Text == string.Empty)
                txtQtyAsal.Text = "0";
            if (txtQtyPOK.Text == string.Empty)
                txtQtyPOK.Text = "0";
            if (txtQtyPBP.Text == string.Empty)
                txtQtyPBP.Text = "0";
            int cek = 0;
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            if (Convert.ToInt32(txtQtyAsal.Text) > 0)
            {
                cek = cekpartno(txtPartnoAsal.Text);
                if (cek > 0)
                {
                    items = new FC_Items();
                    items.ItemTypeID = 3;
                    items.Kode = txtPartnoAsal.Text.Substring(0, 3);
                    items.Tebal = decimal.Parse(txtPartnoAsal.Text.Substring(6, 3)) / 10;
                    items.Lebar = int.Parse(txtPartnoAsal.Text.Substring(9, 4));
                    items.Panjang = int.Parse(txtPartnoAsal.Text.Substring(13, 4));
                    items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                    items.Partno = txtPartnoAsal.Text;
                    items.ItemDesc = "LIST PLANK";
                    items.GroupID = 0;
                    itemsF.Insert(items);

                    //DisplayAJAXMessage(this, "Partno : " + txtPartnoAsal.Text + " belum tersedia");
                    //return;
                }
                cek = ceklok(txtlokAsal.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokAsal.Text + " belum tersedia");
                    return;
                }
            }
            if (Convert.ToInt32(txtQtyPOK.Text) > 0)
            {
                cek = cekpartno(txtPartnoPOK.Text);
                if (cek > 0)
                {
                    items = new FC_Items();
                    items.ItemTypeID = 3;
                    items.Kode = txtPartnoPOK.Text.Substring(0, 3);
                    items.Tebal = decimal.Parse(txtPartnoPOK.Text.Substring(6, 3)) / 10;
                    items.Lebar = int.Parse(txtPartnoPOK.Text.Substring(9, 4));
                    items.Panjang = int.Parse(txtPartnoPOK.Text.Substring(13, 4));
                    items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                    items.Partno = txtPartnoPOK.Text;
                    items.ItemDesc = "LIST PLANK";
                    items.GroupID = 0;
                    itemsF.Insert(items);
                    //DisplayAJAXMessage(this, "Partno : " + txtPartnoPOK.Text + " belum tersedia");
                    //return;
                }
                cek = ceklok(txtlokPOK.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokPOK.Text + " belum tersedia");
                    return;
                }
            }
            if (Convert.ToInt32(txtQtyPBP.Text) > 0)
            {
                cek = cekpartno(txtPartnoPBP.Text);
                if (cek > 0)
                {
                    items = new FC_Items();
                    items.ItemTypeID = 3;
                    items.Kode = txtPartnoPBP.Text.Substring(0, 3);
                    items.Tebal = decimal.Parse(txtPartnoPBP.Text.Substring(6, 3)) / 10;
                    items.Lebar = int.Parse(txtPartnoPBP.Text.Substring(9, 4));
                    items.Panjang = int.Parse(txtPartnoPBP.Text.Substring(13, 4));
                    items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
                    items.Partno = txtPartnoPBP.Text;
                    items.ItemDesc = "LIST PLANK";
                    items.GroupID = 0;
                    itemsF.Insert(items);
                    //DisplayAJAXMessage(this, "Partno : " + txtPartnoPBP.Text + " belum tersedia");
                    //return;
                }
                cek = ceklok(txtlokPBP.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokPBP.Text + " belum tersedia");
                    return;
                }
            }

            #region Verifikasi Closing Periode
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DateTime.Parse(txtDateSerah.Text).Year;
            int Bulan = DateTime.Parse(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion

            FC_Items itemsK = new FC_Items();
            FC_ItemsFacade itemsKF = new FC_ItemsFacade();
            itemsK = itemsKF.RetrieveByPartNo(txtPartnoAsal.Text);

            FC_Lokasi lokK = new FC_Lokasi();
            FC_LokasiFacade lokKF = new FC_LokasiFacade();
            lokK = lokKF.RetrieveByLokasi(txtlokAsal.Text);

            FC_Items itemsT = new FC_Items();
            FC_ItemsFacade itemsTF = new FC_ItemsFacade();
            itemsT = itemsTF.RetrieveByPartNo(txtPartnoPOK.Text);

            FC_Lokasi lokT = new FC_Lokasi();
            FC_LokasiFacade lokTF = new FC_LokasiFacade();
            lokT = lokTF.RetrieveByLokasi(txtlokPOK.Text);

            int luas1 = itemsK.Panjang * itemsK.Lebar;
            int luas2 = itemsT.Panjang * itemsT.Lebar;
            //for (int i = 0; i <= GridViewTerima.Rows.Count - 1; i++)
            //{
            //TextBox txtQtyMutasi = (TextBox)GridViewTerima.Rows[i].FindControl("txtQtyMutasi");
            //CheckBox chkMutasi = (CheckBox)GridViewTerima.Rows[i].FindControl("chkMutasi");
            //if (chkMutasi.Checked == true)
            //{
            T3_RekapFacade rekapFacade = new T3_RekapFacade();//t3_rekap
            FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
            T3_Serah t3serahK = new T3_Serah();//Asal Partno
            T3_SerahFacade t3serahKF = new T3_SerahFacade();
            t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal.Text, txtlokAsal.Text);
            if (t3serahK.ItemID == 0)
            {
                t3serahK.Flag = "tambah";
                t3serahK.LokID = lokK.ID;
                t3serahK.ItemID = itemsK.ID;
                t3serahK.GroupID = itemsK.GroupID;
                t3serahK.Qty = 0;
                t3serahK.HPP = 0;
                t3serahK.CreatedBy = users.UserName;
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();
                AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahK);
                intresult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return;
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal.Text, txtlokAsal.Text);
            }
            int laststock1 = t3serahK.Qty;
            decimal lastAvgHPP1 = t3serahK.HPP;
            t3serahK.Flag = "kurang";
            t3serahK.Qty = Convert.ToInt32(txtQtyAsal.Text);
            t3serahK.CreatedBy = users.UserName;

            T3_Rekap rekapK = new T3_Rekap();//asal partno qtyout
            rekapK.DestID = Convert.ToInt32(LDestID.Text);
            rekapK.SerahID = t3serahK.ID;
            rekapK.T1serahID = 0;
            rekapK.GroupID = itemsK.GroupID;
            rekapK.LokasiID = t3serahK.LokID;
            rekapK.ItemIDSer = t3serahK.ItemID;
            rekapK.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
            rekapK.QtyInTrm = 0;
            rekapK.QtyOutTrm = Convert.ToInt32(txtQtyAsal.Text);
            rekapK.HPP = lastAvgHPP1;
            rekapK.CreatedBy = users.UserName;
            rekapK.Keterangan = txtPartnoPOK.Text;
            rekapK.SA = laststock1;
            rekapK.Process = "Simetris";
            rekapK.CutQty = Convert.ToInt32(txtQtyAsal.Text);
            rekapK.CutLevel = 2;
            rekapK.ID = 0;
            arrrekapK.Add(rekapK);

            //proses lokasi akhir
            /*dapatkan stock partno tujuan*/
            T3_Serah t3serahT = new T3_Serah();//terima Partno
            int laststock2 = t3serahT.Qty;
            T3_SerahFacade t3serahTF = new T3_SerahFacade();
            t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK.Text, txtlokPOK.Text);
            if (t3serahT.ItemID == 0)
            {
                t3serahT.Flag = "tambah";
                t3serahT.LokID = lokT.ID;
                t3serahT.ItemID = itemsT.ID;
                t3serahT.GroupID = itemsT.GroupID;
                t3serahT.Qty = 0;
                t3serahT.HPP = 0;
                t3serahT.CreatedBy = users.UserName;
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                transManager.BeginTransaction();
                AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                intresult = absTrans.Insert(transManager);
                if (absTrans.Error != string.Empty)
                {
                    transManager.RollbackTransaction();
                    return;
                }
                transManager.CommitTransaction();
                transManager.CloseConnection();
                t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK.Text, txtlokPOK.Text);
            }
            decimal lastAvgHPP2 = t3serahT.HPP;

            t3serahT.Flag = "tambah";
            t3serahT.ItemID = itemsT.ID;
            t3serahT.GroupID = itemsT.GroupID;
            t3serahT.ID = t3serahT.ID;
            t3serahT.LokID = lokT.ID;
            t3serahT.Qty = Convert.ToInt32(txtQtyAsal.Text) * pengali();
            decimal HPPnewItem = (luas2 / luas1) * lastAvgHPP1;
            t3serahT.HPP = HPPnewItem;
            t3serahT.CreatedBy = users.UserName;

            T3_Rekap rekapT = new T3_Rekap();//terima partno qtyin
            rekapT.DestID = Convert.ToInt32(LDestID.Text);
            rekapT.SerahID = t3serahK.ID;
            rekapT.GroupID = itemsT.GroupID;
            rekapT.T1serahID = 0;
            rekapT.LokasiID = lokT.ID;
            rekapT.ItemIDSer = itemsT.ID;
            rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
            rekapT.QtyInTrm = Convert.ToInt32(txtQtyAsal.Text) * pengali();
            rekapT.QtyOutTrm = 0;
            rekapT.HPP = HPPnewItem;
            rekapT.GroupID = 0;
            rekapT.CreatedBy = users.UserName;
            rekapT.Keterangan = txtPartnoAsal.Text;
            rekapT.Process = "Simetris";
            rekapT.SA = laststock2;
            rekapT.CutQty = 0;
            rekapT.CutLevel = 2;
            rekapT.ID = 0;
            arrrekapT.Add(rekapT);

            simetris.RekapID = intresult;
            simetris.SerahID = t3serahK.ID;
            simetris.LokasiID = t3serahT.LokID;
            simetris.TglSm = DateTime.Parse(txtDateSerah.Text).Date;
            simetris.ItemID = t3serahT.ItemID;
            simetris.QtyInSm = Convert.ToInt32(txtQtyAsal.Text);
            simetris.QtyOutSm = Convert.ToInt32(txtQtyPOK.Text) + Convert.ToInt32(txtQtyPBP.Text);
            simetris.GroupID = 0;
            simetris.CreatedBy = users.UserName;
            //}    
            //}

            //rekam table simetris
            T3_SimetrisAutoProcessFacade SimetrisProcessFacade = new T3_SimetrisAutoProcessFacade(arrrekapK, arrrekapT, simetris);
            strError = SimetrisProcessFacade.Insert1();
            if (strError == string.Empty)
            {
                //T3_RekapFacade t3rekapF = new T3_RekapFacade();
                //foreach (T3_Rekap rekapK in arrrekapK)
                //{
                //    t3rekapF.UpdateCutLevel2(rekapK.ID, rekapK.QtyOutTrm);
                //}
                if (Convert.ToInt32(txtQtyPBP.Text) > 0)
                {
                    Simetris(txtPartnoPOK.Text, txtlokPOK.Text, txtQtyPBP.Text, txtPartnoPBP.Text, txtlokPBP.Text, txtQtyPBP.Text, LDestID.Text, 2);
                }

                ClearPotong2();
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                DisplayAJAXMessage(this, "Data tersimpan");
            }
            txtnopalet.Text = string.Empty;
            //LoadDataGridTerima();
        }
        //protected void lvl2LisPlankT1()
        //{
        //    string strError = string.Empty;
        //    int intresult = 0;
        //    Users users = (Users)Session["Users"];
        //    string partnoOKT1=string.Empty ;
        //    string partnoBPT1 = string.Empty;
        //    string rekam = "gagal";
        //    BM_DestackingFacade dest = new BM_DestackingFacade();
        //    T1_ListPlank2 t1listplank2 = new T1_ListPlank2();
        //    T1_ListPlank t1listplank = new T1_ListPlank();
        //    T1_ListPlank2Facade t1listplank2F = new T1_ListPlank2Facade();
        //    #region validasi data
        //    if (txtQtyPOK.Text == string.Empty)
        //        txtQtyPOK.Text = "0";
        //    if (txtQtyPBP.Text == string.Empty)
        //        txtQtyPBP.Text = "0";
        //    int cek = 0;
        //    FC_Items items = new FC_Items();
        //    FC_ItemsFacade itemsF = new FC_ItemsFacade();
        //    if (Convert.ToInt32(txtQtyPOK.Text) > 0)
        //    {
        //        cek = cekpartno(txtPartnoPOK.Text);
        //        if (cek > 0)
        //        {
        //            items = new FC_Items();
        //            items.ItemTypeID = 3;
        //            items.Kode = txtPartnoPOK.Text.Substring(0, 3);
        //            items.Tebal = decimal.Parse(txtPartnoPOK.Text.Substring(6, 3)) / 10;
        //            items.Lebar = int.Parse(txtPartnoPOK.Text.Substring(9, 4));
        //            items.Panjang = int.Parse(txtPartnoPOK.Text.Substring(13, 4));
        //            items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
        //            items.Partno = txtPartnoPOK.Text;
        //            items.ItemDesc = "LIST PLANK";
        //            items.GroupID = 0;
        //            itemsF.Insert(items);
        //        }
        //        partnoOKT1 = txtPartnoPOK.Text.Substring(0, 3) + "-1-" + txtPartnoPOK.Text.Substring(6, 3)
        //            + txtPartnoPOK.Text.Substring(9, 4) + txtPartnoPOK.Text.Substring(13, 4);
        //        cek = cekpartno(partnoOKT1);
        //        if (cek > 0)
        //        {
        //            items = new FC_Items();
        //            items.ItemTypeID = 1;
        //            items.Kode = txtPartnoPOK.Text.Substring(0, 3);
        //            items.Tebal = decimal.Parse(txtPartnoPOK.Text.Substring(6, 3)) / 10;
        //            items.Lebar = int.Parse(txtPartnoPOK.Text.Substring(9, 4));
        //            items.Panjang = int.Parse(txtPartnoPOK.Text.Substring(13, 4));
        //            items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
        //            items.Partno = partnoOKT1;
        //            items.ItemDesc = "LIST PLANK";
        //            items.GroupID = 0;
        //            itemsF.Insert(items);
        //        }

        //        cek = ceklok(txtlokPOK.Text);
        //        if (cek > 0)
        //        {
        //            DisplayAJAXMessage(this, "lok : " + txtlokPOK.Text + " belum tersedia");
        //            return;
        //        }
        //    }
        //    if (Convert.ToInt32(txtQtyPBP.Text) > 0)
        //    {
        //        cek = cekpartno(txtPartnoPBP.Text);
        //        if (cek > 0)
        //        {
        //            items = new FC_Items();
        //            items.ItemTypeID = 3;
        //            items.Kode = txtPartnoPBP.Text.Substring(0, 3);
        //            items.Tebal = decimal.Parse(txtPartnoPBP.Text.Substring(6, 3)) / 10;
        //            items.Lebar = int.Parse(txtPartnoPBP.Text.Substring(9, 4));
        //            items.Panjang = int.Parse(txtPartnoPBP.Text.Substring(13, 4));
        //            items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
        //            items.Partno = txtPartnoPBP.Text;
        //            items.ItemDesc = "LIST PLANK";
        //            items.GroupID = 0;
        //            itemsF.Insert(items);
        //        }
        //        partnoBPT1 = txtPartnoPOK.Text.Substring(0, 3) + "-1-" + txtPartnoPBP.Text.Substring(6, 3)
        //            + txtPartnoPBP.Text.Substring(9, 4) + txtPartnoPBP.Text.Substring(13, 4);
        //        cek = cekpartno(partnoBPT1);
        //        if (cek > 0)
        //        {
        //            items = new FC_Items();
        //            items.ItemTypeID = 1;
        //            items.Kode = txtPartnoPBP.Text.Substring(0, 3);
        //            items.Tebal = decimal.Parse(txtPartnoPBP.Text.Substring(6, 3)) / 10;
        //            items.Lebar = int.Parse(txtPartnoPBP.Text.Substring(9, 4));
        //            items.Panjang = int.Parse(txtPartnoPBP.Text.Substring(13, 4));
        //            items.Volume = items.Tebal / 1000 * items.Panjang / 1000 * items.Lebar / 1000;
        //            items.Partno = partnoBPT1;
        //            items.ItemDesc = "LIST PLANK";
        //            items.GroupID = 0;
        //            itemsF.Insert(items);
        //        }

        //        cek = ceklok(txtlokPBP.Text);
        //        if (cek > 0)
        //        {
        //            DisplayAJAXMessage(this, "lok : " + txtlokPBP.Text + " belum tersedia");
        //            return;
        //        }
        //    }
        //    #endregion
        //    t1listplank2 = t1listplank2F.RetrieveByID(LID.Text);
        //    t1listplank2.L1ID = Convert.ToInt32(LID.Text);
        //    t1listplank2.LokasiID0 = dest.GetLokID("I99");
        //    t1listplank2.ItemID0 = dest.GetPartnoID(partnoOKT1);
        //    t1listplank2.ItemID = dest.GetPartnoID(txtPartnoPOK.Text);
        //    t1listplank2.QtyIn = Convert.ToInt32(txtSerah.Text);
        //    t1listplank2.QtyOut = Convert.ToInt32(txtQtyPOK.Text);
        //    t1listplank2.LokasiID = dest.GetLokID(txtlokOK.Text);

        //    #region rekam data serah
        //    T1_Jemur jemur = new T1_Jemur();
        //    FC_Items items1 = new FC_Items();
        //    ArrayList arrserah = new ArrayList();
        //    FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
        //    items1 = itemsfacade.RetrieveByPartNo(LPartno.Text);
        //    int ItemIDDest = items1.ID;
        //    items = itemsfacade.RetrieveByPartNo(txtPartnoPOK.Text);
        //    if (items.ID == 0)
        //    {
        //        btnTansfer.Disabled = false;
        //        DisplayAJAXMessage(this, "PartNo : " + txtPartnoPOK.Text + " belum tersedia!");
        //        return;
        //    }
        //    if (txtPartnoPOK.Text != string.Empty && txtQtyPOK.Text != string.Empty)
        //    {
        //        if (Convert.ToInt32(txtQtyPOK.Text) > 0)
        //        {
        //            T1_Serah serah = new T1_Serah();
        //            serah.PartnoSer = txtPartnoPOK.Text;
        //            serah.PartnoDest = LPartno.Text;
        //            serah.ItemIDSer = items.ID;
        //            if (serah.ItemIDSer == 0)
        //            {
        //                btnTansfer.Disabled = false;
        //                DisplayAJAXMessage(this, txtPartnoPOK.Text + " belum tersedia!");
        //                return;
        //            }
        //            serah.QtyIn = int.Parse(txtQtyPOK.Text);
        //            serah.LokasiID = dest.GetLokID(txtlokPOK.Text);
        //            if (serah.LokasiID == 0)
        //            {
        //                btnTansfer.Disabled = false;
        //                DisplayAJAXMessage(this, "Lokasi : " + txtlokPOK.Text + "belum tersedia!");
        //                return;
        //            }
        //            serah.LokasiSer = txtlokPOK.Text;
        //            serah.DestID = t1listplank2.DestID;
        //            serah.ItemIDDest = dest.GetPartnoID(partnoOKT1);
        //            serah.HPP = 0;
        //            serah.JemurID = t1listplank2.JemurID;
        //            serah.CreatedBy = users.UserName;
        //            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
        //            serah.SFrom = "listplank";
        //            arrserah.Add(serah);
        //        }
        //    }

        //    items = itemsfacade.RetrieveByPartNo(txtPartnoPBP.Text);
        //    if (items.ID == 0)
        //    {
        //        btnTansfer.Disabled = false;
        //        DisplayAJAXMessage(this, "PartNo : " + txtPartnoPBP.Text + " belum tersedia!");
        //        return;
        //    }
        //    if (txtPartnoPBP.Text != string.Empty && txtQtyPBP.Text != string.Empty)
        //    {
        //        if (Convert.ToInt32(txtQtyPBP.Text) > 0)
        //        {
        //            T1_Serah serah = new T1_Serah();
        //            serah.PartnoSer = txtPartnoPBP.Text;
        //            serah.PartnoDest = LPartno.Text;
        //            serah.ItemIDSer = items.ID;
        //            if (serah.ItemIDSer == 0)
        //            {
        //                btnTansfer.Disabled = false;
        //                DisplayAJAXMessage(this, txtPartnoPBP.Text + " belum tersedia!");
        //                return;
        //            }
        //            serah.QtyIn = int.Parse(txtQtyPBP.Text);
        //            serah.LokasiID = dest.GetLokID(txtlokPBP.Text);
        //            if (serah.LokasiID == 0)
        //            {
        //                btnTansfer.Disabled = false;
        //                DisplayAJAXMessage(this, "Lokasi : " + txtlokPBP.Text + "belum tersedia!");
        //                return;
        //            }
        //            serah.LokasiSer = txtlokPBP.Text;
        //            serah.DestID = t1listplank2.DestID;
        //            serah.ItemIDDest = dest.GetPartnoID(partnoBPT1);
        //            serah.HPP = 0;
        //            serah.JemurID = t1listplank2.JemurID;
        //            serah.CreatedBy = users.UserName;
        //            serah.TglSerah = Convert.ToDateTime(txtDateSerah.Text);
        //            serah.SFrom = "listplank";
        //            arrserah.Add(serah);
        //        }
        //    }
        //    #endregion

        //    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
        //    transManager.BeginTransaction();
        //    AbstractTransactionFacadeF absTrans = new T1_ListPlank2Facade(t1listplank2);
        //    intresult = absTrans.Insert(transManager);
        //    if (absTrans.Error != string.Empty)
        //    {
        //        transManager.RollbackTransaction();
        //        DisplayAJAXMessage(this, "Rekam data potongan listplank Error");
        //        return;
        //    }
        //    else
        //    {
        //        foreach (T1_Serah t1serah in arrserah)
        //        {
        //            t1serah.JemurID = intresult;
        //            absTrans = new T1_SerahFacade(t1serah);
        //            intresult = absTrans.Insert(transManager);
        //            if (absTrans.Error != string.Empty)
        //            {
        //                transManager.RollbackTransaction();
        //                DisplayAJAXMessage(this, "Rekam data serah Error");
        //                return;
        //            }
        //            else
        //            {
        //                TextBox partnoTrm = new TextBox();
        //                TextBox lokasiTrm = new TextBox();
        //                TextBox QtyTrm = new TextBox();
        //                partnoTrm.Text = t1serah.PartnoSer;
        //                lokasiTrm.Text = t1serah.LokasiSer;
        //                QtyTrm.Text = t1serah.QtyIn.ToString();
        //                t1serah.ID = intresult;
        //                if ( intresult > 0)
        //                {
        //                    int intterima = TerimaBarangLisplank(partnoTrm, lokasiTrm, QtyTrm, t1serah);
        //                    //if (intterima<=0)
        //                    //{
        //                    //    transManager.RollbackTransaction();
        //                    //    DisplayAJAXMessage(this, "Penerimaan data istplank error");
        //                    //    return;
        //                    //}
        //                }
        //                DisplayAJAXMessage(this, "Data tersimpan");
        //                rekam = "sukses";
        //            }
        //        }
        //    }
        //    transManager.CommitTransaction();
        //    transManager.CloseConnection();

        //    string kode = string.Empty;
        //    if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
        //        kode = LPartno.Text.Substring(0, 3);
        //    else
        //        kode = "INT";
        //    if (rekam == "sukses")
        //    {
        //        T1_ListPlankFacade t1listplankF = new T1_ListPlankFacade();
        //        t1listplankF.UpdateFail(Convert.ToInt32( LID.Text), Convert.ToInt32(txtSerah.Text));
        //    }
        //    ClearPotong2();
        //    IsiPartnoLisplankLvl2(kode);
        //    txtnopalet.Text = string.Empty;
        //    LoadDataJemur();
        //}
        protected int TerimaBarangLisplank(TextBox Partno, TextBox lokasi, TextBox qty, T1_Serah serah)
        {
            T3_RekapFacade rekapFacade = new T3_RekapFacade();
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_GroupsFacade groupsFacade = new T3_GroupsFacade();
            Users users = (Users)HttpContext.Current.Session["Users"];
            int intResult = 0;
            int maxtrans = 0;
            int checktrans = 0;
            decimal AvgHPP = 0;

            TextBox txtQtyTrm = qty;
            TextBox txtLokTrm = lokasi;
            TextBox txtPartnoOK = Partno;
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
            T3_Serah t3serah = new T3_Serah();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();

            items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
            t3serah = SerahFacade.RetrieveStockByPartno(txtPartnoOK.Text, txtLokTrm.Text);
            AvgHPP = 0;
            if (txtQtyTrm.Text == string.Empty)
                txtQtyTrm.Text = "0";
            maxtrans = serah.QtyIn - serah.QtyOut;
            checktrans = Convert.ToInt32(txtQtyTrm.Text);
            T3_Rekap rekap = new T3_Rekap();
            if (txtLokTrm.Text != string.Empty && txtQtyTrm.Text != string.Empty && maxtrans >= checktrans && checktrans > 0)
            {
                rekap.DestID = serah.DestID;
                rekap.SerahID = t3serah.ID;
                rekap.Keterangan = serah.PartnoDest;
                rekap.T1serahID = serah.ID;
                rekap.LokasiID = dest.GetLokID(txtLokTrm.Text);
                rekap.CutQty = 0;
                rekap.CutLevel = 1;
                if (rekap.LokasiID == 0)
                {
                    return 0;
                }

                FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
                rekap.ItemIDSer = items.ID;
                rekap.T1sItemID = serah.ItemIDSer;
                rekap.TglTrm = serah.TglSerah;
                rekap.QtyInTrm = int.Parse(txtQtyTrm.Text);
                rekap.T1SLokID = serah.LokasiID;
                rekap.QtyOutTrm = 0;
                rekap.SA = t3serah.Qty;
                if (int.Parse(txtQtyTrm.Text) > 0 && serah.HPP > 0)
                    AvgHPP = ((t3serah.HPP * t3serah.Qty) + (int.Parse(txtQtyTrm.Text) * serah.HPP)) / (t3serah.Qty + int.Parse(txtQtyTrm.Text));
                else
                    AvgHPP = t3serah.HPP;
                rekap.HPP = AvgHPP;
                rekap.GroupID = items.GroupID;
                rekap.CreatedBy = users.UserName;
                rekap.Process = "Direct";
                rekap.CutQty = 0;
                rekap.CutLevel = 1;
                serah.QtyOut = int.Parse(txtQtyTrm.Text);

                //proses Update Stock
                t3serah.Flag = "tambah";
                t3serah.ItemID = items.ID;
                t3serah.ID = t3serah.ID;
                t3serah.GroupID = items.GroupID;
                t3serah.LokID = rekap.LokasiID;
                t3serah.Qty = int.Parse(txtQtyTrm.Text);
                t3serah.HPP = rekap.HPP;
                t3serah.CreatedBy = users.UserName;
                if (t3serah.ID == 0)
                    rekap.SerahID = intResult;
                else
                    rekap.SerahID = t3serah.ID;
                TerimaProcessFacade TerimaProcessFacade = new TerimaProcessFacade(t3serah, rekap);
                string strError = TerimaProcessFacade.Insert1();
                if (strError != string.Empty)
                    intResult = -1;
                else
                    intResult = 0;
            }
            return intResult;
        }
        protected void Simetrislvl2LisPlank()
        {
            string strError = string.Empty;
            int intresult = 0;

            Users users = (Users)Session["Users"];
            ArrayList arrrekapK = new ArrayList();
            ArrayList arrrekapT = new ArrayList();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            T3_Simetris simetris = new T3_Simetris();

            if (txtQtyAsal.Text == string.Empty)
                txtQtyAsal.Text = "0";
            if (txtQtyPOK.Text == string.Empty)
                txtQtyPOK.Text = "0";
            if (txtQtyPBP.Text == string.Empty)
                txtQtyPBP.Text = "0";
            int cek = 0;

            if (Convert.ToInt32(txtQtyAsal.Text) > 0)
            {
                cek = cekpartno(txtPartnoAsal.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + txtPartnoAsal.Text + " belum tersedia");
                    return;
                }
                cek = ceklok(txtlokAsal.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokAsal.Text + " belum tersedia");
                    return;
                }
            }

            if (Convert.ToInt32(txtQtyPOK.Text) > 0)
            {
                cek = cekpartno(txtPartnoPOK.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + txtPartnoPOK.Text + " belum tersedia");
                    return;
                }
                cek = ceklok(txtlokPOK.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokPOK.Text + " belum tersedia");
                    return;
                }
            }

            if (Convert.ToInt32(txtQtyPBP.Text) > 0)
            {
                cek = cekpartno(txtPartnoPBP.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + txtPartnoPBP.Text + " belum tersedia");
                    return;
                }
                cek = ceklok(txtlokPBP.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokPBP.Text + " belum tersedia");
                    return;
                }
            }

            #region Verifikasi Closing Periode
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DateTime.Parse(txtDateSerah.Text).Year;
            int Bulan = DateTime.Parse(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion

            FC_Items itemsK = new FC_Items();
            FC_ItemsFacade itemsKF = new FC_ItemsFacade();
            itemsK = itemsKF.RetrieveByPartNo(txtPartnoAsal.Text);

            FC_Lokasi lokK = new FC_Lokasi();
            FC_LokasiFacade lokKF = new FC_LokasiFacade();
            lokK = lokKF.RetrieveByLokasi(txtlokAsal.Text);

            FC_Items itemsT = new FC_Items();
            FC_ItemsFacade itemsTF = new FC_ItemsFacade();
            itemsT = itemsTF.RetrieveByPartNo(txtPartnoPOK.Text);

            FC_Lokasi lokT = new FC_Lokasi();
            FC_LokasiFacade lokTF = new FC_LokasiFacade();
            lokT = lokTF.RetrieveByLokasi(txtlokPOK.Text);

            int luas1 = itemsK.Panjang * itemsK.Lebar;
            int luas2 = itemsT.Panjang * itemsT.Lebar;
            for (int i = 0; i <= GridViewTerima.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewTerima.Rows[i].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewTerima.Rows[i].FindControl("chkMutasi");
                if (chkMutasi.Checked == true)
                {
                    T3_RekapFacade rekapFacade = new T3_RekapFacade();//t3_rekap
                    FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
                    T3_Serah t3serahK = new T3_Serah();//Asal Partno
                    T3_SerahFacade t3serahKF = new T3_SerahFacade();
                    t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal.Text, txtlokAsal.Text);
                    if (t3serahK.ItemID == 0)
                    {
                        t3serahK.Flag = "tambah";
                        t3serahK.LokID = lokK.ID;
                        t3serahK.ItemID = itemsK.ID;
                        t3serahK.GroupID = itemsK.GroupID;
                        t3serahK.Qty = 0;
                        t3serahK.HPP = 0;
                        t3serahK.CreatedBy = users.UserName;
                        TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                        transManager.BeginTransaction();
                        AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahK);
                        intresult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return;
                        }
                        transManager.CommitTransaction();
                        transManager.CloseConnection();
                        t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal.Text, txtlokAsal.Text);
                    }
                    int laststock1 = t3serahK.Qty;
                    decimal lastAvgHPP1 = t3serahK.HPP;
                    t3serahK.Flag = "kurang";
                    t3serahK.Qty = Convert.ToInt32(txtQtyMutasi.Text);
                    t3serahK.CreatedBy = users.UserName;

                    T3_Rekap rekapK = new T3_Rekap();//asal partno qtyout
                    rekapK.DestID = Convert.ToInt32(GridViewTerima.Rows[i].Cells[1].Text);
                    rekapK.SerahID = t3serahK.ID;
                    rekapK.T1serahID = 0;
                    rekapK.GroupID = itemsK.GroupID;
                    rekapK.LokasiID = t3serahK.LokID;
                    rekapK.ItemIDSer = t3serahK.ItemID;
                    rekapK.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                    rekapK.QtyInTrm = 0;
                    rekapK.QtyOutTrm = Convert.ToInt32(txtQtyMutasi.Text);
                    rekapK.HPP = lastAvgHPP1;
                    rekapK.CreatedBy = users.UserName;
                    rekapK.Keterangan = txtPartnoPOK.Text;
                    rekapK.SA = laststock1;
                    rekapK.Process = "Simetris";
                    rekapK.CutQty = Convert.ToInt32(txtQtyMutasi.Text);
                    rekapK.CutLevel = 2;
                    rekapK.ID = Convert.ToInt32(GridViewTerima.Rows[i].Cells[0].Text);
                    arrrekapK.Add(rekapK);

                    //proses lokasi akhir
                    /*dapatkan stock partno tujuan*/
                    T3_Serah t3serahT = new T3_Serah();//terima Partno
                    int laststock2 = t3serahT.Qty;
                    T3_SerahFacade t3serahTF = new T3_SerahFacade();
                    t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK.Text, txtlokPOK.Text);
                    if (t3serahT.ItemID == 0)
                    {
                        t3serahT.Flag = "tambah";
                        t3serahT.LokID = lokT.ID;
                        t3serahT.ItemID = itemsT.ID;
                        t3serahT.GroupID = itemsT.GroupID;
                        t3serahT.Qty = 0;
                        t3serahT.HPP = 0;
                        t3serahT.CreatedBy = users.UserName;
                        TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                        transManager.BeginTransaction();
                        AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                        intresult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return;
                        }
                        transManager.CommitTransaction();
                        transManager.CloseConnection();
                        t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK.Text, txtlokPOK.Text);
                    }
                    decimal lastAvgHPP2 = t3serahT.HPP;

                    t3serahT.Flag = "tambah";
                    t3serahT.ItemID = itemsT.ID;
                    t3serahT.GroupID = itemsT.GroupID;
                    t3serahT.ID = t3serahT.ID;
                    t3serahT.LokID = lokT.ID;
                    t3serahT.Qty = Convert.ToInt32(txtQtyMutasi.Text) * pengali();
                    decimal HPPnewItem = (luas2 / luas1) * lastAvgHPP1;
                    t3serahT.HPP = HPPnewItem;
                    t3serahT.CreatedBy = users.UserName;

                    T3_Rekap rekapT = new T3_Rekap();//terima partno qtyin
                    rekapT.DestID = Convert.ToInt32(GridViewTerima.Rows[i].Cells[1].Text);
                    rekapT.SerahID = t3serahK.ID;
                    rekapT.GroupID = itemsT.GroupID;
                    rekapT.T1serahID = 0;
                    rekapT.LokasiID = lokT.ID;
                    rekapT.ItemIDSer = itemsT.ID;
                    rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                    rekapT.QtyInTrm = Convert.ToInt32(txtQtyMutasi.Text) * pengali();
                    rekapT.QtyOutTrm = 0;
                    rekapT.HPP = HPPnewItem;
                    rekapT.GroupID = 0;
                    rekapT.CreatedBy = users.UserName;
                    rekapT.Keterangan = txtPartnoAsal.Text;
                    rekapT.Process = "Simetris";
                    rekapT.SA = laststock2;
                    rekapT.CutQty = 0;
                    rekapT.CutLevel = 2;
                    rekapT.ID = Convert.ToInt32(GridViewTerima.Rows[i].Cells[0].Text);
                    arrrekapT.Add(rekapT);

                    simetris.RekapID = intresult;
                    simetris.SerahID = t3serahK.ID;
                    simetris.LokasiID = t3serahT.LokID;
                    simetris.TglSm = DateTime.Parse(txtDateSerah.Text).Date;
                    simetris.ItemID = t3serahT.ItemID;
                    simetris.QtyInSm = Convert.ToInt32(txtQtyAsal.Text);
                    simetris.QtyOutSm = Convert.ToInt32(txtQtyPOK.Text) + Convert.ToInt32(txtQtyPBP.Text);
                    simetris.GroupID = 0;
                    simetris.CreatedBy = users.UserName;
                }
            }

            //rekam table simetris
            T3_SimetrisAutoProcessFacade SimetrisProcessFacade = new T3_SimetrisAutoProcessFacade(arrrekapK, arrrekapT, simetris);
            strError = SimetrisProcessFacade.Insert1();
            if (strError == string.Empty)
            {
                T3_RekapFacade t3rekapF = new T3_RekapFacade();
                foreach (T3_Rekap rekapK in arrrekapK)
                {
                    t3rekapF.UpdateCutLevel2(rekapK.ID, rekapK.QtyOutTrm);
                }
                if (Convert.ToInt32(txtQtyPBP.Text) > 0)
                {
                    Simetris(txtPartnoPOK.Text, txtlokPOK.Text, txtQtyPBP.Text, txtPartnoPBP.Text, txtlokPBP.Text, txtQtyPBP.Text, LDestID.Text, 2);
                }

                ClearPotong2();
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                DisplayAJAXMessage(this, "Data tersimpan");
            }
            txtnopalet.Text = string.Empty;
            // LoadDataGridTerima();
        }
        protected void Simetris(string PartnoAsal, string LokAsal, string QtyAsal, string PartnoTujuan, string LokTujuan, string QtyTujuan, string DestID, int cutlevel)
        {
            string strError = string.Empty;
            Users users = (Users)Session["Users"];
            ArrayList arrrekapK = new ArrayList();
            ArrayList arrrekapT = new ArrayList();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            T3_Simetris simetris = new T3_Simetris();
            //if (PartnoTujuan.IndexOf("-S-") != -1 )
            //{
            //    DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
            //    return;
            //}
            if (Convert.ToInt32(QtyAsal) > 0 && Convert.ToInt32(QtyTujuan) > 0)
            {
                int intResult = 0;
                FC_Items itemsK = new FC_Items();
                FC_ItemsFacade itemsKF = new FC_ItemsFacade();
                itemsK = itemsKF.RetrieveByPartNo(PartnoAsal);
                if (itemsK.ID == 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + itemsK.Partno + " belum tersedia");
                    return;
                }
                FC_Items itemsT = new FC_Items();
                FC_ItemsFacade itemsTF = new FC_ItemsFacade();
                itemsT = itemsTF.RetrieveByPartNo(PartnoTujuan);
                if (itemsT.ID == 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + itemsT.Partno + " belum tersedia");
                    return;
                }
                FC_Lokasi lokK = new FC_Lokasi();
                FC_LokasiFacade lokKF = new FC_LokasiFacade();
                lokK = lokKF.RetrieveByLokasi(LokAsal);
                if (lokK.ID == 0)
                {
                    DisplayAJAXMessage(this, "Lokasi : " + lokK.Lokasi + " belum tersedia");
                    return;
                }
                FC_Lokasi lokT = new FC_Lokasi();
                FC_LokasiFacade lokTF = new FC_LokasiFacade();
                lokT = lokTF.RetrieveByLokasi(LokTujuan);
                if (lokT.ID == 0)
                {
                    DisplayAJAXMessage(this, "Lokasi : " + lokT.Lokasi + " belum tersedia");
                    return;
                }

                int luas1 = itemsK.Panjang * itemsK.Lebar;
                int luas2 = itemsT.Panjang * itemsT.Lebar;

                T3_RekapFacade rekapFacade = new T3_RekapFacade();//t3_rekap
                FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
                T3_Serah t3serahK = new T3_Serah();//Asal Partno
                T3_SerahFacade t3serahKF = new T3_SerahFacade();
                t3serahK = t3serahKF.RetrieveStockByPartno(PartnoAsal, LokAsal);
                if (t3serahK.ItemID == 0)
                {
                    t3serahK.Flag = "tambah";
                    t3serahK.LokID = lokK.ID;
                    t3serahK.ItemID = itemsK.ID;
                    t3serahK.GroupID = itemsK.GroupID;
                    t3serahK.Qty = 0;
                    t3serahK.HPP = 0;
                    t3serahK.CreatedBy = users.UserName;
                    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                    transManager.BeginTransaction();
                    AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahK);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    transManager.CommitTransaction();
                    transManager.CloseConnection();
                    t3serahK = t3serahKF.RetrieveStockByPartno(PartnoAsal, LokAsal);
                }
                t3serahK.Flag = "kurang";
                t3serahK.Qty = Convert.ToInt32(QtyAsal);
                t3serahK.CreatedBy = users.UserName;

                T3_Rekap rekapK = new T3_Rekap();
                int laststock1 = t3serahK.Qty;
                decimal lastAvgHPP1 = t3serahK.HPP;
                rekapK.DestID = Convert.ToInt32(DestID);
                rekapK.SerahID = t3serahK.ID;
                rekapK.T1serahID = 0;
                rekapK.GroupID = itemsK.GroupID;
                rekapK.LokasiID = t3serahK.LokID;
                rekapK.ItemIDSer = t3serahK.ItemID;
                rekapK.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                rekapK.QtyInTrm = 0;
                rekapK.QtyOutTrm = Convert.ToInt32(QtyAsal);
                rekapK.HPP = lastAvgHPP1;
                rekapK.CreatedBy = users.UserName;
                rekapK.Keterangan = PartnoTujuan;
                rekapK.SA = laststock1;
                rekapK.Process = "Simetris";
                rekapK.CutQty = Convert.ToInt32(QtyAsal);
                rekapK.CutLevel = 2;
                arrrekapK.Add(rekapK);

                //proses lokasi akhir
                /*dapatkan stock partno tujuan*/
                T3_Serah t3serahT = new T3_Serah();//terima Partno

                T3_SerahFacade t3serahTF = new T3_SerahFacade();
                t3serahT = t3serahTF.RetrieveStockByPartno(PartnoTujuan, LokTujuan);
                if (t3serahT.ItemID == 0)
                {
                    t3serahT.Flag = "tambah";
                    t3serahT.LokID = lokT.ID;
                    t3serahT.ItemID = itemsT.ID;
                    t3serahT.GroupID = itemsT.GroupID;
                    t3serahT.Qty = 0;
                    t3serahT.HPP = 0;
                    t3serahT.CreatedBy = users.UserName;
                    TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                    transManager.BeginTransaction();
                    AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    transManager.CommitTransaction();
                    transManager.CloseConnection();
                    t3serahT = t3serahTF.RetrieveStockByPartno(PartnoTujuan, LokTujuan);
                }
                decimal lastAvgHPP2 = t3serahT.HPP;
                int laststock2 = t3serahT.Qty;
                t3serahT.Flag = "tambah";
                t3serahT.ItemID = itemsT.ID;
                t3serahT.GroupID = itemsT.GroupID;
                t3serahT.ID = t3serahT.ID;
                t3serahT.LokID = t3serahT.LokID;
                t3serahT.Qty = Convert.ToInt32(QtyTujuan);
                decimal HPPnewItem = (luas2 / luas1) * lastAvgHPP1;
                t3serahT.HPP = HPPnewItem;
                t3serahT.CreatedBy = users.UserName;

                T3_Rekap rekapT = new T3_Rekap();
                rekapT.DestID = Convert.ToInt32(DestID);
                rekapT.SerahID = t3serahK.ID;
                rekapT.GroupID = itemsT.GroupID;
                rekapT.T1serahID = 0;
                rekapT.LokasiID = t3serahT.LokID;
                rekapT.ItemIDSer = itemsT.ID;
                rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                rekapT.QtyInTrm = Convert.ToInt32(QtyTujuan);
                rekapT.QtyOutTrm = 0;
                rekapT.HPP = HPPnewItem;
                rekapT.GroupID = 0;
                rekapT.CreatedBy = users.UserName;
                rekapT.Keterangan = PartnoAsal;
                rekapT.Process = "Simetris";
                rekapT.SA = laststock2;
                rekapT.CutQty = 0;
                rekapT.CutLevel = 2;
                rekapT.ID = 0;
                arrrekapT.Add(rekapT);

                //rekam table simetris
                simetris.SerahID = t3serahK.ID;
                simetris.LokasiID = t3serahT.LokID;
                simetris.TglSm = DateTime.Parse(txtDateSerah.Text).Date;
                simetris.ItemID = t3serahT.ItemID;
                simetris.QtyInSm = Convert.ToInt32(QtyAsal);
                simetris.QtyOutSm = Convert.ToInt32(QtyTujuan);
                simetris.GroupID = 0;
                simetris.CreatedBy = users.UserName;
            }

            T3_SimetrisAutoProcessFacade SimetrisProcessFacade = new T3_SimetrisAutoProcessFacade(arrrekapK, arrrekapT, simetris);
            strError = SimetrisProcessFacade.Insert1();
        }
        protected void txtQtyOK_TextChanged(object sender, EventArgs e)
        {
            txtSerah.Text = txtQtyOK.Text;
            txtQtyKW.Text = "0";
            txtQtyBPF.Text = "0";
            txtQtyBPU.Text = "0";
            txtQtyBPS.Text = "0";
            txtQtyKW1.Text = "0";
            txtQtyBPF0.Text = "0";
            txtQtyBPU0.Text = "0";
            Session["arrpelarian"] = null;
            ArrayList arrPelarian = new ArrayList();
            T1_Jemur jemur = new T1_Jemur();
            arrPelarian.Add(jemur);
            Session["arrpelarian"] = arrPelarian;
            GridView1.DataSource = arrPelarian;
            GridView1.DataBind();
            if (RB1000.Checked == true)
            {
                txtQtyOK1.Text = (Convert.ToInt32(txtQtyOK.Text) * 2).ToString();
                txtQtyKW.Focus();
            }
            else
            {
                txtQtyOK1.Text = (Convert.ToInt32(txtQtyOK.Text)).ToString();
                txtQtyKW.Focus();
            }
            AutoBST1ALL();
            ArrayList arrbsauto = new ArrayList();
            arrbsauto = (ArrayList)Session["arrbsauto"];
            GridBSAuto.DataSource = arrbsauto;
            GridBSAuto.DataBind();
        }
        private void AutoBST1ALL()
        {
            Session["arrbsauto"] = null;
            AutoBST1OK();
            AutoBST1KW();
            AutoBST1BPF();
        }
        private void AutoBST1OK()
        {

            ArrayList arrbsauto = new ArrayList();
            if (Session["arrbsauto"] != null)
                arrbsauto = (ArrayList)Session["arrbsauto"];
            FC_Items items = new FC_Items();
            FC_Items itemsAsal = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            itemsAsal = itemsF.RetrieveByPartNo(LPartno.Text);
            string partnoBS1 = string.Empty;
            string partnoBS2 = string.Empty;
            int panjangBS1 = 0;
            int lebarBS1 = 0;
            string Lokasi = string.Empty;
            //Session["arrbsauto"] = null;
            int sisaLebar = 0;
            int sisaPajang = 0;
            if (RB4Mili.Checked == false)
                return;
            items = itemsF.RetrieveByPartNo(txtPartnoOK.Text);
            panjangBS1 = itemsAsal.Panjang - items.Panjang;
            lebarBS1 = itemsAsal.Lebar - items.Lebar;
            if (Convert.ToInt32(txtQtyOK.Text) > 0)
            {
                if (panjangBS1 > 0)
                {
                    BSAuto bsauto = new BSAuto();
                    partnoBS1 = txtPartnoOK.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                        lebarBS1.ToString().PadLeft(4, '0') + itemsAsal.Panjang.ToString().PadLeft(4, '0') + "T1";
                    //txtPartnoOK.Text.Substring(17,txtPartnoOK.Text.Length - 17);
                    if (decimal.Parse(partnoBS1.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS1.Substring(9, 4)) / 1000 *
                            decimal.Parse(partnoBS1.Substring(13, 4)) / 1000 > 0)
                    {
                        bsauto.Partno = partnoBS1;
                        bsauto.Qty = Convert.ToInt32(txtQtyOK.Text);
                        bsauto.Lokasi = "bsauto";
                        arrbsauto.Add(bsauto);
                        int newitemID = 0;
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(partnoBS1) == 1)
                        {

                            Item.ItemTypeID = 3;
                            Item.Kode = partnoBS1.Substring(0, 3);
                            Item.Tebal = decimal.Parse(partnoBS1.Substring(6, 3)) / 10;
                            Item.Lebar = int.Parse(partnoBS1.Substring(9, 4));
                            Item.Panjang = int.Parse(partnoBS1.Substring(13, 4));
                            Item.Volume = decimal.Parse(partnoBS1.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS1.Substring(9, 4)) / 1000 *
                                decimal.Parse(partnoBS1.Substring(13, 4)) / 1000;
                            Item.Partno = partnoBS1;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                            Item = ItemF.RetrieveByPartNo(partnoBS1);
                            newitemID = Item.ID;
                        }
                    }
                }

                if (lebarBS1 > 0)
                {
                    BSAuto bsauto = new BSAuto();
                    partnoBS2 = txtPartnoOK.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                        panjangBS1.ToString().PadLeft(4, '0') + (itemsAsal.Lebar - lebarBS1).ToString().PadLeft(4, '0') + "T1";
                    //txtPartnoOK.Text.Substring(17,txtPartnoOK.Text.Length - 17);
                    if (decimal.Parse(partnoBS2.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS2.Substring(9, 4)) / 1000 *
                            decimal.Parse(partnoBS2.Substring(13, 4)) / 1000 > 0)
                    {
                        bsauto.Partno = partnoBS2;
                        bsauto.Qty = Convert.ToInt32(txtQtyOK.Text);
                        bsauto.Lokasi = "bsauto";
                        arrbsauto.Add(bsauto);
                        int newitemID = 0;
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(partnoBS2) == 1)
                        {

                            Item.ItemTypeID = 3;
                            Item.Kode = partnoBS2.Substring(0, 3);
                            Item.Tebal = decimal.Parse(partnoBS2.Substring(6, 3)) / 10;
                            Item.Lebar = int.Parse(partnoBS2.Substring(9, 4));
                            Item.Panjang = int.Parse(partnoBS2.Substring(13, 4));
                            Item.Volume = decimal.Parse(partnoBS2.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS2.Substring(9, 4)) / 1000 *
                                decimal.Parse(partnoBS2.Substring(13, 4)) / 1000;
                            Item.Partno = partnoBS2;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                            Item = ItemF.RetrieveByPartNo(partnoBS2);
                            newitemID = Item.ID;
                        }
                    }
                }
            }
            Session["arrbsauto"] = arrbsauto;
        }
        private void AutoBST1KW()
        {

            ArrayList arrbsauto = new ArrayList();
            if (Session["arrbsauto"] != null)
                arrbsauto = (ArrayList)Session["arrbsauto"];
            FC_Items items = new FC_Items();
            FC_Items itemsAsal = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            itemsAsal = itemsF.RetrieveByPartNo(LPartno.Text);
            string partnoBS1 = string.Empty;
            string partnoBS2 = string.Empty;
            int panjangBS1 = 0;
            int lebarBS1 = 0;
            string Lokasi = string.Empty;
            //Session["arrbsauto"] = null;
            int sisaLebar = 0;
            int sisaPajang = 0;
            if (RB4Mili.Checked == false)
                return;
            items = itemsF.RetrieveByPartNo(txtPartnoKW.Text);
            panjangBS1 = itemsAsal.Panjang - items.Panjang;
            lebarBS1 = itemsAsal.Lebar - items.Lebar;
            if (Convert.ToInt32(txtQtyKW.Text) > 0)
            {
                if (panjangBS1 > 0)
                {
                    BSAuto bsauto = new BSAuto();
                    partnoBS1 = txtPartnoKW.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                        lebarBS1.ToString().PadLeft(4, '0') + itemsAsal.Panjang.ToString().PadLeft(4, '0') + "T1";
                    //txtPartnoOK.Text.Substring(17,txtPartnoOK.Text.Length - 17);
                    if (decimal.Parse(partnoBS1.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS1.Substring(9, 4)) / 1000 *
                            decimal.Parse(partnoBS1.Substring(13, 4)) / 1000 > 0)
                    {
                        bsauto.Partno = partnoBS1;
                        bsauto.Qty = Convert.ToInt32(txtQtyKW.Text);
                        bsauto.Lokasi = "bsauto";
                        arrbsauto.Add(bsauto);
                        int newitemID = 0;
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(partnoBS1) == 1)
                        {
                            Item.ItemTypeID = 3;
                            Item.Kode = partnoBS1.Substring(0, 3);
                            Item.Tebal = decimal.Parse(partnoBS1.Substring(6, 3)) / 10;
                            Item.Lebar = int.Parse(partnoBS1.Substring(9, 4));
                            Item.Panjang = int.Parse(partnoBS1.Substring(13, 4));
                            Item.Volume = decimal.Parse(partnoBS1.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS1.Substring(9, 4)) / 1000 *
                                decimal.Parse(partnoBS1.Substring(13, 4)) / 1000;
                            Item.Partno = partnoBS1;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                            Item = ItemF.RetrieveByPartNo(partnoBS1);
                            newitemID = Item.ID;
                        }
                    }
                }

                if (lebarBS1 > 0)
                {
                    BSAuto bsauto = new BSAuto();
                    partnoBS2 = txtPartnoKW.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                        panjangBS1.ToString().PadLeft(4, '0') + (itemsAsal.Lebar - lebarBS1).ToString().PadLeft(4, '0') + "T1";
                    //txtPartnoOK.Text.Substring(17,txtPartnoOK.Text.Length - 17);
                    if (decimal.Parse(partnoBS2.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS2.Substring(9, 4)) / 1000 *
                            decimal.Parse(partnoBS2.Substring(13, 4)) / 1000 > 0)
                    {
                        bsauto.Partno = partnoBS2;
                        bsauto.Qty = Convert.ToInt32(txtQtyKW.Text);
                        bsauto.Lokasi = "bsauto";
                        arrbsauto.Add(bsauto);
                        int newitemID = 0;
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(partnoBS2) == 1)
                        {

                            Item.ItemTypeID = 3;
                            Item.Kode = partnoBS2.Substring(0, 3);
                            Item.Tebal = decimal.Parse(partnoBS2.Substring(6, 3)) / 10;
                            Item.Lebar = int.Parse(partnoBS2.Substring(9, 4));
                            Item.Panjang = int.Parse(partnoBS2.Substring(13, 4));
                            Item.Volume = decimal.Parse(partnoBS2.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS2.Substring(9, 4)) / 1000 *
                                decimal.Parse(partnoBS2.Substring(13, 4)) / 1000;
                            Item.Partno = partnoBS2;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                            Item = ItemF.RetrieveByPartNo(partnoBS2);
                            newitemID = Item.ID;
                        }
                    }
                }
            }
            Session["arrbsauto"] = arrbsauto;
        }
        private void AutoBST1BPF()
        {

            ArrayList arrbsauto = new ArrayList();
            if (Session["arrbsauto"] != null)
                arrbsauto = (ArrayList)Session["arrbsauto"];
            FC_Items items = new FC_Items();
            FC_Items itemsAsal = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            itemsAsal = itemsF.RetrieveByPartNo(LPartno.Text);
            string partnoBS1 = string.Empty;
            string partnoBS2 = string.Empty;
            int panjangBS1 = 0;
            int lebarBS1 = 0;
            string Lokasi = string.Empty;
            //Session["arrbsauto"] = null;
            int sisaLebar = 0;
            int sisaPajang = 0;
            if (RB4Mili.Checked == false)
                return;
            items = itemsF.RetrieveByPartNo(txtPartnoBPF.Text);
            panjangBS1 = itemsAsal.Panjang - items.Panjang;
            lebarBS1 = itemsAsal.Lebar - items.Lebar;
            if (Convert.ToInt32(txtQtyBPF.Text) > 0)
            {
                if (panjangBS1 > 0)
                {
                    BSAuto bsauto = new BSAuto();
                    partnoBS1 = txtPartnoBPF.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                        lebarBS1.ToString().PadLeft(4, '0') + itemsAsal.Panjang.ToString().PadLeft(4, '0') + "T1";
                    //txtPartnoOK.Text.Substring(17,txtPartnoOK.Text.Length - 17);
                    if (decimal.Parse(partnoBS1.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS1.Substring(9, 4)) / 1000 *
                            decimal.Parse(partnoBS1.Substring(13, 4)) / 1000 > 0)
                    {
                        bsauto.Partno = partnoBS1;
                        bsauto.Qty = Convert.ToInt32(txtQtyBPF.Text);
                        bsauto.Lokasi = "bsauto";
                        arrbsauto.Add(bsauto);
                        int newitemID = 0;
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(partnoBS1) == 1)
                        {

                            Item.ItemTypeID = 3;
                            Item.Kode = partnoBS1.Substring(0, 3);
                            Item.Tebal = decimal.Parse(partnoBS1.Substring(6, 3)) / 10;
                            Item.Lebar = int.Parse(partnoBS1.Substring(9, 4));
                            Item.Panjang = int.Parse(partnoBS1.Substring(13, 4));
                            Item.Volume = decimal.Parse(partnoBS1.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS1.Substring(9, 4)) / 1000 *
                                decimal.Parse(partnoBS1.Substring(13, 4)) / 1000;
                            Item.Partno = partnoBS1;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                            Item = ItemF.RetrieveByPartNo(partnoBS1);
                            newitemID = Item.ID;
                        }
                    }
                }

                if (lebarBS1 > 0)
                {
                    BSAuto bsauto = new BSAuto();
                    partnoBS2 = txtPartnoBPF.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                        panjangBS1.ToString().PadLeft(4, '0') + (itemsAsal.Lebar - lebarBS1).ToString().PadLeft(4, '0') + "T1";
                    //txtPartnoOK.Text.Substring(17,txtPartnoOK.Text.Length - 17);
                    if (decimal.Parse(partnoBS2.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS2.Substring(9, 4)) / 1000 *
                            decimal.Parse(partnoBS2.Substring(13, 4)) / 1000 > 0)
                    {
                        bsauto.Partno = partnoBS2;
                        bsauto.Qty = Convert.ToInt32(txtQtyBPF.Text);
                        bsauto.Lokasi = "bsauto";
                        arrbsauto.Add(bsauto);
                        int newitemID = 0;
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(partnoBS2) == 1)
                        {

                            Item.ItemTypeID = 3;
                            Item.Kode = partnoBS2.Substring(0, 3);
                            Item.Tebal = decimal.Parse(partnoBS2.Substring(6, 3)) / 10;
                            Item.Lebar = int.Parse(partnoBS2.Substring(9, 4));
                            Item.Panjang = int.Parse(partnoBS2.Substring(13, 4));
                            Item.Volume = decimal.Parse(partnoBS2.Substring(6, 3)) / 1000 * decimal.Parse(partnoBS2.Substring(9, 4)) / 1000 *
                                decimal.Parse(partnoBS2.Substring(13, 4)) / 1000;
                            Item.Partno = partnoBS2;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                            Item = ItemF.RetrieveByPartNo(partnoBS2);
                            newitemID = Item.ID;
                        }
                    }
                }
            }
            Session["arrbsauto"] = arrbsauto;
        }


        protected void txtQtyKW_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            int i = 0;
            int totlari = 0;
            try
            {
                foreach (T1_Jemur pelarian in arrPelarian)
                {
                    TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                    totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                    i++;
                }
                if (txtSerah.Text.Trim() == string.Empty)
                    txtSerah.Text = "0";
                if (txtQtyKW.Text.Trim() == string.Empty)
                    txtQtyKW.Text = "0";
                if (txtQtyOK.Text.Trim() == string.Empty)
                    txtQtyOK.Text = "0";
                if (txtQtyBPF.Text.Trim() == string.Empty)
                    txtQtyBPF.Text = "0";
                if (txtQtyBPU.Text.Trim() == string.Empty)
                    txtQtyBPU.Text = "0";
                if (txtQtyBPS.Text.Trim() == string.Empty)
                    txtQtyBPS.Text = "0";
                txtQtyOK.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                if (RBSuperPanel.Checked == true)
                {
                    int kali = pengali();
                    if (txtQtyAsal.Text == string.Empty)
                        txtQtyAsal.Text = "0";
                    if (txtQtyPOK.Text == string.Empty)
                        txtQtyPOK.Text = "0";
                    if (txtQtyPBP.Text == string.Empty)
                        txtQtyPBP.Text = "0";
                    if (txtQtyPBP.Text != string.Empty && txtQtyPOK.Text != string.Empty)
                    {

                        txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text) - Convert.ToUInt32(txtQtyKW.Text) - totlari).ToString();
                        txtQtyOK.Text = txtQtyPOK.Text;
                        txtQtyBPF.Text = txtQtyPBP.Text;
                    }
                }
                if (RB1000.Checked == true)
                {
                    txtQtyKW1.Text = (Convert.ToInt32(txtQtyKW.Text) * 2).ToString();
                    txtQtyBPF.Focus();
                }
                else
                {
                    txtQtyKW1.Text = (Convert.ToInt32(txtQtyKW.Text)).ToString();
                    txtQtyBPF.Focus();
                }
                txtPartnoBPF.Focus();
                AutoBST1ALL();
                ArrayList arrbsauto = new ArrayList();
                arrbsauto = (ArrayList)Session["arrbsauto"];
                GridBSAuto.DataSource = arrbsauto;
                GridBSAuto.DataBind();
            }
            catch { }
        }
        protected void txtQtyBPU_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            int i = 0;
            int totlari = 0;
            try
            {
                foreach (T1_Jemur pelarian in arrPelarian)
                {
                    TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                    totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                    i++;
                }
                if (txtSerah.Text.Trim() == string.Empty)
                    txtSerah.Text = "0";
                if (txtQtyKW.Text.Trim() == string.Empty)
                    txtQtyKW.Text = "0";
                if (txtQtyOK.Text.Trim() == string.Empty)
                    txtQtyOK.Text = "0";
                if (txtQtyBPF.Text.Trim() == string.Empty)
                    txtQtyBPF.Text = "0";
                if (txtQtyBPU.Text.Trim() == string.Empty)
                    txtQtyBPU.Text = "0";
                if (txtQtyBPS.Text.Trim() == string.Empty)
                    txtQtyBPS.Text = "0";
                if (RBSuperPanel.Checked == true)
                {
                    txtQtyOK.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                    txtQtyPOK.Text = txtQtyOK.Text;
                }
                else
                    txtQtyKW.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyOK.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                if (RB4Mili.Checked == true && RBSuperPanel.Checked == true)
                {
                    txtQtyPOK.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                }
                if (RB1000.Checked == true)
                {
                    txtQtyBPU0.Text = (Convert.ToInt32(txtQtyBPU.Text) * 2).ToString();
                    txtQtyBPS.Focus();
                }
                else
                {
                    txtQtyBPU0.Text = (Convert.ToInt32(txtQtyBPU.Text)).ToString();
                    txtQtyBPS.Focus();
                }
                AutoBST1ALL();
                ArrayList arrbsauto = new ArrayList();
                arrbsauto = (ArrayList)Session["arrbsauto"];
                GridBSAuto.DataSource = arrbsauto;
                GridBSAuto.DataBind();
            }
            catch { }
        }
        protected void txtQtyBPF_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            int i = 0;
            int totlari = 0;
            try
            {
                foreach (T1_Jemur pelarian in arrPelarian)
                {
                    TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                    totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                    i++;
                }
                if (txtSerah.Text.Trim() == string.Empty)
                    txtSerah.Text = "0";
                if (txtQtyKW.Text.Trim() == string.Empty)
                    txtQtyKW.Text = "0";
                if (txtQtyOK.Text.Trim() == string.Empty)
                    txtQtyOK.Text = "0";
                if (txtQtyBPF.Text.Trim() == string.Empty)
                    txtQtyBPF.Text = "0";
                if (txtQtyBPU.Text.Trim() == string.Empty)
                    txtQtyBPU.Text = "0";
                if (txtQtyBPS.Text.Trim() == string.Empty)
                    txtQtyBPS.Text = "0";
                if (RBSuperPanel.Checked == true)
                    txtQtyOK.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                else
                {
                    txtQtyKW.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyOK.Text) - Convert.ToInt32(txtQtyBPF.Text)
                    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                    txtQtyPOK.Text = txtQtyKW.Text;
                }
                if (RB1000.Checked == true)
                {
                    txtQtyBPF0.Text = (Convert.ToInt32(txtQtyBPF.Text) * 2).ToString();
                    txtQtyBPU.Focus();
                }
                else
                {
                    txtQtyBPF0.Text = (Convert.ToInt32(txtQtyBPF.Text)).ToString();
                    txtQtyBPU.Focus();
                }
                AutoBST1ALL();
                ArrayList arrbsauto = new ArrayList();
                arrbsauto = (ArrayList)Session["arrbsauto"];
                GridBSAuto.DataSource = arrbsauto;
                GridBSAuto.DataBind();
            }
            catch { }
        }
        protected void txtQtyOK1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RB1000.Checked == true)
                {
                    txtQtyOK.Text = (Convert.ToInt32(txtQtyOK1.Text) / 2).ToString();
                    txtQtyOK1.Focus();
                }
                else
                {
                    txtQtyOK.Text = (Convert.ToInt32(txtQtyOK1.Text)).ToString();
                    txtQtyOK1.Focus();
                }
            }
            catch { }
        }
        protected void txtQtyKW1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RB1000.Checked == true)
                {
                    txtQtyKW.Text = (Convert.ToInt32(txtQtyKW1.Text) / 2).ToString();
                    txtQtyKW1.Focus();
                }
                else
                {
                    txtQtyKW.Text = (Convert.ToInt32(txtQtyKW1.Text)).ToString();
                    txtQtyKW1.Focus();
                }
            }
            catch { }
        }
        protected void txtQtyBPF0_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RB1000.Checked == true)
                {
                    txtQtyBPF.Text = (Convert.ToInt32(txtQtyBPF0.Text) / 2).ToString();
                    txtQtyBPF0.Focus();
                }
                else
                {
                    txtQtyBPF.Text = (Convert.ToInt32(txtQtyBPF0.Text)).ToString();
                    txtQtyBPF0.Focus();
                }
            }
            catch { }
        }
        protected void txtQtyBPU0_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (RB1000.Checked == true)
                {
                    txtQtyBPU.Text = (Convert.ToInt32(txtQtyBPU0.Text) / 2).ToString();
                    txtQtyBPU0.Focus();
                }
                else
                {
                    txtQtyBPU.Text = (Convert.ToInt32(txtQtyBPU0.Text)).ToString();
                    txtQtyBPU0.Focus();
                }
            }
            catch { }
        }
        protected void txtQtyMutasi0_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[row.RowIndex].FindControl("txtQtyMutasi0");
            CheckBox chkMutasi0 = (CheckBox)GridViewTerimaBP.Rows[row.RowIndex].FindControl("chkMutasi0");
            int jumlah = 0;
            int lastindex = 0;
            int kali = 0;
            if (IsNumeric(txtPengali.Text) == false)
                txtPengali.Text = "1";
            kali = Convert.ToInt32(txtPengali.Text);
            //txtQtyMutasi.Text = "0";
            for (int i = 0; i <= GridViewTerimaBP.Rows.Count - 1; i++)
            {
                txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[i].FindControl("txtQtyMutasi0");
                chkMutasi0 = (CheckBox)GridViewTerimaBP.Rows[i].FindControl("chkMutasi0");
                if (chkMutasi0.Checked == false)
                    txtQtyMutasi0.Text = string.Empty;
                else
                {
                    if (txtQtyMutasi0.Text != string.Empty)
                    {
                        jumlah = jumlah + int.Parse(txtQtyMutasi0.Text);
                        lastindex = i;
                        if (Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[9].Text) < Convert.ToInt32(txtQtyMutasi0.Text))
                            txtQtyMutasi0.Text = string.Empty;
                    }
                }
            }
            GridViewTerimaBP.SelectedIndex = lastindex;
            txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[lastindex].FindControl("txtQtyMutasi0");
            txtQtyMutasi0.Focus();
            txtQtyAsal0.Text = jumlah.ToString();
            txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
        }
        protected void txtQtyAsal0_TextChanged(object sender, EventArgs e)
        {
            try
            {
                #region viewgrid
                ArrayList arrTerima = new ArrayList();
                ArrayList arrTerima1 = new ArrayList();
                T3_RekapFacade rekap = new T3_RekapFacade();
                string criteria = string.Empty;
                string tglproduksi = DateTime.Parse(txtDate.Text).ToString("yyyyMMdd");
                string likepartno = string.Empty;
                string sumber = string.Empty;
                int totalmutasi = Convert.ToInt32(txtQtyAsal0.Text);
                string strtglproduksi = "AND CONVERT(char(8), A.tglproduksi, 112) = '";
                string palet = string.Empty;
                if (txtnopalet.Text != string.Empty)
                    palet = " and A.palet='" + txtnopalet.Text + "' ";
                if (ChkTglProduksi0.Checked == false)
                    arrTerima = rekap.RetrieveByTglTerimaBP(strtglproduksi + tglproduksi + "'", txtPartnoAsal0.Text, txtlokAsal0.Text);
                else
                    arrTerima = rekap.RetrieveByTglTerimaBP(" ", txtPartnoAsal0.Text, txtlokAsal0.Text);
                int totalarray = 0;
                foreach (T3_Rekap t3rekap in arrTerima)
                {
                    totalarray = totalarray + t3rekap.QtyInTrm;
                    arrTerima1.Add(t3rekap);
                    if (totalarray > totalmutasi)
                        break;
                }
                GridViewTerimaBP.DataSource = arrTerima1;
                GridViewTerimaBP.DataBind();
                #endregion

                int kali = 0;
                if (IsNumeric(txtPengali.Text) == false)
                    txtPengali.Text = "1";
                kali = Convert.ToInt32(txtPengali.Text);
                if (txtQtyAsal0.Text == string.Empty)
                    txtQtyAsal0.Text = "0";
                if (txtQtyPOK0.Text == string.Empty)
                    txtQtyPOK0.Text = "0";
                txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
                txtPartnoPOK0.Focus();
                int jumlah = Convert.ToInt32(txtQtyAsal0.Text);
                int total = 0;

                for (int i = 0; i <= GridViewTerimaBP.Rows.Count - 1; i++)
                {
                    TextBox txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[i].FindControl("txtQtyMutasi0");
                    CheckBox chkMutasi0 = (CheckBox)GridViewTerimaBP.Rows[i].FindControl("chkMutasi0");
                    if (Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[9].Text) < jumlah)
                    {
                        chkMutasi0.Checked = true;
                        txtQtyMutasi0.Text = (Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[9].Text)).ToString();
                        jumlah = jumlah - (Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[9].Text));
                        total = total + (Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[9].Text));
                    }
                    else
                    {
                        chkMutasi0.Checked = true;
                        txtQtyMutasi0.Text = jumlah.ToString();
                        total = total + jumlah;
                        break;
                    }
                }
                txtQtyAsal0.Text = total.ToString();
                if (total == 0)
                {
                    txtQtyAsal0.Focus();
                    LCQtyBS1.Text = "0";
                    LCPartnoBS1.Text = "-";
                    LCQtyBS2.Text = "0";
                    LCPartnoBS2.Text = "-";
                    return;
                }
                txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
                LTotVolume.Text = (Convert.ToInt32(txtQtyAsal0.Text) * Convert.ToDecimal(LVolumeAsal.Text)).ToString();
                //LCQtyBS1.Text = "0";
                if (txtQtyPOK0.Text != string.Empty && txtPartnoPOK0.Text != string.Empty)
                {
                    FC_Items items = new FC_Items();
                    FC_ItemsFacade itemsF = new FC_ItemsFacade();
                    items = itemsF.RetrieveByPartNo(txtPartnoPOK0.Text);
                    if (RBSimetris.Checked == true)
                        txtPengali.Text = (Math.Truncate(Convert.ToDecimal(LVolumeAsal.Text) / Convert.ToDecimal(items.Volume))).ToString();
                    else
                        txtPengali.Text = "1";
                    kali = Convert.ToInt32(txtPengali.Text);
                    txtPartnoPOK0.Focus();
                    txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
                    LTotVolume.Text = (Convert.ToDecimal(LTotVolume.Text) - (kali * items.Volume)).ToString();
                    AutoBS();
                }
            }
            catch { }
        }
        protected void txtQtyPOK0_TextChanged(object sender, EventArgs e)
        {
            int kali = 0;
            if (IsNumeric(txtPengali.Text) == false)
                txtPengali.Text = "1";
            kali = Convert.ToInt32(txtPengali.Text);
            if (txtQtyAsal0.Text == string.Empty)
                txtQtyAsal0.Text = "0";
            if (txtQtyPOK0.Text == string.Empty)
                txtQtyPOK0.Text = "0";
            CalculateVolume();
            if (RBSimetris.Checked == true)
                btnTansfer2.Focus();
            else
                txtPartnoPOK2.Focus();
        }
        protected void txtQtyPBP0_TextChanged(object sender, EventArgs e)
        {
            int kali = 0;
            if (IsNumeric(txtPengali.Text) == false)
                txtPengali.Text = "1";
            kali = Convert.ToInt32(txtPengali.Text);
            if (txtQtyAsal0.Text == string.Empty)
                txtQtyAsal0.Text = "0";
            if (txtQtyPOK0.Text == string.Empty)
                txtQtyPOK0.Text = "0";
            txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
        }
        protected void txtQtyPBS0_TextChanged(object sender, EventArgs e)
        {
            int kali = 0;
            if (IsNumeric(txtPengali.Text) == false)
                txtPengali.Text = "1";
            kali = Convert.ToInt32(txtPengali.Text);
            if (txtQtyAsal0.Text == string.Empty)
                txtQtyAsal0.Text = "0";
            if (txtQtyPOK0.Text == string.Empty)
                txtQtyPOK0.Text = "0";
            txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
            btnTansfer2.Focus();
        }
        protected void btnTansfer2_ServerClick(object sender, EventArgs e)
        {
            if (RBAsimetris.Checked == true)
                Asimetris();
            else
            {
                #region Verifikasi Closing Periode
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = DateTime.Parse(txtDateSerah.Text).Year;
                int Bulan = DateTime.Parse(txtDateSerah.Text).Month;
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                    return;
                }
                #endregion
                int kali = 0;
                if (IsNumeric(txtPengali.Text) == false)
                    txtPengali.Text = "1";
                kali = Convert.ToInt32(txtPengali.Text);
                string strError = string.Empty;
                int intresult = 0;
                Users users = (Users)Session["Users"];
                ArrayList arrrekapK = new ArrayList();
                ArrayList arrrekapT = new ArrayList();
                T3_Simetris simetris = new T3_Simetris();

                if (txtQtyAsal0.Text == string.Empty)
                    txtQtyAsal0.Text = "0";
                if (txtQtyPOK0.Text == string.Empty)
                    txtQtyPOK0.Text = "0";
                int cek = 0;
                #region Validasi
                if (txtPartnoAsal0.Text == string.Empty)
                    return;
                if (txtQtyAsal0.Text == string.Empty)
                    txtQtyAsal0.Text = "0";
                if (txtQtyPOK0.Text == string.Empty)
                    txtQtyPOK0.Text = "0";
                if (Convert.ToInt32(txtQtyAsal0.Text) > 0)
                {
                    cek = cekpartno(txtPartnoAsal0.Text);
                    if (cek > 0)
                    {
                        DisplayAJAXMessage(this, "Partno : " + txtPartnoAsal0.Text + " belum tersedia");
                        return;
                    }
                    cek = ceklok(txtlokAsal0.Text);
                    if (cek > 0)
                    {
                        DisplayAJAXMessage(this, "lok : " + txtlokAsal0.Text + " belum tersedia");
                        return;
                    }
                }
                if (Convert.ToInt32(txtQtyPOK0.Text) > 0)
                {
                    cek = cekpartno(txtPartnoPOK0.Text);
                    if (cek > 0)
                    {
                        DisplayAJAXMessage(this, "Partno : " + txtPartnoPOK0.Text + " belum tersedia");
                        return;
                    }
                    cek = ceklok(txtlokPOK0.Text);
                    if (cek > 0)
                    {
                        DisplayAJAXMessage(this, "lok : " + txtlokPOK0.Text + " belum tersedia");
                        return;
                    }
                }
                #endregion

                FC_Items itemsK = new FC_Items();
                FC_ItemsFacade itemsKF = new FC_ItemsFacade();
                itemsK = itemsKF.RetrieveByPartNo(txtPartnoAsal0.Text);

                FC_Lokasi lokK = new FC_Lokasi();
                FC_LokasiFacade lokKF = new FC_LokasiFacade();
                lokK = lokKF.RetrieveByLokasi(txtlokAsal0.Text);

                FC_Items itemsT = new FC_Items();
                FC_ItemsFacade itemsTF = new FC_ItemsFacade();
                itemsT = itemsTF.RetrieveByPartNo(txtPartnoPOK0.Text);

                FC_Lokasi lokT = new FC_Lokasi();
                FC_LokasiFacade lokTF = new FC_LokasiFacade();
                lokT = lokTF.RetrieveByLokasi(txtlokPOK0.Text);

                int luas1 = itemsK.Panjang * itemsK.Lebar;
                int luas2 = itemsT.Panjang * itemsT.Lebar;
                int serahIDAsal = 0;
                decimal HPPAsal = 0;

                for (int i = 0; i <= GridViewTerimaBP.Rows.Count - 1; i++)
                {
                    TextBox txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[i].FindControl("txtQtyMutasi0");
                    CheckBox chkMutasi0 = (CheckBox)GridViewTerimaBP.Rows[i].FindControl("chkMutasi0");
                    if (chkMutasi0.Checked == true)
                    {
                        #region rekam rekap keluar
                        T3_RekapFacade rekapFacade = new T3_RekapFacade();//t3_rekap
                        FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
                        T3_Serah t3serahK = new T3_Serah();//Asal Partno
                        T3_SerahFacade t3serahKF = new T3_SerahFacade();
                        t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
                        if (t3serahK.ItemID == 0)
                        {
                            t3serahK.Flag = "kurang";
                            t3serahK.LokID = lokK.ID;
                            t3serahK.ItemID = itemsK.ID;
                            t3serahK.GroupID = itemsK.GroupID;
                            t3serahK.Qty = 0;
                            t3serahK.HPP = 0;
                            t3serahK.CreatedBy = users.UserName;
                            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                            transManager.BeginTransaction();
                            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahK);
                            intresult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return;
                            }
                            transManager.CommitTransaction();
                            transManager.CloseConnection();
                            t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
                        }
                        serahIDAsal = t3serahK.ID;
                        int laststock1 = t3serahK.Qty;
                        decimal lastAvgHPP1 = t3serahK.HPP;
                        HPPAsal = lastAvgHPP1;
                        t3serahK.Flag = "kurang";
                        t3serahK.Qty = Convert.ToInt32(txtQtyMutasi0.Text);
                        t3serahK.CreatedBy = users.UserName;

                        T3_Rekap rekapK = new T3_Rekap();//asal partno qtyout
                        rekapK.DestID = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[1].Text);
                        rekapK.SerahID = t3serahK.ID;
                        rekapK.T1serahID = 0;
                        rekapK.GroupID = itemsK.GroupID;
                        rekapK.LokasiID = t3serahK.LokID;
                        rekapK.ItemIDSer = t3serahK.ItemID;
                        rekapK.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                        rekapK.QtyInTrm = 0;
                        rekapK.QtyOutTrm = Convert.ToInt32(txtQtyMutasi0.Text);
                        rekapK.HPP = lastAvgHPP1;
                        rekapK.CreatedBy = users.UserName;
                        rekapK.Keterangan = txtPartnoPOK0.Text;
                        rekapK.SA = laststock1;
                        rekapK.Process = "Simetris";
                        rekapK.CutQty = Convert.ToInt32(txtQtyMutasi0.Text);
                        rekapK.CutLevel = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[12].Text) + 1;
                        rekapK.ID = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[0].Text);
                        arrrekapK.Add(rekapK);
                        #endregion
                        //proses lokasi akhir
                        /*dapatkan stock partno tujuan*/
                        #region rekam rekap terima
                        T3_Serah t3serahT = new T3_Serah();//terima Partno
                        T3_SerahFacade t3serahTF = new T3_SerahFacade();
                        t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK0.Text, txtlokPOK0.Text);
                        if (t3serahT.ItemID == 0)
                        {
                            t3serahT.Flag = "tambah";
                            t3serahT.LokID = lokT.ID;
                            t3serahT.ItemID = itemsT.ID;
                            t3serahT.GroupID = itemsT.GroupID;
                            t3serahT.Qty = 0;
                            t3serahT.HPP = 0;
                            t3serahT.CreatedBy = users.UserName;
                            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                            transManager.BeginTransaction();
                            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                            intresult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return;
                            }
                            transManager.CommitTransaction();
                            transManager.CloseConnection();
                            t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK0.Text, txtlokPOK0.Text);
                        }
                        decimal lastAvgHPP2 = t3serahT.HPP;
                        int laststock2 = t3serahT.Qty;
                        t3serahT.Flag = "tambah";
                        t3serahT.ItemID = itemsT.ID;
                        t3serahT.GroupID = itemsT.GroupID;
                        t3serahT.ID = t3serahT.ID;
                        t3serahT.LokID = lokT.ID;
                        t3serahT.Qty = Convert.ToInt32(txtQtyMutasi0.Text) * kali;
                        decimal HPPnewItem = (luas2 / luas1) * lastAvgHPP1;
                        t3serahT.HPP = HPPnewItem;
                        t3serahT.CreatedBy = users.UserName;

                        T3_Rekap rekapT = new T3_Rekap();//terima partno qtyin
                        rekapT.DestID = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[1].Text);
                        rekapT.SerahID = t3serahK.ID;
                        rekapT.GroupID = itemsT.GroupID;
                        rekapT.T1serahID = 0;
                        rekapT.LokasiID = lokT.ID;
                        rekapT.ItemIDSer = itemsT.ID;
                        rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                        rekapT.QtyInTrm = Convert.ToInt32(txtQtyMutasi0.Text) * kali;
                        rekapT.QtyOutTrm = 0;
                        rekapT.HPP = HPPnewItem;
                        rekapT.GroupID = 0;
                        rekapT.CreatedBy = users.UserName;
                        rekapT.Keterangan = txtPartnoAsal0.Text;
                        rekapT.Process = "Simetris";
                        rekapT.SA = laststock2;
                        rekapT.CutQty = 0;
                        rekapT.CutLevel = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[12].Text) + 1;
                        rekapT.ID = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[0].Text);
                        arrrekapT.Add(rekapT);
                        #endregion
                    }
                }
                #region Automatic Penerimaan BP1
                int newitemID = 0;
                if (LCPartnoBS1.Text != string.Empty && LCQtyBS1.Text != string.Empty)
                {
                    FC_Items itemsTBS = new FC_Items();
                    itemsTBS = itemsTF.RetrieveByPartNo(LCPartnoBS1.Text);
                    FC_Lokasi lokTBS = new FC_Lokasi();
                    lokTBS = lokTF.RetrieveByLokasi(txtlokPOK0.Text);
                    T3_Serah t3serahT = new T3_Serah();//terima Partno
                    T3_SerahFacade t3serahTF = new T3_SerahFacade();
                    t3serahT = t3serahTF.RetrieveStockByPartno(LCPartnoBS1.Text, txtlokPOK0.Text);
                    cek = cekpartno(LCPartnoBS1.Text);
                    if (cek > 0)
                    {
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(LCPartnoBS1.Text) == 1)
                        {

                            Item.ItemTypeID = 3;
                            Item.Kode = LCPartnoBS1.Text.Substring(0, 3);
                            Item.Tebal = decimal.Parse(LCPartnoBS1.Text.Substring(6, 3)) / 10;
                            Item.Panjang = int.Parse(LCPartnoBS1.Text.Substring(9, 4));
                            Item.Lebar = int.Parse(LCPartnoBS1.Text.Substring(13, 4));
                            Item.Volume = (decimal.Parse(LCPartnoBS1.Text.Substring(6, 3)) / 1000) * (int.Parse(LCPartnoBS1.Text.Substring(9, 4)) / 1000) *
                                (int.Parse(LCPartnoBS1.Text.Substring(13, 4)) / 1000);
                            Item.Partno = LCPartnoBS1.Text;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                        }
                    }
                    if (t3serahT.ItemID == 0)
                    {
                        t3serahT.Flag = "tambah";
                        t3serahT.LokID = lokTBS.ID;
                        t3serahT.ItemID = itemsTBS.ID;
                        t3serahT.GroupID = itemsTBS.GroupID;
                        t3serahT.Qty = 0;
                        t3serahT.HPP = 0;
                        t3serahT.CreatedBy = users.UserName;
                        TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                        transManager.BeginTransaction();
                        AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                        intresult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return;
                        }
                        transManager.CommitTransaction();
                        transManager.CloseConnection();
                        t3serahT = t3serahTF.RetrieveStockByPartno(LCPartnoBS1.Text, txtlokPOK0.Text);
                    }
                    decimal lastAvgHPP3 = t3serahT.HPP;
                    int laststock2 = t3serahT.Qty;
                    t3serahT.Flag = "tambah";
                    t3serahT.ItemID = itemsTBS.ID;
                    t3serahT.GroupID = itemsTBS.GroupID;
                    t3serahT.ID = t3serahT.ID;
                    t3serahT.LokID = lokTBS.ID;
                    t3serahT.Qty = Convert.ToInt32(LCQtyBS1.Text);
                    decimal HPPnewItem = (luas2 / luas1) * HPPAsal;
                    t3serahT.HPP = HPPnewItem;
                    t3serahT.CreatedBy = users.UserName;

                    T3_Rekap rekapT = new T3_Rekap();//terima partno qtyin
                    rekapT.DestID = 0;
                    rekapT.SerahID = serahIDAsal;
                    rekapT.GroupID = itemsTBS.GroupID;
                    rekapT.T1serahID = 0;
                    rekapT.LokasiID = lokTBS.ID;
                    rekapT.ItemIDSer = itemsTBS.ID;
                    rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                    rekapT.QtyInTrm = Convert.ToInt32(LCQtyBS1.Text);
                    rekapT.QtyOutTrm = 0;
                    rekapT.HPP = HPPnewItem;
                    rekapT.GroupID = 0;
                    rekapT.CreatedBy = users.UserName;
                    rekapT.Keterangan = txtPartnoAsal0.Text;
                    rekapT.Process = "Simetris AutoBP";
                    rekapT.SA = laststock2;
                    rekapT.CutQty = 0;
                    rekapT.CutLevel = 1;
                    rekapT.ID = 0;
                    arrrekapT.Add(rekapT);
                }
                #endregion
                #region Automatic Penerimaan BP2
                if (LCPartnoBS2.Text != string.Empty && LCQtyBS2.Text != string.Empty)
                {
                    FC_Items itemsTBS = new FC_Items();
                    itemsTBS = itemsTF.RetrieveByPartNo(LCPartnoBS2.Text);
                    FC_Lokasi lokTBS = new FC_Lokasi();
                    lokTBS = lokTF.RetrieveByLokasi(txtlokPOK0.Text);
                    T3_Serah t3serahT = new T3_Serah();//terima Partno
                    T3_SerahFacade t3serahTF = new T3_SerahFacade();
                    t3serahT = t3serahTF.RetrieveStockByPartno(LCPartnoBS2.Text, txtlokPOK0.Text);
                    cek = cekpartno(LCPartnoBS1.Text);
                    if (cek > 0)
                    {
                        FC_Items Item = new FC_Items();
                        FC_ItemsFacade ItemF = new FC_ItemsFacade();
                        if (cekpartno(LCPartnoBS1.Text) == 1)
                        {

                            Item.ItemTypeID = 3;
                            Item.Kode = LCPartnoBS2.Text.Substring(0, 3);
                            Item.Tebal = decimal.Parse(LCPartnoBS2.Text.Substring(6, 3)) / 10;
                            Item.Panjang = int.Parse(LCPartnoBS2.Text.Substring(9, 4));
                            Item.Lebar = int.Parse(LCPartnoBS2.Text.Substring(13, 4));
                            Item.Volume = (decimal.Parse(LCPartnoBS2.Text.Substring(6, 3)) / 1000) * (int.Parse(LCPartnoBS2.Text.Substring(9, 4)) / 1000) *
                                (int.Parse(LCPartnoBS2.Text.Substring(13, 4)) / 1000);
                            Item.Partno = LCPartnoBS2.Text;
                            Item.ItemDesc = "Sisa Potong";
                            Item.GroupID = 0;
                            newitemID = ItemF.Insert(Item);
                        }
                    }
                    if (t3serahT.ItemID == 0)
                    {
                        t3serahT.Flag = "tambah";
                        t3serahT.LokID = lokTBS.ID;
                        t3serahT.ItemID = itemsTBS.ID;
                        t3serahT.GroupID = itemsTBS.GroupID;
                        t3serahT.Qty = 0;
                        t3serahT.HPP = 0;
                        t3serahT.CreatedBy = users.UserName;
                        TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                        transManager.BeginTransaction();
                        AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                        intresult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return;
                        }
                        transManager.CommitTransaction();
                        transManager.CloseConnection();
                        t3serahT = t3serahTF.RetrieveStockByPartno(LCPartnoBS1.Text, txtlokPOK0.Text);
                    }
                    decimal lastAvgHPP3 = t3serahT.HPP;
                    int laststock2 = t3serahT.Qty;
                    t3serahT.Flag = "tambah";
                    t3serahT.ItemID = itemsTBS.ID;
                    t3serahT.GroupID = itemsTBS.GroupID;
                    t3serahT.ID = t3serahT.ID;
                    t3serahT.LokID = lokTBS.ID;
                    t3serahT.Qty = Convert.ToInt32(LCQtyBS1.Text);
                    decimal HPPnewItem = (luas2 / luas1) * HPPAsal;
                    t3serahT.HPP = HPPnewItem;
                    t3serahT.CreatedBy = users.UserName;

                    T3_Rekap rekapT = new T3_Rekap();//terima partno qtyin
                    rekapT.DestID = 0;
                    rekapT.SerahID = serahIDAsal;
                    rekapT.GroupID = itemsTBS.GroupID;
                    rekapT.T1serahID = 0;
                    rekapT.LokasiID = lokTBS.ID;
                    rekapT.ItemIDSer = itemsTBS.ID;
                    rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                    rekapT.QtyInTrm = Convert.ToInt32(LCQtyBS1.Text);
                    rekapT.QtyOutTrm = 0;
                    rekapT.HPP = HPPnewItem;
                    rekapT.GroupID = 0;
                    rekapT.CreatedBy = users.UserName;
                    rekapT.Keterangan = txtPartnoAsal0.Text;
                    rekapT.Process = "Simetris AutoBP";
                    rekapT.SA = laststock2;
                    rekapT.CutQty = 0;
                    rekapT.CutLevel = 1;
                    rekapT.ID = 0;
                    arrrekapT.Add(rekapT);
                }
                #endregion
                T3_Serah t3serah = new T3_Serah();
                T3_SerahFacade SerahFacade = new T3_SerahFacade();
                t3serah = SerahFacade.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
                simetris.SerahID = t3serah.ID;
                simetris.LokasiID = lokT.ID;
                simetris.TglSm = DateTime.Parse(txtDateSerah.Text).Date;
                simetris.ItemID = itemsT.ID;
                simetris.QtyInSm = Convert.ToInt32(txtQtyAsal0.Text);
                simetris.QtyOutSm = Convert.ToInt32(txtQtyPOK0.Text);
                simetris.GroupID = itemsT.GroupID;
                simetris.CreatedBy = users.UserName;
                //rekam table simetris
                T3_SimetrisAutoProcessFacade SimetrisProcessFacade = new T3_SimetrisAutoProcessFacade(arrrekapK, arrrekapT, simetris);
                strError = SimetrisProcessFacade.Insert1();
                if (strError == string.Empty)
                {
                    T3_RekapFacade t3rekapF = new T3_RekapFacade();
                    foreach (T3_Rekap rekapK in arrrekapK)
                    {
                        t3rekapF.UpdateCutLevel2(rekapK.ID, rekapK.QtyOutTrm);
                    }
                    ClearPotong3();
                    DisplayAJAXMessage(this, "Data tersimpan");
                    LoadDataJemur();
                }
                LoadDataGridTerimaBP();
                if (RBSimetris.Checked == true)
                    LoadDataGridViewSimetris();
                else
                    LoadDataGridViewASimetris();
            }
            clearform();
        }
        private void Asimetris()
        {
            #region Verifikasi Closing Periode
            /**
                 * check closing periode saat ini
                 * added on 13-08-2014
                 */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToDateTime(txtDateSerah.Text).Year;
            int Bulan = Convert.ToDateTime(txtDateSerah.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion

            #region Validasi
            if (txtPartnoAsal0.Text == string.Empty)
                return;
            if (txtQtyAsal0.Text == string.Empty)
                txtQtyAsal0.Text = "0";
            if (txtQtyPOK0.Text == string.Empty)
                txtQtyPOK0.Text = "0";
            if (txtQtyPOK2.Text == string.Empty)
                txtQtyPOK2.Text = "0";
            if (txtQtyPOK3.Text == string.Empty)
                txtQtyPOK3.Text = "0";
            int cek = 0;
            if (Convert.ToInt32(txtQtyAsal0.Text) > 0 && txtPartnoAsal0.Text != string.Empty)
            {
                cek = cekpartno(txtPartnoAsal0.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + txtPartnoAsal0.Text + " belum tersedia");
                    return;
                }
                cek = ceklok(txtlokAsal0.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokAsal0.Text + " belum tersedia");
                    return;
                }
            }
            if (Convert.ToInt32(txtQtyPOK0.Text) > 0 && txtPartnoPOK0.Text != string.Empty)
            {
                cek = cekpartno(txtPartnoPOK0.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + txtPartnoPOK0.Text + " belum tersedia");
                    return;
                }
                cek = ceklok(txtlokPOK0.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokPOK0.Text + " belum tersedia");
                    return;
                }
            }
            if (Convert.ToInt32(txtQtyPOK2.Text) > 0 && txtPartnoPOK2.Text != string.Empty)
            {
                cek = cekpartno(txtPartnoPOK2.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + txtPartnoPOK2.Text + " belum tersedia");
                    return;
                }
                cek = ceklok(txtlokPOK2.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokPOK2.Text + " belum tersedia");
                    return;
                }
            }
            if (Convert.ToInt32(txtQtyPOK3.Text) > 0 && txtPartnoPOK3.Text != string.Empty)
            {
                cek = cekpartno(txtPartnoPOK3.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "Partno : " + txtPartnoPOK3.Text + " belum tersedia");
                    return;
                }
                cek = ceklok(txtlokPOK3.Text);
                if (cek > 0)
                {
                    DisplayAJAXMessage(this, "lok : " + txtlokPOK3.Text + " belum tersedia");
                    return;
                }
            }
            #endregion
            #region Rekam ArrayList Asimetris
            T3_Asimetris asimetris = new T3_Asimetris();
            ArrayList arrAsimetris = new ArrayList();
            Users users = (Users)Session["Users"];
            T3_Serah t3serah = new T3_Serah();
            T3_Rekap rekap = new T3_Rekap();
            FC_Items items0 = new FC_Items();
            FC_Items items1 = new FC_Items();
            FC_Items items2 = new FC_Items();
            FC_Items items3 = new FC_Items();
            FC_ItemsFacade Items0Facade = new FC_ItemsFacade();
            items0 = Items0Facade.RetrieveByPartNo(txtPartnoAsal0.Text);
            items1 = Items0Facade.RetrieveByPartNo(txtPartnoPOK0.Text);
            items2 = Items0Facade.RetrieveByPartNo(txtPartnoPOK2.Text);
            items3 = Items0Facade.RetrieveByPartNo(txtPartnoPOK3.Text);

            FC_Lokasi Lokasi0 = new FC_Lokasi();
            FC_Lokasi Lokasi1 = new FC_Lokasi();
            FC_Lokasi Lokasi2 = new FC_Lokasi();
            FC_Lokasi Lokasi3 = new FC_Lokasi();
            FC_LokasiFacade Lokasi0Facade = new FC_LokasiFacade();
            Lokasi0 = Lokasi0Facade.RetrieveByLokasi(txtlokAsal0.Text);
            Lokasi1 = Lokasi0Facade.RetrieveByLokasi(txtlokPOK0.Text);
            Lokasi2 = Lokasi0Facade.RetrieveByLokasi(txtlokPOK2.Text);
            Lokasi3 = Lokasi0Facade.RetrieveByLokasi(txtlokPOK3.Text);

            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            T3_Serah t3serahK0 = new T3_Serah();//Asal Partno
            T3_Serah t3serahK1 = new T3_Serah();
            T3_Serah t3serahK2 = new T3_Serah();
            T3_Serah t3serahK3 = new T3_Serah();
            T3_SerahFacade t3serahKF = new T3_SerahFacade();
            t3serahK0 = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
            t3serahK1 = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
            t3serahK2 = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
            t3serahK3 = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);

            decimal lastAvgHPP0 = t3serahK0.HPP;
            decimal lastAvgHPP1 = t3serahK1.HPP;
            decimal lastAvgHPP2 = t3serahK2.HPP;
            decimal lastAvgHPP3 = t3serahK3.HPP;
            decimal laststock0 = t3serahK0.Qty;
            decimal laststock1 = t3serahK1.Qty;
            decimal laststock2 = t3serahK2.Qty;
            decimal laststock3 = t3serahK3.Qty;
            decimal luas0 = (items0.Panjang / 1000) * (items0.Lebar / 1000);
            decimal luas1 = (items1.Panjang / 1000) * (items1.Lebar / 1000);
            decimal luas2 = (items2.Panjang / 1000) * (items2.Lebar / 1000);
            decimal luas3 = (items3.Panjang / 1000) * (items3.Lebar / 1000);
            int stock = 0;
            decimal AvgHPP = 0;
            decimal HPPnewItem = 0;
            #region Potongan ke-1
            if (items1.ID > 0)
            {
                asimetris = new T3_Asimetris();
                stock = SerahFacade.GetStock(items1.ID, Lokasi1.ID);
                asimetris.SA = stock;
                asimetris.SerahID = t3serahK0.ID; ;
                asimetris.ItemIDIn = items0.ID;
                asimetris.LokIDIn = Lokasi0.ID;
                asimetris.PartnoIn = items1.Partno;
                asimetris.LokasiIn = Lokasi1.Lokasi;
                asimetris.QtyIn = Convert.ToInt32(txtQtyAsal0.Text);
                asimetris.ItemIDOut = items1.ID; ;
                asimetris.LokIDOut = Lokasi1.ID;
                asimetris.QtyOut = Convert.ToInt32(txtQtyPOK0.Text);
                asimetris.GroupID = items1.ID;
                asimetris.PartnoOut = txtPartnoPOK0.Text;
                asimetris.LokasiOut = txtlokPOK0.Text;
                asimetris.TglTrans = Convert.ToDateTime(txtDateSerah.Text);

                AvgHPP = 0;
                HPPnewItem = (luas1 / luas0) * lastAvgHPP0;
                if (lastAvgHPP1 > 0 || laststock1 > 0)
                {
                    AvgHPP = ((lastAvgHPP1 * laststock1) + (HPPnewItem * Convert.ToInt32(txtQtyPOK0.Text))) / (laststock1 + Convert.ToInt32(txtQtyPOK0.Text));
                }
                else
                {
                    AvgHPP = HPPnewItem;
                }
                asimetris.HPP = AvgHPP;
                arrAsimetris.Add(asimetris);
            }
            #endregion
            #region Potongan ke-2
            if (items2.ID > 0)
            {
                asimetris = new T3_Asimetris();
                stock = SerahFacade.GetStock(items2.ID, Lokasi1.ID);
                asimetris.SA = stock;
                asimetris.SerahID = t3serahK0.ID; ;
                asimetris.ItemIDIn = items0.ID;
                asimetris.LokIDIn = Lokasi0.ID;
                asimetris.PartnoIn = items1.Partno;
                asimetris.LokasiIn = Lokasi1.Lokasi;
                asimetris.QtyIn = Convert.ToInt32(txtQtyAsal0.Text);
                asimetris.ItemIDOut = items2.ID; ;
                asimetris.LokIDOut = Lokasi2.ID;
                asimetris.QtyOut = Convert.ToInt32(txtQtyPOK2.Text);
                asimetris.GroupID = items2.ID;
                asimetris.PartnoOut = txtPartnoPOK2.Text;
                asimetris.LokasiOut = txtlokPOK2.Text;
                asimetris.TglTrans = Convert.ToDateTime(txtDateSerah.Text);

                AvgHPP = 0;
                HPPnewItem = (luas2 / luas0) * lastAvgHPP0;
                if (lastAvgHPP2 > 0 || laststock2 > 0)
                {
                    AvgHPP = ((lastAvgHPP2 * laststock2) + (HPPnewItem * Convert.ToInt32(txtQtyPOK2.Text))) / (laststock2 + Convert.ToInt32(txtQtyPOK2.Text));
                }
                else
                {
                    AvgHPP = HPPnewItem;
                }
                asimetris.HPP = AvgHPP;
                arrAsimetris.Add(asimetris);
            }
            #endregion
            #region Potongan ke-3
            if (items3.ID > 0)
            {
                asimetris = new T3_Asimetris();
                stock = SerahFacade.GetStock(items3.ID, Lokasi1.ID);
                asimetris.SA = stock;
                asimetris.SerahID = t3serahK0.ID; ;
                asimetris.ItemIDIn = items0.ID;
                asimetris.LokIDIn = Lokasi0.ID;
                asimetris.PartnoIn = items1.Partno;
                asimetris.LokasiIn = Lokasi1.Lokasi;
                asimetris.QtyIn = Convert.ToInt32(txtQtyAsal0.Text);
                asimetris.ItemIDOut = items3.ID; ;
                asimetris.LokIDOut = Lokasi3.ID;
                asimetris.QtyOut = Convert.ToInt32(txtQtyPOK3.Text);
                asimetris.GroupID = items3.ID;
                asimetris.PartnoOut = txtPartnoPOK3.Text;
                asimetris.LokasiOut = txtlokPOK3.Text;
                asimetris.TglTrans = Convert.ToDateTime(txtDateSerah.Text);

                AvgHPP = 0;
                HPPnewItem = (luas3 / luas0) * lastAvgHPP0;
                if (lastAvgHPP3 > 0 || laststock3 > 0)
                {
                    AvgHPP = ((lastAvgHPP3 * laststock3) + (HPPnewItem * Convert.ToInt32(txtQtyPOK3.Text))) / (laststock3 + Convert.ToInt32(txtQtyPOK3.Text));
                }
                else
                {
                    AvgHPP = HPPnewItem;
                }
                asimetris.HPP = AvgHPP;
                arrAsimetris.Add(asimetris);
            }

            #endregion
            Session["ListofAsimetris"] = arrAsimetris;
            #endregion
            #region Rekam Asimetris
            T3_Asimetris t3asimetris = new T3_Asimetris();
            T3_AsimetrisFacade t3asimetrisfacade = new T3_AsimetrisFacade();
            T3_Serah t3serahK = new T3_Serah();
            t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
            T3_Rekap rekapK = new T3_Rekap();
            T3_Serah t3serahT = new T3_Serah();
            T3_Rekap rekapT = new T3_Rekap();
            T3_RekapFacade rekapFacade = new T3_RekapFacade();
            int intdocno = t3asimetrisfacade.GetDocNo(Convert.ToDateTime(txtDateSerah.Text)) + 1;
            string docno = "A" + Convert.ToDateTime(txtDateSerah.Text).ToString("yy") + Convert.ToDateTime(txtDateSerah.Text).ToString("MM") + intdocno.ToString().PadLeft(4, '0');
            if (Session["ListofAsimetris"] != null)
            {
                arrAsimetris = (ArrayList)Session["ListofAsimetris"];
                if (arrAsimetris.Count < 2)
                {
                    DisplayAJAXMessage(this, "Jumlah transaksi minimal 2 item");
                    return;
                }
                ArrayList arrrekapK = new ArrayList();
                ArrayList arrrekapT = new ArrayList();
                int intresult = 0;

                for (int i = 0; i <= GridViewTerimaBP.Rows.Count - 1; i++)
                {
                    TextBox txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[i].FindControl("txtQtyMutasi0");
                    CheckBox chkMutasi0 = (CheckBox)GridViewTerimaBP.Rows[i].FindControl("chkMutasi0");
                    if (chkMutasi0.Checked == true)
                    {

                        #region rekam rekap keluar
                        int serahIDAsal = 0;
                        decimal HPPAsal = 0;
                        FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
                        t3serahK = new T3_Serah();
                        t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
                        if (t3serahK.ItemID == 0)
                        {
                            t3serahK.Flag = "kurang";
                            t3serahK.LokID = Lokasi0.ID;
                            t3serahK.ItemID = items0.ID;
                            t3serahK.GroupID = items0.GroupID;
                            t3serahK.Qty = 0;
                            t3serahK.HPP = 0;
                            t3serahK.CreatedBy = users.UserName;
                            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                            transManager.BeginTransaction();
                            AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahK);
                            intresult = absTrans.Insert(transManager);
                            if (absTrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                return;
                            }
                            transManager.CommitTransaction();
                            transManager.CloseConnection();
                            t3serahK = t3serahKF.RetrieveStockByPartno(txtPartnoAsal0.Text, txtlokAsal0.Text);
                        }
                        serahIDAsal = t3serahK.ID;
                        laststock1 = t3serahK.Qty;
                        lastAvgHPP1 = t3serahK.HPP;
                        HPPAsal = lastAvgHPP1;
                        t3serahK.Flag = "kurang";
                        t3serahK.Qty = Convert.ToInt32(txtQtyMutasi0.Text);
                        t3serahK.CreatedBy = users.UserName;

                        rekapK = new T3_Rekap();//asal partno qtyout
                        rekapK.DestID = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[1].Text);
                        rekapK.SerahID = t3serahK.ID;
                        rekapK.T1serahID = 0;
                        rekapK.GroupID = items0.GroupID;
                        rekapK.LokasiID = t3serahK.LokID;
                        rekapK.ItemIDSer = t3serahK.ItemID;
                        rekapK.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                        rekapK.QtyInTrm = 0;
                        rekapK.QtyOutTrm = Convert.ToInt32(txtQtyMutasi0.Text);
                        rekapK.HPP = lastAvgHPP1;
                        rekapK.CreatedBy = users.UserName;
                        rekapK.Keterangan = txtPartnoPOK0.Text;
                        rekapK.Process = "Asimetris";
                        rekapK.CutQty = Convert.ToInt32(txtQtyMutasi0.Text);
                        rekapK.CutLevel = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[12].Text) + 1;
                        rekapK.ID = Convert.ToInt32(GridViewTerimaBP.Rows[i].Cells[0].Text);
                        arrrekapK.Add(rekapK);

                        #endregion
                        #region rekam rekap terima1
                        if (items1.ID > 0 && Lokasi1.ID > 0)
                        {
                            t3serahT = new T3_Serah();//terima Partno
                            T3_SerahFacade t3serahTF = new T3_SerahFacade();
                            t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK0.Text, txtlokPOK0.Text);
                            if (t3serahT.ItemID == 0)
                            {
                                t3serahT.Flag = "tambah";
                                t3serahT.LokID = Lokasi1.ID;
                                t3serahT.ItemID = items1.ID;
                                t3serahT.GroupID = items1.GroupID;
                                t3serahT.Qty = 0;
                                t3serahT.HPP = 0;
                                t3serahT.CreatedBy = users.UserName;
                                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                                transManager.BeginTransaction();
                                AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                                intresult = absTrans.Insert(transManager);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return;
                                }
                                transManager.CommitTransaction();
                                transManager.CloseConnection();
                                t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK0.Text, txtlokPOK0.Text);
                            }
                            lastAvgHPP2 = t3serahT.HPP;
                            laststock2 = t3serahT.Qty;
                            t3serahT.Flag = "tambah";
                            t3serahT.ItemID = items1.ID;
                            t3serahT.GroupID = items1.GroupID;
                            t3serahT.ID = t3serahT.ID;
                            t3serahT.LokID = Lokasi1.ID;
                            t3serahT.Qty = (Convert.ToInt32(txtQtyPOK0.Text) / Convert.ToInt32(txtQtyAsal0.Text)) * Convert.ToInt32(txtQtyMutasi0.Text);
                            HPPnewItem = (luas1 / luas0) * lastAvgHPP1;
                            t3serahT.HPP = HPPnewItem;
                            t3serahT.CreatedBy = users.UserName;

                            rekapT = new T3_Rekap();//terima partno qtyin
                            rekapT.DestID = rekapK.DestID;
                            rekapT.SerahID = t3serahK.ID;
                            rekapT.GroupID = items1.GroupID;
                            rekapT.T1serahID = 0;
                            rekapT.LokasiID = Lokasi1.ID;
                            rekapT.ItemIDSer = items1.ID;
                            rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                            rekapT.QtyInTrm = (Convert.ToInt32(txtQtyPOK0.Text) / Convert.ToInt32(txtQtyAsal0.Text)) * Convert.ToInt32(txtQtyMutasi0.Text);
                            rekapT.QtyOutTrm = 0;
                            rekapT.HPP = HPPnewItem;
                            rekapT.GroupID = 0;
                            rekapT.CreatedBy = users.UserName;
                            rekapT.Keterangan = txtPartnoAsal0.Text;
                            rekapT.Process = "Asimetris";
                            rekapT.CutQty = rekapT.QtyInTrm;
                            arrrekapT.Add(rekapT);
                        }

                        #endregion
                        #region rekam rekap terima2
                        if (items2.ID > 0 && Lokasi2.ID > 0)
                        {
                            t3serahT = new T3_Serah();//terima Partno
                            T3_SerahFacade t3serahTF = new T3_SerahFacade();
                            t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK2.Text, txtlokPOK2.Text);
                            if (t3serahT.ItemID == 0)
                            {
                                t3serahT.Flag = "tambah";
                                t3serahT.LokID = Lokasi2.ID;
                                t3serahT.ItemID = items2.ID;
                                t3serahT.GroupID = items2.GroupID;
                                t3serahT.Qty = 0;
                                t3serahT.HPP = 0;
                                t3serahT.CreatedBy = users.UserName;
                                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                                transManager.BeginTransaction();
                                AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                                intresult = absTrans.Insert(transManager);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return;
                                }
                                transManager.CommitTransaction();
                                transManager.CloseConnection();
                                t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK2.Text, txtlokPOK2.Text);
                            }
                            lastAvgHPP2 = t3serahT.HPP;
                            laststock2 = t3serahT.Qty;
                            t3serahT.Flag = "tambah";
                            t3serahT.ItemID = items2.ID;
                            t3serahT.GroupID = items2.GroupID;
                            t3serahT.ID = t3serahT.ID;
                            t3serahT.LokID = Lokasi2.ID;
                            t3serahT.Qty = (Convert.ToInt32(txtQtyPOK2.Text) / Convert.ToInt32(txtQtyAsal0.Text)) * Convert.ToInt32(txtQtyMutasi0.Text);
                            HPPnewItem = (luas2 / luas0) * lastAvgHPP1;
                            t3serahT.HPP = HPPnewItem;
                            t3serahT.CreatedBy = users.UserName;

                            rekapT = new T3_Rekap();//terima partno qtyin
                            rekapT.DestID = rekapK.DestID;
                            rekapT.SerahID = t3serahK.ID;
                            rekapT.GroupID = items2.GroupID;
                            rekapT.T1serahID = 0;
                            rekapT.LokasiID = Lokasi2.ID;
                            rekapT.ItemIDSer = items2.ID;
                            rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                            rekapT.QtyInTrm = (Convert.ToInt32(txtQtyPOK2.Text) / Convert.ToInt32(txtQtyAsal0.Text)) * Convert.ToInt32(txtQtyMutasi0.Text);
                            rekapT.QtyOutTrm = 0;
                            rekapT.HPP = HPPnewItem;
                            rekapT.GroupID = 0;
                            rekapT.CreatedBy = users.UserName;
                            rekapT.Keterangan = txtPartnoAsal0.Text;
                            rekapT.Process = "Asimetris";
                            rekapT.CutQty = rekapT.QtyInTrm;
                            arrrekapT.Add(rekapT);
                        }
                        #endregion
                        #region rekam rekap terima3
                        if (items3.ID > 0 && Lokasi3.ID > 0)
                        {
                            t3serahT = new T3_Serah();//terima Partno
                            T3_SerahFacade t3serahTF = new T3_SerahFacade();
                            t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK3.Text, txtlokPOK3.Text);
                            if (t3serahT.ItemID == 0)
                            {
                                t3serahT.Flag = "tambah";
                                t3serahT.LokID = Lokasi3.ID;
                                t3serahT.ItemID = items3.ID;
                                t3serahT.GroupID = items3.GroupID;
                                t3serahT.Qty = 0;
                                t3serahT.HPP = 0;
                                t3serahT.CreatedBy = users.UserName;
                                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                                transManager.BeginTransaction();
                                AbstractTransactionFacadeF absTrans = new T3_SerahFacade(t3serahT);
                                intresult = absTrans.Insert(transManager);
                                if (absTrans.Error != string.Empty)
                                {
                                    transManager.RollbackTransaction();
                                    return;
                                }
                                transManager.CommitTransaction();
                                transManager.CloseConnection();
                                t3serahT = t3serahTF.RetrieveStockByPartno(txtPartnoPOK3.Text, txtlokPOK3.Text);
                            }
                            lastAvgHPP2 = t3serahT.HPP;
                            laststock2 = t3serahT.Qty;
                            t3serahT.Flag = "tambah";
                            t3serahT.ItemID = items3.ID;
                            t3serahT.GroupID = items3.GroupID;
                            t3serahT.ID = t3serahT.ID;
                            t3serahT.LokID = Lokasi3.ID;
                            t3serahT.Qty = (Convert.ToInt32(txtQtyPOK3.Text) / Convert.ToInt32(txtQtyAsal0.Text)) * Convert.ToInt32(txtQtyMutasi0.Text);
                            HPPnewItem = (luas3 / luas0) * lastAvgHPP1;
                            t3serahT.HPP = HPPnewItem;
                            t3serahT.CreatedBy = users.UserName;

                            rekapT = new T3_Rekap();//terima partno qtyin
                            rekapT.DestID = rekapK.DestID;
                            rekapT.SerahID = t3serahK.ID;
                            rekapT.GroupID = items3.GroupID;
                            rekapT.T1serahID = 0;
                            rekapT.LokasiID = Lokasi3.ID;
                            rekapT.ItemIDSer = items3.ID;
                            rekapT.TglTrm = DateTime.Parse(txtDateSerah.Text).Date;
                            rekapT.QtyInTrm = (Convert.ToInt32(txtQtyPOK3.Text) / Convert.ToInt32(txtQtyAsal0.Text)) * Convert.ToInt32(txtQtyMutasi0.Text);
                            rekapT.QtyOutTrm = 0;
                            rekapT.HPP = HPPnewItem;
                            rekapT.GroupID = 0;
                            rekapT.CreatedBy = users.UserName;
                            rekapT.Keterangan = txtPartnoAsal0.Text;
                            rekapT.Process = "Asimetris";
                            rekapT.CutQty = rekapT.QtyInTrm;
                            arrrekapT.Add(rekapT);
                        }
                        #endregion
                    }
                }

                T3_AsimetrisAutoProcessFacade AsimetrisProcessFacade = new T3_AsimetrisAutoProcessFacade(t3serahK, arrAsimetris, arrrekapK, arrrekapT, docno, Convert.ToDateTime(txtDateSerah.Text), users.UserName.Trim());
                string strError = AsimetrisProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    DisplayAJAXMessage(this, "Data tersimpan");
                    LoadDataJemur();
                    //clearform();
                }
            }
            #endregion
        }
        protected void ChkMutasi0_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[row.RowIndex].FindControl("txtQtyMutasi0");
            CheckBox chkMutasi0 = (CheckBox)GridViewTerimaBP.Rows[row.RowIndex].FindControl("chkMutasi0");
            int jumlah = 0;
            int lastindex = 0;
            if (chkMutasi0.Checked == true)
            {
                if (txtQtyAsal.Text != string.Empty)
                    jumlah = int.Parse(txtQtyAsal0.Text);
                txtQtyMutasi0.Text = GridViewTerimaBP.Rows[row.RowIndex].Cells[9].Text;
                jumlah = jumlah + int.Parse(txtQtyMutasi0.Text);
                txtPartnoAsal.Text = GridViewTerimaBP.Rows[row.RowIndex].Cells[7].Text;
                txtlokAsal0.Text = GridViewTerimaBP.Rows[row.RowIndex].Cells[8].Text;
                lastindex = row.RowIndex;
            }
            else
            {
                //jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                txtQtyMutasi0.Text = "0";
                for (int i = 0; i <= GridViewTerimaBP.Rows.Count - 1; i++)
                {
                    txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[i].FindControl("txtQtyMutasi0");
                    chkMutasi0 = (CheckBox)GridViewTerimaBP.Rows[i].FindControl("chkMutasi0");
                    if (chkMutasi0.Checked == false)
                        txtQtyMutasi0.Text = string.Empty;
                    if (txtQtyMutasi0.Text != string.Empty)
                    {
                        jumlah = jumlah + int.Parse(txtQtyMutasi0.Text);
                    }
                }
            }
            GridViewTerimaBP.SelectedIndex = lastindex;
            txtQtyMutasi0 = (TextBox)GridViewTerimaBP.Rows[lastindex].FindControl("txtQtyMutasi0");
            txtQtyMutasi0.Focus();
            txtQtyAsal0.Text = jumlah.ToString();
            int kali = 0;
            if (IsNumeric(txtPengali.Text) == false)
                txtPengali.Text = "1";
            kali = Convert.ToInt32(txtPengali.Text);
            if (txtQtyAsal0.Text == string.Empty)
                txtQtyAsal0.Text = "0";

            txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
            IsiPartnoLisplankLvl2(GridViewTerimaBP.Rows[0].Cells[7].Text.Substring(0, 3));
        }
        protected void txtPartnoAsal0_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteExtender14.ContextKey = txtPartnoAsal0.Text;
                FC_Items items = new FC_Items();
                FC_ItemsFacade itemsF = new FC_ItemsFacade();
                items = itemsF.RetrieveByPartNo(txtPartnoAsal0.Text);
                if (txtPartnoAsal0.Text.Substring(3, 3) == "-S-")
                {
                    DisplayAJAXMessage(this, "UpGrade produk BS, tidak diizinkan..");
                    txtPartnoAsal0.Text = string.Empty;
                    return;
                }
                //if (items.ID > 0)
                //{
                //    txtPartnoPOK0_AutoCompleteExtender.ContextKey = " and substring(partno,1,3)='" + txtPartnoAsal0.Text.Substring(0, 3) +
                //            "' and panjang*lebar<" + (items.Panjang * items.Lebar) / Convert.ToInt32(txtPengali.Text) + " and panjang*lebar>=" + Convert.ToInt32((items.Panjang *
                //            items.Lebar) / (Convert.ToInt32(txtPengali.Text) + 0.7)) + " and tebal = " + Convert.ToInt32(items.Tebal) + " ";
                //    txtPartnoPBP0_AutoCompleteExtender.ContextKey = " and substring(partno,1,3)='" + txtPartnoAsal0.Text.Substring(0, 3) +
                //            "' and panjang*lebar<" + (items.Panjang * items.Lebar) / Convert.ToInt32(txtPengali.Text) + " and panjang*lebar>=" + Convert.ToInt32((items.Panjang *
                //            items.Lebar) / (Convert.ToInt32(txtPengali.Text) + 0.7)) + " and tebal = " + Convert.ToInt32(items.Tebal) + " ";
                //}
                if (items.ID > 0)
                {
                    txtPartnoPOK0_AutoCompleteExtender.ContextKey = " and substring(partno,1,3)='" + txtPartnoAsal0.Text.Substring(0, 3) + "' and tebal = " + Convert.ToInt32(items.Tebal) + " ";
                    txtPartnoPOK_AutoCompleteExtender0.ContextKey = " and substring(partno,1,3)='" + txtPartnoAsal0.Text.Substring(0, 3) + "' and tebal = " + Convert.ToInt32(items.Tebal) + " ";
                    txtPartnoPOK1_AutoCompleteExtender0.ContextKey = " and substring(partno,1,3)='" + txtPartnoAsal0.Text.Substring(0, 3) + "' and tebal = " + Convert.ToInt32(items.Tebal) + " ";
                }
                txtlokAsal0.Focus();
                //LoadDataGridTerimaBP();
                LVolumeAsal.Text = items.Volume.ToString();
                LoadDataGridViewtrans();
                txtQtyAsal0.Text = "0";
            }
            catch { }
        }
        protected void txtlokAsal0_TextChanged(object sender, EventArgs e)
        {
            //LoadDataGridTerimaBP();
            txtQtyAsal0.Focus();
            LoadDataGridViewtrans();
        }
        protected void txtPengali_TextChanged(object sender, EventArgs e)
        {
            int kali = 0;
            if (IsNumeric(txtQtyAsal0.Text) == false)
                txtQtyAsal0.Text = "0";
            if (IsNumeric(txtPengali.Text) == false)
                txtPengali.Text = "1";
            kali = Convert.ToInt32(txtPengali.Text);
            txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
        }
        protected void txtPartnoPOK0_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPartnoPOK0.Text == string.Empty || LTotVolume.Text == string.Empty || LVolumePotong.Text == string.Empty)
                    return;
                int kali = 0;
                txtPengali.Text = "1";
                FC_Items items0 = new FC_Items();
                FC_ItemsFacade items0F = new FC_ItemsFacade();
                items0 = items0F.RetrieveByPartNo(txtPartnoAsal0.Text);
                FC_Items items = new FC_Items();
                FC_ItemsFacade itemsF = new FC_ItemsFacade();
                items = itemsF.RetrieveByPartNo(txtPartnoPOK0.Text);
                if (items.Tebal != items0.Tebal)
                {
                    DisplayAJAXMessage(this, "Ketebalan harus sama");
                    txtPartnoPOK0.Text = string.Empty;
                    txtPartnoPOK0.Focus();
                    return;
                }
                if (txtPartnoPOK.Text.IndexOf("-S-") != -1)
                {
                    DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                    txtPartnoPOK0.Text = string.Empty;
                    txtPartnoPOK0.Focus();
                    return;
                }
                //if (RBSimetris.Checked == true)
                //{
                txtPengali.Text = ((Math.Truncate(Convert.ToDecimal(items0.Lebar) / Convert.ToDecimal(items.Lebar))) *
                (Math.Truncate(Convert.ToDecimal(items0.Panjang) / Convert.ToDecimal(items.Panjang)))).ToString();
                //}
                //else
                //{
                //    txtPengali.Text = "1";
                //}
                kali = Convert.ToInt32(txtPengali.Text);
                txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
                CalculateVolume();
                if (Convert.ToDecimal(LTotVolume.Text) < 0)
                {
                    txtPartnoPOK.Text = string.Empty;
                    LVolumePotong.Text = "0";
                    CalculateVolume();
                }
                txtlokPOK0.Focus();
            }
            catch { }
        }
        private void CalculateVolume()
        {
            int kali = 0;
            //if (IsNumeric(txtPengali.Text) == false)
            FC_Items items0 = new FC_Items();
            FC_Items items1 = new FC_Items();
            FC_Items items2 = new FC_Items();
            FC_Items items3 = new FC_Items();

            FC_Items itemsBS1 = new FC_Items();
            FC_Items itemsBS2 = new FC_Items();
            FC_Items itemsBS3 = new FC_Items();
            FC_Items itemsBS4 = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            items0 = itemsF.RetrieveByPartNo(txtPartnoAsal0.Text);
            items1 = itemsF.RetrieveByPartNo(txtPartnoPOK0.Text);
            items2 = itemsF.RetrieveByPartNo(txtPartnoPOK2.Text);
            items3 = itemsF.RetrieveByPartNo(txtPartnoPOK3.Text);

            if (txtPengali.Text == "0")
            {
                txtPartnoPOK0.Text = string.Empty;
                txtPartnoPOK0.Focus();
                return;
            }
            decimal total = 0;
            decimal LVolume2 = 0;
            decimal LVolume3 = 0;
            decimal LVolumeBS1 = 0;
            decimal LVolumeBS2 = 0;
            decimal LVolumeBS3 = 0;
            decimal LVolumeBS4 = 0;
            if (items1.Lebar == 0 || items1.Panjang == 0)
                return;
            //if (RBSimetris.Checked == true)
            //{
            txtPengali.Text = ((Math.Truncate(Convert.ToDecimal(items0.Lebar) / Convert.ToDecimal(items1.Lebar))) *
                (Math.Truncate(Convert.ToDecimal(items0.Panjang) / Convert.ToDecimal(items1.Panjang)))).ToString();
            kali = Convert.ToInt32(txtPengali.Text);
            if (RBSimetris.Checked == true)
                txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
            LTotVolume.Text = (Convert.ToDecimal(LVolumeAsal.Text) * Convert.ToInt32(txtQtyAsal0.Text)).ToString();
            LVolumePotong.Text = (items1.Volume).ToString();
            if (txtQtyPOK2.Text == string.Empty)
                txtQtyPOK2.Text = "0";
            if (items2.ID > 0 && Convert.ToInt32(txtQtyPOK2.Text) > 0)
            {
                LVolume2 = Convert.ToInt32(txtQtyPOK2.Text) * items2.Volume;
            }
            if (txtQtyPOK3.Text == string.Empty)
                txtQtyPOK3.Text = "0";
            if (items3.ID > 0 && Convert.ToInt32(txtQtyPOK3.Text) > 0)
            {
                LVolume3 = Convert.ToInt32(txtQtyPOK3.Text) * items3.Volume;
            }
            if (ChkConvertBS.Checked == true)
            {
                AutoBS();
                itemsBS1 = itemsF.RetrieveByPartNo(LCPartnoBS1.Text);
                if (itemsBS1.ID > 0)
                    LVolumeBS1 = itemsBS1.Volume * Convert.ToInt32(LCQtyBS1.Text);
                else
                {
                    if (cekpartno(LCPartnoBS1.Text) == 1 && LCPartnoBS1.Text.Trim().Length > 10)
                    {
                        itemsBS1.ItemTypeID = 3;
                        itemsBS1.Kode = LCPartnoBS1.Text.Substring(0, 3);
                        itemsBS1.Tebal = decimal.Parse(LCPartnoBS1.Text.Substring(6, 3)) / 10;
                        itemsBS1.Lebar = int.Parse(LCPartnoBS1.Text.Substring(9, 4));
                        itemsBS1.Panjang = int.Parse(LCPartnoBS1.Text.Substring(13, 4));
                        itemsBS1.Volume = itemsBS1.Tebal / 1000 * itemsBS1.Panjang / 1000 * itemsBS1.Lebar / 1000;
                        itemsBS1.Partno = LCPartnoBS1.Text;
                        itemsBS1.ItemDesc = "Sisa Potong";
                        itemsBS1.GroupID = 0;
                        itemsF.Insert(itemsBS1);
                    }
                    else
                    {
                        itemsBS1 = itemsF.RetrieveByPartNo(LCPartnoBS1.Text);
                        LVolumeBS1 = itemsBS1.Volume * Convert.ToInt32(LCQtyBS1.Text);
                    }
                }
                itemsBS2 = itemsF.RetrieveByPartNo(LCPartnoBS2.Text);
                if (itemsBS2.ID > 0)
                    LVolumeBS2 = itemsBS2.Volume * Convert.ToInt32(LCQtyBS2.Text);
                else
                {
                    if (cekpartno(LCPartnoBS2.Text) == 1 && LCPartnoBS2.Text.Trim().Length > 10)
                    {
                        itemsBS2.ItemTypeID = 3;
                        itemsBS2.Kode = LCPartnoBS2.Text.Substring(0, 3);
                        itemsBS2.Tebal = decimal.Parse(LCPartnoBS2.Text.Substring(6, 3)) / 10;
                        itemsBS2.Lebar = int.Parse(LCPartnoBS2.Text.Substring(9, 4));
                        itemsBS2.Panjang = int.Parse(LCPartnoBS2.Text.Substring(13, 4));
                        itemsBS2.Volume = itemsBS2.Tebal / 1000 * itemsBS2.Panjang / 1000 * itemsBS2.Lebar / 1000;
                        itemsBS2.Partno = LCPartnoBS2.Text;
                        itemsBS2.ItemDesc = "Sisa Potong";
                        itemsBS2.GroupID = 0;
                        itemsF.Insert(itemsBS2);
                    }
                    else
                    {
                        itemsBS2 = itemsF.RetrieveByPartNo(LCPartnoBS2.Text);
                        LVolumeBS2 = itemsBS2.Volume * Convert.ToInt32(LCQtyBS2.Text);
                    }
                }
                itemsBS3 = itemsF.RetrieveByPartNo(LCPartnoBS3.Text);
                if (itemsBS3.ID > 0)
                    LVolumeBS3 = itemsBS3.Volume * Convert.ToInt32(LCQtyBS3.Text);
                else
                {
                    if (cekpartno(LCPartnoBS3.Text) == 1 && LCPartnoBS3.Text.Trim().Length > 10)
                    {
                        itemsBS3.ItemTypeID = 3;
                        itemsBS3.Kode = LCPartnoBS3.Text.Substring(0, 3);
                        itemsBS3.Tebal = decimal.Parse(LCPartnoBS3.Text.Substring(6, 3)) / 10;
                        itemsBS3.Lebar = int.Parse(LCPartnoBS3.Text.Substring(9, 4));
                        itemsBS3.Panjang = int.Parse(LCPartnoBS3.Text.Substring(13, 4));
                        itemsBS3.Volume = itemsBS3.Tebal / 1000 * itemsBS3.Panjang / 1000 * itemsBS3.Lebar / 1000;
                        itemsBS3.Partno = LCPartnoBS3.Text;
                        itemsBS3.ItemDesc = "Sisa Potong";
                        itemsBS3.GroupID = 0;
                        itemsF.Insert(itemsBS3);
                    }
                    else
                    {
                        itemsBS3 = itemsF.RetrieveByPartNo(LCPartnoBS3.Text);
                        LVolumeBS3 = itemsBS2.Volume * Convert.ToInt32(LCQtyBS3.Text);
                    }
                }
                itemsBS4 = itemsF.RetrieveByPartNo(LCPartnoBS4.Text);
                if (itemsBS4.ID > 0)
                    LVolumeBS4 = itemsBS4.Volume * Convert.ToInt32(LCQtyBS4.Text);
                else
                {
                    if (cekpartno(LCPartnoBS4.Text) == 1 && LCPartnoBS3.Text.Trim().Length > 10)
                    {
                        itemsBS4.ItemTypeID = 3;
                        itemsBS4.Kode = LCPartnoBS4.Text.Substring(0, 3);
                        itemsBS4.Tebal = decimal.Parse(LCPartnoBS4.Text.Substring(6, 3)) / 10;
                        itemsBS4.Lebar = int.Parse(LCPartnoBS4.Text.Substring(9, 4));
                        itemsBS4.Panjang = int.Parse(LCPartnoBS4.Text.Substring(13, 4));
                        itemsBS4.Volume = itemsBS4.Tebal / 1000 * itemsBS4.Panjang / 1000 * itemsBS4.Lebar / 1000;
                        itemsBS4.Partno = LCPartnoBS4.Text;
                        itemsBS4.ItemDesc = "Sisa Potong";
                        itemsBS4.GroupID = 0;
                        itemsF.Insert(itemsBS4);
                    }
                    else
                    {
                        itemsBS4 = itemsF.RetrieveByPartNo(LCPartnoBS4.Text);
                        LVolumeBS4 = itemsBS4.Volume * Convert.ToInt32(LCQtyBS4.Text);
                    }
                }
            }
            total = (Convert.ToDecimal(LTotVolume.Text) - (Convert.ToDecimal(txtQtyPOK0.Text) *
                Convert.ToDecimal(LVolumePotong.Text))) - LVolume2 - LVolume3 - LVolumeBS1 - LVolumeBS2 - LVolumeBS3 - LVolumeBS4;
            LTotVolume.Text = total.ToString();

            #region remark dulu
            // }
            //else
            //{
            //    txtPengali.Text = "1";
            //    kali = Convert.ToInt32(txtPengali.Text);
            //    if (txtQtyPOK0.Text == string.Empty || txtPartnoPOK0.Text == string.Empty)
            //        txtQtyPOK0.Text = "0";
            //    if (txtQtyPOK2.Text == string.Empty || txtPartnoPOK2.Text == string.Empty)
            //        txtQtyPOK2.Text = "0";
            //    if (txtQtyPOK3.Text == string.Empty || txtPartnoPOK3.Text == string.Empty)
            //        txtQtyPOK3.Text = "0";
            //    //txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) )).ToString();
            //    //txtQtyPOK2.Text = ((Convert.ToInt32(txtQtyAsal0.Text) )).ToString();
            //    //txtQtyPOK3.Text = ((Convert.ToInt32(txtQtyAsal0.Text) )).ToString();
            //    LTotVolume.Text = (Convert.ToDecimal(LVolumeAsal.Text) * Convert.ToInt32(txtQtyAsal0.Text)).ToString();
            //    LVolumePotong.Text = (items1.Volume).ToString();

            //    LVolumePotong3.Text = (items3.Volume).ToString();
            //    total = (Convert.ToDecimal(LTotVolume.Text) - (Convert.ToDecimal(txtQtyPOK0.Text) * Convert.ToDecimal(LVolumePotong.Text))
            //        - (Convert.ToDecimal(txtQtyPOK2.Text) * Convert.ToDecimal(LVolumePotong2.Text))
            //        - (Convert.ToDecimal(txtQtyPOK3.Text) * Convert.ToDecimal(LVolumePotong3.Text)));
            //    if (total < 0)
            //    {
            //        txtPartnoPOK0.Text = string.Empty;
            //        txtQtyPOK0.Text = "0";
            //        txtPartnoPOK2.Text = string.Empty;
            //        txtQtyPOK2.Text = "0";
            //        txtPartnoPOK3.Text = string.Empty;
            //        txtQtyPOK3.Text = "0";
            //        LVolumePotong.Text = "0";
            //        LVolumePotong2.Text = "0";
            //        LVolumePotong3.Text = "0";
            //        DisplayAJAXMessage(this, "Quantity potong terlalu banyak");
            //        total = Convert.ToDecimal(LVolumeAsal.Text) * Convert.ToInt32(txtQtyAsal0.Text);
            //        LCPartnoBS1.Text = string.Empty;
            //        LCQtyBS1.Text = string.Empty;
            //        LCPartnoBS2.Text = string.Empty;
            //        LCQtyBS2.Text = string.Empty;
            //    }
            //    LTotVolume.Text = total.ToString();
            //    AutoBS();        
            //}
            #endregion
            if (LTotVolume.Text == string.Empty)
                LTotVolume.Text = "0";
        }
        protected void txtlokPOK0_TextChanged(object sender, EventArgs e)
        {
            txtQtyPOK0.Focus();
            if (RBSuperPanel.Checked == true)
            {
                txtlokOK.Text = txtlokPOK.Text;
            }
        }
        protected void txtSerah_TextChanged(object sender, EventArgs e)
        {
            txtQtyOK.Text = "0";
            txtQtyKW.Text = "0";
            txtQtyBPF.Text = "0";
            txtQtyBPU.Text = "0";
            txtQtyBPS.Text = "0";
            if (Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text) < Convert.ToInt32(txtSerah.Text))
                txtSerah.Text = (Convert.ToInt32(LQtyIn.Text) - Convert.ToInt32(LQtyOut.Text)).ToString();
            if (RBLisflank.Checked == true)
            {

                int kali = pengali();
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                if (txtQtyPBP.Text != string.Empty && txtQtyPOK.Text != string.Empty)
                {
                    if (RBLisPlank2.Checked == true)
                    {
                        txtQtyAsal.Text = txtSerah.Text;
                        txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text) * kali) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                    }
                    else
                    {
                        txtQtyAsal.Text = txtSerah.Text;
                        txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                    }
                }
                txtQtyKW.Text = txtSerah.Text;
            }
            if (RBSuperPanel.Checked == true)
            {
                txtQtyOK.Text = txtSerah.Text;
                if (txtQtyAsal.Text == string.Empty)
                    txtQtyAsal.Text = "0";
                if (txtQtyPOK.Text == string.Empty)
                    txtQtyPOK.Text = "0";
                if (txtQtyPBP.Text == string.Empty)
                    txtQtyPBP.Text = "0";
                if (txtQtyPBP.Text != string.Empty && txtQtyPOK.Text != string.Empty)
                {
                    txtQtyAsal.Text = txtSerah.Text;
                    txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                }
                txtQtyOK.Text = txtQtyPOK.Text;
                txtQtyBPF.Text = txtQtyPBP.Text;
            }
            AutoBST1ALL();
            ArrayList arrbsauto = new ArrayList();
            arrbsauto = (ArrayList)Session["arrbsauto"];
            GridBSAuto.DataSource = arrbsauto;
            GridBSAuto.DataBind();
        }
        protected void RBSuperPanel_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSuperPanel.Checked == true)
            {
                PanelPartnoOK.Visible = false;
                PanelPartnoKW.Visible = true;
                PanelPartnoBPFinish.Visible = false;
                PanelPartnoBPUnFinish.Visible = true;
                PanelSample.Visible = true;
                PanelOpt.Visible = false;
                PCut2H.Visible = false;
                IsiPartno(0);
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                PanelLevel.Visible = false;
                PanelInput.Visible = true;
                PanelInput2.Visible = true;
                RBSource1.Checked = true;
                RBSource1.Visible = true;
                RBSource2.Visible = false;
                RBSource2.Checked = false;
                Panel9.Visible = true;
                PanelPartnoBPUnFinish0.Visible = true;
            }
        }
        protected void RBLisflank_CheckedChanged(object sender, EventArgs e)
        {
            RBLisPlank1.Checked = true;
            RBLisPlank2.Checked = false;
            RBSource1.Checked = true;
            RBSource1.Visible = true;
            RBSource2.Visible = false;
            RBSource2.Checked = false;
            PanelInput2.Visible = false;
            if (RBLisflank.Checked == true)
            {
                PanelPartnoOK.Visible = false;
                PanelPartnoKW.Visible = false;
                PanelPartnoBPFinish.Visible = false;
                PanelPartnoBPUnFinish.Visible = false;
                PanelSample.Visible = true;
                //PanelInput1.Visible = true;

                PanelOpt.Visible = true;
                PCut2H.Visible = false;
                //RBKali12.Checked = true;
                IsiPartno(0);
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                //Panel6.Visible = false;
                PanelLevel.Visible = true;
                PanelPartnoBPUnFinish0.Visible = false;
                RBKali4.Checked = true;
            }
            //tambahan dibawah (hide option level 2)
            try
            {
                ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
                int i = 0;
                int totlari = 0;
                foreach (T1_Jemur pelarian in arrPelarian)
                {
                    TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                    totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                    i++;
                }
                int kali = pengali();
                if (RBLisPlank1.Checked == true)
                {
                    PanelOpt.Visible = true;
                    PanelPartnoKW.Visible = false;
                    PanelPartnoOK.Visible = false;
                    PanelPartnoBPFinish.Visible = true;
                    PanelPartnoBPUnFinish.Visible = false;
                    PanelSample.Visible = true;
                    PanelInput.Visible = true;

                    PCut2H.Visible = false;
                    IsiPartno(0);
                    txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text) - totlari).ToString();
                    txtQtyKW.Text = txtQtyPOK.Text;
                    txtQtyBPF.Text = txtQtyPBP.Text;
                }
            }
            catch
            {
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ArrayList arrPelarian = new ArrayList();
                if (Session["arrpelarian"] != null)
                    arrPelarian = (ArrayList)Session["arrpelarian"];
                if (arrPelarian.Count > 0)
                {
                    T1_Jemur jemur = (T1_Jemur)arrPelarian[e.Row.RowIndex];
                    TextBox txtLari = (TextBox)e.Row.FindControl("txtPartNo");
                    TextBox txtqtyju = (TextBox)e.Row.FindControl("txtQtyJU");
                    txtLari.Text = jemur.Partno;
                    txtqtyju.Text = jemur.QtyIn.ToString();
                    //txtqtyju.Attributes.Add("onBlur", "onChange");
                }

            }
        }
        protected void GridViewPOK_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ArrayList arrPOK = new ArrayList();
                if (Session["arrPOK"] != null)
                    arrPOK = (ArrayList)Session["arrPOK"];
                if (arrPOK.Count > 0)
                {
                    T1_Jemur POK = (T1_Jemur)arrPOK[e.Row.RowIndex];
                    TextBox txtPOK = (TextBox)e.Row.FindControl("txtPartnoPOK");
                    TextBox txtlokPOK = (TextBox)e.Row.FindControl("txtlokPOK");
                    TextBox txtqtyPOK = (TextBox)e.Row.FindControl("txtqtyPOK");

                    txtPOK.Text = POK.Partno;
                    txtlokPOK.Text = POK.Lokasi.ToString();
                    txtqtyPOK.Text = POK.QtyIn.ToString();
                }
            }
        }
        protected void txtlokPOK_TextChanged(object sender, EventArgs e)
        {

        }
        protected void RBSuperFlank_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSuperFlank.Checked == true)
            {
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                IsiPartno1(0);
            }
        }
        protected void RBSimpleFlank_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSimpleFlank.Checked == true)
            {
                string kode = string.Empty;
                if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                    kode = LPartno.Text.Substring(0, 3);
                else
                    kode = "INT";
                IsiPartnoLisplankLvl2(kode);
                IsiPartno1(0);
            }
        }
        protected void GridViewokbp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtQtyBPS_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            int i = 0;
            int totlari = 0;
            if (Session["arrpelarian"] == null)
                return;
            foreach (T1_Jemur pelarian in arrPelarian)
            {
                TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                i++;
            }
            if (txtSerah.Text.Trim() == string.Empty)
                txtSerah.Text = "0";
            if (txtQtyKW.Text.Trim() == string.Empty)
                txtQtyKW.Text = "0";
            if (txtQtyOK.Text.Trim() == string.Empty)
                txtQtyOK.Text = "0";
            if (txtQtyBPF.Text.Trim() == string.Empty)
                txtQtyBPF.Text = "0";
            if (txtQtyBPU.Text.Trim() == string.Empty)
                txtQtyBPU.Text = "0";
            if (txtQtyBPS.Text.Trim() == string.Empty)
                txtQtyBPS.Text = "0";
            //if (RBSuperPanel.Checked == true && RB9Mili.Checked == true)
            if (Convert.ToInt32(txtQtyOK.Text) > 0)
            {
                txtQtyOK.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyBPF.Text)
                - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                //txtQtyOK.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyKW.Text) - Convert.ToInt32(txtQtyBPF.Text)
                //    - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                txtQtyPOK.Text = txtQtyOK.Text;
            }
            else
            {
                txtQtyKW.Text = (Convert.ToInt32(txtSerah.Text) - Convert.ToInt32(txtQtyOK.Text) - Convert.ToInt32(txtQtyBPF.Text)
                - Convert.ToInt32(txtQtyBPU.Text) - Convert.ToInt32(txtQtyBPS.Text) - totlari).ToString();
                txtQtyPOK.Text = txtQtyKW.Text;
            }
            AutoBST1ALL();
            ArrayList arrbsauto = new ArrayList();
            arrbsauto = (ArrayList)Session["arrbsauto"];
            GridBSAuto.DataSource = arrbsauto;
            GridBSAuto.DataBind();
            GridView1.Focus();
        }
        protected void RBSimetris_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSimetris.Checked == true)
            {
                PanelAsimetrisOK.Visible = false;
                PanelOtomatis.Visible = true;
                CalculateVolume();
                LoadDataGridViewSimetris();
                GridViewSimetris.Visible = true;
                GridViewAsimetris.Visible = false;
            }
        }
        protected void RBAsimetris_CheckedChanged(object sender, EventArgs e)
        {
            if (RBAsimetris.Checked == true)
            {
                PanelAsimetrisOK.Visible = true;
                PanelOtomatis.Visible = true;
                CalculateVolume();
                LoadDataGridViewASimetris();
                GridViewSimetris.Visible = false;
                GridViewAsimetris.Visible = true;
            }
        }
        protected void AutoBS()
        {
            try
            {
                ClearAutoBS();
                FC_Items items = new FC_Items();
                FC_Items itemsAsal = new FC_Items();
                FC_ItemsFacade itemsF = new FC_ItemsFacade();

                int kali = Convert.ToInt32(txtPengali.Text);
                items = itemsF.RetrieveByPartNo(txtPartnoPOK0.Text);
                itemsAsal = itemsF.RetrieveByPartNo(txtPartnoAsal0.Text);
                string partnoBS1 = string.Empty;
                string partnoBS2 = string.Empty;
                string partnoBS3 = string.Empty;
                string partnoBS4 = string.Empty;
                int panjangBS1 = 0;
                int panjangBS2 = 0;
                int panjangBS3 = 0;
                int panjangBS4 = 0;
                int lebarBS1 = 0;
                int lebarBS2 = 0;
                int lebarBS3 = 0;
                int lebarBS4 = 0;
                int luassisa = 0;

                luassisa = (itemsAsal.Lebar * itemsAsal.Panjang) - (items.Lebar * items.Panjang * kali);
                lebarBS1 = 20;
                panjangBS1 = itemsAsal.Panjang;
                lebarBS2 = itemsAsal.Lebar - (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                    Convert.ToDecimal(items.Lebar)))) - lebarBS1;
                panjangBS2 = itemsAsal.Panjang;
                lebarBS3 = 40;
                panjangBS3 = (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                    Convert.ToDecimal(items.Lebar))));
                lebarBS4 = itemsAsal.Panjang - 40 - (items.Panjang *
                    Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Panjang) / Convert.ToDecimal(items.Panjang))));
                if (lebarBS4 > 400)
                {
                    lebarBS4 = 400;
                    lebarBS3 = itemsAsal.Panjang - lebarBS4 - (items.Panjang *
                    Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Panjang) / Convert.ToDecimal(items.Panjang))));
                }
                panjangBS4 = panjangBS3;
                if (txtPartnoPOK0.Text == string.Empty)
                    return;

                if (ChkConvertBS.Checked == true)
                {
                    if (luassisa <= 0)
                        return;
                    if (panjangBS1 > 0 && lebarBS1 > 0)
                        partnoBS1 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                            lebarBS1.ToString().PadLeft(4, '0') + panjangBS1.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS1.Text = partnoBS1;

                    if (LCPartnoBS1.Text != string.Empty)
                    {
                        LCQtyBS1.Text = txtQtyAsal0.Text;
                        if (lebarBS1 > 0)
                            if (lebarBS1 < 400)
                                LCLokBS1.Text = "S99";
                            else
                                LCLokBS1.Text = "Z99";
                    }
                    luassisa = luassisa - (lebarBS1 * panjangBS1);
                    if (luassisa <= 0)
                        return;
                    if (panjangBS2 > 0 && lebarBS2 > 0)
                        partnoBS2 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                            lebarBS2.ToString().PadLeft(4, '0') + panjangBS2.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS2.Text = partnoBS2;
                    if (LCPartnoBS2.Text != string.Empty)
                    {
                        LCQtyBS2.Text = txtQtyAsal0.Text;
                        if (lebarBS2 > 0)
                            if (lebarBS2 < 400)
                                LCLokBS2.Text = "S99";
                            else
                                LCLokBS2.Text = "Z99";
                    }
                    luassisa = luassisa - (lebarBS2 * panjangBS2);
                    if (luassisa <= 0)
                        return;
                    if (panjangBS3 > 0 && lebarBS3 > 0)
                        partnoBS3 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                            lebarBS3.ToString().PadLeft(4, '0') + panjangBS3.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS3.Text = partnoBS3;
                    if (LCPartnoBS3.Text != string.Empty)
                    {
                        LCQtyBS3.Text = txtQtyAsal0.Text;
                        if (lebarBS3 > 0)
                            if (lebarBS3 < 400)
                                LCLokBS3.Text = "S99";
                            else
                                LCLokBS3.Text = "Z99";
                    }
                    luassisa = luassisa - (lebarBS3 * panjangBS3);
                    if (luassisa <= 0)
                        return;
                    if (panjangBS4 > 0 && lebarBS4 > 0)
                        partnoBS4 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                            lebarBS4.ToString().PadLeft(4, '0') + panjangBS4.ToString().PadLeft(4, '0') + "SE";
                    LCPartnoBS4.Text = partnoBS4;
                    if (LCPartnoBS4.Text != string.Empty)
                    {
                        LCQtyBS4.Text = txtQtyAsal0.Text;
                        if (lebarBS4 > 0)
                            if (lebarBS4 < 400)
                                LCLokBS4.Text = "S99";
                            else
                                LCLokBS4.Text = "Z99";
                    }
                }
                //else
                //{
                //    LCQtyBS1.Text = "0";
                //    LCPartnoBS1.Text = string.Empty;
                //    kali = 0;
                //    //if (IsNumeric(txtPengali.Text) == false)
                //    txtPengali.Text = "1";
                //    FC_Items items0 = new FC_Items();
                //    FC_ItemsFacade items0F = new FC_ItemsFacade();
                //    items0 = items0F.RetrieveByPartNo(txtPartnoA.Text);
                //    items = itemsF.RetrieveByPartNo(txtPartnoB.Text);
                //    if (RBSimetris.Checked == true)
                //        txtPengali.Text = ((Math.Truncate(Convert.ToDecimal(items0.Lebar) / Convert.ToDecimal(items.Lebar))) *
                //        (Math.Truncate(Convert.ToDecimal(items0.Panjang) / Convert.ToDecimal(items.Panjang)))).ToString();
                //    else
                //        txtPengali.Text = "1";
                //    kali = Convert.ToInt32(txtPengali.Text);
                //    txtQtyPOK0.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
                //    LTotVolume.Text = (Convert.ToDecimal(LVolumeAsal.Text) * Convert.ToInt32(txtQtyAsal0.Text)).ToString();
                //    LVolumePotong.Text = (items.Volume).ToString();
                //    LTotVolume.Text = (Convert.ToDecimal(LTotVolume.Text) - (Convert.ToDecimal(txtQtyPOK0.Text) * Convert.ToDecimal(LVolumePotong.Text))).ToString();
                //}
            }
            catch { }
            ClearAutoBS();
            try
            {
                FC_Items items = new FC_Items();
                FC_Items itemsAsal = new FC_Items();
                FC_ItemsFacade itemsF = new FC_ItemsFacade();
                int kali = Convert.ToInt32(txtPengali.Text);
                items = itemsF.RetrieveByPartNo(txtPartnoPOK0.Text);
                itemsAsal = itemsF.RetrieveByPartNo(txtPartnoAsal0.Text);
                string partnoBS1 = string.Empty;
                string partnoBS2 = string.Empty;
                string partnoBS3 = string.Empty;
                string partnoBS4 = string.Empty;
                int panjangBS1 = 0;
                int panjangBS2 = 0;
                int panjangBS3 = 0;
                int panjangBS4 = 0;
                int lebarBS1 = 0;
                int lebarBS2 = 0;
                int lebarBS3 = 0;
                int lebarBS4 = 0;
                int luassisa = 0;
                if (RBPotong1.Checked == true)
                {
                    luassisa = (itemsAsal.Lebar * itemsAsal.Panjang) - (items.Lebar * items.Panjang * kali);
                    lebarBS1 = 20;
                    panjangBS1 = itemsAsal.Panjang;
                    lebarBS2 = itemsAsal.Lebar - (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                        Convert.ToDecimal(items.Lebar)))) - lebarBS1;
                    panjangBS2 = itemsAsal.Panjang;
                    lebarBS3 = 40;
                    panjangBS3 = (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                        Convert.ToDecimal(items.Lebar))));
                    lebarBS4 = itemsAsal.Panjang - 40 - (items.Panjang *
                        Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Panjang) / Convert.ToDecimal(items.Panjang))));
                    if (lebarBS4 > 400)
                    {
                        lebarBS4 = 400;
                        lebarBS3 = itemsAsal.Panjang - lebarBS4 - (items.Panjang *
                        Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Panjang) / Convert.ToDecimal(items.Panjang))));
                    }
                    panjangBS4 = panjangBS3;
                    //decimal total = 0;
                    if (txtPartnoPOK0.Text == string.Empty)
                        return;

                    if (ChkConvertBS.Checked == true)
                    {
                        if (luassisa <= 0)
                            return;
                        if (panjangBS1 > 0 && lebarBS1 > 0)
                            partnoBS1 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS1.ToString().PadLeft(4, '0') + panjangBS1.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS1.Text = partnoBS1;

                        if (LCPartnoBS1.Text != string.Empty)
                        {
                            LCQtyBS1.Text = txtQtyAsal0.Text;
                            if (lebarBS1 > 0)
                                if (lebarBS1 < 400)
                                    LCLokBS1.Text = "BSAUTO";
                                else
                                    LCLokBS1.Text = "Z99";
                        }
                        luassisa = luassisa - (lebarBS1 * panjangBS1);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS2 > 0 && lebarBS2 > 0)
                            partnoBS2 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS2.ToString().PadLeft(4, '0') + panjangBS2.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS2.Text = partnoBS2;
                        if (LCPartnoBS2.Text != string.Empty)
                        {
                            LCQtyBS2.Text = txtQtyAsal0.Text;
                            if (lebarBS2 > 0)
                                if (lebarBS2 < 400)
                                    LCLokBS2.Text = "BSAUTO";
                                else
                                    LCLokBS2.Text = "Z99";
                        }
                        luassisa = luassisa - (lebarBS2 * panjangBS2);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS3 > 0 && lebarBS3 > 0)
                            partnoBS3 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS3.ToString().PadLeft(4, '0') + panjangBS3.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS3.Text = partnoBS3;
                        if (LCPartnoBS3.Text != string.Empty)
                        {
                            LCQtyBS3.Text = txtQtyAsal0.Text;
                            if (lebarBS3 > 0)
                                if (lebarBS3 < 400)
                                    LCLokBS3.Text = "BSAUTO";
                                else
                                    LCLokBS3.Text = "Z99";
                        }
                        luassisa = luassisa - (lebarBS3 * panjangBS3);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS4 > 0 && lebarBS4 > 0)
                            partnoBS4 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS4.ToString().PadLeft(4, '0') + panjangBS4.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS4.Text = partnoBS4;
                        if (LCPartnoBS4.Text != string.Empty)
                        {
                            LCQtyBS4.Text = txtQtyAsal0.Text;
                            if (lebarBS4 > 0)
                                if (lebarBS4 < 400)
                                    LCLokBS4.Text = "BSAUTO";
                                else
                                    LCLokBS4.Text = "Z99";
                        }
                    }
                }
                else
                {
                    luassisa = (itemsAsal.Lebar * itemsAsal.Panjang) - (items.Lebar * items.Panjang * kali);
                    lebarBS1 = 20;
                    panjangBS1 = itemsAsal.Panjang;
                    lebarBS2 = itemsAsal.Lebar - (items.Lebar * Convert.ToInt32(Math.Truncate(Convert.ToDecimal(itemsAsal.Lebar) /
                        Convert.ToDecimal(items.Lebar)))) - lebarBS1;
                    panjangBS2 = itemsAsal.Panjang;
                    lebarBS3 = 200;
                    panjangBS3 = items.Lebar;
                    lebarBS4 = itemsAsal.Panjang - (items.Panjang * kali) - lebarBS3;
                    panjangBS4 = panjangBS3;

                    if (txtPartnoPOK0.Text == string.Empty)
                        return;

                    if (ChkConvertBS.Checked == true)
                    {
                        if (luassisa <= 0)
                            return;
                        if (panjangBS1 > 0 && lebarBS1 > 0)
                            partnoBS1 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS1.ToString().PadLeft(4, '0') + panjangBS1.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS1.Text = partnoBS1;

                        if (LCPartnoBS1.Text != string.Empty)
                        {
                            LCQtyBS1.Text = txtQtyAsal0.Text;
                            if (lebarBS1 > 0)
                                if (lebarBS1 < 400)
                                    LCLokBS1.Text = "BSAUTO";
                                else
                                    LCLokBS1.Text = "Z99";
                        }
                        luassisa = luassisa - ((lebarBS1 * panjangBS1) * 2);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS2 > 0 && lebarBS2 > 0)
                            partnoBS2 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS2.ToString().PadLeft(4, '0') + panjangBS2.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS2.Text = partnoBS2;
                        if (LCPartnoBS2.Text != string.Empty)
                        {
                            LCQtyBS2.Text = txtQtyAsal0.Text;
                            if (lebarBS2 > 0)
                                if (lebarBS2 < 400)
                                    LCLokBS2.Text = "BSAUTO";
                                else
                                    LCLokBS2.Text = "Z99";
                        }
                        luassisa = luassisa - (lebarBS2 * panjangBS2);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS3 > 0 && lebarBS3 > 0)
                            partnoBS3 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS3.ToString().PadLeft(4, '0') + panjangBS3.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS3.Text = partnoBS3;
                        if (LCPartnoBS3.Text != string.Empty)
                        {
                            LCQtyBS3.Text = txtQtyAsal0.Text;
                            if (lebarBS3 > 0)
                                if (lebarBS3 < 400)
                                    LCLokBS3.Text = "BSAUTO";
                                else
                                    LCLokBS3.Text = "Z99";
                        }
                        luassisa = luassisa - (lebarBS3 * panjangBS3);
                        if (luassisa <= 0)
                            return;
                        if (panjangBS4 > 0 && lebarBS4 > 0)
                            partnoBS4 = txtPartnoPOK0.Text.Substring(0, 3) + "-S-" + Convert.ToInt32(items.Tebal * 10).ToString().PadLeft(3, '0') +
                                lebarBS4.ToString().PadLeft(4, '0') + panjangBS4.ToString().PadLeft(4, '0') + "SE";
                        LCPartnoBS4.Text = partnoBS4;
                        if (LCPartnoBS4.Text != string.Empty)
                        {
                            LCQtyBS4.Text = txtQtyAsal0.Text;
                            if (lebarBS4 > 0)
                                if (lebarBS4 < 400)
                                    LCLokBS4.Text = "BSAUTO";
                                else
                                    LCLokBS4.Text = "Z99";
                        }
                    }
                }
            }
            catch
            {
            }
        }
        protected void ChkConvertBS_CheckedChanged(object sender, EventArgs e)
        {
            AutoBS();
            CalculateVolume();
        }
        protected void txtPartnoPOK2_TextChanged(object sender, EventArgs e)
        {
            int kali2 = 0;
            //txtPengali.Text = "1";
            FC_Items items0 = new FC_Items();
            FC_ItemsFacade items0F = new FC_ItemsFacade();
            items0 = items0F.RetrieveByPartNo(txtPartnoAsal0.Text);
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            items = itemsF.RetrieveByPartNo(txtPartnoPOK2.Text);
            LPengali2.Text = "0";
            LVolumePotong2.Text = (items.Volume).ToString();
            if (LVolumeAsal.Text != string.Empty && Convert.ToDecimal(LVolumeAsal.Text) > 0 &&
                LVolumePotong.Text != string.Empty && Convert.ToDecimal(LVolumePotong.Text) > 0 &&
                LVolumePotong2.Text != string.Empty && Convert.ToDecimal(LVolumePotong2.Text) > 0)
            {
                kali2 = Convert.ToInt32(Math.Truncate((Convert.ToDecimal(LVolumeAsal.Text) - Convert.ToDecimal(LVolumePotong.Text)) / Convert.ToDecimal(LVolumePotong2.Text)));
            }
            LPengali2.Text = kali2.ToString();
            if (items.Tebal != items0.Tebal)
            {
                DisplayAJAXMessage(this, "Ketebalan harus sama");
                txtPartnoPOK2.Text = string.Empty;
                txtPartnoPOK2.Focus();
                return;
            }
            if (RBSimetris.Checked == true)
                txtQtyPOK2.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali2)).ToString();
            else
                txtQtyPOK2.Text = ((Convert.ToInt32(txtQtyAsal0.Text))).ToString();
            CalculateVolume();
            if (Convert.ToDecimal(LTotVolume.Text) < 0)
            {
                txtPartnoPOK2.Text = string.Empty;
                LVolumePotong2.Text = string.Empty;
                CalculateVolume();
            }

            txtlokPOK2.Focus();
        }
        protected void txtPartnoPOK3_TextChanged(object sender, EventArgs e)
        {
            int kali = 0;
            txtPengali.Text = "1";
            FC_Items items0 = new FC_Items();
            FC_ItemsFacade items0F = new FC_ItemsFacade();
            items0 = items0F.RetrieveByPartNo(txtPartnoAsal0.Text);
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsF = new FC_ItemsFacade();
            items = itemsF.RetrieveByPartNo(txtPartnoPOK3.Text);
            if (items.Tebal != items0.Tebal)
            {
                DisplayAJAXMessage(this, "Ketebalan harus sama");
                txtPartnoPOK3.Text = string.Empty;
                txtlokPOK3.Focus();
                return;
            }
            kali = Convert.ToInt32(txtPengali.Text);
            txtQtyPOK3.Text = ((Convert.ToInt32(txtQtyAsal0.Text) * kali)).ToString();
            CalculateVolume();
            if (Convert.ToDecimal(LTotVolume.Text) < 0)
            {
                txtPartnoPOK3.Text = string.Empty;
                LVolumePotong3.Text = string.Empty;
                CalculateVolume();
            }
            txtlokPOK3.Focus();
        }
        protected void txtlokPOK2_TextChanged(object sender, EventArgs e)
        {
            txtQtyPOK2.Focus();
        }
        protected void txtlokPOK3_TextChanged(object sender, EventArgs e)
        {
            txtQtyPOK3.Focus();
        }
        protected void txtQtyPOK3_TextChanged(object sender, EventArgs e)
        {
            CalculateVolume();
            btnTansfer2.Focus();
        }
        protected void txtQtyPOK2_TextChanged(object sender, EventArgs e)
        {
            CalculateVolume();
            txtPartnoPOK3.Focus();
        }
        protected void GridViewAsimetris_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void GridViewAsimetris_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            GridView grv = (GridView)GridViewAsimetris.Rows[rowindex].FindControl("GridView2");
            Label lbl = (Label)GridViewAsimetris.Rows[rowindex].FindControl("Label2");
            GridViewAsimetris.Rows[rowindex].FindControl("Cancel").Visible = false;
            if (e.CommandName == "Details")
            {
                GridViewAsimetris.Rows[rowindex].FindControl("Cancel").Visible = true;
                GridViewAsimetris.Rows[rowindex].FindControl("btn_Show").Visible = false;
                ////// bind data with child gridview 
                ArrayList arrT3ASimetris = new ArrayList();
                T3_AsimetrisFacade T3ASAimetris = new T3_AsimetrisFacade();
                arrT3ASimetris = T3ASAimetris.RetrieveByDocNo(GridViewAsimetris.Rows[rowindex].Cells[0].Text);
                grv.DataSource = arrT3ASimetris;
                grv.DataBind();
                grv.Visible = true;
            }
            else
            {
                //// child gridview  display false when cancel button raise event
                grv.Visible = false;
                GridViewAsimetris.Rows[rowindex].FindControl("Cancel").Visible = false;
                GridViewAsimetris.Rows[rowindex].FindControl("btn_Show").Visible = true;
            }
        }
        private void LoadDataGridViewASimetris()
        {
            ArrayList arrT3ASimetris = new ArrayList();
            T3_AsimetrisFacade T3ASAimetris = new T3_AsimetrisFacade();
            arrT3ASimetris = T3ASAimetris.RetrieveBytgl(Convert.ToDateTime(txtDateSerah.Text).ToString("yyyyMMdd"));
            GridViewAsimetris.DataSource = arrT3ASimetris;
            GridViewAsimetris.DataBind();
        }
        private void LoadDataGridViewSimetris()
        {
            ArrayList arrT3Simetris = new ArrayList();
            T3_SimetrisFacade T3Simetris = new T3_SimetrisFacade();
            arrT3Simetris = T3Simetris.RetrieveBytgl(Convert.ToDateTime(txtDateSerah.Text).ToString("yyyyMMdd"));
            GridViewSimetris.DataSource = arrT3Simetris;
            GridViewSimetris.DataBind();
        }
        protected void btn_Show_Click(object sender, EventArgs e)
        {

        }
        protected void GridViewAsimetris_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void RBLisPlank1_CheckedChanged(object sender, EventArgs e)
        {

            RBSource1.Checked = true;
            RBSource1.Visible = true;
            RBSource2.Visible = false;
            RBSource2.Checked = false;
            LoadDataJemur();
            try
            {
                ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
                int i = 0;
                int totlari = 0;
                foreach (T1_Jemur pelarian in arrPelarian)
                {
                    TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                    totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                    i++;
                }
                int kali = pengali();
                if (RBLisPlank1.Checked == true)
                {
                    Panel9.Visible = true;
                    PanelOpt.Visible = true;
                    PanelPartnoKW.Visible = false;
                    PanelPartnoOK.Visible = false;
                    PanelPartnoBPFinish.Visible = false;
                    PanelPartnoBPUnFinish.Visible = false;
                    PanelSample.Visible = true;
                    PanelInput.Visible = true;
                    PCut2H.Visible = false;
                    IsiPartno(0);
                    txtQtyPOK.Text = ((Convert.ToInt32(txtSerah.Text)) - Convert.ToUInt32(txtQtyPBP.Text) - totlari).ToString();
                    txtQtyKW.Text = txtQtyPOK.Text;
                    txtQtyBPF.Text = txtQtyPBP.Text;
                }
            }
            catch
            {
            }
        }
        protected void RBLisPlank2_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrPelarian = (ArrayList)Session["arrpelarian"];
            int i = 0;
            int totlari = 0;
            RBSource1.Checked = false;
            RBSource1.Visible = false;
            RBSource2.Visible = true;
            RBSource2.Checked = true;
            try
            {
                foreach (T1_Jemur pelarian in arrPelarian)
                {
                    TextBox txtqtyju = (TextBox)GridView1.Rows[i].FindControl("txtQtyJU");
                    totlari = totlari + Convert.ToInt32(txtqtyju.Text);
                    i++;
                }
                int kali = pengali();
                if (RBLisPlank2.Checked == true)
                {
                    Panel9.Visible = false;
                    PanelSample.Visible = false;
                    PanelInput.Visible = true;
                    PanelOpt.Visible = false;
                    PCut2H.Visible = true;
                    IsiPartno(0);
                    txtQtyPOK.Text = (((Convert.ToInt32(txtSerah.Text) - totlari) * kali) - Convert.ToUInt32(txtQtyPBP.Text)).ToString();
                    txtQtyKW.Text = (Convert.ToInt32(txtSerah.Text) - totlari).ToString();
                    txtQtyBPF.Text = "0";
                }
            }
            catch { }
            LoadDataJemur();
            string kode = string.Empty;
            if (LPartno.Text != string.Empty && LPartno.Text.Trim().Length > 10)
                kode = LPartno.Text.Substring(0, 3);
            else
                kode = "INT";
            IsiPartnoLisplankLvl2(kode);
            RBKali4.Checked = false;
            RBKali6.Checked = false;
            RBKali12.Checked = true;
            txtnopalet.Text = string.Empty;
            //LoadDataGridTerima();
            IsiPartno1(0);
        }
        protected void txtlokPBP_TextChanged(object sender, EventArgs e)
        {
            txtQtyPBP.Focus();
            if (RBSuperPanel.Checked == true)
            {
                txtlokBPF0.Text = txtlokPBP.Text;
            }
        }
        protected void txtlokAsal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ChkHide2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide2.Checked == false)
                Panel6.Visible = false;
            else
                Panel6.Visible = true;
        }
        protected void GridViewtrans_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        private void LoadDataGridViewtrans()
        {
            ArrayList arrT3Serah = new ArrayList();
            T3_SerahFacade T3Serah = new T3_SerahFacade();
            string criteria = string.Empty;
            if (txtlokAsal0.Text != string.Empty)
                criteria = " and B.Lokasi='" + txtlokAsal0.Text.Trim() + "' ";
            //else
            if (txtPartnoAsal0.Text != string.Empty)
                criteria = criteria + " and C.PartNo='" + txtPartnoAsal0.Text.Trim() + "' ";
            //if (criteria != string.Empty)
            arrT3Serah = T3Serah.RetrieveStock(criteria);
            //Session["arrT3Serah"] = arrT3Serah;
            GridViewtrans.DataSource = arrT3Serah;
            GridViewtrans.DataBind();
        }
        protected void txtPartnoPBP_TextChanged(object sender, EventArgs e)
        {
            txtPartnoBPF.Text = txtPartnoPBP.Text;
            txtPartnoBPF0.Text = txtPartnoPBP.Text;
            txtlokPBP.Focus();
            if (txtPartnoBPF.Text.IndexOf("-S-") != -1)
            {
                DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                txtPartnoBPF.Text = string.Empty;
            }
        }
        protected void txtPartnoOK1_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoOK1.Text.IndexOf("-S-") != -1)
            {
                DisplayAJAXMessage(this, "Serah Partno BS tidak diizinkan");
                txtPartnoOK1.Text = string.Empty;
                AutoBST1ALL();
            }
        }
        protected void RBPotong1_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPotong1.Checked == true)
                AutoBS();
        }
        protected void RBPotong2_CheckedChanged(object sender, EventArgs e)
        {
            if (RBPotong2.Checked == true)
                AutoBS();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "doConfirm()", "alert('Hello World');", true);
            //Page.ClientScript.re
        }
        protected void RBSource1_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataJemur();
        }
        protected void RBSource2_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataJemur();
        }
        protected void RBKali16_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali16.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
            }
        }
        protected void RBKali17_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali17.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
            }
        }
        protected void RBKali18_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali18.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
            }
        }
        protected void txtlokOK_TextChanged(object sender, EventArgs e)
        {
            if (txtlokOK.Text.Trim().ToUpper() == "I99")
                txtPartnoOK.Text = LPartno.Text;
        }
        protected void EditOven()
        {
            if (ChkOven.Checked == true)
            {
                if (txtnopalet.Text.Trim() != string.Empty)
                {
                    EditOvenByPalet();
                    return;
                }
                GridViewOven.Visible = true;
                ArrayList arrJemur = new ArrayList();
                T1_JemurFacade Jemur = new T1_JemurFacade();
                arrJemur = Jemur.RetrieveByOven(Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd"));
                Session["arrJemur"] = arrJemur;
                GridViewOven.DataSource = arrJemur;
                GridViewOven.DataBind();
                GridViewokbp.Visible = false;
            }
            else
            {
                GridViewOven.Visible = false;
                GridViewokbp.Visible = true;
            }
        }
        protected void EditOvenByPalet()
        {
            if (ChkOven.Checked == true)
            {
                GridViewOven.Visible = true;
                ArrayList arrJemur = new ArrayList();
                T1_JemurFacade Jemur = new T1_JemurFacade();
                arrJemur = Jemur.RetrieveByOvenByPalet(Convert.ToDateTime(txtDate.Text).ToString("yyyyMMdd"), txtnopalet.Text);
                Session["arrJemur"] = arrJemur;
                GridViewOven.DataSource = arrJemur;
                GridViewOven.DataBind();
                GridViewokbp.Visible = false;
            }
            else
            {
                GridViewOven.Visible = false;
                GridViewokbp.Visible = true;
            }
        }
        protected void ChkOven_CheckedChanged(object sender, EventArgs e)
        {
            txtnopalet.Text = string.Empty;
            EditOven();
        }
        protected void GridViewOven_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ArrayList arrJemur = new ArrayList();
                if (Session["arrJemur"] != null)
                    arrJemur = (ArrayList)Session["arrJemur"];
                if (arrJemur.Count > 0)
                {
                    T1_Jemur jemur = (T1_Jemur)arrJemur[e.Row.RowIndex];
                    TextBox txtJemur = (TextBox)e.Row.FindControl("txtJemur");

                    txtJemur.Text = jemur.Oven;
                }
            }
        }
        protected void txtJemur_TextChanged(object sender, EventArgs e)
        {

            T1_JemurFacade jemurFacade = new T1_JemurFacade();
            string ID = string.Empty;
            GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;
            TextBox txtJemur = (TextBox)GridViewOven.Rows[row.RowIndex].FindControl("txtJemur");
            if (txtJemur.Text.Trim() == "1" || txtJemur.Text.Trim() == "2" || txtJemur.Text.Trim() == "3")
            {
                ID = GridViewOven.Rows[row.RowIndex].Cells[0].Text;
                jemurFacade.UpdateOven(Convert.ToInt32(ID), txtJemur.Text.Trim());
                DisplayAJAXMessage(this, "Edit palet :" + GridViewOven.Rows[row.RowIndex].Cells[4].Text + " selesai");
            }
            else
            {
                DisplayAJAXMessage(this, "Oven :" + txtJemur.Text + " belum tersedia");
            }
        }

        protected void RBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPartnoKW.Text = RBList.SelectedValue + "-M-" + LPartno.Text.Substring(6, 11) + "SE";
        }

        protected void GridViewSerah_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strError = string.Empty;
            T1_SerahFacade tserahF = new T1_SerahFacade();

            string ID = string.Empty;
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "batal")
            {
                if (GridViewSerah.Rows[rowindex].Cells[9].Text == "0")
                {
                    ID = GridViewSerah.Rows[rowindex].Cells[0].Text;
                    strError = tserahF.CancelSerah(ID);
                    if (strError == string.Empty)
                    {
                        LoadDataGridSerah(Convert.ToDateTime(txtDateSerah.Text), "serah");
                        DisplayAJAXMessage(this, "Cancel data selesai");
                    }
                    else
                        DisplayAJAXMessage(this, "Cancel data error");
                }
                else
                {
                    #region Check Status Closing
                    ClosingFacade Closing = new ClosingFacade();
                    int Tahun = Convert.ToDateTime(txtDateSerah.Text).Year;
                    int Bulan = Convert.ToDateTime(txtDateSerah.Text).Month;
                    int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                    int clsStat = Closing.GetClosingStatus("SystemClosing");
                    if (status == 1 && clsStat == 1)
                    {
                        DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                        return;
                    }
                    #endregion
                    int ceksaldo = tserahF.CekSaldoLokasi(GridViewSerah.Rows[rowindex].Cells[0].Text);
                    if (ceksaldo >= 0)
                    {
                        strError = tserahF.CancelSerahByT3(GridViewSerah.Rows[rowindex].Cells[0].Text);
                    }
                    if (strError == string.Empty)
                    {
                        LoadDataGridSerah(Convert.ToDateTime(txtDateSerah.Text), "serah");
                        DisplayAJAXMessage(this, "Cancel data selesai");
                    }
                    else
                        DisplayAJAXMessage(this, "Cancel data error");
                    //DisplayAJAXMessage(this, "Data sudah masuk tahap 3 , tidak bisa di cancel.");
                }
            }
        }

        protected void txtlokBPU_TextChanged(object sender, EventArgs e)
        {
            txtlokBPU.Text = txtlokBPU.Text.Trim().ToUpper();
            if (txtlokBPU.Text == "I99")
            {
                txtPartnoBPU.Text = txtPartnoBPU.Text.Substring(0, 4) + "1" + txtPartnoBPU.Text.Substring(5, 12);
                txtQtyBPU.Focus();
            }
            else
            {
                txtPartnoBPU.Text = txtPartnoBPU.Text.Substring(0, 4) + "P" + txtPartnoBPU.Text.Substring(5, 12);
                txtQtyBPU.Focus();
            }
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridViewAsimetris_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
        protected void txtPartnoKW1_TextChanged(object sender, EventArgs e)
        {
            AutoBST1ALL();
        }
        protected void txtPartnoBPF0_TextChanged(object sender, EventArgs e)
        {
            AutoBST1ALL();
        }
        protected void RBKali20_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali20.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
            }
        }
        protected void RBKali21_CheckedChanged(object sender, EventArgs e)
        {
            if (RBKali21.Checked == true)
            {
                IsiPartno(0);
                LoadDataJemur();
            }
        }
        protected void RBWetCut_CheckedChanged(object sender, EventArgs e)
        {
            IsiPartno(0);
        }
        protected void RBDryCut_CheckedChanged(object sender, EventArgs e)
        {
            IsiPartno(0);
        }
    }
    public class BSAuto : GRCBaseDomain
    {
        public int Qty { get; set; }
        public string Partno { get; set; }
        public string Lokasi { get; set; }
    }
}