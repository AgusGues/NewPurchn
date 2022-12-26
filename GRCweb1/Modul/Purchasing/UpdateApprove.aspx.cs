using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Reflection;
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace GRCweb1.Modul.Purchasing
{
    public partial class UpdateApprove : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";
                Users users = (Users)Session["Users"]; LoadSupplier();

                Session["AlasanCancel"] = null;
                Session["AlasanBatal"] = null;
                Session["AlasanTolak"] = null;

                LoadTermOfPay();
                LoadMataUang();
                LoadSatuan();
                appstat.Visible = false;
                AprovalStatus.Visible = true;
            }
        }

        private void LoadPO(string noPO)
        {
            try
            {
                //POPurchn hPO = new POPurchn();
                ArrayList arrLstPO = new ArrayList();
                POPurchnFacade PO = new POPurchnFacade();
                RevisiPOFacade rPO = new RevisiPOFacade();
                RevisiPO dPO = rPO.RetrieveByNo(noPO);
                TermOfPayFacade trmf = new TermOfPayFacade();
                arrLstPO = rPO.RetrievePODetail(dPO.ID, ((Users)Session["Users"]).UserName, dPO.ItemTypeID);
                Session["Totalharga"] = 0;
                Session["PPN"] = 0;
                Session["PPH"] = 0;
                Session["Disc"] = 0;
                Session["NoPO"] = string.Empty;
                Session["IDPO"] = 0;
                Session.Remove("Aprove");
                HeadID.Text = dPO.ID.ToString();
                string app = (dPO.Approval == 1) ? "Head" : (dPO.Approval == 2) ? "Manager" : "Admin";
                NoPO.Text = dPO.NoPO.ToString();
                ddlSupplierID.Text = dPO.SupplierID.ToString();
                int term = trmf.getterm(dPO.Termin.ToString());
                if (term > 0)
                    ddlTermin.SelectedValue = dPO.Termin.ToString();
                else
                {
                    ddlTermin.SelectedValue = "Lain-lain";
                    txtTermOfPay.Visible = true;
                    txtTermOfPay.Text = dPO.Termin.ToString();
                }
                ppn.Text = dPO.PPN.ToString();
                pph.Text = dPO.PPH.ToString();
                Disc.Text = dPO.Disc.ToString();
                kurs.Text = dPO.NilaiKurs.ToString();
                rmak.Text = dPO.Remark.ToString();
                ket.Text = dPO.Keterangan.ToString();
                ddlMataUang.Text = dPO.Crc.ToString();
                TermOfDelivery.Text = dPO.Delivery.ToString();
                ongkos.Text = dPO.Ongkos.ToString();
                appstat.Text = app;
                txtStatus.Value = dPO.Status.ToString();
                AprovalStatus.SelectedValue = dPO.Approval.ToString();
                POPurchDate.Text = dPO.POPurchnDate.ToString("dd/MM/yyyy");
                totalPrice.Text = TotalPricePO(dPO.ID).ToString("#,#.00#;(#,#.00#)");
                foreach (RevisiPO dp in arrLstPO)
                {
                    itemTypeID.Text = dp.ItemTypeID.ToString();
                }
                //appstat.Visible = (((Users)Session["Users"]).Apv == dPO.Approval) ? true : false;
                AprovalStatus.Enabled = (((Users)Session["Users"]).Apv > 1 || ((Users)Session["Users"]).UserName == "Admin") ? true : false;
                if (CekDP(NoPO.Text) > 0)
                    AprovalStatus.Enabled = false;

                simpan.Enabled = (dPO.Approval > ((Users)Session["Users"]).Apv) ? false : true;
                delete.Enabled = (dPO.Approval > ((Users)Session["Users"]).Apv) ? false : true;
                Session["Totalharga"] = TotalPricePO(dPO.ID);
                Session["NoPO"] = NoPO.Text;
                Session["PPN"] = dPO.PPN;
                Session["PPH"] = dPO.PPH;
                Session["Disc"] = dPO.Disc;
                Session["Aprove"] = dPO.Approval;
                Session["IDPO"] = dPO.ID.ToString();
                if (arrLstPO.Count > 0)
                {

                    detailPO.DataSource = arrLstPO;
                    detailPO.DataBind();
                }
                Session.Remove("AlasanCancel");
                simpan.Attributes.Add("onclick", "return confirm_revisi();");
                DetailUp.Attributes.Add("onclick", "return confirm_revisi();");
                delete.Attributes.Add("onclick", "return confirm_revisi();");

            }
            catch (Exception ex)
            {
                string strError = ex.Message;
                DisplayAJAXMessage(this, "No. PO tidak di kenal, check lagi.");
            }
        }
        private decimal TotalPricePO(int POID)
        {
            POPurchnFacade pOPurchnFacade2 = new POPurchnFacade();
            POPurchn pOPurchn2 = pOPurchnFacade2.ViewPO(POID, ((Users)Session["Users"]).ViewPrice);
            POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
            POPurchnDetail pOPucrhnDetail = pOPurchnDetailFacade.RetrieveTotalById(POID, ((Users)Session["Users"]).ViewPrice);
            decimal totprice = pOPucrhnDetail.Total - ((pOPurchn2.Disc / 100) * pOPucrhnDetail.Total);
            decimal grandtotal = (((pOPurchn2.PPN / 100) * totprice) + ((pOPurchn2.PPH / 100) * totprice) + totprice);

            return grandtotal;
        }

        private void LoadSupplier()
        {
            ArrayList arrSuppPurch = new ArrayList();
            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrSuppPurch = suppPurchFacade.Retrieve();
            ddlSupplierID.Items.Add(new ListItem("-- Pilih Supplier --", string.Empty));
            foreach (SuppPurch suppPurch in arrSuppPurch)
            {
                ddlSupplierID.Items.Add(new ListItem(suppPurch.SupplierName, suppPurch.ID.ToString()));
            }
        }
        private void LoadTermOfPay()
        {
            ArrayList arrTermOfPay = new ArrayList();
            TermOfPayFacade termOfPayFacade = new TermOfPayFacade();
            arrTermOfPay = termOfPayFacade.Retrieve();
            ddlTermin.Items.Clear();
            ddlTermin.Items.Add(new ListItem("-- Pilih Jenis TOP --", string.Empty));
            foreach (TermOfPay termOfPay in arrTermOfPay)
            {
                ddlTermin.Items.Add(new ListItem(termOfPay.TermPay, termOfPay.TermPay.ToString()));
            }
        }
        private void LoadMataUang()
        {
            ArrayList arrMataUang = new ArrayList();
            MataUangFacade mataUangFacade = new MataUangFacade();
            arrMataUang = mataUangFacade.Retrieve();
            ddlMataUang.Items.Add(new ListItem("-- Pilih Mata Uang --", string.Empty));
            foreach (MataUang mataUang in arrMataUang)
            {
                ddlMataUang.Items.Add(new ListItem(mataUang.Lambang, mataUang.ID.ToString()));
            }
        }
        protected void detailPO_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl = (Label)e.Item.FindControl("Act");
                if (lbl != null)
                {
                    int apv = (Session["Aprove"] != null) ? (int)Session["Aprove"] : 0;
                    if (apv > ((Users)Session["Users"]).Apv)
                    {
                        lbl.Visible = false;
                    }
                    else
                    {
                        lbl.Visible = true;
                    }
                }
            }
            Image del = (Image)e.Item.FindControl("btnHapus");
            
            del.Attributes.Add("onclick", "return confirm_revisi();");
        }
        protected void detailPO_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RevisiPOFacade dPO = new RevisiPOFacade();
            RevisiPO Hd = dPO.RetrieveByNo(NoPO.Text);
            switch (e.CommandName)
            {
                case ("Edit"):
                    DetailPOEdit(e.CommandArgument.ToString());
                    break;
                case ("Hapus"):
                    if (Hd.Approval > 0)
                    {
                        DisplayAJAXMessage(this, "PO Sudah di Approve tidak bisa di hapus");
                    }
                    else
                    {
                        //popup delete detail
                        panEdit3.Visible = true;
                        mpePopUp3.Show();

                        Session["iddetailpo"] = e.CommandArgument.ToString();

                    }
                    break;
            }
        }
        public void DetailPOHapus(string ID, string Proses)
        {
            /**
             * PO di rubah status nya menjadi -1
             * tidak di hapus secara permanent
             * ini bukan tombol hapus yang di klick tetapi button delete pada list yang di klik
             */
            RevisiPOFacade hPO = new RevisiPOFacade();
            RevisiPO arrSPP = new RevisiPO();
            RevisiPO SPP = hPO.RetrieveDetailID(int.Parse(ID), int.Parse(itemTypeID.Text));
            decimal QtyPO = hPO.GetSPPQtyPO(SPP.SPPDetailID, "QtyPO");
            decimal QtySPP = hPO.GetSPPQtyPO(SPP.SPPDetailID, "Quantity");

            RevisiPO arrhPO = new RevisiPO();
            arrhPO.ID = int.Parse(ID);//PODetailID
            arrhPO.Status = -1;
            arrhPO.LastModifiedBy = ((Users)Session["Users"]).UserName;
            int result = hPO.Update(arrhPO);
            if (hPO.Error == string.Empty && result > 0)
            {
                /**
                 * Update QTY PO di SPP yng bersangkutan
                 **/

                arrSPP.ID = SPP.SPPDetailID;
                if (Proses == "Edit")
                {
                    if (Qty.Text == string.Empty)
                    {
                        /** po detail di hapus */
                        arrSPP.Jumlah = (QtyPO <= 0) ? 0 : (QtyPO - SPP.Qty);
                    }
                    else
                    {

                        arrSPP.Jumlah = (QtyPO - SPP.Qty) + decimal.Parse(Qty.Text);
                    }
                    /**
                     * Get Jumlah di QtyPO spp
                     */
                    decimal QtyPO2 = hPO.GetSPPQtyPO(SPP.SPPDetailID, "QtyPO");

                    if ((QtySPP - QtyPO2) == 0) { arrSPP.Status = 2; } else if (QtySPP > QtyPO2) { arrSPP.Status = 1; } else if (QtyPO2 == 0) { arrSPP.Status = 0; }
                }
                else if (Proses == "Hapus" || Proses == "Delete")
                {
                    decimal QtyPO3 = (QtyPO - SPP.Qty);
                    arrSPP.Jumlah = ((QtyPO - SPP.Qty) < 0) ? 0 : (QtyPO - SPP.Qty);
                    if ((QtySPP - QtyPO3) == 0) { arrSPP.Status = 2; } else if (QtySPP > QtyPO3 && QtyPO3 > 0) { arrSPP.Status = 1; } else if (QtyPO3 <= 0) { arrSPP.Status = 0; }
                }
                if (Proses == "Delete")
                {
                    int rst = hPO.UpdateQtyPOSPP(arrSPP);
                }
                else
                {
                    if (SPP.Qty != decimal.Parse(Qty.Text) || Proses == "Hapus")
                    {
                        int rst = hPO.UpdateQtyPOSPP(arrSPP);
                    }
                }
                if (hPO.Error == string.Empty && result > 0)
                {
                    /**
                     * Update terbilang jika yang di rubah price atau total nya
                     **/
                    decimal totalBaru = TotalPricePO(SPP.POID);
                    decimal jmm = (Qty.Text == string.Empty) ? 0 : Convert.ToDecimal(Qty.Text);
                    string sQty = (Session["Qty"] == null) ? "0" : Session["Qty"].ToString();
                    decimal ttharga = (Session["TotalHarga"] == null) ? 0 : Convert.ToDecimal(Session["TotalHarga"].ToString());
                    decimal ttQty = ((Session["Qty"] == null) || Session["Qty"].ToString() == "0") ? 0 : Convert.ToDecimal(Session["Qty"].ToString());
                    if ((ttharga != totalBaru) || (ttQty != jmm))
                    {
                        TerbilangFacade terbilang = new TerbilangFacade();
                        string kekata = terbilang.ConvertMoneyToWords(totalBaru);
                        RevisiPO upTerbilang = new RevisiPO();

                        upTerbilang.ID = SPP.POID;
                        upTerbilang.Terbilang = kekata;
                        int rest = hPO.UpdateTerbilang(upTerbilang);
                        if (hPO.Error == string.Empty && rest > 0)
                        {
                            /**
                             * Tampilkan hasil update ke grid
                             **/
                            clearDetail(this);
                            txtDetailPO.Visible = false;
                            headerPO.Visible = true;
                            #region simpan log transaksi revisi
                            Revisine ro = new Revisine();
                            RevisiPOnya rv = new RevisiPOnya();
                            rv.Criteria = "NoPO,POID,PODetailID,RevisiKe,AlasanRevisi,RevisiBy,RevisiTime,ItemID";
                            rv.Pilihan = "Insert";
                            rv.TableName = "POPurchnRevisi";
                            POPurchn podetail = new POPurchnFacade().RetrieveByID(int.Parse(HeadID.Text));
                            rv.Where = " where POID=" + podetail.ID.ToString();
                            rv.Where += " and Convert(Char,RevisiTime,112) < ('" + DateTime.Now.ToString("yyyyMMdd") + "')";
                            ro.NoPO = podetail.NoPO;
                            ro.POID = podetail.ID;
                            ro.PODetailID = int.Parse(ID);
                            ro.ItemID = podetail.ItemID;// int.Parse(KodeBarang.SelectedValue);
                            ro.RevisiKe = rv.GetRevisiNum() + 1;
                            ro.RevisiTime = DateTime.Now;
                            ro.AlasanRevisi = Session["AlasanCancel"].ToString();
                            ro.RevisiBy = ((Users)Session["Users"]).UserID.ToString();
                            string rs = rv.CreateProcedure(ro, "spPOPurchnRevisi_Insert");
                            if (rs == string.Empty)
                            {
                                int rste = rv.ProcessData(ro, "spPOPurchnRevisi_Insert");
                                if (rste > 0)
                                {
                                    Session["AlasanCancel"] = null;
                                }
                            }
                            #endregion
                            LoadPO(Session["NoPO"].ToString());

                        }
                        else
                        {
                            DisplayAJAXMessage(this, hPO.Error);
                        }

                    }

                }
            }


        }
        public void DetailPOEdit(string ID)
        {
            txtDetailPO.Visible = true;
            headerPO.Visible = false;
            DetailID.Text = ID.ToString();
            RevisiPOFacade dPO = new RevisiPOFacade();
            RevisiPO detail = dPO.RetrieveDetailID(int.Parse(ID), int.Parse(itemTypeID.Text));
            Session["Qty"] = 0;
            Session["ItemTypeID"] = string.Empty;
            Session["SPPID"] = 0;
            nmbarang.Text = detail.NamaBarang;
            itemID.Text = detail.ItemID.ToString();
            LoadddlItems(KodeBarang, detail.SPPID);
            ddlSatuan.Text = detail.UOMID.ToString();
            Qty.Text = detail.Qty.ToString();
            Price.Text = detail.Price.ToString();
            itemTpID.Text = detail.ItemTypeID.ToString();
            DelDate.Text = detail.DlvDate.ToString("dd/MM/yyyy");
            KodeBarang.SelectedValue = detail.ItemID.ToString();
            RevisiPO Hd = dPO.RetrieveByNo(NoPO.Text);
            DetailUp.Enabled = (Hd.Approval > 0 && ((Users)Session["Users"]).DeptID == 15) ? false : true;
            Session["Qty"] = detail.Qty;
            Session["ItemTypeID"] = detail.ItemTypeID;
            Session["SPPID"] = detail.SPPID;
            string[] arrUser = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("EditUser", "PO").Split(new string[] { "," }, StringSplitOptions.None);
            decimal QtyReceipt = dPO.CheckReceiptPO(detail.ID);
            if (QtyReceipt > 0)
            {
                txt.InnerHtml = "Item ini sudah di receipt sebanyak " + QtyReceipt.ToString("###,##0.00") + " " + ddlSatuan.SelectedItem.ToString() + " tidak bisa di edit lagi";
                DetailUp.Enabled = (arrUser.Contains(((Users)Session["Users"]).UserName) && Hd.Approval == 0) ? true : false;
            }
            else
            {
                txt.InnerHtml = "";
                DetailUp.Enabled = true;
            }
        }

        private void LoadddlItems(DropDownList ddl, int itemTypeID)
        {
            ArrayList arrItems = new ArrayList();

            SPPDetailFacade inventoryFacade = new SPPDetailFacade();
            AssetFacade assetFacade = new AssetFacade();
            BiayaFacade biayaFacade = new BiayaFacade();

            arrItems = inventoryFacade.RetrieveById(itemTypeID);
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (SPPDetail groupsPurchn in arrItems)
            {
                ddl.Items.Add(new ListItem(groupsPurchn.ItemName, groupsPurchn.ItemID.ToString()));
            }
        }
        private void LoadSatuan()
        {
            ArrayList arrUOM = new ArrayList();
            UOMFacade uOMFacade = new UOMFacade();
            arrUOM = uOMFacade.Retrieve1();
            ddlSatuan.Items.Add(new ListItem("-- Pilih Satuan --", "0"));
            foreach (UOM uOM in arrUOM)
            {
                ddlSatuan.Items.Add(new ListItem(uOM.UOMDesc, uOM.ID.ToString()));
            }
        }



        protected void btnUpdateAlasan_ServerClick(object sender, EventArgs e)
        {

            Session["AlasanCancel"] = txtAlasanCancel.Text;
            Session["AlasanBatal"] = txtAlasanCancel.Text;
            Session["AlasanTolak"] = txtAlasanCancel.Text;
            
            RevisiPO nPO = new RevisiPO();
            RevisiPOFacade iPo = new RevisiPOFacade();
            DetailUp.Enabled = false;
            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Tidak boleh kosong");
                    return;
                }
                RevisiPOFacade hPO = new RevisiPOFacade();
                RevisiPO arrSPP = new RevisiPO();
                RevisiPO SPP = hPO.RetrieveDetailID(int.Parse(DetailID.Text), int.Parse(itemTypeID.Text));
                decimal QtyPO = hPO.GetSPPQtyPO(SPP.SPPDetailID, "QtyPO");
                decimal QtySPP = hPO.GetSPPQtyPO(SPP.SPPDetailID, "Quantity");
                if (QtySPP < decimal.Parse(Qty.Text))
                {
                    DisplayAJAXMessage(this, "Quanity PO Melebihai Qty SPP");
                    return;
                }
                nPO.ItemID = int.Parse(KodeBarang.SelectedValue);
                nPO.Qty = decimal.Parse(Qty.Text);
                nPO.Price = (Price.Text == string.Empty) ? 0 : decimal.Parse(Price.Text);
                nPO.UOMID = int.Parse(ddlSatuan.SelectedValue);
                nPO.DlvDate = DateTime.Parse(DelDate.Text);
                RevisiPO dPO = iPo.RetrieveDetailID(int.Parse(DetailID.Text), int.Parse(itemTypeID.Text));
                nPO.POID = dPO.POID;
                nPO.SPPID = dPO.SPPID;
                nPO.SPPDetailID = dPO.SPPDetailID;
                nPO.DocumentNo = dPO.DocumentNo;
                nPO.GroupID = dPO.GroupID;
                nPO.ItemTypeID = dPO.ItemTypeID;
                nPO.NoUrut = dPO.NoUrut + 1;
                nPO.LastModifiedBy = ((Users)Session["Users"]).UserName;
                decimal QtyReceipt = iPo.CheckReceiptPO(nPO.ID);
                int result = iPo.Insert(nPO);
                if (iPo.Error == string.Empty && result > 0)
                {
                    /** hapus id sebelumnya*/
                    DetailPOHapus(DetailID.Text, "Edit");
                    LoadPO(NoPO.Text);
                }
                else
                {
                    DisplayAJAXMessage(this, iPo.Error);
                }

                
            }

            txtAlasanCancel.Text = string.Empty;

            
        }

        protected void btnUpdateClose_ServerClick(object sender, EventArgs e)
        {
            
        }
        protected void DetailUp_Click(object sender, EventArgs e)
        {
            panEdit.Visible = true;
            mpePopUp.Show();

            //RevisiPO nPO = new RevisiPO();
            //RevisiPOFacade iPo = new RevisiPOFacade();
            //DetailUp.Enabled = false;
            //if (Session["AlasanCancel"] != null)
            //{
            //    if (Session["AlasanCancel"].ToString() == string.Empty)
            //    {
            //        DisplayAJAXMessage(this, "Alasan Tidak boleh kosong");
            //        return;
            //    }
            //    RevisiPOFacade hPO = new RevisiPOFacade();
            //    RevisiPO arrSPP = new RevisiPO();
            //    RevisiPO SPP = hPO.RetrieveDetailID(int.Parse(DetailID.Text), int.Parse(itemTypeID.Text));
            //    decimal QtyPO = hPO.GetSPPQtyPO(SPP.SPPDetailID, "QtyPO");
            //    decimal QtySPP = hPO.GetSPPQtyPO(SPP.SPPDetailID, "Quantity");
            //    if (QtySPP < decimal.Parse(Qty.Text))
            //    {
            //        DisplayAJAXMessage(this, "Quanity PO Melebihai Qty SPP");
            //        return;
            //    }
            //    nPO.ItemID = int.Parse(KodeBarang.SelectedValue);
            //    nPO.Qty = decimal.Parse(Qty.Text);
            //    nPO.Price = (Price.Text == string.Empty) ? 0 : decimal.Parse(Price.Text);
            //    nPO.UOMID = int.Parse(ddlSatuan.SelectedValue);
            //    nPO.DlvDate = DateTime.Parse(DelDate.Text);
            //    RevisiPO dPO = iPo.RetrieveDetailID(int.Parse(DetailID.Text), int.Parse(itemTypeID.Text));
            //    nPO.POID = dPO.POID;
            //    nPO.SPPID = dPO.SPPID;
            //    nPO.SPPDetailID = dPO.SPPDetailID;
            //    nPO.DocumentNo = dPO.DocumentNo;
            //    nPO.GroupID = dPO.GroupID;
            //    nPO.ItemTypeID = dPO.ItemTypeID;
            //    nPO.NoUrut = dPO.NoUrut + 1;
            //    nPO.LastModifiedBy = ((Users)Session["Users"]).UserName;
            //    decimal QtyReceipt = iPo.CheckReceiptPO(nPO.ID);
            //    int result = iPo.Insert(nPO);
            //    if (iPo.Error == string.Empty && result > 0)
            //    {
            //        /** hapus id sebelumnya*/
            //        DetailPOHapus(DetailID.Text, "Edit");
            //        LoadPO(NoPO.Text);
            //    }
            //    else
            //    {
            //        DisplayAJAXMessage(this, iPo.Error);
            //    }
            //}
        }
        protected void DetailCn_Click(object sender, EventArgs e)
        {
            clearDetail(this);
            txtDetailPO.Visible = false;
            headerPO.Visible = true;

        }
        private void ClearForm()
        {
            NoPO.Text = string.Empty;
            ddlSupplierID.SelectedIndex = 0;
            ppn.Text = string.Empty;
            ddlMataUang.SelectedIndex = 0;
            Disc.Text = string.Empty;
            TermOfDelivery.Text = string.Empty;
            rmak.Text = string.Empty;
            ket.Text = string.Empty;
            POPurchDate.Text = string.Empty;
            kurs.Text = string.Empty;
            ongkos.Text = string.Empty;
            totalPrice.Text = string.Empty;
            txtTermOfPay.Visible = false;
            txtTermOfPay.Text = "";
            txtAlasanCancel.Text = string.Empty;
            txtAlasanCancel1.Text = string.Empty;
        }
        private void clearDetail(Control con)
        {
            itemTpID.Text = string.Empty;
            itemID.Text = string.Empty;
            ddlSatuan.SelectedIndex = 0;
            // KodeBarang.SelectedIndex = 0;
            Qty.Text = string.Empty;
            Price.Text = string.Empty;
            DetailID.Text = string.Empty;
            DelDate.Text = string.Empty;
            txtTermOfPay.Visible = false;
            txtTermOfPay.Text = "";
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void NoPO_TextChanged(object sender, EventArgs e)
        {
            clearDetail(this);
            LoadPO(NoPO.Text);
            if (checkreceipt(NoPO.Text) > 0)
                AprovalStatus.Enabled = false;
            else
            {
                AprovalStatus.Enabled = true;
                if (CekDP(NoPO.Text) > 0)
                    AprovalStatus.Enabled = false;
            }


        }
        protected int checkreceipt(string nopo)
        {
            int Treceipt = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select count(id)Treceipt from Receipt where status>-1 and poid in (select ID from popurchn where status>-1 and nopo='" + nopo + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Treceipt = Int32.Parse(sdr["Treceipt"].ToString());
                }
            }
            return Treceipt;
        }
        protected int CekDP(string nopo)
        {
            int Treceipt = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select count(id)Treceipt from TerminBayar where rowstatus>-1 and poid in (select ID from popurchn where status>-1 and approval=2 and nopo='" + nopo + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    Treceipt = Int32.Parse(sdr["Treceipt"].ToString());
                }
            }
            return Treceipt;
        }


        protected void btnUpdateClose1_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnUpdateAlasan1_ServerClick(object sender, EventArgs e)
        {
            Session["AlasanCancel"] = txtAlasanCancel1.Text;
            Session["AlasanBatal"] = txtAlasanCancel1.Text;
            Session["AlasanTolak"] = txtAlasanCancel1.Text;

            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Revisi harus diisi");
                    return;
                }
                RevisiPO arrHeader = new RevisiPO();
                RevisiPOFacade hPO = new RevisiPOFacade();
                //try
                //{
                arrHeader.ID = int.Parse(HeadID.Text);
                arrHeader.SupplierID = int.Parse(ddlSupplierID.Text);
                arrHeader.Termin = ddlTermin.Text;
                arrHeader.PPN = decimal.Parse(ppn.Text.ToString());
                arrHeader.PPH = decimal.Parse(pph.Text);
                arrHeader.Crc = int.Parse(ddlMataUang.Text);
                arrHeader.NilaiKurs = int.Parse(kurs.Text);
                arrHeader.Disc = decimal.Parse(Disc.Text);
                arrHeader.Ongkos = int.Parse(ongkos.Text);
                arrHeader.Delivery = TermOfDelivery.Text;
                arrHeader.Remark = rmak.Text;
                arrHeader.Keterangan = ket.Text;
                arrHeader.Status = int.Parse(txtStatus.Value.ToString());
                arrHeader.Approval = (appstat.Visible == true) ? int.Parse(appstat.Text) : int.Parse(AprovalStatus.SelectedValue);
                arrHeader.LastModifiedBy = ((Users)Session["users"]).UserID;
                if (ddlTermin.SelectedItem.Text.Trim().ToUpper() == "LAIN-LAIN")
                {
                    arrHeader.Termin = txtTermOfPay.Text.Trim();
                }
                else
                {
                    arrHeader.Termin = ddlTermin.SelectedItem.ToString();
                }

                int result = hPO.UpdateHeaderPO(arrHeader);
                if (hPO.Error == string.Empty && result >= 0)
                {
                    //LoadPO(NoPO.Text);
                    if (Convert.ToDecimal(Session["PPN"]) != Convert.ToDecimal(ppn.Text.ToString()) ||
                        Convert.ToDecimal(Session["PPH"]) != Convert.ToDecimal(pph.Text) ||
                        Convert.ToDecimal(Session["Disc"]) != Convert.ToDecimal(Disc.Text))
                    {
                        _UpdateTerbilang(int.Parse(HeadID.Text));
                    }
                    #region proses log approval le
                    EventLogProcess mp = new EventLogProcess();
                    EventLog evl = new EventLog();
                    mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                    mp.Pilihan = "Insert";
                    evl.UserID = ((Users)Session["Users"]).ID;
                    evl.AppLevel = ((Users)Session["Users"]).Apv;
                    evl.DocNo = NoPO.Text;
                    evl.DocType = "Revisi PO";
                    evl.AppDate = DateTime.Now;
                    evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                    mp.EventLogInsert(evl);
                    #endregion
                    #region simpan log transaksi revisi
                    Revisine ro = new Revisine();
                    RevisiPOnya rv = new RevisiPOnya();
                    rv.Criteria = "NoPO,POID,PODetailID,RevisiKe,AlasanRevisi,RevisiBy,RevisiTime,ItemID";
                    rv.Pilihan = "Insert";
                    rv.TableName = "POPurchnRevisi";
                    POPurchnFacade pod = new POPurchnFacade();
                    int IDn = int.Parse(Session["IDPO"].ToString());
                    POPurchn podetail = pod.RetrieveByID(int.Parse(HeadID.Text));
                    rv.Where = " where POID=" + podetail.ID.ToString();
                    //rv.Where += " and Convert(Char,RevisiTime,112) < ('" + DateTime.Now.ToString("yyyyMMdd") + "')";
                    ro.NoPO = podetail.NoPO;
                    ro.POID = podetail.ID;
                    ro.PODetailID = 0;
                    ro.ItemID = 0;
                    ro.RevisiKe = rv.GetRevisiNum() + 1;
                    ro.RevisiTime = DateTime.Now;
                    ro.AlasanRevisi = Session["AlasanCancel"].ToString();
                    ro.RevisiBy = ((Users)Session["Users"]).UserID.ToString();

                    string rs = rv.CreateProcedure(ro, "spPOPurchnRevisi_Insert");
                    if (rs == string.Empty)
                    {
                        int rest = rv.ProcessData(ro, "spPOPurchnRevisi_Insert");
                        if (rest > 0)
                        {
                            Session["AlasanCancel"] = null;
                        }
                    }
                    #endregion


                }
                else
                {
                    DisplayAJAXMessage(this, hPO.Error);
                }
                //}
                //catch (Exception ex)
                //{
                //    DisplayAJAXMessage(this, ex.Message);
                //}
                ClearForm();
            }
        }

        protected void simpan_Click(object sender, EventArgs e)
        {
            panEdit1.Visible = true;
            mpePopUp1.Show();
            /**
             * Update header PO
             */
          
        }


        protected void btnUpdateClose3_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnUpdateAlasan3_ServerClick(object sender, EventArgs e)
        {
            Session["AlasanCancel"] = txtAlasanCancel3.Text;
            Session["AlasanBatal"] = txtAlasanCancel3.Text;
            Session["AlasanTolak"] = txtAlasanCancel3.Text;

            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Cancel harus diisi");
                    return;
                }
                DetailPOEdit(Session["iddetailpo"].ToString());
                DetailPOHapus(Session["iddetailpo"].ToString(), "Hapus");
            }

            txtAlasanCancel3.Text = string.Empty;
            //Response.Write("<script language='javascript'>window.close();</script>");
        }

        protected void btnUpdateClose2_ServerClick(object sender, EventArgs e)
        {

        }

        protected void btnUpdateAlasan2_ServerClick(object sender, EventArgs e)
        {
            Session["AlasanCancel"] = txtAlasanCancel2.Text;
            Session["AlasanBatal"] = txtAlasanCancel2.Text;
            Session["AlasanTolak"] = txtAlasanCancel2.Text;
            /**
             * Hapus PO
             * Tombol Hapus yang di tekan dan hanya oleh level approval tertinggi dari status approval
             */
            RevisiPO arrHeader = new RevisiPO();
            RevisiPOFacade hPO = new RevisiPOFacade();
            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Cancel harus diisi");
                    return;
                }
                RevisiPO dPO = hPO.RetrieveByNo(NoPO.Text);
                //Check Receipt
                int SudahDireceipt = hPO.CheckPOReceipt(dPO.ID.ToString());
                if (SudahDireceipt > 0)
                {
                    DisplayAJAXMessage(this, "PO Sudah di Process Receipt tidak bisa di hapus");
                    return;
                }
                arrHeader.POID = dPO.ID;
                arrHeader.Keterangan = "Canceled by " + ((Users)Session["Users"]).UserName + "-" + HttpContext.Current.Request.UserHostAddress;
                arrHeader.LastModifiedBy = ((Users)Session["Users"]).UserName;
                if (dPO.Approval <= ((Users)Session["Users"]).Apv || ((Users)Session["Users"]).UserName == "Admin")
                {
                    int result = hPO.Delete(arrHeader);
                    if (hPO.Error == string.Empty && result > 0)
                    {
                        ArrayList arrData = new ArrayList();
                        arrData = hPO.DetailPO(dPO.ID.ToString());
                        foreach (RevisiPO rvp in arrData)
                        {
                            DetailPOHapus(rvp.ID.ToString(), "Delete");
                        }
                        LoadPO(NoPO.Text);
                        #region simpan log transaksi revisi
                        Revisine ro = new Revisine();
                        RevisiPOnya rv = new RevisiPOnya();
                        rv.Criteria = "NoPO,POID,PODetailID,RevisiKe,AlasanRevisi,RevisiBy,RevisiTime";
                        rv.Pilihan = "Insert";
                        rv.TableName = "POPurchnRevisi";
                        POPurchn podetail = new POPurchnFacade().RetrieveByID(dPO.ID);
                        rv.Where = " where POID=" + podetail.ID.ToString();
                        rv.Where += " and Convert(Char,RevisiTime,112)< ('" + DateTime.Now.ToString("yyyyMMdd") + "')";
                        ro.NoPO = podetail.NoPO;
                        ro.POID = podetail.ID;
                        ro.PODetailID = 0;
                        ro.RevisiKe = rv.GetRevisiNum() + 1;
                        ro.RevisiTime = DateTime.Now;
                        ro.AlasanRevisi = (Session["AlasanCancel"] != null) ? Session["AlasanCancel"].ToString() : "";
                        ro.RevisiBy = ((Users)Session["Users"]).UserID.ToString();
                        string rs = rv.CreateProcedure(ro, "spPOPurchnRevisi_Insert");
                        if (rs == string.Empty)
                        {
                            int rest = rv.ProcessData(ro, "spPOPurchnRevisi_Insert");
                            if (rest > 0)
                            {
                                Session["AlasanCancel"] = null;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    DisplayAJAXMessage(this, "Level Approval tidak mencukupi");
                }
            }
            txtAlasanCancel2.Text = string.Empty;
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            panEdit2.Visible = true;
            mpePopUp2.Show();
        }

        protected void _UpdateTerbilang(int POID)
        {
            decimal totalBaru = TotalPricePO(POID);
            TerbilangFacade terbilang = new TerbilangFacade();
            string kekata = terbilang.ConvertMoneyToWords(totalBaru);
            RevisiPO upTerbilang = new RevisiPO();
            RevisiPOFacade hPO = new RevisiPOFacade();

            upTerbilang.ID = POID;
            upTerbilang.Terbilang = kekata;
            int rest = hPO.UpdateTerbilang(upTerbilang);
            if (hPO.Error == string.Empty && rest > 0)
            {
                /**
                 * Tampilkan hasil update ke grid
                 **/
                LoadPO(Session["NoPO"].ToString());

            }
            else
            {
                DisplayAJAXMessage(this, hPO.Error);
            }
        }
        protected void nmbarang_TextChanged(object sender, EventArgs e)
        {
            LoadddlItems(KodeBarang, int.Parse(Session["SPPID"].ToString()));
        }
        protected void ddlTermin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTermin.SelectedItem.Text.Trim().ToUpper() == "LAIN-LAIN")
            {
                txtTermOfPay.Visible = true;
            }
            else
            {
                txtTermOfPay.Visible = false;
            }
        }
    }
    public class RevisiPOnya
    {
        private ArrayList arrData = new ArrayList();
        private Revisine hlp = new Revisine();
        private List<SqlParameter> sqlListParam;
        public string Criteria { get; set; }
        public string TableName { get; set; }
        public string Pilihan { get; set; }
        public string Where { get; set; }
        public int ProcessData(object help, string spName)
        {
            try
            {
                hlp = (Revisine)help;
                string[] arrCriteria = this.Criteria.Split(',');
                PropertyInfo[] data = hlp.GetType().GetProperties();
                DataAccess da = new DataAccess(Global.ConnectionString());
                var equ = new List<string>();
                sqlListParam = new List<SqlParameter>();
                foreach (PropertyInfo items in data)
                {
                    if (items.GetValue(hlp, null) != null && arrCriteria.Contains(items.Name))
                    {
                        sqlListParam.Add(new SqlParameter("@" + items.Name.ToString(), items.GetValue(hlp, null)));
                    }
                }
                int result = da.ProcessData(sqlListParam, spName);
                string err = da.Error;
                return result;
            }
            catch (Exception ex)
            {
                string er = ex.Message;
                return -1;
            }
        }
        public string CreateProcedure(object help, string spName)
        {
            string message = string.Empty;
            hlp = (Revisine)help;
            string[] arrCriteria = this.Criteria.Split(',');
            PropertyInfo[] data = hlp.GetType().GetProperties();
            DataAccess da = new DataAccess(Global.ConnectionString());
            string param = "";
            string value = "";
            string field = "";
            string FieldUpdate = "";
            try
            {
                foreach (PropertyInfo items in data)
                {
                    if (arrCriteria.Contains(items.Name))
                    {
                        param += "@" + items.Name.ToString() + " " + GetTypeData(this.TableName, items.Name.ToString()) + ",";
                        value += "@" + items.Name.ToString() + ",";
                        field += items.Name.ToString() + ",";
                        if (items.Name.ToString() != "ID")
                            FieldUpdate += items.Name.ToString() + "=@" + items.Name.ToString() + ",";
                    }
                }
                string strSQL = "CREATE PROCEDURE " + spName + " " + param.Substring(0, param.Length - 1) +
                                " AS BEGIN SET NOCOUNT ON; ";
                if (this.Pilihan == "Insert")
                {
                    strSQL += "INSERT INTO " + this.TableName + " (" + field.Substring(0, field.Length - 1).ToString() + ")VALUES(" +
                     value.Substring(0, value.Length - 1) + ") SELECT @@IDENTITY as ID";
                }
                else
                {
                    strSQL += "UPDATE " + this.TableName + " set " + FieldUpdate.Substring(0, FieldUpdate.Length - 1).ToString() + " where ID=@ID SELECT @@ROWCOUNT";
                }
                strSQL += " END";
                SqlDataReader result = da.RetrieveDataByString(strSQL);
                if (result != null)
                {
                    message = string.Empty;
                }
                else
                {
                    message = "";
                }
                return message;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return message;
            }
        }
        private string GetTypeData(string TableName, string ColumName)
        {
            string result = string.Empty;
            string strsql = "select DATA_TYPE,CHARACTER_MAXIMUM_LENGTH from INFORMATION_SCHEMA.COLUMNS IC where " +
                            "TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + ColumName + "'";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strsql);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["DATA_TYPE"].ToString() + " ";
                    result += (sdr["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value) ? "(" + sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")" : "";
                    if (sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() == "-1")
                    {
                        result = result.Replace("-1", "Max");
                    }
                }
            }
            return result;
        }
        public int GetRevisiNum()
        {
            int result = 0;
            string strSQL = "Select isnull(Max(RevisiKe),0)RevisiKe from POPurchnRevisi " + this.Where + " /*group by convert(char,RevisiTime,112)*/,POID";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = Convert.ToInt32(sdr["RevisiKe"].ToString());
                }
            }
            return result;
        }
    }
    public class Revisine : POPurchn
    {
        
        public int PODetailID { get; set; }
        public int RevisiKe { get; set; }
        public string AlasanRevisi { get; set; }
        public string RevisiBy { get; set; }
        public DateTime RevisiTime { get; set; }
        
    }
}
