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
//using System.Web.Script.Serialization;
using AjaxControlToolkit;

namespace GRCweb1.Modul.Purchasing
{
    public partial class DelivBayarKertas : System.Web.UI.Page
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

                LoadDeptKertas();

                txtTglBayar.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                txtTglJTempo.Text = DateTime.Now.AddDays(2).ToString("dd-MMM-yyyy");
                LoadDocno();
                LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
                btnSimpan.Enabled = (pos != -1) ? true : false;
                NomorDocument();
                btnSimpan.Enabled = true;
                txtJumlahBayar.Text = "0";
                Session["totalbayar"] = null;
                Session["data"] = null;
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

        private void GetGroup(string supplierID)
        {
            Session["groupname"] = null;
            Session["min30"] = null;
            Session["max30"] = null;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select isnull(groupname,'-')groupname,isnull(Min30,0)Min30,isnull(max30,0)max30 " +
                "from supppurchgroup where rowstatus>-1 and supplierID=" + supplierID + " and plantid=" + ddlTujuanKirim.SelectedValue;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Session["groupname"] = sdr["groupname"].ToString();
                    Session["min30"] = sdr["min30"].ToString();
                    Session["max30"] = sdr["max30"].ToString();
                }
            }
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
        private int GetMinKirim(string supplierID, int plantid)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select min30 jumlah from supppurchgroup where rowstatus>-1 and supplierID=" + supplierID + " and plantid=" + plantid;
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
        private int GetMaxKirim(string supplierID, int plantid)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select max30 jumlah from supppurchgroup where rowstatus>-1 and supplierID=" + supplierID + " and plantid=" + plantid;
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
        private int GetHargaT(Int32 tkirim)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select top 1 tambahHarga from HargaKertasDepoTambahan  where rowstatus>-1 and min2>=" + tkirim + " order by ID";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Int32.Parse(sdr["tambahHarga"].ToString());
                }
            }
            return result;
        }
        private int GetTotalKirim(string plantID, string supplierID, string periode1, string periode2)
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
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
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
        }

        protected void ddlTujuanKirim_Change(object sender, EventArgs e)
        {
            AutoCompleteExtender autotxt = (AutoCompleteExtender)this.FindControl("act1");
            act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
            NomorDocument();
            //LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
            //ArrayList arrData = new ArrayList();
            //Session["data"] = null;
            //arrData = (ArrayList)Session["data"];
            //lstDepo.DataSource = arrData;
            //lstDepo.DataBind();
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
            txtBayarKpd.Text = string.Empty;
        }
        private void NomorDocument()
        {
            DepoKertasKA dp = new DepoKertasKA();
            int Nomor = 0;
            //Nomor += DateTime.Parse(txtBayar.Text).Year.ToString().Substring(2, 2);
            //Nomor += DateTime.Parse(txtBayar.Text).Month.ToString().PadLeft(2, '0');
            //Nomor += "-";
            Nomor = dp.GetNoDocBayar(DateTime.Parse(txtTglBayar.Text).ToString("yyyy")) + 1;
            if (DateTime.Now.Year >= 2021)
                txtDocNo.Text = "BBK/" + Nomor.ToString().PadLeft(5, '0') + "/KAT/" + DateTime.Parse(txtTglBayar.Text).ToString("yyyy");
            else
                txtDocNo.Text = "BKK/" + Nomor.ToString().PadLeft(5, '0') + "/KAT/" + DateTime.Parse(txtTglBayar.Text).ToString("yyyy");
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
            //Session["next"] = (TextBox)Page.FindControl("txtNOPOL");
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
            Response.Redirect("DelivBayarKertas.aspx");
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
            Users user = (Users)Session["Users"];
            if (RBayarPelunasan.Checked == true)
            {
                if (txtBGNo.Text.Trim() == string.Empty)
                    return;
                NomorDocument();

                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "update delivbayarkertas set totalharga=qty*harga where docno='" + txtCari.Text + "' insert delivbayarkertas " +
                    "SELECT IDBeli, '" + txtDocNo.Text + "' DocNo, Penerima, '" + DateTime.Parse(txtTglBayar.Text).ToString("yyyy-MM-dd") +
                    "' TglBayar, JTempo, '" + txtBGNo.Text + "' BGNo, AnBGNo, SupplierID, SupplierName, ItemCode, ItemName, Harga, Qty, " +
                    "TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, LMuatID, TypeByr, BayarExp, DP FROM DelivBayarKertas WHERE (DocNo = '" + txtCari.Text + "') " +
                    "insert DelivPelunasanDP (DocnoPelunasan,DocnoDP,createdBy,createdtime,rowstatus)values('" + txtDocNo.Text + "','" + txtCari.Text +
                    "','" + user.UserName + "',getdate(),0) ";
                SqlDataReader sdr = zl.Retrieve();
                txtCari.Text = txtDocNo.Text;
                btnCari_Click(null, null);
                return;
            }
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

                    dks = dk;
                    dks.TypeByr = 1;
                    dks.LMuatID = 0;
                    txtJumlahBayar.Text = txtJumlahBayar.Text.Replace(".", "");
                    txtJumlahBayar.Text = txtJumlahBayar.Text.Replace(",", "");
                    if (RBayarDP.Checked == true)
                        dks.DP = Int32.Parse(txtJumlahBayar.Text.Replace(".", ""));
                    else
                        dks.DP = 0;
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
                    dks = dk;
                    dks.TypeByr = 1;
                    dks.LMuatID = 0;
                    txtJumlahBayar.Text = txtJumlahBayar.Text.Replace(".", "");
                    txtJumlahBayar.Text = txtJumlahBayar.Text.Replace(",", "");
                    if (RBayarDP.Checked == true)
                    {
                        if (txtDP.Text.Trim() == "0")
                            dks.DP = Int32.Parse(txtJumlahBayar.Text.Replace(".", ""));
                        else
                            dks.DP = Int32.Parse(txtDP.Text.Replace(".", ""));
                        result += dp.InsertBayarDP(dks);
                    }
                    else
                    {
                        dks.DP = 0;
                        result += dp.InsertBayar(dks);
                    }
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
            zl.CustomQuery = "Select * from Company where RowStatus=0 and depoID in (1,7,13)";
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
            AutoCompleteExtender autotxt = (AutoCompleteExtender)this.FindControl("act1");
            act1.CompletionSetCount = int.Parse(ddlTujuanKirim.SelectedValue);
        }
        private string GetUPSupplier(int plantID, int supplierID)
        {
            string result = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (plantID == 7)
                zl.CustomQuery = "Select UP from suppPurch where RowStatus>-1 and ID=" + supplierID;
            if (plantID == 1)
                zl.CustomQuery = "Select UP from [sqlctrp.grcboard.com].bpasctrp.dbo.suppPurch where RowStatus>-1 and ID=" + supplierID;
            if (plantID == 13)
                zl.CustomQuery = "Select UP from [sqlJombang.grcboard.com].bpasJombang.dbo.suppPurch where RowStatus>-1 and ID=" + supplierID;
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
        private string GetUPSupplierID(int plantID, string suppliername)
        {
            string result = string.Empty;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            if (plantID == 7)
                zl.CustomQuery = "Select ID from suppPurch where RowStatus>-1 and suppliername='" + suppliername + "'";
            if (plantID == 1)
                zl.CustomQuery = "Select ID from [sqlctrp.grcboard.com].bpasctrp.dbo.suppPurch where RowStatus>-1 and suppliername='" + suppliername + "'";
            if (plantID == 13)
                zl.CustomQuery = "Select ID from [sqlJombang.grcboard.com].bpasJombang.dbo.suppPurch where RowStatus>-1 and suppliername='" + suppliername + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["ID"].ToString();
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
            //viewonly = 1;
            //if (RBayarPelunasan.Checked==false)
            //    btnSimpan.Enabled = false;
            //else
            //    btnSimpan.Enabled = true;
            Session["totalbayar"] = null;
            Session["data"] = null;
            if (txtCari.Text == string.Empty) return;
            Users user = (Users)Session["Users"];
            DepoKertasKA dk = new DepoKertasKA();
            string Criteria = " And DocNo like '%" + txtCari.Text + "%'";
            BayarKertas hbayar = new BayarKertas();

            if (RBayarFull.Checked == true)
                hbayar = dk.RetrieveHeaderBayar(txtCari.Text);
            if (RBayarDP.Checked == true)
                hbayar = dk.RetrieveHeaderBayarDP(txtCari.Text);
            if (RBayarPelunasan.Checked == true)
                hbayar = dk.RetrieveHeaderBayarPelunasan(txtCari.Text);

            if (hbayar.TypeByr == 1)
            {
                ddlDepo.SelectedValue = hbayar.DepoID.ToString();
                txtJumlahBayar.Text = hbayar.TotalHarga.ToString("N0");
                txtDP.Text = hbayar.DP.ToString();
                if (RBayarPelunasan.Checked == true && CekPelunasan(hbayar.DocNo) != 0)
                {
                    txtBGNo.Text = "";
                    txtTglBayar.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    NomorDocument();
                }
                else
                {
                    txtBGNo.Text = hbayar.BGNo;
                    txtTglBayar.Text = hbayar.TglBayar.ToString("dd-MMM-yyyy");
                    txtDocNo.Text = hbayar.DocNo;
                }
                txtAnBGNo.Text = hbayar.AnBGNo;
                txtTglJTempo.Text = hbayar.JTempo.ToString("ddMMMyyyy");

                txtBayarKpd.Text = hbayar.Penerima;

                ArrayList arrData = dk.RetrieveBayar(Criteria);
                lstDepo.DataSource = arrData;
                lstDepo.DataBind();
            }
            //else
            //    DisplayAJAXMessage(this, "Data tidak ditemukan");

            if (RBayarDP.Checked == true)
            {
                txtSupplierID.Value = GetUPSupplierID(int.Parse(ddlTujuanKirim.SelectedValue), txtBayarKpd.Text);
                txtBayarKpd_TextChanged(null, null);
            }
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue));
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
                    QAKadarAir dkbeli = new QAKadarAir();
                    DepoKertasKA dka = new DepoKertasKA();
                    ArrayList arrDataKA = new ArrayList();
                    Label txttotalharga = (Label)e.Item.FindControl("txttotalharga");
                    if (txttotalharga != null)
                        Session["totalbayar"] = (Session["totalbayar"] != null) ? decimal.Parse(Session["totalbayar"].ToString()) - decimal.Parse(txttotalharga.Text) : 0;
                    txtJumlahBayar.Text = decimal.Parse(Session["totalbayar"].ToString()).ToString("N0");
                    dkbeli = dka.RetrieveBeli("and ID=" + Idxbeli + ")beli", true);
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
            Response.Redirect("DelivBayarKertasList.aspx?v=1");
        }
        protected void lstKA_Command(object sender, RepeaterCommandEventArgs e)
        {
            Users user = (Users)HttpContext.Current.Session["Users"];
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
                    QAKadarAir dkbeli = new QAKadarAir();
                    DepoKertasKA dka = new DepoKertasKA();

                    dkbeli = dka.RetrieveBeli("and ID=" + idbeli + ")beli", true);
                    GetGroup(dkbeli.SupplierID.ToString());
                    string LPeriode1 = string.Empty;
                    if (dkbeli.NoSJ.Trim().Length == 1)
                        LPeriode1 = DateTime.Parse("01-" + dkbeli.TglCheck.ToString("MMM-yyyy")).ToString("yyyyMMdd");
                    else
                        LPeriode1 = GetTglKirim(dkbeli.NoSJ).ToString("yyyyMMdd");
                    string LPeriode2 = DateTime.Parse(dkbeli.TglCheck.ToString("dd-MMM-yyyy")).ToString("yyyyMMdd");
                    int TotKirim = GetTotalKirim(dkbeli.PlantID.ToString(), dkbeli.SupplierID.ToString(), LPeriode1, LPeriode2);
                    int minkirim = GetMinKirim(dkbeli.SupplierID.ToString(), dkbeli.PlantID);
                    int maxkirim = GetMaxKirim(dkbeli.SupplierID.ToString(), dkbeli.PlantID);
                    int HargaT = 0;
                    //if (TotKirim >= minkirim && IsProgram30(dkbeli.SupplierID.ToString()) > 0 && TotKirim <= maxkirim + minkirim)
                    //{
                    //    HargaT = GetHargaT(minkirim);
                    //}
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
                    dk.IDBeli = dkbeli.ID;
                    dk.TglBayar = DateTime.Parse(txtTglBayar.Text);
                    dk.JTempo = DateTime.Parse(txtTglJTempo.Text);
                    dk.ItemCode = dkbeli.ItemCode;
                    dk.ItemName = dkbeli.ItemName;
                    dk.Penerima = txtBayarKpd.Text;
                    dk.Harga = GetHargaKertasDepo(dk.SupplierID, dk.PlantID, dk.ItemCode) + HargaT;
                    dk.TotalHarga = (dk.Harga) * dkbeli.NettPlant;
                    if (RBayarDP.Checked == true)
                    {
                        dk.TotalHarga = (dk.Harga * dkbeli.GrossPlant) / 2;
                    }
                    dk.TKirim = TotKirim;
                    dk.MinKirim = minkirim;
                    dk.HargaT = HargaT;
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
        private decimal GetHargaKertasDepo(int supplierID, int plantID, string itemcode)
        {
            decimal result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //if (plantID == 7)
            zl.CustomQuery = "select * from suppPurchhrgkertasdepo A inner join hargakertasdepo B on A.hargaid=B.id and A.rowstatus>-1 and B.rowstatus>-1 " +
                "and supplierID=" + supplierID + " and A.plantid=" + plantID + " and itemcode='" + itemcode + "'";
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
        private DateTime GetTglKirim(string nosj)
        {
            DateTime result = DateTime.Now;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            //if (plantID == 7)
            zl.CustomQuery = "select * from deliverykertas where nosj='" + nosj + "'";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = DateTime.Parse(sdr["tglkirim"].ToString());
                }
            }
            return result;
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
        protected void LoadPembelian(int depoid)
        {
            if (RBayarPelunasan.Checked == true)
            {
                //lstDepo.Visible = false;
                lstKA.Visible = false;
                return;
            }
            lstDepo.Visible = true;
            lstKA.Visible = true;
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            Session["arrDataKA"] = null;
            string bayardp = string.Empty;
            //if (RBayarDP.Checked == true)
            //    bayardp = "and isnull(DP,0)=1";
            //else
            //    bayardp = "and isnull(DP,0)=0";
            //string Criteria = (ddlBulan.SelectedIndex > 0) ? " And Month(TglCheck)=" + ddlBulan.SelectedValue : "";
            //Criteria += " And Year(TglCheck)=" + ddlTahun.SelectedValue;
            string Criteria = string.Empty;
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            Criteria += (ddlDepo.SelectedIndex == 0) ? "" : " and bayar=0 " + bayardp + " and DepoID=" + depoid + "  and plantID=" + ddlTujuanKirim.SelectedValue + " "; ;
            Criteria += ")beli Order By DocNo Desc,ID ";

            arrData = ka.RetrieveBeli(Criteria);
            Session["arrDataKA"] = arrData;
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }
        protected void LoadPembelianBySupplierID(int supplierID, int depoid)
        {
            if (RBayarPelunasan.Checked == true)
            {
                lstDepo.Visible = false;
                return;
            }
            lstDepo.Visible = true;
            ArrayList arrData = new ArrayList();
            DepoKertasKA ka = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            Session["arrDataKA"] = null;
            string bayardp = string.Empty;
            //if (RBayarDP.Checked == true)
            //    bayardp = "and isnull(DP,0)=1";
            //else
            //    bayardp = "and isnull(DP,0)=0";
            //string Criteria = (ddlBulan.SelectedIndex > 0) ? " And Month(TglCheck)=" + ddlBulan.SelectedValue : "";
            //Criteria += " And Year(TglCheck)=" + ddlTahun.SelectedValue;
            string Criteria = string.Empty;
            string[] UserViewAll = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ViewAllData", "DepoKertas").Split(',');
            int pos = Array.IndexOf(UserViewAll, user.UserName);
            Criteria += (ddlDepo.SelectedIndex == 0) ? "" : " and isnull(bayar,0)=0 " + bayardp + " and DepoID=" + depoid + " and supplierID=" + supplierID +
                "  and plantID=" + ddlTujuanKirim.SelectedValue + " ";
            Criteria += ")beli Order By DocNo Desc,ID ";

            arrData = ka.RetrieveBeli(Criteria);
            Session["arrDataKA"] = arrData;
            lstKA.DataSource = arrData;
            lstKA.DataBind();
        }

        protected void txtBayarKpd_TextChanged(object sender, EventArgs e)
        {
            if (txtBayarKpd.Text != string.Empty && txtSupplierID.Value != string.Empty)
            {
                LoadPembelianBySupplierID(int.Parse(txtSupplierID.Value), int.Parse(ddlDepo.SelectedValue));
            }
            else
                LoadPembelian(int.Parse(ddlDepo.SelectedValue));
            if (txtSupplierID.Value != string.Empty)
                txtAnBGNo.Text = GetUPSupplier(int.Parse(ddlTujuanKirim.SelectedValue), int.Parse(txtSupplierID.Value));
            txtBGNo.Focus();
        }
        private int CekPelunasan(string docno)
        {
            int result = 0;
            decimal total = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select sum(totalharga) -sum(qty*harga) total from delivbayarkertas where RowStatus>-1 and docno='" + docno + "'";
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
        private int GetTotalBayar(string docno)
        {
            int result = 0;
            decimal total = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select sum(totalharga) -isnull((select top 1 dp from DelivBayarKertas where rowstatus>-1 and docno='" + docno + "' ),0) total from delivbayarkertas where RowStatus>-1 and docno='" + docno + "'";
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
        private int GetTotalBayarDP(string docno)
        {
            int result = 0;
            decimal total = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select isnull((select top 1 dp from DelivBayarKertas where rowstatus>-1 and docno='" + docno + "' ),0) total from delivbayarkertas where RowStatus>-1 and docno='" + docno + "'";
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
        protected void btnCetak_Click(object sender, EventArgs e)
        {
            Session["pelunasan"] = 0;
            if (txtCari.Text.Trim() != txtDocNo.Text.Trim())
                return;
            string strSQL = string.Empty;
            if (RBayarPelunasan.Checked == true)
            {
                strSQL = "select  DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo, SupplierID, SupplierName, ItemCode, ItemName, Harga, Qty, " +
                    "TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, LMuatID, TypeByr, BayarExp, isnull(DP,0)DP from delivbayarkertas "
                    + "where rowstatus>-1 and totalharga>0 and docno='" + txtDocNo.Text + "' " +
                    "union all select top 1 DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo, SupplierID, SupplierName, ItemCode,'Pembayaran DP ' ItemName, " +
                    "0 Harga, 0 Qty, isnull(DP,0) * -1 TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, LMuatID, TypeByr, BayarExp, " +
                    "isnull(DP,0)DP from delivbayarkertas where rowstatus>-1 and totalharga>0 and docno in (select docnodp from DelivPelunasanDP where DocnoPelunasan='" + txtDocNo.Text + "') ";
                Session["docno"] = txtDocNo.Text;
                Session["total"] = GetTotalBayar(txtDocNo.Text);
                if (RBayarPelunasan.Checked == true)
                    Session["pelunasan"] = 2;
            }
            else if (RBayarDP.Checked == true)
            {
                strSQL = "select top 1 DocNo, Penerima, TglBayar, JTempo, BGNo, AnBGNo, SupplierID, SupplierName, ItemCode,'Pembayaran DP ' ItemName, " +
                   "0 Harga, 0 Qty, isnull(DP,0) TotalHarga, Rowstatus, CreatedBy, CreatedTime, DepoID, LMuatID, TypeByr, BayarExp, " +
                   "isnull(DP,0)DP from delivbayarkertas where rowstatus>-1 and totalharga>0 and docno='" + txtDocNo.Text + "' ";
                Session["docno"] = txtDocNo.Text;
                Session["total"] = GetTotalBayarDP(txtDocNo.Text);
                Session["pelunasan"] = 1;
            }
            else if (RBayarFull.Checked == true)
            {
                Session["docno"] = txtDocNo.Text;
                Session["total"] = GetTotalBayar(txtDocNo.Text);
                Session["pelunasan"] = 0;
            }
            Cetak(this);
        }
        static public void Cetak(Control page)
        {

            //string myScript = "var wn = window.showModalDialog('../../Report/Report.aspx?IdReport=bayarkertas', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 900px;scrollbars=yes');";
            string myScript = "Cetak();";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void RBayarDP_CheckedChanged(object sender, EventArgs e)
        {
            if (RBayarDP.Checked == true)
            {
                btnCetak.Visible = true;
                Panel3.Visible = false;
            }
            else
                btnCetak.Visible = true;
            ArrayList arrData = new ArrayList();
            Session["data"] = null;
            if (int.Parse(txtID.Value) == 0)
            {
                lstDepo.DataSource = arrData;
                lstDepo.DataBind();
            }
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
            txtBayarKpd.Text = string.Empty;
            txtJumlahBayar.Text = "0";
            Session["totalbayar"] = null;
        }
        protected void RBayarPelunasan_CheckedChanged(object sender, EventArgs e)
        {
            if (RBayarPelunasan.Checked == true)
            {
                btnCetak.Visible = true;
                Panel3.Visible = true;
            }
            else
                btnCetak.Visible = false;
            ArrayList arrData = new ArrayList();
            Session["data"] = null;
            if (int.Parse(txtID.Value) == 0)
            {
                lstDepo.DataSource = arrData;
                lstDepo.DataBind();
            }
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
            txtBayarKpd.Text = string.Empty;
            txtJumlahBayar.Text = "0";
            Session["totalbayar"] = null;
        }
        protected void RBayarFull_CheckedChanged(object sender, EventArgs e)
        {
            if (RBayarFull.Checked == true)
            {
                btnCetak.Visible = true;
                Panel3.Visible = false;
            }
            else
                btnCetak.Visible = false;

            ArrayList arrData = new ArrayList();
            Session["data"] = null;
            if (int.Parse(txtID.Value) == 0)
            {
                lstDepo.DataSource = arrData;
                lstDepo.DataBind();
            }
            LoadPembelian(Int32.Parse(ddlDepo.SelectedValue.ToString()));
            txtBayarKpd.Text = string.Empty;
            txtJumlahBayar.Text = "0";
            Session["totalbayar"] = null;
        }
        private void LoadDocno()
        {
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select distinct Docno from DelivBayarKertas where RowStatus>-1 and typebyr=1 and qty*harga<>totalharga and isnull(dp,0)>=0";
            SqlDataReader sdr = zl.Retrieve();
            ddlDocNo.Items.Clear();
            ddlDocNo.Items.Add(new ListItem("Pilih BKK yang akan dilunasi ", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDocNo.Items.Add(new ListItem(sdr["Docno"].ToString(), sdr["Docno"].ToString()));
                }
            }
            ddlDocNo.SelectedIndex = 0;
        }
        protected void ddlDocNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCari.Text = ddlDocNo.SelectedItem.Value;
            btnCari_Click(null, null);
        }

    }
}