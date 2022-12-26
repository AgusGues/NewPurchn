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
    public partial class MutasiLokasiPelarian : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadPartnoPelarian();
                LoadUkuran();
                T1_PaletPelarianFacade ppf = new T1_PaletPelarianFacade();
                int countP = ppf.GetCount(DateTime.Parse(txtTanggal.Text).ToString("ddMMyy"));
                txtnopalet.Text = DateTime.Parse(txtTanggal.Text).ToString("ddMMyy") + (countP + 1).ToString().PadLeft(2, '0');
            }
        }

        private void LoadPartnoPelarian()
        {
            FC_ItemsFacade Itemf = new FC_ItemsFacade();
            ArrayList arrItems = new ArrayList();
            arrItems = Itemf.RetrieveByPelarian();
            //
            ddlPartno.Items.Clear();
            ddlPartno.Items.Add(new ListItem("", "0"));
            foreach (FC_Items items in arrItems)
            {
                ddlPartno.Items.Add(new ListItem(items.Partno, items.ID.ToString()));
            }
        }
        private void clearform()
        {
            LoadDataTransit();
            txtQtyOut.Text = string.Empty;
            txtnopalet.Text = string.Empty;
            txtPartnoOK.Text = string.Empty;
            txtlokOK.Text = string.Empty;
            txtPartnoBP.Text = string.Empty;
            txtLokBP.Text = string.Empty;
            LoadDataGridViewMutasiLok();
        }
        private void LoadDataGridViewMutasiLok()
        {
            ArrayList arrT3MutasiLok = new ArrayList();
            T3_MutasiLokFacade T3MutasiLok = new T3_MutasiLokFacade();
            //arrT3MutasiLok = T3MutasiLok.RetrieveBytgl(TglMutasi.SelectedDate.ToString("yyyyMMdd"));
            GridViewMutasiLok.DataSource = arrT3MutasiLok;
            GridViewMutasiLok.DataBind();
        }

        private void LoadDataTransit()
        {
            ArrayList arrT1Serah = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();
            arrT1Serah = Serah.RetrieveStockPelarian(ddlPartno.SelectedItem.Text);
            GridViewtrans0.DataSource = arrT1Serah;
            GridViewtrans0.DataBind();
            txtQtyOut.Text = string.Empty;
            int total = 0;
            T1_PaletPelarianFacade ppf = new T1_PaletPelarianFacade();
            int countP = ppf.GetCount(DateTime.Parse(txtTanggal.Text).ToString("ddMMyy"));
            txtnopalet.Text = DateTime.Parse(txtTanggal.Text).ToString("ddMMyy") + (countP + 1).ToString().PadLeft(2, '0');
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                chkMutasi.Checked = true;
                txtQtyMutasi.Text = (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text)).ToString();
                total = total + (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
            }
            txtQtyOut.Text = total.ToString();
            txtQtyOut.Focus();
        }
        private void LoadDataTransitkriteria()
        {
            ArrayList arrT1Serah = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();
            string kriteria = string.Empty;
            string ukuran = string.Empty;
            if (ddlUkuran.SelectedIndex > 0 && ChkUkuran.Checked == true)
            {
                ukuran = ddlUkuran.SelectedItem.Text.Substring(0, 4) + ddlUkuran.SelectedItem.Text.Substring(7, 4);
                kriteria = " and C.PartNo like '%" + ukuran + "%' ";
            }
            if (txtTglPelarian.Text.Trim() != string.Empty && ChkTgl.Checked == true)
                kriteria = kriteria + " and convert(char,A.TglSerah,112)='" + DateTime.Parse(txtTglPelarian.Text).ToString("yyyyMMdd") + "' ";
            arrT1Serah = Serah.RetrieveStockPelarianByKriteria(kriteria);
            GridViewtrans0.DataSource = arrT1Serah;
            GridViewtrans0.DataBind();
            txtQtyOut.Text = string.Empty;
            int total = 0;
            T1_PaletPelarianFacade ppf = new T1_PaletPelarianFacade();
            int countP = ppf.GetCount(DateTime.Parse(txtTanggal.Text).ToString("ddMMyy"));
            txtnopalet.Text = DateTime.Parse(txtTanggal.Text).ToString("ddMMyy") + (countP + 1).ToString().PadLeft(2, '0');
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                chkMutasi.Checked = true;
                txtQtyMutasi.Text = (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text)).ToString();
                total = total + (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
            }
            txtQtyOut.Text = total.ToString();
            txtQtyOut.Focus();
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
            T1_PaletPelarian paletP = new T1_PaletPelarian();
            T1_PaletPelarianFacade paletPF = new T1_PaletPelarianFacade();
            int intResult = 0;
            decimal AvgHPP = 0;
            #region Verifikasi Closing Periode
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToInt32(DateTime.Parse(txtTanggal.Text).ToString("yyyy"));
            int Bulan = Convert.ToInt32(DateTime.Parse(txtTanggal.Text).ToString("MM"));
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            #region Verifikasi lokasi tujuan
            FC_Lokasi lokasiOK = new FC_Lokasi();
            FC_LokasiFacade lokasiOKF = new FC_LokasiFacade();
            FC_Lokasi lokasiBP = new FC_Lokasi();
            FC_LokasiFacade lokasiBPF = new FC_LokasiFacade();
            if (RBOK.Checked == true)
            {
                lokasiOK = lokasiOKF.RetrieveByLokasi(txtlokOK.Text);
                if (lokasiOK.ID == 0)
                {
                    DisplayAJAXMessage(this, "Lokasi tujuan Produk OK belum tersedia");
                    return;
                }
            }
            else
            {
                lokasiBP = lokasiBPF.RetrieveByLokasi(txtLokBP.Text);
                if (lokasiBP.ID == 0)
                {
                    DisplayAJAXMessage(this, "Lokasi tujuan Produk BP belum tersedia");
                    return;
                }
            }
            #endregion


            #region Create Palet Pelarian
            //int intResult1 = 0;
            //TglPotong, NoPalet, PartnoAsal, QtyAsal, PartnoOK, QtyOK, PartnoBP, QtyBP
            paletP.TglPotong = DateTime.Parse(txtTanggal.Text);
            paletP.NoPalet = txtnopalet.Text;
            paletP.PartnoAsal = ddlPartno.SelectedItem.Text.Trim();
            paletP.QtyAsal = Convert.ToInt32(txtQtyOut.Text);
            if (RBOK.Checked == true)
                paletP.PartnoOK = txtPartnoOK.Text;
            if (RBBP.Checked == true)
                paletP.PartnoBP = txtPartnoBP.Text;
            paletP.CreatedBy = users.UserName.Trim();
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            AbstractTransactionFacadeF absTrans = new T1_PaletPelarianFacade(paletP);
            intResult = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            #endregion
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                if (txtQtyMutasi.Text != string.Empty)
                {
                    #region Proses serah terima dataOK
                    T1_Serah serah = new T1_Serah();
                    T1_SerahFacade serahF = new T1_SerahFacade();
                    t3serah = SerahFacade.RetrieveStockByPartno(txtPartnoOK.Text, txtlokOK.Text);
                    if (RBOK.Checked == true)
                        items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                    else
                        items = itemsfacade.RetrieveByPartNo(txtPartnoBP.Text);
                    serah = serahF.RetrieveByID(Convert.ToInt32(GridViewtrans0.Rows[i].Cells[0].Text));
                    //if (serah.QtyIn - serah.QtyOut - Convert.ToInt32(txtQtyMutasi.Text) < 0)
                    //{
                    //    DisplayAJAXMessage(this, "Qty terima lebih besar dari qty serah");
                    //    break;
                    //}
                    T3_Rekap rekap = new T3_Rekap();
                    rekap.DestID = serah.DestID;
                    rekap.SerahID = t3serah.ID;
                    rekap.Keterangan = serah.PartnoDest;
                    rekap.T1serahID = serah.ID;
                    if (RBOK.Checked == true)
                        rekap.LokasiID = lokasiOK.ID;
                    else
                        rekap.LokasiID = lokasiBP.ID;
                    rekap.ItemIDSer = items.ID;
                    rekap.T1sItemID = serah.ItemIDSer;
                    rekap.TglTrm = DateTime.Parse(txtTanggal.Text);
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
                    //t3serah.ID = t3serah.ID;
                    t3serah.GroupID = items.GroupID;
                    t3serah.LokID = rekap.LokasiID;
                    t3serah.Qty = int.Parse(txtQtyMutasi.Text);
                    t3serah.HPP = rekap.HPP;
                    t3serah.CreatedBy = users.UserName;
                    if (t3serah.ID == 0)
                        rekap.SerahID = intResult;
                    else
                        rekap.SerahID = t3serah.ID;
                    absTrans = new T3_SerahFacade(t3serah);
                    intResult = absTrans.Insert(transManager);
                    if (absTrans.Error != string.Empty)
                    {
                        transManager.RollbackTransaction();
                        return;
                    }
                    if (intResult > 0)
                    {
                        rekap.SerahID = intResult;
                        absTrans = new T3_RekapFacade(rekap);
                        intResult = absTrans.Insert(transManager);
                        if (absTrans.Error != string.Empty)
                        {
                            transManager.RollbackTransaction();
                            return;
                        }
                    }
                    string strError = string.Empty;
                    #endregion
                    TransactionManager transManager1 = new TransactionManager(Global.ConnectionString());
                    transManager1.BeginTransaction();
                    if (intResult > 0)
                    {
                        if (serah.ItemIDSer > 0 && Convert.ToInt32(txtQtyMutasi.Text) > 0)
                        {
                            //intResult = rekapFacade.Insert(rekap);
                            serah.CreatedBy = users.UserName;
                            AbstractTransactionFacadeF abstrans1 = new T1_SerahFacade(serah);
                            intResult = abstrans1.Update(transManager1);
                            if (abstrans1.Error != string.Empty)
                            {
                                transManager1.RollbackTransaction();
                                return;
                            }
                            transManager1.CommitTransaction();
                            transManager1.CloseConnection();
                            string tgltrans = DateTime.Parse(txtTanggal.Text).ToString("MM/dd/yyyy");
                            FC_LokasiFacade lokasiF = new FC_LokasiFacade();
                            int lokIDLari = lokasiF.GetID("C99");
                            if (txtQtyMutasi.Text.Trim() == GridViewtrans0.Rows[i].Cells[7].Text.Trim())
                                strError = T1serahF.UpdateserahBypelarianFullNew(serah.JemurID, serah.ID, lokIDLari,
                                 items.ID, Convert.ToInt32(txtQtyMutasi.Text), tgltrans, txtnopalet.Text);
                            else
                                strError = T1serahF.UpdateserahBypelarianPartialNew(serah.JemurID, serah.ID, lokIDLari, items.ID,
                                Convert.ToInt32(txtQtyMutasi.Text), tgltrans, txtnopalet.Text);
                            //if (strError != string.Empty)
                            //{
                            //    transManager1.RollbackTransaction();
                            //    return;
                            //}
                        }

                    }
                }
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
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
        private void clearlokasiawal()
        {
            txtQtyOut.Text = string.Empty;
            //txtPartnoA.Text = string.Empty;
            //txtPartnoA.Focus();
        }



        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            int qty = t3serah.Qty;
            Session["serahID"] = t3serah.ID;
            Session["GroupID"] = t3serah.GroupID;
            Session["lokid1"] = t3serah.LokID;
            int cekLoading = 0;
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
        protected void txtLokasiC_TextChanged(object sender, EventArgs e)
        {
        }

        protected void txtQty1_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            #region viewgrid
            ArrayList arrT1Serah = new ArrayList();
            ArrayList arrT1Serah1 = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();
            int totalmutasi = Convert.ToInt32(txtQtyOut.Text);
            string palet = string.Empty;
            arrT1Serah = Serah.RetrieveStockPelarian(ddlPartno.SelectedItem.Text);
            int totalarray = 0;
            foreach (T1_Serah t1serah in arrT1Serah)
            {
                totalarray = totalarray + t1serah.QtyIn;
                arrT1Serah1.Add(t1serah);
                if (totalarray > totalmutasi)
                    break;
            }
            GridViewtrans0.DataSource = arrT1Serah1;
            GridViewtrans0.DataBind();
            #endregion
            int jumlah = Convert.ToInt32(txtQtyOut.Text);
            int total = 0;

            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                if (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text) < jumlah)
                {
                    chkMutasi.Checked = true;
                    txtQtyMutasi.Text = (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text)).ToString();
                    jumlah = jumlah - (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
                    total = total + (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
                }
                else
                {
                    chkMutasi.Checked = true;
                    txtQtyMutasi.Text = jumlah.ToString();
                    total = total + jumlah;
                    break;
                }
            }
            txtQtyOut.Text = total.ToString();
            if (total == 0)
            {
                txtQtyOut.Focus();
                return;
            }
            txtPartnoOK.Focus();
            //}
            //catch { }
        }


        private void Getfocus()
        {
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
            //txtPartnoA.Focus();
        }
        protected void ChkHide1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide1.Checked == false)
                Panel8.Visible = false;
            else
                Panel8.Visible = true;
        }
        static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }


        protected void ChkMutasi_CheckedChanged(object sender, EventArgs e)
        {

            GridViewRow row = ((CheckBox)sender).Parent.Parent as GridViewRow;
            TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[row.RowIndex].FindControl("txtQtyMutasi");
            CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[row.RowIndex].FindControl("chkMutasi");
            int jumlah = 0;
            int lastindex = 0;
            if (IsNumeric(txtQtyOut.Text) == true)
                jumlah = Convert.ToInt32(txtQtyOut.Text);
            else
                txtQtyOut.Text = "0";
            if (chkMutasi.Checked == true)
            {
                jumlah = int.Parse(txtQtyOut.Text);
                txtQtyMutasi.Text = GridViewtrans0.Rows[row.RowIndex].Cells[7].Text;
                jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                lastindex = row.RowIndex;
            }
            else
            {
                //jumlah = jumlah + int.Parse(txtQtyMutasi.Text);
                jumlah = int.Parse(txtQtyOut.Text);
                txtQtyMutasi.Text = GridViewtrans0.Rows[row.RowIndex].Cells[7].Text;
                jumlah = jumlah - int.Parse(txtQtyMutasi.Text);
                txtQtyMutasi.Text = string.Empty;
                lastindex = row.RowIndex;
            }
            GridViewtrans0.SelectedIndex = lastindex;
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
                    if (int.Parse(GridViewtrans0.Rows[i].Cells[7].Text) < int.Parse(txtQtyMutasi.Text))
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
        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
        }
        protected void txtNoPalet_TextChanged(object sender, EventArgs e)
        {
        }
        protected void ddlBulan_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPartnoPelarian();
        }
        protected void txtPartNo_TextChanged(object sender, EventArgs e)
        {

        }
        protected void ddTahun_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPartnoPelarian();
        }
        protected void ddlPartno_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataTransit();
            if (ddlPartno.SelectedItem.Text.Trim() != string.Empty)
            {
                string partnoAsal = ddlPartno.SelectedItem.Text.Trim();
                txtPartnoOK.Text = partnoAsal.Substring(0, 4) + "3" + partnoAsal.Substring(5, 6);
                txtPartnoBP.Text = partnoAsal.Substring(0, 4) + "P" + partnoAsal.Substring(5, 6);
            }
            int total = 0;
            T1_PaletPelarianFacade ppf = new T1_PaletPelarianFacade();
            int countP = ppf.GetCount(DateTime.Parse(txtTanggal.Text).ToString("ddMMyy"));
            txtnopalet.Text = DateTime.Parse(txtTanggal.Text).ToString("ddMMyy") + (countP + 1).ToString().PadLeft(2, '0');
            for (int i = 0; i <= GridViewtrans0.Rows.Count - 1; i++)
            {
                TextBox txtQtyMutasi = (TextBox)GridViewtrans0.Rows[i].FindControl("txtQtyMutasi");
                CheckBox chkMutasi = (CheckBox)GridViewtrans0.Rows[i].FindControl("chkMutasi");
                chkMutasi.Checked = true;
                txtQtyMutasi.Text = (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text)).ToString();
                total = total + (Convert.ToInt32(GridViewtrans0.Rows[i].Cells[7].Text));
            }
            txtQtyOut.Text = total.ToString();
            txtQtyOut.Focus();
        }

        protected void txtPartnoOK_TextChanged(object sender, EventArgs e)
        {
            txtlokOK.Focus();
        }
        protected void txtlokOK_TextChanged(object sender, EventArgs e)
        {
            txtPartnoBP.Focus();
        }
        protected void txtPartnoBP_TextChanged(object sender, EventArgs e)
        {
            txtLokBP.Focus();
        }
        protected void txtTanggal_TextChanged(object sender, EventArgs e)
        {

        }
        protected void RBOK_CheckedChanged(object sender, EventArgs e)
        {
            txtPartnoOK.Visible = true;
            txtlokOK.Visible = true;
            LabelOK.Visible = true;
            txtPartnoBP.Visible = false;
            txtLokBP.Visible = false;
            LabelOK0.Visible = false;
        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtPartnoOK.Visible = false;
            txtlokOK.Visible = false;
            LabelOK.Visible = false;
            txtPartnoBP.Visible = true;
            txtLokBP.Visible = true;
            LabelOK0.Visible = true;
        }
        protected void ChkUkuran_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkUkuran.Checked == true)
            {
                ddlUkuran.Visible = true;
                ChkPartno.Checked = false;
                ddlPartno.Visible = false;
            }
            else
                ddlUkuran.Visible = false;
            LoadDataTransitkriteria();
        }
        protected void ChkTgl_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTgl.Checked == true)
            {
                txtTglPelarian.Visible = true;
                ChkPartno.Checked = false;
                ddlPartno.Visible = false;
            }
            else
                txtTglPelarian.Visible = false;
            LoadDataTransitkriteria();
        }
        private void LoadUkuran()
        {
            FC_ItemsFacade Itemf = new FC_ItemsFacade();
            ArrayList arrItems = new ArrayList();
            arrItems = Itemf.RetrieveByUkuranPelarian();
            //
            ddlUkuran.Items.Clear();
            ddlUkuran.Items.Add(new ListItem("", "0"));
            foreach (FC_Items items in arrItems)
            {
                ddlUkuran.Items.Add(new ListItem(items.Ukuran, items.Ukuran));
            }
        }

        protected void ChkPartno_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPartno.Checked == true)
            {
                ddlPartno.Visible = true;
                ChkUkuran.Checked = false;
                ddlUkuran.Visible = false;
                ChkTgl.Checked = false;
                txtTglPelarian.Visible = false;
                LoadDataTransit();
            }
            else
                ddlPartno.Visible = false;

        }
        protected void ddlUkuran_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataTransitkriteria();
        }
        protected void txtTglPelarian_TextChanged(object sender, EventArgs e)
        {
            LoadDataTransitkriteria();
            T1_PaletPelarianFacade ppf = new T1_PaletPelarianFacade();
            int countP = ppf.GetCount(DateTime.Parse(txtTglPelarian.Text).ToString("ddMMyy"));
            txtnopalet.Text = DateTime.Parse(txtTglPelarian.Text).ToString("ddMMyy") + (countP + 1).ToString().PadLeft(2, '0');
        }
    }
}