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
using System.Web.Script.Serialization;
using AjaxControlToolkit;

namespace GRCweb1.Modul.Purchasing
{
    public partial class DelivKirimKertas0 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users user = (Users)Session["Users"];
                string[] depo = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Depo", "DepoKertas").Split(',');
                int pos = Array.IndexOf(depo, user.UnitKerjaID.ToString());
                LoadTujuanKirim();
                LoadExpedisi();
                LoadJenisBarang();
                txtExpedisi.Text = ddlExpedisi.SelectedItem.Text;
                LoadDeptKertas();

                txtTglKirim.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtETA.Text = DateTime.Now.AddDays(2).ToString("dd-MMM-yyyy");

                //LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
                btnSimpan.Enabled = (pos != -1) ? true : false;
                NomorDocument();
                LoadLokasiMuat(ddlTypeMobil.SelectedValue, ddlTujuanKirim.SelectedValue);

            }
            btnHapus.Attributes.Add("onclick", "return confirm_Delete();");
        }
        private void LoadExpedisi()
        {
            string[] Checker = new Inifiles(HttpContext.Current.Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Expedisi", "DepoKertas").Split(',');
            //string depo = ddlDepo.SelectedItem.Text.Replace("Depo", "").TrimStart();
            ddlExpedisi.Items.Clear();
            for (int n = 0; n < Checker.Count(); n++)
            {
                //string[] Check = Checker[n].Split(':');
                //int pos = Array.IndexOf(Check, ":");
                //if (pos != -1)
                //{
                //string[] nChek = Check[1].Split('-');
                //for (int i = 0; i < nChek.Count(); i++)
                //{
                ddlExpedisi.Items.Add(new ListItem(Checker[n].ToString(), Checker[n].ToString()));
                //}
                //}
            }
        }
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
        private void LoadLokasiMuat(string typemobil, string lokasitujuan)
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from delivlokasimuat where RowStatus>-1 and typemobil='" + typemobil + "' and lokasitujuan= " + lokasitujuan + " order by lokasimuat";
            SqlDataReader sdr = zl.Retrieve();
            ddlLokasiMuat.Items.Clear();
            ddlLokasiMuat.Items.Add(new ListItem(" ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlLokasiMuat.Items.Add(new ListItem(sdr["lokasimuat"].ToString(), sdr["ID"].ToString().TrimEnd()));
                }
            }
            ddlLokasiMuat.SelectedValue = "0";
        }
        protected void txtSupplierName_Change(object sender, EventArgs e)
        {
            //act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
            //btnAddItem.Enabled = true;
        }
        protected void ddlDepo_Change(object sender, EventArgs e)
        {
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
        }

        protected void ddlTujuanKirim_Change(object sender, EventArgs e)
        {
            //AutoCompleteExtender autotxt = (AutoCompleteExtender)this.FindControl("act1");
            NomorDocument();
            LoadLokasiMuat(ddlTypeMobil.SelectedValue, ddlTujuanKirim.SelectedValue);
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
            ArrayList arrData = new ArrayList();
            Session["data"] = null;
            arrData = (ArrayList)Session["data"];
            lstDepo.DataSource = arrData;
            lstDepo.DataBind();
        }

        private void NomorDocument()
        {
            DepoKertas dp = new DepoKertas();
            string Nomor = (ddlTujuanKirim.SelectedItem.Text.Trim().Split('-'))[1].ToString().Substring(1, 1);
            Nomor += DateTime.Parse(txtTglKirim.Text).Year.ToString().Substring(2, 2);
            Nomor += DateTime.Parse(txtTglKirim.Text).Month.ToString().PadLeft(2, '0');
            Nomor += "-";
            Nomor += (int.Parse(dp.DocumentNo().ToString()) + 1).ToString().PadLeft(4, '0');
            txtDocNo.Text = Nomor;
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
            string etal = DateTime.Parse(txtTglKirim.Text).AddDays(3).ToString("dd-MMM-yyyy");
            txtETA.Text = etal;
            NomorDocument();

        }
        protected void txtNoSJ_Change(object sender, EventArgs e)
        {

        }
        protected void txtExpedisi_Change(object sender, EventArgs e)
        {
            //Session["next"] = (TextBox)Page.FindControl("txtNOPOL");
        }
        protected void txtNOPOL_Change(object sender, EventArgs e)
        {
            //Session["next"] = (TextBox)Page.FindControl("txtGross");
        }
        protected void txtGross_Change(object sender, EventArgs e)
        {


        }
        protected void txtKA_Change(object sender, EventArgs e)
        {

            //decimal gd = 0; decimal nd = 0; decimal ka = 0; decimal jb = 0;
            //string stdKA = ddlstdKA.SelectedValue.ToString();// new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            //decimal.TryParse(txtGross.Text, out gd);
            //decimal.TryParse(txtKA.Text, out ka);
            //jb = (ka - decimal.Parse(stdKA));
            //nd = gd - Math.Round(((gd * jb / 100)), 0, MidpointRounding.AwayFromZero);
            //if (Session["Netto"] != null && Session["Sampah"] != null)
            //{
            //    txtNetto.Text = Session["Netto"].ToString();
            //    txtSampah.Value = Session["Sampah"].ToString();
            //}
            //else
            //{
            //    txtNetto.Text = nd.ToString("N0");
            //}
            //txtJmlBAL.Focus();
        }
        protected void btnAddItemClick(object sender, EventArgs e)
        {
            //if (txtSupplierID.Value == "0") { txtNoSJ.Focus(); return; }
            //check surat jalan double
            if (isSuratJalan() == true && txtID.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "Nomor Surat Jalan Sudah pernah di input", true);
                return;
            }
            else
            {
                ArrayList arrData = new ArrayList();
                arrData = (Session["data"] != null) ? (ArrayList)Session["data"] : new ArrayList();
                decimal gd = 0; decimal nd = 0; decimal ka = 0; decimal jb = 0; decimal stdka = 0;
                DeliveryKertas dk = new DeliveryKertas();
                dk.DepoID = int.Parse(ddlDepo.SelectedValue.ToString());
                dk.DepoName = ddlDepo.SelectedItem.Text;
                dk.PlantID = int.Parse(ddlTujuanKirim.SelectedValue.ToString());
                dk.NoSJ = txtNoSJ.Text.ToUpper();
                dk.TglKirim = DateTime.Parse(txtTglKirim.Text);
                dk.TglETA = DateTime.Parse(txtETA.Text);
                //decimal.TryParse(txtGross.Text, out gd);
                //decimal.TryParse(txtNetto.Text, out nd);
                //decimal.TryParse(txtKA.Text, out ka);
                //decimal.TryParse(txtJmlBAL.Text, out jb);
                //decimal.TryParse(ddlstdKA.SelectedValue, out stdka);
                // if (gd <= 0 || ka <= 0 || jb <= 0) { return; }
                //dk.Checker = ddlChecker.SelectedValue;
                dk.Expedisi = txtExpedisi.Text.ToUpper();
                dk.NOPOL = txtNOPOL.Text.ToUpper().Replace(" ", "").Replace("_", "");
                dk.GrossDepo = gd;
                //dk.ItemCode = (ddlNamaBarang.SelectedValue);
                dk.NettDepo = nd;
                dk.KADepo = ka;
                dk.JmlBAL = jb;
                dk.CreatedBy = ((Users)Session["Users"]).UserName;
                dk.RowStatus = 0;
                //dk.SupplierID = int.Parse(txtSupplierID.Value);
                //dk.SupplierName = txtSupplier.Text;
                dk.DocNo = txtDocNo.Text.ToUpper();
                dk.CreatedTime = DateTime.Now;
                dk.StdKA = stdka;
                dk.Sampah = decimal.Parse(txtSampah.Value);
                //dk.POKAID = 0;
                dk.ID = int.Parse(txtID.Value);
                dk.KirimVia = (ddlTujuanKirim.SelectedItem.Text.Split('-'))[1].ToString().Substring(1, 1);
                dk.LMuatID = Int32.Parse(ddlLokasiMuat.SelectedValue);
                arrData.Add(dk);
                Session["data"] = arrData;
                if (int.Parse(txtID.Value) == 0)
                {
                    lstDepo.DataSource = arrData;
                    lstDepo.DataBind();
                }
                ClearField();
            }
        }

        private bool isSuratJalan()
        {
            bool result = false;
            DeliveryKertas dp = new DeliveryKertas();
            DepoKertas dk = new DepoKertas();
            //string Criteria = " and NoSj='" + txtNoSJ.Text + "' AND RowStatus>-1";
            //Criteria += " And ItemCode='" + ddlNamaBarang.SelectedValue.ToString() + "' ";
            //Criteria += " And GrossDepo=" + decimal.Parse(txtGross.Text.ToString().Replace(",", "")).ToString();
            //dp = dk.Retrieve(Criteria, true);
            if (dp.ID > 0)
            {
                result = true;
            }
            return result;
        }

        private void ClearField()
        {
            //txtSupplier.Text = "";
            //txtSupplierID.Value = "0";
            //txtGross.Text = "";
            //txtNetto.Text = "";
            //txtKA.Text = "";
            //txtJmlBAL.Text = "";
            ddlTujuanKirim.Enabled = false;
            ddlDepo.Enabled = false;
            txtNOPOL.ReadOnly = true;
            txtNoSJ.ReadOnly = true;
            //btnAddItem.Enabled = false;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session["data"] = null;
            Response.Redirect("DelivKirimKertas0.aspx");
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
        private void GetGroup(string supplierID)
        {
            //string result = string.Empty ;
            Session["groupname"] = null;
            Session["min30"] = null;
            Session["max30"] = null;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(groupname,'-')groupname,isnull(Min30,0)Min30,isnull(max30,0)max30 from supppurchgroup where rowstatus>-1 and supplierID=" +
                supplierID;
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

        protected void btnHapus_ServerClick(object sender, EventArgs e)
        {
            //nyusul

            //// Add By Anang Perubahan Status Surat Jalan TO 26 Juni 2018
            //string test = Session["AlasanCancel"].ToString();
            //if (Session["AlasanCancel"] == null || Session["AlasanCancel"].ToString().Length <= 2 || txtNoSJ.Text.Trim() == string.Empty  )
            //{
            //    DisplayAJAXMessage(this, "Alasan Hapus SJ kertas tidak boleh kosong ");
            //    return;
            //}
            //else
            //{
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "update deliverykertas set rowstatus=-1 where nosj='" + txtNoSJ.Text + "' " +
                    "update deliverykertaska set rowstatus=-1 where nosj='" + txtNoSJ.Text + "' " +
                    "update delivbelika set rowstatus=-1 where id in (select idbeli from deliverykertas where nosj='" + txtNoSJ.Text + "') " +
                    "update delivbayarkertas set rowstatus=-1 where idbeli in (select idbeli from deliverykertas where nosj='" + txtNoSJ.Text + "')";
                SqlDataReader sdr = zl.Retrieve();
            //}
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            //try
            //{
            DepoKertas dp = new DepoKertas();
            int dataID = 0;
            int.TryParse(txtID.Value, out dataID);
            int result = 0;

            bpas_api.WebService1 bpas = new bpas_api.WebService1();
            if (dataID > 0)
            {
                btnAddItemClick(null, null);
                if (Session["data"] == null) { return; }
                ArrayList arrData = (ArrayList)Session["data"];

                foreach (DeliveryKertas dk in arrData)
                {
                    DeliveryKertas dks = new DeliveryKertas();

                    if (dk.PlantID == 7)
                    {
                        dks = dk;
                        result += dp.InsertKirim(dks, true);
                    }
                    else
                    {
                        if (dk.PlantID == 1)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            dks.ID = dk.ID;
                            dks.PlantID = dk.PlantID;
                            dks.Checker = dk.Checker;
                            dks.NoSJ = dk.NoSJ;
                            dks.TglETA = dk.TglETA;
                            dks.TglKirim = dk.TglKirim;
                            dks.Expedisi = dk.Expedisi;
                            dks.NOPOL = dk.NOPOL;
                            dks.NoPol = dk.NoPol;
                            dks.GrossDepo = dk.GrossDepo;
                            dks.NettDepo = dk.NettDepo;
                            dks.KADepo = dk.KADepo;
                            dks.JmlBAL = dk.JmlBAL;
                            dks.RowStatus = dk.RowStatus;
                            dks.DocNo = dk.DocNo;
                            dks.ItemCode = dk.ItemCode;
                            dks.SupplierID = dk.SupplierID;
                            dks.LastModifiedBy = ((Users)Session["Users"]).UserName;
                            dks.LastModifiedTime = DateTime.Now;
                            dks.ReceiptID = dk.ReceiptID;
                            dks.StdKA = dk.StdKA;
                            dks.StdKA = dk.IDBeli;
                            dks.LMuatID = Int32.Parse(ddlLokasiMuat.SelectedValue);
                            //dks.Sampah = dk.Sampah;
                            string dkss = js.Serialize(dks);
                            result = bpas.UpdateDeliveryKertasDepo(dkss, "GRCBoardCtrp");
                            if (result > 0)
                            {
                                dks = dk;
                                result += dp.Insert(dks, true);
                            }
                        }
                        if (dk.PlantID == 13)
                        {
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            dks.ID = dk.ID;
                            dks.PlantID = dk.PlantID;
                            dks.Checker = dk.Checker;
                            dks.NoSJ = dk.NoSJ;
                            dks.TglETA = dk.TglETA;
                            dks.TglKirim = dk.TglKirim;
                            dks.Expedisi = dk.Expedisi;
                            dks.NOPOL = dk.NOPOL;
                            dks.NoPol = dk.NoPol;
                            dks.GrossDepo = dk.GrossDepo;
                            dks.NettDepo = dk.NettDepo;
                            dks.KADepo = dk.KADepo;
                            dks.JmlBAL = dk.JmlBAL;
                            dks.RowStatus = dk.RowStatus;
                            dks.DocNo = dk.DocNo;
                            dks.ItemCode = dk.ItemCode;
                            dks.SupplierID = dk.SupplierID;
                            dks.LastModifiedBy = ((Users)Session["Users"]).UserName;
                            dks.LastModifiedTime = DateTime.Now;
                            dks.ReceiptID = dk.ReceiptID;
                            dks.StdKA = dk.StdKA;
                            dks.StdKA = dk.IDBeli;
                            dks.LMuatID = Int32.Parse(ddlLokasiMuat.SelectedValue);
                            //dks.Sampah = dk.Sampah;
                            string dkss = js.Serialize(dks);
                            result = bpas.UpdateDeliveryKertasDepo(dkss, "GRCBoardJmb");
                            if (result > 0)
                            {
                                dks = dk;
                                result += dp.Insert(dks, true);
                            }
                        }
                    }
                }
                btnCari_Click(null, null);
            }
            else
            {
                if (Session["data"] == null) { return; }
                ArrayList arrData = (ArrayList)Session["data"];
                foreach (DeliveryKertas dk in arrData)
                {
                    DeliveryKertas dks = new DeliveryKertas();
                    if (dk.PlantID == 7)
                    {
                        dks = dk;
                        result += dp.InsertKirim0(dks);
                    }
                    else
                    {
                        if (dk.PlantID == 1)
                        {
                            ArrayList arrD = new ArrayList();
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            dks.DepoID = dk.DepoID;//1
                            dks.DepoName = dk.DepoName;//2
                            dks.PlantID = dk.PlantID;//3
                            dks.Checker = dk.Checker;//4
                            dks.NoSJ = dk.NoSJ;//5
                            dks.TglETA = dk.TglETA;//6
                            dks.TglKirim = dk.TglKirim;//7
                            dks.Expedisi = dk.Expedisi;//8
                            dks.NOPOL = dk.NOPOL;//9
                            dks.GrossDepo = dk.GrossDepo;//11
                            dks.NettDepo = dk.NettDepo;//12
                            dks.KADepo = dk.KADepo;//13
                            dks.JmlBAL = dk.JmlBAL;//14
                            dks.RowStatus = dk.RowStatus;//15
                            dks.CreatedBy = dk.CreatedBy;//16
                            dks.CreatedTime = DateTime.Now;//17
                            dks.DocNo = dk.DocNo;//18
                            dks.ItemCode = dk.ItemCode;//19
                            dks.SupplierID = dk.SupplierID;//20
                            dks.StdKA = dk.StdKA;//10
                            dks.NoPol = dk.NOPOL;
                            dks.Sampah = dk.Sampah;
                            dks.IDBeli = dk.IDBeli;
                            dks.LMuatID = Int32.Parse(ddlLokasiMuat.SelectedValue);
                            string dkss = js.Serialize(dks).ToString();
                            int rst = bpas.InsertDeliveryKertasDepo(dkss, "GRCBoardCtrp");
                            if (rst > -1)
                            {
                                dks = dk;
                                result += dp.Insert(dks);
                            }
                        }
                        if (dk.PlantID == 13)
                        {
                            ArrayList arrD = new ArrayList();
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            dks.DepoID = dk.DepoID;//1
                            dks.DepoName = dk.DepoName;//2
                            dks.PlantID = dk.PlantID;//3
                            dks.Checker = dk.Checker;//4
                            dks.NoSJ = dk.NoSJ;//5
                            dks.TglETA = dk.TglETA;//6
                            dks.TglKirim = dk.TglKirim;//7
                            dks.Expedisi = dk.Expedisi;//8
                            dks.NOPOL = dk.NOPOL;//9
                            dks.GrossDepo = dk.GrossDepo;//11
                            dks.NettDepo = dk.NettDepo;//12
                            dks.KADepo = dk.KADepo;//13
                            dks.JmlBAL = dk.JmlBAL;//14
                            dks.RowStatus = dk.RowStatus;//15
                            dks.CreatedBy = dk.CreatedBy;//16
                            dks.CreatedTime = DateTime.Now;//17
                            dks.DocNo = dk.DocNo;//18
                            dks.ItemCode = dk.ItemCode;//19
                            dks.SupplierID = dk.SupplierID;//20
                            dks.StdKA = dk.StdKA;//10
                            dks.NoPol = dk.NOPOL;
                            dks.Sampah = dk.Sampah;
                            dks.IDBeli = dk.IDBeli;
                            dks.LMuatID = Int32.Parse(ddlLokasiMuat.SelectedValue);
                            string dkss = js.Serialize(dks).ToString();
                            int rst = bpas.InsertDeliveryKertasDepo(dkss, "GRCBoardJmb");
                            if (rst > -1)
                            {
                                dks = dk;
                                result += dp.Insert(dks);
                            }
                        }
                    }
                }
            }
            if (result > 0)
            {
                btnSimpan.Enabled = false;
                //btnAddItem.Enabled = false;
            }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Alert", "alert(" + ex.Message + ")", true);
            //}
            btnNew_Click(null, null);
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
            ddlTujuanKirim.SelectedIndex = 0;
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
            if (txtCari.Text == string.Empty) return;
            //btnNew_Click(null, null);
            txtNoSJ.Text = string.Empty;
            Users user = (Users)Session["Users"];
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            DepoKertas dk = new DepoKertas();
            string Criteria = " And NoSJ like '%" + txtCari.Text + "%'";
            Criteria += (pos != -1) ? "" : " and CreatedBy='" + user.UserName + "'";
            ArrayList arrData = dk.Retrieve(Criteria);
            lstDepo.DataSource = arrData;
            lstDepo.DataBind();
            if (arrData.Count > 0)
            {
                txtNoSJ.Text = txtCari.Text;
            }
        }
        protected void lstDepo_DataBound(object sender, RepeaterItemEventArgs e)
        {
            DeliveryKertas dk = (DeliveryKertas)e.Item.DataItem;
            ((Image)e.Item.FindControl("edt")).Visible = true;
            ((Image)e.Item.FindControl("simpan")).Visible = false;
            if (txtNoSJ.Text.Trim() != string.Empty)
                ((Image)e.Item.FindControl("del")).Visible = true;
            else
                ((Image)e.Item.FindControl("del")).Visible = false;
            ((Image)e.Item.FindControl("sts")).Visible = false;
            TextBox nopol = (TextBox)e.Item.FindControl("txtnopol");
            TextBox txtjmlbal = (TextBox)e.Item.FindControl("txtjmlbal");
            nopol.ReadOnly = true;
            txtjmlbal.ReadOnly = true;
            nopol.Text = dk.NOPOL;
            txtjmlbal.Text = dk.JmlBAL.ToString("N0");
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
            TextBox nopol = (TextBox)e.Item.FindControl("txtnopol");
            TextBox txtjmlbal = (TextBox)e.Item.FindControl("txtjmlbal");
            //Image Simpan = (Image)lstDepo.Items[RowNum].FindControl("simpan");
            switch (e.CommandName)
            {
                case "hapus":
                    QAKadarAir dkbeli = new QAKadarAir();
                    DepoKertasKA dka = new DepoKertasKA();
                    ArrayList arrDataKA = new ArrayList();
                    dkbeli = dka.RetrieveBeli("and ID=" + Idxbeli + ")beli", true);
                    arrDataKA = (ArrayList)Session["arrDataKA"];
                    arrDataKA.Add(dkbeli);
                    lstKA.DataSource = arrDataKA;
                    lstKA.DataBind();
                    arrData = (ArrayList)Session["data"];
                    arrData.RemoveAt(int.Parse(Idx));
                    lstDepo.DataSource = arrData;
                    lstDepo.DataBind();
                    //tr.Visible = false;
                    break;
                case "edit":
                    ((Image)e.Item.FindControl("simpan")).Visible = true;
                    ((Image)e.Item.FindControl("edt")).Visible = false;
                    nopol.ReadOnly = false;
                    txtjmlbal.ReadOnly = false;
                    break;
                case "Save":
                    ((Image)e.Item.FindControl("simpan")).Visible = false;
                    ((Image)e.Item.FindControl("edt")).Visible = true;
                    Users user = (Users)HttpContext.Current.Session["Users"];
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "exec spUpdateDeliveryKertas " + "'" + txtCari.Text + "','" + nopol.Text + "'," + txtjmlbal.Text + " " +
                        "exec [sqlctrp.grcboard.com].bpasctrp.dbo.spUpdateDeliveryKertas " + "'" + txtCari.Text + "','" + nopol.Text + "'," + txtjmlbal.Text + " " +
                        "exec [sqljombang.grcboard.com].bpasjombang.dbo.spUpdateDeliveryKertas " + "'" + txtCari.Text + "','" + nopol.Text + "'," + txtjmlbal.Text;
                    SqlDataReader sdr = zl.Retrieve();
                    nopol.ReadOnly = true;
                    txtjmlbal.ReadOnly = true;
                    break;
            }
        }
        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("KertasDepoList0.aspx?v=1");
        }
        protected void lstKA_Command(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "prn":

                    break;
                case "po":
                    if (txtNoSJ.Text.Trim() == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Nomor surat jalan tidak boleh kosong");
                        break;
                    }
                    if (txtExpedisi.Text.Trim() == string.Empty)
                    {
                        DisplayAJAXMessage(this, "Expedisi tidak boleh kosong");
                        break;
                    }
                    if (txtNOPOL.Text.Trim() == string.Empty || txtNOPOL.Text.Trim() == "__-____-___")
                    {
                        DisplayAJAXMessage(this, "Nomor Polisi tidak boleh kosong");
                        break;
                    }

                    ArrayList arrData = new ArrayList();
                    ArrayList arrDataKA = new ArrayList();
                    int Idx = e.Item.ItemIndex;
                    int idbeli = int.Parse(e.CommandArgument.ToString());
                    arrData = (Session["data"] != null) ? (ArrayList)Session["data"] : new ArrayList();
                    arrDataKA = (Session["arrdataKA"] != null) ? (ArrayList)Session["arrdataKA"] : new ArrayList();
                    decimal gd = 0; decimal nd = 0; decimal ka = 0; decimal jb = 0; decimal stdka = 0;
                    DeliveryKertas dk = new DeliveryKertas();
                    QAKadarAir dkbeli = new QAKadarAir();
                    DepoKertasKA dka = new DepoKertasKA();
                    dkbeli = dka.RetrieveBeli("and ID=" + idbeli + ")beli", true);
                    int checknosj = CheckNomorSJ(txtNoSJ.Text, dkbeli.ItemCode);
                    #region Validasi nomor SJ
                    if (checknosj > 0)
                    {
                        DisplayAJAXMessage(this, "Nomor SJ : " + txtNoSJ.Text.Trim() + " sudah diinput...");
                        return;
                    }
                    #endregion

                    dk.DepoID = dkbeli.DepoID;
                    dk.DepoName = ddlDepo.SelectedItem.Text;
                    dk.PlantID = dkbeli.PlantID;
                    dk.NoSJ = txtNoSJ.Text.ToUpper();
                    dk.TglKirim = DateTime.Parse(txtTglKirim.Text);
                    dk.TglETA = DateTime.Parse(txtETA.Text);
                    // if (gd <= 0 || ka <= 0 || jb <= 0) { return; }
                    dk.Checker = dkbeli.Checker;
                    dk.Expedisi = txtExpedisi.Text.ToUpper();
                    dk.NOPOL = txtNOPOL.Text.ToUpper().Replace(" ", "").Replace("_", "");
                    dk.GrossDepo = dkbeli.GrossPlant;
                    dk.ItemCode = dkbeli.ItemCode;
                    dk.ItemName = dkbeli.ItemName;
                    dk.NettDepo = dkbeli.NettPlant;
                    dk.KADepo = dkbeli.AvgKA;
                    dk.JmlBAL = dkbeli.JmlBAL;
                    dk.CreatedBy = ((Users)Session["Users"]).UserName;
                    dk.RowStatus = 0;
                    dk.SupplierID = dkbeli.SupplierID;
                    dk.SupplierName = dkbeli.SupplierName;
                    dk.DocNo = txtDocNo.Text.ToUpper();
                    dk.CreatedTime = DateTime.Now;
                    dk.StdKA = dkbeli.StdKA;
                    dk.Sampah = dkbeli.Sampah;
                    //dk.POKAID = 0;
                    dk.ID = int.Parse(txtID.Value);
                    dk.KirimVia = (ddlTujuanKirim.SelectedItem.Text.Split('-'))[1].ToString().Substring(1, 1);
                    dk.IDBeli = dkbeli.ID;
                    dk.LMuatID = int.Parse(ddlLokasiMuat.SelectedValue);
                    dk.JMobil = ddlTypeMobil.SelectedItem.Text;
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
        protected void lstKA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users user = (Users)Session["Users"];
            string[] UserApp = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "DepoKertas").Split(',');
            int posx = Array.IndexOf(UserApp, user.UserName.ToString());

            QAKadarAir qa = (QAKadarAir)e.Item.DataItem;
            Label stat = (Label)e.Item.FindControl("sts");
            switch (qa.Keputusan)
            {
                case 1: stat.Text = "OK"; break;
                case 2: stat.Text = "Sortir"; break;
                case -1: stat.Text = "NO"; break;
            }
            Image ct = (Image)((Image)e.Item.FindControl("btnPrint"));//print dialog
            ct.Attributes.Add("onclick", "CetakFrom('" + qa.DocNo + "')");
            Image v = (Image)e.Item.FindControl("btnPrev");//print preview
            Image app = (Image)e.Item.FindControl("btnApp");
            Image pop = (Image)e.Item.FindControl("btnPO");
            CheckBox chk = (CheckBox)e.Item.FindControl("chk");
            Label lbl = (Label)e.Item.FindControl("lblNo");
            ct.Visible = false;
            v.Visible = true;
            app.Visible = true;
            pop.Visible = true;
            chk.Visible = false;
            lbl.Visible = true;
            v.Attributes.Add("onclick", "CetakFrom('" + qa.DocNo + "')");
            app.Visible = false;
        }
        protected void LoadPembelian(int depoid)
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
            Criteria += (ddlDepo.SelectedIndex == 0) ? "" : " and beli=0 and DepoID=" + depoid + " and plantID=" + ddlTujuanKirim.SelectedValue + " ";
            Criteria += " and itemname ='" + ddlNamaBarang.SelectedItem.Text + "')beli Order By DocNo Desc,ID ";

            arrData = ka.RetrieveBeli(Criteria);
            Session["arrDataKA"] = arrData;
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }

        protected void txtETA_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.Parse(txtTglKirim.Text) > DateTime.Parse(txtETA.Text))
            {
                txtETA.Text = DateTime.Parse(txtTglKirim.Text).AddDays(2).ToString("dd-MMM-yyyy");
                DisplayAJAXMessage(this, "Tanggal estimasi tidak boleh lebih kecil dari tanggal kirim");
                return;
            }
        }
        protected void ddlExpedisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtExpedisi.Text = ddlExpedisi.SelectedItem.Text;
        }
        protected void ddlNamaBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
        }

    }
}