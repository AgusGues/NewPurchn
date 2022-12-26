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
    public partial class T3Adjust : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                DatePicker1.SelectedDate = DateTime.Now.AddDays(-1);
                //clearform();
                LoadDataGridViewItem();
            }
        }

        private void clearformitems()
        {
            txtPartnoA.Text = string.Empty;
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
            txtPartname1.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtStock1.Text = string.Empty;
            txtQty1.Text = string.Empty;
        }

        private void clearform()
        {
            Users users = (Users)Session["Users"];
            T3_AdjustDetail AdjustDetail = new T3_AdjustDetail();
            ArrayList arrAdjustItems = new ArrayList();
            if (users.Apv < 1)
                RBApproval.Visible = false;
            txtPartnoA.Text = string.Empty;
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
            txtPartname1.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtStock1.Text = string.Empty;
            txtQty1.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtBA.Text = string.Empty;
            txtAdjustNo.Text = string.Empty;
            Session["ListofAdjustItems"] = null;
            arrAdjustItems.Add(AdjustDetail);
            GridItem0.DataSource = arrAdjustItems;
            GridItem0.DataBind();
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

        private void LoadDataGridViewItem()
        {
            ArrayList arrAdjustDetail = new ArrayList();
            T3_AdjustDetailFacade AdjustDetailF = new T3_AdjustDetailFacade();
            if (RBTanggal.Checked == true)
            {
                arrAdjustDetail = AdjustDetailF.RetrieveByTgl(DatePicker1.SelectedDate.ToString("yyyyMMdd"));
            }
            if (RBNoBA.Checked == true)
            {
                arrAdjustDetail = AdjustDetailF.RetrieveByNoBA(txtBA.Text);
            }
            if (RBApproval.Checked == true)
            {
                clearform();
                arrAdjustDetail = AdjustDetailF.RetrieveByapv("0");
                GridApprove.DataSource = arrAdjustDetail;
                GridApprove.DataBind();
                Session["arrT3AdjustDetail"] = arrAdjustDetail;
                return;
            }
            GridItem.DataSource = arrAdjustDetail;
            GridItem.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            T3_AdjustDetail AdjustDetail = new T3_AdjustDetail();
            ArrayList arrAdjustItems = new ArrayList();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            Users users = (Users)Session["Users"];
            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DatePicker1.SelectedDate.Year;
            int Bulan = DatePicker1.SelectedDate.Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            if (txtLokasi1.Text == string.Empty || txtQty1.Text == string.Empty || txtBA.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Data belum lengkap");
                return;
            }

            if (Session["ListofAdjustItems"] != null)
            {
                arrAdjustItems = (ArrayList)Session["ListofAdjustItems"];
                foreach (T3_AdjustDetail cekdjustItems in arrAdjustItems)
                {
                    if (txtPartnoA.Text.Trim() == cekdjustItems.PartNo && txtLokasi1.Text.Trim() == cekdjustItems.Lokasi.Trim())
                    {
                        DisplayAJAXMessage(this, "Partno : " + txtPartnoA.Text.Trim() + " sudah di input");
                        clearformitems();
                        return;
                    }
                }
            }
            AdjustDetail = new T3_AdjustDetail();
            AdjustDetail.ItemID = int.Parse(Session["itemid1"].ToString());
            AdjustDetail.LokID = int.Parse(Session["lokid1"].ToString());
            int stock = 0;
            stock = SerahFacade.GetStock(int.Parse(Session["lokid1"].ToString()), int.Parse(Session["itemid1"].ToString()));
            if (RBIn.Checked == true)
            {
                AdjustDetail.QtyIn = int.Parse(txtQty1.Text);
                AdjustDetail.QtyOut = 0;
            }
            else
            {
                AdjustDetail.QtyIn = 0;
                AdjustDetail.QtyOut = int.Parse(txtQty1.Text);
            }
            AdjustDetail.PartNo = txtPartnoA.Text;
            AdjustDetail.Lokasi = txtLokasi1.Text;
            AdjustDetail.AdjustDate = DatePicker1.SelectedDate;
            if (RBIn.Checked == true)
                AdjustDetail.AdjustType = "In";
            else
            {
                AdjustDetail.AdjustType = "Out";
                if (int.Parse(txtStock1.Text) - int.Parse(txtQty1.Text) < 0)
                {
                    DisplayAJAXMessage(this, "Adjust Out tidak bisa dilakukan");
                    return;
                }
            }
            AdjustDetail.NoBA = txtBA.Text;
            AdjustDetail.Keterangan = txtKeterangan.Text;
            AdjustDetail.CreatedBy = users.UserName;
            AdjustDetail.Approval = "Admin";
            //AdjustDetail.SA = stock;
            arrAdjustItems.Add(AdjustDetail);
            Session["ListofAdjustItems"] = arrAdjustItems;
            GridItem0.DataSource = arrAdjustItems;
            GridItem0.DataBind();
            clearformitems();
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

        protected void btnSimpan_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            ArrayList arrAdjustItems = new ArrayList();
            T3_Adjust T3adjust = new T3_Adjust();
            T3_AdjustFacade T3adjustFacade = new T3_AdjustFacade();
            T3_AdjustDetail T3adjustDetail = new T3_AdjustDetail();
            T3_AdjustDetailFacade T3adjustDetailFacade = new T3_AdjustDetailFacade();

            int intResult = 0;
            if (Session["ListofAdjustItems"] != null && txtBA.Text != string.Empty)
            {
                arrAdjustItems = (ArrayList)Session["ListofAdjustItems"];
                //rekam master
                int intdocno = T3adjustFacade.GetDocNo(DatePicker1.SelectedDate.Date) + 1;
                string docno = "Adj" + DatePicker1.SelectedDate.ToString("yy") + DatePicker1.SelectedDate.ToString("MM") + intdocno.ToString().PadLeft(4, '0');
                txtAdjustNo.Text = docno;
                T3adjust.AdjustNo = docno;
                T3adjust.AdjustDate = DatePicker1.SelectedDate;
                T3adjust.CreatedBy = users.UserName;
                T3adjust.NoBA = txtBA.Text;
                T3adjust.Keterangan = txtKeterangan.Text;
                #region depreciated line
                //intResult = T3adjustFacade.Insert(T3adjust);
                //rekam detail
                //if (intResult > 0)
                //{
                //    foreach (T3_AdjustDetail AdjustItems in arrAdjustItems)
                //    {
                //        AdjustItems.AdjustID = intResult;
                //        //T3adjustDetailFacade.Insert(AdjustItems);
                //    }
                //}
                #endregion
                #region Verifikasi Closing Periode
                /**
                 * check closing periode saat ini
                 * added on 13-08-2014
                 */
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = DatePicker1.SelectedDate.Year;
                int Bulan = DatePicker1.SelectedDate.Month;
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                    return;
                }
                #endregion

                T3_AdjustProcessFacade AdjustProcessFacade = new T3_AdjustProcessFacade(T3adjust, arrAdjustItems);
                string strError = AdjustProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    LoadDataGridViewItem();
                    clearform();
                }
            }
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridViewItem();
        }

        private void clearlokasiawal()
        {
            txtTebal1.Text = string.Empty;
            txtLebar1.Text = string.Empty;
            txtPanjang1.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtQty1.Text = string.Empty;
            txtPartnoA.Text = string.Empty;
            txtPartnoA.Focus();
        }

        protected void txtPartnoA_TextChanged(object sender, EventArgs e)
        {
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
            AutoCompleteExtender4.ContextKey = txtPartnoA.Text;
            txtLokasi1.Focus();
        }

        protected void txtLokasi1_TextChanged(object sender, EventArgs e)
        {
            T3_SerahFacade t3_SerahFacade = new T3_SerahFacade();
            T3_Serah t3serah = new T3_Serah();
            FC_Lokasi lok = new FC_Lokasi();
            FC_LokasiFacade fclokasifacade = new FC_LokasiFacade();

            lok = fclokasifacade.RetrieveByLokasi(txtLokasi1.Text);
            t3serah = t3_SerahFacade.RetrieveStockByPartno(txtPartnoA.Text, txtLokasi1.Text);
            FC_LokasiFacade lokasifacde = new FC_LokasiFacade();
            int cekLoading = 0;
            cekLoading = lokasifacde.CekLokasiLoading(txtLokasi1.Text);
            if (cekLoading > 0)
            {
                txtLokasi1.Text = string.Empty;
                DisplayAJAXMessage(this, "Lokasi hanya untuk penyiapan kirim");
                return;
            }
            int qty = t3serah.Qty;
            Session["lokid1"] = lok.ID;
            txtStock1.Text = qty.ToString();
            txtQty1.Text = string.Empty;
            txtQty1.Focus();
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
                txtQty1.Text = string.Empty;
                Session["serahID"] = row.Cells[1].Text;
                Session["lokid1"] = row.Cells[2].Text;
                Session["lokasi1"] = row.Cells[9].Text;
                Session["partno1"] = row.Cells[3].Text;
                Session["itemid1"] = row.Cells[0].Text;
                txtQty1.Focus();
            }
        }

        protected void txtPartnoC_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrAdjustDetail = new ArrayList();
            T3_AdjustDetailFacade AdjustDetailF = new T3_AdjustDetailFacade();
            if (txtPartnoC.Text != string.Empty)
                arrAdjustDetail = AdjustDetailF.RetrieveByPartno(txtPartnoC.Text);

            GridItem.DataSource = arrAdjustDetail;
            GridItem.DataBind();
            LoadDataGridViewtrans();
            txtPartnoC.Text = string.Empty;
        }
        protected void txtLokasiC_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrAdjustDetail = new ArrayList();
            T3_AdjustDetailFacade AdjustDetailF = new T3_AdjustDetailFacade();

            if (txtLokasiC.Text != string.Empty)
                arrAdjustDetail = AdjustDetailF.RetrieveByLokasi(txtLokasiC.Text);
            GridItem.DataSource = arrAdjustDetail;
            GridItem.DataBind();
            LoadDataGridViewtrans();
            txtLokasiC.Text = string.Empty;
        }
        protected void txtQty1_TextChanged(object sender, EventArgs e)
        {
            if (txtQty1.Text != string.Empty && Convert.ToInt32(txtQty1.Text) > 0)
            {
                btnTansfer.Focus();
                //clearform();
            }
            else
            {
                txtQty1.Text = string.Empty;
                txtQty1.Focus();
            }
        }


        private void Getfocus()
        {
            if (txtPartnoA.Text == string.Empty)
                txtPartnoA.Focus();
            else
                if (txtLokasi1.Text == string.Empty)
                txtLokasi1.Focus();
            else
                    if (txtQty1.Text == string.Empty)
                txtQty1.Focus();
            else
                btnTansfer.Focus();
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
                txtCariBA.Visible = false;
                txtPartnoC.Text = string.Empty;
                txtLokasiC.Text = string.Empty;
            }
            else
                if (ddlCari.SelectedIndex == 1)
            {
                txtPartnoC.Visible = false;
                txtCariBA.Visible = false;
                txtLokasiC.Visible = true;
                txtCariBA.Text = string.Empty;
                txtPartnoC.Text = string.Empty;
                txtLokasiC.Text = string.Empty;
            }
            else
            {
                txtPartnoC.Visible = false;
                txtLokasiC.Visible = false;
                txtCariBA.Visible = true;
                txtCariBA.Text = string.Empty;
                txtPartnoC.Text = string.Empty;
                txtLokasiC.Text = string.Empty;
            }
        }
        protected void RBOut_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBOut.Checked == true)
            //    this.b = System.Drawing.Color.Yellow;
        }
        protected void RBApproval_CheckedChanged(object sender, EventArgs e)
        {
            if (RBApproval.Checked == true)
            {
                PanelApprove.Visible = true; GridApprove.Visible = true; GridItem.Visible = false;
                LoadDataGridViewItem();
            }
        }

        protected void RBTanggal_CheckedChanged(object sender, EventArgs e)
        {
            if (RBTanggal.Checked == true)
            {
                PanelApprove.Visible = false; GridApprove.Visible = false; GridItem.Visible = true;
                LoadDataGridViewItem();
            }
        }
        protected void RBAdjustNo_CheckedChanged(object sender, EventArgs e)
        {
            if (RBNoBA.Checked == true)
            {
                PanelApprove.Visible = false;
                PanelApprove.Visible = false;
                GridApprove.Visible = false;
                GridItem.Visible = true;
                LoadDataGridViewItem();
            }
        }

        protected void txtCariBA_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrAdjustDetail = new ArrayList();
            T3_AdjustDetailFacade AdjustDetailF = new T3_AdjustDetailFacade();
            if (txtCariBA.Text != string.Empty)
                arrAdjustDetail = AdjustDetailF.RetrieveByNoBA(txtCariBA.Text);

            GridItem.DataSource = arrAdjustDetail;
            GridItem.DataBind();
            txtCariBA.Text = string.Empty;
        }

        protected void GridItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            T3_AdjustDetail AdjustDetail = new T3_AdjustDetail();
            T3_AdjustDetailFacade AdjustDetailF = new T3_AdjustDetailFacade();
            if (e.CommandName == "hapus")
            {
                if (users.Apv < 1)
                {
                    DisplayAJAXMessage(this, "Hak akses tidak mencukupi");
                    return;
                }
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItem.Rows[index];
                if (row.Cells[9].Text == "Admin")
                {
                    AdjustDetail.CreatedBy = users.UserName;
                    AdjustDetail.ID = int.Parse(row.Cells[0].Text);
                    //AdjustDetailF.Delete(AdjustDetail);
                    LoadDataGridViewItem();
                }
                else
                {
                    DisplayAJAXMessage(this, "Data tidak bisa di cancel, karena sudah approve accounting");
                }
            }
        }
        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrT3AdjustDetail = (ArrayList)Session["arrT3AdjustDetail"];
            int i = 0;
            if (ChkAll.Checked == true) //& arrLembur.Count >0 
            {

                foreach (T3_AdjustDetail T3AdjustDetail in arrT3AdjustDetail)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = true;
                    i = i + 1;
                }
            }
            else
            {
                foreach (T3_AdjustDetail T3AdjustDetail in arrT3AdjustDetail)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = false;
                    i = i + 1;
                }
            }
        }
        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            T3_AdjustDetailFacade T3AdjustDetailFacade = new T3_AdjustDetailFacade();
            ArrayList arrT3AdjustDetail = (ArrayList)Session["arrT3AdjustDetail"];
            Users users = (Users)Session["Users"];
            T3_Serah t3serah = new T3_Serah();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            int i = 0;
            foreach (T3_AdjustDetail T3AdjustDetail in arrT3AdjustDetail)
            {
                CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                if (chk.Checked)
                {
                    if (users.Apv == 0)
                    {
                        DisplayAJAXMessage(this, "Level Approval tidak mencukupi");
                        return;
                    }
                    string Tgl = GridApprove.Rows[i].Cells[2].Text;
                    #region Verifikasi Closing Periode
                    /**
                         * check closing periode saat ini
                         * added on 13-08-2014
                         */
                    ClosingFacade Closing = new ClosingFacade();
                    int Tahun = DateTime.Parse(GridApprove.Rows[i].Cells[2].Text).Year;
                    int Bulan = DateTime.Parse(GridApprove.Rows[i].Cells[2].Text).Month;
                    int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                    int clsStat = Closing.GetClosingStatus("SystemClosing");
                    if (status == 1 && clsStat == 1)
                    {
                        DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi " + GridApprove.Rows[i].Cells[2].Text + " Tidak bisa Approved");
                        return;
                    }
                    #endregion
                    string username = users.UserName;
                    T3AdjustDetail.Apv = users.Apv;
                    T3AdjustDetail.ID = int.Parse(GridApprove.Rows[i].Cells[0].Text);
                    T3AdjustDetail.CreatedBy = users.UserName;
                    //intResult = T3AdjustDetailFacade.Update(T3AdjustDetail);
                    if (T3AdjustDetailFacade.Error != string.Empty)
                    {
                        break;
                    }
                    if (T3AdjustDetail.QtyOut > 0)
                    {
                        t3serah.Flag = "kurang";
                        t3serah.Qty = T3AdjustDetail.QtyOut;
                        int stock = 0;
                        stock = SerahFacade.GetStock(T3AdjustDetail.LokID, T3AdjustDetail.ItemID);
                        T3AdjustDetail.SA = stock;
                        if (stock - Convert.ToInt32(t3serah.Qty) < 0)
                        {
                            DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                            T3AdjustDetail.Apv = 0;
                        }
                    }
                    else
                    {
                        t3serah.Flag = "tambah";
                        t3serah.Qty = T3AdjustDetail.QtyIn;
                        int stock = 0;
                        stock = SerahFacade.GetStock(T3AdjustDetail.LokID, T3AdjustDetail.ItemID);
                        T3AdjustDetail.SA = stock;
                    }
                    t3serah.GroupID = 0;
                    t3serah.ItemID = T3AdjustDetail.ItemID;
                    t3serah.LokID = T3AdjustDetail.LokID;
                    t3serah.CreatedBy = users.UserName;
                    // intResult = SerahFacade.Insert(t3serah);
                }
                i = i + 1;
            }
            T3_Adjust T3adjust = new T3_Adjust();
            T3_AdjustProcessFacade AdjustProcessFacade = new T3_AdjustProcessFacade(T3adjust, arrT3AdjustDetail);
            string strError = AdjustProcessFacade.Update();
            if (strError == string.Empty)
            {
                LoadDataGridViewItem();
            }

        }
    }
}