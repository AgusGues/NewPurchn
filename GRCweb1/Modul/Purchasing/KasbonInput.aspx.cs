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
using System.Threading;
using System.Globalization;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
//using System.Drawing;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Web.Script.Serialization;

namespace GRCweb1.Modul.Purchasing
{
    
    public partial class KasbonInput : System.Web.UI.Page
    {
        public decimal gTotal = 0;
        public decimal gHarga = 0;
        public decimal totalkasbon = 0;

        string TanggalBulanTahun = DateTime.Now.ToString("dd-MM-yyyy");
        string Bulan = DateTime.Now.ToString("MM");
        string Tahun = DateTime.Now.ToString("yyyy");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Users users = (Users)Session["Users"];
                if (users.DeptID == 15)
                {
                    btnPrint.Visible = false;
                }
                if (users.DeptID == 12)
                {
                    btnCancel.Visible = false;
                }
                txtDept.Text = "Purchasing";
                LoadPIC();
                txtDate.SelectedDate = DateTime.Now;
                txtDate.Enabled = false;
                btnCancel.Enabled = false;
                if (Request.QueryString["NoPengajuan"] != null)
                {
                    clearForm();
                    LoadKasbon(Request.QueryString["NoPengajuan"].ToString());
                    btnUpdate.Enabled = false;
                    btnCancel.Enabled = true;
                    txtCancel.Visible = true;
                    txtCancel.Attributes.Add("placeholder", "1," + Environment.NewLine + "2," + Environment.NewLine + "3,");
                    int Apv = 0;
                    int CetakKasbon = 0;
                    int Status = 0;
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "SELECT Approval,CetakKasbon, Status FROM Kasbon WHERE NoPengajuan='" + Request.QueryString["NoPengajuan"].ToString() + "' ";
                    SqlDataReader sdr = zl.Retrieve();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            Apv = Convert.ToInt32(sdr["Approval"].ToString());
                            CetakKasbon = Convert.ToInt32(sdr["CetakKasbon"].ToString());
                            Status = Convert.ToInt32(sdr["Status"].ToString());
                        }
                        if (users.DeptID == 12 && Apv == 2 && CetakKasbon == 0)
                        {
                            btnPrint.Enabled = true;
                        }
                        else if (Status == -1)
                        {
                            btnCancel.Visible = false;
                            txtCancel.Visible = false;
                        }
                        else
                        {
                            btnPrint.Enabled = false;
                        }
                    }
                }
            }
        }

        private void LoadPIC()
        {
            Users users = (Users)Session["Users"];
            ArrayList arrKasbon = new ArrayList();
            KasbonFacade kasbonFacade = new KasbonFacade();
            arrKasbon = kasbonFacade.RetrievePIC(users.DeptID);
            ddlPIC.Items.Add(new ListItem("-- Pilih PIC --", string.Empty));
            //ddlSupPurch.Items.Add(new ListItem(" ", string.Empty));
            foreach (Kasbon kasbon in arrKasbon)
            {
                ddlPIC.Items.Add(new ListItem(kasbon.UserName, kasbon.ID.ToString()));
            }
        }
        private void LoadKasbon(string strNoPengajuan)
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();

            Kasbon kasbon = kasbonFacade.RetrieveByNoWithKasbonNO(strNoPengajuan);
            try
            {
                if (kasbonFacade.Error == string.Empty && kasbon.ID > 0)
                {
                    Session["id"] = kasbon.ID;
                    txtNoPengajuan.Text = kasbon.NoPengajuan;
                    txtSPP.Text = kasbon.NoSPP;
                    txtNoKasbon.Text = kasbon.NoKasbon;
                    txtJumlahSPP.Text = kasbon.Qty.ToString();
                    txtEstimasiKasbon.Text = kasbon.EstimasiKasbon.ToString();
                    ddlItemSPP.ClearSelection();
                    ddlPIC.ClearSelection();

                    KasbonFacade kasbonFacade3 = new KasbonFacade();
                    ArrayList arrItemList = new ArrayList();
                    arrItemList = kasbonFacade3.ViewGridKasbon(kasbon.ID);
                    //arrItemList.Add(kasbon);
                    Session["NoPengajuan"] = strNoPengajuan;
                    lstKasbon.DataSource = arrItemList;
                    lstKasbon.DataBind();
                }
                else
                {
                    DisplayAJAXMessage(this, "No. Kasbon tersebut tidak bisa ditampilkan karena tidak ada, atau telah dicancel");
                    return;
                }
            }

            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
            }
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("ListOfKasbonDetail");
            Response.Redirect(Request.Url.AbsolutePath);
            clearForm();
            //Response.Redirect("KasbonInput.aspx");
        }
        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            if (txtCancel.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Alasan Cancel tidak boleh kosong");
                return;
            }

            string strError = string.Empty;
            KasbonFacade kasbonFacade = new KasbonFacade();
            Kasbon kasbon = kasbonFacade.RetrieveByNo(txtNoPengajuan.Text);
            Users users = (Users)Session["Users"];
            if (kasbonFacade.Error == string.Empty && kasbon.ID > 0)
            {
                kasbon.AlasanNotApproval = txtCancel.Text;
                kasbon.Apv = 0;
                kasbon.CreatedBy = users.UserName;
                if (kasbon.ID > 0)
                {
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "UPDATE Kasbon set Status=-1, AlasanNotApproved='" + kasbon.AlasanNotApproval + "', LastModifiedBy='" + kasbon.CreatedBy + "', " +
                                     "LastModifiedTime=getdate(), ApprovalCancel=0, TglCancel=getdate() WHERE ID=" + kasbon.ID + " ";
                    SqlDataReader sdr = zl.Retrieve();
                }
                Response.Redirect("KasbonInput.aspx");
            }
        }
        private void clearForm()
        {
            ViewState["id"] = null;
            Session["NoPengajuan"] = null;
            Session.Remove("id");
            Session["ListOfKasbonDetail"] = null;


            txtDate.SelectedDate = DateTime.Now;
            txtSPP.Text = string.Empty;
            txtEstimasiKasbon.Text = string.Empty;
            ddlItemSPP.Items.Clear();
            ArrayList arrList = new ArrayList();
            //arrList.Add(new SPPDetail());
            lstKasbon.DataSource = arrList;
            lstKasbon.DataBind();
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            string strValidate = ValidateText();
            decimal totalPrice = 0;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            string strEvent = "Insert";
            if (Session["TotalPrice"] != null)
            {
                totalPrice = (decimal)Session["TotalPrice"];
            }

            Kasbon kasbon = new Kasbon();
            if (Session["id"] != null)
            {
                kasbon.ID = int.Parse(Session["id"].ToString());
                strEvent = "Edit";
            }

            KasbonFacade kasbonFacade = new KasbonFacade();
            kasbon = kasbonFacade.RetrieveNumberID();
            if (kasbonFacade.Error == string.Empty)
            {
                if (kasbon.ID > 0)
                {
                    kasbon.KasbonCounter = kasbon.KasbonCounter + 1;
                }
            }

            string no_pengajuan = string.Empty;
            string kas = txtDate.SelectedDate.ToString("yyyy");
            //int urutan = kasbonFacade.GetLastUrutan(Convert.ToInt32(txtDate.SelectedDate.ToString("yyyy"))) + 1;
            int urutan = kasbon.KasbonCounter + 1;
            string bulanR = Global.ConvertNumericToRomawi(Convert.ToInt32(txtDate.SelectedDate.ToString("MM")));
            CompanyFacade companyFacade = new CompanyFacade();
            string ErMM = " ";
            string code = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            //no_pengajuan = urutan.ToString().PadLeft(4, '0').Trim() + "/" + code + "/" + bulanR + "/" + txtDate.SelectedDate.ToString("yy");
            no_pengajuan = kasbon.KasbonCounter.ToString().PadLeft(4, '0').Trim() + "/" + code + "/" + bulanR + "/" + txtDate.SelectedDate.ToString("yy");
            if (txtNoPengajuan.Text == string.Empty)
            {
                txtNoPengajuan.Text = no_pengajuan;
                ErMM = no_pengajuan;
            }
            else
            {
                ErMM = txtNoPengajuan.Text;
                txtNoPengajuan.Text = txtNoPengajuan.Text.Substring(0, txtNoPengajuan.Text.Trim().Length - 5) +
                    txtNoPengajuan.Text.Substring(txtNoPengajuan.Text.Trim().Length - 3, 3);
            }

            kasbon.NoPengajuan = txtNoPengajuan.Text;
            kasbon.KasbonDate = txtDate.SelectedDate;
            kasbon.DeptID = users.DeptID;
            //kasbon.ItemID = Convert.ToInt32(ddlItemSPP.SelectedValue);
            kasbon.PIC = ddlPIC.SelectedItem.ToString();
            kasbon.Status = 0;
            kasbon.Apv = 0;
            kasbon.CreatedBy = users.UserName;
            kasbon.KasbonType = Convert.ToInt32(rbTopUrgent.Checked);
            //kasbon.KasbonCounter = kasbon.KasbonCounter + 1;
            if (rbTopUrgent.Checked == false)
            {
                TextBox txtAns = (TextBox)lstKasbon.Controls[lstKasbon.Controls.Count - 1].FindControl("txtDanaCadangan");
                if (txtAns.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Dana Cadangan Harus Diisi");
                    return;
                }
                kasbon.DanaCadangan = Convert.ToDecimal(txtAns.Text);
            }
            else
            {
                TextBox txtAns = (TextBox)Repeater1.Controls[Repeater1.Controls.Count - 1].FindControl("txtDanaCadangan");
                if (txtAns.Text == string.Empty)
                {
                    DisplayAJAXMessage(this, "Dana Cadangan Harus Diisi");
                    return;
                }
                kasbon.DanaCadangan = Convert.ToDecimal(txtAns.Text);
            }

            string strError = string.Empty;
            ArrayList arrKasbonDetail = new ArrayList();
            if (Session["ListOfKasbonDetail"] != null)
            {
                arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];
            }

            KasbonProcessFacade kasbonProcessFacade = new KasbonProcessFacade(kasbon, arrKasbonDetail);
            if (kasbon.ID > 0)
            {
                strError = kasbonProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtNoPengajuan.Text = kasbonProcessFacade.NoPengajuan;
                    Session["ID"] = kasbon.ID;
                    Session["NoPengajuan"] = kasbonProcessFacade.NoPengajuan;

                }
            }
            else
            {
                //strError = kasbonProcessFacade.Insert();
                //if (strError == string.Empty)
                //{
                //    txtNoPengajuan.Text = kasbonProcessFacade.NoPengajuan;
                //    Session["ID"] = kasbon.ID;
                //    Session["NoPengajuan"] = kasbonProcessFacade.NoPengajuan;

                //}
            }

            if (strError == string.Empty)
            {

                InsertLog(strEvent);
                btnUpdate.Enabled = false;
                if (strEvent == "Edit")
                    ClearForm();
            }

        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Session["ListOfPODetail"] = null;
            Session["NoSPP"] = null;

            Response.Redirect("KasbonList.aspx?approve=" + (((Users)Session["Users"]).GroupID));
        }

        //protected void btnRevisi_ServerClick(object sender, EventArgs e)
        //{
        //    Users users = (Users)Session["Users"];
        //    KasbonFacade kasbonFacade = new KasbonFacade();
        //    Kasbon kasbon = new Kasbon();

        //    txtJumlahSPP.Text = kasbon.Qty.ToString();
        //    txtEstimasiKasbon.Text = kasbon.EstimasiKasbon.ToString();
        //    kasbon.NoSPP = txtSPP.Text;

        //    if (kasbon.ID > 0)
        //    {
        //        ZetroView zl = new ZetroView();
        //        zl.QueryType = Operation.CUSTOM;

        //        zl.CustomQuery = "UPDATE KasbonDetail set Qty='" + kasbon.Qty + "', EstimasiKasbon='" + kasbon.EstimasiKasbon + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
        //        SqlDataReader sdr = zl.Retrieve();
        //    }
        //}

        private string ValidateText()
        {
            return string.Empty;
        }

        private void ClearForm()
        {
            ViewState["id"] = null;
            Session.Remove("id");
            Session["NoPengajuan"] = null;
            Session["ListOfKasbonDetail"] = null;
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "KASBON";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtNoPengajuan.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        protected void txtSPP_TextChanged(object sender, EventArgs e)
        {
            ddlItemSPP.Items.Clear();

            TextBox txl = (TextBox)sender;

            if (txl.Text != string.Empty)
            {
                if (ddlItemSPP.SelectedIndex == 0)
                {
                    DisplayAJAXMessage(this, "Kode Barang tidak boleh kosong");
                    txl.Text = string.Empty;
                    return;
                }

                DropDownList ddlName = (DropDownList)ddlItemSPP;
                LoadItem(ddlName, txl.Text);
            }
        }

        protected void txtDanaCadangan_TextChanged(object sender, EventArgs e)
        {
            TextBox tb1 = (TextBox)sender;
            RepeaterItem rp1 = ((RepeaterItem)(tb1.NamingContainer));

            TextBox dana = (TextBox)rp1.FindControl("txtDanaCadangan");
            TextBox total = (TextBox)rp1.FindControl("txtTotal");
            TextBox gtot = (TextBox)rp1.FindControl("gTotal");
            if (dana.ToString() != string.Empty)
            {
                total.Text = Convert.ToString(Convert.ToDouble(gtot.Text) + Convert.ToDouble(dana.Text));

                //total.Text = string.Format("###,##0.#0");

            }

        }
        private void LoadItem(DropDownList ddlName, string strNoSPP)
        {
            Users users = (Users)Session["Users"];
            int depoID = users.UnitKerjaID;
            strNoSPP = txtSPP.Text;

            SPPFacade sPPFacade = new SPPFacade();
            SPP sPP = new SPP();
            sPP = sPPFacade.RetrieveByNo(strNoSPP);
            if (sPPFacade.Error == string.Empty)
            {
                if (sPP.Approval == 0)
                {
                    DisplayAJAXMessage(this, "SPP tersebut belum di-approval Head Dept");
                    txtSPP.Text = string.Empty;
                    ddlItemSPP.Items.Clear();
                    return;
                }
                if (sPP.Approval == 1)
                {
                    DisplayAJAXMessage(this, "SPP tersebut belum di-approval Manager Dept");
                    txtSPP.Text = string.Empty;
                    ddlItemSPP.Items.Clear();
                    return;
                }
                if (sPP.Approval == 2)
                {
                    DisplayAJAXMessage(this, "SPP tersebut belum di-approval Plant Manager");
                    txtSPP.Text = string.Empty;
                    ddlItemSPP.Items.Clear();
                    return;
                }
                string strNoSPP2 = sPP.NoSPP;

                //string test3 = sPP.ID.ToString();

                ArrayList arrSPPDetail = new ArrayList();
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                AccClosingFacade biaya = new AccClosingFacade();
                AccClosing aktif = biaya.BiayaNewActive();
                Session["ForKasbon"] = "yes";
                int tgl = (aktif.CreatedTime <= sPP.CreatedTime) ? 1 : 0;
                arrSPPDetail = sPPDetailFacade.RetrieveBySPPID(sPP.ID);
                if (sPPDetailFacade.Error == string.Empty)
                {
                    ddlItemSPP.Items.Add(new ListItem("-- Pilih Item SPP --", string.Empty));

                    foreach (SPPDetail sPPDetail in arrSPPDetail)
                    {
                        if (sPPDetail.ItemTypeID == 3 && tgl == 1)
                        {
                            ddlItemSPP.Items.Add(new ListItem(sPPDetail.ItemName, sPPDetail.ID.ToString()));
                        }
                        else
                        {
                            ddlItemSPP.Items.Add(new ListItem(sPPDetail.ItemName, sPPDetail.ID.ToString()));
                        }
                    }
                }
            }
        }

        protected void ddlItemSPP_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            Users users = (Users)Session["Users"];
            SPPFacade sp = new SPPFacade();
            SPP sppDate = sp.RetrieveByNo(txtSPP.Text);
            string ItemeID = "";
            if (ddl.SelectedIndex > 0)
            {
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                SPPDetail sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(int.Parse(ddl.SelectedValue));
                POPurchnDetailFacade podetail = new POPurchnDetailFacade();
                LastPrice lastPrice = new LastPrice();
                Session["groupidpo"] = sPPDetail.GroupID;
                //lastPrice = podetail.GetLastPrice(int.Parse(ddl.SelectedValue), users.ViewPrice);
                if (sPPDetailFacade.Error == string.Empty)
                {

                    if (sPPDetail.ID > 0)
                    {
                        //ddlTipeSPP.SelectedValue = sPPDetail.GroupID.ToString();
                        //txtDate0.Text = sPPDetail.TglKirim.ToString("dd-MMM-yyyy"); //aktifkan saat leadtime diterapkan
                        AccClosingFacade biaya = new AccClosingFacade();
                        AccClosing aktif = biaya.BiayaNewActive();
                        int tgl = (aktif.CreatedTime <= sppDate.CreatedTime) ? 1 : 0;
                        //txtNamaBarang.Text = (CheckStatusBiaya() == 1 && sPPDetail.ItemTypeID == 3 && tgl == 1) ? sPPDetail.Keterangan : sPPDetail.ItemName;
                        //sehingga qty sisa spp yg terambil
                        //txtJumlahSPP.Text = (sPPDetail.Quantity - sPPDetail.QtyPO).ToString("N2");
                        txtJumlahSPP.Text = sPPDetail.Quantity.ToString("N2");

                        //Session["QuantitySPP"] = sPPDetail.Quantity - sPPDetail.QtyPO;
                        Session["QuantitySPP"] = sPPDetail.Quantity;
                        //txtSatuan.Text = sPPDetail.Satuan;
                        if (sPPDetail.ItemTypeID == 3)
                        {
                            string[] ItemName = ddlItemSPP.SelectedItem.Text.Split('-');
                            ItemeID = new POPurchnFacade().GetIDBiaya(ItemName[1].ToString());

                        }
                        else
                        {
                            ItemeID = sPPDetail.ItemID.ToString();
                        }
                        //DateTime nextDate = SchDelivery(sPPDetail.SPPID.ToString(), ItemeID);// AddBusinessDays(sppDate.ApproveDate2, CheckLeadTime(sPPDetail.ItemID));
                        //txtDate0.Text = nextDate.ToString("dd-MMM-yyyy");
                        //txtItemCode.Text = sPPDetail.ItemCode;
                        //txtIDBiaya.Text = ItemIDBiayaOld(sPPDetail.Keterangan);
                        txtEstimasiKasbon.Text = lastPrice.Price.ToString();
                        string[] arrUnLock = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnLockItemCode", "PO").Split(',');
                        //leadTime.InnerHtml = (ItemeID != string.Empty) ? "Lead Time :" + CheckLeadTime(int.Parse(ItemeID.ToString()), sPPDetail.ItemTypeID).ToString() + "WD" : "";
                        //txtDate0.Enabled = (!arrUnLock.Contains(sPPDetail.ItemCode) && CheckLeadTime(sPPDetail.ItemID, sPPDetail.ItemTypeID) > 0 && CheckDeliveStatus() > 0) ? false : true;
                        //txtDate0.Enabled = (sppDate.PermintaanType < 3) ? txtDate0.Enabled : true;
                        //if (sppDate.GroupID == 4 || sppDate.GroupID == 5)
                        //{
                        //    txtDate0.Enabled = true;
                        //}
                        //else
                        //{
                        //    txtDate0.Enabled = false;
                        //}
                        //if (lastPrice.Crc > 0)
                        //    ddlMataUang.SelectedValue = lastPrice.Crc.ToString();
                        // SelectTipeSPP(sPPDetail.GroupID);
                        //if (sPPDetail.Quantity - sPPDetail.QtyPO <= 0)
                        //{
                        //    DisplayAJAXMessage(this, "Qty <= Nol ..!");
                        //    return;
                        //}
                    }
                }
            }
        }


        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        protected void lbAddItem_Click(object sender, EventArgs e)
        {
            int jmlItem = 0;

            if (ViewState["Baris"] != null)
            {
                jmlItem = (int)ViewState["Baris"];
                jmlItem = jmlItem + 1;
            }
            else
            {
                jmlItem = 1;
            }
            if (rbTopUrgent.Checked == false)
            {
                string strValidate = ValidateItem();
                if (strValidate != string.Empty)
                {
                    DisplayAJAXMessage(this, strValidate);
                    return;
                }
            }
            ArrayList arrKasbonDetail = new ArrayList();
            decimal totalPrice = 0;
            if (Session["ListOfKasbonDetail"] != null)
            {
                arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];
            }
            if (Session["TotalPrice"] != null)
            {
                totalPrice = (decimal)Session["TotalPrice"];
            }

            //ambil data SPP
            if (rbTopUrgent.Checked == false)
            {
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                SPPDetail sPPDetail = new SPPDetail();
                //int zSPPDetailID = Convert.ToInt32(Session["SPPDetailID"].ToString());utk edit PO
                sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(int.Parse(ddlItemSPP.SelectedValue));
                //sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(zSPPDetailID); -- utk edit PO
                if (sPPDetailFacade.Error == string.Empty)
                {
                    //if ((sPPDetail.Quantity - (sPPDetail.QtyPO + decimal.Parse(txtJumlahSPP.Text))) < 0)
                    //{
                    //    DisplayAJAXMessage(this, "Qty PO melebihi Qty SPP ..!");
                    //    return;
                    //}
                    if (sPPDetail.ItemTypeID < 1 && sPPDetail.ItemTypeID > 3)
                    {
                        DisplayAJAXMessage(this, "Items tersebut tidak termasuk dalam Tipe Barang");
                        return;
                    }
                }

                //check lagi apaka sudah pernah di buat po atau belum spp tersebut
                POPurchnDetailFacade poDetail = new POPurchnDetailFacade();
                decimal qtyPO4SPP = poDetail.CheckQtyPO(sPPDetail.ID.ToString());
                //if ((qtyPO4SPP + decimal.Parse(txtJumlahSPP.Text)) > sPPDetail.Quantity)
                //{
                //    DisplayAJAXMessage(this, "Quantity PO sudah melebihi Quantity SPP");
                //    //nilaiKurs.Focus();
                //    return;
                //}

                if (lbAddItem.Text == "Add Item")
                {
                    Kasbon kasbon = new Kasbon();
                    KasbonDetail kasbonDetail = new KasbonDetail();
                    InventoryFacade inventoryFacade = new InventoryFacade();

                    int intItemsID = 0;
                    if (sPPDetail.ItemTypeID == 1)
                    {
                        Inventory inv = inventoryFacade.RetrieveById(sPPDetail.ItemID);
                        if (inventoryFacade.Error == string.Empty && inv.ID > 0)
                            intItemsID = inv.ID;
                    }
                    if (sPPDetail.ItemTypeID == 2)
                    {
                        Inventory ase = inventoryFacade.AssetRetrieveById(sPPDetail.ItemID);
                        if (inventoryFacade.Error == string.Empty && ase.ID > 0)
                            intItemsID = ase.ID;
                    }
                    if (sPPDetail.ItemTypeID == 3)
                    {
                        Inventory bia = inventoryFacade.BiayaRetrieveById(sPPDetail.ItemID);
                        if (inventoryFacade.Error == string.Empty && bia.ID > 0)
                            intItemsID = bia.ID;
                    }

                    if (intItemsID > 0)
                    {
                        kasbonDetail.NoSPP = txtSPP.Text;
                        kasbonDetail.NamaBarang = ddlItemSPP.SelectedItem.ToString();
                        kasbonDetail.Qty = decimal.Parse(txtJumlahSPP.Text);
                        kasbonDetail.EstimasiKasbon = decimal.Parse(txtEstimasiKasbon.Text);

                        kasbonDetail.ItemID = sPPDetail.ItemID;
                        kasbonDetail.Satuan = sPPDetail.Satuan;
                        kasbonDetail.GroupID = sPPDetail.GroupID;
                        kasbonDetail.ItemTypeID = sPPDetail.ItemTypeID;
                        kasbonDetail.UOMID = sPPDetail.UOMID;
                        kasbonDetail.SPPID = sPPDetail.SPPID;

                        string intSppDetailID = int.Parse(ddlItemSPP.SelectedValue).ToString();
                        kasbonDetail.SPPDetailID = int.Parse(intSppDetailID);

                        arrKasbonDetail.Add(kasbonDetail);
                        totalPrice = totalPrice + (decimal.Parse(txtJumlahSPP.Text) * decimal.Parse(txtEstimasiKasbon.Text));
                    }
                }
            }

            ViewState["Baris"] = jmlItem;
            Session["TotalPrice"] = totalPrice;
            Session["ListOfKasbonDetail"] = arrKasbonDetail;

            GridView2.DataSource = arrKasbonDetail;
            GridView2.DataBind();
            lstKasbon.DataSource = arrKasbonDetail;
            lstKasbon.DataBind();

        }

        protected void lbLAdd_Click(object sender, EventArgs e)
        {
            int jmlItem = 0;

            if (ViewState["Baris"] != null)
            {
                jmlItem = (int)ViewState["Baris"];
                jmlItem = jmlItem + 1;
            }
            else
            {
                jmlItem = 1;
            }

            ArrayList arrKasbonDetail = new ArrayList();
            decimal totalPrice = 0;
            if (Session["ListOfKasbonDetail"] != null)
            {
                arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];
            }
            if (Session["TotalPrice"] != null)
            {
                totalPrice = (decimal)Session["TotalPrice"];
            }

            if (lbAddItem.Text == "Add Item")
            {
                Kasbon kasbon = new Kasbon();
                KasbonDetail kasbonDetail = new KasbonDetail();
                int intItemsID = 0;

                kasbonDetail.NamaBarang = txtNamaBarang.Text;
                kasbonDetail.Qty = decimal.Parse(txtJumlahSPP.Text);
                kasbonDetail.EstimasiKasbon = decimal.Parse(txtEstimasiKasbon.Text);

                arrKasbonDetail.Add(kasbonDetail);
                totalPrice = totalPrice + (decimal.Parse(txtJumlahSPP.Text) * decimal.Parse(txtEstimasiKasbon.Text));
            }

            ViewState["Baris"] = jmlItem;
            Session["TotalPrice"] = totalPrice;
            Session["ListOfKasbonDetail"] = arrKasbonDetail;

            Repeater1.DataSource = arrKasbonDetail;
            Repeater1.DataBind();
        }

        private int CheckStatusBiaya()
        {
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing stat = cls.CheckBiayaAktif();

            return stat.Status;
        }

        private string ValidateItem()
        {
            if (ddlPIC.SelectedIndex == 0)
                return "Pilih Nama PIC";
            else if (ddlItemSPP.SelectedIndex == 0)
                return "Pilih Item Barang dari SPP yang bersangkutan";
            else if (txtSPP.Text == string.Empty)
                return "Isi No. SPP";

            try
            {
                decimal dec = decimal.Parse(txtEstimasiKasbon.Text);
            }
            catch
            {
                return "Estimasi Kasbon harus numeric";
            }

            try
            {
                decimal dec = decimal.Parse(txtJumlahSPP.Text);
            }
            catch
            {
                return "Jumlah SPP harus numeric";
            }

            if (decimal.Parse(txtJumlahSPP.Text) <= 0)
                return "Quantity harus lebih dari Nol";
            return string.Empty;
        }

        protected void lstKasbon_DataBound(object sender, RepeaterItemEventArgs e)
        {
            if (txtNoPengajuan.Text == string.Empty)
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    KasbonDetail kasbonn = (KasbonDetail)e.Item.DataItem;
                    decimal tharga = kasbonn.Qty * kasbonn.EstimasiKasbon;
                    gHarga += tharga;
                    //  ((System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("dlv")).Attributes.Add("title", "SPP Approval date : ");
                    //((Label)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                }

                if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                }
            }
            else
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    ((TextBox)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                }
            }
        }

        private void SelectItem(string IntSPPDetailID, string strItemName)
        {
            Session["SPPDetailID"] = IntSPPDetailID;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                // blom ada pengecekan ke spp sdh full / blom
                GridViewRow row = GridView1.Rows[index];
                //LoadAllItems();            

                // in utk apa ??????????????????????????????????????????????????????????
                SelectItem(row.Cells[0].Text, row.Cells[2].Text);

                //txtQty.Text = row.Cells[2].Text;
                //ddlItemName.Enabled = false;
                txtJumlahSPP.Focus();
            }
            if (e.CommandName == "AddDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrKasbonDetail = new ArrayList();
                arrKasbonDetail = (ArrayList)Session["ListOfkasbonDetail"];
                Kasbon kasbonDetail = (Kasbon)arrKasbonDetail[index];
                string xNoSPP = kasbonDetail.NamaBarang;
                int zSPPDetailID = kasbonDetail.SPPDetailID;
                int zPOPurchnDetailID = kasbonDetail.ID;
                decimal zjml = kasbonDetail.Qty;

                SPPDetail sPPDetail = new SPPDetail();
                sPPDetail.ID = zSPPDetailID;
                sPPDetail.QtyPO = zjml;

                POPurchnDetail sOPurchnDetail = new POPurchnDetail();
                sOPurchnDetail.ID = zPOPurchnDetailID;


                MinusQtyPOFacade minusQtyPOFacade = new MinusQtyPOFacade(sPPDetail, sOPurchnDetail);
                string strError = minusQtyPOFacade.MinusQtyPO();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, strError);
                    return;
                }
                arrKasbonDetail.RemoveAt(index);
                Session["ListOfTransferDetail"] = arrKasbonDetail;
                GridView1.DataSource = arrKasbonDetail;
                GridView1.DataBind();

                //decimal decTonase = 0;
                //foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
                //{
                //    decTonase = decTonase + (transferDetail.Berat * transferDetail.Qty);
                //}

                //txtKubikasi.Text = decTonase.ToString();
            }
            if (e.CommandName == "Hapus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrKasbonDetail = new ArrayList();
                arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];
                Kasbon kasbonDetail = (Kasbon)arrKasbonDetail[index];
                arrKasbonDetail.RemoveAt(index);
                Session["ListOfTransferDetail"] = arrKasbonDetail;
                GridView1.DataSource = arrKasbonDetail;
                GridView1.DataBind();
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ArrayList arrKasbonDetail = new ArrayList();
                    if (Session["ListOfKasbonDetail"] != null)
                    {
                        arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];
                    }
                }
            }
            catch
            {
            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "batal")
            {
                int approval = (int)Session["POPurchnApproval"];
                Users users = (Users)Session["Users"];
                if (users.Apv == 0)
                {
                    DisplayAJAXMessage(this, "Level Approval anda tidak memenuhi untuk cancel item ini");
                    return;
                }
                if (approval >= 3)
                {
                    DisplayAJAXMessage(this, "Cancel tidak bisa dilakukan karena sudah Approve Plant Manager");
                    return;
                }
                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrKasbonDetail = new ArrayList();
                arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];
                Kasbon kasbonDetail = new Kasbon();
                kasbonDetail = (Kasbon)arrKasbonDetail[index];
                string xNoSPP = kasbonDetail.NoSPP;
                int zSPPDetailID = kasbonDetail.SPPDetailID;
                int zPOPurchnDetailID = kasbonDetail.ID;
                decimal zjml = kasbonDetail.Qty;
                SPPDetail sPPDetail = new SPPDetail();
                sPPDetail.ID = zSPPDetailID;
                sPPDetail.QtyPO = zjml;
                POPurchnDetail sOPurchnDetail = new POPurchnDetail();
                sOPurchnDetail.ID = zPOPurchnDetailID;
                sOPurchnDetail.Status = -1;
                MinusQtyPOFacade minusQtyPOFacade = new MinusQtyPOFacade(sPPDetail, sOPurchnDetail);
                string strError = minusQtyPOFacade.MinusQtyPO();
                if (strError != string.Empty)
                {
                    DisplayAJAXMessage(this, strError);
                    return;
                }
                arrKasbonDetail.RemoveAt(index);
                Session["ListOfKasbonDetail"] = arrKasbonDetail;
                GridView2.DataSource = arrKasbonDetail;
                GridView2.DataBind();
            }
            else if (e.CommandName == "Hapus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrKasbonDetail = new ArrayList();
                arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];
                Kasbon kasbonDetail = (Kasbon)arrKasbonDetail[index];
                arrKasbonDetail.RemoveAt(index);
                Session["ListOfKasbonDetail"] = arrKasbonDetail;
                GridView2.DataSource = arrKasbonDetail;
                GridView2.DataBind();
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }

        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            if (txtNoPengajuan.Text.Trim() != string.Empty)
            {
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "UPDATE Kasbon SET CetakKasbon='1' WHERE NoPengajuan='" + txtNoPengajuan.Text.Trim() + "' ";
                SqlDataReader sdr = zl.Retrieve();
            }
            string strQuery = "SELECT ID, KasbonNo, NoPengajuan, DeptID, Pic, Status, Approval, format( TglKasbon,'dd/MM/yyyy') TglKasbon, DanaCadangan, AlasanNotApproved, "+
                "format(ApprovedDate2,'dd/MM/yyyy')ApprovedDate2, format(ApprovedDate4,'dd/MM/yyyy')ApprovedDate4, ApprovalCancel, format(ApprovedDateCancel1,'dd/MM/yyyy')ApprovedDateCancel1, "+
                "format(ApprovedDateCancel2,'dd/MM/yyyy')ApprovedDateCancel2, format(TglCancel,'dd/MM/yyyy')TglCancel, ApvMgr FROM Kasbon " +
                              "WHERE NoPengajuan='" + txtNoPengajuan.Text.Trim() + "' ";
            string strQuery1 = "SELECT distinct kd.ID,kd.KID,kd.SPPID,kd.PODetailID,kd.SppDetailID,s.NoSPP,kd.ItemName,sd.Quantity AS QtySpp,kd.Qty, " +
                               "kd.QtyPO,kd.HargaPO,kd.EstimasiKasbon,kd.Status,k.DanaCadangan FROM Kasbon AS k LEFT JOIN KasbonDetail AS kd ON k.ID=kd.KID " +
                               "LEFT JOIN SPP AS s ON kd.SPPID=s.ID LEFT JOIN SPPDetail AS sd ON kd.SppDetailID=sd.ID WHERE kd.KID IN " +
                               "(SELECT ID FROM Kasbon WHERE NoPengajuan='" + txtNoPengajuan.Text.Trim() + "') AND kd.Status>-1 ";
            Session["Query"] = strQuery;
            Session["Query1"] = strQuery1;
            Cetak(this);
        }

        static public void Cetak(Control page)
        {
            string myScript = "var wn = window.showModalDialog('../report/Report.aspx?IdReport=kasbon', '', 'resizable:yes;dialogHeight: 600px; dialogWidth: 780px;scrollbars=yes');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),"MyScript", myScript, true);
        }

        protected void rbTopUrgent_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTopUrgent.Checked == true)
            {
                txtSPP.Enabled = false;
                ddlItemSPP.Enabled = false;
                txtNamaBarang.Visible = true;
                Panel1.Visible = false;
                Panel2.Visible = true;
                lbAddItem.Visible = false;
                lblAdd.Visible = true;
            }
        }
    }
}