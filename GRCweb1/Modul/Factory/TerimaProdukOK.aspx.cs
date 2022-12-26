using BusinessFacade;
using Cogs;
using DataAccessLayer;
using Domain;
using Factory;
using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GRCweb1.Modul.Factory
{
    public partial class TerimaProdukOK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtDateSerah.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                txtDateProduksi.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                txtDateTrm.Text = txtDateSerah.Text;
                LoadGroups();
                //LoadTransit();
                LoadDataGridSerah();
                LoadDataGridTerima();
            }
        }

        private void LoadGroups()
        {
            ddlGroups.Items.Clear();
            ArrayList arrGroups = new ArrayList();
            T3_GroupsFacade groupsFacade = new T3_GroupsFacade();
            arrGroups = groupsFacade.Retrieve();
            ddlGroups.Items.Add(new ListItem("-- Pilih Group Marketing --", "0"));
            try
            {
                foreach (T3_Groups T3Groups in arrGroups)
                {
                    ddlGroups.Items.Add(new ListItem(T3Groups.Groups, T3Groups.ID.ToString()));
                }
            }
            catch { }
        }

        protected void btnTansfer_LostFocus(object sender, EventArgs e)
        {
            if (txtDateSerah.Enabled == true)
                txtDateSerah.Focus();
            else
                txtDateProduksi.Focus();
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            T3_RekapFacade rekapFacade = new T3_RekapFacade();
            T1_SerahFacade serahFacade = new T1_SerahFacade();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_GroupsFacade groupsFacade = new T3_GroupsFacade();
            ArrayList arrSerah = (ArrayList)Session["arrSerah"];
            Users users = (Users)Session["Users"];
            int intResult = 0;
            int i = 0;
            int maxtrans = 0;
            int checktrans = 0;
            decimal AvgHPP = 0;

            //try
            //{
            foreach (T1_Serah serah in arrSerah)
            {
                TextBox txtQtyTrm = (TextBox)GridViewSerah.Rows[i].FindControl("txtQtyTrm");
                TextBox txtLokTrm = (TextBox)GridViewSerah.Rows[i].FindControl("txtLokTrm");
                TextBox txtPartnoOK = (TextBox)GridViewSerah.Rows[i].FindControl("txtPartnoOK");
                Label LMarketing = (Label)GridViewSerah.Rows[i].FindControl("LMarketing");
                FC_Items items = new FC_Items();
                FC_ItemsFacade itemsfacade = new FC_ItemsFacade();
                T3_Serah t3serah = new T3_Serah();
                T3_SerahFacade SerahFacade = new T3_SerahFacade();
                if (LMarketing.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Group Marketing belum ditentukan");
                    break;
                }
                if (serah.QtyIn - serah.QtyOut - Convert.ToInt32(txtQtyTrm.Text) < 0)
                {
                    DisplayAJAXMessage(this, "Qty terima lebih besar dari qty serah");
                    break;
                }

                #region Verifikasi Closing Periode

                /**
                     * check closing periode saat ini
                     * added on 13-08-2014
                     */
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = DateTime.Parse(txtDateTrm.Text).Year;
                int Bulan = DateTime.Parse(txtDateTrm.Text).Month;
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun +
                 " sudah Closing. Transaksi Tidak bisa dilakukan");
                    return;
                }

                #endregion Verifikasi Closing Periode

                items = itemsfacade.RetrieveByPartNo(txtPartnoOK.Text);
                t3serah = SerahFacade.RetrieveStockByPartno(txtPartnoOK.Text, txtLokTrm.Text);
                AvgHPP = 0;
                if (txtQtyTrm.Text == string.Empty)
                    txtQtyTrm.Text = "0";
                maxtrans = serah.QtyIn - serah.QtyOut;
                checktrans = Convert.ToInt32(txtQtyTrm.Text);
                T3_Rekap rekap = new T3_Rekap();
                if (txtLokTrm.Text != string.Empty && txtQtyTrm.Text != string.Empty && maxtrans >= checktrans && checktrans > 0)
                {
                    if (items.ID == 0)
                    {
                        DisplayAJAXMessage(this, "Partno tidak ditemukan");
                        break;
                    }
                    rekap.DestID = serah.DestID;
                    rekap.SerahID = t3serah.ID;
                    rekap.Keterangan = serah.PartnoDest;
                    rekap.T1serahID = serah.ID;
                    rekap.LokasiID = dest.GetLokID(txtLokTrm.Text);
                    if (rekap.LokasiID == 0)
                    {
                        DisplayAJAXMessage(this, "Lokasi Penyimpanan tidak ditemukan");
                        break;
                    }

                    FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
                    int cekLoading = 0;
                    cekLoading = lokasifacde.CekLokasiLoading(txtLokTrm.Text);
                    if (cekLoading > 0)
                    {
                        txtLokTrm.Text = string.Empty;
                        DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                        return;
                    }
                    rekap.ItemIDSer = items.ID;
                    rekap.T1sItemID = serah.ItemIDSer;
                    rekap.TglTrm = DateTime.Parse(txtDateTrm.Text);
                    rekap.QtyInTrm = int.Parse(txtQtyTrm.Text);
                    rekap.T1SLokID = serah.LokasiID;
                    rekap.QtyOutTrm = 0;
                    rekap.SA = t3serah.Qty;
                    if (int.Parse(txtQtyTrm.Text) > 0 && serah.HPP > 0)
                        AvgHPP = ((t3serah.HPP * t3serah.Qty) + (int.Parse(txtQtyTrm.Text) * serah.HPP)) / (t3serah.Qty + int.Parse(txtQtyTrm.Text));
                    else
                        AvgHPP = t3serah.HPP;
                    rekap.HPP = AvgHPP;
                    rekap.GroupID = groupsFacade.GetID(LMarketing.Text);
                    rekap.CreatedBy = users.UserName;
                    rekap.Process = "Direct";
                    serah.QtyOut = int.Parse(txtQtyTrm.Text);

                    //proses Update Stock
                    t3serah.Flag = "tambah";
                    t3serah.ItemID = items.ID;
                    t3serah.ID = t3serah.ID;
                    t3serah.GroupID = Convert.ToInt32(ddlGroups.SelectedValue);
                    t3serah.LokID = rekap.LokasiID;
                    t3serah.Qty = int.Parse(txtQtyTrm.Text);
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
                        if (serah.ItemIDSer > 0 && Convert.ToInt32(txtQtyTrm.Text) > 0)
                        {
                            //intResult = rekapFacade.Insert(rekap);
                            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
                            transManager.BeginTransaction();
                            AbstractTransactionFacadeF abstrans = new T1_SerahFacade(serah);

                            intResult = abstrans.Update(transManager);
                            if (abstrans.Error != string.Empty)
                            {
                                transManager.RollbackTransaction();
                                break;
                            }
                            transManager.CommitTransaction();
                            transManager.CloseConnection();
                            DisplayAJAXMessage(this, "Data tersimpan");
                        }
                    }
                }
                //else
                //{
                //    arrSerah.RemoveAt(i);
                //}
                i = i + 1;
            }
            GridViewSerah.DataSource = arrSerah;
            GridViewSerah.DataBind();
            //}
            //catch
            //{
            //    return;
            //}

            clearform();
            LoadTransit();
            if (txtDateSerah.Enabled == true)
                txtDateSerah.Focus();
            else
                txtDateProduksi.Focus();
        }

        private void clearform()
        {
            LoadDataGridSerah();
            LoadDataGridTerima();
        }

        private void LoadDataGridSerah()
        {
            ArrayList arrSerah = new ArrayList();
            T1_SerahFacade Serah = new T1_SerahFacade();
            string lokasi = string.Empty;
            string tglserah = DateTime.Parse(txtDateSerah.Text).ToString("yyyyMMdd");
            string tglproduksi = DateTime.Parse(txtDateProduksi.Text).ToString("yyyyMMdd");
            if (RH99.Checked == true)
                lokasi = "C1.Lokasi='H99' and ";
            if (RW28.Checked == true)
                lokasi = "C1.Lokasi='W28' and ";
            if (RC99.Checked == true)
                lokasi = "C1.Lokasi='C99' and ";
            if (RP99.Checked == true)
                lokasi = "C1.Lokasi='P99' and ";
            if (RB99.Checked == true)
                lokasi = "C1.Lokasi='B99' and ";
            if (RBLain.Checked == true)
                lokasi = "C1.Lokasi not in ('B99','C99','W28','H99') and ";
            if (txtPartNo.Text.Trim() != string.Empty && txtPartNo.Text.Trim().Length > 7)
                lokasi = lokasi + "  C2.Partno like '%" + txtPartNo.Text.Trim() + "%' and ";
            if (RBtglserah.Checked == true)
                arrSerah = Serah.RetrieveByTglSerah(tglserah, lokasi);
            else
                arrSerah = Serah.RetrieveByTglProduksi(tglproduksi, lokasi, "produksi");

            Session["arrSerah"] = arrSerah;
            GridViewSerah.DataSource = arrSerah;
            GridViewSerah.DataBind();
        }

        private void LoadDataGridTerima()
        {
            ArrayList arrTerima = new ArrayList();
            T3_RekapFacade rekap = new T3_RekapFacade();
            string criteria = string.Empty;
            string tglterima = DateTime.Parse(txtDateTrm.Text).ToString("yyyyMMdd");
            arrTerima = rekap.RetrieveByTglTerima(tglterima);
            GridViewTerima.DataSource = arrTerima;
            GridViewTerima.DataBind();
        }

        protected void GridViewSerah_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            T3_GroupsFacade groupsFacade = new T3_GroupsFacade();
            T3_Groups groups = new T3_Groups();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Session["arrSerah"] != null)
                {//txtPartnoOK
                    ArrayList arrSerah = new ArrayList();
                    arrSerah = (ArrayList)Session["arrSerah"];
                    TextBox txtPartnoOK = (TextBox)e.Row.FindControl("txtPartnoOK");
                    TextBox txtQtyTrm = (TextBox)e.Row.FindControl("txtQtyTrm");
                    TextBox txtLokTrm = (TextBox)e.Row.FindControl("txtLokTrm");
                    Label LMarketing = (Label)e.Row.FindControl("LMarketing");
                    if (arrSerah.Count > 0)
                    {
                        if (RC99.Checked == true)
                            txtPartnoOK.Enabled = true;
                        else
                            txtPartnoOK.Enabled = false;
                        T1_Serah Serah = (T1_Serah)arrSerah[e.Row.RowIndex];
                        txtPartnoOK.Text = Serah.PartnoSer;
                        txtQtyTrm.Text = (Serah.QtyIn - Serah.QtyOut).ToString();
                        groups = groupsFacade.RetrieveByItem(txtPartnoOK.Text);
                        if (groups.ID == 0)
                            LMarketing.Text = ddlGroups.SelectedItem.Text;
                        else
                            LMarketing.Text = groups.Groups;
                        if (txtLokTrm0.Text != string.Empty)
                            txtLokTrm.Text = txtLokTrm0.Text;
                    }
                }
            }
        }

        protected void txtDateTrm_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridTerima();
        }

        protected void txtDateSerah_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah();
            LoadTransit();
            txtDateTrm.Text = txtDateSerah.Text;
            //txtDateTrm.Text = txtDateSerah.Text;
            //LoadDataGridTerima();
        }

        protected void ChkHide1_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide1.Checked == false)
            {
                Panel3.Visible = false;
                btnTansfer.Visible = false;
            }
            else
            {
                Panel3.Visible = true;
                btnTansfer.Visible = true;
            }
        }

        protected void GridViewSerah_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSerah.PageIndex = e.NewPageIndex;
            LoadDataGridSerah();
        }

        protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah();
        }

        protected void RH99_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah();
        }

        protected void RW28_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah();
        }

        protected void RP99_CheckedChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah();
        }

        public static void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void txtPartNo_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah();
        }

        protected void LoadTransit()
        {
            ArrayList arrTransit = new ArrayList();
            T1_SerahFacade tf = new T1_SerahFacade();

            string tglserah = DateTime.Parse(txtDateSerah.Text).ToString("yyyyMMdd");
            string tglproduksi = DateTime.Parse(txtDateProduksi.Text).ToString("yyyyMMdd");
            if (RBtglserah.Checked == true)
                arrTransit = tf.RetrieveByTransit(tglserah);
            else
                arrTransit = tf.RetrieveByTransitP(tglproduksi);

            GridViewSaldo.DataSource = arrTransit;
            GridViewSaldo.DataBind();
        }

        protected void LoadTransitPartnoNLokasi(string partno, string lokasi)
        {
            ArrayList arrTransit = new ArrayList();
            T1_SerahFacade tf = new T1_SerahFacade();
            arrTransit = tf.RetrieveByTransitPartnoNLokasi(partno, lokasi);
            GridViewSaldo.DataSource = arrTransit;
            GridViewSaldo.DataBind();
        }

        protected void LoadTotalTransit()
        {
            ArrayList arrTransit = new ArrayList();
            T1_SerahFacade tf = new T1_SerahFacade();
            arrTransit = tf.RetrieveByTotalTransit();
            GridViewSaldo1.DataSource = arrTransit;
            GridViewSaldo1.DataBind();
        }

        protected void ChkHide2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide2.Checked == true)
            {
                Panel4.Visible = true;
                LoadTransit();
            }
            else
            {
                Panel4.Visible = false;
            }
        }

        protected void clearchecklist()
        {
            RH99.Checked = false;
            RW28.Checked = false;
            RC99.Checked = false;
            RP99.Checked = false;
            RB99.Checked = false;
            RBLain.Checked = false;
        }

        protected void GridViewSaldo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "pilih")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewSaldo.Rows[index];
                txtDateSerah.Text = DateTime.Parse(row.Cells[0].Text).ToString("dd-MMM-yyyy");
                //txtDateTrm.Text = DateTime.Parse(row.Cells[0].Text).ToString("dd-MMM-yyyy");
                txtDateProduksi.Text = DateTime.Parse(row.Cells[1].Text).ToString("dd-MMM-yyyy");
                switch (row.Cells[3].Text.Trim().ToUpper())
                {
                    case "H99":
                        clearchecklist();
                        RH99.Checked = true;
                        break;

                    case "W28":
                        clearchecklist();
                        RW28.Checked = true;
                        break;

                    case "C99":
                        clearchecklist();
                        RC99.Checked = true;
                        break;

                    case "P99":
                        clearchecklist();
                        RP99.Checked = true;
                        break;

                    case "B99":
                        clearchecklist();
                        RB99.Checked = true;
                        break;

                    default:
                        clearchecklist();
                        RBLain.Checked = true;
                        break;
                }
                txtPartNo.Text = row.Cells[2].Text.Trim();
                LoadDataGridSerah();
            }
        }

        protected void RBtglserah_CheckedChanged(object sender, EventArgs e)
        {
            if (RBtglserah.Checked == true)
            {
                txtDateSerah.Enabled = true;
                txtDateProduksi.Enabled = false;
                LoadTransit();
                LoadDataGridSerah();
            }
        }

        protected void RBtglproduksi_CheckedChanged(object sender, EventArgs e)
        {
            if (RBtglproduksi.Checked == true)
            {
                txtDateSerah.Enabled = false;
                txtDateProduksi.Enabled = true;
                LoadTransit();
                LoadDataGridSerah();
            }
        }

        protected void GridViewSaldo1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "pilih")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewSaldo1.Rows[index];
                LoadTransitPartnoNLokasi(row.Cells[0].Text, row.Cells[1].Text);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                Panel1.Visible = true;
                LoadTotalTransit();
            }
            else
            {
                Panel1.Visible = false;
            }
        }

        protected void txtLokTrm0_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridSerah();
        }
    }
}