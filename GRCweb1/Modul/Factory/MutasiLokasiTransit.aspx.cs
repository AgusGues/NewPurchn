using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using Factory;
using Cogs;

namespace GRCweb1.Modul.Factory
{
    public partial class MutasiLokasiTransit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                TglMutasi.SelectedDate = DateTime.Now.AddDays(-1);
                TglSerah.SelectedDate = DateTime.Now.AddDays(-1);
                LoadLokasiTransit();
                HookOnFocus(this.Page as Control);
                Page.ClientScript.RegisterStartupScript(
                    typeof(MutasiLokasiTransit),
                    "ScriptDoFocus",
                    SCRIPT_DOFOCUS.Replace("REQUEST_LASTFOCUS", Request["__LASTFOCUS"]),
                    true);
            }
        }
        private const string SCRIPT_DOFOCUS =
              @"window.setTimeout('DoFocus()', 1);
            function DoFocus()
            {
                try {
                    document.getElementById('REQUEST_LASTFOCUS').focus();
                } catch (ex) {}
            }";

        private void HookOnFocus(Control CurrentControl)
        {
            //checks if control is one of TextBox, DropDownList, ListBox or Button
            if ((CurrentControl is TextBox) ||
                (CurrentControl is DropDownList) ||
                (CurrentControl is ListBox) ||
                (CurrentControl is Button))
                //adds a script which saves active control on receiving focus 
                //in the hidden field __LASTFOCUS.
                (CurrentControl as WebControl).Attributes.Add("onfocus", "try{document.getElementById('__LASTFOCUS').value=this.id} catch(e) {}");
            //checks if the control has children
            if (CurrentControl.HasControls())
                //if yes do them all recursively
                foreach (Control CurrentChildControl in CurrentControl.Controls)
                    HookOnFocus(CurrentChildControl);
        }
        private void LoadLokasiTransit()
        {
            FC_LokasiFacade FC_Lokasifacade = new FC_LokasiFacade();
            ArrayList arrFC_Lokasi = new ArrayList();
            arrFC_Lokasi = FC_Lokasifacade.RetrieveTransit();
            ddlLokasi.Items.Clear();
            ddlLokasi.Items.Add(new ListItem("-- Pilih Lokasi Transit--", "0"));
            foreach (FC_Lokasi lokasi in arrFC_Lokasi)
            {
                ddlLokasi.Items.Add(new ListItem(lokasi.Lokasi, lokasi.ID.ToString()));
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
            txtQtyOut.Text = string.Empty;
            txtnopalet.Text = string.Empty;

            txtLokasi2.Text = string.Empty;
            LoadDataGridViewtrans();
            LoadDataTransit();
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
            arrT3MutasiLok = T3MutasiLok.RetrieveBytgl(TglMutasi.SelectedDate.ToString("yyyyMMdd"));
            GridViewMutasiLok.DataSource = arrT3MutasiLok;
            GridViewMutasiLok.DataBind();
        }

        private void LoadDataTransit()
        {
            ArrayList arrT1Serah = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();
            decimal tebal = 0;
            decimal lebar = 0;
            decimal panjang = 0;
            int range = 0;
            if (TglSerah.Visible == true)
                range = 1;
            else
                range = 0;
            if (txtTebal1.Text != string.Empty)
            {
                tebal = Convert.ToDecimal(txtTebal1.Text);
                lebar = Convert.ToDecimal(txtLebar1.Text);
                panjang = Convert.ToDecimal(txtPanjang1.Text);
            }
            if (ddlLokasi.SelectedItem.Text.Trim().ToUpper() == "B99")
                arrT1Serah = Serah.RetrieveStockTransitV(ddlLokasi.SelectedItem.Text, TglSerah.SelectedDate.ToString("yyyyMMdd"), range, tebal, lebar, panjang, txtnopalet.Text.Trim());
            else
                arrT1Serah = Serah.RetrieveStockTransit(ddlLokasi.SelectedItem.Text, TglSerah.SelectedDate.ToString("yyyyMMdd"), range, tebal, txtnopalet.Text.Trim());
            GridViewtrans0.DataSource = arrT1Serah;
            GridViewtrans0.DataBind();
            txtQtyOut.Text = string.Empty;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }

        protected void TransferData()
        {
            Users users = (Users)Session["Users"];
            T3_RekapFacade rekapFacade = new T3_RekapFacade();
            T1_SerahFacade T1serahF = new T1_SerahFacade();
            T1_Serah T1serah = new T1_Serah();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_GroupsFacade groupsFacade = new T3_GroupsFacade();
            FC_Items items = new FC_Items();
            FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
            T3_Serah t3serah = new T3_Serah();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            int intResult = 0;
            decimal AvgHPP = 0;
            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = TglMutasi.SelectedDate.Year;
            int Bulan = TglMutasi.SelectedDate.Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            #region Verifikasi lokasi tujuan
            if (txtLokasi2.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Lokasi tujuan belum ditentukan");

                return;
            }
            #endregion
            #region Verifikasi lokasi siap kirim
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            int cekLoading = 0;
            cekLoading = lokasifacde.CekLokasiLoading(txtLokasi2.Text);
            if (cekLoading > 0)
            {
                txtLokasi2.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            #endregion
            #region verifikasi lokasi penyimpanan dan Partno
            int LokasiID = dest.GetLokID(txtLokasi2.Text);
            if (LokasiID == 0)
            {
                DisplayAJAXMessage(this, "Lokasi Penyimpanan tidak ditemukan");
                return;
            }
            if (ddlLokasi.SelectedItem.Text.Trim().ToUpper() == "P99")
            {
                items = itemsfacade.RetrieveByPartNo(txtPartnoA.Text);
                if (items.ID == 0)
                {
                    DisplayAJAXMessage(this, "Partno tidak ditemukan");
                    return;
                }
            }
            #endregion
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                if (txtQtyMutasi.Text != string.Empty)
                {
                    T1_Serah serah = new T1_Serah();
                    T1_SerahFacade serahF = new T1_SerahFacade();
                    t3serah = SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi2.Text);
                    if (ddlLokasi.SelectedItem.Text.Trim().ToUpper() != "P99")
                        //items = itemsfacade.RetrieveByPartNo(GridViewtrans0.Rows[i].Cells[8].Text);
                        items = itemsfacade.RetrieveByPartNo(txtPartnoA.Text);
                    serah = serahF.RetrieveByID(Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text));
                    if (serah.QtyIn - serah.QtyOut - Convert.ToInt32(txtQtyMutasi.Text) < 0)
                    {
                        DisplayAJAXMessage(this, "Qty terima lebih besar dari qty serah");
                        break;
                    }
                    T3_Rekap rekap = new T3_Rekap();
                    rekap.DestID = serah.DestID;
                    rekap.SerahID = t3serah.ID;
                    rekap.Keterangan = serah.PartnoDest;
                    rekap.T1serahID = serah.ID;
                    rekap.LokasiID = LokasiID;
                    rekap.ItemIDSer = items.ID;
                    rekap.T1sItemID = serah.ItemIDSer;
                    rekap.TglTrm = TglMutasi.SelectedDate;
                    rekap.QtyInTrm = int.Parse(txtQtyMutasi.Text);
                    rekap.T1SLokID = serah.LokasiID;
                    rekap.QtyOutTrm = 0;
                    rekap.SA = t3serah.Qty;
                    rekap.CreatedBy = users.UserName;
                    if (int.Parse(txtQtyMutasi.Text) > 0 && serah.HPP > 0)
                        AvgHPP = ((t3serah.HPP * t3serah.Qty) + (int.Parse(txtQtyMutasi.Text) * serah.HPP)) / (t3serah.Qty + int.Parse(txtQtyMutasi.Text));
                    else
                        AvgHPP = t3serah.HPP;
                    rekap.HPP = AvgHPP;
                    rekap.GroupID = items.GroupID;
                    rekap.CreatedBy = users.UserName;
                    rekap.Process = "Direct";
                    serah.QtyOut = int.Parse(txtQtyMutasi.Text);

                    //proses Update Stock
                    t3serah.Flag = "tambah";
                    //if (txtPartnoA.Text.IndexOf("-S-") != -1)
                    t3serah.ItemID = items.ID;
                    t3serah.ID = t3serah.ID;
                    t3serah.GroupID = items.GroupID;
                    t3serah.LokID = rekap.LokasiID;
                    t3serah.Qty = int.Parse(txtQtyMutasi.Text);
                    t3serah.HPP = rekap.HPP;
                    t3serah.CreatedBy = users.UserName;
                    //intResult = SerahFacade.Insert(t3serah);
                    //end Update Stock
                    if (t3serah.ID == 0)
                        rekap.SerahID = intResult;
                    else
                        rekap.SerahID = t3serah.ID;
                    TerimaProcessFacade TerimaProcessFacade = new TerimaProcessFacade(t3serah, rekap);
                    string strError = TerimaProcessFacade.Insert();
                    if (strError == string.Empty)
                    {
                        if (serah.ItemIDSer > 0 && Convert.ToInt32(txtQtyMutasi.Text) > 0)
                        {
                            //intResult = rekapFacade.Insert(rekap);
                            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                            transManager.BeginTransaction();
                            serah.CreatedBy = users.UserName;
                            AbstractTransactionFacadeF abstrans = new T1_SerahFacade(serah);
                            intResult = abstrans.Update(transManager);
                            if (abstrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                break;
                            }
                            transManager.CommitTransaction();
                            transManager.CloseConnection();
                            string tgltrans = TglMutasi.SelectedDate.ToString("MM/dd/yyyy");
                            if (ddlLokasi.SelectedItem.Text.Trim().ToUpper() == "P99")
                            {
                                FC_LokasiFacade lokasiF = new FC_LokasiFacade();
                                int lokIDLari = lokasiF.GetID("C99");
                                //strError = T1serahF.UpdatePelarian(serah.JemurID, serah.ID, lokIDLari, items.ID, tgltrans);

                                if (txtQtyMutasi.Text.Trim() == GridViewtrans0.Rows[i].Cells[10].Text.Trim())
                                    strError = T1serahF.UpdateserahBypelarianFull(serah.JemurID, serah.ID, lokIDLari,
                                     items.ID, Convert.ToInt32(txtQtyMutasi.Text), tgltrans);
                                else
                                    strError = T1serahF.UpdateserahBypelarianPartial(serah.JemurID, serah.ID, lokIDLari, items.ID,
                                    Convert.ToInt32(txtQtyMutasi.Text), tgltrans);
                            }
                            DisplayAJAXMessage(this, "Data tersimpan");
                        }
                    }
                }
            }
            DisplayAJAXMessage(this, "Data tersimpan");
            clearform();
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
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

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridViewMutasiLok();
            LoadDataTransit();
        }
        private void clearlokasiawal()
        {
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
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
            txtPartnoC.Text = txtPartnoA.Text;
            Session["itemid1"] = fC_Items.ID;
            LoadDataGridViewtrans();
            if (fC_Items.Tebal == 0)
            {
                clearlokasiawal();
                return;
            }
            LoadDataTransit();
        }

        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            //t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
            int qty = t3serah.Qty;
            Session["serahID"] = t3serah.ID;
            Session["GroupID"] = t3serah.GroupID;
            Session["lokid1"] = t3serah.LokID;
            int cekLoading = 0;
            //cekLoading = lokasifacde.CekLokasiLoading(txtLokasi1.Text);
            if (cekLoading > 0)
            {
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            Session["lokasi1"] = t3serah.Lokasi;
            Session["partno1"] = t3serah.Partno;
            Session["itemid1"] = t3serah.ItemID;
            Session["hpp1"] = t3serah.HPP;
            Session["stock1"] = t3serah.Qty;
            Session["luas1"] = t3serah.Lebar * t3serah.Panjang;
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
                txtQtyOut.Text = string.Empty;
                T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
                T3_Serah t3serah = new T3_Serah();
                //t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
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

        protected void ChkMutasi_CheckedChanged(object sender, EventArgs e)
        {

            GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[row.RowIndex].FindControl("txtQtyMutasi");
            CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[row.RowIndex].FindControl("chkMutasi");
            int jumlah = 0;
            if (ddlLokasi.SelectedItem.Text.Trim().ToUpper() == "P99")
            {
                if (txtPartnoA.Text == string.Empty && txtTebal1.Text == string.Empty)
                {

                    DisplayAJAXMessage(this, "Partno Tujuan belum ditentukan");
                    return;
                }
                else
                {
                    if (Convert.ToDouble(txtTebal1.Text) != Convert.ToDouble(GridViewtrans0.Rows[row.RowIndex].Cells[5].Text) && Convert.ToDouble(GridViewtrans0.Rows[row.RowIndex].Cells[5].Text) > 4)
                    {
                        DisplayAJAXMessage(this, "Ketebalan produk tidak sama");
                        chkMutasi.Checked = false;
                        return;
                    }
                }
            }
            if (chkMutasi.Checked == true)
            {
                if (txtQtyOut.Text != string.Empty)
                    jumlah = int.Parse(txtQtyOut.Text);
                txtQtyMutasi.Text = GridViewtrans0.Rows[row.RowIndex].Cells[10].Text;
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
                    if (int.Parse(GridViewtrans0.Rows[i].Cells[10].Text) < int.Parse(txtQtyMutasi.Text))
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
        protected void ddlLokasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataTransit();
        }
        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAll.Checked == true)
                TglSerah.Visible = false;
            else
                TglSerah.Visible = true;
        }
        protected void txtNoPalet_TextChanged(object sender, EventArgs e)
        {

        }
    }
}