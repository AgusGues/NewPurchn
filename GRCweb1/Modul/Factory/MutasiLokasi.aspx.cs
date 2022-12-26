using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using Factory;
using Cogs;
using System.Collections;

namespace GRCweb1.Modul.Factory
{
    public partial class MutasiLokasi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                DatePicker1.Text = DateTime.Now.AddDays(-1).ToString("dd MMM yyyy");

            }
        }

        private void clearform()
        {

            //Session["hpp2"] = null;
            //Session["luas1"] = null;
            //Session["luas2"] = null;

            txtPartnoA.Text = string.Empty;
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
            txtPartname1.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtStock1.Text = string.Empty;
            txtQtyOut.Text = string.Empty;


            txtLokasi2.Text = string.Empty;
            LoadDataGridViewtrans();
            LoadDataLoading();
            LoadDataGridViewMutasiLok();
        }

        private void LoadDataGridViewtrans()
        {
            ArrayList arrT3Serah = new ArrayList();
            T3_SerahFacade T3Serah = new T3_SerahFacade();
            string criteria = string.Empty;
            if (txtLokasiC.Visible = true && txtLokasiC.Text != string.Empty)
                criteria = " and B.Lokasi='" + txtLokasiC.Text.Trim() + "' ";
            else
                if (txtPartnoC.Text != string.Empty)
                criteria = " and C.PartNo='" + txtPartnoC.Text.Trim() + "' ";
            //if (criteria != string.Empty)
            arrT3Serah = T3Serah.RetrieveStock(criteria);
            //Session["arrT3Serah"] = arrT3Serah;
            GridViewtrans.DataSource = arrT3Serah;
            GridViewtrans.DataBind();
        }

        private void LoadDataGridViewMutasiLok()
        {
            ArrayList arrT3MutasiLok = new ArrayList();
            T3_MutasiLokFacade T3MutasiLok = new T3_MutasiLokFacade();
            arrT3MutasiLok = T3MutasiLok.RetrieveBytgl(DateTime.Parse(DatePicker1.Text).ToString("yyyyMMdd"));
            GridViewMutasiLok.DataSource = arrT3MutasiLok;
            GridViewMutasiLok.DataBind();
        }

        private void LoadDataLoading()
        {
            ArrayList arrT3Serah = new ArrayList();
            T3_SerahFacade T3Serah = new T3_SerahFacade();

            arrT3Serah = T3Serah.RetrieveFromLoading(txtPartnoA.Text.Trim());
            GridViewtrans0.DataSource = arrT3Serah;
            GridViewtrans0.DataBind();
            txtQtyOut.Text = string.Empty;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }
        protected void TransferData()
        {
            //proses lokasi awal


            //int intresult = 0;

            Users users = (Users)Session["Users"];
            T3_Serah t3serahK = new T3_Serah();
            T3_Serah t3serahT = new T3_Serah();
            FC_Items items = new FC_Items();

            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
            T3_SiapKirimFacade t3siapkirimfacade = new T3_SiapKirimFacade();
            T3_MutasiLok t3mutasilok = new T3_MutasiLok();
            T3_MutasiLokFacade MutasiLokFacade = new T3_MutasiLokFacade();
            int awalID = 0;
            int stock = 0;
            int stock2 = 0;
            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DateTime.Parse(DatePicker1.Text).Year;
            int Bulan = DateTime.Parse(DatePicker1.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            if (ChkLoading.Checked == false)
            {
                if (txtLokasi1.Text == string.Empty || txtLokasi2.Text == string.Empty || txtQtyOut.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Data belum lengkap");
                    return;
                }
                t3serahK.Flag = "kurang";
                t3serahK.GroupID = int.Parse(Session["GroupID"].ToString());
                t3serahK.ItemID = int.Parse(Session["itemid1"].ToString());
                t3serahK.ID = int.Parse(Session["serahid"].ToString());
                t3serahK.LokID = int.Parse(Session["lokid1"].ToString());
                t3serahK.Qty = Convert.ToInt32(txtQtyOut.Text);
                t3serahK.CreatedBy = users.UserName;
                /** dapatkan stock lokasi awal */
                T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
                T3_Serah t3serah = new T3_Serah();
                t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
                stock = SerahFacade.GetStock(t3serah.LokID, t3serah.ItemID);
                //stock = SerahFacade.GetStock(int.Parse(Session["lokid1"].ToString()), int.Parse(Session["itemid1"].ToString()));
                if (stock - Convert.ToInt32(txtQtyOut.Text) < 0)
                {
                    DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                    return;
                }
                //intresult = SerahFacade.Insert(t3serah);
                awalID = t3serahK.ID;

                t3serahT.Flag = "tambah";
                t3serahT.ItemID = int.Parse(Session["itemid1"].ToString());
                t3serahT.ID = int.Parse(Session["serahid"].ToString());
                t3serahT.LokID = int.Parse(Session["lokid2"].ToString());
                t3serahT.Qty = Convert.ToInt32(txtQtyOut.Text);
                t3serahT.CreatedBy = users.UserName;
                //intresult = SerahFacade.Insert(t3serah);
                awalID = t3serahT.ID;

                //rekam table MutasiLok
                T3_Serah t3serah2 = new T3_Serah();
                t3serah2 = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi2.Text);
                stock2 = SerahFacade.GetStock(t3serah2.LokID, t3serah2.ItemID);

                //stock2 = SerahFacade.GetStock(int.Parse(Session["lokid2"].ToString()), int.Parse(Session["itemid1"].ToString()));
                t3mutasilok.SerahID = int.Parse(Session["serahid"].ToString());
                t3mutasilok.LokID1 = int.Parse(Session["lokid1"].ToString());
                t3mutasilok.LokID2 = int.Parse(Session["lokid2"].ToString());
                t3mutasilok.TglML = DateTime.Parse(DatePicker1.Text);
                t3mutasilok.ItemID = int.Parse(Session["itemid1"].ToString());
                t3mutasilok.Qty = Convert.ToInt32(txtQtyOut.Text);
                t3mutasilok.SA1 = stock;
                t3mutasilok.SA2 = stock2;
                //MutasiLok.QtyOutSm = Convert.ToInt32(txtQty2.Text);
                t3mutasilok.CreatedBy = users.UserName;
                //intresult = MutasiLokFacade.Insert(t3mutasilok);
                T3_MutasiLokProcessFacade SimetrisProcessFacade = new T3_MutasiLokProcessFacade(t3serahK, t3serahT, t3mutasilok, 0);
                string strError = SimetrisProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    DisplayAJAXMessage(this, "Data tersimpan");
                    txtPartnoA.Focus();
                    clearform();
                }
            }
            else
            {
                for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
                {
                    TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                    if (txtQtyMutasi.Text != string.Empty)
                    {

                        stock = SerahFacade.GetStock(int.Parse(GridViewtrans0.Rows[i].Cells[3].Text), int.Parse(GridViewtrans0.Rows[i].Cells[2].Text));
                        if (stock - Convert.ToInt32(txtQtyMutasi.Text) < 0)
                        {
                            DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                            return;
                        }
                        awalID = t3serahK.ID;
                        //t3siapkirimfacade.UpdatebyKirim(int.Parse(GridViewtrans0.Rows[i].Cells[0].Text), Convert.ToInt32(txtQtyMutasi.Text) * -1, users.UserName,t3mutasilok );
                        t3serahK = SerahFacade.RetrieveStockByPartno(GridViewtrans0.Rows[i].Cells[4].Text, GridViewtrans0.Rows[i].Cells[8].Text);
                        Session["lokid1"] = t3serahK.LokID;
                        t3serahK.Flag = "kurang";
                        //t3serah.GroupID = int.Parse(GridViewtrans0.Rows[i].Cells[1].Text);
                        //t3serah.ItemID = int.Parse(GridViewtrans0.Rows[i].Cells[2].Text);
                        //t3serah.ID = t3serah.;
                        //t3serah.LokID = int.Parse(GridViewtrans0.Rows[i].Cells[9].Text);
                        t3serahK.Qty = int.Parse(txtQtyMutasi.Text);
                        t3serahK.CreatedBy = users.UserName;
                        //intresult = SerahFacade.Insert(t3serah);

                        t3serahT = SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi2.Text);
                        //Session["lokid2"] = t3serahT.LokID;
                        t3serahT.Flag = "tambah";
                        //Session["itemid1"] = t3serahT.ItemID;
                        //Session["serahid"] = t3serahT.ID;
                        t3serahT.ItemID = int.Parse(Session["itemid1"].ToString());
                        //t3serah.ID = int.Parse(Session["serahid"].ToString());
                        t3serahT.LokID = int.Parse(Session["lokid2"].ToString());
                        t3serahT.Qty = int.Parse(txtQtyMutasi.Text);
                        t3serahT.CreatedBy = users.UserName;
                        //intresult = SerahFacade.Insert(t3serah);
                        awalID = t3serahT.ID;
                        //rekam table MutasiLok
                        stock2 = SerahFacade.GetStock(int.Parse(Session["lokid2"].ToString()), int.Parse(Session["itemid1"].ToString()));
                        t3mutasilok.SerahID = int.Parse(Session["serahid"].ToString());
                        t3mutasilok.LokID1 = int.Parse(Session["lokid1"].ToString());
                        t3mutasilok.LokID2 = int.Parse(Session["lokid2"].ToString());
                        t3mutasilok.TglML = DateTime.Parse(DatePicker1.Text);
                        t3mutasilok.ItemID = int.Parse(Session["itemid1"].ToString());
                        t3mutasilok.Qty = int.Parse(txtQtyMutasi.Text);
                        t3mutasilok.SA1 = stock;
                        t3mutasilok.SA2 = int.Parse(Session["stock2"].ToString());
                        //MutasiLok.QtyOutSm = Convert.ToInt32(txtQty2.Text);
                        t3mutasilok.CreatedBy = users.UserName;
                        //intresult = MutasiLokFacade.Insert(t3mutasilok);
                        T3_MutasiLokProcessFacade MutasiLokProcessFacade = new T3_MutasiLokProcessFacade(t3serahK, t3serahT, t3mutasilok, int.Parse(GridViewtrans0.Rows[i].Cells[0].Text));
                        string strError = MutasiLokProcessFacade.Insert();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, "Mutasi Lokasi Error");
                            //txtPartnoA.Focus();
                            //clearform();
                            return;
                        }
                    }
                }
                DisplayAJAXMessage(this, "Data tersimpan");
                clearform();

            }
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            //TransferData();
            //clearform();
            //Session["serahID"] = null;
            //Session["lokid1"] = null;
            //Session["lokid2"] = null;
            //Session["lokasi1"] = null;
            //Session["partno1"] = null;
            //Session["itemid1"] = null;
            //Session["GrouID"] = null;
            //Session["hpp1"] = null;
            //Session["stock1"] = null;
            //Session["hpp2"] = null;
            //Session["stock2"] = null;
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridViewMutasiLok();
        }
        private void clearlokasiawal()
        {
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtQtyOut.Text = string.Empty;
            txtPartnoA.Text = string.Empty;
            txtPartnoA.Focus();
        }

        protected void txtPartnoA_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoA.Text.Trim().Length < 10)
                return;
            FC_Items fC_Items = new FC_Items();
            FC_ItemsFacade fC_ItemsFacade = new FC_ItemsFacade();
            if (txtPartnoA.Text.Trim() != string.Empty)
                fC_Items = fC_ItemsFacade.RetrieveByPartNo(txtPartnoA.Text.Trim());
            txtTebal1.Text = fC_Items.Tebal.ToString();
            txtLebar1.Text = fC_Items.Lebar.ToString();
            txtPanjang1.Text = fC_Items.Panjang.ToString();
            txtPartname1.Text = fC_Items.ItemDesc;
            txtPartnoC.Text = txtPartnoA.Text;
            Session["itemid1"] = fC_Items.ID;
            LoadDataGridViewtrans();
            if (fC_Items.Tebal == 0)
            {
                clearlokasiawal();
                return;
            }
            //AutoCompleteExtender4.ContextKey = txtPartnoA.Text;
            LoadDataLoading();
            txtLokasi1.Focus();
        }

        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            if (txtLokasi1.Text.Trim().ToUpper() == "S99")
            {
                txtLokasi1.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                txtLokasi1.Focus();
                return;
            }
            t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
            int qty = t3serah.Qty;
            Session["serahID"] = t3serah.ID;
            Session["GroupID"] = t3serah.GroupID;
            Session["lokid1"] = t3serah.LokID;
            int cekLoading = 0;
            cekLoading = lokasifacde.CekLokasiLoading(txtLokasi1.Text);
            if (cekLoading > 0)
            {
                txtLokasi1.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            Session["lokasi1"] = t3serah.Lokasi;
            Session["partno1"] = t3serah.Partno;
            Session["itemid1"] = t3serah.ItemID;
            Session["hpp1"] = t3serah.HPP;
            Session["stock1"] = t3serah.Qty;
            Session["luas1"] = t3serah.Lebar * t3serah.Panjang;
            txtStock1.Text = qty.ToString();
            txtQtyOut.Text = string.Empty;
            txtQtyOut.Focus();
        }

        protected void GridViewtrans_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pilih")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewtrans.Rows[index];
                clearlokasiawal();
                txtPartnoA.Text = row.Cells[3].Text;
                txtTebal1.Text = row.Cells[5].Text;
                txtLebar1.Text = row.Cells[6].Text;
                txtPanjang1.Text = row.Cells[7].Text;
                txtPartname1.Text = row.Cells[4].Text;
                txtLokasi1.Text = row.Cells[9].Text;
                txtStock1.Text = row.Cells[10].Text;
                txtQtyOut.Text = string.Empty;
                T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
                T3_Serah t3serah = new T3_Serah();
                t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
                int qty = t3serah.Qty;
                Session["serahID"] = t3serah.ID;
                Session["GroupID"] = t3serah.GroupID;
                Session["lokid1"] = t3serah.LokID;
                Session["lokasi1"] = t3serah.Lokasi;
                Session["partno1"] = t3serah.Partno;
                Session["itemid1"] = t3serah.ItemID;
                Session["hpp1"] = t3serah.HPP;
                Session["stock1"] = t3serah.Qty;
                Session["luas1"] = t3serah.Lebar * t3serah.Panjang;
                txtQtyOut.Focus();
            }
        }

        protected void txtPartnoC_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoC.Text.Trim().Length < 10)
                return;
            LoadDataGridViewtrans();
        }

        protected void txtLokasiC_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridViewtrans();
        }

        protected void txtQty1_TextChanged(object sender, EventArgs e)
        {
            if (IsNumeric(txtQtyOut.Text) == false || txtQtyOut.Text.Trim() == "0")
            {
                txtQtyOut.Text = string.Empty;
                return;
            }
            if (txtStock1.Text != string.Empty && Convert.ToInt32(txtStock1.Text) > 0 && Convert.ToInt32(txtQtyOut.Text) <= Convert.ToInt32(txtStock1.Text))
            {
                btnTansfer.Focus();
                //clearform();
            }
            else
            {
                txtQtyOut.Text = string.Empty;
                txtQtyOut.Focus();
            }
            Getfocus();
        }

        protected void txtLokasi2_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            FC_Lokasi lokasi = new FC_Lokasi();
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            lokasi = lokasifacde.RetrieveByLokasi(txtLokasi2.Text);
            int lokid = lokasi.ID;
            if (lokid == 0)
            {
                txtLokasi2.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi belum terdaftar");
                return;
            }
            else
                txtLokasi2.Text = lokasi.Lokasi;
            int cekLoading = 0;
            cekLoading = lokasifacde.CekLokasiLoading(txtLokasi2.Text);
            if (cekLoading > 0)
            {
                txtLokasi2.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi2.Text);
            int qty = t3serah.Qty;
            Session["lokid2"] = lokid;
            Session["hpp2"] = t3serah.HPP;
            Session["stock2"] = t3serah.Qty;
            Session["serahid"] = t3serah.ID;
            Getfocus();
        }

        private void Getfocus()
        {
            if (txtPartnoA.Text == string.Empty)
                txtPartnoA.Focus();
            else
                if (txtLokasi1.Text == string.Empty)
                txtLokasi1.Focus();
            else
                    if (txtLokasi2.Text == string.Empty)
                txtLokasi2.Focus();
            else
                        if (txtQtyOut.Text == string.Empty)
                txtQtyOut.Focus();
            else
            {
                btnTansfer.Focus();
            }
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtLokasi1_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }
        protected void txtQty1_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }
        protected void DatePicker1_SelectionChanged1(object sender, EventArgs e)
        {
            clearform();
            txtPartnoA.Focus();
        }

        protected void ChkHide1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide1.Checked == false)
                Panel8.Visible = false;
            else
                Panel8.Visible = true;
        }
        protected void ChkHide2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide2.Checked == false)
                Panel3.Visible = false;
            else
                Panel3.Visible = true;
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCari.SelectedIndex == 0)
            {
                txtPartnoC.Visible = true;
                txtLokasiC.Visible = false;
                txtPartnoC.Text = string.Empty;
                txtLokasiC.Text = string.Empty;
            }
            else
            {
                txtPartnoC.Visible = false;
                txtLokasiC.Visible = true;
                txtPartnoC.Text = string.Empty;
                txtLokasiC.Text = string.Empty;
            }
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        protected void btnTansfer_Click(object sender, EventArgs e)
        {
            TransferData();
            clearform();
            Session["serahID"] = null;
            Session["lokid1"] = null;
            Session["lokid2"] = null;
            Session["lokasi1"] = null;
            Session["partno1"] = null;
            Session["itemid1"] = null;
            Session["GrouID"] = null;
            Session["hpp1"] = null;
            Session["stock1"] = null;
            Session["hpp2"] = null;
            Session["stock2"] = null;
        }
        protected void ChkLoading_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkLoading.Checked == true)
            {
                LoadDataLoading();
                GridViewtrans0.Visible = true;
                txtLokasi1.Visible = false;
                txtQtyOut.ReadOnly = true;
                txtStock1.Visible = false;
            }
            else
            {
                GridViewtrans0.Visible = false;
                txtLokasi1.Visible = true;
                txtQtyOut.ReadOnly = false;
                txtQtyOut.Text = string.Empty;
                txtStock1.Visible = true;
            }
        }

        protected void ChkMutasi_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[row.RowIndex].FindControl("txtQtyMutasi");
            CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[row.RowIndex].FindControl("chkMutasi");
            int jumlah = 0;

            if (chkMutasi.Checked == true)
            {
                if (txtQtyOut.Text != string.Empty)
                    jumlah = int.Parse(txtQtyOut.Text);
                txtQtyMutasi.Text = GridViewtrans0.Rows[row.RowIndex].Cells[9].Text;
                jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
            }
            else
            {
                //jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                txtQtyMutasi.Text = "0";
                for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
                {
                    txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                    chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                    if (chkMutasi.Checked == false)
                        txtQtyMutasi.Text = string.Empty;
                    if (txtQtyMutasi.Text != string.Empty)
                    {
                        jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                    }
                }
            }
            txtQtyMutasi.Focus();
            txtQtyOut.Text = jumlah.ToString();
        }

        protected void txtQtyOut_TextChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((TextBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[row.RowIndex].FindControl("txtQtyMutasi");
            CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[row.RowIndex].FindControl("chkMutasi");
            int jumlah = 0;
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                if (chkMutasi.Checked == false)
                    txtQtyMutasi.Text = string.Empty;
                if (txtQtyMutasi.Text != string.Empty)
                {
                    if (int.Parse(GridViewtrans0.Rows[i].Cells[9].Text) < int.Parse(txtQtyMutasi.Text))
                    {
                        txtQtyMutasi.Text = string.Empty;
                        chkMutasi.Checked = false;
                    }
                    else
                        jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                }
            }
            txtQtyMutasi.Focus();
            txtQtyOut.Text = jumlah.ToString();
        }
    }
}