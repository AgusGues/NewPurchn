using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using Cogs;
using BusinessFacade;
using Factory;
using System.Collections;
using DataAccessLayer;

namespace GRCweb1.Modul.Factory
{
    public partial class T1AdjustListplank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTanggal.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                //clearform();
                LoadDataGridViewItem();
            }
        }

        private void clearformitems()
        {
            txtPartnoA.Text = string.Empty;
            txtQty1.Text = string.Empty;
        }

        private void clearform()
        {
            Users users = (Users)Session["Users"];
            T1_AdjustListplankDetail AdjustDetail = new T1_AdjustListplankDetail();
            ArrayList arrAdjustItems = new ArrayList();
            if (users.Apv < 1)
                RBApproval.Visible = false;
            txtPartnoA.Text = string.Empty;
            txtQty1.Text = string.Empty;
            txtBA.Text = string.Empty;
            txtAdjustNo.Text = string.Empty;
            Session["ListofAdjustItems"] = null;
            arrAdjustItems.Add(AdjustDetail);
            GridItem0.DataSource = arrAdjustItems;
            GridItem0.DataBind();
        }

        private void LoadDataGridViewItem()
        {
            ArrayList arrAdjustDetail = new ArrayList();
            T1_AdjustListplankDetailFacade AdjustDetailF = new T1_AdjustListplankDetailFacade();
            if (RBTanggal.Checked == true)
            {
                arrAdjustDetail = AdjustDetailF.RetrieveByTgl(DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd"));
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
            T1_AdjustListplankDetail AdjustDetail = new T1_AdjustListplankDetail();
            ArrayList arrAdjustItems = new ArrayList();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            Users users = (Users)Session["Users"];
            #region Verifikasi Closing Periode
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = DateTime.Parse(txtTanggal.Text).Year;
            int Bulan = DateTime.Parse(txtTanggal.Text).Month;
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            if (Session["itemid0"] == null)
            {
                DisplayAJAXMessage(this, "Partno asal belum ditentukan");
                return;
            }
            if (Session["ListofAdjustItems"] != null)
            {
                arrAdjustItems = (ArrayList)Session["ListofAdjustItems"];
                foreach (T1_AdjustListplankDetail cekdjustItems in arrAdjustItems)
                {
                    if (txtPartnoA.Text.Trim() == cekdjustItems.PartNo)
                    {
                        DisplayAJAXMessage(this, "Partno : " + txtPartnoA.Text.Trim() + " sudah di input");
                        clearformitems();
                        return;
                    }
                }
            }
            AdjustDetail = new T1_AdjustListplankDetail();
            AdjustDetail.ItemID0 = int.Parse(Session["itemid0"].ToString());
            AdjustDetail.ItemID = int.Parse(Session["itemid1"].ToString());
            int stock = 0;
            //stock = SerahFacade.GetStock(int.Parse(Session["lokid1"].ToString()), int.Parse(Session["itemid1"].ToString()));
            if (RBIn.Checked == true)
            {
                AdjustDetail.Qty = int.Parse(txtQty1.Text);
            }
            else
            {
                AdjustDetail.Qty = int.Parse(txtQty1.Text);
            }
            AdjustDetail.PartNo = txtPartnoA.Text;
            AdjustDetail.AdjustDate = DateTime.Parse(txtTanggal.Text);
            if (RBIn.Checked == true)
                AdjustDetail.AdjustType = "In";
            else
            {
                AdjustDetail.AdjustType = "Out";
            }
            AdjustDetail.NoBA = txtBA.Text;
            AdjustDetail.CreatedBy = users.UserName;
            AdjustDetail.Approval = "Admin";
            AdjustDetail.Process = ddlprocess.SelectedItem.Text;
            //AdjustDetail.SA = stock;
            arrAdjustItems.Add(AdjustDetail);
            Session["ListofAdjustItems"] = arrAdjustItems;
            GridItem0.DataSource = arrAdjustItems;
            GridItem0.DataBind();
            clearformitems();
            Session["serahID"] = null;
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
            T1_AdjustListplank T3adjust = new T1_AdjustListplank();
            T1_AdjustListplankFacade T3adjustFacade = new T1_AdjustListplankFacade();
            T1_AdjustListplankDetail T3adjustDetail = new T1_AdjustListplankDetail();
            T1_AdjustListplankDetailFacade T3adjustDetailFacade = new T1_AdjustListplankDetailFacade();

            if (txtBA.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Nomor berita acara belum diisi");
                return;
            }

            int intResult = 0;
            if (Session["ListofAdjustItems"] != null)
            {
                arrAdjustItems = (ArrayList)Session["ListofAdjustItems"];
                //rekam master
                int intdocno = T3adjustFacade.GetDocNo(DateTime.Parse(txtTanggal.Text).Date) + 1;
                string docno = "Adj" + DateTime.Parse(txtTanggal.Text).ToString("yy") + DateTime.Parse(txtTanggal.Text).ToString("MM") +
                    intdocno.ToString().PadLeft(4, '0');
                txtAdjustNo.Text = docno;
                T3adjust.AdjustNo = docno;
                T3adjust.AdjustDate = DateTime.Parse(txtTanggal.Text);
                T3adjust.CreatedBy = users.UserName;
                T3adjust.NoBA = txtBA.Text;

                #region depreciated line
                #endregion
                #region Verifikasi Closing Periode
                /**
                 * check closing periode saat ini
                 * added on 13-08-2014
                 */
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = DateTime.Parse(txtTanggal.Text).Year;
                int Bulan = DateTime.Parse(txtTanggal.Text).Month;
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                    return;
                }
                #endregion
                T1_AdjustListplankProcessFacade AdjustProcessFacade = new T1_AdjustListplankProcessFacade(T3adjust, arrAdjustItems);
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
            Session["itemid1"] = fC_Items.ID;
            if (fC_Items.Tebal == 0)
            {
                clearlokasiawal();
                return;
            }

            ArrayList arrAdjustDetail = new ArrayList();
            T1_AdjustListplankDetailFacade AdjustDetailF = new T1_AdjustListplankDetailFacade();
            GridItem.DataSource = arrAdjustDetail;
            GridItem.DataBind();
        }
        protected void txtPartnoA0_TextChanged(object sender, EventArgs e)
        {
            FC_Items fC_Items = new FC_Items();
            FC_ItemsFacade fC_ItemsFacade = new FC_ItemsFacade();
            if (txtPartnoA0.Text.Trim() != string.Empty)
                fC_Items = fC_ItemsFacade.RetrieveByPartNo(txtPartnoA0.Text.Trim());
            Session["itemid0"] = fC_Items.ID;
            if (fC_Items.Tebal == 0)
            {
                txtPartnoA0.Text = string.Empty;
                return;
            }
            txtPartnoA.Focus();
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

        protected void txtQty1_PreRender(object sender, EventArgs e)
        {
            Getfocus();
        }
        protected void DatePicker1_SelectionChanged1(object sender, EventArgs e)
        {
            clearform();
            txtPartnoA.Focus();
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

        protected void GridItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            T1_AdjustListplankDetail AdjustDetail = new T1_AdjustListplankDetail();
            T1_AdjustListplankDetailFacade AdjustDetailF = new T1_AdjustListplankDetailFacade();
            ArrayList arrAdjustItems = new ArrayList();
            if (e.CommandName == "hapus")
            {
                if (users.Apv < 1)
                {
                    DisplayAJAXMessage(this, "Hak akses tidak mencukupi");
                    return;
                }
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItem0.Rows[index];
                if (row.Cells[9].Text == "Admin" || row.Cells[9].Text.Trim() == string.Empty)
                {
                    arrAdjustItems = (ArrayList)Session["ListofAdjustItems"];
                    arrAdjustItems.RemoveAt(index);
                    GridItem0.DataSource = arrAdjustItems;
                    GridItem0.DataBind();
                    //AdjustDetail.CreatedBy = users.UserName;
                    //AdjustDetail.ID = int.Parse(row.Cells[0].Text);
                    ////AdjustDetailF.Delete(AdjustDetail);
                    //LoadDataGridViewItem();
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
                foreach (T1_AdjustListplankDetail T3AdjustDetail in arrT3AdjustDetail)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = true;
                    i = i + 1;
                }
            }
            else
            {
                foreach (T1_AdjustListplankDetail T3AdjustDetail in arrT3AdjustDetail)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = false;
                    i = i + 1;
                }
            }
        }

        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            T1_AdjustListplankDetailFacade T3AdjustDetailFacade = new T1_AdjustListplankDetailFacade();
            ArrayList arrT3AdjustDetail = (ArrayList)Session["arrT3AdjustDetail"];
            Users users = (Users)Session["Users"];
            T3_Serah t3serah = new T3_Serah();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            int i = 0;
            foreach (T1_AdjustListplankDetail T3AdjustDetail in arrT3AdjustDetail)
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
                }
                i = i + 1;
            }
            T1_AdjustListplank T3adjust = new T1_AdjustListplank();
            T1_AdjustListplankProcessFacade AdjustProcessFacade = new T1_AdjustListplankProcessFacade(T3adjust, arrT3AdjustDetail);
            string strError = AdjustProcessFacade.Update();
            if (strError == string.Empty)
            {
                LoadDataGridViewItem();
            }
        }
        protected void txtTanggal_TextChanged(object sender, EventArgs e)
        {
            LoadDataGridViewItem();
        }
    }
}