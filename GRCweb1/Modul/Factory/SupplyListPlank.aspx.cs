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
using DataAccessLayer;
using System.Collections;

namespace GRCweb1.Modul.Factory
{
    public partial class SupplyListPlank : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                DatePicker1.SelectedDate = DateTime.Now.AddDays(-1);
            }
        }

        private void clearform()
        {
            txtPartnoA.Text = string.Empty;
            txtLokasi1.Text = string.Empty;
            txtStock1.Text = string.Empty;
            txtQtyOut.Text = string.Empty;
            LoadDataGridViewtrans();
            LoadDataGridViewSupply();
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
            arrT3Serah = T3Serah.RetrieveStock(criteria);
            GridViewtrans.DataSource = arrT3Serah;
            GridViewtrans.DataBind();
        }

        private void LoadDataGridViewSupply()
        {
            ArrayList arrT3Supply = new ArrayList();
            T3_SupplyFacade T3Supply = new T3_SupplyFacade();
            arrT3Supply = T3Supply.RetrieveBytgl(DatePicker1.SelectedDate.ToString("yyyyMMdd"));
            GridViewSupply.DataSource = arrT3Supply;
            GridViewSupply.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
        }
        protected void TransferData()
        {
            Users users = (Users)Session["Users"];
            T3_Serah t3serahK = new T3_Serah();
            T3_Serah t3serahT = new T3_Serah();
            FC_Items items = new FC_Items();
            BM_DestackingFacade dest = new BM_DestackingFacade();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            FC_ItemsFacade ItemsFacade = new FC_ItemsFacade();
            T3_SiapKirimFacade t3siapkirimfacade = new T3_SiapKirimFacade();
            T3_Supply T3Supply = new T3_Supply();
            T3_SupplyFacade SupplyFacade = new T3_SupplyFacade();
            int stock = 0;
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
            if (txtLokasi1.Text == string.Empty || txtQtyOut.Text == string.Empty)
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
            if (stock - Convert.ToInt32(txtQtyOut.Text) < 0)
            {
                DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
                return;
            }
            //rekam table Supply
            T3Supply.LokasiID = int.Parse(Session["lokid1"].ToString());
            T3Supply.TglTrans = DatePicker1.SelectedDate.Date;
            T3Supply.ItemID = int.Parse(Session["itemid1"].ToString());
            T3Supply.Qty = Convert.ToInt32(txtQtyOut.Text);
            T3Supply.CreatedBy = users.UserName;

            int intResult = 0;
            TransactionManager transManager = new TransactionManager(Global.ConnectionString());
            transManager.BeginTransaction();
            #region rekam listplank
            T1_LR1_ListPlank T1LR1ListPlank = new T1_LR1_ListPlank();
            T1_LR1_ListPlankFacade T1LR1ListPlankF = new T1_LR1_ListPlankFacade();
            items = ItemsFacade.RetrieveByPartNo(txtPartnoA.Text);
            //T1LR1ListPlank = T1LR1ListPlankF.RetrieveByID(GridViewtrans0.Rows[i].Cells[0].Text);
            T1LR1ListPlank.ItemID0 = items.ID;
            T1LR1ListPlank.LokasiID0 = int.Parse(Session["lokid1"].ToString());
            T1LR1ListPlank.LokasiID = T1LR1ListPlank.LokasiID0;
            T1LR1ListPlank.L1ID = 0;
            T1LR1ListPlank.ItemID = items.ID;
            T1LR1ListPlank.TglTrans = DatePicker1.SelectedDate.Date;
            T1LR1ListPlank.QtyIn = Convert.ToInt32(txtQtyOut.Text);
            T1LR1ListPlank.QtyOut = 0;
            T1LR1ListPlank.HPP = 0;
            T1LR1ListPlank.CreatedBy = users.UserName;
            AbstractTransactionFacadeF absTrans = new T1_LR1_ListPlankFacade(T1LR1ListPlank);
            intResult = absTrans.Insert1(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            #endregion

            #region Commit data
            AbstractTransactionFacadeF absTrans1 = new T3_SerahFacade(t3serahK);
            int intSerah = absTrans1.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            intSerah = 0;
            intResult = 0;
            absTrans = new T3_SerahFacade(t3serahT);
            intSerah = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            intResult = 0;
            absTrans = new T3_SupplyFacade(T3Supply);
            intResult = absTrans.Insert(transManager);
            if (absTrans.Error != string.Empty)
            {
                transManager.RollbackTransaction();
                return;
            }
            transManager.CommitTransaction();
            transManager.CloseConnection();
            #endregion
            //string strError = SimetrisProcessFacade.Insert();
            if (intResult > -1)
            {
                DisplayAJAXMessage(this, "Data tersimpan");
                txtPartnoA.Focus();
                clearform();
            }
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
        }

        protected void btnRefresh_ServerClick(object sender, EventArgs e)
        {
            LoadDataGridViewSupply();
        }
        private void clearlokasiawal()
        {
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

        private void Getfocus()
        {
            if (txtPartnoA.Text == string.Empty)
                txtPartnoA.Focus();
            else
                if (txtLokasi1.Text == string.Empty)
                txtLokasi1.Focus();
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
    }
}