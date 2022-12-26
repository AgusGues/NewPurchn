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
using DataAccessLayer;
using Domain;
using System.Collections.Generic;
using System.Reflection;
using AjaxControlToolkit;

namespace GRCweb1.Modul.Purchasing
{
    public partial class DelivBayarExpedisi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                string[] depo = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Depo", "DepoKertas").Split(',');
                int pos = Array.IndexOf(depo, user.UnitKerjaID.ToString());
                Session["data"] = null;
                LoadTujuanKirim();
                LoadDeptKertas();
                txtTglBayar.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtTglJTempo.Text = DateTime.Now.AddDays(2).ToString("dd-MMM-yyyy");
                LoadPengiriman();
                btnSimpan.Enabled = (pos != -1) ? true : false;
                NomorDocument();
                btnSimpan.Enabled = true;
                txtJumlahBayar.Text = "0";
                Session["totalbayar"] = null;

            }

        }

        private int viewonly = 0;
        private decimal totalbayar = 0;
        private void LoadStdKA()
        {
            //string[] stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO").Split(',');
            //ddlstdKA.Items.Clear();
            //for (int i = 0; i < stdKA.Count(); i++)
            //{
            //    ddlstdKA.Items.Add(new ListItem(stdKA[i].ToString(), stdKA[i].ToString()));
            //}
            //string stdDefault = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            //ddlstdKA.SelectedValue = stdDefault.ToString();
        }
        private void LoadJenisBarang()
        {
            //string ItemCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialKertas", "BeritaAcara");
            //ArrayList arrInventory = new ArrayList();
            //InventoryFacade inventoryFacade = new InventoryFacade();
            //inventoryFacade.Criteriane = " and ItemCode in('" + ItemCode.Replace(",", "','") + "')";
            ////arrInventory = (ItemCode == string.Empty) ? inventoryFacade.RetrieveByCriteria("A.ItemName", NamaBarang) :
            //arrInventory = inventoryFacade.Retrieve();
            //ddlNamaBarang.Items.Clear();
            //ddlNamaBarang.Items.Add(new ListItem("-- Pilih Barang --", "0"));
            //foreach (Inventory Inv in arrInventory)
            //{
            //    ddlNamaBarang.Items.Add(new ListItem(Inv.ItemName, Inv.ItemCode));
            //}
            //ddlNamaBarang.SelectedIndex = 1;
        }
        protected void txtSupplierName_Change(object sender, EventArgs e)
        {
            //act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
            //btnAddItem.Enabled = true;
        }
        protected void ddlDepo_Change(object sender, EventArgs e)
        {

        }

        protected void ddlTujuanKirim_Change(object sender, EventArgs e)
        {
            txtBayarKpd.Text = string.Empty;
            LoadPengiriman();
        }
        private void NomorDocument()
        {
            DepoKertasKA dp = new DepoKertasKA();
            int Nomor = 0;
            //Nomor += DateTime.Parse(txtBayar.Text).Year.ToString().Substring(2, 2);
            //Nomor += DateTime.Parse(txtBayar.Text).Month.ToString().PadLeft(2, '0');
            //Nomor += "-";
            Nomor = dp.GetNoDocBayar(DateTime.Parse(txtTglBayar.Text).ToString("yyyy")) + 1;
            //txtDocNo.Text = "BKK/" + Nomor.ToString().PadLeft(5, '0') + "/KAT/" + DateTime.Now.Year.ToString();
            txtDocNo.Text = "BBK/" + Nomor.ToString().PadLeft(5, '0') + "/KAT/" + DateTime.Now.Year.ToString();
        }

        private void LoadChecker()
        {
            //string[] Checker = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Checker", "DepoKertas").Split(',');
            //string depo = ddlDepo.SelectedItem.Text.Replace("Depo", "").TrimStart();
            //ddlChecker.Items.Clear();
            //for (int n = 0; n < Checker.Count(); n++)
            //{
            //    string[] Check = Checker[n].Split(':');
            //    int pos = Array.IndexOf(Check, depo);
            //    if (pos != -1)
            //    {
            //        string[] nChek = Check[1].Split('-');
            //        for (int i = 0; i < nChek.Count(); i++)
            //        {
            //            ddlChecker.Items.Add(new ListItem(nChek[i].ToString(), nChek[i].ToString()));
            //        }
            //    }
            //}
        }
        protected void txtTglKirim_Change(object sender, EventArgs e)
        {
            DepoKertas dp = new DepoKertas();
            string etal = DateTime.Parse(txtTglBayar.Text).AddDays(3).ToString("dd-MMM-yyyy");
            NomorDocument();

        }
        protected void txtNoSJ_Change(object sender, EventArgs e)
        {

        }
        protected void txtExpedisi_Change(object sender, EventArgs e)
        {
            if (txtBayarKpd.Text != string.Empty)
                LoadPengirimanByExpedisi();
            else
                LoadPengiriman();

            // txtAnBGNo.Text = GetUPSupplier(int.Parse(ddlTujuanKirim.SelectedValue), int.Parse(txtSupplierID.Value));
            txtAnBGNo.Focus();
        }
        protected void txtNOPOL_Change(object sender, EventArgs e)
        {
            //Session["next"] = (TextBox)Page.FindControl("txtGross");
        }
        protected void txtGross_Change(object sender, EventArgs e)
        {


        }

        private bool isSuratJalan()
        {
            bool result = false;
            //DeliveryKertas dp = new DeliveryKertas();
            //DepoKertas dk = new DepoKertas();
            //string Criteria = " and NoSj='" + txtNoSJ.Text + "' AND RowStatus>-1";
            //Criteria += " And ItemCode='" + ddlNamaBarang.SelectedValue.ToString() + "' ";
            //Criteria += " And GrossDepo=" + decimal.Parse(txtGross.Text.ToString().Replace(",", "")).ToString();
            //dp = dk.Retrieve(Criteria, true);
            //if (dp.ID > 0)
            //{
            //    result = true;
            //}
            return result;
        }

        private void ClearField()
        {
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["data"] = null;
            Response.Redirect("DelivBayarExpedisi.aspx");
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private int CheckNomorSJ(string nosj, string itemcode)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from deliveryKertas where RowStatus>-1 and nosj='" + nosj + "' and itemcode='" + itemcode + "'";
            SqlDataReader sdr = zl.Retrieve();
            //ddlTujuanKirim.Items.Clear();
            if (sdr.HasRows)
            {
                result = 1;
            }
            return result;
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            //try
            //{
            DepoKertas dp = new DepoKertas();
            int dataID = 0;
            int.TryParse(txtID.Value, out dataID);
            int result = 0;
            NomorDocument();
            if (dataID > 0)
            {
                if (Session["data"] == null) { return; }
                ArrayList arrData = (ArrayList)Session["data"];

                foreach (BayarKertas dk in arrData)
                {
                    BayarKertas dks = new BayarKertas();
                    dk.TypeByr = 2;
                    dks = dk;
                    result += dp.InsertBayar(dks, true);
                }
                btnCari_Click(null, null);
            }
            else
            {
                if (Session["data"] == null) { return; }
                ArrayList arrData = (ArrayList)Session["data"];
                foreach (BayarKertas dk in arrData)
                {
                    BayarKertas dks = new BayarKertas();
                    dk.TypeByr = 2;
                    dks = dk;
                    result += dp.InsertBayar(dks);
                }
                txtCari.Text = txtDocNo.Text;
                btnCari_Click(null, null);
                btnSimpan.Enabled = false;
            }
            if (result > 0)
            {
                btnSimpan.Enabled = false;
            }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert(" + ex.Message + ")", true);
            //}
            // btnNew_Click(null, null);
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

        private string GetUPSupplier(int plantID, int supplierID)
        {
            string result = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (plantID == 7)
                zl.CustomQuery = "Select UP from suppPurch where RowStatus>-1 and ID=" + supplierID;
            else
                zl.CustomQuery = "Select UP from [sqlctrp.grcboard.com].bpasctrp.dbo.suppPurch where RowStatus>-1 and ID=" + supplierID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["UP"].ToString();
                }
            }
            return result;
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
            ddlDepo_Change(null, null);
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            viewonly = 1;
            btnSimpan.Enabled = false;
            if (txtCari.Text == string.Empty) return;
            Users user = (Users)Session["Users"];
            DepoKertasKA dk = new DepoKertasKA();
            string Criteria = " And DocNo like '%" + txtCari.Text + "%'";
            BayarKertas hbayar = new BayarKertas();
            hbayar = dk.RetrieveHeaderBayarExp(txtCari.Text);
            if (hbayar.TypeByr == 2)
            {
                ddlDepo.SelectedValue = hbayar.DepoID.ToString();
                txtJumlahBayar.Text = hbayar.TotalHarga.ToString("N0");
                txtBGNo.Text = hbayar.BGNo;
                txtAnBGNo.Text = hbayar.AnBGNo;
                txtTglBayar.Text = hbayar.TglBayar.ToString("ddMMMyyyy");
                txtTglJTempo.Text = hbayar.JTempo.ToString("ddMMMyyyy");
                txtDocNo.Text = hbayar.DocNo;
                txtBayarKpd.Text = hbayar.Penerima;
                ArrayList arrData = dk.RetrieveBayarExp(Criteria);
                lstDepo.DataSource = arrData;
                lstDepo.DataBind();
            }
            else
                DisplayAJAXMessage(this, "Data tidak ditemukan");
            LoadPengiriman();
        }
        protected void lstDepo_DataBound(object sender, RepeaterItemEventArgs e)
        {
            BayarKertas dk = (BayarKertas)e.Item.DataItem;
            ((Image)e.Item.FindControl("edt")).Visible = false;
            ((Image)e.Item.FindControl("edt")).Visible = false;
            ((Image)e.Item.FindControl("hps")).Visible = false;
            ((Image)e.Item.FindControl("del")).Visible = viewonly == 0 ? true : false;
            ((Image)e.Item.FindControl("sts")).Visible = false;
        }

        protected void lstDepo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
        protected void lstDepo_Command(object sender, RepeaterCommandEventArgs e)
        {
            ArrayList arrData = new ArrayList();
            DepoKertas dks = new DepoKertas();
            DeliveryKertas dv = new DeliveryKertas();
            DeliveryKertas dk = new DeliveryKertas();
            string Idx = e.Item.ItemIndex.ToString();
            string Idxbeli = e.CommandArgument.ToString();
            HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("xx");
            switch (e.CommandName)
            {
                case "hapus":
                    DeliveryKertas dkbeli = new DeliveryKertas();
                    DepoKertasKA dka = new DepoKertasKA();
                    ArrayList arrDataKA = new ArrayList();
                    Label txttotalharga = (Label)e.Item.FindControl("txttotalharga");
                    if (txttotalharga != null)
                        Session["totalbayar"] = (Session["totalbayar"] != null) ? decimal.Parse(Session["totalbayar"].ToString()) - decimal.Parse(txttotalharga.Text) : 0;
                    txtJumlahBayar.Text = decimal.Parse(Session["totalbayar"].ToString()).ToString("N0");
                    dkbeli = dka.RetrieveKirimExpedisi1(" and IDBeli=" + Idxbeli);
                    arrDataKA = (ArrayList)Session["arrDataKA"];
                    arrDataKA.Add(dkbeli);
                    lstKA.DataSource = arrDataKA;
                    lstKA.DataBind();
                    arrData = (ArrayList)Session["data"];
                    string harga = string.Empty;

                    arrData.RemoveAt(int.Parse(Idx));
                    lstDepo.DataSource = arrData;
                    lstDepo.DataBind();

                    //tr.Visible = false;
                    break;
            }
        }
        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("DelivBayarExpedisiList.aspx?v=1");
        }
        protected void lstKA_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "prn":

                    break;
                case "po":
                    //if (txtNoSJ.Text.Trim() == string.Empty)
                    //{
                    //    DisplayAJAXMessage(this, "Nomor surat jalan tidak boleh kosong");
                    //    break;
                    //}
                    if (txtBayarKpd.Text.Trim() == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Bayar Kepada tidak boleh kosong");
                        break;
                    }
                    if (txtBGNo.Text.Trim() == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Nomor BG tidak boleh kosong");
                        break;
                    }
                    ArrayList arrData = new ArrayList();
                    ArrayList arrDataKA = new ArrayList();
                    int Idx = e.Item.ItemIndex;
                    int idbeli = int.Parse(e.CommandArgument.ToString());
                    arrData = (Session["data"] != null) ? (ArrayList)Session["data"] : new ArrayList();
                    arrDataKA = (Session["arrdataKA"] != null) ? (ArrayList)Session["arrdataKA"] : new ArrayList();
                    BayarKertas dk = new BayarKertas();
                    DeliveryKertas dkbeli = new DeliveryKertas();
                    DepoKertasKA dka = new DepoKertasKA();
                    int plantID = dka.getplantidbeli(idbeli.ToString());
                    if (plantID == 7)
                        dkbeli = dka.RetrieveKirimExpedisi1(" and IDBeli=" + idbeli.ToString());
                    if (plantID == 1)
                        dkbeli = dka.RetrieveKirimExpedisi2(" and IDBeli=" + idbeli.ToString());
                    if (plantID == 13)
                        dkbeli = dka.RetrieveKirimExpedisi3(" and IDBeli=" + idbeli.ToString());
                    //int checknosj = CheckNomorSJ(txtNoSJ.Text, dkbeli.ItemCode);
                    //#region Validasi nomor SJ
                    //if (checknosj > 0)
                    //{
                    //    DisplayAJAXMessage(this, "Nomor SJ : " + txtNoSJ.Text.Trim() + " sudah diinput...");
                    //    return;
                    //}
                    //#endregion
                    string cekSJ = string.Empty;
                    String nopol = string.Empty;
                    decimal harga = 0;
                    foreach (BayarKertas BayarKertas in arrData)
                    {
                        cekSJ = dkbeli.NoSJ.Trim();
                        nopol = dkbeli.NOPOL.Trim();
                        if ( nopol==BayarKertas.NoPol.Trim())
                            harga = 0;
                        else
                            harga = GetHargaExpedisi(dkbeli.LMuatID);
                    }
                    if (arrData.Count == 0)
                        harga = GetHargaExpedisi(dkbeli.LMuatID);
                    dk.DepoID = dkbeli.DepoID;
                    dk.DepoName = ddlDepo.SelectedItem.Text;
                    dk.PlantID = dkbeli.PlantID;
                    dk.ItemCode = dkbeli.ItemCode;
                    dk.CreatedBy = ((Users)Session["Users"]).UserName;
                    dk.RowStatus = 0;
                    dk.DocNo = txtDocNo.Text;
                    dk.BGNo = txtBGNo.Text;
                    dk.AnBGNo = txtAnBGNo.Text;
                    dk.SupplierID = dkbeli.SupplierID;
                    dk.SupplierName = dkbeli.SupplierName;
                    dk.CreatedBy = ((Users)Session["Users"]).UserID;
                    dk.CreatedTime = DateTime.Now;
                    dk.ID = int.Parse(txtID.Value);
                    dk.IDBeli = dkbeli.IDBeli;
                    dk.TglBayar = DateTime.Parse(txtTglBayar.Text);
                    dk.JTempo = DateTime.Parse(txtTglJTempo.Text);
                    dk.ItemCode = dkbeli.ItemCode;
                    dk.ItemName = dkbeli.ItemName;
                    dk.Penerima = txtBayarKpd.Text;
                    dk.NoSJ = dkbeli.NoSJ;
                    dk.NoPol = dkbeli.NOPOL;
                    //if (decimal.Parse(txtJumlahBayar.Text) > 0)
                    //    dk.Harga = 0;
                    //else
                    dk.Harga = harga;
                    dk.TotalHarga = dk.Harga;
                    dk.LMuatID = dkbeli.LMuatID;
                    dk.TypeByr = 2;
                    Session["totalbayar"] = (Session["totalbayar"] != null) ? decimal.Parse(Session["totalbayar"].ToString()) + dk.TotalHarga : dk.TotalHarga;
                    txtJumlahBayar.Text = decimal.Parse(Session["totalbayar"].ToString()).ToString("N0");
                    dk.Qty = dkbeli.NettPlant;
                    arrData.Add(dk);
                    Session["data"] = arrData;
                    if (int.Parse(txtID.Value) == 0)
                    {
                        lstDepo.DataSource = arrData;
                        lstDepo.DataBind();
                    }
                    arrDataKA = (ArrayList)Session["arrDataKA"];
                    arrDataKA.RemoveAt(Idx);
                    lstKA.DataSource = arrDataKA;
                    lstKA.DataBind();
                    break;

            }
        }
        private decimal GetHargaExpedisi(int ID)
        {
            decimal result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select * from DelivLokasiMuat where ID=" + ID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = decimal.Parse(sdr["harga"].ToString());
                }
            }
            return result;
        }
        protected void lstKA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];
            string[] UserApp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "DepoKertas").Split(',');
            int posx = Array.IndexOf(UserApp, user.UserName.ToString());

            DeliveryKertas qa = (DeliveryKertas)e.Item.DataItem;
            Label stat = (Label)e.Item.FindControl("sts");

            Image ct = (Image)((Image)e.Item.FindControl("btnPrint"));//print dialog
            ct.Attributes.Add("onclick", "CetakFrom('" + qa.DocNo + "')");
            Image v = (Image)e.Item.FindControl("btnPrev");//print preview
            Image app = (Image)e.Item.FindControl("btnApp");
            Image pop = (Image)e.Item.FindControl("btnPO");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            Label lbl = (Label)e.Item.FindControl("lblNo");
            //if (Request.QueryString["v"].ToString() == "5")
            //{
            //if (posx > -1)
            //{
            ct.Visible = false;
            v.Visible = viewonly == 0 ? true : false;
            app.Visible = false;
            pop.Visible = viewonly == 0 ? true : false;
            chk.Visible = false;
            lbl.Visible = true;
            v.Attributes.Add("onclick", "CetakFrom('" + qa.DocNo + "')");
        }
        protected void LoadPengiriman()
        {
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            Session["arrDataKA"] = null;
            //string Criteria = (ddlBulan.SelectedIndex > 0) ? " And Month(TglCheck)=" + ddlBulan.SelectedValue : "";
            //Criteria += " And Year(TglCheck)=" + ddlTahun.SelectedValue;
            string Criteria = string.Empty;
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            Criteria = " and plantid=" + ddlTujuanKirim.SelectedValue + " ";
            if (txtBayarKpd.Text.Trim() != string.Empty)
                Criteria += " and expedisi='" + txtBayarKpd.Text + "' ";
            arrData = ka.RetrieveKirimExpedisi(Criteria);
            Session["arrDataKA"] = arrData;
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }
        protected void LoadPengirimanByExpedisi()
        {
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            Session["arrDataKA"] = null;
            //string Criteria = (ddlBulan.SelectedIndex > 0) ? " And Month(TglCheck)=" + ddlBulan.SelectedValue : "";
            //Criteria += " And Year(TglCheck)=" + ddlTahun.SelectedValue;
            string Criteria = string.Empty;
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            Criteria += "and expedisi='" + txtBayarKpd.Text + "' ";
            Criteria += " and plantid=" + ddlTujuanKirim.SelectedValue + " ";
            Criteria += " Order By NoSJ ";

            arrData = ka.RetrieveKirimExpedisi(Criteria);
            Session["arrDataKA"] = arrData;
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }

        protected void txtBayarKpd_TextChanged(object sender, EventArgs e)
        {

        }
        protected void btnCetak_Click(object sender, EventArgs e)
        {
            string strSQL = "select * from delivbayarkertas where docno='" + txtDocNo.Text + "'";
            Session["docno"] = txtDocNo.Text;
            Session["total"] = GetTotalBayar(txtDocNo.Text);
            Cetak(this);
        }
        protected void btnCetak_Click2(object sender, EventArgs e)
        {
            string strSQL = "select * from delivbayarkertas where docno='" + txtDocNo.Text + "'";
            Session["docno"] = txtDocNo.Text;
            Session["total"] = GetTotalBayar(txtDocNo.Text);
            Cetak2(this);
        }
        private int GetTotalBayar(string docno)
        {
            int result = 0;
            decimal total = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select sum(totalharga)total from delivbayarkertas where RowStatus>-1 and docno='" + docno + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    total = decimal.Parse(sdr["Total"].ToString());
                }
            }
            result = Convert.ToInt32(total);
            return result;
        }

        static public void Cetak(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=bayarExp', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        static public void Cetak2(Control page)
        {
            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=bayarExp', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak2();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

    }
}