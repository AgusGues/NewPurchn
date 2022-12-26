using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using Factory;
using DataAccessLayer;
using System.Collections;
using Cogs;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.IO;

namespace GRCweb1.Modul.Factory
{
    public partial class Destacking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadFormula();
                LoadPlantGroup();
                Calendar1.SelectedDate = DateTime.Now;
                txtDate.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
                clearform();
                ddlFormula.Focus();
                txtdrtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
                txtsdtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
                txtTglIn.Text = DateTime.Parse(txtsdtanggal.Text).ToString("dd-MMM-yyyy") + " 07:00:00";
                txtTglOut.Text = DateTime.Parse(txtsdtanggal.Text).ToString("dd-MMM-yyyy") + " 15:00:00";
            }
            else
            {
                var ctrlName = Request.Params[Page.postEventSourceID];
                var args = Request.Params[Page.postEventArgumentID];
                HandleCustomPostbackEvent(ctrlName, args);
            }
       ((ScriptManager)Master.FindControl("ScriptManager1")).RegisterPostBackControl(LinkButton4);
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridView1.ClientID + "', 310, 100 , 15,false); </script>", false);
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtJumlah, "OnBlur");
            txtJumlah.Attributes.Add("onblur", onBlurScript);
        }
        private void HandleCustomPostbackEvent(string ctrlName, string args)
        {
            if (ctrlName == txtJumlah.UniqueID && args == "OnBlur")
            {
                if (Label3.Text == "Insert Mode")
                    InsertItem();
                getfocus();
            }
        }
        private void clearform()
        {
            txtdrtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            txtsdtanggal.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            txtTglIn.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy") + " 07:00:00";
            txtTglOut.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy") + " 15:00:00";
            ddlFormula.SelectedIndex = 0;
            ddlGroup.SelectedIndex = 0;
            txtNoProduksi.Text = string.Empty;
            txtLokasi.Text = "A1000";
            txtNoPalet.Text = "1000";
            txtJumlah.Text = string.Empty;
            Label3.Text = "Insert Mode";
            LoadDataGrid();
        }

        private void LoadFormula()
        {
            ddlFormula.Items.Clear();
            ArrayList arrFormula = new ArrayList();
            FormulaFacade formulaFacade = new FormulaFacade();
            arrFormula = formulaFacade.Retrieve1();
            ddlFormula.Items.Add(new ListItem("-- Pilih Jenis --", "0"));
            foreach (Formula formula in arrFormula)
            {
                ddlFormula.Items.Add(new ListItem(formula.FormulaCode, formula.ID.ToString()));
            }
        }
        private void LoadPartno(string partno)
        {
            ddlPartno.Items.Clear();
            ArrayList arrpartno = new ArrayList();
            FC_ItemsFacade partnoFacade = new FC_ItemsFacade();
            arrpartno = partnoFacade.RetrieveByPartNodestacking(partno);

            //ddlFormula.Items.Add(new ListItem("-- Pilih Jenis --", "0"));
            foreach (FC_Items fcitems in arrpartno)
            {
                ddlPartno.Items.Add(new ListItem(fcitems.Partno, fcitems.ID.ToString()));
            }
            txtNoProduksi.Text = ddlPartno.SelectedItem.Text;
        }
        private void LoadPlantGroup()
        {
            ddlGroup.Items.Clear();
            ArrayList arrPlantGroup = new ArrayList();
            PlantGroupFacade plantGroupFacade = new PlantGroupFacade();
            arrPlantGroup = plantGroupFacade.Retrieve();
            ddlGroup.Items.Clear();
            ddlGroup.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (PlantGroup plantGroup in arrPlantGroup)
            {
                ddlGroup.Items.Add(new ListItem(plantGroup.Group, plantGroup.ID.ToString()));
            }
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrDestacking = new ArrayList();
            BM_DestackingFacade destf = new BM_DestackingFacade();
            string strValue = string.Empty;
            if (ddlSearch.SelectedIndex < 2 || txtSearch.Text != string.Empty)
                strValue = "'" + txtSearch.Text + "'";
            else
                strValue = txtSearch.Text;
            arrDestacking = destf.RetrieveCriteria(ddlSearch.SelectedValue, strValue, Calendar1.SelectedDate.ToString("MM/dd/yyyy"));
            GridView1.DataSource = arrDestacking;
            GridView1.DataBind();
        }

        protected void btnTransfer_ServerClick(object sender, EventArgs e)
        {
            int insert = 0;
            if (ddlFormula.SelectedIndex == 0 || ddlGroup.SelectedIndex == 0 || txtNoProduksi.Text == string.Empty ||
                txtLokasi.Text == string.Empty || txtNoPalet.Text == string.Empty || txtJumlah.Text == string.Empty)
            {
                //DisplayAJAXMessage(this, "Input data belum lengkap.");
                return;
            }

            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DateTime.Parse(txtDate.Text).Year;
            int Bulan = DateTime.Parse(txtDate.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            InsertItem();
            //txtNoPalet.Text = string.Empty;
            txtJumlah.Text = string.Empty;
            ddlFormula.Focus();
            Label3.Text = "Insert Mode";
            txtID.Text = "0";
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            if (ddlFormula.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Jenis Produksi belum ditentukan");
                return;
            }
            if (ddlGroup.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Group Produksi belum ditentukan");
                return;
            }
            if (txtNoPalet.Text == string.Empty || txtLokasi.Text == string.Empty)
                return;

            int ItemsID = 0;
            int lokasiID = 0;
            int paletID = 0;
            int plantID = 0;
            BM_DestackingFacade BM_Dest = new BM_DestackingFacade();
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            #region verifikasi data input
            ItemsID = BM_Dest.GetPartnoID(txtNoProduksi.Text.Trim());
            if (ItemsID == 0)
            {
                DisplayAJAXMessage(this, "Partno belum terdaftar");
                return;
            }
            lokasiID = BM_Dest.GetLokID(txtLokasi.Text.Trim());
            if (lokasiID == 0)
            {
                DisplayAJAXMessage(this, "Lokasi belum terdaftar");
                return;
            }
            paletID = BM_Dest.GetPaletID(txtNoPalet.Text.Trim());
            if (paletID == 0)
            {
                DisplayAJAXMessage(this, "Palet belum terdaftar / masih dipakai");
                return;
            }
            #endregion
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DateTime.Parse(txtDate.Text).Year;
            int Bulan = DateTime.Parse(txtDate.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            plantID = BM_Dest.GetPlantID(Convert.ToInt32(ddlGroup.SelectedValue));
            Users users = (Users)Session["Users"];
            BM_Destacking Destacking = new BM_Destacking();
            Destacking.ID = int.Parse(txtID.Text);
            Destacking.PlantID = plantID;
            Destacking.PlantGroupID = int.Parse(ddlGroup.SelectedValue);
            Destacking.FormulaID = int.Parse(ddlFormula.SelectedValue);
            Destacking.LokasiID = lokasiID;
            Destacking.PaletID = paletID;
            Destacking.ItemID = ItemsID;
            Destacking.Qty = int.Parse(txtJumlah.Text);
            Destacking.CreatedBy = users.UserName;
            txtID.Text = "0";
            BM_Dest.Update(Destacking);
            clearform();
            btnDelete.Enabled = false;
            btnUpdate.Disabled = true;
            btnTransfer.Enabled = true;
            Label3.Text = "Insert Mode";
        }

        private int InsertItem()
        {
            #region Verifikasi Closing Periode
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToInt32(Calendar1.SelectedDate.ToString("yyyy"));
            int Bulan = Convert.ToInt32(Calendar1.SelectedDate.ToString("MM"));
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return 0;
            }
            #endregion
            #region Verifikasi data input

            if (txtJumlah.Text == string.Empty)
                return -1;
            if (ddlFormula.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Jenis Produksi belum ditentukan");
                return -1;
            }
            if (ddlGroup.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Group Produksi belum ditentukan");
                return -1;
            }
            if (txtNoPalet.Text == string.Empty || txtLokasi.Text == string.Empty)
                return 0;
            #endregion

            int ItemsID = 0;
            int lokasiID = 0;
            int paletID = 0;
            int plantID = 0;
            decimal totalbiaya = 0;
            string tglproduksi = Calendar1.SelectedDate.ToString("yyyyMMdd");
            int totalLastProduk = 0;
            decimal HPP = 0;
            string id_dstk = string.Empty;
            string ThBl = Calendar1.SelectedDate.ToString("yyMM"); //ddlBulan.SelectedIndex.ToString().PadLeft(2, '0');
            BM_DestackingFacade BM_Dest = new BM_DestackingFacade();
            PakaiFormula pakaiformula = new PakaiFormula();
            PakaiFormulaFacade pakaiformulafacade = new PakaiFormulaFacade();
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            int recCount = BM_Dest.GetRecordCountInYear(Calendar1.SelectedDate.ToString("yyyyMM")) + 1;
            id_dstk = kd + ThBl + recCount.ToString().PadLeft(5, '0');

            ItemsID = BM_Dest.GetPartnoID(txtNoProduksi.Text.Trim());
            if (ItemsID == 0)
            {
                DisplayAJAXMessage(this, "Partno belum terdaftar");
                return -1;
            }
            lokasiID = BM_Dest.GetLokIDBM(txtLokasi.Text.Trim());
            if (lokasiID == 0)
            {
                DisplayAJAXMessage(this, "Lokasi belum terdaftar");
                return -1;
            }
            paletID = BM_Dest.GetPaletID(txtNoPalet.Text.Trim());
            if (paletID == 0)
            {
                DisplayAJAXMessage(this, "Palet belum terdaftar / masih dipakai");
                return -1;
            }
            if (txtJumlah.Text.Trim() == string.Empty)
                return -1;

            plantID = BM_Dest.GetPlantID(Convert.ToInt32(ddlGroup.SelectedValue));
            Users users = (Users)Session["Users"];

            BM_Destacking Destacking = new BM_Destacking();
            Destacking.PlantID = plantID;
            Destacking.PlantGroupID = int.Parse(ddlGroup.SelectedValue);
            Destacking.FormulaID = int.Parse(ddlFormula.SelectedValue);
            Destacking.LokasiID = lokasiID;
            Destacking.PaletID = paletID;
            Destacking.ItemID = ItemsID;
            Destacking.TglProduksi = Calendar1.SelectedDate;
            Destacking.Qty = int.Parse(txtJumlah.Text);
            Destacking.CreatedBy = users.UserName;
            Destacking.Id_dstk = id_dstk;
            Destacking.Shift = int.Parse(ddlShift.SelectedValue);
            Destacking.DrJam = DateTime.Parse(txtTglIn.Text);
            Destacking.SdJam = DateTime.Parse(txtTglOut.Text);

            int destid = 0;
            int strerror = 0;
            int JemurID = 0;
            if (txtNoPalet.Text != string.Empty && txtLokasi.Text != string.Empty)
            {
                strerror = BM_Dest.Insert(Destacking);
                destid = strerror;
                totalLastProduk = BM_Dest.GetTotalProduk(plantID, int.Parse(ddlGroup.SelectedValue), int.Parse(ddlFormula.SelectedValue), tglproduksi);
                HPP = (totalLastProduk + Destacking.Qty == 0) ? 0 : totalbiaya / (totalLastProduk + Destacking.Qty);
                //HPP = totalbiaya / (totalLastProduk + Destacking.Qty);
                BM_Dest.UpdateHPP(plantID, int.Parse(ddlGroup.SelectedValue), int.Parse(ddlFormula.SelectedValue), tglproduksi, HPP);
                TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                if (RBProses0.Checked == true)
                {
                    T1_Jemur Jemur = new T1_Jemur();
                    T1_JemurFacade JemurFacade = new T1_JemurFacade();
                    transManager.BeginTransaction();
                    AbstractTransactionFacadeF absTrans = new T1_JemurFacade(Jemur);
                    int intResult = 0;
                    Jemur.RakID = JemurFacade.GetRakID("00");
                    if (Jemur.RakID > 0 && destid > 0)
                    {
                        Jemur.CreatedBy = users.UserName;
                        Jemur.TglJemur = Calendar1.SelectedDate;
                        Jemur.DestID = destid;
                        Jemur.QtyIn = Convert.ToInt32(txtJumlah.Text);
                        intResult = absTrans.Insert(transManager);
                        JemurID = intResult;
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return 0;
                        }
                        transManager.CommitTransaction();
                        transManager.CloseConnection();
                        intResult = BM_Dest.UpdateStatus(Jemur.DestID, 1);
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "update bm_destacking set drJam ='" + txtTglIn.Text + "',sdJam='" + txtTglOut.Text + "' where ID=" + destid;
                        SqlDataReader sdr = zl.Retrieve();
                    }
                }

            }

            if (Label3.Text == "Insert Mode Ukuran Khusus, Auto serah ke lokasi pemotongan (Simetris)")
            {
                #region rekam data serah
                T1_SerahFacade serahFacade = new T1_SerahFacade();
                T1_JemurFacade jemurFacade = new T1_JemurFacade();
                BM_DestackingFacade dest = new BM_DestackingFacade();
                T1_Jemur jemur = new T1_Jemur();
                FC_Items items = new FC_Items();
                FC_Items items1 = new FC_Items();
                FC_ItemsFacade itemsF = new FC_ItemsFacade();
                ArrayList arrserah = new ArrayList();
                FC_ItemsFacade itemsfacade = new FC_ItemsFacade();

                jemur = jemurFacade.RetrieveJemurByID(JemurID.ToString());
                items = itemsfacade.RetrieveByPartNo(ddlPartno.SelectedItem.Text);

                items1 = itemsfacade.RetrieveByPartNo(ddlPartno.SelectedItem.Text);
                jemur.ItemID0 = dest.GetPartnoID(ddlPartno.SelectedItem.Text);
                jemur.LokasiID0 = Destacking.LokasiID;
                jemur.CreatedBy = users.UserName;
                //jemur.Oven = ddlOven.SelectedItem.Text;
                jemur.TglJemur = Calendar1.SelectedDate;
                int ItemIDDest = items1.ID;
                if (ddlPartno.SelectedItem.Text != string.Empty && txtJumlah.Text != string.Empty)
                {
                    if (Convert.ToInt32(txtJumlah.Text) > 0)
                    {
                        T1_Serah serah = new T1_Serah();
                        serah.PartnoSer = ddlPartno.SelectedItem.Text;
                        serah.PartnoDest = ddlPartno.SelectedItem.Text;
                        serah.ItemIDSer = items.ID;

                        serah.QtyIn = int.Parse(txtJumlah.Text);
                        serah.LokasiID = dest.GetLokID("C99");

                        serah.LokasiSer = "A99";
                        serah.DestID = jemur.DestID;
                        serah.ItemIDDest = ItemIDDest;
                        serah.HPP = 0;
                        serah.JemurID = jemur.ID;
                        serah.CreatedBy = users.UserName;
                        serah.TglSerah = Calendar1.SelectedDate;
                        serah.SFrom = "jemur";
                        arrserah.Add(serah);
                    }
                }
                #endregion rekam data serah
                T1_SerahProcessFacade T1SerahProcessFacade = new T1_SerahProcessFacade(jemur, arrserah);
                string strError = string.Empty;
                strError = T1SerahProcessFacade.Insert4mili();
            }
            //txtNoPalet.Text = string.Empty;
            txtJumlah.Text = string.Empty;
            if (strerror > -1)
            {
                DisplayAJAXMessage(this, "Data tersimpan, tekan tombol Enter untuk melanjutkan");
                BM_Dest.UpdatePalet(paletID, 1);
                //txtNoPalet.Text = string.Empty;
                txtJumlah.Text = string.Empty;
            }
            else
            {
                DisplayAJAXMessage(this, "Proses Simpan Error, ulangi input data atau hubungi IT Dept.");
            }

            LoadDataGrid();
            // ddlFormula.Focus();
            ddlShift.Focus();
            return 0;
        }

        private void LoadDataGrid()
        {
            ArrayList arrDestacking = new ArrayList();
            BM_DestackingFacade destf = new BM_DestackingFacade();
            if (Calendar1.SelectedDate.Year > 1900)
            {
                arrDestacking = destf.RetrieveByTglProduksi(Calendar1.SelectedDate.ToString("yyyyMMdd"));
                GridView1.DataSource = arrDestacking;
                GridView1.DataBind();
            }
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", "<script>MakeStaticHeader('" + GridView1.ClientID + "', 310, 100 , 15,false); </script>", false);
        }
        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrDestacking = new ArrayList();
            BM_DestackingFacade destf = new BM_DestackingFacade();
            if (Calendar1.SelectedDate.Year > 1900)
            {
                arrDestacking = destf.RetrieveByTglProduksi1(DateTime.Parse(txtdrtanggal.Text).ToString("yyyyMMdd"), DateTime.Parse(txtsdtanggal.Text).ToString("yyyyMMdd"));
                GridView1.DataSource = arrDestacking;
                GridView1.DataBind();
            }
        }
        private string ValidateText()
        {

            return string.Empty;
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string formats = string.Empty;
                formats = Calendar1.SelectedDate.GetDateTimeFormats().ToString();
                if (txtSearch.Text == string.Empty)
                {
                    //Calendar1.SelectedDate = DateTime.Parse(txtDate.Text);
                    LoadDataGrid();
                }
                else
                {
                    ArrayList arrDestacking = new ArrayList();
                    BM_DestackingFacade destf = new BM_DestackingFacade();
                    //Calendar1.SelectedDate = DateTime.Parse(txtDate.Text);
                    string strValue = string.Empty;
                    if (ddlSearch.SelectedIndex < 2 || txtSearch.Text != string.Empty)
                        strValue = "'" + txtSearch.Text + "'";
                    else
                        strValue = txtSearch.Text;

                    arrDestacking = destf.RetrieveCriteria(ddlSearch.SelectedValue, strValue, Calendar1.SelectedDate.ToString("MM/dd/yyyy"));
                    GridView1.DataSource = arrDestacking;
                    GridView1.DataBind();
                }
                ddlFormula.Focus();
                ddlFormula.Focus();
            }
            catch
            {
                txtDate.Text = Calendar1.SelectedDate.ToString("dd-MM-yyyy");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            if (txtSearch.Text == string.Empty)
                LoadDataGrid();
            else
            {
                ArrayList arrDestacking = new ArrayList();
                BM_DestackingFacade destf = new BM_DestackingFacade();
                string strValue = string.Empty;
                if (ddlSearch.SelectedIndex < 2 || txtSearch.Text != string.Empty)
                    strValue = "'" + txtSearch.Text + "'";
                else
                    strValue = txtSearch.Text;
                arrDestacking = destf.RetrieveCriteria(ddlSearch.SelectedValue, strValue, Calendar1.SelectedDate.ToString("MM/dd/yyyy"));
                GridView1.DataSource = arrDestacking;
                GridView1.DataBind();
            }
        }

        protected void txtNoProduksi_TextChanged(object sender, EventArgs e)
        {
            ////getfocus();
            //txtLokasi.Focus();
        }

        protected void txtLokasi_TextChanged(object sender, EventArgs e)
        {
            ////getfocus();
            //if (ddlPartno.SelectedItem.Text.Trim().ToUpper() == "INT-1-04012202440" || ddlPartno.SelectedItem.Text.Trim().ToUpper() == "INT-1-04012002400")
            //{
            //    RBProses.Checked = false;
            //    RBProses0.Checked = true;
            //}
            //else
            //{
            //    RBProses.Checked = true;
            //    RBProses0.Checked = false;
            //}
            txtNoPalet.Focus();
        }
        protected void txtNoPalet_TextChanged(object sender, EventArgs e)
        {
            ////getfocus();
            int autoserah = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(autoserah,0) autoserah from fc_items where rowstatus>-1 and partno='" + ddlPartno.SelectedItem.Text + "'";
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //result = sdr["groupname"].ToString();
                    autoserah = Convert.ToInt32(sdr["autoserah"]);
                }
            }
            txtNoProduksi.Text = ddlPartno.SelectedItem.Text;
            if (autoserah > 0)
                Label3.Text = "Insert Mode Ukuran Khusus, Auto serah ke lokasi pemotongan (Simetris)";
            else
                Label3.Text = "Insert Mode";
            txtJumlah.Focus();
        }

        protected void txtDate_PreRender(object sender, EventArgs e)
        {

        }
        private void getfocus()
        {
            //if (ddlFormula.SelectedIndex == 0)
            //    ddlFormula.Focus();
            //else
            //    if (ddlGroup.SelectedIndex == 0)
            //        ddlGroup.Focus();
            //    else
            //        if (txtNoProduksi.Text == string.Empty)
            //            txtNoProduksi.Focus();
            //        else
            //            if (txtLokasi.Text == string.Empty)
            //                txtLokasi.Focus();
            //            else
            //                if (txtNoPalet.Text == string.Empty)
            //                    txtNoPalet.Focus();
            //                else
            //                    if (txtJumlah.Text == string.Empty)
            //                        txtJumlah.Focus();
            //                    else
            //                        if (txtNoPalet.Text == string.Empty && txtJumlah.Text == string.Empty)
            //                            ddlFormula.Focus();
        }

        protected void txtNoProduksi_PreRender(object sender, EventArgs e)
        {
            ////getfocus();
        }
        protected void ddlFormula_PreRender(object sender, EventArgs e)
        {
            // //getfocus();
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            clearform();
            ddlFormula.Focus();
        }
        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
        }
        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
            {
                ArrayList arrDestacking = new ArrayList();
                BM_DestackingFacade destf = new BM_DestackingFacade();
                string strValue = string.Empty;
                if (ddlSearch.SelectedIndex < 2 || txtSearch.Text != string.Empty)
                    strValue = "'" + txtSearch.Text + "'";
                else
                    strValue = txtSearch.Text;
                arrDestacking = destf.RetrieveCriteria(ddlSearch.SelectedValue, strValue, Calendar1.SelectedDate.ToString("MM/dd/yyyy"));
                GridView1.DataSource = arrDestacking;
                GridView1.DataBind();
            }
            else
                LoadDataGrid();
        }
        protected void ChkHide_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide.Checked == false)
                Panel4.Visible = false;
            else
                Panel4.Visible = true;

        }
        protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
        {

        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int formulaID = 0;
            int groupID = 0;
            GridViewRow row = GridView1.Rows[index];
            FormulaFacade ff = new FormulaFacade();
            PlantGroupFacade pgf = new PlantGroupFacade();

            if (e.CommandName == "rubah")
            {
                if (row.Cells[10].Text == "Serah")
                {
                    DisplayAJAXMessage(this, "Data tidak bisa di edit");
                }
                else
                {
                    btnDelete.Enabled = true;
                    btnUpdate.Disabled = false;
                    btnTransfer.Enabled = false;
                    Label3.Text = "Edit Mode";
                    formulaID = ff.GetID(row.Cells[2].Text);
                    ddlFormula.SelectedValue = formulaID.ToString();
                    groupID = pgf.GetID(row.Cells[3].Text);
                    ddlGroup.SelectedValue = groupID.ToString();
                    txtNoProduksi.Text = row.Cells[5].Text;
                    txtNoPalet.Text = row.Cells[6].Text;
                    txtLokasi.Text = row.Cells[4].Text;
                    txtJumlah.Text = row.Cells[7].Text;
                    txtID.Text = row.Cells[0].Text;

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BM_Destacking dest = new BM_Destacking();
            BM_DestackingFacade BM_Dest = new BM_DestackingFacade();
            #region Verifikasi Closing Periode
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToInt32(Calendar1.SelectedDate.ToString("yyyy"));
            int Bulan = Convert.ToInt32(Calendar1.SelectedDate.ToString("MM"));
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion

            dest.ID = int.Parse(txtID.Text);
            BM_Dest.Delete(dest);
            clearform();
            btnDelete.Enabled = false;
            btnUpdate.Disabled = true;
            btnTransfer.Enabled = true;
            Label3.Text = "Insert Mode";
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            int insert = 0;
            if (ddlFormula.SelectedIndex == 0 || ddlGroup.SelectedIndex == 0 || txtNoProduksi.Text == string.Empty ||
                txtLokasi.Text == string.Empty || txtNoPalet.Text == string.Empty || txtJumlah.Text == string.Empty)
            {
                //DisplayAJAXMessage(this, "Input data belum lengkap.");
                return;
            }
            //insert = InsertItem();
            //if (insert == -1)
            //{
            //    DisplayAJAXMessage(this, "Error insert Data");
            //    return;
            //}


            //txtLokasi.Text = string.Empty;

            InsertItem();
            //txtNoPalet.Text = string.Empty;
            txtJumlah.Text = string.Empty;
            ddlFormula.Focus();
            Label3.Text = "Insert Mode";
            txtID.Text = "0";
        }
        protected void txtJumlah_PreRender(object sender, EventArgs e)
        {
            //getfocus();
        }
        protected void txtJumlah_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ddlFormula_TextChanged(object sender, EventArgs e)
        {
            LoadDataGrid();
            ddlGroup.Focus();
        }
        protected void ddlGroup_TextChanged(object sender, EventArgs e)
        {

        }


        protected void ddlFormula_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlFormula.SelectedItem.Text.Substring(0, 1) == "G" )
            //LoadPartno("INT-1-%' or PartNo like '%TBP-1-%' or PartNo like '%CLB-1-%' or PartNo like '%LOT-1-%' or PartNo like '%SUB-1-%'or PartNo like '%GRD-1-%' or PartNo like '%GRC-1-%' or PartNo like '%PNK-1-%");
            LoadPartno("'%-1-%' ");
            if (ddlFormula.SelectedItem.Text.Substring(0, 1) == "I")
                LoadPartno("INP");
            if (ddlFormula.SelectedItem.Text.Substring(0, 1) == "E")
                LoadPartno("EXT");
            if (ddlFormula.SelectedItem.Text.Substring(0, 1) == "T")
                LoadPartno("TBP");
            if (ddlFormula.SelectedItem.Text.Substring(0, 3) == "CLB")
                LoadPartno("CLB");
            if (ddlFormula.SelectedItem.Text.Substring(0, 3) == "LOT")
                LoadPartno("LOT");
            if (ddlFormula.SelectedItem.Text.Substring(0, 3) == "P")
                LoadPartno("PNK");
        }
        protected void ddlPartno_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////getfocus();
            int autoserah = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(autoserah,0) autoserah from fc_items where rowstatus>-1 and partno='" + ddlPartno.SelectedItem.Text + "'";
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //result = sdr["groupname"].ToString();
                    autoserah = Convert.ToInt32(sdr["autoserah"]);
                }
            }
            txtNoProduksi.Text = ddlPartno.SelectedItem.Text;
            if (autoserah > 0)
                Label3.Text = "Insert Mode Ukuran Khusus, Auto serah ke lokasi pemotongan (Simetris)";
            else
                Label3.Text = "Insert Mode";
            txtJumlah.Focus();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0)
                return;
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "List Destacking" + Calendar1.SelectedDate.ToString("ddMMyyyy") + ".xls"));
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            GridView1.AllowPaging = false;
            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");
            for (int i = 0; i < GridView1.HeaderRow.Cells.Count; i++)
            {
                GridView1.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
            }
            GridView1.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShift.SelectedIndex == 0)
            {
                txtTglIn.Text = DateTime.Parse(txtsdtanggal.Text).ToString("dd-MMM-yyyy") + " 07:00:00";
                txtTglOut.Text = DateTime.Parse(txtsdtanggal.Text).ToString("dd-MMM-yyyy") + " 15:00:00";
            }
            if (ddlShift.SelectedIndex == 1)
            {
                txtTglIn.Text = DateTime.Parse(txtsdtanggal.Text).ToString("dd-MMM-yyyy") + " 15:00:00";
                txtTglOut.Text = DateTime.Parse(txtsdtanggal.Text).ToString("dd-MMM-yyyy") + " 23:00:00";
            }
            if (ddlShift.SelectedIndex == 2)
            {
                txtTglIn.Text = DateTime.Parse(txtsdtanggal.Text).ToString("dd-MMM-yyyy") + " 15:00:00";
                txtTglOut.Text = DateTime.Parse(txtsdtanggal.Text).AddDays(1).ToString("dd-MMM-yyyy") + " 06:59:00";
            }
        }
    }
}