using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using System.Web.UI.HtmlControls;
using System.Data;
using System.IO;

namespace GRCweb1.Modul.Purchasing
{
    public partial class SuratJalanKeluar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Session["ListSuratJalan"] = null;
                //LoadMasterRak();
                //LoadSatuan();
                LoadTipeBarang();
                LoadTipeSatuan();
                txtTglSJ.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                //txtUomID.Enabled = false;
                //ddlStatus.Enabled = false;
                btnCetak.Enabled = false;
                //btnList.Enabled = false;
                if (Request.QueryString["NoSJ"] != null)
                {
                    TampilkanData(Request.QueryString["NoSJ"].ToString());
                }

            }
            else
            {
                if (txtCariNamaBrg.Text != string.Empty)
                {
                    LoadItems();
                    txtCariNamaBrg.Text = string.Empty;
                }
            }
        }

        protected void ddlTipeBarang_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (ddlTipeBarang.SelectedValue)
            {
                case "99":
                    dll.Visible = true;
                    cb1.Visible = false;
                    cB.Visible = false;
                    break;
                default:
                    dll.Visible = false;
                    cb1.Visible = true;
                    cB.Visible = true;
                    break;
            }
        }

        private void TampilkanData(string NoSJ)
        {
            Session["ListSuratJalan"] = null;
            Users users = (Users)Session["Users"];
            SuratJalankeluar sjkt = new SuratJalankeluar();
            INVSuratJalanKeluarFacade invsjk = new INVSuratJalanKeluarFacade();
            sjkt = invsjk.RetrieveByNoSJ(NoSJ);
            if (invsjk.Error == string.Empty && sjkt.ID > 0)
            {
                Session["id"] = sjkt.ID;
                txtNoSJ.Text = sjkt.NoSJ;
                // txtNopol.Text = sjkt.NoPolisi;
                txtTglSJ.Text = sjkt.TglSJ.ToString("dd-MMM-yyyy");
                ddlTipeBarang.SelectedIndex = sjkt.ItemTypeID;


                INVSuratJalanKeluarFacade invsjk1 = new INVSuratJalanKeluarFacade();
                ArrayList arrsjkt1 = invsjk1.RetrieveBySJ1(sjkt.NoSJ);
                if (invsjk1.Error == string.Empty)
                {
                    Session["NoSJ"] = sjkt.NoSJ;
                    Session["ListSuratJalan"] = arrsjkt1;
                    if (arrsjkt1.Count <= 0) arrsjkt1 = new ArrayList();
                    GridView1.DataSource = arrsjkt1;
                    GridView1.DataBind();
                    lstSJ.DataSource = arrsjkt1;
                    lstSJ.DataBind();
                    txtTglSJ.Enabled = false;
                    //txtNoSJ.Enabled = false;
                    txtCariNamaBrg.Enabled = false;
                    ddlTipeBarang.Enabled = false;
                    ddlItemName.Enabled = false;
                    ddlSatuan.Enabled = false;
                    txtTujuan.Enabled = false;
                    txtQty.Enabled = false;
                    txtNopol.Enabled = false;
                    txtKet.Enabled = false;
                    lbAddOP.Enabled = false;
                    btnCetak.Enabled = true;
                }
            }

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        private void LoadItems()
        {
            try
            {
                //arrItems = inventoryFacade.RetrieveByCriteriaWithGroupIDROP("ItemName", strNabar, int.Parse(ddlTipeSPP.SelectedValue), users.ID);
                ddlItemName.Items.Clear();
                ArrayList arrItems = new ArrayList();
                //Users users = (Users)Session["Users"];
                InventoryFacade inventoryFacade = new InventoryFacade();
                AssetFacade assetFacade = new AssetFacade();
                BiayaFacade biayaFacade = new BiayaFacade();

                //if (ddlTipeBarang.SelectedIndex == 1)
                //{
                //    arrItems = inventoryFacade.RetrieveByCriteria("A.ItemName", txtCariNamaBrg.Text);
                //}
                //if (ddlTipeBarang.SelectedIndex == 2)
                //{
                //    arrItems = assetFacade.RetrieveByCriteria("A.ItemName", txtCariNamaBrg.Text);
                //}
                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    arrItems = inventoryFacade.RetrieveByCriteria("A.ItemName", txtCariNamaBrg.Text);
                }
                if (ddlTipeBarang.SelectedIndex == 2)
                {
                    arrItems = assetFacade.RetrieveByCriteriaSJ("A.ItemName", txtCariNamaBrg.Text, "Asset");
                }
                if (ddlTipeBarang.SelectedIndex == 3)
                {
                    arrItems = assetFacade.RetrieveByCriteriaSJ("A.ItemName", txtCariNamaBrg.Text, "Biaya");
                }
                ddlItemName.Items.Add(new ListItem("-- Pilih Items --", "0"));
                if (ddlTipeBarang.SelectedIndex == 1)
                {
                    foreach (Inventory inventory in arrItems)
                    {

                        ddlItemName.Items.Add(new ListItem(inventory.ItemName + " (" + inventory.ItemCode + ")", inventory.ID.ToString()));

                    }
                }
                if (ddlTipeBarang.SelectedIndex == 2)
                {
                    foreach (Asset asset in arrItems)
                    {
                        ddlItemName.Items.Add(new ListItem(asset.ItemName + " (" + asset.ItemCode + ")", asset.ID.ToString()));
                    }
                }
                if (ddlTipeBarang.SelectedIndex == 3)
                {
                    foreach (Asset asset in arrItems)
                    {
                        ddlItemName.Items.Add(new ListItem(asset.ItemName + " (" + asset.ItemCode + ")", asset.ID.ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                DisplayAJAXMessage(this, error + " Item tidak ditemukan");
            }
        }

        private void LoadTipeBarang()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();

            ddlTipeBarang.Items.Add(new ListItem("-- Pilih Tipe Barang --", "0"));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlTipeBarang.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
            ddlTipeBarang.Items.Add(new ListItem("LAIN - LAIN", "99"));
        }

        private void LoadTipeSatuan()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.RetrieveSatuan();

            ddlSatuan.Items.Add(new ListItem("-- Pilih Tipe Satuan --", "0"));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlSatuan.Items.Add(new ListItem(itemTypePurchn.UOMCode, itemTypePurchn.UOMCode));
            }
            ddlSatuan.Items.Add(new ListItem("LAIN - LAIN", "99"));
        }


        private void clearForm()
        {
            ViewState["id"] = null;
            Session.Remove("id");
            Session["NoSJ"] = null;
            Session["ListSuratJalan"] = null;
            txtTglSJ.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCariNamaBrg.Text = string.Empty;
            ddlItemName.Items.Clear();
            if (ddlTipeBarang.SelectedIndex > 0) ddlTipeBarang.SelectedIndex = 0;
            txtNoSJ.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtNopol.Text = string.Empty;
            txtKet.Text = string.Empty;
            txtTujuan.Text = string.Empty;
            ArrayList arrData = new ArrayList();
            lstSJ.DataSource = arrData;
            lstSJ.DataBind();
            txtCariNamaBrg.Focus();
            txtTglSJ.Enabled = true;
            //txtNoSJ.Enabled = false;
            txtCariNamaBrg.Enabled = true;
            ddlTipeBarang.Enabled = true;
            ddlItemName.Enabled = true;
            txtTujuan.Enabled = true;
            txtQty.Enabled = true;
            txtNopol.Enabled = true;
            txtKet.Enabled = true;
            lbAddOP.Enabled = true;
            ddlSatuan.Enabled = true;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("NoSJ");
            Session.Remove("ListSuratJalan");
            clearForm();
        }

        protected void btnUpdate_serverClick(object sender, EventArgs e)
        {
            int intResult = 0;
            string strEvent = "Insert";
            int maxID = 0;
            SuratJalankeluar cekLastID = new SuratJalankeluar();
            SuratJalankeluar sjk = new SuratJalankeluar();
            INVSuratJalanKeluarFacade sjkFacade = new INVSuratJalanKeluarFacade();
            sjkFacade.Tahun = DateTime.Parse(txtTglSJ.Text).Year;//ini akan otomatis balik ke 1 nomor nya jika tahun baru lagi
            maxID = sjkFacade.RetrieveMaxId();

            sjkFacade = new INVSuratJalanKeluarFacade();
            if (ViewState["id"] != null)
            {
                sjk.ID = int.Parse(ViewState["id"].ToString());
                strEvent = "Edit";
            }
            if (Session["ListSuratJalan"] == null)
            {
                DisplayAJAXMessage(this, "Form Isian Masih Kosong");
                return;
            }
            ArrayList arrData = (ArrayList)Session["ListSuratJalan"];
            foreach (SuratJalankeluar sjkb in arrData)
            {
                intResult = 0;
                sjk.NoUrut = maxID + 1;
                sjk.NoSJ = (maxID + 1).ToString().PadLeft(3, '0') + "/LOG/" + Global.ConvertNumericToRomawi(DateTime.Now.Month) + "/" + DateTime.Now.Year.ToString();
                sjk.TglSJ = sjkb.TglSJ;
                sjk.Tujuan = sjkb.Tujuan;
                sjk.ItemTypeID = sjkb.ItemTypeID;
                sjk.Satuan = sjkb.Satuan;
                sjk.ItemID = sjkb.ItemID;
                sjk.ItemName = sjkb.ItemName;
                sjk.Jumlah = sjkb.Jumlah;
                sjk.Ket = sjkb.Ket;
                sjk.NoPolisi = sjkb.NoPolisi;
                sjk.CreatedBy = ((Users)Session["Users"]).UserName;
                sjk.LastModifiedBy = ((Users)Session["Users"]).UserName;

                if (sjk.ID > 0)
                {
                    intResult = sjkFacade.Update(sjk);
                    InsertLog(strEvent);
                }
                else
                {
                    intResult = sjkFacade.Insert(sjk);
                    if (intResult > 0)
                    {
                        DisplayAJAXMessage(this, "Data Telah Disimpan");
                        //clearForm();
                        InsertLog(strEvent);

                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Gagal Simpan");
                    }
                }

            }

        }

        protected void btnRekap_ServerClick(object sender, EventArgs e)
        {
            Response.Redirect("RekapSuratJalanKeluar.aspx");
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            //Company company = new Company();
            //CompanyFacade companyFacade = new CompanyFacade();
            //string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "SJ";
            //Response.Redirect("ListSuratJalanKeluar.aspx?plant=" + kd);
            Session["ListSuratJalan"] = null;
            Session["NoSJ"] = null;
            Response.Redirect("ListSuratJalanKeluar.aspx?SJ=" + (((Users)Session["Users"]).GroupID));
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {

        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            ArrayList arrItem = (Session["ListSuratJalan"] == null) ? new ArrayList() : (ArrayList)Session["ListSuratJalan"];
            SuratJalankeluar sjk = new SuratJalankeluar();
            if (ddlItemName.SelectedIndex == 0 && ddlTipeBarang.SelectedValue != "99")
            {
                DisplayAJAXMessage(this, " Nama Barang tidak ada ");
                return;
            }
            if (ddlTipeBarang.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, " Pilih Tipe Barang ");
                return;
            }
            if (txtQty.Text == string.Empty)
            {
                DisplayAJAXMessage(this, " Isi Jumlah ");
                return;
            }
            if (txtNopol.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi No Polisi");
                return;
            }
            if (txtTujuan.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi Tujuan");
                return;
            }
            if (txtKet.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Isi Keterangan");
                return;
            }
            if (ddlSatuan.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, " Pilih Tipe Satuan ");
                return;
            }

            string[] item = txtCariNamaBrg.Text.Split('-');
            sjk.ID = int.Parse(txtID.Value);
            sjk.ItemID = (ddlTipeBarang.SelectedValue == "99") ? 0 : int.Parse(ddlItemName.SelectedValue);
            sjk.ItemName = (ddlTipeBarang.SelectedValue == "99") ? txtNamaMaterial.Text : ddlItemName.SelectedItem.Text;
            sjk.ItemTypeID = (ddlTipeBarang.SelectedValue == "99") ? 0 : int.Parse(ddlTipeBarang.SelectedValue);
            sjk.Jumlah = decimal.Parse(txtQty.Text);
            sjk.Satuan = (ddlSatuan.SelectedValue == "99") ? "" : ddlSatuan.SelectedItem.Text;
            sjk.Tujuan = txtTujuan.Text;
            sjk.Ket = txtKet.Text;
            sjk.NoPolisi = txtNopol.Text;
            sjk.TglSJ = DateTime.Parse(txtTglSJ.Text);

            arrItem.Add(sjk);
            Session["ListSuratJalan"] = arrItem;
            lstSJ.DataSource = arrItem;
            lstSJ.DataBind();

        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void lstSJ_Databound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void lstSJ_Command(object sender, RepeaterCommandEventArgs e)
        {

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Gudang";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtNoSJ.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
            //if (eventLogFacade.Error == string.Empty)
            //    //clearForm();
        }


        protected void btnCetak_Click(object sender, EventArgs e)
        {

        }

    }
}