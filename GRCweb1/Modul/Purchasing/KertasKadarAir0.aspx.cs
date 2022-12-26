using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace GRCweb1.Modul.Purchasing
{
    public partial class KertasKadarAir0 : System.Web.UI.Page
    {
        private decimal TotalKA = 0;
        private decimal TotalData = 0;
        private const string SCRIPT_DOFOCUS =
              @"window.setTimeout('DoFocus()', 1);
            function DoFocus()
            {
                try {
                    document.getElementById('REQUEST_LASTFOCUS').focus();
                } catch (ex) {}
            }";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["Tusukan"] = null;
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");

                LoadSupplier();
                loadItemkertas();
                txtDocNo.Enabled = false;//.Text = GetDocumentNumber();
                HookOnFocus(this.Page as Control);
                //Page.ClientScript.RegisterStartupScript(typeof(Page), "ScriptDoFocus", SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request["__LASTFOCUS"]), true);
                btnCetak.Enabled = false;
                periode30p();
                tarikdatakertas();
                //btnTolak.Attributes.Add("onclick", "return confirm_revisi();");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", " SetFocus();", true);
            }
            act1.CompletionSetCount = ((Users)Session["Users"]).UnitKerjaID;
        }

        private void tarikdatakertas()
        {
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "exec tarik_data_kertas";
            SqlDataReader sdr = zl.Retrieve();
        }
        private void periode30p()
        {
            LPeriode1.Text = "01-" + DateTime.Parse(txtTanggal.Text).ToString("MMM-yyyy");
            LPeriode2.Text = DateTime.Parse(txtTanggal.Text).ToString("dd-MMM-yyyy");
        }
        private void loadItemkertas()
        {
            LoadJenisBarang();
        }

        private void LoadSupplier()
        {

        }
        private void LoadJenisBarang()
        {
            try
            {
                // string ItemCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialKertas", "BeritaAcara");
                ArrayList arrInventory = new ArrayList();
                InventoryFacade inventoryFacade = new InventoryFacade();
                inventoryFacade.Criteriane = " and ItemCode in(select itemcode from masterinputkertas where rowstatus>-1)";
                //arrInventory = (ItemCode == string.Empty) ? inventoryFacade.RetrieveByCriteria("A.ItemName", NamaBarang) :
                arrInventory = inventoryFacade.Retrieve();
                ddlNamaBarang.Items.Clear();
                ddlNamaBarang.Items.Add(new ListItem("-- Pilih Barang --", "0"));
                foreach (Inventory Inv in arrInventory)
                {
                    ddlNamaBarang.Items.Add(new ListItem(Inv.ItemName, Inv.ItemCode));
                }
                ddlNamaBarang.SelectedIndex = 1;
            }
            catch { }
        }
        protected void txtSupplier_Change(object sender, EventArgs e)
        {
            //get data from depo
            if (txtNOPOL.Text != string.Empty)
            {
                txtNOPOL_Change(null, null);
                inputKA();
            }

            LTotalKirim.Text = GetTotalKirim(((Users)Session["Users"]).UnitKerjaID.ToString(), txtSupplierID.Value.ToString(), DateTime.Parse(LPeriode1.Text).ToString("yyyyMMdd"),
                        DateTime.Parse(LPeriode2.Text).ToString("yyyyMMdd")).ToString();
            LTotalKirim2.Text = GetTotalKirimMax30(((Users)Session["Users"]).UnitKerjaID.ToString(), txtSupplierID.Value.ToString(), DateTime.Parse(LPeriode1.Text).ToString("yyyyMMdd"),
                        DateTime.Parse(LPeriode2.Text).ToString("yyyyMMdd")).ToString();
            if (LTotalKirim.Text.Trim() != "0")
            {
                LTotalKirim0.Text = Session["min30"].ToString();
                LTotalKirim1.Text = Session["max30"].ToString();
                Panel3.Visible = true;
            }
            else
            {
                LTotalKirim0.Text = "0";
                LTotalKirim1.Text = "0";
                Panel3.Visible = false;
            }

        }
        private int GetTotalKirimMax30(string plantID, string supplierID, string periode1, string periode2)
        {
            int result = 0;
            GetGroup(supplierID);
            string groupname = string.Empty;
            if (Session["groupname"] != null)
                groupname = Session["groupname"].ToString();
            else
            {
                return result;
            }
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "declare @plantID int " +
                "declare @ThnBln varchar(8) " +
                "set @ThnBln ='" + periode1.Substring(0, 6) + "' " +
                "select SUM(tkirim) tkirim from ( " +
                "select isnull(SUM(nettplant),0)tkirim from DeliveryKertasKA D where D.RowStatus>-1 and left(convert(char,D.TglCheck,112),6)=@ThnBln " +
                "and itemcode not in (select itemcode from Inventory where ItemName like '%kardus import%') and SupplierID in (select supplierID from supppurchgroup where rowstatus>-1 and groupname='" + groupname + "') and PlantID in (1,7,13) and NoSJ='0' and stdKA=30 " +
                "union all " +
                "select isnull(SUM(nettdepo),0)tkirim from DeliveryKertas D where D.RowStatus>-1 and left(convert(char,D.TglKirim,112),6)=@ThnBln " +
                "and itemcode not in (select itemcode from Inventory where ItemName like '%kardus import%') and SupplierID in (select supplierID from supppurchgroup where rowstatus>-1 and groupname='" + groupname + "') and PlantID in (1,7,13) and stdKA=30 )S";
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["tkirim"]);
                }
            }
            DateTime tglmulai = DateTime.Parse(txtTanggal.Text);
            //if (tglmulai.Day < 23)

            //    result = 0;
            return result;
        }
        protected void ddlNamaBarang_Change(object sender, EventArgs e)
        {
            if (txtNOPOL.Text.Length > 4)
            {
                ddlSJDepo_Change(null, null);
            }
        }
        protected void txtNOPOL_Change(object sender, EventArgs e)
        {
            try
            {
                int suppID = 0;
                int.TryParse(txtSupplierID.Value, out suppID);
                //if (suppID == 0) { DisplayAJAXMessage(this, "Nama Supplier tidak di kenal\\nSilahkan pilih lagi"); return; }
                DepoKertas dp = new DepoKertas();
                DeliveryKertas dk = new DeliveryKertas();
                ArrayList arrData = new ArrayList();
                string Criteria = " AND (POKAID is null or POKAID=0) and NOPOL='" + txtNOPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper() + "'";
                Criteria += " AND PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                Criteria += "AND Nosj NOT IN(SELECT NoSJ FROM DeliveryKertasKA WHERE DeliveryKertasKA.NoSJ=DeliveryKertas.NoSJ AND DeliveryKertasKA.RowStatus>-1 " +
                            "AND PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString() + " AND DeliveryKertasKA.ItemCode=DeliveryKertas.ItemCode )";
                arrData = dp.Retrieve(Criteria);
                ddlSJDepo.Items.Clear();
                string item = string.Empty;
                ddlSJDepo.Items.Add(new ListItem("--Pilih SJ Depo--", "0"));
                foreach (DeliveryKertas d in arrData)
                {
                    if (item != d.NoSJ.ToString())
                    {
                        ddlSJDepo.Items.Add(new ListItem(d.NoSJ.ToString(), d.NoSJ.ToString()));
                    }
                    item = d.NoSJ.ToString();
                }
            }
            catch { }
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }
        protected void ddlSJDepo_Change(object sender, EventArgs e)
        {
            try
            {
                Users users = (Users)Session["Users"];
                DepoKertas dp = new DepoKertas();
                DeliveryKertas dk = new DeliveryKertas();
                ArrayList arrData = new ArrayList();
                int stdka = 0;
                string Criteria = " AND (POKAID is null or POKAID=0) and NOPOL='" + txtNOPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper() + "'";
                Criteria += (ddlSJDepo.SelectedIndex > 0) ? " AND NoSJ='" + ddlSJDepo.SelectedValue + "'" : "";
                Criteria += " AND PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                Criteria += " AND ItemCode='" + ddlNamaBarang.SelectedValue + "'";
                arrData = dp.Retrieve(Criteria);
                string item = string.Empty;
                foreach (DeliveryKertas d in arrData)
                {
                    if (item != d.NoSJ.ToString())
                    {
                        txtSupplier.Text = d.SupplierName;
                        txtSupplierID.Value = d.SupplierID.ToString();
                    }
                    item = d.NoSJ.ToString();
                    //stKA.Text  = Convert.ToInt32(d.StdKA).ToString() ;
                    stKA.Text = Convert.ToDecimal(d.StdKA).ToString();
                }

                LTotalKirim.Text = GetTotalKirim(((Users)Session["Users"]).UnitKerjaID.ToString(), txtSupplierID.Value.ToString(), DateTime.Parse(LPeriode1.Text).ToString("yyyyMMdd"),
                        DateTime.Parse(LPeriode2.Text).ToString("yyyyMMdd")).ToString();
                if (LTotalKirim.Text.Trim() != "0")
                {
                    LTotalKirim0.Text = Session["min30"].ToString();
                    LTotalKirim1.Text = Session["max30"].ToString();
                    Panel3.Visible = true;
                }
                else
                {
                    LTotalKirim0.Text = "0";
                    LTotalKirim1.Text = "0";
                    Panel3.Visible = false;
                }

                if (users.UnitKerjaID == 7 || users.UnitKerjaID == 1)
                {
                    inputKA();
                }

                stKA.Text = string.Empty;
            }
            catch { }
        }
        protected void txtAvgKA_Change(object sender, EventArgs e)
        {
            btnAdd_Click(null, null);
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            //btnSimpan_Click(null, null);
            Session["Tusukan"] = null;
            btnSimpan.Enabled = true;
            txtDocNo.Enabled = true;
            btnCetak.Enabled = false;
            Response.Redirect("KertasKadarAir0.aspx");
        }
        protected void txtTusuk1_Change(object sender, EventArgs e)
        {
            try
            {
                decimal tusuk1 = 0;
                decimal tusuk2 = 0;
                decimal.TryParse(txtTusuk1.Text, out tusuk1);
                decimal.TryParse(txtTusuk2.Text, out tusuk2);
                txtAvgKA.Text = (tusuk2 > 0) ? ((tusuk1 + tusuk2) / 2).ToString("N2") : "";
            }
            catch { }
        }
        protected void txtTusuk2_Change(object sender, EventArgs e)
        {
            try
            {
                decimal tusuk1 = 0;
                decimal tusuk2 = 0;

                decimal.TryParse(txtTusuk1.Text, out tusuk1);
                decimal.TryParse(txtTusuk2.Text, out tusuk2);
                if (tusuk2 > 0)
                {
                    txtAvgKA.Text = ((tusuk1 + tusuk2) / 2).ToString("N2");
                    txtAvgKA_Change(null, null);
                }
                if (tusuk1 == 0) { txtTusuk1.Focus(); }
            }
            catch { }
        }
        protected void txtJmlTimbangan_Change(object sender, EventArgs e)
        {
            txtBK.Text = txtJmlTimbangan.Text;
            int suppID = 0;
            int.TryParse(txtSupplierID.Value, out suppID);
            if (suppID == 0) { DisplayAJAXMessage(this, "Nama Supplier tidak di kenal\\nSilahkan pilih lagi"); return; }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList arrTusukan = (Session["Tusukan"] != null) ? (ArrayList)Session["Tusukan"] : new ArrayList();
                QAKadarAir ka = new QAKadarAir();
                int NoBall = 0; decimal tusuk1 = 0; decimal tusuk2 = 0; decimal avege = 0;
                int balke = 1; int editbal = 0;
                int.TryParse(txtID.Value, out editbal);
                balke = int.Parse(txtBalke.Text);
                int.TryParse(txtBall.Text, out NoBall);
                decimal.TryParse(txtTusuk1.Text, out tusuk1);
                decimal.TryParse(txtTusuk2.Text, out tusuk2);
                decimal.TryParse(txtAvgKA.Text, out avege);
                //balke = (editbal == 0) ? balke : editbal;
                ka.BALKe = (balke == 0) ? 1 : balke;// int.Parse(txtBalke.Text);
                ka.NoBall = NoBall;
                ka.Tusuk1 = tusuk1;
                ka.Tusuk2 = tusuk2;
                ka.AvgTusuk = avege;
                arrTusukan.Add(ka);
                //arrTusukan.Sort();
                Session["Tusukan"] = arrTusukan;
                lstKA.DataSource = arrTusukan;
                lstKA.DataBind();

                txtBall.Text = "";
                txtTusuk1.Text = "";
                txtTusuk2.Text = "";
                txtAvgKA.Text = "";
                txtBall.Focus();
            }
            catch { }
        }
        protected void lstKA_Command(object sender, RepeaterCommandEventArgs e)
        {
            ArrayList arrData = (ArrayList)Session["Tusukan"];
            switch (e.CommandName)
            {
                case "hapus":
                    arrData.RemoveAt(int.Parse(e.CommandArgument.ToString()));
                    Session["Tusukan"] = arrData;
                    lstKA.DataSource = arrData;
                    lstKA.DataBind();
                    break;
                case "edit":
                    txtID.Value = e.CommandArgument.ToString();
                    txtBalke.Text = ((QAKadarAir)arrData[int.Parse(e.CommandArgument.ToString())]).BALKe.ToString();
                    txtTusuk1.Text = ((QAKadarAir)arrData[int.Parse(e.CommandArgument.ToString())]).Tusuk1.ToString();
                    txtTusuk2.Text = ((QAKadarAir)arrData[int.Parse(e.CommandArgument.ToString())]).Tusuk2.ToString();
                    txtTusuk1.Focus();
                    arrData.RemoveAt(int.Parse(e.CommandArgument.ToString()));
                    Session["Tusukan"] = arrData;
                    break;
            }
        }
        protected void lstKA00_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //try
            //{

            /** Syarat 1 = [Verifikasi apakah ada item kertas import dimana tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk01 = new DepoKertasKA();
            QAKadarAir ka01 = new QAKadarAir();
            ka01 = dk01.RetrieveDataKertasImport(ddlNamaBarang.SelectedValue);
            /** End Syarat 1 **/

            /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk02 = new DepoKertasKA();
            QAKadarAir ka02 = new QAKadarAir();
            ka02 = dk02.RetrieveDataSupplierByName(txtSupplier.Text.ToString().Trim());
            /** End Syarat 2 **/

            string DefaultStdKA = string.Empty;
            string stdNow = string.Empty;
            stdNow = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
            DefaultStdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");

            QAKadarAir qa = (QAKadarAir)e.Item.DataItem;
            ArrayList arrData = (ArrayList)Session["Tusukan"];
            TotalKA += qa.AvgTusuk;
            TotalData = arrData.Count;// TotalData + 1;
            txtAvgKadarAir.Text = (TotalKA / TotalData).ToString("N2");
            //beratpotongan
            txtJmlSample.Text = arrData.Count.ToString();
            string stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
            else
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
            //if (Convert.ToInt32(LTotalKirim.Text) >= Convert.ToInt32(LTotalKirim0.Text) && LTotalKirim.Text.Trim() != "0")
            //{
            //    stdKA = "30";
            //}

            if (ka01.ID > 0 || ka02.ID > 0)
            {
                if (stKA.Text == string.Empty)
                    stKA.Text = DefaultStdKA;
            }
            else
            {
                if (stKA.Text == string.Empty)
                    stKA.Text = stdKA;
            }

            decimal stdKAs1 = 0; decimal selisihKA1 = 0; string BeratPotongan = string.Empty; decimal potongan1 = 0; string BeratBersih = string.Empty;
            decimal selisihKA = 0; decimal potongan = 0;
            decimal stdKAs = 0; decimal bk = 0; decimal smph = 0; decimal avgka = 0;
            decimal.TryParse(txtSampah.Text/*.Replace(".", ",")*/, out smph);
            decimal.TryParse(txtAvgKadarAir.Text/*.Replace(".", ",")*/, out avgka);

            if (stdNow != DefaultStdKA)
            { decimal.TryParse(DefaultStdKA, out stdKAs1); }

            decimal.TryParse(stKA.Text, out stdKAs);

            decimal.TryParse(txtBK.Text/*.Replace(".", ",")*/, out bk);

            if (stdNow != DefaultStdKA)
            { selisihKA1 = (avgka - stdKAs1) + smph; }

            selisihKA = (avgka - stdKAs) + smph;

            if (stdNow != DefaultStdKA)
            { BeratPotongan = ((bk * (selisihKA1) / 100)).ToString(); }

            txtBeratPotongan.Text = Math.Round((bk * (selisihKA) / 100), 0, MidpointRounding.AwayFromZero).ToString("N0");
            txtBeratPotongan0.Text = BeratPotongan;
            if (stdNow != DefaultStdKA)
            { decimal.TryParse(BeratPotongan.Replace("(", "").Replace(")", ""), out potongan1); }

            decimal.TryParse(txtBeratPotongan.Text.Replace("(", "").Replace(")", ""), out potongan);

            if (stdNow != DefaultStdKA)
            { BeratBersih = (bk - potongan1).ToString("N2"); }

            txtBeratBersih.Text = Math.Round((bk - potongan), 0, MidpointRounding.AwayFromZero).ToString("N0");
            txtBeratBersih0.Text = (Convert.ToDecimal(txtBeratBersih.Text) * ((100 - Convert.ToDecimal(stKA.Text))/100)).ToString("N2");
            txtBalke.Text = (arrData.Count + 1).ToString();
            //}
            //catch { }
        }

        protected void lstKA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //try
            //{

            /** Syarat 1 = [Verifikasi apakah ada item kertas import dimana tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk01 = new DepoKertasKA();
            QAKadarAir ka01 = new QAKadarAir();
            ka01 = dk01.RetrieveDataKertasImport(ddlNamaBarang.SelectedValue);
            /** End Syarat 1 **/

            /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk02 = new DepoKertasKA();
            QAKadarAir ka02 = new QAKadarAir();
            ka02 = dk02.RetrieveDataSupplierByName(txtSupplier.Text.ToString().Trim());
            /** End Syarat 2 **/

            /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk03 = new DepoKertasKA();
            QAKadarAir ka03 = new QAKadarAir();
            //ka02 = dk02.RetrieveDataSupplier(int.Parse(txtSupplierID.Value));
            ka03 = dk03.RetrieveDataSupplierByName0(txtSupplier.Text.ToString().Trim());
            /** End Syarat 2 **/

            string DefaultStdKA = string.Empty;
            string stdNow = string.Empty;
            string stdNow2 = string.Empty;
            stdNow = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
            stdNow2 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA2", "PO");
            DefaultStdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            
            
            QAKadarAir qa = (QAKadarAir)e.Item.DataItem;
            ArrayList arrData = (ArrayList)Session["Tusukan"];
            TotalKA += qa.AvgTusuk;
            TotalData = arrData.Count;// TotalData + 1;
            txtAvgKadarAir.Text = (TotalKA / TotalData).ToString("N2");
            //beratpotongan
            Session["TotalKA"] = TotalKA;
            Session["TotalData"] = TotalData;

            txtJmlSample.Text = arrData.Count.ToString();
            string stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
            else
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");


            decimal stdKAs0 = 0; decimal stdKAs2 = 0;
            decimal stdKAs1 = 0; decimal selisihKA1 = 0; string BeratPotongan = string.Empty; decimal potongan1 = 0; string BeratBersih = string.Empty;
            decimal selisihKA = 0; decimal potongan = 0;
            decimal stdKAs = 0; decimal bk = 0; decimal smph = 0; decimal avgka = 0;
            decimal.TryParse(txtSampah.Text/*.Replace(".", ",")*/, out smph);
            decimal.TryParse(txtAvgKadarAir.Text/*.Replace(".", ",")*/, out avgka);

            string KA = string.Empty;
            decimal.TryParse(stdNow2, out stdKAs2);
            decimal.TryParse(stdNow, out stdKAs1);
            decimal.TryParse(DefaultStdKA, out stdKAs0);
            decimal.TryParse(txtBK.Text/*.Replace(".", ",")*/, out bk);

            /* item yang mendapatkan dispensasi KA*/
            string stdkadisp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAdisp", "PO");
            string[] arritemdispenKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("itemdispensasiKA", "PO").Split(',');
            if (arritemdispenKA.Contains(ddlNamaBarang.SelectedValue))
            {
                stdKA = stdkadisp;
                stdNow = stdkadisp;
                stdNow2 = stdkadisp;
            }

            if (ka01.ID > 0 || ka02.ID > 0)
            {
                if (stKA.Text == string.Empty)
                    stKA.Text = stdNow2;
                KA = stdNow2;
                LabelBP.Text = stdNow2 + "%";
                LabelBB.Text = stdNow2 + "%";
            }
            else
            {
                if (stKA.Text == string.Empty)
                    stKA.Text = stdKA;
                KA = stdKA;
                LabelBP.Text = stdKA + "%";
                LabelBB.Text = stdKA + "%";
            }

            if (stdNow != DefaultStdKA.Replace(".", ""))
            {
                /** Perhitungan 20% **/
                if (stdNow != KA)
                {
                    selisihKA1 = (avgka - stdKAs2) + smph;
                }
                /** Perhitungan 23% **/
                else
                {
                    selisihKA1 = (avgka - stdKAs1) + smph;
                }
                BeratPotongan = Math.Round((bk * (selisihKA1) / 100),0, MidpointRounding.AwayFromZero).ToString("N0");
                txtBeratPotongan.Text = BeratPotongan;
            }
            /** Perhitungan 0% **/
            selisihKA = (avgka - stdKAs0) + smph;
            txtBeratPotongan0.Text = ((bk * (selisihKA) / 100)).ToString();

            decimal.TryParse(BeratPotongan.Replace("(", "").Replace(")", ""), out potongan1);
            decimal.TryParse(txtBeratPotongan0.Text.Replace("(", "").Replace(")", ""), out potongan);

            BeratBersih = (bk - potongan1).ToString("N0");
            txtBeratBersih.Text = BeratBersih;
            txtBeratBersih0.Text = (Convert.ToDecimal(txtBeratBersih.Text) * ((100 - Convert.ToDecimal(stKA.Text))/100)).ToString("N2");
            
            txtBalke.Text = (arrData.Count + 1).ToString();

        }


        protected void txtSampah_Change(object sender, EventArgs e)
        {
            try
            {
                /** Syarat 1 = [Verifikasi apakah ada item kertas import dimana tidak ikut perhitungan KA yg Aktif saat ini] **/
                DepoKertasKA dk01 = new DepoKertasKA();
                QAKadarAir ka01 = new QAKadarAir();
                ka01 = dk01.RetrieveDataKertasImport(ddlNamaBarang.SelectedValue);
                /** End Syarat 1 **/

                /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
                DepoKertasKA dk02 = new DepoKertasKA();
                QAKadarAir ka02 = new QAKadarAir();
                //ka02 = dk02.RetrieveDataSupplier(int.Parse(txtSupplierID.Value));
                ka02 = dk02.RetrieveDataSupplierByName(txtSupplier.Text.ToString().Trim());
                /** End Syarat 2 **/

                /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
                DepoKertasKA dk03 = new DepoKertasKA();
                QAKadarAir ka03 = new QAKadarAir();
                //ka02 = dk02.RetrieveDataSupplier(int.Parse(txtSupplierID.Value));
                ka03 = dk03.RetrieveDataSupplierByName0(txtSupplier.Text.ToString().Trim());
                /** End Syarat 2 **/

                string DefaultStdKA = string.Empty;
                string stdNow = string.Empty;
                stdNow = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
                DefaultStdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                decimal selisihKA1 = 0;
                decimal avgka = 0; decimal smph = 0; decimal selisihKA = 0;
                //decimal.TryParse(txtSampah.Text.Replace(".", ","), out smph);
                // decimal.TryParse(txtAvgKadarAir.Text.Replace(".", ","), out avgka);
                smph = decimal.Parse(txtSampah.Text);
                avgka = decimal.Parse(txtAvgKadarAir.Text);
                string stdKA = string.Empty;
                Session["Netto"] = 0;
                if (stKA.Text.Trim() == string.Empty)
                {
                    stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                    if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                        stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
                    else
                        stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
                }
                else
                    stdKA = stKA.Text;
                decimal stdKAs1 = 0; string BeratPotongan = string.Empty; string BeratBersih = string.Empty; string BeratBersih2 = string.Empty;
                decimal stdKAs = 0; decimal bk = 0; decimal stdKAs2 = 0; decimal selisihKA2 = 0; string BeratPotongan2 = string.Empty;

                string A = txtAvgKadarAir.Text;

                /** Modifikasi 2 KA ( ditambahkan : 10-02-2020 oleh Beny ) **/
                if (stdNow != DefaultStdKA)
                {
                    //decimal.TryParse(stdKA, out stdKAs1);
                    //decimal.TryParse(DefaultStdKA, out stdKAs2);
                    stdKAs1=decimal.Parse(stdKA);
                    stdKAs2=decimal.Parse(DefaultStdKA);

                }
                else
                {
                    stdKAs=decimal.Parse(stdKA);
                }

                //decimal.TryParse(txtBK.Text.Replace(".", ","), out bk);
                bk=decimal.Parse(txtBK.Text.Replace(".", ","));

                if (stdNow != DefaultStdKA)
                {
                    selisihKA1 = (avgka - stdKAs1) + smph;
                    selisihKA2 = (avgka - stdKAs2) + smph;
                }
                else
                {
                    selisihKA = (avgka - stdKAs) + smph;
                }

                if (stdNow != DefaultStdKA)
                {

                    //string BeratPotongan2 = string.Empty;
                    BeratPotongan2 = (bk * (selisihKA2) / 100).ToString("N2");
                    

                    txtBeratPotongan.Text = Math.Round((bk * (selisihKA1) / 100), 0, MidpointRounding.AwayFromZero).ToString("N0");
                    txtBeratPotongan0.Text = BeratPotongan2;
                    Session["Potongan"] = BeratPotongan2;

                }
                else
                {
                    txtBeratPotongan.Text = Math.Round((bk * (selisihKA) / 100), 0, MidpointRounding.AwayFromZero).ToString("N0");
                    Session["Potongan"] = 0;
                }

                if (stdNow != DefaultStdKA)
                {
                    if (ka01.ID == 0 && ka02.ID == 0 || ka03.ID == 1)
                    {
                        //string BeratBersih2 = string.Empty;
                        BeratBersih2 = (bk - (decimal.Parse(BeratPotongan2))).ToString("N2");
                        txtBeratBersih.Text = Math.Round((bk - (decimal.Parse(txtBeratPotongan.Text))), 0, MidpointRounding.AwayFromZero).ToString("N0");
                        ////txtBeratBersih0.Text = BeratBersih2;
                        txtBeratBersih0.Text =  (Convert.ToDecimal(txtBeratBersih.Text) * ((100 - Convert.ToDecimal(stKA.Text))/100)).ToString("N2");

                        Session["Netto"] = BeratBersih2;
                    }
                    else
                    {
                        BeratBersih2 = (bk - (decimal.Parse(BeratPotongan2))).ToString("N2");
                        txtBeratBersih.Text = Math.Round((bk - (decimal.Parse(txtBeratPotongan.Text))), 0, MidpointRounding.AwayFromZero).ToString("N0");

                        txtBeratBersih0.Text =  (Convert.ToDecimal(txtBeratBersih.Text) * ((100 - Convert.ToDecimal(stKA.Text))/100) ).ToString("N2");

                        Session["Netto"] = BeratBersih2;
                    }
                }
                else
                {
                    txtBeratBersih.Text = Math.Round((bk - (decimal.Parse(txtBeratPotongan.Text))), 0, MidpointRounding.AwayFromZero).ToString("N0");
                    Session["Netto"] = 0;
                }
                /** end Modifikasi **/
            }
            catch { }
        }

        protected void txtSampah00_Change(object sender, EventArgs e)
        {
            try
            {
                /** Syarat 1 = [Verifikasi apakah ada item kertas import dimana tidak ikut perhitungan KA yg Aktif saat ini] **/
                DepoKertasKA dk01 = new DepoKertasKA();
                QAKadarAir ka01 = new QAKadarAir();
                ka01 = dk01.RetrieveDataKertasImport(ddlNamaBarang.SelectedValue);
                /** End Syarat 1 **/

                /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
                DepoKertasKA dk02 = new DepoKertasKA();
                QAKadarAir ka02 = new QAKadarAir();
                ka02 = dk02.RetrieveDataSupplierByName(txtSupplier.Text.ToString().Trim());
                /** End Syarat 2 **/

                string DefaultStdKA = string.Empty;
                string stdNow = string.Empty;
                string stdNow2 = string.Empty;
                stdNow = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
                stdNow2 = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA2", "PO");
                DefaultStdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                

                TotalKA = Convert.ToDecimal(Session["TotalKA"]);
                TotalData = Convert.ToDecimal(Session["TotalData"]);


                txtAvgKadarAir.Text = (TotalKA / TotalData).ToString("N2");

                string stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");

                /* item yang mendapatkan dispensasi KA*/
                string stdkadisp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAdisp", "PO");
                string[] arritemdispenKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("itemdispensasiKA", "PO").Split(',');
                if (arritemdispenKA.Contains(ddlNamaBarang.SelectedValue))
                {
                    stdKA = stdkadisp;
                    stdNow = stdkadisp;
                    stdNow2 = stdkadisp;
                }

                if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                    stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
                else
                    stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");

                decimal stdKAs0 = 0; decimal stdKAs2 = 0;
                decimal stdKAs1 = 0; decimal selisihKA1 = 0; string BeratPotongan = string.Empty; decimal potongan1 = 0; string BeratBersih = string.Empty;
                decimal selisihKA = 0; decimal potongan = 0;
                decimal stdKAs = 0; decimal bk = 0; decimal smph = 0; decimal avgka = 0;
                //decimal.TryParse(txtSampah.Text/*.Replace(".", ",")*/, out smph);
                //decimal.TryParse(txtAvgKadarAir.Text/*.Replace(".", ",")*/, out avgka);

                //string KA = string.Empty;
                //decimal.TryParse(stdNow2, out stdKAs2);
                //decimal.TryParse(stdNow, out stdKAs1);
                //decimal.TryParse(DefaultStdKA, out stdKAs0);
                //decimal.TryParse(txtBK.Text/*.Replace(".", ",")*/, out bk);
                smph= decimal.Parse(txtSampah.Text);

                string KA = string.Empty;
                stdKAs2=decimal.Parse(stdNow2);
                stdKAs1=decimal.Parse(stdNow);
                stdKAs0=decimal.Parse(DefaultStdKA);
                bk=decimal.Parse(txtBK.Text);

                if (ka01.ID > 0 || ka02.ID > 0)
                {
                    if (stKA.Text == string.Empty)
                        stKA.Text = stdNow2;
                    KA = stdNow2;
                }
                else
                {
                    if (stKA.Text == string.Empty)
                        stKA.Text = stdKA;
                    KA = stdKA;
                }

                if (stdNow != DefaultStdKA.Replace(".", ""))
                {
                    /** Perhitungan 20% **/
                    if (stdNow != KA)
                    {
                        selisihKA1 = (avgka - stdKAs2) + smph;
                    }
                    /** Perhitungan yg berlaku **/
                    else
                    {
                        selisihKA1 = (avgka - stdKAs1) + smph;
                    }
                    BeratPotongan = Math.Round((bk * (selisihKA1) / 100), 0, MidpointRounding.AwayFromZero).ToString();
                    txtBeratPotongan.Text = BeratPotongan;
                }

                /** Perhitungan 0% **/
                selisihKA = (avgka - stdKAs0) + smph;
                txtBeratPotongan0.Text = (bk * (selisihKA) / 100).ToString();

                potongan1=decimal.Parse(BeratPotongan);
                potongan=decimal.Parse(txtBeratPotongan0.Text);

                BeratBersih = Math.Round((bk - potongan1), 0, MidpointRounding.AwayFromZero).ToString("N0");

                txtBeratBersih.Text = BeratBersih;
                txtBeratBersih0.Text = (Convert.ToDecimal(txtBeratBersih.Text) * ((100 - Convert.ToDecimal(stKA.Text))/100)).ToString("N2");
                Session["Netto"] = BeratBersih;
            }
            catch { }
        }

        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            /** Syarat 1 = [Verifikasi apakah ada item kertas import dimana tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk01 = new DepoKertasKA();
            QAKadarAir ka01 = new QAKadarAir();
            ka01 = dk01.RetrieveDataKertasImport(ddlNamaBarang.SelectedValue);
            /** End Syarat 1 **/

            /** Syarat 2 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk02 = new DepoKertasKA();
            QAKadarAir ka02 = new QAKadarAir();
            //ka02 = dk02.RetrieveDataSupplier(int.Parse(txtSupplierID.Value));
            ka02 = dk02.RetrieveDataSupplierByName(txtSupplier.Text.ToString().Trim());
            /** End Syarat 2 **/

            /** Syarat 3 = [Verifikasi apakah ada Supplier yg tidak ikut perhitungan KA yg Aktif saat ini] **/
            DepoKertasKA dk03 = new DepoKertasKA();
            QAKadarAir ka03 = new QAKadarAir();
            ka03 = dk03.RetrieveDataSupplierByName0(txtSupplier.Text.ToString().Trim());
            /** End Syarat 3 **/

            ArrayList arrKA = new ArrayList();
            ArrayList arrData = (ArrayList)Session["Tusukan"];
            QAKadarAir qa = new QAKadarAir();
            DepoKertasKA dk = new DepoKertasKA();
            int sudahAda = 0;
            string stdKA = string.Empty; string DefaultStdKA = string.Empty; string stdNow = string.Empty;

            stdNow = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
            DefaultStdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");

            if (stKA.Text.Trim() == string.Empty)
            {
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                    stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
                else
                    stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
            }
            else
                stdKA = stKA.Text;

            if (Session["Netto"].ToString().Trim() == string.Empty)
                Session["Netto"] = 0;

            txtSampah_Change(null, null);

            decimal bk = 0; decimal jmlsample = 0; decimal prossample = 0; decimal jmlsamplebasah = 0;
            decimal avgka = 0; decimal bpotong = 0; decimal bpotongan = 0; decimal bb = 0; decimal bb2 = 0; decimal potonganstd = 0;
            decimal stdka = 0; decimal jmlbal = 0; decimal sampah = 0;
            decimal beratsample = 0; decimal beratsampah = 0;
            decimal.TryParse(txtBK.Text, out bk);
            decimal.TryParse(txtJmlSample.Text, out jmlsample);
            decimal.TryParse(txtjmlBal.Text, out jmlbal);
            decimal.TryParse(txtJmlSampleBasah.Text, out jmlsamplebasah);
            decimal.TryParse(txtProsSB.Text, out prossample);
            decimal.TryParse(txtberatDiPotong.Text, out bpotong);
            decimal.TryParse(txtBeratPotongan.Text, out bpotongan);
            decimal.TryParse(txtBeratBersih.Text, out bb);
            decimal.TryParse(Session["Netto"].ToString(), out bb2);
            decimal.TryParse(Session["Potongan"].ToString(), out potonganstd);

            decimal.TryParse(stdKA, out stdka);
            decimal.TryParse(txtAvgKadarAir.Text, out avgka);
            decimal.TryParse(txtSampah.Text, out sampah);
            decimal.TryParse(txtBeratSample.Text, out beratsample);
            decimal.TryParse(txtBeratSampah.Text, out beratsampah);
            if (Session["Tusukan"] == null) { return; }
            int keputusan = 0;
            keputusan = (chkKA.Checked == true) ? 1 : keputusan;
            keputusan = (chkKA1.Checked == true) ? 2 : keputusan;
            keputusan = (chkKA2.Checked == true) ? -1 : keputusan;
            qa.TglCheck = DateTime.Parse(txtTanggal.Text);
            qa.ItemCode = ddlNamaBarang.SelectedValue;
            qa.NoSJ = ddlSJDepo.SelectedValue;
            qa.NOPOL = txtNOPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper();
            qa.DefaultStdKA = Convert.ToInt32(DefaultStdKA);
            qa.GrossPlant = bk;
            qa.Brutto = bk;
            qa.NettPlant = bb;

            #region Lama
            /** modifikasi utk lokal no SJ oleh Beny : 06-01-2021 **/
            /** Per tanggal 05 Juli sdh tidak dipakai perhitungan dibawah ini **/
            //if (ka01.ID == 0 && ka02.ID == 0 || ka03.ID >0)
            //{
            //    decimal BPot0 = Convert.ToDecimal(txtBeratPotongan0.Text); 
            //    decimal A = bk * (sampah + avgka) / 100;           
            //    decimal bersih = bk - A;                     
            //    decimal Netto0 = Convert.ToDecimal(txtBeratBersih0.Text);
            //    qa.Potongan2 = BPot0;           
            //    qa.Netto = Math.Round(bb * ((100 - Convert.ToDecimal(stKA.Text))/100));
            //}
            //else
            //{
            //    qa.Potongan2 = potonganstd;
            //    qa.Netto = Math.Round(bb2);             
            //} 
            #endregion

            /** Hasil diskusi Purchasing, Logistik, QA Citeureup tidak ada pengecualian semua supplier di kali 0.8 
             *  02 Juli 2021 info by Phone (Bu Ika-Head Purchasing, Pak Hengky-Mgr Logistik, Susilo-Head QA) serta ada request by Email oleh QA **/
            decimal BPot0 = Convert.ToDecimal(txtBeratPotongan0.Text);
            decimal A = bk * (sampah + avgka) / 100;
            decimal bersih = bk - A;
            decimal Netto0 = Convert.ToDecimal(txtBeratBersih0.Text);
            qa.Potongan2 = BPot0;
            qa.Netto = bb * ((100 - Convert.ToDecimal(stKA.Text))/100);
            /** End **/

            qa.DefaultStdKA = Convert.ToInt32(DefaultStdKA);
            qa.ItemName = ddlNamaBarang.SelectedItem.Text;
            qa.JmlBAL = jmlbal;
            qa.JmlSample = jmlsample;
            qa.JmlSampleBasah = jmlsamplebasah;
            qa.BeratPotong = bpotong;
            qa.Potongan = bpotongan;
            qa.ProsSampleBasah = prossample;
            qa.AvgKA = avgka;
            qa.SupplierName = txtSupplier.Text.ToUpper();
            qa.SupplierID = int.Parse(txtSupplierID.Value);
            qa.RowStatus = keputusan;
            qa.CreatedBy = ((Users)Session["Users"]).UserID;
            qa.CreatedTime = DateTime.Now;
            qa.DocNo = GetDocumentNumber();
            qa.PlantID = ((Users)Session["Users"]).UnitKerjaID;
            qa.Sampah = sampah;
            qa.BeratSampah = beratsampah;
            qa.BeratSample = beratsample;
            qa.StdKA = stdka;
            qa.Approval = 0;
            arrKA.Add(qa);
            txtDocNo.Text = GetDocumentNumber();
            string criteria = " and NOPOL='" + txtNOPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper() + "'";
            criteria += " and GrossPlant=" + bk;
            criteria += " and NOSJ='" + ddlSJDepo.SelectedValue + "'";
            criteria += " and Itemcode='" + ddlNamaBarang.SelectedValue + "'";
            criteria += " and Convert(Char,TglCheck,112)=" + DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
            criteria += " and POKAID IS NULL";
            criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
            sudahAda = dk.RecordFound(criteria);//mencegah double input
            int result = (sudahAda == 0) ? dk.Insert0(qa) : 0;
            //int result = -1;
            qa.DKKAID = result;

            string deponame = string.Empty;
            ZetroView zl0 = new ZetroView();
            zl0.QueryType = Operation.CUSTOM;
            zl0.CustomQuery =
                "select deponame from deliverykertas where nosj='" + qa.NoSJ + "'";
            SqlDataReader sdr1 = zl0.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr1.HasRows)
            {
                while (sdr1.Read())
                {
                    deponame = sdr1["deponame"].ToString().Trim().ToUpper(); ;
                }
            }
            if (result > 0)
            {
                foreach (QAKadarAir qaka in arrData)
                {
                    QAKadarAir gk = new QAKadarAir();
                    gk.DKKAID = result;
                    gk.NoBall = qaka.NoBall;
                    gk.BALKe = qaka.BALKe;
                    gk.Tusuk1 = qaka.Tusuk1;
                    gk.Tusuk2 = qaka.Tusuk2;
                    gk.AvgTusuk = qaka.AvgTusuk;
                    gk.RowStatus = 0;
                    gk.CreatedBy = ((Users)Session["Users"]).UserID;
                    gk.CreatedTime = DateTime.Now;
                    int rest = dk.InsertDetail(gk);
                    if (rest == -1)
                    {
                        ZetroLib zl = new ZetroLib();
                        zl.TableName = "DeliveryKertasKA";
                        int rs = zl.DeleteRecords(result);
                    }
                }

                if (ka03.ID > 0 || ka01.ID > 0 && ka02.ID > 0)
                {
                    int hasil2 = 0;
                    hasil2 = dk.Insert2(qa);
                }

                if (stdNow != DefaultStdKA && ka01.ID == 0 && ka02.ID == 0)
                {
                    int hasil = 0;
                    hasil = dk.Insert2(qa);
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    if (deponame != "TEAM KHUSUS")
                    {
                        if (qa.PlantID == 7)
                            zl.CustomQuery =
                                "insert DeliveryKertasKADetail " +
                                "select (select id from deliverykertaska where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode +
                                "' and PlantID<>" + qa.PlantID + " " + ") DKKAID, BALKe, Tusuk1, Tusuk2, AvgTusuk, RowStatus, CreatedBy, CreatedTime, NoBall " +
                                "from DeliveryKertasKADetail where DKKAID=" + result + " " +

                                "insert delivbelikaDetail " +
                                "select (select idbeli from deliverykertas where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode +
                                "') DKKAID, BALKe, Tusuk1, Tusuk2, AvgTusuk, RowStatus, CreatedBy, CreatedTime, NoBall " +
                                "from DeliveryKertasKADetail where DKKAID=" + result +

                                " update DeliveryKertas set " +
                                " GrossDepo=" + qa.GrossPlant.ToString().Replace(",", ".") +
                                " ,NettDepo=" + qa.NettPlant.ToString().Replace(",", ".") + " " +
                                " ,KADepo=" + qa.AvgKA.ToString().Replace(",", ".") +
                                " ,StdKA=" + qa.StdKA.ToString().Replace(",", ".") + " " +
                                " ,NettPlant0 =" + qa.Netto.ToString().Replace(",", ".") +
                                "  where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' " +

                                "update DeliveryKertasKA set GrossPlant=" + qa.GrossPlant.ToString().Replace(",", ".") +
                                ",NettPlant=" + qa.NettPlant.ToString().Replace(",", ".") +
                                ",AvgKA=" + qa.AvgKA.ToString().Replace(",", ".") +
                                ",StdKA=" + qa.StdKA.ToString().Replace(",", ".") +
                                ",BeratPotong=" + qa.BeratPotong.ToString().Replace(",", ".") + ", " +
                                "Potongan=" + qa.Potongan.ToString().Replace(",", ".") +
                                ",Sampah=" + qa.Sampah.ToString().Replace(",", ".") +
                                " ,BeratSampah=" + qa.BeratSampah.ToString().Replace(",", ".") +
                                " ,BeratSample=" + qa.BeratSample.ToString().Replace(",", ".") +
                                ",JmlSample=" + qa.JmlSample.ToString().Replace(",", ".") +
                                ",JmlSampleBasah=" + qa.JmlSampleBasah.ToString().Replace(",", ".") + " " +
                                " where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' and PlantID<>" + qa.PlantID + " " +

                                "update DelivBeliKA set GrossPlant=" + qa.GrossPlant.ToString().Replace(",", ".") +
                                ",NettPlant=" + qa.NettPlant.ToString().Replace(",", ".") +
                                ",AvgKA=" + qa.AvgKA.ToString().Replace(",", ".") +
                                ",StdKA=" + qa.StdKA.ToString().Replace(",", ".") +
                                ",BeratPotong=" + qa.BeratPotong.ToString().Replace(",", ".") + ", " +
                                "Potongan=" + qa.Potongan.ToString().Replace(",", ".") +
                                ",Sampah=" + qa.Sampah.ToString().Replace(",", ".") +
                                " ,BeratSampah=" + qa.BeratSampah.ToString().Replace(",", ".") +
                                " ,BeratSample=" + qa.BeratSample.ToString().Replace(",", ".") +
                                ",JmlSample=" + qa.JmlSample.ToString().Replace(",", ".") +
                                ",JmlSampleBasah=" + qa.JmlSampleBasah.ToString().Replace(",", ".") + " " +
                                " where id=(select idbeli from DeliveryKertas where NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' and rowstatus>-1) " +

                                " update DelivBayarKertas set qty=" + qa.NettPlant.ToString().Replace(",", ".") +
                                ",totalharga= (case when isnull(dp,0)=0 then " + qa.NettPlant.ToString().Replace(",", ".") +
                                "*harga else dp end) where idbeli in (select idbeli from DeliveryKertas where NoSJ='" +
                                qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' and rowstatus>-1) "

                                //"exec [Get_HrgKhusus_Bayar_Kertas_Depo] '" + qa.NoSJ + "','" + qa.ItemCode + "','" + 
                                //DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd") + "'," + qa.NettPlant.ToString().Replace(",", ".") + ""
                                ;
                        else
                            if (qa.NoSJ.Trim().Length > 2)
                            zl.CustomQuery =
                                " update [sqlkrwg.grcboard.com].bpaskrwg.dbo.DeliveryKertas set " +
                                " GrossDepo=" + qa.GrossPlant.ToString().Replace(",", ".") +
                                " ,NettDepo=" + qa.NettPlant.ToString().Replace(",", ".") + " " +
                                " ,KADepo=" + qa.AvgKA.ToString().Replace(",", ".") +
                                " ,StdKA=" + qa.StdKA.ToString().Replace(",", ".") + " " +
                                " ,NettPlant0 =" + qa.Netto.ToString().Replace(",", ".") +
                                "  where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' " +

                                "update [sqlkrwg.grcboard.com].bpaskrwg.dbo.DeliveryKertasKA set GrossPlant=" + qa.GrossPlant.ToString().Replace(",", ".") +
                                ",NettPlant=" + qa.NettPlant.ToString().Replace(",", ".") + ",AvgKA=" + qa.AvgKA.ToString().Replace(",", ".") +
                                ",StdKA=" + qa.StdKA.ToString().Replace(",", ".") + ",BeratPotong=" + qa.BeratPotong.ToString().Replace(",", ".") + ", " +
                                "Potongan=" + qa.Potongan.ToString().Replace(",", ".") + ",Sampah=" + qa.Sampah.ToString().Replace(",", ".") +
                                " ,BeratSampah=" + qa.BeratSampah.ToString().Replace(",", ".") + " ,BeratSample=" + qa.BeratSample.ToString().Replace(",", ".") +
                                ",JmlSample=" + qa.JmlSample.ToString().Replace(",", ".") + ",JmlSampleBasah=" + qa.JmlSampleBasah.ToString().Replace(",", ".") + " " +
                                " where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' and PlantID<>" + qa.PlantID + " " +

                                "update [sqlkrwg.grcboard.com].bpaskrwg.dbo.DelivBeliKA set GrossPlant=" + qa.GrossPlant.ToString().Replace(",", ".") +
                                ",NettPlant=" + qa.NettPlant.ToString().Replace(",", ".") + ",AvgKA=" + qa.AvgKA.ToString().Replace(",", ".") +
                                ",StdKA=" + qa.StdKA.ToString().Replace(",", ".") + ",BeratPotong=" + qa.BeratPotong.ToString().Replace(",", ".") + ", " +
                                 "Potongan=" + qa.Potongan.ToString().Replace(",", ".") + ",Sampah=" + qa.Sampah.ToString().Replace(",", ".") +
                                " ,BeratSampah=" + qa.BeratSampah.ToString().Replace(",", ".") + " ,BeratSample=" + qa.BeratSample.ToString().Replace(",", ".") +
                                ",JmlSample=" + qa.JmlSample.ToString().Replace(",", ".") + ",JmlSampleBasah=" + qa.JmlSampleBasah.ToString().Replace(",", ".") + " " +
                                " where id=(select idbeli from [sqlkrwg.grcboard.com].bpaskrwg.dbo.DeliveryKertas where NoSJ='" + qa.NoSJ +
                                "' and ItemCode='" + qa.ItemCode + "' and rowstatus>-1) " +

                                " exec  [sqlkrwg.grcboard.com].bpaskrwg.dbo.spDeliveryKertasKADetail_Insert_from_Other " +
                                qa.PlantID + "," + result + ",'" + qa.NoSJ + "','" + qa.ItemCode + "' " +

                                " update [sqlkrwg.grcboard.com].bpaskrwg.dbo.DelivBayarKertas set qty=" + qa.NettPlant.ToString().Replace(",", ".") +
                                ",totalharga= (case when isnull(dp,0)=0 then " + qa.NettPlant.ToString().Replace(",", ".") +
                                "*harga else dp end) where idbeli in (select idbeli from [sqlkrwg.grcboard.com].bpaskrwg.dbo.DeliveryKertas " +
                                "where NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' and rowstatus>-1) "
                                //"exec [sqlkrwg.grcboard.com].bpaskrwg.dbo.Get_HrgKhusus_Bayar_Kertas_Depo '" + qa.NoSJ + "','" + qa.ItemCode + "','" + 
                                //DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd") + "'," + qa.NettPlant.ToString().Replace(",", ".") + ""
                                ;
                        else
                            zl.CustomQuery = string.Empty;
                    }
                    else
                    {
                        if (qa.PlantID == 7)
                            zl.CustomQuery =
                                " update DeliveryKertas set NettDepo=" + qa.NettPlant.ToString().Replace(",", ".") + " " +
                                " ,KADepo=" + qa.AvgKA.ToString().Replace(",", ".") +
                                " ,StdKA=" + qa.StdKA.ToString().Replace(",", ".") + " " +
                                " ,NettPlant0 =" + qa.Netto.ToString().Replace(",", ".") +
                                "  where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' " +

                                "update DeliveryKertasKA set NettPlant=" + qa.NettPlant.ToString().Replace(",", ".") +
                                ",AvgKA=" + qa.AvgKA.ToString().Replace(",", ".") +
                                ",StdKA=" + qa.StdKA.ToString().Replace(",", ".") +
                                ",BeratPotong=" + qa.BeratPotong.ToString().Replace(",", ".") + ", " +
                                "Potongan=" + qa.Potongan.ToString().Replace(",", ".") +
                                ",Sampah=" + qa.Sampah.ToString().Replace(",", ".") +
                                " ,BeratSampah=" + qa.BeratSampah.ToString().Replace(",", ".") +
                                " ,BeratSample=" + qa.BeratSample.ToString().Replace(",", ".") +
                                ",JmlSample=" + qa.JmlSample.ToString().Replace(",", ".") +
                                ",JmlSampleBasah=" + qa.JmlSampleBasah.ToString().Replace(",", ".") + " " +
                                " where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' and PlantID<>" + qa.PlantID + " ";
                        else
                            if (qa.NoSJ.Trim().Length > 2)
                            zl.CustomQuery =
                                " update [sqlkrwg.grcboard.com].bpaskrwg.dbo.DeliveryKertas set " +
                                " NettDepo=" + qa.NettPlant.ToString().Replace(",", ".") + " " +
                                " ,KADepo=" + qa.AvgKA.ToString().Replace(",", ".") +
                                " ,StdKA=" + qa.StdKA.ToString().Replace(",", ".") + " " +
                                " ,NettPlant0 =" + qa.Netto.ToString().Replace(",", ".") +
                                "  where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' " +

                                "update [sqlkrwg.grcboard.com].bpaskrwg.dbo.DeliveryKertasKA set " +
                                "NettPlant=" + qa.NettPlant.ToString().Replace(",", ".") + ",AvgKA=" + qa.AvgKA.ToString().Replace(",", ".") +
                                ",StdKA=" + qa.StdKA.ToString().Replace(",", ".") + ",BeratPotong=" + qa.BeratPotong.ToString().Replace(",", ".") + ", " +
                                "Potongan=" + qa.Potongan.ToString().Replace(",", ".") + ",Sampah=" + qa.Sampah.ToString().Replace(",", ".") +
                                " ,BeratSampah=" + qa.BeratSampah.ToString().Replace(",", ".") + " ,BeratSample=" + qa.BeratSample.ToString().Replace(",", ".") +
                                ",JmlSample=" + qa.JmlSample.ToString().Replace(",", ".") + ",JmlSampleBasah=" + qa.JmlSampleBasah.ToString().Replace(",", ".") + " " +
                                " where rowstatus>-1 and NoSJ='" + qa.NoSJ + "' and ItemCode='" + qa.ItemCode + "' and PlantID<>" + qa.PlantID + " ";
                        else
                            zl.CustomQuery = string.Empty;
                    }
                    SqlDataReader sdr = zl.Retrieve();
                }


                btnSimpan.Enabled = false;
                txtDocNo.Enabled = false;
                btnCetak.Attributes.Add("onclick", "CetakFrom('" + txtDocNo.Text + "')");
                btnCetak.Enabled = true;
            }
            Response.Redirect("KertasKadarAir0.aspx");
        }

        private string GetDocumentNumber()
        {
            DepoKertasKA dk = new DepoKertasKA();
            DepoFacade dp = new DepoFacade();
            Depo d = dp.RetrieveById(((Users)Session["Users"]).UnitKerjaID);
            int Nomor = dk.QAKadarAirDocNo();
            string result = d.InitialToko.ToUpper() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-" + (Nomor + 1).ToString().PadLeft(5, '0');
            return result;
        }
        protected void btnList_Clik(object sender, EventArgs e)
        {
            Response.Redirect("KertasKadarAirList0.aspx?v=1");
        }
        private void HookOnFocus(Control CurrentControl)
        {
            try
            {
                if ((CurrentControl is TextBox) ||
                    (CurrentControl is DropDownList) ||
                    (CurrentControl is ListBox) ||
                    (CurrentControl is Button))
                    //adds a script which saves active control on receiving focus 
                    //in the hidden field __LASTFOCUS.
                    (CurrentControl as WebControl).Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value='" + txtTusuk1.ClientID + "'} catch(e) {}");
                //checks if the control has children
                if (CurrentControl.HasControls())
                    //if yes do them all recursively
                    foreach (Control CurrentChildControl in CurrentControl.Controls)
                        HookOnFocus(CurrentChildControl);
            }
            catch { }
        }

        protected void txtBS_Change(object sender, EventArgs e)
        {
            try
            {
                decimal beratsample = 0;
                decimal beratsampah = 0;
                decimal prossampah = 0;
                decimal.TryParse(txtBeratSample.Text, out beratsample);
                decimal.TryParse(txtBeratSampah.Text, out beratsampah);
                if (beratsampah > 0)
                {
                    prossampah = (beratsampah / beratsample) * 100;
                    txtprosSampah.Text = prossampah.ToString("N2");
                    txtSampah.Text = prossampah.ToString("N2");
                    txtSampah_Change(null, null);
                }
                else
                {
                    txtprosSampah.Text = "0"; txtSampah.Text = "0"; txtSampah_Change(null, null);
                }
            }
            catch { }
        }
        protected void btnCetak_Click(object sender, EventArgs e)
        {

        }
        private void GetGroup_old(string supplierID)
        {
            //string result = string.Empty ;
            Session["groupname"] = null;
            Session["min30"] = null;
            Session["max30"] = null;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(groupname,'-')groupname,isnull(Min30,0)Min30,isnull(max30,0)max30 from supppurchgroup where rowstatus>-1 and supplierID=" + supplierID;
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //result = sdr["groupname"].ToString();
                    Session["groupname"] = sdr["groupname"].ToString();
                    Session["min30"] = sdr["min30"].ToString();
                    Session["max30"] = sdr["max30"].ToString();
                }
            }
            //return result;
        }

        private void GetGroup(string supplierID)
        {
            //string result = string.Empty ;
            Session["groupname"] = null;
            Session["min30"] = null;
            Session["max30"] = null;
            string linkserver = string.Empty;
            string plantID = ((Users)Session["Users"]).UnitKerjaID.ToString().Trim();
            if (plantID.Trim() != "7")
                linkserver = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;

            zl.CustomQuery = "select isnull(groupname,'-')groupname,isnull(Min30,0)Min30,isnull(max30,0)max30 " +
                "from " + linkserver + "supppurchgroup where rowstatus>-1 and supplierID=" + supplierID + " and plantid=" + plantID;

            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    //result = sdr["groupname"].ToString();
                    Session["groupname"] = sdr["groupname"].ToString();
                    Session["min30"] = sdr["min30"].ToString();
                    Session["max30"] = sdr["max30"].ToString();
                }
            }
            //return result;
        }

        private int IsProgram30(string supplierID)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select count(ID) jumlah from supppurchgroup where rowstatus>-1 and supplierID=" + supplierID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["jumlah"].ToString());
                }
            }
            return result;
        }

        private int GetTotalKirim_old(string plantID, string supplierID, string periode1, string periode2)
        {
            int result = 0;
            GetGroup(supplierID);
            int ikut = IsProgram30(supplierID);
            if (ikut == 0)
                return 0;
            string groupname = string.Empty;
            if (Session["groupname"] != null)
                groupname = Session["groupname"].ToString();
            else
            {
                return result;
            }
            string linkserver = string.Empty;
            if (plantID.Trim() == "7")
                linkserver = "[sqlctrp.grcboard.com].bpasctrp.dbo.";
            else
                linkserver = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            string oplantid = string.Empty;
            if (plantID.Trim() == "7")
                oplantid = "1";
            else
                oplantid = "1";
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            try
            {
                zl.CustomQuery =

                    "declare @plantID int " +
                    "declare @periode1 varchar(8) " +
                    "declare @periode2 varchar(8) " +
                    "set @periode1 ='" + periode1 + "' " +
                    "set @periode2 ='" + periode2 + "' " +
                    "set @plantID=" + plantID + " " +
                    "select SUM(tkirim) tkirim from ( " +
                    "select isnull(SUM(nettplant),0)tkirim from DeliveryKertasKA D where D.RowStatus>-1 and convert(char,D.TglCheck,112)>=@periode1 and convert(char,D.TglCheck ,112)<=@periode2  " +
                    "and itemcode not in (select itemcode from Inventory where ItemName like '%kardus import%') and SupplierID in (select supplierID from supppurchgroup where rowstatus>-1 and groupname='" + groupname + "' and plantID=" + plantID + ") and PlantID=@plantID and NoSJ='0'  " +
                    "union all " +
                    "select isnull(SUM(nettplant),0)tkirim from " + linkserver + "DeliveryKertasKA D where D.RowStatus>-1 and convert(char,D.TglCheck,112)>=@periode1 and convert(char,D.TglCheck ,112)<=@periode2  " +
                    "and SupplierID in (select supplierID from supppurchgroup where rowstatus>-1 and groupname='" + groupname + "') and PlantID=" + oplantid + " and NoSJ='0'  " +
                    "union all " +
                    "select isnull(SUM(nettdepo),0)tkirim from DeliveryKertas D where D.RowStatus>-1 and convert(char,D.TglKirim,112)>=@periode1 and convert(char,D.TglKirim,112)<=@periode2  " +
                    "and itemcode not in (select itemcode from Inventory where ItemName like '%kardus import%') and SupplierID in (select supplierID from supppurchgroup where rowstatus>-1 and groupname='" + groupname + "') and PlantID=@plantID )S";

                SqlDataReader sdr = zl.Retrieve();
                //ddlTujuanKirim.Items.Clear();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = Convert.ToInt32(sdr["tkirim"]);
                    }
                }
                DateTime tglmulai = DateTime.Parse(txtTanggal.Text);
            }
            catch { }
            //if (tglmulai.Day < 23)

            //    result = 0;
            return result;
        }

        private int GetTotalKirim(string plantID, string supplierID, string periode1, string periode2)
        {
            int result = 0;
            string linkserver = string.Empty;
            if (plantID.Trim() != "7")
                linkserver = "[sqlkrwg.grcboard.com].bpaskrwg.dbo.";
            GetGroup(supplierID);
            int ikut = IsProgram30(supplierID);
            if (ikut == 0)
                return 0;
            string groupname = string.Empty;
            if (Session["groupname"] != null)
                groupname = Session["groupname"].ToString();
            else
            {
                return result;
            }
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "exec " + linkserver + "gettotalkirimkertas  " + supplierID + "," + plantID + ",'" + periode2 + "'";

            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["totalkirim"]);
                }
            }
            return result;
        }

        protected void txtTanggal_TextChanged(object sender, EventArgs e)
        {
            periode30p();
        }
        private static string setExcelConn(string fileName, string fileType)
        {
            return setExcelConn(fileName, fileType, true);
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (Upload1.HasFile)
            {
                DataScaner0 fm = new DataScaner0();
                string FilePath = Upload1.PostedFile.FileName;
                string filename = Path.GetFileName(FilePath);
                string ext = Path.GetExtension(filename);
                fm.DropTable();
                if (ext.ToLower() == ".xls" || ext.ToLower() == ".xlsx")
                {
                    string UploadPath = "D:\\DATA LAMPIRAN PURCHN\\KAScaner\\" + Path.GetFileName(Upload1.PostedFile.FileName);
                    string conExcel = string.Empty;
                    string fixFilename = RenameIfExist(Path.GetFileName(Upload1.PostedFile.FileName)); ;
                    string[] nFile = fixFilename.Split(new string[] { "\\" }, StringSplitOptions.None);
                    string fileName = "D:\\DATA LAMPIRAN PURCHN\\KAScaner\\" + nFile[nFile.Count() - 1];
                    string fType = Upload1.PostedFile.ContentType.ToString();
                    conExcel = setExcelConn(fileName, fType, false);

                    if (conExcel != string.Empty && conExcel != "INVALID")
                    {
                        Upload1.SaveAs(fileName);
                        using (OleDbConnection cnn = new OleDbConnection(conExcel))
                        {
                            cnn.Open();
                            string sheet1 = cnn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                            DataTable dtExcel = new DataTable();

                            using (OleDbDataAdapter odb = new OleDbDataAdapter("Select * from [" + sheet1 + "]", conExcel))
                            {
                                odb.Fill(dtExcel);
                                lst.DataSource = dtExcel;
                                lst.DataBind();
                            }

                            cnn.Close();
                            string exists = this.CreatedTable(dtExcel);
                            SqlConnection sqlcon = new SqlConnection(Global.ConnectionString());
                            sqlcon.Open();

                            #region Kumpulin Data Kadar Air
                            if (exists == null)
                            {
                                foreach (DataColumn dc in dtExcel.Columns)
                                {
                                    string fld = "";
                                    // (dtExcel.Columns[0].ColumnName == "Date") ? "Tanggal" : dc.ColumnName.Replace(" ", "_");
                                    //fld = (dtExcel.Columns[1].ColumnName != "Speed Forming Monitoring") ? "Speed_Forming_Monitoring" :
                                    // (dc.ColumnName == "Date") ? "Tanggal" : dc.ColumnName.Replace(" ", "_");
                                    string Tahun = (dc.ColumnName.Length > 4) ? dc.ColumnName.Substring(0, 4) : dc.ColumnName;
                                    switch (Tahun)
                                    {
                                        case "Date": fld = "Tanggal"; break;
                                        case "2017": fld = "Speed_Forming_Monitoring"; break;
                                        default: fld = dc.ColumnName.Replace(" ", "_"); break;
                                    }
                                    if (exists == null)
                                    {
                                        SqlCommand createtable = new SqlCommand("CREATE TABLE PRD_KAScanerTemp (ID INT IDENTITY(1,1) NOT NULL," + fld + " varchar(MAX)NULL) ON[PRIMARY] ", sqlcon);
                                        createtable.ExecuteNonQuery();
                                        exists = "PRD_KAScanerTemp";// dtExcel.TableName;
                                    }
                                    else
                                    {
                                        SqlCommand addcolumn = new SqlCommand("ALTER TABLE PRD_KAScanerTemp ADD " + fld + " varchar(MAX) NULL", sqlcon);
                                        addcolumn.ExecuteNonQuery();
                                    }
                                }
                                using (SqlBulkCopy bulkcopy = new SqlBulkCopy(sqlcon))
                                {
                                    bulkcopy.DestinationTableName = "PRD_KAScanerTemp";
                                    bulkcopy.ColumnMappings.Add("SUPPLIER", "SUPPLIER"); bulkcopy.ColumnMappings.Add("NO_KEND", "NO_KEND"); bulkcopy.ColumnMappings.Add("TGL_MSK", "TGL_MSK"); bulkcopy.ColumnMappings.Add("JNS_KERTAS", "JNS_KERTAS"); bulkcopy.ColumnMappings.Add("KG_KOTOR", "KG_KOTOR"); bulkcopy.ColumnMappings.Add("KG_SAMPEL", "KG_SAMPEL"); bulkcopy.ColumnMappings.Add("KG_SAMPAH", "KG_SAMPAH"); bulkcopy.ColumnMappings.Add("BALE_01", "BALE_01"); bulkcopy.ColumnMappings.Add("BALE_02", "BALE_02"); bulkcopy.ColumnMappings.Add("BALE_03", "BALE_03"); bulkcopy.ColumnMappings.Add("BALE_04", "BALE_04"); bulkcopy.ColumnMappings.Add("BALE_05", "BALE_05"); bulkcopy.ColumnMappings.Add("BALE_06", "BALE_06"); bulkcopy.ColumnMappings.Add("BALE_07", "BALE_07"); bulkcopy.ColumnMappings.Add("BALE_08", "BALE_08"); bulkcopy.ColumnMappings.Add("BALE_09", "BALE_09"); bulkcopy.ColumnMappings.Add("BALE_10", "BALE_10"); bulkcopy.ColumnMappings.Add("BALE_11", "BALE_11"); bulkcopy.ColumnMappings.Add("BALE_12", "BALE_12"); bulkcopy.ColumnMappings.Add("BALE_13", "BALE_13"); bulkcopy.ColumnMappings.Add("BALE_14", "BALE_14"); bulkcopy.ColumnMappings.Add("BALE_15", "BALE_15"); bulkcopy.ColumnMappings.Add("BALE_16", "BALE_16"); bulkcopy.ColumnMappings.Add("BALE_17", "BALE_17"); bulkcopy.ColumnMappings.Add("BALE_18", "BALE_18"); bulkcopy.ColumnMappings.Add("BALE_19", "BALE_19"); bulkcopy.ColumnMappings.Add("BALE_20", "BALE_20"); bulkcopy.ColumnMappings.Add("BALE_21", "BALE_21"); bulkcopy.ColumnMappings.Add("BALE_22", "BALE_22"); bulkcopy.ColumnMappings.Add("BALE_23", "BALE_23"); bulkcopy.ColumnMappings.Add("BALE_24", "BALE_24"); bulkcopy.ColumnMappings.Add("BALE_25", "BALE_25"); bulkcopy.ColumnMappings.Add("BALE_26", "BALE_26"); bulkcopy.ColumnMappings.Add("BALE_27", "BALE_27"); bulkcopy.ColumnMappings.Add("BALE_28", "BALE_28"); bulkcopy.ColumnMappings.Add("BALE_29", "BALE_29"); bulkcopy.ColumnMappings.Add("BALE_30", "BALE_30"); bulkcopy.ColumnMappings.Add("BALE_31", "BALE_31"); bulkcopy.ColumnMappings.Add("BALE_32", "BALE_32"); bulkcopy.ColumnMappings.Add("BALE_33", "BALE_33"); bulkcopy.ColumnMappings.Add("BALE_34", "BALE_34"); bulkcopy.ColumnMappings.Add("BALE_35", "BALE_35"); bulkcopy.ColumnMappings.Add("BALE_36", "BALE_36"); bulkcopy.ColumnMappings.Add("BALE_37", "BALE_37"); bulkcopy.ColumnMappings.Add("BALE_38", "BALE_38"); bulkcopy.ColumnMappings.Add("BALE_39", "BALE_39"); bulkcopy.ColumnMappings.Add("BALE_40", "BALE_40");
                                    bulkcopy.WriteToServer(dtExcel);
                                    bulkcopy.Close();
                                }
                            }
                            #endregion

                            sqlcon.Close();
                            this.InsertKeTable();
                            DisplayAJAXMessage(this, "Upload data :" + dtExcel.Rows.Count.ToString("N2") + " Record updated");
                        }
                    }


                }
                else
                {
                    DisplayAJAXMessage(this, "Upload file excel error");
                    return;
                }
            }

        }
        private void inputKA()
        {
            Session["Tusukan"] = null;
            DataScaner0 fm = new DataScaner0();
            KAScaner0 sp = new KAScaner0();
            ArrayList arrData = new ArrayList();
            ArrayList arrTusukan = new ArrayList();
            ArrayList arrD = new ArrayList();
            if (txtJmlTimbangan.Text.Trim() == string.Empty)
                return;
            arrData = fm.RetrieveCek(txtSupplierID.Value, txtNOPOL.Text.Trim(), DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd"),
                ddlNamaBarang.SelectedValue.Trim(), Int32.Parse(txtJmlTimbangan.Text));
            int i = 0;
            if (arrData.Count > 0)
            {
                foreach (KAScaner0 s in arrData)
                {
                    txtBalke.Text = s.BALL;
                    txtBall.Text = "0";
                    txtTusuk1.Text = Convert.ToDecimal(s.KA).ToString();
                    txtTusuk2.Text = Convert.ToDecimal(s.KA).ToString();
                    txtJmlTimbangan.Text = Convert.ToInt32(s.KG_KOTOR).ToString();
                    txtBK.Text = Convert.ToInt32(s.KG_KOTOR).ToString();
                    txtBeratSample.Text = Convert.ToDecimal(s.KG_SAMPEL).ToString();
                    txtBeratSampah.Text = Convert.ToDecimal(s.KG_SAMPAH).ToString();
                    hitungtusukan();
                    i = i + 1;
                }
                txtBS_Change(null, null);
                txtjmlBal.Text = i.ToString();
            }
            arrTusukan = (ArrayList)Session["Tusukan"];
            lstKA.DataSource = arrTusukan;
            lstKA.DataBind();
        }
        private void addtusukan()
        {
            try
            {
                ArrayList arrTusukan = (Session["Tusukan"] != null) ? (ArrayList)Session["Tusukan"] : new ArrayList();
                QAKadarAir ka = new QAKadarAir();
                int NoBall = 0; decimal tusuk1 = 0; decimal tusuk2 = 0; decimal avege = 0;
                int balke = 1; int editbal = 0;
                int.TryParse(txtID.Value, out editbal);
                balke = int.Parse(txtBalke.Text);
                int.TryParse(txtBall.Text, out NoBall);
                decimal.TryParse(txtTusuk1.Text, out tusuk1);
                decimal.TryParse(txtTusuk2.Text, out tusuk2);
                decimal.TryParse(txtAvgKA.Text, out avege);
                //balke = (editbal == 0) ? balke : editbal;
                ka.BALKe = (balke == 0) ? 1 : balke;// int.Parse(txtBalke.Text);
                ka.NoBall = NoBall;
                ka.Tusuk1 = tusuk1;
                ka.Tusuk2 = tusuk2;
                ka.AvgTusuk = avege;
                arrTusukan.Add(ka);
                //arrTusukan.Sort();
                Session["Tusukan"] = arrTusukan;
                //lstKA.DataSource = arrTusukan;
                //lstKA.DataBind();

                txtBall.Text = "";
                txtTusuk1.Text = "";
                txtTusuk2.Text = "";
                txtAvgKA.Text = "";
                txtBall.Focus();
            }
            catch { }
        }
        private void hitungtusukan()
        {
            try
            {
                decimal tusuk1 = 0;
                decimal tusuk2 = 0;

                decimal.TryParse(txtTusuk1.Text, out tusuk1);
                decimal.TryParse(txtTusuk2.Text, out tusuk2);
                if (tusuk2 > 0)
                {
                    txtAvgKA.Text = ((tusuk1 + tusuk2) / 2).ToString("N2");
                    addtusukan();
                }
                if (tusuk1 == 0) { txtTusuk1.Focus(); }
            }
            catch { }
        }
        private void InsertKeTable()
        {
            DataScaner0 fm = new DataScaner0();
            KAScaner0 sp = new KAScaner0();
            ArrayList arrData = new ArrayList();
            ArrayList arrD = new ArrayList();
            arrData = fm.Retrieve(true);

            //DataScaner fms = new DataScaner();
            //fms.DropTable();
        }
        private static string RenameIfExist(string fileName)
        {
            string result = string.Empty;
            string[] nmFile = fileName.Split(new string[] { "\\" }, StringSplitOptions.None);
            string[] tmpFile = fileName.Split(new string[] { "." }, StringSplitOptions.None);
            if (File.Exists(("D:\\DATA LAMPIRAN PURCHN\\KAscaner\\" + nmFile[nmFile.Count() - 1])))
            {
                Random rnd = new Random();
                int numExt = rnd.Next(1, 9999);
                string sExt = numExt.ToString().PadLeft(4, '0');

                var tmpResult = "";
                int arrLen = tmpFile.Length;
                for (var idx = 0; idx < arrLen; idx++)
                {
                    if (idx < (arrLen - 1))
                    {
                        tmpResult += tmpFile[idx].ToString();
                    }
                    else if (idx == (arrLen - 1))
                    {
                        tmpResult += sExt + "." + tmpFile[idx].ToString();
                    }
                }
                result = tmpResult;
            }
            else
            {
                result = fileName;
            }
            return result;
        }
        private static string setExcelConn(string fileName, string fileType, bool HDR)
        {

            string result = "INVALID";
            if ((fileName != null || fileName != string.Empty) &&
                (fileType != null || fileType != string.Empty))
            {

                string incHDR = ""; //(HDR == true) ? ";HDR=YES" : "";
                                    //try
                                    //{
                switch (fileType)
                {
                    case "application/vnd.ms-excel":
                        result = "Provider=Microsoft.Jet.OLEDB.4.0;";
                        result += "Data Source=" + fileName + ";";
                        result += "Extended Properties='Excel 8.0" + incHDR + ";";
                        result += " IMEX=1'";
                        break;
                    case "application/octet-stream":
                        result = "Provider=Microsoft.Jet.OLEDB.4.0;";
                        result += "Data Source=" + fileName + ";";
                        result += "Extended Properties='Excel 8.0" + incHDR + ";";
                        result += " IMEX=1'";
                        break;
                    case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                        result = "Provider=Microsoft.ACE.OLEDB.12.0;";
                        result += "Data Source=" + fileName + ";";
                        result += "Extended Properties='Excel 8.0" + incHDR + ";";
                        result += " IMEX=1'";
                        break;
                    default:
                        result = "INVALID";
                        break;
                }
                //}
                //catch (Exception ex)
                //{
                //    result = ex.Message;
                //}
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        private string CreatedTable(DataTable dtExcel)
        {
            SqlConnection sqlcon = new SqlConnection(Global.ConnectionString());
            sqlcon.Open();
            string exists = null;
            try
            {

                SqlCommand cmd = new SqlCommand("SELECT * FROM sysobjects where name = 'PRD_KAScanerTemp'", sqlcon);
                exists = cmd.ExecuteScalar().ToString();
                sqlcon.Close();
            }
            catch (Exception exce)
            {
                exists = null;
                sqlcon.Close();
            }
            return exists;
        }
        protected void btnTolak_Click(object sender, EventArgs e)
        {
            mpePopUp.Show();

            
        }

        protected void txtjmlBal_TextChanged(object sender, EventArgs e)
        {
            if (ddlSJDepo.SelectedIndex > 0 && txtSupplier.Text.Trim() != string.Empty)
            {
                int JmlBal = 0;
                JmlBal = CekJmlBal();
                if (JmlBal > 0)
                {
                    if (Convert.ToInt32(txtjmlBal.Text) != JmlBal)
                    {
                        DisplayAJAXMessage(this, "Jumlah ball yang diinput plant tidak sama dengan jumlah ball yang diinput Depo, Silahkan hubungi Purchasing Dept.");
                        txtjmlBal.Text = string.Empty;
                        btnSimpan.Enabled = false;
                        return;
                    }
                    else
                        btnSimpan.Enabled = true;
                }
            }
        }

        protected int CekJmlBal()
        {
            int jmlbal = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select top 1 jmlbal from deliverykertas where nosj='" + ddlSJDepo.SelectedItem.Text.Trim() + "' ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jmlbal = Convert.ToInt32(sdr["jmlbal"]);
                }
            }
            return jmlbal;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //if (Session["AlasanTolak"] != null)
            //{
                if (txtAlasanTolak.Text.Trim() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Penolakan harus diisi");
                    return;
                }
                ArrayList arrKA = new ArrayList();
                ArrayList arrData = (ArrayList)Session["Tusukan"];
                QAKadarAir qa = new QAKadarAir();
                DepoKertasKA dk = new DepoKertasKA();
                int sudahAda = 0;
                string stdKA = string.Empty;
                if (stKA.Text.Trim() == string.Empty)
                {
                    stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                    if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                        stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
                    else
                        stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
                }
                else
                    stdKA = stKA.Text;
                decimal bk = 0; decimal jmlsample = 0; decimal prossample = 0; decimal jmlsamplebasah = 0;
                decimal avgka = 0; decimal bpotong = 0; decimal bpotongan = 0; decimal bb = 0;
                decimal stdka = 0; decimal jmlbal = 0; decimal sampah = 0;
                decimal beratsample = 0; decimal beratsampah = 0;
                decimal.TryParse(txtBK.Text, out bk);
                decimal.TryParse(txtJmlSample.Text, out jmlsample);
                decimal.TryParse(txtjmlBal.Text, out jmlbal);
                decimal.TryParse(txtJmlSampleBasah.Text, out jmlsamplebasah);
                decimal.TryParse(txtProsSB.Text, out prossample);
                decimal.TryParse(txtberatDiPotong.Text, out bpotong);
                decimal.TryParse(txtBeratPotongan.Text, out bpotongan);
                decimal.TryParse(txtBeratBersih.Text, out bb);
                decimal.TryParse(stdKA, out stdka);
                decimal.TryParse(txtAvgKadarAir.Text.Replace(".", ","), out avgka);
                decimal.TryParse(txtSampah.Text.Replace(".", ","), out sampah);
                decimal.TryParse(txtBeratSample.Text, out beratsample);
                decimal.TryParse(txtBeratSampah.Text, out beratsampah);
                //if (Session["Tusukan"] == null) { return; }
                int keputusan = 0;
                keputusan = (chkKA.Checked == true) ? 1 : keputusan;
                keputusan = (chkKA1.Checked == true) ? 2 : keputusan;
                keputusan = (chkKA2.Checked == true) ? -1 : keputusan;
                qa.TglCheck = DateTime.Parse(txtTanggal.Text);
                qa.ItemCode = ddlNamaBarang.SelectedValue;
                qa.NoSJ = ddlSJDepo.SelectedValue;
                qa.NOPOL = txtNOPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper();
                qa.GrossPlant = bk;
                qa.ItemName = ddlNamaBarang.SelectedItem.Text;
                qa.JmlBAL = jmlbal;
                qa.JmlSample = jmlsample;
                qa.JmlSampleBasah = jmlsamplebasah;
                qa.BeratPotong = bpotong;
                qa.Potongan = bpotongan;
                qa.ProsSampleBasah = prossample;
                qa.AvgKA = avgka;
                qa.NettPlant = 0;
                qa.SupplierName = txtSupplier.Text.ToUpper();
                qa.SupplierID = int.Parse(txtSupplierID.Value);
                qa.RowStatus = keputusan;
                qa.CreatedBy = ((Users)Session["Users"]).UserID;
                qa.CreatedTime = DateTime.Now;
                qa.DocNo = GetDocumentNumber();
                qa.PlantID = ((Users)Session["Users"]).UnitKerjaID;
                qa.Sampah = sampah;
                qa.BeratSampah = beratsampah;
                qa.BeratSample = beratsample;
                qa.StdKA = stdka;
                qa.Approval = 2;
                arrKA.Add(qa);
                txtDocNo.Text = GetDocumentNumber();
                string criteria = " and NOPOL='" + txtNOPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper() + "'";
                criteria += " and GrossPlant=" + bk;
                criteria += " and NOSJ='" + ddlSJDepo.SelectedValue + "'";
                criteria += " and Convert(Char,TglCheck,112)=" + DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
                criteria += " and POKAID IS NULL";
                criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                sudahAda = dk.RecordFound(criteria);//mencegah double input
                int result = (sudahAda == 0) ? dk.Insert(qa) : 0;
                if (result > 0)
                {
                    btnSimpan.Enabled = false;
                    txtDocNo.Enabled = false;
                    //btnCetak.Attributes.Add("onclick", "CetakFrom('" + txtDocNo.Text + "')");
                    //btnCetak.Enabled = true;
                }
                string updateDP = string.Empty;
                if (((Users)Session["Users"]).UnitKerjaID == 7)
                    updateDP = "update delivbayarkertas set harga=0,totalharga=0,qty=0 where idbeli in (select idbeli from deliverykertas where nosj='" + qa.NoSJ + "') ";
                else
                    updateDP = "update [sqlkrwg].bpaskrwg.dbo.delivbayarkertas set harga=0,totalharga=0,qty=0 where idbeli in (select idbeli from [sqlkrwg].bpaskrwg.dbo.deliverykertas where nosj='" + qa.NoSJ + "') ";
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "insert ListPenolakanKertas (nosj,alasan,createdby,createdtime)values('" + qa.NoSJ + "','" +
                    txtAlasanTolak.Text.Trim() + "','" + ((Users)Session["Users"]).UserName + "',getdate()) ";
                SqlDataReader sdr = zl.Retrieve();

                Session["AlasanCancel"] = null;
                Session["AlasanBatal"] = null;
                btnNew_Click(null, null);
            //}
        }
    }

    public class KAScaner0 : GRCBaseDomain
    {
        public string SUPPLIER { get; set; }
        public string NO_KEND { get; set; }
        public string TGL_MSK { get; set; }
        public string KG_KOTOR { get; set; }
        public string KG_SAMPEL { get; set; }
        public string KG_SAMPAH { get; set; }
        public string KA { get; set; }
        public string BALL { get; set; }
    }
    public class DataScaner0
    {
        ArrayList arrData = new ArrayList();
        KAScaner0 spc = new KAScaner0();
        public string Criteria { get; set; }
        public string MachineLine { get; set; }

        private string FieldData()
        {
            string result = string.Empty;
            switch (MachineLine)
            {
                case "1": result = "ID,Tanggal,Speed_Forming_Monitoring,F3 "; break;
                case "2": result = "ID,F4 Tanggal,F5,F6 "; break;
                case "3": result = "ID,F7 Tanggal,F8,F9 "; break;
                case "4": result = "ID,F10 Tanggal,F11,F12 "; break;
                case "5": result = "ID,F13 Tanggal,F14,F15 "; break;
                case "6": result = "ID,F16 Tanggal,F17,F18 "; break;
            }
            return result;
        }
        private string FieldData(bool Where)
        {
            string result = string.Empty;
            switch (MachineLine)
            {
                case "1": result = " AND Tanggal IS NOT NULL "; break;
                case "2": result = " AND F4 IS NOT NULL "; break;
                case "3": result = " AND F7 IS NOT NULL"; break;
                case "4": result = " AND F10 IS NOT NULL "; break;
                case "5": result = " AND F13 IS NOT NULL "; break;
                case "6": result = " AND F16 IS NOT NULL "; break;
            }
            return result;
        }
        public ArrayList Retrieve(bool Data)
        {
            string strSQL = "insert PRD_KAScaner select * from (select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_01,' ',''),',','.')KA,01 ball , 0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_02,' ',''),',','.')KA,02 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_03,' ',''),',','.')KA,03 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_04,' ',''),',','.')KA,04 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_05,' ',''),',','.')KA,05 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_06,' ',''),',','.')KA,06 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_07,' ',''),',','.')KA,07 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_08,' ',''),',','.')KA,08 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_09,' ',''),',','.')KA,09 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_10,' ',''),',','.')KA,10 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_11,' ',''),',','.')KA,11 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_12,' ',''),',','.')KA,12 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_13,' ',''),',','.')KA,13 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_14,' ',''),',','.')KA,14 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_15,' ',''),',','.')KA,15 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_16,' ',''),',','.')KA,16 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_17,' ',''),',','.')KA,17 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_18,' ',''),',','.')KA,18 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_19,' ',''),',','.')KA,19 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_20,' ',''),',','.')KA,20 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_21,' ',''),',','.')KA,21 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_22,' ',''),',','.')KA,22 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_23,' ',''),',','.')KA,23 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_24,' ',''),',','.')KA,24 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_25,' ',''),',','.')KA,25 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_26,' ',''),',','.')KA,26 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_27,' ',''),',','.')KA,27 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_28,' ',''),',','.')KA,28 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_29,' ',''),',','.')KA,29 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_30,' ',''),',','.')KA,30 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_31,' ',''),',','.')KA,31 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_32,' ',''),',','.')KA,32 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_33,' ',''),',','.')KA,33 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_34,' ',''),',','.')KA,34 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_35,' ',''),',','.')KA,35 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_36,' ',''),',','.')KA,36 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_37,' ',''),',','.')KA,37 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_38,' ',''),',','.')KA,38 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_39,' ',''),',','.')KA,39 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp  union all 	select SUPPLIER,NO_KEND,'20'+substring(TGL_MSK,5,2)+substring(TGL_MSK,3,2)+substring(TGL_MSK,1,2)TGL_MSK,KG_KOTOR,replace(replace(KG_SAMPEL,' ',''),',','.')KG_SAMPEL,replace(replace(KG_SAMPAH,' ',''),',','.')KG_SAMPAH,replace(replace(BALE_40,' ',''),',','.')KA,40 ball ,0 rowstatus,JNS_KERTAS KERTAS from PRD_KAScanerTemp " +
                ")A where KA<>'.'";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr, Data));
                }
            }
            return arrData;
        }
        public ArrayList RetrieveCek(string supplier, string nopol, string tglmasuk, string itemcode, int BK)
        {
            string strSQL = "select * from PRD_KAScaner where kertas in (select kode from MasterInputKertas where itemcode ='" + itemcode + "') and supplier in " +
                "(select suppliercode from SuppPurch where id=" + supplier + ") and no_kend='" + nopol +
                "' and convert(char,tgl_msk,112)='" + tglmasuk + "' and RowStatus>-1 and kg_kotor=" + BK;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GenerateObject(sdr, true));
                }
            }
            return arrData;
        }

        private string mc = string.Empty;
        private KAScaner0 GenerateObject(SqlDataReader sdr)
        {
            spc = new KAScaner0();
            if (sdr["ID"].ToString() == "7") { mc = (sdr[2].ToString().Replace(".00", "")); }
            //if (sdr["ID"].ToString() == "9") { spc.MachineNo = int.Parse(mc); spc.MachineName = sdr[2].ToString(); }
            return spc;
        }
        private KAScaner0 GenerateObject(SqlDataReader sdr, bool data)
        {
            if (sdr[2] != DBNull.Value)
            {
                spc = new KAScaner0();
                spc.SUPPLIER = sdr["SUPPLIER"].ToString();
                spc.NO_KEND = sdr["NO_KEND"].ToString();
                spc.TGL_MSK = sdr["TGL_MSK"].ToString();
                spc.KG_KOTOR = sdr["KG_KOTOR"].ToString();
                spc.KG_SAMPEL = sdr["KG_SAMPEL"].ToString();
                spc.KG_SAMPAH = sdr["KG_SAMPAH"].ToString();
                spc.KA = sdr["KA"].ToString().ToString();
                spc.BALL = sdr["BALL"].ToString().ToString();
            }
            return spc;
        }

        private KAScaner0 GenerateObj(SqlDataReader sdr)
        {
            spc = new KAScaner0();
            //spc.ID = int.Parse(sdr["ID"].ToString());
            //spc.MachineNo = int.Parse(sdr["MachineNo"].ToString());
            //spc.MachineName = sdr["MachineName"].ToString();
            //spc.Tanggal = DateTime.Parse(sdr["Tanggal"].ToString());
            //spc.Jam = DateTime.Parse(sdr["Tanggal"].ToString());
            //spc.Speed = int.Parse(sdr["Speed"].ToString());
            //spc.SpeedAvg = int.Parse(sdr["Avg"].ToString());
            return spc;
        }
        public void DropTable()
        {
            string strSQL = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRD_KAScanerTemp]') AND type in (N'U'))" +
                          "DROP TABLE [dbo].[PRD_KAScanerTemp] ";
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        }


    }
}