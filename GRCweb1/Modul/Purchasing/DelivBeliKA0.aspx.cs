using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using BusinessFacade;
using Domain;


namespace GRCweb1.Modul.Purchasing
{
    public partial class DelivBeliKA0 : System.Web.UI.Page
    {
        private decimal TotalKA = 0;
        private decimal TotalData = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["Tusukan"] = null;

                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDeptKertas();
                LoadSupplier();
                //LoadStdKA();
                LoadChecker();
                loadItemkertas();
                LoadTujuanKirim();
                txtDocNo.Text = GetDocumentNumber();
                act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
                txtDocNo.Enabled = false;//.Text = GetDocumentNumber();
                                         //HookOnFocus(this.Page as Control);
                                         //Page.ClientScript.RegisterStartupScript(typeof(Page), "ScriptDoFocus", SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request["__LASTFOCUS"]), true);
                btnCetak.Enabled = false;
                periode30p();
                if (Request.QueryString["docno"] != null)
                {
                    LoadBeli(Request.QueryString["docno"].ToString());
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", " SetFocus();", true);
                act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
            }

        }
        private void LoadBeli(string docno)
        {
            DepoKertasKA dk = new DepoKertasKA();
            QAKadarAir ka = new QAKadarAir();
            string Criteria = " AND DocNo='" + docno + "')beli";
            ka = dk.RetrieveBeli(Criteria, true);
            txtDocNo.Text = ka.DocNo;
            ddlDepo.SelectedValue = ka.DepoID.ToString();
            ddlChecker.SelectedValue = ka.Checker;
            ddlNamaBarang.SelectedValue = ka.ItemCode;
            ddlTujuanKirim.SelectedValue = ka.PlantID.ToString();
            txtJmlTimbangan.Text = Convert.ToInt32(ka.GrossPlant).ToString();
            txtTanggal.Text = ka.TglCheck.ToString("dd-MM-yyyy");
            txtSupplier.Text = ka.SupplierName;
            txtjmlBal.Text = Convert.ToInt32( ka.JmlBAL).ToString();
            ddlTujuanKirim.Enabled = false;
            txtSupplier.Enabled = false;

        }
        private void LoadTujuanKirim()
        {
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from Company where RowStatus=0";
            SqlDataReader sdr = zl.Retrieve();
            ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlTujuanKirim.Items.Add(new ListItem(sdr["Spv"].ToString(), sdr["DepoID"].ToString()));
                }
            }
            ddlTujuanKirim.SelectedValue = "7";
        }
        protected void ddlTujuanKirim_Change(object sender, EventArgs e)
        {
            act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
            txtDocNo.Text = GetDocumentNumber();
            txtSupplier.Text = "";
            txtSupplierID.Value = "0";
            ClearListKA();
        }

        protected void ClearListKA()
        {
            ArrayList arrTusukan = new ArrayList();
            Session["Tusukan"] = null;
            lstKA.DataSource = arrTusukan;
            lstKA.DataBind();
        }
        private void LoadDeptKertas()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DepoKertas where RowStatus=0";
            SqlDataReader sdr = zl.Retrieve();
            ddlDepo.Items.Clear();
            ddlDepo.Items.Add(new ListItem("--Pilih Depo--", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDepo.Items.Add(new ListItem(sdr["DepoName"].ToString(), sdr["Alamat"].ToString().TrimEnd()));
                }
            }
            ddlDepo.SelectedValue = user.UnitKerjaID.ToString();
        }

        private void LoadChecker()
        {
            string[] Checker = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Checker", "DepoKertas").Split(',');
            string depo = ddlDepo.SelectedItem.Text.Replace("Depo", "").TrimStart();
            ddlChecker.Items.Clear();
            for (int n = 0; n < Checker.Count(); n++)
            {
                string[] Check = Checker[n].Split(':');
                int pos = Array.IndexOf(Check, depo);
                if (pos != -1)
                {
                    string[] nChek = Check[1].Split('-');
                    for (int i = 0; i < nChek.Count(); i++)
                    {
                        ddlChecker.Items.Add(new ListItem(nChek[i].ToString(), nChek[i].ToString()));
                    }
                }
            }
        }
        protected void ddlDepo_Change(object sender, EventArgs e)
        {
            LoadChecker();
        }
        private void periode30p()
        {
            LPeriode1.Text = "01-" + DateTime.Parse(txtTanggal.Text).ToString("MMM-yyyy");
            LPeriode2.Text = DateTime.Parse(txtTanggal.Text).ToString("dd-MMM-yyyy");
            LPeriode1S.Text = "01-" + DateTime.Parse(txtTanggal.Text).ToString("MMM-yyyy");
            LPeriode2S.Text = DateTime.Parse(txtTanggal.Text).ToString("dd-MMM-yyyy");
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
                //string ItemCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialKertasdepo", "BeritaAcara");
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
            //if (txtNOPOL.Text != string.Empty)
            //{
            //    txtNOPOL_Change(null, null);
            //}
            periode30p();
            LTotalKirim.Text = GetTotalKirim(ddlTujuanKirim.SelectedValue, txtSupplierID.Value.ToString(), DateTime.Parse(LPeriode1.Text).ToString("yyyyMMdd"),
                        DateTime.Parse(LPeriode2.Text).ToString("yyyyMMdd")).ToString();
            LTotalKirimS.Text = GetTotalKirimSemen(ddlTujuanKirim.SelectedValue, txtSupplierID.Value.ToString(), DateTime.Parse(LPeriode1.Text).ToString("yyyyMMdd"),
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
            txtJmlTimbangan.Focus();
            ClearListKA();
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
                "and SupplierID in (select supplierID from supppurchgroup where rowstatus>-1 and groupname='" + groupname + "') and PlantID in (1,7,13) and NoSJ='0' and stdKA>=30 " +
                "union all " +
                "select isnull(SUM(nettdepo),0)tkirim from DeliveryKertas D where D.RowStatus>-1 and left(convert(char,D.TglKirim,112),6)=@ThnBln " +
                "and SupplierID in (select supplierID from supppurchgroup where rowstatus>-1 and groupname='" + groupname + "') and PlantID in (1,7,13) and stdKA>=30 )S";
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["tkirim"]);
                }
            }
            //DateTime tglmulai = DateTime.Parse(txtTanggal.Text);
            //if (tglmulai.Day < 23)

            //    result = 0;

            return result;
        }
        protected void ddlNamaBarang_Change(object sender, EventArgs e)
        {
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
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
            Response.Redirect("DelivBeliKA0.aspx");
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
            //try
            //{
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
            //}
            //catch { }
        }
        protected void txtJmlTimbangan_Change(object sender, EventArgs e)
        {
            txtBK.Text = txtJmlTimbangan.Text;
            int suppID = 0;
            int.TryParse(txtSupplierID.Value, out suppID);
            if (suppID == 0) { DisplayAJAXMessage(this, "Nama Supplier tidak di kenal\\nSilahkan pilih lagi"); btnNew_Click(null, null); return; }
            ClearListKA();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (txtJmlTimbangan.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Jumlah timbangan tidak boleh kosong");
                btnNew_Click(null, null);
            }
            if (IsNumeric(txtJmlTimbangan.Text.Trim()) == false)
            {
                DisplayAJAXMessage(this, "Jumlah timbangan Harus numeric");
                btnNew_Click(null, null);
            }
            else
            {
                if (Int32.Parse(txtJmlTimbangan.Text.Trim()) == 0)
                {
                    DisplayAJAXMessage(this, "Jumlah timbangan Harus lebih besar dari 0");
                    btnNew_Click(null, null);
                }
            }
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
            //}
            //catch { }
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
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        protected void lstKA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            //try
            //{

            QAKadarAir qa = (QAKadarAir)e.Item.DataItem;
            ArrayList arrData = (ArrayList)Session["Tusukan"];
            TotalKA += qa.AvgTusuk;
            TotalData = arrData.Count;// TotalData + 1;
            txtAvgKadarAir.Text = Math.Round((TotalKA / TotalData), 2, MidpointRounding.AwayFromZero).ToString("N2");
            //beratpotongan
            txtJmlSample.Text = arrData.Count.ToString();
            string stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
            else
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");

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
            if (Convert.ToInt32(LTotalKirim.Text) >= Convert.ToInt32(LTotalKirim0.Text) &&
                 Convert.ToInt32(LTotalKirim2.Text) <= Convert.ToInt32(LTotalKirim1.Text) && LTotalKirim.Text.Trim() != "0")
            {
                //stdKA = "30";
                decimal persen = GetPersenKA(txtSupplierID.Value);
                stdKA = persen.ToString("N1");
            }
            stKA.Text = stdKA; decimal selisihKA = 0;
            decimal stdKAs = 0; decimal bk = 0; decimal smph = 0; decimal avgka = 0;
            decimal.TryParse(txtSampah.Text, out smph);
            decimal.TryParse(txtAvgKadarAir.Text, out avgka);
            decimal.TryParse(stdKA, out stdKAs);
            decimal.TryParse(txtBK.Text, out bk);
            selisihKA = (avgka - stdKAs) + smph;
            txtBeratPotongan.Text = Math.Round(((bk * (selisihKA) / 100)), 0, MidpointRounding.AwayFromZero).ToString("N2");
            txtBeratBersih.Text = Math.Round((bk - (Math.Round(((bk * (selisihKA) / 100)), 0, MidpointRounding.AwayFromZero))), 0, MidpointRounding.AwayFromZero).ToString("N2");
            txtBalke.Text = (arrData.Count + 1).ToString();
            //}
            //catch { }
        }
        protected void txtSampah_Change(object sender, EventArgs e)
        {
            try
            {
                decimal avgka = 0; decimal smph = 0; decimal selisihKA = 0;
                decimal.TryParse(txtSampah.Text.Replace(".", ","), out smph);
                decimal.TryParse(txtAvgKadarAir.Text.Replace(".", ","), out avgka);
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
                decimal stdKAs = 0; decimal bk = 0;
                decimal.TryParse(stdKA, out stdKAs);
                decimal.TryParse(txtBK.Text.Replace(".", ","), out bk);
                selisihKA = (avgka - stdKAs) + smph;
                txtBeratPotongan.Text = Math.Round((bk * (selisihKA) / 100), 0, MidpointRounding.AwayFromZero).ToString("N2");
                txtBeratBersih.Text = Math.Round((bk - (decimal.Parse(txtBeratPotongan.Text))), 0, MidpointRounding.AwayFromZero).ToString("N2");
            }
            catch { }

        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
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
            int keputusan = 0;
            keputusan = (chkKA.Checked == true) ? 1 : keputusan;
            keputusan = (chkKA1.Checked == true) ? 2 : keputusan;
            keputusan = (chkKA2.Checked == true) ? -1 : keputusan;
            qa.TglCheck = DateTime.Parse(txtTanggal.Text);
            qa.ItemCode = ddlNamaBarang.SelectedValue;
            qa.GrossPlant = bk;
            qa.ItemName = ddlNamaBarang.SelectedItem.Text;
            qa.JmlBAL = jmlbal;
            qa.JmlSample = jmlsample;
            qa.JmlSampleBasah = jmlsamplebasah;
            qa.BeratPotong = bpotong;
            qa.Potongan = bpotongan;
            qa.ProsSampleBasah = prossample;
            qa.AvgKA = avgka;
            qa.NettPlant = bb;
            qa.SupplierName = txtSupplier.Text.ToUpper();
            qa.SupplierID = int.Parse(txtSupplierID.Value);
            qa.RowStatus = keputusan;
            qa.CreatedBy = ((Users)Session["Users"]).UserID;
            qa.CreatedTime = DateTime.Now;
            if (ddlTujuanKirim.Enabled == true)
                qa.DocNo = GetDocumentNumber();
            else
                qa.DocNo = txtDocNo.Text;
            qa.PlantID = int.Parse(ddlTujuanKirim.SelectedValue);
            qa.Sampah = sampah;
            qa.BeratSampah = beratsampah;
            qa.BeratSample = beratsample;
            qa.StdKA = stdka;
            qa.Approval = 0;
            qa.DepoName = ddlDepo.SelectedItem.Text;
            qa.Checker = ddlChecker.SelectedItem.Text;
            qa.DepoID = int.Parse(ddlDepo.SelectedValue);
            arrKA.Add(qa);
            txtDocNo.Text = GetDocumentNumber();
            int result = dk.InsertBeliKA(qa);
            if (result > 0)
            {
                btnSimpan.Enabled = false;
                txtDocNo.Enabled = false;
                btnCetak.Attributes.Add("onclick", "CetakFrom('" + txtDocNo.Text + "')");
                btnCetak.Enabled = true;
                btnNew_Click(null, null);

            }
        }

        private string GetDocumentNumber()
        {
            DepoKertasKA dk = new DepoKertasKA();
            DepoFacade dp = new DepoFacade();
            Depo d = dp.RetrieveById(((Users)Session["Users"]).UnitKerjaID);
            int Nomor = dk.QAKadarAirDocNoBeli();
            string result = d.InitialToko.ToUpper() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString().PadLeft(2, '0') +
                "-" + (Nomor + 1).ToString().PadLeft(5, '0');
            return result;
        }
        protected void btnList_Clik(object sender, EventArgs e)
        {
            Response.Redirect("DelivBeliKaList0.aspx?v=1");
        }
        private void HookOnFocus(Control CurrentControl)
        {
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
                prossampah = (beratsampah / beratsample) * 100;
                txtprosSampah.Text = Math.Round(prossampah, 2, MidpointRounding.AwayFromZero).ToString();
                txtSampah.Text = Math.Round(prossampah, 2, MidpointRounding.AwayFromZero).ToString();
                txtSampah_Change(null, null);
            }
            catch { }
        }
        protected void btnCetak_Click(object sender, EventArgs e)
        {

        }
        private void GetGroup(string supplierID)
        {
            //string result = string.Empty ;
            Session["groupname"] = null;
            Session["min30"] = null;
            Session["max30"] = null;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(groupname,'-')groupname,isnull(Min30,0)Min30,isnull(max30,0)max30 " +
                "from supppurchgroup where rowstatus>-1 and supplierID=" + supplierID + " and plantid=" + ddlTujuanKirim.SelectedValue;
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
        private decimal GetPersenKA(string supplierID)
        {
            decimal result = 0;
            GetGroup(supplierID);
            int ikut = IsProgram30(supplierID);
            if (ikut == 0)
                return 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "select top 1 persen from suppPurchGroup where rowstatus>-1 and supplierid='" + supplierID + "'";
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToDecimal(sdr["persen"]);
                }
            }
            return result;
        }
        private int GetTotalKirim(string plantID, string supplierID, string periode1, string periode2)
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
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "exec gettotalkirimkertas  " + supplierID + "," + plantID + ",'" + periode2 + "'";

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
        private int GetTotalKirimSemen(string plantID, string supplierID, string periode1, string periode2)
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
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery =
                "exec gettotalkirimkertasSemen  " + supplierID + "," + plantID + ",'" + periode2 + "'";

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
        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {
            txtSupplier_Change(null, null);
        }

        protected void txtTanggal_TextChanged1(object sender, EventArgs e)
        {
            
        }

    }
}