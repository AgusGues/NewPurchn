using BusinessFacade;
using Cogs;
using DefectFacade;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.DefectMenu
{
    public partial class FormDefectNew : System.Web.UI.Page
    {
        TextBox LastTxt = new TextBox();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";

                LoadFormula();
                LoadPlantGroup();
                LoadCutterGroup();
                LoadJemurGroup();
                LoadPalet();
                LoadUkuran();
                Calendar1.SelectedDate = DateTime.Now.Date.AddDays(-1);
                LoadDataGridSerah(Calendar1.SelectedDate);
                txtdrtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
                txtsdtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
                txtDateProd.Text = DateTime.Now.Date.AddDays(-1).ToString("dd-MMM-yyyy");
                txtDatePeriksa.Text = DateTime.Now.Date.AddDays(-1).ToString("dd-MMM-yyyy");
                LoadDefectTrans(DateTime.Parse(txtDatePeriksa.Text));
                LoadDataGrid1();
                CreateTabIndex();
                ddlJemur.Attributes.Add("onblur", "__doPostBack('ddlDropDown','');");
                LastTxt.Attributes.Add("onKeyPress", "doClick('','" + Button1.ClientID + "',event)");
            }
        ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton3);
        }
        private void clearform()
        {
            ViewState["id"] = null;
            txtDefectNo.Text = string.Empty;
            //txtDatePeriksa.DateFormat = DateTime.Now.Date.ToString("dd-MM-yyyy");
            //txtDate.DateFormat = DateTime.Now.Date.ToString("dd-MM-yyyy");
            txtNoPalet.Text = string.Empty;
            ddlSearch.SelectedIndex = 0;
            txtSearch.Text = string.Empty;
            ddlJenis.SelectedIndex = 0;
            ddlFormula.SelectedIndex = 0;
            ddlUkuran.SelectedIndex = 0;
            ddlCutter.SelectedIndex = 0;
            ddlJemur.SelectedIndex = 0;
            ddlProd.SelectedIndex = 0;
            txtTotalDefect.Text = "0";
            txtTotalPotong.Text = "0";
            LoadDataGrid1();
            Session["serahid"] = null;
            PanelDef.Enabled = false;
            txtDefQty0.Text = string.Empty;
            txtDefQty1.Text = string.Empty;
            txtDefQty2.Text = string.Empty;
            txtDefQty3.Text = string.Empty;
            txtDefQty4.Text = string.Empty;
            txtDefQty5.Text = string.Empty;
            txtDefQty6.Text = string.Empty;
            txtDefQty7.Text = string.Empty;
            txtDefQty8.Text = string.Empty;
            txtDefQty9.Text = string.Empty;
            txtDefQty10.Text = string.Empty;
            txtDefQty11.Text = string.Empty;
            txtDefQty12.Text = string.Empty;
            txtDefQty13.Text = string.Empty;
            txtDefQty14.Text = string.Empty;
            txtDefQty15.Text = string.Empty;
            txtTotalBP.Text = string.Empty;
            txtTotalkw.Text = string.Empty;
        }
        private void AktivateDefect()
        {
            if (txtDateProd.Text != string.Empty && txtNoPalet.Text != string.Empty && ddlCutter.SelectedIndex > 0 &&
                ddlJemur.SelectedIndex > 0)
            {
                PanelDef.Enabled = true;
                //DataList1.Focus();
            }
        }
        protected void btnActivate_ServerClick(object sender, EventArgs e)
        {
            //btnUpdate.Disabled = false;
            AktivateDefect();
        }
        private void LoadDataGridSerah(DateTime tgl)
        {
            ArrayList arrDestacking = new ArrayList();
            BM_DestackingFacade destf = new BM_DestackingFacade();
            if (Calendar1.SelectedDate.Year > 1900)
            {
                string strSQL = "select *,Qty-QtyPotong Sisa from (SELECT A.ID,D.PlantName, B.[Group] PlantGroup, E.FormulaCode Formula, F.NoPAlet PAlet, A.TglProduksi, A.Qty, C.Lokasi, G.PartNo,A.ItemID,A.lokasiID, " +
                        "case A.status when 0 then 'Curing' when 1 then 'Jemur' when 2 then 'Serah' end Status, " +
                        "isnull((select sum(TPotong) from def_defect where [status]>-1 and destid=A.ID),0) QtyPotong FROM BM_Destacking AS A LEFT OUTER JOIN  " +
                        "BM_Formula AS E ON A.FormulaID = E.ID LEFT OUTER JOIN FC_Lokasi AS C ON A.LokasiID = C.ID LEFT OUTER JOIN " +
                        "BM_PlantGroup AS B ON A.PlantGroupID = B.ID LEFT OUTER JOIN BM_Palet AS F ON A.PaletID = F.ID LEFT OUTER JOIN " +
                        "FC_Items AS G ON A.ItemID = G.ID LEFT OUTER JOIN BM_Plant AS D ON A.PlantID = D.ID where  A.rowstatus>-1 and convert(varchar,tglProduksi,112)='" +
                        Calendar1.SelectedDate.ToString("yyyyMMdd") + "')S where Qty-QtyPotong>0 order by PlantGroup";
                SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
                sqlCon.Open();
                SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
                da.SelectCommand.CommandTimeout = 0;
                #region Code for preparing the DataTable
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
                dcol.AutoIncrement = true;
                #endregion

                GridViewSerah.DataSource = dt;

                //arrDestacking = destf.RetrieveByTglProduksi(Calendar1.SelectedDate.ToString("yyyyMMdd"));
                //Session["arrSerah"] = arrDestacking;
                //GridViewSerah.DataSource = arrDestacking;
                GridViewSerah.DataBind();
            }
        }
        private void LoadDefectTrans(DateTime tgl)
        {
            //ArrayList arrDefect= new ArrayList();
            //DefectFacades Defect = new DefectFacades();
            //arrDefect = Defect.RetrieveByTgl(tgl.ToString("yyyyMMdd"));
            //GridViewDef.DataSource = arrDefect;
            //GridViewDef.DataBind();
            //DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd");
            if (RBSerah1.Checked == true)
                loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
            else
                loadDynamicGridL(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        private void LoadSerahByPalet(DateTime tgl)
        {
            string errmessage = string.Empty;
            string lastpalet = string.Empty;
            //clearform();
            if (txtNoPalet.Text == string.Empty || txtNoPalet.Text == "____")
            {
                txtNoPalet.Text = string.Empty;
                txtNoPalet.Focus();
                return;
            }

            try
            {
                T1_Serah Serah = new T1_Serah();
                T1_SerahFacade SerahF = new T1_SerahFacade();
                T1_Jemur jemur = new T1_Jemur();
                T1_JemurFacade jemurF = new T1_JemurFacade();
                if (txtNoPalet.Text.Trim() == string.Empty)
                    return;
                if (Session["serahid"] != null)
                {
                    Serah = SerahF.RetrieveBySerahID(Convert.ToInt32(Session["serahid"].ToString()));
                    lastpalet = Serah.Palet.Trim();
                    //return;
                }

                if (RBTglProduksi.Checked == true)
                {
                    Serah = SerahF.RetrieveByPaletPDefect(tgl.ToString("yyyyMMdd"), txtNoPalet.Text);
                }
                else
                {
                    Serah = SerahF.RetrieveByPaletSDefect(tgl.ToString("yyyyMMdd"), txtNoPalet.Text);
                }
                if (Serah.Palet.Trim() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Palet " + txtNoPalet.Text.Trim() + " tgl serah : " + Calendar1.SelectedDate.ToString("dd-MMM-yyyy") + " tidak ditemukan ");
                    txtNoPalet.Text = string.Empty;
                    txtNoPalet.Focus();
                    clearform();
                    return;
                }

                if (Serah.Palet.Trim() == lastpalet)
                {
                    LoadSerahByID(Session["serahid"].ToString());
                    //Session["serahid"] = null;
                    return;
                }
                jemur = jemurF.RetrieveJemur(Serah.TglProduksi.ToString("yyyyMMdd"), txtNoPalet.Text);
                txtTotalBP.Text = Serah.QtyIn.ToString();
                txtTotalPotong.Text = jemur.QtyIn.ToString();
                txtDateProd.Text = Serah.TglProduksi.ToString("dd/MM/yyyy");
                txtDatePeriksa.Text = Serah.TglSerah.ToString("dd/MM/yyyy");
                //ddlsample.SelectedIndex = ddlsample.Items.IndexOf(ddlsample.Items.FindByText("x"));
                ddlFormula.SelectedIndex = ddlFormula.Items.IndexOf(ddlFormula.Items.FindByText(jemur.Formula.Trim()));
                ddlProd.SelectedIndex = ddlProd.Items.IndexOf(ddlProd.Items.FindByText(jemur.PlantGroup.Trim()));
                ddlJenis.SelectedIndex = ddlJenis.Items.IndexOf(ddlJenis.Items.FindByText(Serah.PartnoSer.Substring(0, 3)));
                string ukuran = Serah.PartnoDest.Substring(9, 4) + "-" + Serah.PartnoDest.Substring(13, 4);
                errmessage = "Ukuran barang " + ukuran + " belum tersedia";
                ddlUkuran.SelectedIndex = ddlUkuran.Items.IndexOf(ddlUkuran.Items.FindByText(Serah.PartnoDest.Substring(9, 4)
                    + "-" + Serah.PartnoDest.Substring(13, 4)));
                Session["serahid"] = Serah.ID;
                if (RBSerah1.Checked == true)
                    Session["destid"] = Serah.DestID;
                else
                    Session["destid"] = 0;
                LoadDataGrid1();
                PanelDef.Enabled = false;

                ArrayList arrSerah = new ArrayList();
                string criteria = string.Empty;
                if (RBTglProduksi.Checked == true)
                    arrSerah = SerahF.RetrieveForDefectPalet(tgl.ToString("yyyyMMdd"), "produksi", txtNoPalet.Text);
                else
                    arrSerah = SerahF.RetrieveForDefectPalet(tgl.ToString("yyyyMMdd"), "serah", txtNoPalet.Text);
                Session["arrSerah"] = arrSerah;
                GridViewSerah.DataSource = arrSerah;
                GridViewSerah.DataBind();
            }
            catch
            {
                DisplayAJAXMessage(this, errmessage);
                txtNoPalet.Text = string.Empty;
                txtNoPalet.Focus();
            }
        }
        private void LoadSerahByID(string ID)
        {
            string errmessage = string.Empty;
            //clearform();
            try
            {
                if (txtNoPalet.Text.Trim() == string.Empty)
                    return;
                T1_Serah Serah = new T1_Serah();
                T1_SerahFacade SerahF = new T1_SerahFacade();
                T1_Jemur jemur = new T1_Jemur();
                T1_JemurFacade jemurF = new T1_JemurFacade();
                if (RBSerah1.Checked == true)
                    Serah = SerahF.RetrieveBySerahIDDefect(Convert.ToInt32(ID));
                else
                    Serah = SerahF.RetrieveBySerahIDDefectPel(Convert.ToInt32(ID));
                if (Serah.Palet.Trim() == string.Empty)
                    return;
                jemur = jemurF.RetrieveJemur(Serah.TglProduksi.ToString("yyyyMMdd"), txtNoPalet.Text);
                if (RBSerah1.Checked == true)
                {
                    txtTotalBP.Text = Serah.QtyIn.ToString();
                    txtTotalPotong.Text = jemur.QtyIn.ToString();
                }
                if (RBSerah2.Checked == true)
                {

                    txtTotalBP.Text = Serah.QtyIn.ToString();
                    txtTotalPotong.Text = Serah.QtyIn.ToString();
                }
                txtDateProd.Text = Serah.TglProduksi.ToString("dd/MM/yyyy");
                txtDatePeriksa.Text = Serah.TglSerah.ToString("dd/MM/yyyy");
                Session["serahid"] = Serah.ID.ToString();
                if (RBSerah1.Checked == true)
                    Session["destid"] = Serah.DestID;
                else
                    Session["destid"] = 0;
                //ddlsample.SelectedIndex = ddlsample.Items.IndexOf(ddlsample.Items.FindByText("x"));
                ddlFormula.SelectedIndex = ddlFormula.Items.IndexOf(ddlFormula.Items.FindByText(jemur.Formula.Trim()));
                ddlProd.SelectedIndex = ddlProd.Items.IndexOf(ddlProd.Items.FindByText(jemur.PlantGroup.Trim()));
                ddlJenis.SelectedIndex = ddlJenis.Items.IndexOf(ddlJenis.Items.FindByText(Serah.PartnoSer.Substring(0, 3)));
                string ukuran = Serah.PartnoDest.Substring(9, 4) + "-" + Serah.PartnoDest.Substring(13, 4);
                errmessage = "Ukuran barang " + ukuran + " belum tersedia";
                ddlUkuran.SelectedIndex = ddlUkuran.Items.IndexOf(ddlUkuran.Items.FindByText(Serah.PartnoDest.Substring(9, 4) + "-" + Serah.PartnoDest.Substring(13, 4)));
                Session["serahid"] = Serah.ID;
                LoadDataGrid1();
                PanelDef.Enabled = false;
            }
            catch
            {
                DisplayAJAXMessage(this, errmessage);
            }
        }
        private void LoadFormula()
        {
            ddlFormula.Items.Clear();
            ArrayList arrFormula = new ArrayList();
            FormulaFacade formulaFacade = new FormulaFacade();
            arrFormula = formulaFacade.Retrieve1();
            ddlFormula.Items.Add(new ListItem("-- ALL --", "0"));
            foreach (Formula formula in arrFormula)
            {
                ddlFormula.Items.Add(new ListItem(formula.FormulaCode.Trim(), formula.ID.ToString()));
            }
        }
        private void LoadPlantGroup()
        {
            ddlProd.Items.Clear();
            ArrayList arrPlantGroup = new ArrayList();
            PlantGroupFacade plantGroupFacade = new PlantGroupFacade();
            arrPlantGroup = plantGroupFacade.Retrieve();
            ddlProd.Items.Clear();
            ddlProd.Items.Add(new ListItem("-- ALL --", "0"));
            foreach (PlantGroup plantGroup in arrPlantGroup)
            {
                ddlProd.Items.Add(new ListItem(plantGroup.Group.Trim(), plantGroup.ID.ToString()));
            }
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
        private void LoadJemurGroup()
        {
            ddlJemur.Items.Clear();
            ArrayList arrjemurGroupFacade = new ArrayList();
            MasterGroupJemurFacade jemurGroupFacade = new MasterGroupJemurFacade();
            arrjemurGroupFacade = jemurGroupFacade.Retrieve();
            ddlJemur.Items.Clear();
            ddlJemur.Items.Add(new ListItem("-- ALL --", "0"));
            foreach (MasterGroupJemur jemurGroup in arrjemurGroupFacade)
            {
                ddlJemur.Items.Add(new ListItem(jemurGroup.GroupJemurCode, jemurGroup.ID.ToString()));
            }
        }
        private void LoadUkuran()
        {
            ddlUkuran.Items.Clear();
            ArrayList arrmasterUkuranFacade = new ArrayList();
            MasterUkuranFacade masterUkuranFacade = new MasterUkuranFacade();
            arrmasterUkuranFacade = masterUkuranFacade.Retrieve();
            ddlUkuran.Items.Clear();
            ddlUkuran.Items.Add(new ListItem("-- Pilih Ukuran --", "0"));
            ddlUkuran0.Items.Clear();
            ddlUkuran0.Items.Add(new ListItem("-- Pilih Ukuran --", "0"));
            foreach (MasterUkuran masterUkuran in arrmasterUkuranFacade)
            {
                ddlUkuran.Items.Add(new ListItem(masterUkuran.Description, masterUkuran.ID.ToString()));
                ddlUkuran0.Items.Add(new ListItem(masterUkuran.Description, masterUkuran.ID.ToString()));
            }
        }
        private void LoadPalet()
        {
            //    string tgl = txtDate.SelectedDate.ToString("yyyyMMdd");
            //AutoCompleteExtender4.ContextKey = " ID IN(SELECT PALETID FROM BM_DESTACKING WHERE CONVERT(char(8),TglProduksi, 112) ='" + tgl + "') ";
            ArrayList arrpalet = new ArrayList();
            BM_PaletFacade bmPaletFacade = new BM_PaletFacade();
            arrpalet = bmPaletFacade.Retrieve();

        }
        private string ValidateText()
        {

            //if (ddlFormula.SelectedIndex ==0)
            //    return "Formula tidak boleh KOSONG";
            if (ddlCutter.SelectedIndex == 0)
                return "Group Cutter tidak boleh KOSONG";
            if (txtNoPalet.Text == string.Empty)
                return "No PALET tidak boleh KOSONG";
            //if (ddlUkuran.SelectedIndex == 0)
            //    return "Ukuran tidak boleh KOSONG";
            if (ddlJemur.SelectedIndex == 0)
                return "Group Jemur tidak boleh KOSONG";
            //if (ddlJenis.SelectedIndex == 0)
            //    return "Jenis tidak boleh KOSONG";
            //if (ddlProd.SelectedIndex == 0)
            //return "Group Produksi tidak boleh KOSONG";
            //try
            // {
            //     decimal dec = decimal.Parse(txtQty.Text);
            //     if (dec < 1)
            //         return "Jumlah tidak boleh kosong";
            // }
            // catch
            // {
            //     return "Jumlah harus numeric";
            // }

            return string.Empty;

        }
        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            //string strValidate = ValidateText();
            string defname = string.Empty;
            string strError = string.Empty;


            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Calendar1.SelectedDate.Year;
            int Bulan = Calendar1.SelectedDate.Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            Defect defect = new Defect();

            ArrayList arrdefdet = new ArrayList();
            int maxID = 0;
            Defect cekLastID = new Defect();
            DefectFacades defectFacade = new DefectFacades();
            BM_Palet palet = new BM_Palet();
            BM_PaletFacade paletF = new BM_PaletFacade();
            int defqty = 0;

            Hitung();
            defqty = defqty / Convert.ToInt32(LblBagi.Text);
            //txtTotalDefect.Text = defqty.ToString();
            txtTotalDefect.Text = Hitung().ToString();
            if (Convert.ToInt32(txtTotalBP.Text) != Convert.ToInt32(txtTotalDefect.Text))
            {
                DisplayAJAXMessage(this, "Jumlah Defect tidak sama dengan total BP");
                //DataList1.Focus();
                return;
            }
            cekLastID = defectFacade.RetrieveMaxId();
            if (cekLastID.ID > 0)
                maxID = cekLastID.ID;
            else
                maxID = 1;
            defect.Tgl = DateTime.Parse(txtDatePeriksa.Text);
            defect.DefectNo = (maxID + 1).ToString().PadLeft(6, '0') + "/Def/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            defect.TglProduksi = Convert.ToDateTime(txtDateProd.Text);
            defect.ProdID = int.Parse(ddlFormula.SelectedValue.ToString());
            defect.GroupProdID = int.Parse(ddlProd.SelectedValue.ToString());
            defect.JenisID = int.Parse(ddlJenis.SelectedValue.ToString());
            defect.GroupCutID = int.Parse(ddlCutter.SelectedValue.ToString());
            defect.GroupJemurID = int.Parse(ddlJemur.SelectedValue.ToString());
            defect.UkuranID = int.Parse(ddlUkuran0.SelectedValue.ToString());
            defect.TPot = int.Parse(txtTotalPotong.Text);
            defect.TKw= int.Parse(txtTotalkw.Text);
            palet = paletF.RetrieveByNo1((txtTotalkw.Text));
            defect.PaletID = palet.ID;
            if (RBSerah1.Checked == true)
                defect.SerahID = Convert.ToInt32(Session["serahid"].ToString());
            else
                defect.SerahID = 0;
            defect.DestID = Convert.ToInt32(Session["destid"].ToString());
            defect.CreatedBy = ((Users)Session["Users"]).UserName;
            DefectProcessFacade defProcessFacade = new DefectProcessFacade(defect, arrdefdet, "biasa", "");
            if (RBSerah1.Checked == true)
                defProcessFacade = new DefectProcessFacade(defect, arrdefdet, "biasa", "");
            else
                defProcessFacade = new DefectProcessFacade(defect, arrdefdet, "listplank", ddlPartno.SelectedValue);
            strError = defProcessFacade.Insert1();
            if (strError == string.Empty)
            {
                DisplayAJAXMessage(this, "Data tersimpan");
                clearform();
            }
            txtDateProd.Focus();
            clearform();
            arrdefdet.Clear();
            LoadDataGridSerah(Calendar1.SelectedDate);
            if (RBSerah2.Checked == true)
                LoadDefectTrans(DateTime.Parse(txtDatePeriksa.Text));
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            //btnUpdate.Disabled = false;
            clearform();
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            //LoadDataGridGrade(LoadGridByCriteria());
            LoadDataGrid(LoadGridByCriteria());
        }
        private void LoadDataGrid(ArrayList arrGrid)
        {
            //this.GridView1.DataSource = arrGrid;
            //this.GridView1.DataBind();
        }
        private ArrayList LoadGridByCriteria()
        {
            ArrayList arrGrid = new ArrayList();
            DefectFacades defectFacade = new DefectFacades();
            if (txtSearch.Text == string.Empty)
                arrGrid = defectFacade.Retrieve();
            //else
            //    arrGrid = defectFacade.RetrieveByCriteria(ddlSearch.SelectedValue, txtSearch.Text);

            if (arrGrid.Count > 0)
            {
                return arrGrid;
            }

            arrGrid.Add(new DefectFacades());
            return arrGrid;
        }
        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            //PanelDelete.Visible = false;
            //Panel2.Visible = true;
            //PanelDef.Visible = true;
            //RBInput.Checked = true;
            //RBInput0.Checked = false;

            if (txtNoPaletD.Text != string.Empty)
            {
                DefectFacades deff = new DefectFacades();
                int defectID = deff.getDefectID(txtNoPaletD.Text, txtDatePeriksa0.SelectedDate.ToString("yyyyMMdd"));
                if (defectID > 0)
                {
                    int interror = deff.CancelDefect(defectID.ToString());
                    if (interror > -1)
                    {
                        DisplayAJAXMessage(this, "Hapus Data berhasil");
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Hapus Data Error");
                    }
                }
                else

                    DisplayAJAXMessage(this, "Palet tidak ditemukan");
                txtNoPaletD.Text = string.Empty;
            }
        }
        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            //LoadDataGrid1();
            //LoadDataGrid2();
        }
        protected void txtNoPalet_TextChanged(object sender, EventArgs e)
        {
            if (txtNoPalet.Text == string.Empty || txtNoPalet.Text == "____")
            {
                txtNoPalet.Text = string.Empty;
                txtNoPalet.Focus();
                return;
            }
            if (txtNoPalet.Text.Trim() != string.Empty || txtNoPalet.Text == "____")
            {
                int intselect = ddlUkuran0.SelectedIndex;
                ddlUkuran0.SelectedIndex = 0;
                LoadSerahByPalet(Calendar1.SelectedDate);
                ddlUkuran0.SelectedIndex = intselect;
                ddlCutter.Focus();
            }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        protected void txtDateProd_TextChanged(object sender, EventArgs e)
        {
            txtNoPalet.Focus();
        }
        protected void ddlCutter_SelectedIndexChanged(object sender, EventArgs e)
        {
            AktivateDefect();
            ddlJemur.Focus();
        }
        protected void ddlJemur_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterUkuran def = new MasterUkuran();
            MasterUkuranFacade deff = new MasterUkuranFacade();
            def = deff.RetrieveById(Convert.ToInt32(ddlUkuran0.SelectedValue));
            LblBagi.Text = def.Bagi.ToString();
            AktivateDefect();
            PanelDef.Focus();

        }
        protected void ddlJenis_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlFormula.Focus();
        }
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            if (ChkPartNo.Checked == true)
            {
                ddlPartno.Visible = true;
                ArrayList arrSerah = new ArrayList();
                T1_SerahFacade Serah = new T1_SerahFacade();
                ddlPartno.Items.Clear();
                arrSerah = Serah.RetrievePartNoPelarianForDefect(Calendar1.SelectedDate.ToString("yyyyMMdd"));
                foreach (T1_Serah serah in arrSerah)
                {
                    ddlPartno.Items.Add(new ListItem(serah.PartnoSer, serah.PartnoSer));
                }
            }
            if (RBSerah2.Checked == true)
            {
                ArrayList arrSerah = new ArrayList();
                T1_SerahFacade Serah = new T1_SerahFacade();
                ddlPartno.Items.Clear();
                arrSerah = Serah.RetrievePartnoListplankForDefect(Calendar1.SelectedDate.ToString("yyyyMMdd"));
                foreach (T1_Serah serah in arrSerah)
                {
                    ddlPartno.Items.Add(new ListItem(serah.PartnoSer, serah.PartnoSer));
                }
            }
            LoadDataGridSerah(Calendar1.SelectedDate);
            //txtdrtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            //txtsdtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            txtDateProd.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            //DateTime.Parse( txtDatePeriksa.Text) = Calendar1.SelectedDate;


            txtDatePeriksa0.SelectedDate = Calendar1.SelectedDate;
        }
        protected void GridViewSerah_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "pilih")
            {
                ddlUkuran0.SelectedIndex = 0;
                txtNoPalet.Text = GridViewSerah.Rows[rowindex].Cells[6].Text;
                ddlFormula.SelectedIndex = ddlFormula.Items.IndexOf(ddlFormula.Items.FindByText(GridViewSerah.Rows[rowindex].Cells[2].Text.Trim()));
                ddlProd.SelectedIndex = ddlProd.Items.IndexOf(ddlProd.Items.FindByText(GridViewSerah.Rows[rowindex].Cells[3].Text.Trim()));
                ddlJenis.SelectedIndex = ddlJenis.Items.IndexOf(ddlJenis.Items.FindByText(GridViewSerah.Rows[rowindex].Cells[5].Text.Substring(0, 3)));
                string ukuran = GridViewSerah.Rows[rowindex].Cells[5].Text.Substring(9, 4) + "-" + GridViewSerah.Rows[rowindex].Cells[5].Text.Substring(13, 4);
                //errmessage = "Ukuran barang " + ukuran + " belum tersedia";
                ddlUkuran.SelectedIndex = ddlUkuran.Items.IndexOf(ddlUkuran.Items.FindByText(GridViewSerah.Rows[rowindex].Cells[5].Text.Substring(9, 4) + "X" +
                    GridViewSerah.Rows[rowindex].Cells[5].Text.Substring(13, 4)));
                ddlUkuran0.SelectedIndex = ddlUkuran.Items.IndexOf(ddlUkuran.Items.FindByText(GridViewSerah.Rows[rowindex].Cells[5].Text.Substring(9, 4) + "X" +
                    GridViewSerah.Rows[rowindex].Cells[5].Text.Substring(13, 4)));
                txtTotalPotong.Text = GridViewSerah.Rows[rowindex].Cells[9].Text;
                Session["Tpotong"] = GridViewSerah.Rows[rowindex].Cells[9].Text;
                txtTotalBP.Text = "";
                Session["destid"] = GridViewSerah.Rows[rowindex].Cells[0].Text;
                txtTotalBP.Focus();

                //LoadSerahByID(GridViewSerah.Rows[rowindex].Cells[10].Text);
            }
        }
        protected void ChkHide_CheckedChanged(object sender, EventArgs e)
        {
            //if (ChkHide.Checked == true)
            //{
            //    Panel3.Visible = true;
            //}
            //else
            //    Panel3.Visible = false;
        }
        protected void RBTglProduksi_CheckedChanged(object sender, EventArgs e)
        {
            if (RBTglProduksi.Checked == true)
            {
                clearform();
                LoadDataGridSerah(Calendar1.SelectedDate);
            }
        }
        protected void RBTglSerah_CheckedChanged(object sender, EventArgs e)
        {
            {
                clearform();
                LoadDataGridSerah(Calendar1.SelectedDate);
            }
        }
        protected void txtDefectNo_TextChanged(object sender, EventArgs e)
        {

        }
        protected void GridViewDef_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowindex = Convert.ToInt32(e.CommandArgument.ToString());
            GridView grv = (GridView)GridViewDef.Rows[rowindex].FindControl("GridViewtrans");
            GridView grv0 = (GridView)GridViewDef.Rows[rowindex].FindControl("GridViewtrans0");
            Label lbl = (Label)GridViewDef.Rows[rowindex].FindControl("Label2");
            GridViewDef.Rows[rowindex].FindControl("Cancel").Visible = false;

            //int luas = 0;
            //int i = 0;
            T3_KirimDetailFacade T3KirimDetail = new T3_KirimDetailFacade();
            // 
            switch (e.CommandName)
            {
                case "Details":
                    GridViewDef.Rows[rowindex].FindControl("Cancel").Visible = true;
                    GridViewDef.Rows[rowindex].FindControl("btn_Show").Visible = false;
                    ArrayList arrDefectDetail = new ArrayList();
                    DefectDetailFacade DefectDetailF = new DefectDetailFacade();
                    arrDefectDetail = DefectDetailF.RetrieveByDefectID(Convert.ToInt32(GridViewDef.Rows[rowindex].Cells[0].Text));
                    grv0.DataSource = arrDefectDetail;
                    grv0.DataBind();
                    grv0.Visible = true;
                    break;
                case "Cancel":
                    //grv.Visible = false;
                    grv0.Visible = false;
                    GridViewDef.Rows[rowindex].FindControl("Cancel").Visible = false;
                    GridViewDef.Rows[rowindex].FindControl("btn_Show").Visible = true;
                    break;
            }
        }
        protected void GridViewDef_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void GridViewDef_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
        protected void GridViewDef_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlUkuran0_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterUkuran def = new MasterUkuran();
            MasterUkuranFacade deff = new MasterUkuranFacade();
            def = deff.RetrieveById(Convert.ToInt32(ddlUkuran0.SelectedValue));
            LblBagi.Text = def.Bagi.ToString();
            AktivateDefect();
            PanelDef.Focus();
        }
        protected void GridViewSerah_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridViewDef_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    loadDynamicGrid();
        //}
        private void loadDynamicGrid(string tgl1, string tgl2)
        {
            string userid = ((Users)Session["Users"]).ID.ToString();
            string strTgl = string.Empty;
            string strTgl1 = string.Empty;
            if (RBLoadByTglProduksi.Checked == true)
            {
                strTgl = "TglProd";
                strTgl1 = "TglProduksi";
            }
            else
            {
                strTgl = "TglPeriksa";
            }
            string ispressing = string.Empty;
            if (ddlPressing.SelectedValue.Trim().ToUpper() == "YES")
            {
                ispressing = " and pressing='YES' ";
            }
            if (ddlPressing.SelectedValue.Trim().ToUpper() == "NO")
            {
                ispressing = " and pressing='NO' ";
            }
            string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListdefect" + userid + "]') AND type in (N'U')) DROP TABLE [dbo].[tempListdefect" + userid + "] " +
                "declare @tgl1 varchar(8) " +
                "declare @tgl2 varchar(8) " +
                "set @tgl1='" + tgl1 + "' " +
                "set @tgl2='" + tgl2 + "' " +
                "select * into tempListdefect" + userid + " from (  " +
                "select A.tgl TglPeriksa,E.Shift,E.tglProduksi TglProd,I.partno,TJ.TglJemur,rtrim(cast(CAST(I.Tebal as decimal(18,1)) as CHAR)) + ' X ' +   " +
                "rtrim(cast(CAST(I.Lebar as decimal(18)) as CHAR)) + ' X ' + rtrim(cast(CAST(I.Panjang as decimal(18)) as CHAR)) Ukuran ,P.NoPAlet,   " +
                "case when isnull(D.DefCode,'')='' then (select top 1 defcode from Def_MasterDefect where rowstatus>-1 order by id desc) else D.DefCode end DefCode, " +
                "BP.PlantName as Line, PG.[Group] as GP,J.GroupJemurCode as GJ,BF.FormulaCode as Jenis,  " +
                "case when isnull(D.DefCode,'')='' then (select top 1 DefName from Def_MasterDefect where rowstatus>-1 order by id desc) else D.DefName end DefName, " +
                "case when isnull(D.DefCode,'')='' then (select top 1 Urutan from Def_MasterDefect where rowstatus>-1 order by id desc) else D.Urutan end Urutan, " +
                "isnull(D.DeptID,0) DeptID,ISNULL(B.ID,0) ID,A.ID DefectID,  " +
                "case when isnull(D.DefCode,'')='' then (select top 1 ID from Def_MasterDefect where rowstatus>-1 order by id desc) else D.ID end MasterID, " +
                "isnull(B.qty,0) Qty,A.DestID,isnull((select sum(qty) from def_defectdetail where defectID=A.ID),0) QtyIn,A.TPotong as TotPotong,I.Pressing " +
                "from (SELECT * FROM def_defect WHERE status > -1) A left join def_defectdetail B on A.ID=B.defectID left join bm_destacking E on A.destid=E.id  " +
                "left join BM_PlantGroup PG on A.GroupProdID=PG.ID  left join fc_items I on E.itemid=I.ID left join BM_Palet P on E.PaletID=P.ID left join BM_Plant BP on BP.ID=PG.PlantID  " +
                "left join Def_GroupJemur J on A.GroupJemurID = J.ID left join BM_Formula BF on BF.ID=E.FormulaID  Left join t1_jemur TJ on E.ID=TJ.destid  " +
                "left join Def_MasterDefect D on B.MasterID=D.ID and D.rowstatus>-1 " +
                ") as Def where CONVERT(varchar," + strTgl + ",112)>=@tgl1 and CONVERT(varchar, " + strTgl + " ,112)<=@tgl2 " + ispressing +
                "DECLARE @cols AS NVARCHAR(MAX),  " +
                "    @query  AS NVARCHAR(MAX);  " +
                "set @cols= STUFF((SELECT distinct ',' + QUOTENAME(REPLACE(STR(Urutan, 2), SPACE(1), '0')+'.'+ c.defcode)  as d  FROM Def_MasterDefect c   where c.rowstatus>-1  order by  d    " +
                "            FOR XML PATH(''), TYPE  " +
                "            ).value('.', 'NVARCHAR(MAX)')   " +
                "        ,1,1,'')  " +
                "set @query = ' " +
                "select * ,(select tkw from Def_Defect where id=X.ID)TotKW " +
                "from (  " +
                "    select A.ID,B.Destid,TglPeriksa,TglProd,TglJemur,B.Partno,Ukuran,Line,GP,B.shift,GJ,GroupCutCode as GC,Jenis, B.NoPAlet as Palet,TotPotong as TotPtg,B.QtyIn as TotDef,  " +
                "    REPLACE(STR(B.Urutan, 2), SPACE(1), ''0'')+ ''.''+ B.defcode as defcode,QTY from Def_Defect A inner join tempListdefect" + userid + "   B on A.ID=B.DefectID    " +
                "    left join Def_GroupCutter D on A.groupcutid=D.ID " +

                ") as X1   " +
                "pivot   " +
                "(  " +
                "sum(QTY) for defcode in (' + @cols + ')   " +
                ") as X order by ID '  " +
                "execute(@query) " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListdefect" + userid +
                "]') AND type in (N'U')) DROP TABLE [dbo].[tempListdefect" + userid + "]  ";

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.DataField = col.ColumnName;
                if (col.ColumnName == "TglPeriksa" || col.ColumnName == "TglProd" || col.ColumnName == "TglJemur")
                    bfield.DataFormatString = "{0:dd-MMM-yyyy}";
                else
                    bfield.DataFormatString = "{0:N0}";
                if (col.ColumnName == "ID" || col.ColumnName == "TglPeriksa" || col.ColumnName == "TglProd" || col.ColumnName == "Line"
                    || col.ColumnName == "GP" || col.ColumnName == "GJ" || col.ColumnName == "GC" || col.ColumnName == "Jenis"
                    || col.ColumnName == "Palet" || col.ColumnName == "shift" || col.ColumnName == "TotPtg" || col.ColumnName == "TotKW" || col.ColumnName == "TotDef"
                    || col.ColumnName == "Ukuran" || col.ColumnName == "Partno")
                    bfield.HeaderText = col.ColumnName;
                else
                {
                    string txtfield = col.ColumnName + "     ";
                    bfield.HeaderText = txtfield.Substring(3, 6);
                }
                bfield.HeaderText = bfield.HeaderText.Trim();
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        private void loadDynamicGridL(string tgl1, string tgl2)
        {
            string strTgl = string.Empty;
            string strTgl1 = string.Empty;
            if (RBLoadByTglProduksi.Checked == true)
            {
                strTgl = "TglProd";
                strTgl1 = "TglProduksi";
            }
            else
            {
                strTgl = "TglPeriksa";
            }

            string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempListdefect] " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempListdefect1]  " +
                "select * into tempListdefect from (  " +
                "select A.Tgl as TglPeriksa,E.TglProduksi as TglProd, TJ.TglJemur,DU.[Description] Ukuran, P.NoPAlet,  " +
                "D.DefCode,BP.PlantName as Line, PG.[Group] as GP,J.GroupJemurCode as GJ,BF.FormulaCode as Jenis, D.DefName,D.DeptID,B.ID,B.DefectID, " +
                "B.MasterID,B.Qty,C.DestID,C.QtyIn,0 TotPotong  " +
                "from Def_DefectLDetail B inner join Def_Defect A on A.Id=B.DefectID left join T1_Serah C on A.SerahID=C.ID  left join FC_Items I on C.ItemID =I.ID  " +
                "left join Def_MasterDefect D on B.MasterID=D.ID left join BM_Destacking E on E.ID=C.DestID left join BM_Palet P on E.PaletID=P.ID  Left join t1_jemur TJ on E.ID=TJ.destid  " +
                "left join BM_PlantGroup PG on A.GroupProdID=PG.ID left join BM_Formula BF on BF.ID=E.FormulaID  left join BM_Plant BP on BP.ID=E.PlantID  left join Def_GroupJemur J on A.GroupJemurID = J.ID left join Def_Ukuran DU on DU.ID=A.UkuranID  " +
                "where B.RowStatus>-1 ) as Def    " +
                "where CONVERT(varchar,TglPeriksa,112)>='" + tgl1 + "' and CONVERT(varchar,TglPeriksa,112)<='" + tgl2 + "' " +
                "DECLARE @cols AS NVARCHAR(MAX),  " +
                "    @query  AS NVARCHAR(MAX);  " +
                "set @cols= STUFF((SELECT distinct ',' + QUOTENAME(REPLACE(STR(Urutan, 2), SPACE(1), '0')+'.'+ c.defcode)  as d  FROM Def_MasterDefect c     order by  d    " +
                "            FOR XML PATH(''), TYPE  " +
                "            ).value('.', 'NVARCHAR(MAX)')   " +
                "        ,1,1,'')  " +
                "set @query = '  " +
                "select *  " +
                "from (  " +
                "    select A.ID,B.Destid,TglPeriksa,TglProd,TglJemur,Ukuran,Line,GP,GJ,'''' GC,Jenis, B.NoPAlet as Palet,TotPotong as TotPtg,B.QtyIn as TotDef,  " +
                "    REPLACE(STR(Urutan, 2), SPACE(1), ''0'')+ ''.''+ B.defcode as defcode,QTY from Def_Defect A inner join tempListdefect B on A.ID=B.DefectID   " +
                "    inner join Def_MasterDefect C on B.MasterID =C.ID  " +
                "/*        union all  " +
                "--select (select top 1 ID from def_defect order by ID desc)+1 ID,B.destid,TglPeriksa,TglProd,TglJemur,Ukuran,Line,GP,GJ,'''' as GC,Jenis, B.NoPAlet as Palet,TotPotong as TotPtg,B.QtyIn as TotDef,   " +
                "--REPLACE(STR(0, 2), SPACE(1), ''0'')+ ''.''+ B.defcode as defcode,QTY from tempListdefect1 B */ " +
                ") as X1   " +
                "pivot   " +
                "(  " +
                "sum(QTY) for defcode in (' + @cols + ')   " +
                ") as X order by ID '  " +
                "execute(@query)" +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListdefect]') AND type in (N'U')) DROP TABLE [dbo].[tempListdefect]  " +
                "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tempListdefect1]') AND type in (N'U')) DROP TABLE [dbo].[tempListdefect1] ";

            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();
            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();
                bfield.DataField = col.ColumnName;
                if (col.ColumnName == "TglPeriksa" || col.ColumnName == "TglProd" || col.ColumnName == "TglJemur")
                    bfield.DataFormatString = "{0:dd-MMM-yyyy}";
                else
                    bfield.DataFormatString = "{0:N0}";
                if (col.ColumnName == "ID" || col.ColumnName == "TglPeriksa" || col.ColumnName == "TglProd" || col.ColumnName == "Line"
                    || col.ColumnName == "GP" || col.ColumnName == "GJ" || col.ColumnName == "GC" || col.ColumnName == "Jenis"
                    || col.ColumnName == "Palet" || col.ColumnName == "TotPtg" || col.ColumnName == "TotDef" || col.ColumnName == "Ukuran")
                    bfield.HeaderText = col.ColumnName;
                else
                {
                    string txtfield = col.ColumnName + "     ";
                    bfield.HeaderText = txtfield.Substring(3, 6);
                }
                bfield.HeaderText = bfield.HeaderText.Trim();
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();
        }
        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            if (RBSerah1.Checked == true)
                loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
            else
                loadDynamicGridL(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GrdDynamic.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Defect " + Calendar1.SelectedDate.ToString("ddMMyyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GrdDynamic.AllowPaging = false;
            GrdDynamic.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GrdDynamic.HeaderRow.Cells.Count; i++)
            {
                GrdDynamic.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GrdDynamic.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void txtsdtanggal_TextChanged(object sender, EventArgs e)
        {
            if (RBSerah1.Checked == true)
                loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
            else
                loadDynamicGridL(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        protected void txtdrtanggal_TextChanged(object sender, EventArgs e)
        {
            if (RBSerah1.Checked == true)
                loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
            else
                loadDynamicGridL(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        protected void RBLoadByTglProduksi_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSerah1.Checked == true)
                loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
            else
                loadDynamicGridL(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        protected void RBLoadByTglSerah_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSerah1.Checked == true)
                loadDynamicGrid(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
            else
                loadDynamicGridL(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
        }
        protected void DataList1_UpdateCommand(object source, DataListCommandEventArgs e)
        {
            //int test = 0; 
            //test = Convert.ToInt32(((Label)DataList1.Items[e.Item.ItemIndex].FindControl("LblUrutan")).Text);
            //((TextBox)DataList1.Items[test+1].FindControl("txtDefQty")).Focus();
        }
        protected void GrdDynamic_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        int tabIndex = 0;
        protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox tbName = (TextBox)e.Item.FindControl("txtDefQty");
                tbName.TabIndex = (short)++tabIndex;
            }
        }
        protected void CreateTabIndex()
        {
            txtDefQty0.TabIndex = 1;
            txtDefQty1.TabIndex = 2;
            txtDefQty2.TabIndex = 3;
            txtDefQty3.TabIndex = 4;
            txtDefQty4.TabIndex = 5;
            txtDefQty5.TabIndex = 6;
            txtDefQty6.TabIndex = 7;
            txtDefQty7.TabIndex = 8;
            txtDefQty8.TabIndex = 9;
            txtDefQty9.TabIndex = 10;
            txtDefQty10.TabIndex = 11;
            txtDefQty11.TabIndex = 12;
            txtDefQty12.TabIndex = 13;
            txtDefQty13.TabIndex = 14;
            txtDefQty14.TabIndex = 15;
            txtDefQty15.TabIndex = 16;

        }
        private void LoadDataGrid1()
        {
            ArrayList arrMasterDefect = new ArrayList();
            MasterDefectFacade MDF = new MasterDefectFacade();
            arrMasterDefect = MDF.Retrieve();
            //Session["arrMasterDefect"] = arrMasterDefect;
            //int i = 0;
            //foreach (MasterDefect def in arrMasterDefect)
            //{
            //    //Control myControl1 =  (Label)Parent.FindControl("LblDef0");
            //    Label LblDef = ((Label)(Page.FindControl(("LblDef0"))));

            //    if (LblDef != null)
            //    {
            //        LblDef.Text = def.DefName;
            //    }
            //    i++;
            //}
            //LID0.Visible = true;
            //LID1.Visible = true;
            //LID2.Visible = true;
            //LID3.Visible = true;
            //LID4.Visible = true;
            //LID5.Visible = true;
            //LID6.Visible = true;
            //LID7.Visible = true;
            //LID8.Visible = true;
            //LID9.Visible = true;
            //LID10.Visible = true;
            //LID11.Visible = true;
            //LID12.Visible = true;
            //LID13.Visible = true;
            //LID14.Visible = true;
            //LID15.Visible = true;
            int j = 0;

            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef0.Visible = true;
                    txtDefQty0.Visible = true;
                    LblDef0.Text = md.DefName;
                    LID0.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty0;
                    break;
                }
                else
                {
                    LblDef0.Visible = false;
                    txtDefQty0.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef1.Visible = true;
                    txtDefQty1.Visible = true;
                    LblDef1.Text = md.DefName;
                    LID1.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty1;
                    break;
                }
                else
                {
                    LblDef1.Visible = false;
                    txtDefQty1.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef2.Visible = true;
                    txtDefQty2.Visible = true;
                    LblDef2.Text = md.DefName;
                    LID2.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty2;
                    break;
                }
                else
                {
                    LblDef2.Visible = false;
                    txtDefQty2.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef3.Visible = true;
                    txtDefQty3.Visible = true;
                    LblDef3.Text = md.DefName;
                    LID3.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty3;
                    break;
                }
                else
                {
                    LblDef3.Visible = false;
                    txtDefQty3.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef4.Visible = true;
                    txtDefQty4.Visible = true;
                    LblDef4.Text = md.DefName;
                    LID4.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty4;
                    break;
                }
                else
                {
                    LblDef4.Visible = false;
                    txtDefQty4.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef5.Visible = true;
                    txtDefQty5.Visible = true;
                    LblDef5.Text = md.DefName;
                    LID5.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty5;
                    break;
                }
                else
                {
                    LblDef5.Visible = false;
                    txtDefQty5.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef6.Visible = true;
                    txtDefQty6.Visible = true;
                    LblDef6.Text = md.DefName;
                    LID6.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty6;
                    break;
                }
                else
                {
                    LblDef6.Visible = false;
                    txtDefQty6.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef7.Visible = true;
                    txtDefQty7.Visible = true;
                    LblDef7.Text = md.DefName;
                    LID7.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty7;
                    break;
                }
                else
                {
                    LblDef7.Visible = false;
                    txtDefQty7.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef8.Visible = true;
                    txtDefQty8.Visible = true;
                    LblDef8.Text = md.DefName;
                    LID8.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty8;
                    break;
                }
                else
                {
                    LblDef8.Visible = false;
                    txtDefQty8.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef9.Visible = true;
                    txtDefQty9.Visible = true;
                    LblDef9.Text = md.DefName;
                    LID9.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty9;
                    break;
                }
                else
                {
                    LblDef9.Visible = false;
                    txtDefQty9.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef10.Visible = true;
                    txtDefQty10.Visible = true;
                    LblDef10.Text = md.DefName;
                    LID10.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty10;
                    break;
                }
                else
                {
                    LblDef10.Visible = false;
                    txtDefQty10.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef11.Visible = true;
                    txtDefQty11.Visible = true;
                    LblDef11.Text = md.DefName;
                    LID11.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty11;
                    break;
                }
                else
                {
                    LblDef11.Visible = false;
                    txtDefQty11.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef12.Visible = true;
                    txtDefQty12.Visible = true;
                    LblDef12.Text = md.DefName;
                    LID12.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty12;
                    break;
                }
                else
                {
                    LblDef12.Visible = false;
                    txtDefQty12.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef13.Visible = true;
                    txtDefQty13.Visible = true;
                    LblDef13.Text = md.DefName;
                    LID13.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty13;
                    break;
                }
                else
                {
                    LblDef13.Visible = false;
                    txtDefQty13.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef14.Visible = true;
                    txtDefQty14.Visible = true;
                    LblDef14.Text = md.DefName;
                    LID14.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty14;
                    break;
                }
                else
                {
                    LblDef14.Visible = false;
                    txtDefQty14.Visible = false;
                }
            }
            foreach (MasterDefect md in arrMasterDefect)
            {
                if (md.Urutan > j)
                {
                    LblDef15.Visible = true;
                    txtDefQty15.Visible = true;
                    LblDef15.Text = md.DefName;
                    LID15.Text = md.ID.ToString();
                    j = j + 1;
                    LastTxt = txtDefQty15;
                    break;
                }
                else
                {
                    LblDef15.Visible = false;
                    txtDefQty15.Visible = false;
                }
            }
            #region remark
            //MasterDefect md = new MasterDefect();
            //md = new MasterDefect();
            //int j = 0;
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef0.Visible = true;
            //        txtDefQty0.Visible = true;
            //        LblDef0.Text = md.DefName;
            //        LID0.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty0;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef0.Visible = false;
            //        txtDefQty0.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef1.Visible = true;
            //        txtDefQty1.Visible = true;
            //        LblDef1.Text = md.DefName;
            //        LID1.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty1;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef1.Visible = false;
            //        txtDefQty1.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef2.Visible = true;
            //        txtDefQty2.Visible = true;
            //        LblDef2.Text = md.DefName;
            //        LID2.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty2;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef2.Visible = false;
            //        txtDefQty2.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef3.Visible = true;
            //        txtDefQty3.Visible = true;
            //        LblDef3.Text = md.DefName;
            //        LID3.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty3;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef3.Visible = false;
            //        txtDefQty3.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef4.Visible = true;
            //        txtDefQty4.Visible = true;
            //        LblDef4.Text = md.DefName;
            //        LID4.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty4;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef4.Visible = false;
            //        txtDefQty4.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef5.Visible = true;
            //        txtDefQty5.Visible = true;
            //        LblDef5.Text = md.DefName;
            //        LID5.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty5;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef5.Visible = false;
            //        txtDefQty5.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef6.Visible = true;
            //        txtDefQty6.Visible = true;
            //        LblDef6.Text = md.DefName;
            //        LID6.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty6;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef6.Visible = false;
            //        txtDefQty6.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef7.Visible = true;
            //        txtDefQty7.Visible = true;
            //        LblDef7.Text = md.DefName;
            //        LID7.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty7;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef7.Visible = false;
            //        txtDefQty7.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef8.Visible = true;
            //        txtDefQty8.Visible = true;
            //        LblDef8.Text = md.DefName;
            //        LID8.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty8;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef8.Visible = false;
            //        txtDefQty8.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef9.Visible = true;
            //        txtDefQty9.Visible = true;
            //        LblDef9.Text = md.DefName;
            //        LID9.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty9;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef9.Visible = false;
            //        txtDefQty9.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef10.Visible = true;
            //        txtDefQty10.Visible = true;
            //        LblDef10.Text = md.DefName;
            //        LID10.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty10;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef10.Visible = false;
            //        txtDefQty10.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef11.Visible = true;
            //        txtDefQty11.Visible = true;
            //        LblDef11.Text = md.DefName;
            //        LID11.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty11;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef11.Visible = false;
            //        txtDefQty11.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef12.Visible = true;
            //        txtDefQty12.Visible = true;
            //        LblDef12.Text = md.DefName;
            //        LID12.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty12;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef12.Visible = false;
            //        txtDefQty12.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef13.Visible = true;
            //        txtDefQty13.Visible = true;
            //        LblDef13.Text = md.DefName;
            //        LID13.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty13;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef13.Visible = false;
            //        txtDefQty13.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef14.Visible = true;
            //        txtDefQty14.Visible = true;
            //        LblDef14.Text = md.DefName;
            //        LID14.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty14;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef14.Visible = false;
            //        txtDefQty14.Visible = false;
            //    }
            //}
            //for (int i = j; i <= 100; i++)
            //{
            //    md = new MasterDefect();
            //    md = MDF.RetrieveByUrutan(i);
            //    if (md.DefName != string.Empty)
            //    {
            //        LblDef15.Visible = true;
            //        txtDefQty15.Visible = true;
            //        LblDef15.Text = md.DefName;
            //        LID15.Text = md.ID.ToString();
            //        j = i + 1;
            //        LastTxt = txtDefQty15;
            //        break;
            //    }
            //    else
            //    {
            //        LblDef15.Visible = false;
            //        txtDefQty15.Visible = false;
            //    }
            //}
            #endregion
            string txtID = LastTxt.ID;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox txtDefQty = (System.Web.UI.WebControls.TextBox)FindControl("txtDefQty0");
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            string defname = string.Empty;
            string strError = string.Empty;

            //if (strValidate != string.Empty || txtTotalDefect.Text==string.Empty )
            //{
            //    DisplayAJAXMessage(this, strValidate);
            //    LoadDataGrid1();
            //    return;
            //}
            if (PanelDef.Enabled == false)
                return;
            Defect defect = new Defect();
            ArrayList arrdefdet = new ArrayList();
            int maxID = 0;
            Defect cekLastID = new Defect();
            DefectFacades defectFacade = new DefectFacades();
            BM_Palet palet = new BM_Palet();
            BM_PaletFacade paletF = new BM_PaletFacade();
            int defqty = 0;
            //for (int i = 0; i <= DataList1.Items.Count - 1; i++)
            //{
            //    if (((TextBox)DataList1.Items[i].FindControl("txtDefQty")).Text != string.Empty)
            //    {
            //        DefectDetail defdet = new DefectDetail();
            //        defname = ((Label)DataList1.Items[i].FindControl("LblDefName")).Text;
            //        defqty = defqty + Convert.ToInt32(((TextBox)DataList1.Items[i].FindControl("txtDefQty")).Text);
            //        //MdefID = Convert.ToInt32(((Label)DataList1.Items[i].FindControl("txtID")).Text);
            //        defdet.MasterID = MdefID;
            //        defdet.Qty = Convert.ToInt32(((TextBox)DataList1.Items[i].FindControl("txtDefQty")).Text);
            //        arrdefdet.Add(defdet);
            //    }
            //}
            DefectDetail defdet = new DefectDetail();
            string ukuran = string.Empty;
            //if (ddlPartno.Visible == true && ddlPartno.SelectedItem.Text.Trim() !=string.Empty )
            //    ukuran=ddlPartno.SelectedItem.Text.Substring(9, 8);
            //else
            ukuran = ddlUkuran0.SelectedItem.Text.Trim();
            if (txtDefQty0.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty0.Text); defdet.MasterID = Convert.ToInt32(LID0.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty1.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty1.Text); defdet.MasterID = Convert.ToInt32(LID1.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty2.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty2.Text); defdet.MasterID = Convert.ToInt32(LID2.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty3.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty3.Text); defdet.MasterID = Convert.ToInt32(LID3.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty4.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty4.Text); defdet.MasterID = Convert.ToInt32(LID4.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty5.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty5.Text); defdet.MasterID = Convert.ToInt32(LID5.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty6.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty6.Text); defdet.MasterID = Convert.ToInt32(LID6.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty7.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty7.Text); defdet.MasterID = Convert.ToInt32(LID7.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty8.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty8.Text); defdet.MasterID = Convert.ToInt32(LID8.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty9.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty9.Text); defdet.MasterID = Convert.ToInt32(LID9.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty10.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty10.Text); defdet.MasterID = Convert.ToInt32(LID10.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty11.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty11.Text); defdet.MasterID = Convert.ToInt32(LID11.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty12.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty12.Text); defdet.MasterID = Convert.ToInt32(LID12.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty13.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty13.Text); defdet.MasterID = Convert.ToInt32(LID13.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty14.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty14.Text); defdet.MasterID = Convert.ToInt32(LID14.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }
            if (txtDefQty15.Text.Trim() != string.Empty) { defdet = new DefectDetail(); defdet.Qty = Convert.ToInt32(txtDefQty15.Text); defdet.MasterID = Convert.ToInt32(LID15.Text); defdet.Ukuran = ukuran; arrdefdet.Add(defdet); }

            defqty = Hitung();
            defqty = defqty / Convert.ToInt32(LblBagi.Text);
            txtTotalDefect.Text = defqty.ToString();
            if (txtTotalBP.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Total BP harus diisi");
                return;
            }
            if (Convert.ToInt32(txtTotalBP.Text) != Convert.ToInt32(txtTotalDefect.Text))
            {
                DisplayAJAXMessage(this, "Jumlah Defect tidak sama dengan total BP");
                //DataList1.Focus();
                return;
            }
            cekLastID = defectFacade.RetrieveMaxId();
            if (cekLastID.ID > 0)
                maxID = cekLastID.ID;
            else
                maxID = 1;
            defect.Tgl = DateTime.Parse(txtDatePeriksa.Text);
            defect.DefectNo = (maxID + 1).ToString().PadLeft(6, '0') + "/Def/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
            defect.TglProduksi = Convert.ToDateTime(txtDateProd.Text);
            defect.ProdID = int.Parse(ddlFormula.SelectedValue.ToString());
            defect.GroupProdID = int.Parse(ddlProd.SelectedValue.ToString());
            defect.JenisID = int.Parse(ddlJenis.SelectedValue.ToString());
            defect.GroupCutID = int.Parse(ddlCutter.SelectedValue.ToString());
            defect.GroupJemurID = int.Parse(ddlJemur.SelectedValue.ToString());
            defect.UkuranID = int.Parse(ddlUkuran0.SelectedValue.ToString());
            defect.TPot = int.Parse(txtTotalPotong.Text);
            defect.TKw  = int.Parse(txtTotalkw.Text);
            palet = paletF.RetrieveByNo1(txtNoPalet.Text);
            defect.PaletID = palet.ID;
            if (RBSerah1.Checked == true)
            {
                //defect.SerahID = Convert.ToInt32(Session["serahid"].ToString());
                defect.DestID = Convert.ToInt32(Session["destid"].ToString());
            }
            else
            {
                defect.SerahID = Int32.Parse(GridViewSerah.Rows[0].Cells[10].Text);
            }
            defect.CreatedBy = ((Users)Session["Users"]).UserName;
            DefectProcessFacade defProcessFacade = new DefectProcessFacade(defect, arrdefdet, "biasa", "");
            if (RBSerah1.Checked == true)
                defProcessFacade = new DefectProcessFacade(defect, arrdefdet, "biasa", "");
            else
                defProcessFacade = new DefectProcessFacade(defect, arrdefdet, "listplank", ddlPartno.SelectedValue);
            strError = defProcessFacade.Insert1();
            if (strError == string.Empty)
            {
                DisplayAJAXMessage(this, "Data tersimpan");
                clearform();
            }
            txtDateProd.Focus();
            clearform();
            arrdefdet.Clear();
            if (RBSerah2.Checked == true)
            {
                LoadDefectTrans(DateTime.Parse(txtDatePeriksa.Text));
                clearform();
                ArrayList arrSerah = new ArrayList();
                T1_SerahFacade Serah = new T1_SerahFacade();
                ddlPartno.Items.Clear();
                arrSerah = Serah.RetrievePartnoListplankForDefect(Calendar1.SelectedDate.ToString("yyyyMMdd"));
                foreach (T1_Serah serah in arrSerah)
                {
                    ddlPartno.Items.Add(new ListItem(serah.PartnoSer, serah.PartnoSer));
                }
            }
            LoadDataGridSerah(Calendar1.SelectedDate);
            //ChkPartNo.Visible = false;
            //ChkPartNo.Checked = false;
            ddlPartno.Visible = true;
            LoadDefectTrans(DateTime.Parse(txtDatePeriksa.Text));
        }
        protected int Hitung()
        {
            int hasil = 0;
            if (txtDefQty0.Text != string.Empty && txtDefQty0.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty0.Text);
            if (txtDefQty1.Text != string.Empty && txtDefQty1.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty1.Text);
            if (txtDefQty2.Text != string.Empty && txtDefQty2.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty2.Text);
            if (txtDefQty3.Text != string.Empty && txtDefQty3.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty3.Text);
            if (txtDefQty4.Text != string.Empty && txtDefQty4.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty4.Text);
            if (txtDefQty5.Text != string.Empty && txtDefQty5.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty5.Text);
            if (txtDefQty6.Text != string.Empty && txtDefQty6.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty6.Text);
            if (txtDefQty7.Text != string.Empty && txtDefQty7.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty7.Text);
            if (txtDefQty8.Text != string.Empty && txtDefQty8.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty8.Text);
            if (txtDefQty9.Text != string.Empty && txtDefQty9.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty9.Text);
            if (txtDefQty10.Text != string.Empty && txtDefQty10.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty10.Text);
            if (txtDefQty11.Text != string.Empty && txtDefQty11.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty11.Text);
            if (txtDefQty12.Text != string.Empty && txtDefQty12.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty12.Text);
            if (txtDefQty13.Text != string.Empty && txtDefQty13.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty13.Text);
            if (txtDefQty14.Text != string.Empty && txtDefQty14.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty14.Text);
            if (txtDefQty15.Text != string.Empty && txtDefQty15.Text.Substring(0, 1) != "_")
                hasil = hasil + Convert.ToInt32(txtDefQty15.Text);

            return hasil;
        }
        protected void txtDefQty0_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty0.Text == string.Empty || txtDefQty0.Text.Substring(0, 1) == "_")
            {
                txtDefQty0.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty1.Focus();
        }
        protected void txtDefQty1_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty1.Text == string.Empty || txtDefQty1.Text.Substring(0, 1) == "_")
            {
                txtDefQty1.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty2.Focus();
        }
        protected void txtDefQty2_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty2.Text == string.Empty || txtDefQty2.Text.Substring(0, 1) == "_")
            {
                txtDefQty2.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
        }
        protected void txtDefQty3_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty3.Text == string.Empty || txtDefQty3.Text.Substring(0, 1) == "_")
            {
                txtDefQty3.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty4.Focus();
        }
        protected void txtDefQty4_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty4.Text == string.Empty || txtDefQty4.Text.Substring(0, 1) == "_")
            {
                txtDefQty4.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty5.Focus();
        }
        protected void txtDefQty5_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty5.Text == string.Empty || txtDefQty5.Text.Substring(0, 1) == "_")
            {
                txtDefQty5.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty6.Focus();
        }
        protected void txtDefQty6_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty6.Text == string.Empty || txtDefQty6.Text.Substring(0, 1) == "_")
            {
                txtDefQty6.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty7.Focus();
        }
        protected void txtDefQty7_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty7.Text == string.Empty || txtDefQty7.Text.Substring(0, 1) == "_")
            {
                txtDefQty7.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty8.Focus();
        }
        protected void txtDefQty8_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty8.Text == string.Empty || txtDefQty8.Text.Substring(0, 1) == "_")
            {
                txtDefQty8.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty9.Focus();
        }
        protected void txtDefQty9_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty9.Text == string.Empty || txtDefQty9.Text.Substring(0, 1) == "_")
            {
                txtDefQty9.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty10.Focus();
        }
        protected void txtDefQty10_TextChanged(object sender, EventArgs e)
        {
            if (txtDefQty10.Text == string.Empty || txtDefQty10.Text.Substring(0, 1) == "_")
            {
                txtDefQty10.Focus();
                return;
            }
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty11.Focus();
        }
        protected void txtDefQty11_TextChanged(object sender, EventArgs e)
        {
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty12.Focus();
        }
        protected void txtDefQty12_TextChanged(object sender, EventArgs e)
        {
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty13.Focus();
        }
        protected void txtDefQty13_TextChanged(object sender, EventArgs e)
        {
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty14.Focus();
        }
        protected void txtDefQty14_TextChanged(object sender, EventArgs e)
        {
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            txtDefQty15.Focus();
        }
        protected void txtDefQty15_TextChanged(object sender, EventArgs e)
        {
            int hasil = 0; if (CheckBox1.Checked == true) hasil = Hitung();
            txtTotalDefect.Text = hasil.ToString();
            Button1.Focus();
        }
        protected void RBSerah1_CheckedChanged(object sender, EventArgs e)
        {
            clearform();
            LoadDataGridSerah(Calendar1.SelectedDate);
            ChkPartNo.Visible = true;
            ChkPartNo.Checked = false;
            ddlPartno.Visible = false;
            txtdrtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            txtsdtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            //DateTime.Parse( txtDatePeriksa.Text) = Calendar1.SelectedDate;
            LoadDefectTrans(DateTime.Parse(txtDatePeriksa.Text));
        }
        protected void RBSerah2_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSerah2.Checked == true)
            {
                clearform();
                ArrayList arrSerah = new ArrayList();
                T1_SerahFacade Serah = new T1_SerahFacade();
                ddlPartno.Items.Clear();
                arrSerah = Serah.RetrievePartnoListplankForDefect(Calendar1.SelectedDate.ToString("yyyyMMdd"));
                foreach (T1_Serah serah in arrSerah)
                {
                    ddlPartno.Items.Add(new ListItem(serah.PartnoSer, serah.PartnoSer));
                }
            }
            LoadDataGridSerah(Calendar1.SelectedDate);
            ChkPartNo.Visible = false;
            ChkPartNo.Checked = false;
            ddlPartno.Visible = true;
            LoadDefectTrans(DateTime.Parse(txtDatePeriksa.Text));
        }
        protected void ChkPartNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPartNo.Checked == true)
            {
                ddlPartno.Visible = true;
                ArrayList arrSerah = new ArrayList();
                T1_SerahFacade Serah = new T1_SerahFacade();
                ddlPartno.Items.Clear();
                arrSerah = Serah.RetrievePartNoPelarianForDefect(Calendar1.SelectedDate.ToString("yyyyMMdd"));
                foreach (T1_Serah serah in arrSerah)
                {
                    ddlPartno.Items.Add(new ListItem(serah.PartnoSer, serah.PartnoSer));
                }
                LoadDataGridSerah(Calendar1.SelectedDate);
            }
            else
            {
                ddlPartno.Items.Clear();
                LoadDataGridSerah(Calendar1.SelectedDate);
                ddlPartno.Visible = false;
            }
        }
        protected void ddlPartno_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah(Calendar1.SelectedDate);
        }
        protected void RBInput_CheckedChanged(object sender, EventArgs e)
        {
            PanelDelete.Visible = false;
            Panel2.Visible = true;
            PanelDef.Visible = true;
        }
        protected void RBInput0_CheckedChanged(object sender, EventArgs e)
        {
            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Calendar1.SelectedDate.Year;
            int Bulan = Calendar1.SelectedDate.Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            PanelDelete.Visible = true;
            Panel2.Visible = false;
            PanelDef.Visible = false;
        }
        protected void ddlUkuran_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtTotalPotong_TextChanged(object sender, EventArgs e)
        {
            if (Session["TPotong"] != null )
            {
                if (Int32.Parse(Session["TPotong"].ToString()) < Int32.Parse(txtTotalPotong.Text))
                {
                    txtTotalPotong.Text = Session["TPotong"].ToString();
                    txtTotalPotong.Focus();
                    return;
                }
                else
                    txtTotalBP.Focus();
            }
        }
        protected void txtDatePeriksa_TextChanged(object sender, EventArgs e)
        {
            txtdrtanggal.Text = txtDatePeriksa.Text;
            txtsdtanggal.Text = txtDatePeriksa.Text;
            LoadDefectTrans(DateTime.Parse(txtDatePeriksa.Text));
        }
    }
}