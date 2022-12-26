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
using System.Collections.Generic;
using System.Reflection;
using System.Web.Script.Serialization;
using AjaxControlToolkit;

namespace GRCweb1.Modul.Purchasing
{
    public partial class DelivBeliKhusus : System.Web.UI.Page
    {
        string HargaMax = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["Tusukan"] = null;

                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDeptKertas();
                LoadSupplier();
                loadItemkertas();
                LoadTujuanKirim();
                LoadExpedisi();
                txtDocNo.Text = GetDocumentNumber();
                act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
                txtDocNo.Enabled = false;//.Text = GetDocumentNumber();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", " SetFocus();", true);
                act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
            }
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
        }
        private void LoadExpedisi()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DelivExpedisi where RowStatus=0 order by Expedisi";
            SqlDataReader sdr = zl.Retrieve();
            ddlExpedisi.Items.Clear();
            ddlExpedisi.Items.Add(new ListItem(" ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlExpedisi.Items.Add(new ListItem(sdr["Expedisi"].ToString(), sdr["ID"].ToString().TrimEnd()));
                }
            }
            //ddlDepo.SelectedValue = user.UnitKerjaID.ToString();
        }
        private void LoadDeptKertas()
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DepoKertasTK where RowStatus=0 order by deponame";
            SqlDataReader sdr = zl.Retrieve();
            ddlDepo.Items.Clear();
            ddlDepo.Items.Add(new ListItem(" ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDepo.Items.Add(new ListItem(sdr["DepoName"].ToString(), sdr["ID"].ToString().TrimEnd()));
                }
            }
            //ddlDepo.SelectedValue = user.UnitKerjaID.ToString();
        }

        protected void ddlDepo_Change(object sender, EventArgs e)
        {
            // LoadChecker();
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
                string ItemCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialKertas", "BeliKertasTeamKhusus");
                ArrayList arrInventory = new ArrayList();
                InventoryFacade inventoryFacade = new InventoryFacade();
                inventoryFacade.Criteriane = " and ItemCode in('" + ItemCode.Replace(",", "','") + "')";
                //arrInventory = (ItemCode == string.Empty) ? inventoryFacade.RetrieveByCriteria("A.ItemName", NamaBarang) :
                arrInventory = inventoryFacade.Retrieve();
                ddlNamaBarang.Items.Clear();
                ddlNamaBarang.Items.Add(new ListItem(" ", "0"));
                foreach (Inventory Inv in arrInventory)
                {
                    ddlNamaBarang.Items.Add(new ListItem(Inv.ItemName, Inv.ItemCode));
                }
                ddlNamaBarang.SelectedIndex = 1;
                HargaMax = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(ddlNamaBarang.SelectedValue, "BeliKertasTeamKhusus");
            }
            catch { }
        }

        protected void txtSupplier_Change(object sender, EventArgs e)
        {
            txtJmlTimbangan.Focus();
            ClearListKA();
        }

        protected void ddlNamaBarang_Change(object sender, EventArgs e)
        {
            //if (txtNOPOL.Text.Length > 4)
            //{
            //    ddlSJDepo_Change(null, null);
            //}
            HargaMax = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(ddlNamaBarang.SelectedValue, "BeliKertasTeamKhusus");
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(), "MyScript", myScript, true);
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            //btnSimpan_Click(null, null);
            btnSimpan.Disabled = false;
            txtDocNo.Enabled = true;
            Response.Redirect("DelivBeliKhusus.aspx");
        }
        protected void txtJmlTimbangan_Change(object sender, EventArgs e)
        {
            int suppID = 0;
            int.TryParse(txtSupplierID.Value, out suppID);
            if (suppID == 0) { DisplayAJAXMessage(this, "Nama Supplier tidak di kenal\\nSilahkan pilih lagi"); btnNew_ServerClick(null, null); }
            ClearListKA();
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        protected void btnSimpan_ServerClick(object sender, EventArgs e)
        {
            ArrayList arrKA = new ArrayList();
            ArrayList arrData = (ArrayList)Session["Tusukan"];
            QAKadarAir qa = new QAKadarAir();
            DepoKertasKA dk = new DepoKertasKA();
            DepoKertas dp = new DepoKertas();
            bpas_api.WebService1 bpas = new bpas_api.WebService1();
            int sudahAda = 0;
            string stdKA = string.Empty;
            stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            if (ddlNamaBarang.SelectedItem.Text.ToUpper() == "KARDUS IMPORT")
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKAkardusimport", "PO");
            else
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
            decimal bk = 0; decimal jmlsample = 0; decimal prossample = 0; decimal jmlsamplebasah = 0;
            decimal avgka = 0; decimal bpotong = 0; decimal bpotongan = 0; decimal bb = 0;
            decimal stdka = 0; decimal jmlbal = 0; decimal sampah = 0;
            decimal beratsample = 0; decimal beratsampah = 0;
            decimal.TryParse(txtJmlTimbangan.Text, out bk);
            decimal.TryParse(txtJmlTimbangan.Text, out bb);
            jmlsample = 0;
            jmlbal = 0;
            jmlsamplebasah = 0;
            prossample = 0;
            bpotong = 0;
            bpotongan = 0;

            decimal.TryParse(stdKA, out stdka);
            avgka = 0;
            sampah = 0;
            beratsample = 0;
            beratsampah = 0;
            int keputusan = 0;
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DepoKertasTK where RowStatus=0 and ID=" + ddlDepo.SelectedValue;
            SqlDataReader sdr = zl.Retrieve();
            int depoID = 0;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    depoID = Int32.Parse(sdr["Alamat"].ToString());
                }
            }
            qa.TglCheck = DateTime.Parse(txtTanggal.Text);
            qa.ItemCode = ddlNamaBarang.SelectedValue;
            //qa.NoSJ = ddlSJDepo.SelectedValue;
            //qa.NOPOL = txtNOPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper();
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
            qa.DocNo = GetDocumentNumber();
            qa.PlantID = int.Parse(ddlTujuanKirim.SelectedValue);
            qa.Sampah = sampah;
            qa.BeratSampah = beratsampah;
            qa.BeratSample = beratsample;
            qa.StdKA = stdka;
            qa.Approval = 0;
            qa.DepoName = ddlDepo.SelectedItem.Text;
            qa.Checker = "-";
            qa.DepoID = depoID;
            qa.Harga = decimal.Parse(txtHarga.Text);
            qa.Total = decimal.Parse(txtBayar.Text);
            qa.Expedisi = ddlExpedisi.SelectedValue;
            arrKA.Add(qa);
            txtDocNo.Text = GetDocumentNumber();
            int result = dk.InsertBeliTK(qa);

            //simpan kirim
            ArrayList arrData1 = new ArrayList();
            //arrData = (Session["data"] != null) ? (ArrayList)Session["data"] : new ArrayList();
            decimal gd = 0; decimal nd = 0; decimal ka = 0; decimal jb = 0;
            DeliveryKertas dk1 = new DeliveryKertas();
            decimal.TryParse(txtJmlTimbangan.Text, out gd);
            decimal.TryParse(txtJmlTimbangan.Text, out nd);
            dk1.DepoID = depoID;
            dk1.DepoName = ddlDepo.SelectedItem.Text;
            dk1.PlantID = int.Parse(ddlTujuanKirim.SelectedValue.ToString());
            dk1.NoSJ = txtNoSJ.Text.ToUpper();
            dk1.TglKirim = DateTime.Parse(txtTanggal.Text);
            dk1.TglETA = DateTime.Parse(txtTanggal.Text);
            //decimal.TryParse(txtGross.Text, out gd);
            //decimal.TryParse(txtNetto.Text, out nd);
            //decimal.TryParse(txtKA.Text, out ka);
            //decimal.TryParse(txtJmlBAL.Text, out jb);
            //decimal.TryParse(ddlstdKA.SelectedValue, out stdka);
            // if (gd <= 0 || ka <= 0 || jb <= 0) { return; }
            //dk.Checker = ddlChecker.SelectedValue;
            dk1.Expedisi = ddlExpedisi.SelectedItem.Text;
            dk1.NOPOL = txtNOPOL.Text.ToUpper().Replace(" ", "").Replace("_", "");
            dk1.GrossDepo = gd;
            dk1.ItemCode = (ddlNamaBarang.SelectedValue);
            dk1.NettDepo = nd;
            dk1.KADepo = ka;
            dk1.JmlBAL = jb;
            dk1.CreatedBy = ((Users)Session["Users"]).UserName;
            dk1.RowStatus = 0;
            dk1.SupplierID = int.Parse(txtSupplierID.Value);
            dk1.SupplierName = txtSupplier.Text;
            dk1.DocNo = txtDocNo.Text.ToUpper();
            dk1.CreatedTime = DateTime.Now;
            dk1.StdKA = stdka;
            dk1.Sampah = 0;
            //dk.POKAID = 0;
            dk1.ID = 0;
            dk1.KirimVia = (ddlTujuanKirim.SelectedItem.Text.Split('-'))[1].ToString().Substring(1, 1);
            dk1.LMuatID = 0;
            arrData1.Add(dk1);

            ArrayList arrData2 = arrData1;
            foreach (DeliveryKertas dk2 in arrData2)
            {
                DeliveryKertas dks = new DeliveryKertas();
                if (dk2.PlantID == 7)
                {
                    dks = dk2;
                    dks.Checker = "-";
                    dks.JMobil = "-";
                    result += dp.InsertKirim(dks);
                }
                else
                {
                    ArrayList arrD = new ArrayList();
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    dks.DepoID = dk2.DepoID;//1
                    dks.DepoName = dk2.DepoName;//2
                    dks.PlantID = dk2.PlantID;//3
                    dks.Checker = dk2.Checker;//4
                    dks.NoSJ = dk2.NoSJ;//5
                    dks.TglETA = dk2.TglETA;//6
                    dks.TglKirim = dk2.TglKirim;//7
                    dks.Expedisi = dk2.Expedisi;//8
                    dks.NOPOL = dk2.NOPOL;//9
                    dks.GrossDepo = dk2.GrossDepo;//11
                    dks.NettDepo = dk2.NettDepo;//12
                    dks.KADepo = dk2.KADepo;//13
                    dks.JmlBAL = dk2.JmlBAL;//14
                    dks.RowStatus = dk2.RowStatus;//15
                    dks.CreatedBy = dk2.CreatedBy;//16
                    dks.CreatedTime = DateTime.Now;//17
                    dks.DocNo = dk2.DocNo;//18
                    dks.ItemCode = dk2.ItemCode;//19
                    dks.SupplierID = dk2.SupplierID;//20
                    dks.StdKA = dk2.StdKA;//10
                    dks.NoPol = dk2.NOPOL;
                    dks.Sampah = dk2.Sampah;
                    dks.IDBeli = 0;
                    dks.LMuatID = 0;
                    string dkss = js.Serialize(dks).ToString();
                    int rst = bpas.InsertDeliveryKertasDepo(dkss, "GRCBoardCtrp");
                    if (rst > -1)
                    {
                        dks = dk2;
                        result += dp.Insert(dks);
                    }
                }
            }
            if (result > 0)
            {

                btnSimpan.Disabled = true;
                txtDocNo.Enabled = false;
                btnNew_ServerClick(null, null);

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
            Response.Redirect("DelivBeliKaList.aspx?v=1");
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
        protected void txtTanggal_TextChanged(object sender, EventArgs e)
        {
        }
        protected void txtSupplier_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlDepo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void txtHarga_TextChanged(object sender, EventArgs e)
        {
            HargaMax = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read(ddlNamaBarang.SelectedValue, "BeliKertasTeamKhusus");
            if (Int32.Parse(txtHarga.Text) > Int32.Parse(HargaMax))
            {
                DisplayAJAXMessage(this, "Harga tidak boleh lebih dari " + HargaMax);
                return;
            }

            else
            {
                txtBayar.Text = (Int32.Parse(txtHarga.Text) * Int32.Parse(txtJmlTimbangan.Text)).ToString();
            }
        }
    }
}