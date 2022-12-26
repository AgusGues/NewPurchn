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
// nilai kurs
namespace GRCweb1.Modul.Purchasing
{
    public partial class FormPOPurchn : System.Web.UI.Page
    {
        public enum Status
        {
            Open = 0,
            Approval = 1,
            ApprovalSC = 2,
            ScheduledPartial = 3,
            Scheduled = 4,
            Cancel = -1
            //status -1 = cancel/batal, 0 = baru dibuat, 1 = PO,  2 = Parsial   3 = Close
            //Approval 0 =  br dibuat,  1 = Wulan, 2=Pa' Ichwan, 3=approve by Pa'Jarot, 4 =  approve by Ibu Vero ...
        }

        protected void LogActivity(string activity, bool recordPageUrl)
        {
            if (Request.IsAuthenticated)
            {
                // Get information about the currently logged on user

                Users user = (Users)Session["Users"];
                if (user != null)
                {
                    DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
                    try
                    {
                        List<SqlParameter> param = new List<SqlParameter>();
                        param.Add(new SqlParameter("@UserID", user.ID));
                        param.Add(new SqlParameter("@Activity", activity));
                        param.Add(new SqlParameter("@PageUrl", Request.RawUrl));
                        param.Add(new SqlParameter("@IPAddress", Global.GetIPAddress()));
                        param.Add(new SqlParameter("@Browser", Request.UserAgent));
                        int intResult = da.ProcessData(param, "sp_LogUserActivity");
                    }
                    catch
                    {
                    }
                    finally
                    {
                        da.CloseConnection();
                    }
                }
            }
        }
        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            txtNoPO.Enabled = false;
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LogActivity("Input PO", true);
                Users users = (Users)Session["Users"];

                LoadSupplier();
                LoadTermOfPay();
                LoadIndent();
                LoadMataUang();
                btnRevisi.Visible = false;
                LoadTipeSPP();

                clearForm();
                Session["id"] = null;
                nilaiKurs.Text = "";
                //nk.Visible = false;
                //nilaiKurs.Visible = false;
                btnKirimEmail.Enabled = false;
                if (Request.QueryString["PONo"] != null)
                {
                    LoadPO(Request.QueryString["PONo"].ToString());
                }
                if (Request.QueryString["NoSPP"] != null)
                {
                    txtSPP.Text = Request.QueryString["NoSPP"].ToString();
                    DropDownList ddlName = (DropDownList)ddlItemSPP;
                    LoadItem(ddlName, txtSPP.Text);
                }
                //readonlykan textbox delivery date ( di table pourch_tools status 1)
                POPurchnFacade poTools = new POPurchnFacade();
                Domain.POPurchn objTools = poTools.PurchnTools("PODeliv");
                txtDate0.Enabled = (objTools.Status == 1) ? false : true;
                txtDelivery.Text = ddlDelivery.SelectedItem.Text;
            }
            else
            {
                if (txtCariSupplier.Text != string.Empty)
                {
                    getSupplier();
                    txtCariSupplier.Text = string.Empty;
                    ddlSupplier.Focus();
                }
            }
            string[] arrDpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDeptID", "PO").Split(',');
            btnListSPP.Visible = (arrDpt.Contains("14")) ? true : false;
            btnCancel.Attributes.Add("onclick", "return confirm_batal();");
            btnRevisi.Attributes.Add("onclick", "return confirm_revisi();");
            btnUangMuka.Attributes.Add("onclick", "return input_uang_muka();");

            //  btnClose.Attributes.Add("onclick", "return confirm_close();");
            string test = ddlSupplier.Items[ddlSupplier.SelectedIndex].Text;


        }
        #endregion
        private void getSupplier()
        {
            SuppPurchFacade supplierFacade = new SuppPurchFacade();
            ArrayList arrCustomer = supplierFacade.RetrieveByCriteria("A.SupplierName", txtCariSupplier.Text);
            if (supplierFacade.Error == string.Empty)
            {
                if (arrCustomer.Count > 0)
                {

                    ddlSupplier.Items.Clear();
                    ddlSupplier.Items.Add(new ListItem("-- Pilih Supplier Name --", "0"));

                    if (arrCustomer.Count > 0)
                    {
                        foreach (SuppPurch supp in arrCustomer)
                        {
                            if (supp.Aktif == 0)
                                ddlSupplier.Items.Add(new ListItem(supp.SupplierName, supp.ID.ToString()));
                        }
                    }
                }
            }
        }
        protected void ddlMatauang_Change(object sender, EventArgs e)
        {
            string InputKurs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
            if (int.Parse(ddlMataUang.SelectedValue) > 1 && InputKurs == "Aktif")
            {
                nilaiKurs.Focus();
                nk.Visible = true;
                nilaiKurs.Visible = true;
            }
            else
            {
                nk.Visible = false;
                nilaiKurs.Visible = false;
            }
        }
        protected void lstPO_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lbl = (Label)e.Item.FindControl("lblRevInfo");
            Image img = (Image)e.Item.FindControl("lstDel");
            Image edt = (Image)e.Item.FindControl("lstEdit");
            img.Attributes.Add("onclick", "return confirm_revisi();");
            try
            {
                Domain.POPurchn po = (Domain.POPurchn)e.Item.DataItem;
                //POPurchnDetail po = (POPurchnDetail)e.Item.DataItem;
                img.Visible = (po.Approval > 0) ? false : true;
                lbl.Visible = (po.NoUrut > 0) ? true : false;
                edt.Visible = (po.Approval > 0) ? false : true;
                lbl.Text = po.NoUrut.ToString();
                lbl.CssClass = po.ID.ToString();
                RevisiPOne rvp = new RevisiPOne();
                rvp.Where = " where POID=" + po.POID.ToString();
                rvp.Where += " and ItemID=" + po.ItemID.ToString();
                lbl.ToolTip = rvp.GetReasonRevision();
                //if(((Users)Session["Users"]).ViewPrice<1)
                //{
                //    HtmlTableCell td = (HtmlTableCell)e.Item.FindControl("xx");
                //    td.InnerHtml = "********";
                //}
            }
            catch
            {
                lbl.Visible = false;
            }
        }
        protected void btnKirimEmail_Click(object sender, EventArgs e)
        {
            Response.Redirect("POPurchnSendMail.aspx?Nopo=" + txtNoPO.Text);
        }
        private void LoadPO(string strPONo)
        {
            Users users = (Users)Session["Users"];
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByNoWithDepo2(strPONo, users.UnitKerjaID);
            if (CekUangMuka(strPONo) > 0)
                btnUangMuka.Enabled = false;
            else
                btnUangMuka.Enabled = true;
            try
            {
                if (pOPurchnFacade.Error == string.Empty && pOPurchn.ID > 0)
                {
                    btnKirimEmail.Enabled = (pOPurchn.Approval >= 2) ? true : false;
                    Session["id"] = pOPurchn.ID;
                    txtNoPO.Text = pOPurchn.NoPO;
                    Session["POPurchnNo"] = pOPurchn.NoPO;
                    Session["POPurchnApproval"] = pOPurchn.Approval;
                    txtDate.Text = pOPurchn.POPurchnDate.ToString("dd-MMM-yyyy");
                    ddlTipeSPP.ClearSelection();
                    ddlSupplier.ClearSelection();
                    SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
                    SuppPurch suppPurch = suppPurchFacade.RetrieveById(pOPurchn.SupplierID);
                    if (suppPurchFacade.Error == string.Empty)
                    {

                        #region
                        //foreach (ListItem item in ddlSupplier.Items)
                        //{
                        //    if (item.Text == suppPurch.SupplierName)
                        //    {
                        //        item.Selected = true;

                        //    }
                        //}
                        #endregion
                        //ddlSupplier.SelectedValue  = suppPurch.ID.ToString();
                        txtSuppID.Value = suppPurch.ID.ToString();
                        txtSup.Text = suppPurch.SupplierName;
                        txtUp.Text = suppPurch.UP;
                        txtKodeSupplier.Text = suppPurch.SupplierCode;
                        txtTelepon.Text = suppPurch.Telepon;
                        txtFax.Text = suppPurch.Fax;
                        txtDate0.Text = string.Empty;
                        btnPrint.Disabled = (suppPurch.Aktif == 0) ? false : true;
                        btnPrintFax.Disabled = (suppPurch.Aktif == 0) ? false : true;
                    }

                    if (pOPurchn.Status > 0 || pOPurchn.Approval > 0)
                    {
                        btnUpdate.Disabled = false;
                        btnCancel.Enabled = false;
                        btnClose.Enabled = false;
                        btnRevisi.Visible = (((Users)Session["Users"]).Apv > 1) ? true : false;
                        btnRevisi.Enabled = ((pOPurchn.Status > 0)) ? false : true;
                    }

                    if (pOPurchn.Status < 0 || pOPurchn.Approval < 0)
                    {
                        btnUpdate.Disabled = true;
                        btnCancel.Enabled = false;
                        btnClose.Enabled = true;
                        btnRevisi.Visible = false;
                    }
                    #region Approval
                    switch (pOPurchn.Approval)
                    {
                        case 0:
                            stPO.Text = "Not Approval";
                            break;
                        case 1:
                            stPO.Text = "Approval Head";
                            break;
                        case 2:
                        case 3:
                        case 4:
                            stPO.Text = "Approval Manager";
                            break;

                        default:
                            stPO.Text = "";
                            break;
                    }

                    #endregion
                    #region Check History Revision
                    RevisiPOne rvp = new RevisiPOne();
                    rvp.Where = " where POID=" + pOPurchn.ID.ToString();
                    //revInfo.InnerHtml = rvp.GetReasonRevision().Replace("\n", "<br>");
                    txtInfoRevisi.Text = rvp.GetReasonRevision(true);
                    txtInfoRevisi.ToolTip = rvp.GetReasonRevision();
                    revInfo.Attributes.Add("style", "color:Blue;font-size:x-small; position:absolute; float:left;border:2px solid grey; background-color:Highlight");
                    #endregion
                    POPurchnFacade pOPurchnFacade3 = new POPurchnFacade();
                    ArrayList arrItemList = pOPurchnFacade3.ViewGridPO(pOPurchn.ID, ((Users)Session["Users"]).ViewPrice);

                    //SPPDetail sppDetail = new SPPDetail();
                    //SPPDetailFacade arrSPPDetail = new SPPDetailFacade();

                    Session["NoPO"] = strPONo;
                    Session["JenisTransaksi"] = "PO";
                    Session["ListOfPOPurchnDetail"] = arrItemList;
                    GridView1.DataSource = arrItemList;
                    GridView1.DataBind();
                    //GridView2.Visible = true;
                    GridView2.DataSource = arrItemList;
                    GridView2.DataBind();
                    GridView2.Columns[9].Visible = false;
                    GridView2.Columns[8].Visible = true;
                    GridView2.Columns[7].Visible = true;
                    lstPO.DataSource = arrItemList;
                    lstPO.DataBind();
                    /* Check proses revisi */
                }
                else
                {
                    DisplayAJAXMessage(this, "No. PO tersebut tidak bisa ditampilkan karena tidak ada, atau telah dicancel");
                    return;
                }
                POPurchnFacade pOPurchnFacade2 = new POPurchnFacade();
                Domain.POPurchn pOPurchn2 = pOPurchnFacade2.ViewPO(pOPurchn.ID, ((Users)Session["Users"]).ViewPrice);
                txtSPP.Text = pOPurchn2.NOSPP;
                txtNamaBarang.Text = pOPurchn2.NamaBarang;
                txtSatuan.Text = pOPurchn2.Satuan;
                txtHarga.Text = pOPurchn2.Price.ToString();
                //txtQty.Text = pOPurchn2.Qty.ToString();
                txtDiscount.Text = pOPurchn2.Disc.ToString("N2");
                txtDelivery.Text = pOPurchn2.Delivery;
                txtPPN.Text = pOPurchn2.PPN.ToString();
                txtPPH.Text = pOPurchn2.PPH.ToString();
                txtRemark.Text = pOPurchn2.Remark;
                txtOngkos.Text = pOPurchn2.Ongkos.ToString();
                int strCrc = pOPurchn.Crc;

                if (pOPurchn2.ItemFrom == 0)
                {
                    rbLocal.Checked = true;
                    rbImport.Checked = false;

                }
                else
                {
                    rbLocal.Checked = false;
                    rbImport.Checked = true;
                }
                LoadMataUang();
                ddlMataUang.SelectedIndex = pOPurchn2.Crc;
                LoadTermOfPay();
                #region tidak digunakan lagi
                int TerminID = 0;
                switch (pOPurchn2.Termin)
                {
                    case "1 Minggu":
                        rbCredit.Checked = true;
                        TerminID = 1;
                        break;
                    case "2 Minggu":
                        TerminID = 2;
                        rbCredit.Checked = true;
                        break;
                    case "30 Hari":
                        rbCredit.Checked = true;
                        TerminID = 3;
                        break;
                    case "40 Hari":
                        rbCredit.Checked = true;
                        TerminID = 4;
                        break;
                    case "2 Bulan":
                        TerminID = 5;
                        rbCredit.Checked = true;
                        break;
                    case "Cash":
                        rbCash.Checked = true;
                        TerminID = 6;
                        break;
                    case "1 Bulan Setelah OK dari BPAS":
                        rbCredit.Checked = true;
                        TerminID = 7;
                        break;
                    case "DP 25% dari Total Pembayaran":
                        rbCredit.Checked = true;
                        TerminID = 8;
                        break;
                    default:
                        rbCredit.Checked = true;
                        TerminID = 9;
                        break;
                }
                //int TerminID = 0;
                //if (pOPurchn2.Termin == "1 Minggu")
                //    TerminID = 1;
                //if (pOPurchn2.Termin == "2 Minggu")
                //    TerminID = 2;
                //if (pOPurchn2.Termin == "30 Hari")
                //    TerminID = 3;
                //if (pOPurchn2.Termin == "2 Bulan")
                //    TerminID = 4;
                //if (pOPurchn2.Termin == "Cash")
                //    TerminID = 5;
                //if (pOPurchn2.Termin == "1 Bulan Setelah OK dari BPAS")
                //    TerminID = 6;
                //if (pOPurchn2.Termin == "DP 25% dari Total Pembayaran")
                //    TerminID = 7;
                //if (pOPurchn2.Termin == "-")
                //    TerminID = 8;
                //ddlTermOfPay.SelectedIndex = TerminID;
                #endregion

                LoadTipeSPP();
                ddlTipeSPP.SelectedIndex = pOPurchn2.GroupID;
                POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
                POPurchnDetail pOPucrhnDetail = pOPurchnDetailFacade.RetrieveTotalById(pOPurchn.ID, ((Users)Session["Users"]).ViewPrice);
                int MataUang = pOPucrhnDetail.RowStatus;
                string InputKurs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
                decimal totprice = pOPucrhnDetail.Total - ((pOPurchn2.Disc / 100) * pOPucrhnDetail.Total);
                decimal grandtotal = (((pOPurchn2.PPN / 100) * totprice) + ((pOPurchn2.PPH / 100) * totprice) + totprice);
                int MU = (InputKurs == "Aktif" && MataUang <= 0 && pOPucrhnDetail.Price > 0) ? 1 : strCrc;
                nk.Visible = (MU != strCrc) ? true : false;
                nilaiKurs.Visible = (MU != strCrc) ? true : false;
                nilaiKurs.Text = pOPurchn.NilaiKurs.ToString("#,##0.0#");
                MataUangFacade mataUangFacade = new MataUangFacade();
                MataUang mataUang = mataUangFacade.RetrieveById(MU);
                string nmUang = mataUang.Nama;
                string zLambang = mataUang.Lambang;
                txtTotalPrice.Text = mataUang.Lambang + " " + pOPucrhnDetail.Total.ToString("#,#.00#;(#,#.00#)");
                ddlTermOfPay.SelectedValue = TerminPO(pOPurchn2.Termin).ToString();
                #region

                #endregion
                TerbilangFacade terbilangFacade = new TerbilangFacade();
                Label3.Text = terbilangFacade.ConvertMoneyToWords(grandtotal);
                //Label4.Text = terbilangFacade.changeNumericToWords(Convert.ToDouble(grandtotal));
                tipeSpp.InnerHtml = "";
                leadTime.InnerHtml = "";

            }
            catch (Exception ex)
            {
                DisplayAJAXMessage(this, ex.Message);
            }
        }
        private int CekUangMuka(string strNoPO)
        {
            int idcount = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select count(ID) countID from terminbayar where rowstatus>-1 and poid in (select id from popurchn where nopo='" + strNoPO + "')";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    idcount = Convert.ToInt32(sdr["countID"].ToString());
                }
            }
            return idcount;
        }
        private int TerminPO(string Termin)
        {
            TermOfPayFacade term = new TermOfPayFacade();
            ArrayList arrTerm = new ArrayList();
            arrTerm = term.Retrieve();
            foreach (TermOfPay objTerm in arrTerm)
            {
                if (objTerm.TermPay == Termin)
                {
                    return int.Parse(objTerm.ID.ToString());
                }
            }
            return 0;
        }
        private void LoadDataGridTO(Domain.POPurchn pOPurchn)
        {
            LoadPO(pOPurchn.NoPO);
            //this.GridView1.DataSource = arrTO;
            //this.GridView1.DataBind();
        }
        private void LoadSupplier()
        {
            ArrayList arrSuppPurch = new ArrayList();
            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            arrSuppPurch = suppPurchFacade.Retrieve();
            ddlSupplier.Items.Add(new ListItem("-- Pilih Supplier --", string.Empty));
            ddlSupPurch.Items.Add(new ListItem(" ", string.Empty));
            foreach (SuppPurch suppPurch in arrSuppPurch)
            {
                if (suppPurch.Aktif == 0)
                {
                    ddlSupplier.Items.Add(new ListItem(suppPurch.SupplierName, suppPurch.ID.ToString()));
                    ddlSupPurch.Items.Add(new ListItem(suppPurch.SupplierName, suppPurch.ID.ToString()));
                }
            }
            //ddlSupPurch.DataSource = arrSuppPurch;
            //ddlSupPurch.DataBind();
        }
        private void LoadMataUang()
        {
            ArrayList arrMataUang = new ArrayList();
            MataUangFacade mataUangFacade = new MataUangFacade();
            arrMataUang = mataUangFacade.Retrieve();
            ddlMataUang.Items.Clear();
            ddlMataUang.Items.Add(new ListItem("-- Pilih Mata Uang --", string.Empty));
            foreach (MataUang mataUang in arrMataUang)
            {
                ddlMataUang.Items.Add(new ListItem(mataUang.Lambang, mataUang.ID.ToString()));
            }

        }
        private void LoadTermOfPay()
        {
            ArrayList arrTermOfPay = new ArrayList();
            TermOfPayFacade termOfPayFacade = new TermOfPayFacade();
            arrTermOfPay = termOfPayFacade.Retrieve();
            ddlTermOfPay.Items.Clear();
            ddlTermOfPay.Items.Add(new ListItem("-- Pilih Jenis TOP --", string.Empty));
            foreach (TermOfPay termOfPay in arrTermOfPay)
            {
                ddlTermOfPay.Items.Add(new ListItem(termOfPay.TermPay, termOfPay.ID.ToString()));
            }
        }
        private void LoadIndent()
        {
            ArrayList arrIndent = new ArrayList();
            IndentFacade indentFacade = new IndentFacade();
            arrIndent = indentFacade.Retrieve();
            ddlIndent.Items.Add(new ListItem("-- Pilih Tenggang Indent --", string.Empty));
            foreach (Indent indent in arrIndent)
            {
                ddlIndent.Items.Add(new ListItem(indent.Tenggang, indent.ID.ToString()));
            }
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
        private void LoadItem(DropDownList ddlName, string strNoSPP)
        {
            Users users = (Users)Session["Users"];
            int depoID = users.UnitKerjaID;
            strNoSPP = txtSPP.Text;

            SPPFacade sPPFacade = new SPPFacade();
            SPP sPP = new SPP();
            sPP = sPPFacade.RetrieveByNo(strNoSPP);
            if (sPP.PermintaanType == 1)
            {
                tipeSpp.InnerHtml = "Tipe SPP: Top Urgent";
            }
            else if (sPP.PermintaanType == 2)
            {
                tipeSpp.InnerHtml = "Tipe SPP: Biasa";
            }
            else
            {
                tipeSpp.InnerHtml = "Tipe SPP: Sesuai Schedule";
            }
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
                #region
                //if (sPP.Approval == 2)
                //{
                //    DisplayAJAXMessage(this, "SPP tersebut belum di-approval Purchasing");
                //    txtSPP.Text = string.Empty;
                //    ddlItemSPP.Items.Clear();
                //    return;
                //}
                //if (sPP.Approval == 3)
                //{
                //    DisplayAJAXMessage(this, "SPP tersebut Sudah di-approval Plan Manager");
                //    txtSPP.Text = string.Empty;
                //    ddlItemSPP.Items.Clear();
                //    return;
                //}
                #endregion
                string strNoSPP2 = sPP.NoSPP;

                //string test3 = sPP.ID.ToString();

                ArrayList arrSPPDetail = new ArrayList();
                SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                AccClosingFacade biaya = new AccClosingFacade();
                AccClosing aktif = biaya.BiayaNewActive();
                Session["ForPO"] = "yes";
                int tgl = (aktif.CreatedTime <= sPP.CreatedTime) ? 1 : 0;
                arrSPPDetail = sPPDetailFacade.RetrieveBySPPID(sPP.ID);
                if (sPPDetailFacade.Error == string.Empty)
                {
                    ddlItemSPP.Items.Add(new ListItem("-- Pilih Item SPP --", string.Empty));

                    foreach (SPPDetail sPPDetail in arrSPPDetail)
                    {
                        if (sPPDetail.ItemTypeID == 3 && CheckStatusBiaya() == 1 && tgl == 1)
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
        private void LoadTipeSPP()
        {
            ArrayList arrGroupsPurchn = new ArrayList();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroupsPurchn = groupsPurchnFacade.Retrieve();
            ddlTipeSPP.Items.Add(new ListItem("-- Pilih Tipe SPP --", string.Empty));
            foreach (GroupsPurchn groupsPurchn in arrGroupsPurchn)
            {
                ddlTipeSPP.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        private ArrayList LoadGridPOPurchn()
        {
            ArrayList arrPOPurchn = new ArrayList();
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            arrPOPurchn = pOPurchnFacade.Retrieve();
            if (arrPOPurchn.Count > 0)
            {
                return arrPOPurchn;
            }

            arrPOPurchn.Add(new POPurchn());
            return arrPOPurchn;
        }
        private void clearQty()
        {
            txtQty.Text = string.Empty;
            txtSatuan.Text = string.Empty;
            txtHarga.Text = string.Empty;
            txtNamaBarang.Text = string.Empty;
            //txtDiscount.Text = "0";
            //txtPPN.Text = "0";
            //txtPPH.Text = "0";

        }
        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfPOPurchn"] = null;
            Session["id"] = null;
            Session["POPurchnNo"] = null;
            Session["NoPO"] = null;
            Session["JenisTransaksi"] = null;
            Session["ListOfPOPurchnDetail"] = null;
            Session["TotalPrice"] = null;
            Session["QuantitySPP"] = null;
            Session["ListOfPODetail"] = null;
            Session["groupidpo"] = null;
            ViewState["Baris"] = null;
            Session["POPurchnApproval"] = null;
            txtItemCode.Text = string.Empty;
            txtNoPO.Text = string.Empty;
            txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtDate0.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtDate0.Enabled = (CheckDeliveStatus() > 0) ? false : true;
            txtSPP.Text = string.Empty;
            ddlItemSPP.Items.Clear();
            ddlTipeSPP.SelectedIndex = 0;
            ddlSupplier.SelectedIndex = 0;
            txtKodeSupplier.Text = string.Empty;
            txtUp.Text = string.Empty;
            txtTelepon.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtNamaBarang.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtSatuan.Text = string.Empty;
            txtHarga.Text = string.Empty;
            ddlMataUang.SelectedIndex = 0;
            txtDiscount.Text = "0";
            txtPPN.Text = "0";
            txtPPN.Enabled = true;
            txtPPH.Text = "0";
            txtTotalPrice.Text = string.Empty;
            txtOngkos.Text = "0";
            ddlMataUang.SelectedIndex = 1;
            ddlSupplier.Enabled = true;
            ArrayList arrList = new ArrayList();
            //arrList.Add(new POPurchnDetail());
            GridView1.DataSource = arrList;
            GridView1.DataBind();
            GridView2.DataSource = arrList;
            GridView2.DataBind();
            btnUpdate.Disabled = false;
            GridView2.Columns[9].Visible = true;
            GridView2.Columns[8].Visible = false;
            GridView2.Columns[7].Visible = false;
            leadTime.InnerHtml = "";
            tipeSpp.InnerHtml = "";
            stPO.Text = "";
            revInfo.InnerHtml = "";
            revInfo.Attributes.Remove("style");
            lstPO.DataSource = arrList;
            lstPO.DataBind();
            txtRemark.Text = "";
            nilaiKurs.Text = "";
            nk.Visible = false;
            nilaiKurs.Visible = false;
            txtSup.Text = "";
            txtSuppID.Value = "0";
            frmKA.Visible = false;
            txtGrossPlant.Text = "0";
            txtNettoPlant.Text = "0";
            txtNoPOL.Text = "";
            txtSampah.Text = "0";
            txtKadarAirPlant.Text = "0";
            txtSup.ReadOnly = false;
            txtTermOfPay.Visible = false;
            txtTermOfPay.Text = "";
            btnUangMuka.Enabled = true;
        }
        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session["TotalPrice"] = null;
            Session.Remove("Baris");
            Session.Remove("id");
            Session.Remove("ListOfPOPurchn");
            Session.Remove("id");
            Session.Remove("POPurchnNo");
            Session.Remove("NoPO");
            Session.Remove("JenisTransaksi");
            Session.Remove("ListOfPOPurchnDetail");
            Session.Remove("TotalPrice");
            Session.Remove("QuantitySPP");
            Session.Remove("ListOfPODetail");
            Session.Remove("AlasanCancel");
            Session["KadarAir"] = null;
            Session["KadarAirDepo"] = null;
            clearForm();
            ArrayList arrData = new ArrayList();
            lstPO.DataSource = arrData;
            lstPO.DataBind();
        }
        protected void btnHd_Click(object sender, EventArgs e)
        {
            if (Session["KadarAir"] != null)
            {
                ArrayList arrData = (ArrayList)Session["KadarAir"];
                foreach (Domain.POPurchn pp in arrData)
                {
                    txtQty.Text = pp.Netto.ToString();
                }
            }
        }
        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {

            /** PPN 11% added 31 Maret 2022 **/
            string thn = DateTime.Now.Year.ToString();
            string bln = (DateTime.Now.Month.ToString().Length == 1) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string thnbln = thn + bln;

            Users users = (Users)Session["Users"];
            string strValidate = ValidateText();
            decimal totalPrice = 0;
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            string DefaultKadarAir = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            string KadarAirAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KadarAirAktif", "PO");
            string[] ItemKertas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemCheckedCode", "PO").Split(',');
            int kertas = Array.IndexOf(ItemKertas, txtItemCode.Text);
            if (KadarAirAktif == "1" && kertas > -1)
            {
                if (Session["KadarAir"] == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", "alert('Data Kadar Air belum lengkap');", true);
                    return;
                }
            }
            string strEvent = "Insert";
            if (Session["TotalPrice"] != null)
            {
                totalPrice = (decimal)Session["TotalPrice"];
            }
            //Check Nilai Kurs
            string InputKurs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();

            //if (int.Parse(ddlMataUang.SelectedValue) > 1 && nilaiKurs.Text == string.Empty && InputKurs=="Aktif")
            //if (nilaiKurs.Text == string.Empty)
            //{
            //    DisplayAJAXMessage(this, "Nilai Kurs Harus di isi untuk mata uang " + ddlMataUang.SelectedItem.Text);
            //    return;
            //}

            #region Proses Header PO
            Domain.POPurchn pOPurchn = new Domain.POPurchn();

            if (Session["id"] != null)
            {
                pOPurchn.ID = int.Parse(Session["id"].ToString());
                strEvent = "Edit";
            }
            pOPurchn.NoPO = txtNoPO.Text;
            string test = txtTotalPrice.Text.Replace(".", "");
            string test21 = test.Replace(",00", "").Substring(3, test.Replace(",00", "").Length - 3);

            string test2 = (test21) != "" ? test21 : "0";

            pOPurchn.POPurchnDate = DateTime.Parse(txtDate.Text);
            totalPrice = decimal.Parse(test2);
            decimal ppn = 0;
            if (txtPPN.Text.Trim() != string.Empty)
            {
                ppn = decimal.Parse(txtPPN.Text);

                /** PPN 11% added 31 Maret 2022 oleh : Beny **/
                if (Convert.ToInt32(thnbln) > 202203)
                {
                    if (ppn > 0)
                        totalPrice = totalPrice + (totalPrice * 11 / 100);
                }
                else
                {

                    if (ppn > 0)
                        totalPrice = totalPrice + (totalPrice * 10 / 100);
                }
            }
            //totalPrice = (decimal.Parse(test2)) > 0 ? decimal.Parse(test2) : 0 ;

            if (txtNoPO.Text.Trim() != string.Empty)
            {
                ////// Uang Muka
                TerminBayar terminBayar = new TerminBayar();
                TerminBayarFacade terminBayarFacade = new TerminBayarFacade();
                if (Session["NilaiUangMuka"] != null)
                {
                    Domain.POPurchn po = new POPurchnFacade().RetrieveByNo(txtNoPO.Text);
                    terminBayar.POID = po.ID;
                    ArrayList arrD = (ArrayList)Session["NilaiUangMuka"];
                    foreach (TerminBayar t in arrD)
                    {
                        //terminBayar.DP = t.DP;
                        //terminBayar.TerminKe = t.TerminKe;
                        //terminBayar.JmlTermin = arrD.Count  ;
                        //terminBayar.TotalBayar = t.Persentase * totalPrice/100;
                        //terminBayar.Persentase = t.Persentase;
                        ////insert ke table termin bayar
                        //terminBayarFacade.Insert(terminBayar);
                        //pOPurchn.UangMuka = t.DP;
                        //pOPurchn.CoaID = 0;
                        // @POID int, @DP decimal(18,2), @TerminKe int, @JmlTermin int, @TotalBayar decimal(18,5),@Persentase decimal(18,2)
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        zl.CustomQuery = "declare @i int " +
                            "set @i=(select count(id) from TerminBayar where TerminKe=" + t.TerminKe + " and POID=" + po.ID + ") " +
                            "if @i=0 " +
                            "begin " +
                            "INSERT INTO TerminBayar( POID, DP, TerminKe, JmlTermin, TotalBayar,rowstatus,Persentase ) values " +
                            "(" + po.ID + "," + t.DP + "," + t.TerminKe + "," + arrD.Count + "," + (t.Persentase * totalPrice / 100).ToString().Replace(",", ".") +
                            ",0," + t.Persentase.ToString().Replace(",", ".") + ")" +
                            "SELECT @@IDENTITY as ID" +
                            "end" +
                            "else" +
                            "begin" +
                            "    update TerminBayar set DP=" + t.DP + " , JmlTermin=" + arrD.Count + ", TotalBayar=" +
                            (t.Persentase * totalPrice / 100).ToString().Replace(",", ".") +
                            ",Persentase=" + t.Persentase.ToString().Replace(",", ".") + " " +
                            "where TerminKe=" + t.TerminKe + " and POID=" + po.ID + " " +
                            "end";
                        SqlDataReader sdr = zl.Retrieve();
                    }

                }
                else
                {
                    pOPurchn.UangMuka = 0;
                    pOPurchn.CoaID = 0;
                }
                //update uang muka di po
                POPurchnFacade pf = new POPurchnFacade();
                int resto = pf.UpdateUangMuka(pOPurchn);
                return;
            }
            if (int.Parse(ddlSupplier.SelectedValue.ToString()) == 0)
            {
                DisplayAJAXMessage(this, "Supplier belum di pilih");
                return;
            }
            pOPurchn.SupplierID = int.Parse(ddlSupplier.SelectedValue);
            if (ddlTermOfPay.SelectedIndex == 0)
            {
                pOPurchn.Termin = "-";
            }
            else
            {
                pOPurchn.Termin = ddlTermOfPay.SelectedItem.ToString();
            }
            //txtTermOfPay.Text = string.Empty;
            if (ddlTermOfPay.SelectedItem.Text.Trim().ToUpper() == "LAIN-LAIN")
            {
                pOPurchn.Termin = txtTermOfPay.Text.Trim();
            }
            else
            {
                pOPurchn.Termin = ddlTermOfPay.SelectedItem.ToString();
            }
            if (ChkIndent.Checked == true)
            {
                pOPurchn.Indent = ddlIndent.SelectedItem.ToString();
            }
            else
            {
                pOPurchn.Indent = " ";
            }

            pOPurchn.Delivery = txtDelivery.Text;

            pOPurchn.Crc = int.Parse(ddlMataUang.SelectedValue);
            pOPurchn.Keterangan = string.Empty;
            //pOPurchn.Terbilang = txtTerbilang.Text;
            //pOPurchn.PPN = ((decimal.Parse(txtPPN.Text)) / 100) * totalPrice;
            //pOPurchn.Disc = (decimal.Parse(txtDiscount.Text)) / 100 * totalPrice;
            //pOPurchn.PPH = ((decimal.Parse(txtPPH.Text)) / 100) * totalPrice;

            pOPurchn.PPN = Convert.ToDecimal(txtPPN.Text);
            pOPurchn.Disc = Convert.ToDecimal(txtDiscount.Text);
            pOPurchn.PPH = Convert.ToDecimal(txtPPH.Text);
            pOPurchn.Ongkos = Convert.ToDecimal(txtOngkos.Text);
            //pOPurchn.NilaiKurs = 0;
            pOPurchn.Cetak = 0;
            pOPurchn.CountPrt = 0;
            pOPurchn.Status = 0;
            pOPurchn.Approval = 0;
            pOPurchn.CreatedBy = users.UserName;
            pOPurchn.Remark = txtRemark.Text;
            pOPurchn.NilaiKurs = (int.Parse(ddlMataUang.SelectedValue) > 1) ? decimal.Parse(nilaiKurs.Text) : 0;
            //if (rbCredit.Checked == true && rbCash.Checked == false) Cash=0;Selain Cash =1
            pOPurchn.PaymentType = (int.Parse(ddlTermOfPay.SelectedValue) == 6) ? 0 : 1;
            if (rbImport.Checked = true && rbLocal.Checked == false) { pOPurchn.ItemFrom = 1; }
            int intGroupID = 0;
            if (int.Parse(ddlTipeSPP.SelectedValue) == 9 || int.Parse(ddlTipeSPP.SelectedValue) == 8)
            {
                intGroupID = 8;
                //krn utk penomoran elektrik & mekanik = KS / sparepart
            }
            else
            {
                intGroupID = int.Parse(ddlTipeSPP.SelectedValue);
            }
            /** penggabungan supplier kertas kantong semen
             * added on 26-01-2016
             */
            string SubCompanyIDAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("SubCompanyAktif", "PO").ToString();
            if (SubCompanyIDAktif == "1")
            {
                pOPurchn.SubCompanyID = int.Parse(txtSubCompanyID.Value);
            }
            CompanyFacade companyFacade = new CompanyFacade();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            string kd = companyFacade.GetKodeCompany(users.UnitKerjaID) + groupsPurchnFacade.GetKodeSPP(intGroupID);
            //3 nomor PO
            string kdnew = string.Empty;
            //if (intGroupID < 4 || intGroupID > 5)
            if (intGroupID < 4 || intGroupID > 5 && intGroupID < 12)
            {
                kdnew = companyFacade.GetKodeCompany(users.UnitKerjaID) + "I";
            }
            else
            {
                kdnew = companyFacade.GetKodeCompany(users.UnitKerjaID) + groupsPurchnFacade.GetKodeSPP(intGroupID);
            }
            // end 3
            #endregion
            #region proses nomor PO
            SPPNumber sPPNumber = new SPPNumber();
            SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
            sPPNumber = sPPNumberFacade.RetrieveByGroupsID(intGroupID);
            if (sPPNumberFacade.Error == string.Empty)
            {
                if (sPPNumber.ID > 0)
                {
                    //otomatis reset counter jika tahun baru
                    //added on 02-01-2016
                    if (sPPNumber.LastModifiedTime.Year == DateTime.Parse(txtDate.Text).Year)
                    {
                        sPPNumber.POCounter = sPPNumber.POCounter + 1;
                    }
                    else
                    {
                        sPPNumber.POCounter = 1;
                    }
                    sPPNumber.KodeCompany = kdnew.Substring(0, 1);
                    sPPNumber.KodeSPP = kdnew.Substring(1, 1);
                    sPPNumber.LastModifiedBy = users.UserName;
                    sPPNumber.Flag = 1;
                }
            }
            #endregion
            #region proses Detail PO
            //update status PODetail : 0 = open, 1 = parsial pettycash, 2 = full payment, 3 = close & 4 = receipt
            string strError = string.Empty;
            ArrayList arrPOPurchnDetail = new ArrayList();
            if (Session["ListOfPOPurchnDetail"] != null)
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
            InventoryFacade cekharga = new InventoryFacade();
            string[] arrKertas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemKertas", "PO").Split(',');
            int Hargakertas = 0;
            int IsHargakertas = 0;
            foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
            {
                if (arrKertas.Contains(pOPurchnDetail.ItemID.ToString()))
                {
                    IsHargakertas = cekharga.IsHargaKertas(pOPurchnDetail.ItemID);
                    if (IsHargakertas > 0)
                    {
                        Hargakertas = cekharga.GetHargaKertas(pOPurchnDetail.ItemID, pOPurchn.SupplierID);
                        if (Hargakertas == 0)
                        {
                            DisplayAJAXMessage(this, "List Price not found");
                            return;
                        }
                        else
                            pOPurchnDetail.Price = Hargakertas;
                    }
                }
                //Check lagi Qty PO tidak boleh melebihi Qty SPP
                decimal QtyPO = new POPurchnDetailFacade().CheckQtyPO(pOPurchnDetail.SPPDetailID.ToString());
                SPPDetail spp = new SPPDetailFacade().RetrieveBySPPDetailID(pOPurchnDetail.SPPDetailID);
                if ((QtyPO + pOPurchnDetail.Qty) > spp.Quantity)
                {
                    DisplayAJAXMessage(this, "Quantity PO sudah melebihi Qty SPP, silahkan Check Lagi");
                    return;
                }
            }
            #endregion
            POPurchnProcessFacade pOPurchnProcessFacade = new POPurchnProcessFacade(pOPurchn, arrPOPurchnDetail, sPPNumber);
            if (pOPurchn.ID > 0)
            {
                strError = pOPurchnProcessFacade.Update();
            }
            else
            {
                strError = pOPurchnProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtNoPO.Text = pOPurchnProcessFacade.POPurchnNo;
                    Session["id"] = pOPurchn.ID;
                    totalPrice = totalPrice - Convert.ToDecimal(pOPurchn.Disc) + pOPurchn.PPN + pOPurchn.PPH;
                    txtTotalPrice.Text = totalPrice.ToString();
                    TerbilangFacade terbilangFacade = new TerbilangFacade();
                    /**
                     * Simpan Data Kadar Air
                     * added on 02-10-2015
                     * Penambahan data surat jalan depo on 02-11-2016
                     */

                    if (KadarAirAktif == "1" && kertas > -1)
                    {
                        Domain.POPurchn po = new POPurchnFacade().RetrieveByNo(txtNoPO.Text);
                        Domain.POPurchn pod = new POPurchnFacade().ViewPO(po.ID, 0);
                        ArrayList arrData = (ArrayList)Session["KadarAir"];
                        Revisi dataKA = new Revisi();
                        #region kadarAir depo
                        foreach (Domain.POPurchn p in arrData)
                        {
                            dataKA.StdKA = p.StdKA;
                            dataKA.Gross = p.Gross;
                            dataKA.AktualKA = p.AktualKA;
                            dataKA.Netto = p.Netto;
                            dataKA.POID = po.ID;
                            dataKA.ItemID = pod.ItemID;
                            dataKA.RowStatus = 0;
                            dataKA.PODetailID = po.PODetailID;
                            dataKA.CreatedBy = ((Users)Session["Users"]).UserName;
                            dataKA.CreatedTime = DateTime.Now;
                            dataKA.NoPol = p.NoPol.ToUpper();
                            dataKA.Sampah = p.Sampah;
                            dataKA.SchID = p.SchID;
                            dataKA.SchNo = p.SchNo;
                            string log = "POID:" + po.PODetailID + ",StdKA :" + p.StdKA + ",Gross:" + p.Gross + ",AktualKA:" + p.AktualKA + ",Netto:" + p.Netto;
                            RevisiPOne rpo = new RevisiPOne();
                            rpo.Criteria = "StdKA,Gross,ItemID,AktualKA,NoPol,Netto,POID,PODetailID,Sampah,RowStatus,SchID,SchNo,CreatedBy,CreatedTime";
                            rpo.Pilihan = "Insert";
                            rpo.TableName = "POPurchnKadarAir";
                            string rst = rpo.CreateProcedure(dataKA, "spPOPurchnKadarAir2_0_insert");
                            if (rst == string.Empty)
                            {
                                int rest = rpo.ProcessData(dataKA, "spPOPurchnKadarAir2_0_insert");
                                if (rest > 0)
                                {
                                    /**
                                     * Update Table DeliveryKertas dengan data kadar air Khusus Depo
                                     * added on 02-11-2016
                                     */
                                    if (p.SchID.ToString().Substring(0, 1) != "7")
                                    {
                                        DeliveryKertas dk = new DeliveryKertas();
                                        dk.ID = p.SchID;
                                        dk.POKAID = rest;
                                        dk.GrossPlant = p.Gross;
                                        dk.NettPlant = p.Netto;
                                        dk.KAPlant = p.AktualKA;
                                        dk.LastModifiedBy = ((Users)Session["Users"]).UserName + "-" + p.SchNo.Substring(0, 2);
                                        dk.LastModifiedTime = DateTime.Now;
                                        dk.TglReceipt = DateTime.Now;
                                        UpdateDeliveryKertas(dk);
                                    }
                                    else
                                    {
                                        QAKadarAir qa = new QAKadarAir();
                                        qa.ID = int.Parse(p.SchID.ToString().Substring(1, (p.SchID.ToString().Length - 1)));
                                        qa.POKAID = rest;
                                        qa.LastModifiedBy = ((Users)Session["Users"]).UserName;
                                        qa.LastModifiedTime = DateTime.Now;
                                        UpdateKadarAirQC(qa);
                                    }
                                }
                                else
                                {
                                    CatatLogKadarAir(log);
                                }
                            }
                        }
                        Session["KadarAir"] = null;
                        #endregion
                        #region Kadar Air Plant Khusus depo KAT

                        if (Session["KadarAirDepo"] != null)
                        {
                            ArrayList arrKAPlant = (ArrayList)Session["KadarAirDepo"];
                            ZetroLib zl = new ZetroLib();
                            zl.hlp = new DeliveryKertas();
                            zl.TableName = "DeliveryKertasKAPlant";
                            zl.Option = "Insert";
                            zl.Criteria = "SchID,NoSJ,ItemCode,NOPOL,PlantID,GrossDepo,NettDepo,KADepo,GrossPlant,NettPlant,KAPlant,StdKA,SelisihKA,CreatedBy,CreatedTime";
                            zl.StoreProcedurName = "spDeliveryKertasKAPlant_Insert1";
                            string rst = zl.CreateProcedure();
                            if (rst == string.Empty)
                            {
                                foreach (DeliveryKertas dk in arrKAPlant)
                                {
                                    DeliveryKertas dks = new DeliveryKertas();
                                    #region insert date ke table deliverykertaskaplant
                                    switch (users.UnitKerjaID)
                                    {
                                        case 7:
                                            dks = dk;
                                            zl.hlp = dks;
                                            int result = zl.ProcessData();
                                            break;
                                        default:

                                            dks = dk;
                                            zl.hlp = dks;
                                            result = zl.ProcessData();
                                            //update ke karawang juga via web service
                                            bpas_api.WebService1 bpas = new bpas_api.WebService1();
                                            JavaScriptSerializer js = new JavaScriptSerializer();
                                            dks.PlantID = dk.PlantID;//1
                                            dks.ItemCode = dk.ItemCode;//2
                                            dks.NoSJ = dk.NoSJ;//3
                                            dks.GrossDepo = dk.GrossDepo;//4
                                            dks.GrossPlant = dk.GrossPlant;//5
                                            dks.NettDepo = dk.NettDepo;//6
                                            dks.NettPlant = dk.NettPlant;//7
                                            dks.KADepo = dk.KADepo;//8
                                            dks.KAPlant = dk.KAPlant;//9
                                            dks.StdKA = dk.StdKA;//10
                                            dks.SelisihKA = dks.SelisihKA;//11
                                            dks.NOPOL = dk.NOPOL;//12
                                            dks.CreatedBy = ((Users)Session["Users"]).UserID;//13
                                            dks.CreatedTime = DateTime.Now;//14
                                            dks.SchID = dk.SchID;
                                            string dkss = js.Serialize(dks);
                                            try
                                            {
                                                int rstu = bpas.UpdateDeliveryKertasKAPlant(dkss, "GRCBoardKrwg");
                                                if (rstu < 0)
                                                {
                                                    CatatLogKadarAir(dkss);
                                                }
                                            }
                                            catch
                                            {
                                                CatatLogKadarAir(dkss);
                                            }
                                            break;

                                    }
                                    #endregion
                                    #region update ke tabledeliverykertas (update pokaid)

                                    DepoKertasKA dka = new DepoKertasKA();
                                    DeliveryKertas dkst = new DeliveryKertas();
                                    dkst.ID = dk.KAID;
                                    dkst.POKAID = po.ID;
                                    dkst.NoSJ = dk.NoSJ;
                                    dkst.NOPOL = dk.NOPOL;
                                    dkst.ItemCode = dk.ItemCode;
                                    dkst.LastModifiedBy = ((Users)Session["Users"]).UserID;
                                    dkst.LastModifiedTime = DateTime.Now;
                                    dkst.PlantID = ((Users)Session["Users"]).UnitKerjaID;
                                    int r = dka.Update(dkst);

                                    #endregion
                                    #region Update ke table deliverykertska (update pokaid)

                                    QAKadarAir qad = new QAKadarAir();
                                    qad.ID = dk.SchID;
                                    qad.NOPOL = dk.NOPOL;
                                    qad.POKAID = po.ID;
                                    qad.NoSJ = dk.NoSJ;
                                    qad.ItemCode = dk.ItemCode;
                                    qad.GrossPlant = dk.GrossPlant;
                                    qad.PlantID = ((Users)Session["Users"]).UnitKerjaID;
                                    qad.LastModifiedBy = ((Users)Session["Users"]).UserName;
                                    qad.LastModifiedTime = DateTime.Now;
                                    UpdateKadarAirQC(qad, true);

                                    #endregion
                                }
                            }

                            Session["KadarAirDepo"] = null;
                        }
                        #endregion
                    }
                }
            }

            ////// Uang Muka
            TerminBayar terminBayar1 = new TerminBayar();
            TerminBayarFacade terminBayarFacade1 = new TerminBayarFacade();
            if (Session["NilaiUangMuka"] != null)
            {
                /** PPN 11% **/
                string PPN = string.Empty;
                if (Convert.ToInt32(thnbln) > 202203)
                {
                    PPN = "11";
                }
                else
                {
                    PPN = "10";
                }

                Domain.POPurchn po = new POPurchnFacade().RetrieveByNo(txtNoPO.Text);
                terminBayar1.POID = po.ID;
                ArrayList arrD = (ArrayList)Session["NilaiUangMuka"];
                foreach (TerminBayar t in arrD)
                {
                    //terminBayar1.DP = t.DP;
                    //terminBayar1.TerminKe = t.TerminKe;
                    //terminBayar1.JmlTermin = arrD.Count;
                    //terminBayar1.TotalBayar = t.Persentase * totalPrice / 100;
                    //terminBayar1.Persentase = t.Persentase;
                    ////insert ke table termin bayar
                    //terminBayarFacade1.Insert(terminBayar1);
                    //pOPurchn.UangMuka = t.DP;
                    //pOPurchn.CoaID = 0;
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "declare @i int " +
                            "set @i=(select count(id) from TerminBayar where TerminKe=" + t.TerminKe + " and POID=" + po.ID + ") " +
                            "if @i=0 " +
                            "begin " +
                            "INSERT INTO TerminBayar( POID, DP, TerminKe, JmlTermin, TotalBayar,rowstatus,Persentase ) values " +
                            "(" + po.ID + "," + t.DP + "," + t.TerminKe + "," + arrD.Count + "," + (t.Persentase * totalPrice / 100).ToString().Replace(",", ".") +
                            ",0," + t.Persentase.ToString().Replace(",", ".") + ")" +
                            "SELECT @@IDENTITY as ID" +
                            "end" +
                            "else" +
                            "begin" +
                            "    update TerminBayar set DP=" + t.DP + " , JmlTermin=" + arrD.Count + ", TotalBayar=" +
                            (t.Persentase * totalPrice / 100).ToString().Replace(",", ".") +
                            ",Persentase=" + t.Persentase.ToString().Replace(",", ".") + " " +
                            "where TerminKe=" + t.TerminKe + " and POID=" + po.ID + " " +
                            "end " +
                            " update TerminBayar set TotalBayar=(select " +
                            "case when (select ppn from POPurchn where id=TerminBayar.POID ) =0 then ((select price *QtY from POPurchnDetail where POID=TerminBayar.POID ) * persentase/100) " +
                            "else (((select price*QtY from POPurchnDetail where POID=TerminBayar.POID )+ ((select price*QtY from POPurchnDetail where POID=TerminBayar.POID )*" + PPN + "/100))* persentase/100) end) " +
                            "where  poid=" + po.ID;
                    SqlDataReader sdr = zl.Retrieve();
                }

            }
            else
            {
                pOPurchn.UangMuka = 0;
                pOPurchn.CoaID = 0;
            }
            //update uang muka di po
            POPurchnFacade pf1 = new POPurchnFacade();
            int resto1 = pf1.UpdateUangMuka(pOPurchn);

            ////// end Uang Muka
            txtKadarAir.Text = string.Empty;

            if (strError == string.Empty)
            {
                Session["NilaiUangMuka"] = null;

                InsertLog(strEvent);
                btnUpdate.Disabled = true;
            }
            Session["TotalPrice"] = null;
            LoadPO(txtNoPO.Text);
            if (CekUangMuka(txtNoPO.Text) > 0)
                btnUangMuka.Enabled = false;
            else
                btnUangMuka.Enabled = true;
            LoadPO(txtNoPO.Text);
        }
        private void UpdateKadarAirQC(QAKadarAir ka)
        {
            ZetroLib zl = new ZetroLib();
            zl.StoreProcedurName = "spDeliveryKertasKA_UpdatePO";
            zl.TableName = "DeliveryKertasKA";
            zl.Criteria = "ID,POKAID,LastModifiedBy,LastModifiedTime";
            zl.hlp = new QAKadarAir();
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = ka;
                int r = zl.ProcessData();
            }
        }
        private void UpdateKadarAirQC(QAKadarAir ka, bool depo)
        {
            QAKadarAir kad = (QAKadarAir)ka;
            string strSQL = "UPDATE DeliveryKertasKA set POKAID=" + kad.POKAID + " WHERE ID=" + kad.ID + " AND NoSJ='" + kad.NoSJ + "' AND NOPOL='" + kad.NOPOL + "' AND ItemCode='" + kad.ItemCode + "' and GrossPlant=" + kad.GrossPlant;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            int rst = sdr.RecordsAffected;
        }
        private void UpdateDeliveryKertas(DeliveryKertas dk)
        {
            RevisiPOne rpo = new RevisiPOne();
            ZetroLib zl = new ZetroLib();
            zl.StoreProcedurName = "spDeliveryKertas_UpdatePO";
            zl.TableName = "DeliveryKertas";
            zl.Criteria = "ID,POKAID,GrossPlant,NettPlant,KAPlant,TglReceipt,LastModifiedBy,LastModifiedTime";
            zl.hlp = new DeliveryKertas();
            zl.Option = "Update";
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = dk;
                int rest = zl.ProcessData();
            }
        }

        private void CatatLogKadarAir(string dataKA)
        {
            /**
            string path = Server.MapPath("~/App_Data/Upload/Log_KadarAirPO_Input.txt");
            string message = string.Format("[Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")+"]");
            message += "\n\r" + new JavaScriptSerializer().Serialize(dataKA).ToString() + "\n\r";
            using (StreamWriter Log = new StreamWriter(path, true))
            {
                Log.WriteLine(message);
                Log.Close();
            }
             */
        }
        protected void lbAddItem0_Click(object sender, EventArgs e)
        {
            if (txtNoPO.Text == string.Empty)
            {
                return;
            }
            if (lbEdit.Text == "Edit Detail")
            {
                lbEdit.Text = "Update Detail";
                GridView1.Visible = true;
                GridView2.Visible = false;
            }
            else
            {


                POPurchnDetailEditFacade pOPurchnDetailFacade = new POPurchnDetailEditFacade();
                ArrayList arrPOPurchnDetail = new ArrayList();
                if (Session["ListOfPOPurchnDetail"] != null)
                    arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
                Users users = (Users)Session["Users"];
                string strUsers = string.Empty;
                DateTime DlvDate;
                int ID;
                int intResult = 0;
                int i = 0;

                try
                {
                    foreach (Domain.POPurchn pOPurchnDetail in arrPOPurchnDetail)
                    {
                        TextBox txtDlvDate = (TextBox)GridView1.Rows[i].FindControl("txtdlvdate");
                        DlvDate = DateTime.Parse(txtDlvDate.Text);
                        strUsers = users.UserName;
                        ID = pOPurchnDetail.ID;
                        intResult = pOPurchnDetailFacade.UpdateDlv(ID, DlvDate, strUsers);
                        if (pOPurchnDetailFacade.Error != string.Empty)
                        {
                            break;
                        }
                        i = i + 1;
                    }
                }
                catch
                {
                    return;
                }
                lbEdit.Text = "Edit Detail";
                GridView1.Visible = false;
                GridView2.Visible = true;
                LoadPO(txtNoPO.Text);
                lstPO.DataSource = arrPOPurchnDetail;
                lstPO.DataBind();

            }
        }
        protected void btnDelete_ServerClick(object sender, EventArgs e)
        {
            //TransferOrder transferOrder = new TransferOrder();
            //transferOrder.ID = int.Parse(Session["id"].ToString());
            //transferOrder.LastModifiedBy = txtUsers.Text;

            //string strError = string.Empty;
            //TransferOrderProcessFacade transferOrderProsessFacade = new TransferOrderProcessFacade(transferOrder, new ArrayList());

            //strError = transferOrderProsessFacade.Delete();

            //if (strError == string.Empty)
            //{            
            //    clearForm();
            //}
        }
        protected void btnClose_ServerClick(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            if (users.Apv > 0)
            {
                POPurchnFacade pOPurchnFacade = new POPurchnFacade();
                Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByNo(txtNoPO.Text);
                if (pOPurchnFacade.Error == string.Empty)
                {
                    string strError = string.Empty;
                    pOPurchn.Status = -3; //PO di Close
                    POCloseCancelFacade pOCloseCancelFacade = new POCloseCancelFacade(pOPurchn);

                    if (pOPurchn.ID > 0)
                    {
                        strError = pOCloseCancelFacade.Update();
                    }
                }
            }
        }
        protected void btnPrint_ServerClick(object sender, EventArgs e)
        {
            //TransferOrder transferOrder = new TransferOrder();
            //transferOrder.ID = int.Parse(Session["id"].ToString());
            //transferOrder.LastModifiedBy = txtUsers.Text;

            //string strError = string.Empty;
            //TransferOrderProcessFacade transferOrderProsessFacade = new TransferOrderProcessFacade(transferOrder, new ArrayList());

            //strError = transferOrderProsessFacade.Delete();

            //if (strError == string.Empty)
            //{            
            //    clearForm();
            //}
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
                txtQty.Focus();
            }
            if (e.CommandName == "AddDelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrPOPurchnDetail = new ArrayList();
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
                Domain.POPurchn pOPurchnDetail = (Domain.POPurchn)arrPOPurchnDetail[index];
                string xNoSPP = pOPurchnDetail.NamaBarang;
                int zSPPDetailID = pOPurchnDetail.SPPDetailID;
                int zPOPurchnDetailID = pOPurchnDetail.ID;
                decimal zjml = pOPurchnDetail.Qty;

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
                arrPOPurchnDetail.RemoveAt(index);
                Session["ListOfTransferDetail"] = arrPOPurchnDetail;
                GridView1.DataSource = arrPOPurchnDetail;
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
                ArrayList arrPOPurchnDetail = new ArrayList();
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
                POPurchn pOPurchnDetail = (POPurchn)arrPOPurchnDetail[index];
                arrPOPurchnDetail.RemoveAt(index);
                Session["ListOfTransferDetail"] = arrPOPurchnDetail;
                GridView1.DataSource = arrPOPurchnDetail;
                GridView1.DataBind();
            }
        }
        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadPO(txtSearch.Text);
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    ArrayList arrPOPurchnDetail = new ArrayList();
                    if (Session["ListOfPOPurchnDetail"] != null)
                    {
                        arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
                    }
                    if (arrPOPurchnDetail.Count > 0)
                    {
                        Domain.POPurchn pOPurchnDetail = (Domain.POPurchn)arrPOPurchnDetail[e.Row.RowIndex];
                        TextBox txtDlvDate = (TextBox)e.Row.FindControl("txtDlvDate");
                        txtDlvDate.Text = pOPurchnDetail.DlvDate.ToString();
                    }
                }
            }
            catch
            {
            }
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
            string strValidate = ValidateItem();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                return;
            }
            SuppPurchFacade spf = new SuppPurchFacade();
            int sp = spf.SubCompanyID(int.Parse(txtSuppID.Value));
            int fDK = spf.SubCompanyID(int.Parse(txtSuppID.Value), true);
            if ((sp == 5 || fDK == 5) && ddlSJDepo.SelectedIndex == 0)
            {
                //DisplayAJAXMessage(this, "Nomor Surat Jalan harus di pilih");
                //return;
            }
            // ddlItemName.Enabled = true;
            ArrayList arrPOPurchnDetail = new ArrayList();
            ArrayList arrKadarAir = new ArrayList();
            ArrayList arrKadarAirPlant = new ArrayList();
            decimal totalPrice = 0;
            if (ddlTermOfPay.SelectedItem.Text.Trim().ToUpper() == "LAIN-LAIN" && txtTermOfPay.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Term payment lain-lain belum diisi");
                return;
            }
            if (Session["ListOfPOPurchnDetail"] != null)
            {
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
            }
            if (Session["KadarAir"] != null)
            {
                arrKadarAir = (ArrayList)Session["KadarAir"];
            }
            if (Session["KadarAirDepo"] != null)
            {
                arrKadarAirPlant = (ArrayList)Session["KadarAirDepo"];

            }
            if (Session["TotalPrice"] != null)
            {
                totalPrice = (decimal)Session["TotalPrice"];
            }
            //Nilai kurs jika selain rupiah harus di isi
            string InputKurs = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("InputNilaiKurs", "PO").ToString();
            //if (int.Parse(ddlMataUang.SelectedValue) > 1 && (nilaiKurs.Text == string.Empty || decimal.Parse(nilaiKurs.Text) < 1)
            //    && InputKurs == "Aktif")
            //{
            //    DisplayAJAXMessage(this, "Nilai Kurs Harus di isi untuk mata uang " + ddlMataUang.SelectedItem.Text);
            //    return;
            //}
            //ambil data SPP
            SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
            SPPDetail sPPDetail = new SPPDetail();
            //int zSPPDetailID = Convert.ToInt32(Session["SPPDetailID"].ToString());utk edit PO
            sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(int.Parse(ddlItemSPP.SelectedValue));
            //sPPDetail = sPPDetailFacade.RetrieveBySPPDetailID(zSPPDetailID); -- utk edit PO
            if (sPPDetailFacade.Error == string.Empty)
            {
                if ((sPPDetail.Quantity - (sPPDetail.QtyPO + decimal.Parse(txtQty.Text))) < 0)
                {
                    DisplayAJAXMessage(this, "Qty PO melebihi Qty SPP ..!");
                    return;
                }
                if (sPPDetail.ItemTypeID < 1 && sPPDetail.ItemTypeID > 3)
                {
                    DisplayAJAXMessage(this, "Items tersebut tidak termasuk dalam Tipe Barang");
                    return;
                }
            }
            //check lagi apaka sudah pernah di buat po atau belum spp tersebut
            POPurchnDetailFacade poDetail = new POPurchnDetailFacade();
            decimal qtyPO4SPP = poDetail.CheckQtyPO(sPPDetail.ID.ToString());
            if ((qtyPO4SPP + decimal.Parse(txtQty.Text)) > sPPDetail.Quantity)
            {
                DisplayAJAXMessage(this, "Quantity PO sudah melebihi Quantity SPP");
                nilaiKurs.Focus();
                return;
            }
            SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
            SuppPurch suppPurch = new SuppPurch();
            suppPurch = suppPurchFacade.RetrieveById(int.Parse(ddlSupplier.SelectedValue));

            MataUang mataUang = new MataUang();
            MataUangFacade mataUangFacade = new MataUangFacade();
            mataUang = mataUangFacade.RetrieveById(int.Parse(ddlMataUang.SelectedValue));

            if (lbAddItem.Text == "Add Item")
            {
                foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
                {
                    if (pOPurchnDetail.NoSPP == txtSPP.Text)
                    {
                        if (CheckStatusBiaya() == 1)
                        {
                            if (pOPurchnDetail.ItemID2.ToString() == txtIDBiaya.Text)
                            {
                                DisplayAJAXMessage(this, "No. SPP: '" + txtSPP.Text + "' Item Code: '" + txtItemCode.Text + "' sudah di entry !!");

                            }
                        }
                        else
                        {
                            if (pOPurchnDetail.ItemCode == txtItemCode.Text)
                            {
                                DisplayAJAXMessage(this, "No. SPP: '" + txtSPP.Text + "' Item Code: '" + txtItemCode.Text + "' sudah di entry !!");
                                //return;
                            }
                        }
                    }

                }
            }

            if (lbAddItem.Text == "Add Item")
            {

                POPurchnDetail pOPurchnDetail = new POPurchnDetail();
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

                //until here
                if (intItemsID > 0)
                //if (items.ID > 0)
                {
                    decimal xPPN = decimal.Parse(txtPPN.Text);
                    decimal xPPH = decimal.Parse(txtPPH.Text);
                    decimal xDiscount = Convert.ToDecimal(txtDiscount.Text);
                    //Check hargakertas
                    pOPurchnDetail.Price2 = HargaKhususKertas(int.Parse(txtSuppID.Value), sPPDetail.ItemID);
                    pOPurchnDetail.NoSPP = txtSPP.Text;
                    pOPurchnDetail.ItemCode = txtItemCode.Text;
                    pOPurchnDetail.NamaBarang = (CheckStatusBiaya() == 1) ? txtNamaBarang.Text : ddlItemSPP.SelectedItem.ToString();
                    pOPurchnDetail.Qty = decimal.Parse(txtQty.Text);
                    pOPurchnDetail.Price = decimal.Parse(txtHarga.Text);

                    pOPurchnDetail.ItemID = sPPDetail.ItemID;
                    pOPurchnDetail.Satuan = sPPDetail.Satuan;
                    pOPurchnDetail.GroupID = sPPDetail.GroupID;
                    pOPurchnDetail.ItemTypeID = sPPDetail.ItemTypeID;
                    pOPurchnDetail.UOMID = sPPDetail.UOMID;
                    pOPurchnDetail.SPPID = sPPDetail.SPPID;

                    string intSppDetailID = int.Parse(ddlItemSPP.SelectedValue).ToString();
                    pOPurchnDetail.SPPDetailID = int.Parse(intSppDetailID);
                    pOPurchnDetail.DocumentNo = txtSPP.Text;

                    pOPurchnDetail.DlvDate = DateTime.Parse(txtDate0.Text);
                    arrPOPurchnDetail.Add(pOPurchnDetail);
                    totalPrice = totalPrice + (decimal.Parse(txtQty.Text) * decimal.Parse(txtHarga.Text));
                    totalPrice = totalPrice + ((xPPH / 100) * totalPrice) + ((xPPN / 100) * totalPrice) - ((xDiscount / 100) * totalPrice);
                }
            }

            TerbilangFacade terbilangFacade = new TerbilangFacade();
            txtTotalPrice.Text = mataUang.Lambang + " " + totalPrice.ToString("#,#.00#;(#,#.00#)");
            //txtTerbilang.Text = terbilangFacade.ConvertMoneyToWords(totalPrice) + ' ' + mataUang.Nama;
            //POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            //string xterbilang = pOPurchnFacade.Terbilang(totalPrice);
            //txtTerbilang.Text = xterbilang + ' ' + mataUang.Nama;
            //txtTotalPrice.Text = totalPrice.ToString();

            ViewState["Baris"] = jmlItem;
            Session["TotalPrice"] = totalPrice;
            Session["ListOfPOPurchnDetail"] = arrPOPurchnDetail;
            //GridView1.DataSource = arrPOPurchnDetail;
            //GridView1.DataBind();
            GridView2.DataSource = arrPOPurchnDetail;
            GridView2.DataBind();
            lstPO.DataSource = arrPOPurchnDetail;
            lstPO.DataBind();
            decimal actKA = 0; decimal actSampah = 0; decimal actStdKA = 0; decimal actGros = 0;
            decimal NettoPlant = 0;
            decimal.TryParse(txtStdKAPlant.Text, out actStdKA);
            decimal.TryParse(txtKadarAirPlant.Text, out actKA);
            decimal.TryParse(txtGrossPlant.Text, out actGros);
            decimal.TryParse(txtSampah.Text, out actSampah);
            decimal.TryParse(txtNettoPlant.Text, out NettoPlant);
            Domain.POPurchn po = new Domain.POPurchn();
            po.StdKA = actStdKA;// Convert.ToDecimal(txtStdKAPlant.Text.Replace(".", ","));
            po.Gross = actGros;// Convert.ToDecimal(txtGross.Text.Replace(".", ","));
            po.AktualKA = actKA;// Convert.ToDecimal(txtKadarAir.Text.Replace(".", ","));
            po.Netto = NettoPlant;// Convert.ToDecimal(txtNetto.Text.Replace(".", ","));
            po.NoPol = txtNoPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper();
            po.Sampah = actSampah;// decimal.Parse(txtSampah.Text);
            po.SchNo = ddlSJDepo.SelectedItem.Text;
            string sch = ddlSJDepo.SelectedItem.Text.Substring(0, 2);
            //po.SchID = (sch == "KR" || sch == "CT") ? int.Parse(ddlSJDepo.SelectedValue.Substring(1, ddlSJDepo.SelectedValue.Length - 1)) : 
            po.SchID = int.Parse(ddlSJDepo.SelectedValue);//untuk KA inputan plan di kasih angka 7 di depan ID dropdown
            arrKadarAir.Add(po);
            Session["KadarAir"] = (arrKadarAir.Count > 0) ? arrKadarAir : null;
            if (sp == 5 || fDK == 5)
            {
                RevisiPOne rpo = new RevisiPOne();
                int jmlData = rpo.JumlahKirimanDepo(ddlSJDepo.SelectedItem.Text, txtItemCode.Text);
                if (jmlData == 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", "inputKadarAir('" + ddlSJDepo.SelectedItem.Text + ":" + txtItemCode.Text + "')", true);
                }
            }
            clearQty();
            ddlSupplier.Enabled = false;
            txtSup.ReadOnly = true;
            /** tambahkan session Kadar Air */
        }
        protected void txtSup_Change(object sender, EventArgs e)
        {
            string Supplierx = txtSup.Text.ToString();
            if (Session["groupidpo"] != null)
            {
                if (Session["groupidpo"].ToString().Trim() == "13")
                {
                    if (cekSupplierNonGRC(txtSuppID.Value) != 8)
                    {
                        txtSup.Text = "";
                        ddlItemSPP.Focus();
                        DisplayAJAXMessage(this, "Supplier harus kelompok Non GRC");
                        return;
                    }
                }
                else
                {
                    if (cekSupplierNonGRC(txtSuppID.Value) == 8)
                    {
                        txtSup.Text = "";
                        ddlItemSPP.Focus();
                        DisplayAJAXMessage(this, "Supplier hanya untuk kelompok barang Non GRC");
                        return;
                    }
                }
            }
            else
            {
                txtSup.Text = "";
                ddlItemSPP.Focus();
                return;
            }
            ddlSupplier.Items.Clear();
            ddlSupplier.Items.Add(new ListItem("--Pilih Supplier--", "0"));
            ddlSupplier.Items.Add(new ListItem(Supplierx, txtSuppID.Value));
            ddlSupplier.SelectedValue = txtSuppID.Value;
            ddlSupplier_SelectedIndexChanged(ddlSupplier, null);
            txtHarga.Focus();
            TampikanFormKadarAir(txtSuppID.Value);
        }
        private int cekSupplierNonGRC(string supplierID)
        {
            int subcompany = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from SuppPurch where ID=" + supplierID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    subcompany = Convert.ToInt32(sdr["SubcompanyID"].ToString());
                }
            }

            return subcompany;
        }
        private decimal HargaKhususKertas(int SupplierID, int ItemID)
        {
            POPurchnFacade po = new POPurchnFacade();
            decimal harga = po.GetHargaKertas(SupplierID, ItemID);
            return harga;
        }
        private void TampikanFormKadarAir(string p)
        {
            string DefaultKadarAir = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
            string KadarAirAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KadarAirAktif", "PO");
            string[] ItemKertas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemCheckedCode", "PO").Split(',');
            int PosKertas = Array.IndexOf(ItemKertas, txtItemCode.Text);
            if (PosKertas > -1)
            {
                frmKA.Visible = true;
                LoadSuratJalanDepo(p);
                txtQty.ReadOnly = true;
                txtStdKAPlant.Text = DefaultKadarAir.ToString();
            }
            else
            {
                frmKA.Visible = false;
                txtQty.ReadOnly = false;
                LoadSuratJalanDepo(p);
            }
        }
        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            string thn = DateTime.Now.Year.ToString();
            string bln = (DateTime.Now.Month.ToString().Length == 1) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string thnbln = thn + bln;

            try
            {
                DropDownList ddl = (DropDownList)sender;
                Users users = (Users)Session["Users"];
                POPurchnDetailFacade podetail = new POPurchnDetailFacade();
                LastPrice lastPrice = new LastPrice();
                //rbCredit.Checked = false;
                //rbCash.Checked = false;
                lastPrice = podetail.GetLastPrice(int.Parse(ddlItemSPP.SelectedValue), ddlSupplier.SelectedItem.Text, users.ViewPrice);
                string npwp = string.Empty;
                if (ddl.SelectedIndex > 0)
                {
                    SuppPurchFacade suppPurchFacade = new SuppPurchFacade();
                    SuppPurch suppPurch = suppPurchFacade.RetrieveById(int.Parse(ddl.SelectedValue));
                    npwp = suppPurch.NPWP.Trim();
                    if (suppPurchFacade.Error == string.Empty)
                    {
                        if (suppPurch.ID > 0)
                        {
                            txtKodeSupplier.Text = suppPurch.SupplierCode;
                            txtUp.Text = suppPurch.UP;
                            txtTelepon.Text = suppPurch.Telepon;
                            txtFax.Text = suppPurch.Fax;
                            txtHarga.Text = lastPrice.Price.ToString();
                            if (lastPrice.Crc > 0)
                                ddlMataUang.SelectedValue = lastPrice.Crc.ToString();
                            if (npwp.Length > 5)
                            {
                                /** PPN 11% added 31 Maret 2022 oleh : Beny **/
                                if (Convert.ToInt32(thnbln) > 202203)
                                {
                                    txtPPN.Text = "11";
                                    txtPPN.Enabled = false;
                                }
                                else
                                {
                                    txtPPN.Text = "10";
                                    txtPPN.Enabled = false;
                                }
                            }
                            else
                            {
                                txtPPN.Text = "0";
                                txtPPN.Enabled = true;
                            }
                            //txtPPN.Text = (suppPurch.PKP == "yes") ? "10" : "0";
                            string TerminPay = suppPurchFacade.TermOfPayment(suppPurch.ID.ToString());
                            ddlTermOfPay.ClearSelection();
                            foreach (ListItem ls in ddlTermOfPay.Items)
                            {
                                if (ls.Text == TerminPay)
                                {
                                    ls.Selected = true;
                                    //return;
                                }
                            }
                            //rbCash.Checked = (TerminPay == "Cash") ? true : false;
                            //rbCredit.Checked = (rbCash.Checked == false) ? true : false;
                            ddlTermOfPay_SelectedIndexChanged(null, null);
                            txtSubCompanyID.Value = suppPurchFacade.SubCompanyID(suppPurch.ID).ToString();
                        }
                    }
                    //if (Session["KadarAir"] != null)
                    //{
                    //    ArrayList arrKA = (ArrayList)Session["KadarAir"];
                    //    foreach (POPurchn ka in arrKA)
                    //    {
                    //        txtQty.Text = ka.Netto.ToString();
                    //    }
                    //}
                }
            }
            catch { }
        }
        protected void ChkIndent_Changed(object sender, EventArgs e)
        {
            if (ChkIndent.Checked == true)
            {
                ddlIndent.Enabled = true;
            }
            else
            {
                ddlIndent.Enabled = false;
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
                lastPrice = podetail.GetLastPrice(int.Parse(ddl.SelectedValue), ddlSupplier.SelectedItem.Text, users.ViewPrice);
                if (sPPDetailFacade.Error == string.Empty)
                {

                    if (sPPDetail.ID > 0)
                    {
                        ddlTipeSPP.SelectedValue = sPPDetail.GroupID.ToString();
                        //txtDate0.Text = sPPDetail.TglKirim.ToString("dd-MMM-yyyy"); //aktifkan saat leadtime diterapkan
                        AccClosingFacade biaya = new AccClosingFacade();
                        AccClosing aktif = biaya.BiayaNewActive();
                        int tgl = (aktif.CreatedTime <= sppDate.CreatedTime) ? 1 : 0;
                        txtNamaBarang.Text = (CheckStatusBiaya() == 1 && sPPDetail.ItemTypeID == 3 && tgl == 1) ? sPPDetail.Keterangan : sPPDetail.ItemName;
                        //sehingga qty sisa spp yg terambil
                        txtQty.Text = (sPPDetail.Quantity - sPPDetail.QtyPO).ToString("N2");

                        Session["QuantitySPP"] = sPPDetail.Quantity - sPPDetail.QtyPO;
                        txtSatuan.Text = sPPDetail.Satuan;
                        if (sPPDetail.ItemTypeID == 3)
                        {
                            string[] ItemName = ddlItemSPP.SelectedItem.Text.Split('-');
                            ItemeID = new POPurchnFacade().GetIDBiaya(ItemName[1].ToString());

                        }
                        else
                        {
                            ItemeID = sPPDetail.ItemID.ToString();
                        }
                        DateTime nextDate = SchDelivery(sPPDetail.SPPID.ToString(), ItemeID);// AddBusinessDays(sppDate.ApproveDate2, CheckLeadTime(sPPDetail.ItemID));
                        txtDate0.Text = nextDate.ToString("dd-MMM-yyyy");
                        txtItemCode.Text = sPPDetail.ItemCode;
                        txtIDBiaya.Text = ItemIDBiayaOld(sPPDetail.Keterangan);
                        txtHarga.Text = lastPrice.Price.ToString();
                        string[] arrUnLock = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnLockItemCode", "PO").Split(',');
                        leadTime.InnerHtml = (ItemeID != string.Empty) ? "Lead Time :" + CheckLeadTime(int.Parse(ItemeID.ToString()), sPPDetail.ItemTypeID).ToString() + "WD" : "";
                        txtDate0.Enabled = (!arrUnLock.Contains(sPPDetail.ItemCode) && CheckLeadTime(sPPDetail.ItemID, sPPDetail.ItemTypeID) > 0 && CheckDeliveStatus() > 0) ? false : true;
                        txtDate0.Enabled = (sppDate.PermintaanType < 3) ? txtDate0.Enabled : true;
                        if (sppDate.GroupID == 4 || sppDate.GroupID == 5)
                        {
                            txtDate0.Enabled = true;
                        }
                        else
                        {
                            txtDate0.Enabled = false;
                        }
                        if (lastPrice.Crc > 0)
                            ddlMataUang.SelectedValue = lastPrice.Crc.ToString();
                        // SelectTipeSPP(sPPDetail.GroupID);
                        if (sPPDetail.Quantity - sPPDetail.QtyPO <= 0)
                        {
                            DisplayAJAXMessage(this, "Qty <= Nol ..!");
                            return;
                        }
                    }
                }
                /**
                 * Kadar Air Input
                 */
                string DefaultKadarAir = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                string KadarAirAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KadarAirAktif", "PO");
                string[] ItemKertas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("ItemCheckedCode", "PO").Split(',');
                if (KadarAirAktif == "1" && ItemKertas.Contains(txtItemCode.Text))
                {
                    //if (ddlSupplier.SelectedIndex == 0) { return; }
                    //Panel1.Attributes.Add("style", "display:block;position:absolute;top:150px;left:130px;");
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", "inputKadarAir('"+ddlSupplier.SelectedValue+"')", true);
                }
                if (!ItemKertas.Contains(txtItemCode.Text))
                {
                    GetHargaTerendah(ItemeID, sPPDetail.ItemTypeID, txtItemCode.Text.TrimEnd().TrimStart());
                }
            }
        }
        private void GetHargaTerendah(string ItemID, int TypeID, string ItemCode)
        {
            try
            {
                POPurchnFacade po = new POPurchnFacade();
                decimal Harga1 = 0; decimal Harga2 = 0;
                string Plant = string.Empty;
                string Plant2 = string.Empty;
                switch (((Users)Session["Users"]).UnitKerjaID)
                {
                    case 1:
                        Plant = "Citeureup";
                        Plant2 = "Karawang";
                        break;
                    case 7:
                        Plant = "Karawang";
                        Plant2 = "Citeureup";
                        break;

                }
                string Message1 = string.Empty; string Message2 = string.Empty;
                ArrayList arrData = po.HargaRendah(ItemID, TypeID);
                //GetItemID dari plant  lain
                string ImdID = ItemIDPlantLine(ItemCode, TypeID);
                ArrayList arrData2 = HargaPlantLine(ImdID, TypeID); ;
                string titikdua = " : ";
                Message1 += "Perolehan Harga Terendah :\\n\\n";
                foreach (Domain.POPurchn p1 in arrData)
                {
                    Harga1 = Convert.ToDecimal(p1.Price);
                    Message1 += "Plant " + titikdua.PadLeft((27 - ("Plant").ToString().Length), ' ') + Plant.ToUpper() + "\\n";
                    Message1 += "Harga Terendah " + titikdua.PadLeft((17 - ("Harga Terendah").ToString().Length), ' ') + (Harga1.ToString()) + "\\n";
                    Message1 += "Supplier Name" + titikdua.PadLeft((19 - ("Supplier Name").ToString().Length), ' ') + p1.SupplierName;
                }
                foreach (Domain.POPurchn p2 in arrData2)
                {
                    Harga2 = Convert.ToDecimal(p2.Price);
                    Message2 += "\\n\\n";
                    Message2 += "Plant " + titikdua.PadLeft((27 - ("Plant").ToString().Length), ' ') + Plant2.ToUpper() + "\\n";
                    Message2 += "Harga Terendah " + titikdua.PadLeft((17 - ("Harga Terendah").ToString().Length), ' ') + (Harga2.ToString()) + "\\n";
                    Message2 += "Supplier Name" + titikdua.PadLeft((19 - ("Supplier Name").ToString().Length), ' ') + p2.SupplierName;
                }
                //if (Harga1 > 0 && Harga1 < Harga2)
                //{
                DisplayAJAXMessage(this, Message1 + "\\n" + Message2);
                //}
                //else if (Harga2 > 0 && Harga2 < Harga1)
                //{
                //    DisplayAJAXMessage(this, Message2);
                //}
            }
            catch { }
        }
        public string ItemIDPlantLine(string ItemCode, int ItemTypeID)
        {
            string result = string.Empty;
            //bpas_api.WebService1 bpas = new bpas_api.WebService1();
            Global2 bpas = new Global2();
            DataSet ds = new DataSet();
            string con = string.Empty;
            string Tabel = string.Empty;
            string Criteria = string.Empty;
            switch (((Users)Session["Users"]).UnitKerjaID)
            {
                case 7: con = "GRCBoardCtrp"; break;
                case 1: con = "GRCBoardKrwg"; break;
                default: con = "GRCBoardPurch"; break;
            }
            switch (ItemTypeID)
            {
                case 1: Tabel = "Inventory"; break;
                case 2: Tabel = "Asset"; break;
                case 3: Tabel = "Biaya"; break;
            }
            Criteria = " Where ItemCode='" + ItemCode + "'";
            try
            {
                ds = bpas.GetDataFromTable(Tabel, Criteria, con);
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["ID"].ToString();
                }
            }
            catch { }
            return result;
        }
        public ArrayList HargaPlantLine(string ItemID, int ItemTypeID)
        {
            bpas_api.WebService1 bpas = new bpas_api.WebService1();
            ArrayList arrData = new ArrayList();
            DataSet ds = new DataSet();
            string con = string.Empty;
            switch (((Users)Session["Users"]).UnitKerjaID)
            {
                case 7:
                    con = "GRCBoardCtrp";
                    break;
                case 1:
                    con = "GRCBoardKrwg";
                    break;
                default:
                    con = "GRCBoardPurch";
                    break;
            }
            try
            {
                ds = bpas.GetHargaTerendah(ItemID, ItemTypeID.ToString(), con);
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    arrData.Add(new Domain.POPurchn
                    {
                        ID = Convert.ToInt32(d["ID"].ToString()),
                        ItemID = Convert.ToInt32(d["ItemID"].ToString()),
                        Price = Convert.ToDecimal(d["Price"].ToString()),
                        SupplierID = Convert.ToInt32(d["SupplierID"].ToString()),
                        SupplierName = d["SupplierName"].ToString()
                    });
                }
                return arrData;
            }
            catch (Exception)
            {
                return arrData;
            }
        }
        private DateTime SchDelivery(string SPPID, string ItemID)
        {
            POPurchnFacade po = new POPurchnFacade();
            DateTime TglDelive = po.LeadTime(SPPID, ItemID);
            int CheckHariAPA = this.OffDayCalender(TglDelive, TglDelive);
            return TglDelive.AddDays(CheckHariAPA);
        }
        public DateTime AddBusinessDays(DateTime date, int days)
        {
            Domain.POPurchn WorkDays = new POPurchnFacade().PurchnTools("WorkDay");
            if (days == 0) return date;
            #region perhitungan lama tidak sempurna
            int i = 0;
            while (i <= days)
            {
                if (WorkDays.Status == 5)
                {
                    if (!(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday))
                    {
                        i++;
                    }

                    if (i == days && date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        date = date.AddDays(1);
                    }
                    else if (i == days && date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        date = date.AddDays(2);
                    }
                    else
                    {
                        date = date.AddDays(1);
                    }
                }
                else if (WorkDays.Status == 6)
                {
                    if (!(date.DayOfWeek == DayOfWeek.Sunday)) i++;
                    date = date.AddDays(1);
                }
            }
            #endregion
            #region perhitungan baru langsung baca dari sql server

            #endregion
            return date;
        }
        public int OffDayCalender(DateTime StarDate, DateTime EndData)
        {
            int TambahHari = 0;
            POPurchnFacade po = new POPurchnFacade();
            Domain.POPurchn Libur = po.DayOffCalender(StarDate.ToString("yyyyMMdd"), EndData.ToString("yyyyMMdd"));
            if (Libur.Status > 0)
            {
                Domain.POPurchn HariAPA = po.DayOffCalender(StarDate.ToString("yyyyMMdd"), EndData.ToString("yyyyMMdd"), true);
                switch (HariAPA.Remark.Trim())
                {
                    case "Friday":
                        TambahHari = 2;
                        break;
                    default:
                        TambahHari = 0;
                        break;
                }
            }
            return TambahHari;
        }
        protected void txtKts_Change(object sender, EventArgs e)
        {

        }
        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            if (txtQty.Text != string.Empty)
            {
                //int xQtySPP = Convert.ToInt32(Session["QuantitySPP"]);
                //int xText = int.Parse(txtQty.Text);
                /**
                 * Penambahan input kadar air
                 * 01-10-2015
                 */

                decimal xQtySPP = Convert.ToDecimal(Session["QuantitySPP"]);
                decimal xText = decimal.Parse(txtQty.Text);
                if (xText > xQtySPP)
                {
                    txtQty.Text = xQtySPP.ToString();
                }
            }
        }
        protected void showMessageBox(string message)
        {
            string sScript;
            message = message.Replace("'", "\'");
            sScript = String.Format("alert('{0}');", message);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        }
        private void SelectItem(string IntSPPDetailID, string strItemName)
        {
            //ddlItemName.ClearSelection();
            //ddlItemSPP.SelectedIndex = Convert.ToInt16(IntSPPDetailID.ToString());
            Session["SPPDetailID"] = IntSPPDetailID;
            //foreach (ListItem item in ddlItemSPP.Items)
            //{
            //    if (item.Value == IntSPPDetailID)
            //    {
            //        item.Selected = true;
            //        return;
            //    }
            //}
        }
        private void SelectTipeSPP(int groupPurchn)
        {

            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            GroupsPurchn groupsPurchn = groupsPurchnFacade.RetrieveById(groupPurchn);
            if (groupsPurchnFacade.Error == string.Empty)
            {
                ddlTipeSPP.ClearSelection();
                foreach (ListItem item in ddlTipeSPP.Items)
                {
                    if (item.Text == groupsPurchn.GroupDescription)
                    {
                        item.Selected = true;
                        return;
                    }
                }
            }
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {

            Session["ListOfPODetail"] = null;
            Session["NoSPP"] = null;

            Response.Redirect("ListPOPurchn.aspx?approve=" + (((Users)Session["Users"]).GroupID));
        }
        protected void btnUangMuka_ServerClick(object sender, EventArgs e)
        {
            //    //pastikan NoPO, isi detail & data supplierID ada
            Session["NoPO"] = null;
            if (txtNoPO.Text.Trim() != string.Empty)
                Session["NoPO"] = txtNoPO.Text;
            else
            {
                DisplayAJAXMessage(this, "PO belum tersimpan");
                return;
            }
            //    Session["ListOfPODetail"] = null;
            //    Session["NoSPP"] = null;

            //    Response.Redirect("ListPOPurchn.aspx?approve=" + (((Users)Session["Users"]).GroupID));
        }
        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            if (Session["id"] != null)
            {
                POPurchnFacade pOPurchnFacade = new POPurchnFacade();
                Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByNo(txtNoPO.Text);
                if (pOPurchnFacade.Error == string.Empty)
                {
                    string strError = string.Empty;
                    pOPurchn.Status = -1; //PO di Cancel
                    POCloseCancelFacade pOCloseCancelFacade = new POCloseCancelFacade(pOPurchn);
                    if (pOPurchn.ID > 0)
                    {
                        strError = pOCloseCancelFacade.Update();
                    }
                }
            }
            Response.Redirect("FormPOPurchn.aspx");
        }
        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Input PO";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtNoPO.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;
            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);

        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        private string ValidateText()
        {
            //if (ddlFromDepo.SelectedIndex == 0)
            //    return "Pilih Dari Depo";
            //else if (ddlToDepo.SelectedIndex == 0)
            //    return "Pilih Ke Depo";

            //ArrayList arrTransferDetail = new ArrayList();
            //if (Session["ListOfTransferDetail"] != null)
            //{
            //    arrTransferDetail = (ArrayList)Session["ListOfTransferDetail"];
            //    if (arrTransferDetail.Count == 0)
            //        return "Item Barang tidak ada yang di transfer";
            //}

            return string.Empty;
        }
        private string ValidateItem()
        {
            if (ddlSupplier.SelectedIndex == 0)
                return "Pilih Nama Supplier";
            else if (ddlItemSPP.SelectedIndex == 0)
                return "Pilih Item Barang dari SPP yang bersangkutan";
            else if (txtSPP.Text == string.Empty)
                return "Isi No. SPP";
            else if (ddlMataUang.SelectedIndex == 0)
                return "Kurs/Mata Uang harus di pilih";

            try
            {
                decimal dec = decimal.Parse(txtHarga.Text);
            }
            catch
            {
                return "Harga harus numeric";
            }

            try
            {
                decimal dec = decimal.Parse(txtQty.Text);
            }
            catch
            {
                return "Quantity harus numeric";
            }

            if (decimal.Parse(txtQty.Text) <= 0)
                return "Quantity harus lebih dari Nol";
            return string.Empty;
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
                ArrayList arrPOPurchnDetail = new ArrayList();
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
                Domain.POPurchn pOPurchnDetail = new Domain.POPurchn();
                pOPurchnDetail = (Domain.POPurchn)arrPOPurchnDetail[index];
                string xNoSPP = pOPurchnDetail.NOSPP;
                int zSPPDetailID = pOPurchnDetail.SPPDetailID;
                int zPOPurchnDetailID = pOPurchnDetail.ID;
                decimal zjml = pOPurchnDetail.Qty;
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
                arrPOPurchnDetail.RemoveAt(index);
                Session["ListOfPOPurchnDetail"] = arrPOPurchnDetail;
                GridView2.DataSource = arrPOPurchnDetail;
                GridView2.DataBind();
                if (arrPOPurchnDetail.Count == 0)
                {
                    POPurchnFacade pOPurchnFacade = new POPurchnFacade();
                    Domain.POPurchn pOPurchn = pOPurchnFacade.RetrieveByNo(txtNoPO.Text);
                    if (pOPurchnFacade.Error == string.Empty)
                    {
                        strError = string.Empty;
                        pOPurchn.Status = -1; //PO di Cancel
                        POCloseCancelFacade pOCloseCancelFacade = new POCloseCancelFacade(pOPurchn);
                        if (pOPurchn.ID > 0)
                        {
                            strError = pOCloseCancelFacade.Update();
                        }
                    }
                    clearForm();
                }
            }
            else if (e.CommandName == "Hapus")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ArrayList arrPOPurchnDetail = new ArrayList();
                arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];
                POPurchnDetail pOPurchnDetail = (POPurchnDetail)arrPOPurchnDetail[index];
                arrPOPurchnDetail.RemoveAt(index);
                Session["ListOfPOPurchnDetail"] = arrPOPurchnDetail;
                GridView2.DataSource = arrPOPurchnDetail;
                GridView2.DataBind();
            }
        }
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int value = int.Parse(e.Row.Cells[10].Text);
                if (value == -1)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[1].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[2].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[3].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[4].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[5].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[6].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[7].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[8].BackColor = System.Drawing.Color.FromName("#FF3300");
                    e.Row.Cells[7].Enabled = false;
                    e.Row.Cells[8].Enabled = false;
                }
            }
        }
        protected void ddlTermOfPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            string UangMukaAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UangMuka", "PO");
            try
            {
                switch (Convert.ToInt16(ddlTermOfPay.SelectedValue))
                {
                    case 6:
                        rbCredit.Checked = false;
                        rbCash.Checked = true;
                        break;
                    case 8:
                        rbCredit.Checked = false;
                        rbCash.Checked = true;
                        if (UangMukaAktif == "1")
                        {
                            //btnUangMuka_ServerClick(null, null);
                            //btnUangMuka.Attributes.Add("onclick", "return input_uang_muka();");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "UangMuka", "input_uang_muka()", true);
                        }
                        break;
                    default:
                        rbCredit.Checked = true;
                        rbCash.Checked = false;
                        break;
                }
                txtTermOfPay.Text = string.Empty;
                if (ddlTermOfPay.SelectedItem.Text.Trim().ToUpper() == "LAIN-LAIN")
                {
                    txtTermOfPay.Visible = true;
                }
                else
                {
                    txtTermOfPay.Visible = false;
                }
            }
            catch
            {
            }
        }
        private int CheckStatusBiaya()
        {
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing stat = cls.CheckBiayaAktif();

            return stat.Status;
        }
        private int CheckLeadTime(int ItemID, int ItemTypeID)
        {
            Inventory objInv = new Inventory();
            InventoryFacade Inv = new InventoryFacade();
            objInv = Inv.RetrieveByIdNew(ItemID, ItemTypeID);
            return (Inv.Error == string.Empty) ? objInv.LeadTime : 1;
        }
        protected void txtCariSupplier_TextChanged(object sender, EventArgs e)
        {

        }
        private int CheckDeliveStatus()
        {
            POPurchnFacade poTools = new POPurchnFacade();
            Domain.POPurchn objTools = poTools.PurchnTools("PODeliv");
            return objTools.Status;
        }
        private string ItemIDBiayaOld(string BiayaName)
        {
            BiayaFacade biayafacade = new BiayaFacade();
            Biaya obj = biayafacade.RetrieveByName(BiayaName);
            if (biayafacade.Error == string.Empty && obj.ID > 0)
            {
                return obj.ID.ToString();
            }
            return string.Empty;
        }
        protected void btnListSPP_ServerClick(object sender, EventArgs e)
        {
            Session["ListOfPODetail"] = null;
            Session["NoSPP"] = null;

            Response.Redirect("ListSPPnew.aspx?approve=2");

        }
        protected void btnUpdHeader_Click(object sender, EventArgs e)
        {

        }
        protected void btnRevisi_Click(object sender, EventArgs e)
        {
            if (Session["AlasanCancel"] != null)
            {
                if (Session["AlasanCancel"].ToString() == string.Empty)
                {
                    DisplayAJAXMessage(this, "Alasan Revisi tidak boleh kosong");
                    return;
                }
                RevisiPOne rpo = new RevisiPOne();
                Revisi rp = new Revisi();
                rp.ID = int.Parse(Session["ID"].ToString());
                rp.Approval = 0;// ((Users)Session["Users"]).Apv - 1;
                rp.LastModifiedBy = ((Users)Session["Users"]).UserID.ToString();
                rp.LastModifiedTime = DateTime.Now;
                rpo.Criteria = "ID,Approval,LastModifiedBy,LastModifiedTime";
                rpo.Pilihan = "Update";
                rpo.TableName = "POPurchn";
                string rst = rpo.CreateProcedure(rp, "spPOPurchn_Revisi");
                if (rst == string.Empty)
                {
                    int rest = rpo.ProcessData(rp, "spPOPurchn_Revisi");
                    /*process loging transaksi*/
                    if (rest > 0)
                    {
                        RevisiPOne mp = new RevisiPOne();
                        Revisi ro = new Revisi();
                        mp.Criteria = "NoPO,POID,PODetailID,RevisiKe,AlasanRevisi,RevisiBy,RevisiTime";
                        mp.Pilihan = "Insert";
                        mp.TableName = "POPurchnRevisi";
                        POPurchnFacade podtl = new POPurchnFacade();
                        Domain.POPurchn podetail = podtl.RetrieveByID(int.Parse(Session["ID"].ToString()));
                        ro.NoPO = podetail.NoPO;
                        ro.POID = podetail.ID;
                        ro.PODetailID = 0;
                        ro.RevisiKe = podetail.NoUrut;
                        ro.RevisiTime = DateTime.Now;
                        ro.AlasanRevisi = Session["AlasanCancel"].ToString();
                        ro.RevisiBy = ((Users)Session["Users"]).UserID.ToString();
                        string rs = mp.CreateProcedure(ro, "spPOPurchnRevisi_Insert");
                        if (rs == string.Empty)
                        {
                            int result = mp.ProcessData(ro, "spPOPurchnRevisi_Insert");
                            if (result > 0)
                            {
                                DisplayAJAXMessage(this, "PO Siap di revisi oleh admin");
                                Session["AlasanCancel"] = null;
                            }
                        }
                    }
                }
                btnNew_ServerClick(null, null);
            }
        }
        private string Round = string.Empty;
        private bool Rounded = false;
        /**
        protected void txtKadarAir_Change(object sender, EventArgs e)
        {
            if (txtGross.Text.Replace(",","") != string.Empty)
            {
                Round = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Rounded", "PO");
                Rounded = (Round == "yes") ? true : false;
                decimal SelisihKA = 0;
                decimal Netto = 0;
                decimal stdrKA = Convert.ToDecimal(txtStdKA.Text.Replace(".", ","));
                decimal hasilKA = Convert.ToDecimal(txtKadarAir.Text.Replace(".", ","));
                decimal Gross = Convert.ToDecimal(txtGross.Text.Replace(".", ","));
                SelisihKA = stdrKA - hasilKA;
                //Netto = (SelisihKA > 0) ? (Gross + (Gross * SelisihKA / 100)) : (Gross - (Gross * SelisihKA / 100));
                Netto = (Rounded == true) ? Math.Round((Gross + (Gross * SelisihKA / 100)), 0) : (Gross + (Gross * SelisihKA / 100));
                txtNetto.Text = Netto.ToString();
                txtQty.Text = Netto.ToString();
            }
        }
        protected void txtGross_Change(object sender, EventArgs e)
        {
            if (txtKadarAir.Text != string.Empty || txtKadarAir.Text != ",")
            {
                Round = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Rounded", "PO");
                Rounded = (Round == "yes") ? true : false;
                decimal SelisihKA = 0;
                decimal Netto = 0;
                decimal stdrKA = decimal.Parse(txtStdKA.Text.Replace(".", ","));
                decimal hasilKA = decimal.Parse(txtKadarAir.Text.Replace(".", ","));
                decimal Gross = decimal.Parse(txtGross.Text.Replace(".", ","));
                SelisihKA = stdrKA - hasilKA;
                Netto = (Rounded == true) ? Math.Round((Gross + (Gross * SelisihKA / 100)), 0) : (Gross + (Gross * SelisihKA / 100));
                txtNetto.Text = Netto.ToString();
                txtQty.Text = Netto.ToString();
            }
        }*/
        protected void btnBatal_Click(object sender, EventArgs e)
        {
            txtKadarAir.Text = string.Empty;
            txtGross.Text = string.Empty;
            txtNetto.Text = string.Empty;
            Panel1.Attributes.Add("style", "display:none");
            // bgPanel.Attributes.Add("style", "display:none");
        }
        /** proses kadar air dialihkan ke form dari popup
         * added on 03-11-2016
         */
        protected void txtKadarAir_Change(object sender, EventArgs e)
        {
            decimal actGros = 0;
            decimal.TryParse(txtGrossPlant.Text.Replace(".", ","), out actGros);

            if (actGros > 0)
            {
                Round = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Rounded", "PO");
                Rounded = (Round == "yes") ? true : false;
                decimal actKA = 0; decimal actSampah = 0; decimal actStdKA = 0;
                decimal SelisihKA = 0;
                decimal Netto = 0;
                decimal.TryParse(txtStdKAPlant.Text.Replace(".", ","), out actStdKA);
                decimal.TryParse(txtKadarAirPlant.Text.Replace(".", ","), out actKA);
                decimal.TryParse(txtSampah.Text.Replace(".", ","), out actSampah);
                decimal stdrKA = actStdKA;// Convert.ToDecimal(txtStdKA.Text.Replace(".", ","));
                decimal hasilKA = actKA;// Convert.ToDecimal(txtKadarAir.Text.Replace(".", ","));
                decimal Gross = actGros;// Convert.ToDecimal(txtGross.Text.Replace(".", ","));
                decimal Sampah = actSampah;// decimal.Parse(txtSampah.Text.Replace(".", ","));
                SelisihKA = stdrKA - hasilKA;
                SelisihKA = SelisihKA - Sampah;
                decimal SelisihG = Math.Round((Gross * SelisihKA / 100), 0, MidpointRounding.AwayFromZero);
                Netto = Math.Round((Gross + SelisihG), 0, MidpointRounding.AwayFromZero);
                txtNettoPlant.Text = Netto.ToString();
                txtSelisih.Text = SelisihG.ToString();
                txtQty.Text = Netto.ToString();
            }
        }
        protected void txtGross_Change(object sender, EventArgs e)
        {

        }

        protected void txtGrossPlant_Change(object sender, EventArgs e)
        {
            decimal actKA = 0;
            decimal.TryParse(txtKadarAirPlant.Text.Replace(".", ","), out actKA);
            if (actKA > 0)
            {
                Round = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Rounded", "PO");
                Rounded = (Round == "yes") ? true : false;
                decimal actSampah = 0; decimal actStdKA = 0; decimal actGros = 0;
                decimal SelisihKA = 0;
                decimal Netto = 0;
                decimal.TryParse(txtStdKAPlant.Text.Replace(".", ","), out actStdKA);
                decimal.TryParse(txtGrossPlant.Text.Replace(".", ","), out actGros);
                decimal.TryParse(txtSampah.Text.Replace(".", ","), out actSampah);
                decimal stdrKA = actStdKA;// Convert.ToDecimal(txtStdKA.Text.Replace(".", ","));
                decimal hasilKA = actKA;// Convert.ToDecimal(txtKadarAir.Text.Replace(".", ","));
                decimal Gross = actGros;// Convert.ToDecimal(txtGross.Text.Replace(".", ","));
                decimal Sampah = actSampah;// decimal.Parse(txtSampah.Text.Replace(".", ","));
                SelisihKA = stdrKA - hasilKA;
                SelisihKA = SelisihKA - Sampah;
                decimal SelisihG = Math.Round((Gross * SelisihKA / 100), 0, MidpointRounding.AwayFromZero);
                Netto = Math.Round((Gross + SelisihG), 0, MidpointRounding.AwayFromZero);
                txtNettoPlant.Text = Netto.ToString();
                txtSelisih.Text = SelisihG.ToString();
                txtQty.Text = Netto.ToString();
            }
            else
            {
                txtNettoPlant.Text = txtGrossPlant.Text;
            }
        }
        protected void txtNetto_Change(object sender, EventArgs e)
        {
        }
        protected void txtSampah_Change(object sender, EventArgs e)
        {

            decimal actKA = 0; decimal actSampah = 0; decimal actStdKA = 0; decimal actGros = 0;
            decimal.TryParse(txtSampah.Text.Replace(".", ","), out actSampah);
            if (actSampah > 0)
            {
                Round = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Rounded", "PO");
                Rounded = (Round == "yes") ? true : false;
                decimal SelisihKA = 0;
                decimal Netto = 0;
                decimal.TryParse(txtStdKAPlant.Text.Replace(".", ","), out actStdKA);
                decimal.TryParse(txtKadarAirPlant.Text.Replace(".", ","), out actKA);
                decimal.TryParse(txtGrossPlant.Text.Replace(".", ","), out actGros);
                decimal stdrKA = actStdKA;// Convert.ToDecimal(txtStdKA.Text.Replace(".", ","));
                decimal hasilKA = actKA;// Convert.ToDecimal(txtKadarAir.Text.Replace(".", ","));
                decimal Gross = actGros;// Convert.ToDecimal(txtGross.Text.Replace(".", ","));
                decimal Sampah = actSampah;// decimal.Parse(txtSampah.Text.Replace(".", ","));
                SelisihKA = stdrKA - hasilKA;
                SelisihKA = SelisihKA - Sampah;
                decimal SelisihG = Math.Round((Gross * SelisihKA / 100), 0, MidpointRounding.AwayFromZero);
                Netto = Math.Round((Gross + SelisihG), 0, MidpointRounding.AwayFromZero);
                txtNettoPlant.Text = Netto.ToString();
                txtSelisih.Text = SelisihG.ToString();
                txtQty.Text = Netto.ToString();
            }
        }
        protected void txtNoPOL_Change(object sender, EventArgs e)
        {
            //get data from inputan kadar air QA
            if (ddlSJDepo.SelectedIndex == 0)
            {
                DepoKertasKA dks = new DepoKertasKA();
                ArrayList arrQA = new ArrayList();
                string Criteria = " AND NOPOL='" + txtNoPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper() +
                                  "' AND ItemCode='" + txtItemCode.Text + "' AND RowStatus>-1 AND Approval>1 ";
                Criteria += " AND (ISNULL(POKAID,0)=0 OR POKAID='') AND PlantID=" + ((Users)Session["Users"]).UnitKerjaID;
                Criteria += " ORDER BY ID";
                arrQA = dks.Retrieve(Criteria);
                foreach (QAKadarAir q in arrQA)
                {
                    txtGrossPlant.Text = q.GrossPlant.ToString();
                    txtKadarAirPlant.Text = q.AvgKA.ToString();
                    txtNettoPlant.Text = q.NettPlant.ToString();
                    txtSampah.Text = q.Sampah.ToString();
                    txtQty.Text = q.NettPlant.ToString();
                    txtSelisih.Text = q.Potongan.ToString();
                }
                LoadDataKA(txtNoPOL.Text.Replace(" ", "").Replace("_", "").Trim().ToUpper());
            }
        }
        protected void LoadSuratJalanDepo(string SupplierID)
        {
            DeliveryKertas dk = new DeliveryKertas();
            ArrayList arrData = new ArrayList();
            DepoKertas dpk = new DepoKertas();
            Users user = ((Users)Session["Users"]);
            string Criteria = " AND PlantID=" + user.UnitKerjaID;
            Criteria += " AND SupplierID=" + txtSuppID.Value;
            Criteria += " AND (POKAID IS NULL OR POKAID='' )";
            Criteria += " AND (ItemCode='" + txtItemCode.Text + "' OR ItemCode IS NULL)";
            ddlSJDepo.Items.Clear();
            ddlSJDepo.Items.Add(new ListItem("--Pilih No SJ--", "0"));
            SuppPurchFacade spf = new SuppPurchFacade();
            int sp = spf.SubCompanyID(int.Parse(SupplierID));
            int fDK = spf.SubCompanyID(int.Parse(SupplierID), true);
            if (sp == 5 || fDK == 5)
            {
                arrData = dpk.Retrieve(Criteria);
                foreach (DeliveryKertas dks in arrData)
                {
                    ddlSJDepo.Items.Add(new ListItem(dks.NoSJ.ToString(), dks.ID.ToString()));
                }
                if (arrData.Count > 0)
                {
                    ddlSJDepo.SelectedIndex = 1;
                    LoadDataKadarAir(ddlSJDepo.SelectedValue);
                }
                else
                {
                    txtGrossPlant.Text = "0";
                    txtKadarAirPlant.Text = "0";
                    txtNettoPlant.Text = "0";
                    txtNoPOL.Text = "";
                    txtSampah.Text = "0";
                }
            }
            else
            {

            }
        }
        protected void ddlSJDepo_Click(object sender, EventArgs e)
        {
            if (ddlSJDepo.SelectedIndex > 0)
            {
                if (ddlSJDepo.SelectedItem.Text.Substring(0, 2) == "KR" ||
                    ddlSJDepo.SelectedItem.Text.Substring(0, 2) == "CT")
                {
                    LoadDataKAD(ddlSJDepo.SelectedValue.ToString());
                }
                else
                {
                    LoadDataKadarAir(ddlSJDepo.SelectedValue);
                }
            }
            else
            {
                txtGrossPlant.Text = "0";
                txtKadarAirPlant.Text = "0";
                txtNettoPlant.Text = "0";
                txtNoPOL.Text = "";
                txtSampah.Text = "0";
            }
        }
        private void LoadDataKA(string Nopol)
        {
            if (Nopol == string.Empty) { return; }
            DepoKertasKA dp = new DepoKertasKA();
            ArrayList dk = new ArrayList();
            string Criteria = " AND NOPOL='" + Nopol + "' and (POKAID is NULL OR POKAID !=0)";
            Criteria += " AND Approval>1 ";
            dk = dp.Retrieve(Criteria);
            ddlSJDepo.Items.Clear();
            ddlSJDepo.Items.Add(new ListItem("--Pilih Doc KA--", "0"));
            foreach (QAKadarAir q in dk)
            {
                ddlSJDepo.Items.Add(new ListItem(q.DocNo + "- BK : " + q.GrossPlant.ToString(), "7" + (q.ID.ToString())));
            }
            LoadDataKA(Nopol, txtGrossPlant.Text);
        }
        private void LoadDataKA(string Nopol, string Gross)
        {
            try
            {
                DepoKertasKA dp = new DepoKertasKA();
                QAKadarAir dk = new QAKadarAir();
                string Criteria = " AND Nopol='" + Nopol + "' and (POKAID is NULL OR POKAID !=0) AND GrossPlant=" + Gross;
                Criteria += " AND Approval>1 ";
                dk = dp.Retrieve(Criteria, true);
                ddlSJDepo.SelectedValue = dk.ID.ToString();
            }
            catch { }
        }
        private void LoadDataKAD(string ID)
        {
            DepoKertasKA dp = new DepoKertasKA();
            QAKadarAir dk = new QAKadarAir();
            string Criteria = " AND ID=" + ID.Substring(1, ID.Length - 1);
            Criteria += " AND Approval >1 ";
            dk = dp.Retrieve(Criteria, true);
            string[] nopol = dk.NOPOL.ToString().Split('-');
            string pol = "";
            if (nopol.Count() > 1)
            {
                string awal = (nopol[0].Length == 1) ? nopol[0].ToString() + " " : nopol[0].ToString();
                string tengah = (nopol[1].Length == 4) ? nopol[1].ToString() : nopol[1].PadRight(4, ' ').ToString();
                pol = awal + "-" + tengah + "-" + nopol[2].ToString();
            }
            txtGrossPlant.Text = dk.GrossPlant.ToString();
            txtNettoPlant.Text = dk.NettPlant.ToString();
            txtKadarAirPlant.Text = dk.AvgKA.ToString();
            txtSampah.Text = dk.Sampah.ToString();
            txtNoPOL.Text = pol;
            txtQty.Text = dk.NettPlant.ToString();
        }
        private void LoadDataKadarAir(string DocID)
        {

            DepoKertas dp = new DepoKertas();
            DeliveryKertas dk = new DeliveryKertas();
            string Criteria = " AND ID=" + DocID;
            //Criteria += " AND Approval>1 ";
            dk = dp.Retrieve(Criteria, true);
            string[] nopol = dk.NOPOL.ToString().Split('-');
            string pol = "";
            if (nopol.Count() > 1)
            {
                string awal = (nopol[0].Length == 1) ? nopol[0].ToString() + " " : nopol[0].ToString();
                string tengah = (nopol[1].Length == 4) ? nopol[1].ToString() : nopol[1].PadRight(4, ' ').ToString();
                pol = awal + "-" + tengah + "-" + nopol[2].ToString();
            }
            txtGrossPlant.Text = dk.GrossDepo.ToString();
            txtKadarAirPlant.Text = dk.KADepo.ToString();
            txtNettoPlant.Text = dk.NettDepo.ToString();
            txtNoPOL.Text = pol;// dk.NOPOL.ToString();
            txtSampah.Text = "0";
            txtQty.Text = dk.NettDepo.ToString();
        }
        protected void ddlDelivery_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDelivery.Text = ddlDelivery.SelectedItem.Text;
        }
    }
}

public class RevisiPOne
{
    private ArrayList arrData = new ArrayList();
    private Revisi hlp = new Revisi();
    private List<SqlParameter> sqlListParam;
    public string Criteria { get; set; }
    public string TableName { get; set; }
    public string Pilihan { get; set; }
    public string Where { get; set; }
    public int ProcessData(object help, string spName)
    {
        try
        {
            hlp = (Revisi)help;
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
        hlp = (Revisi)help;
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
        string strsql = "select DATA_TYPE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION,NUMERIC_SCALE from INFORMATION_SCHEMA.COLUMNS IC where " +
                        "TABLE_NAME = '" + TableName + "' and COLUMN_NAME = '" + ColumName + "'";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strsql);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                result = sdr["DATA_TYPE"].ToString() + " ";
                result += (sdr["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value) ? "(" + sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() + ")" : "";
                result += (sdr["DATA_TYPE"].ToString() == "decimal") ? "(" + sdr["NUMERIC_PRECISION"].ToString() + "," + sdr["NUMERIC_SCALE"] + ")" : "";
                if (sdr["CHARACTER_MAXIMUM_LENGTH"].ToString() == "-1")
                {
                    result = result.Replace("-1", "Max");
                }
            }
        }
        return result;
    }
    public string GetReasonRevision()
    {
        string result = string.Empty;
        string strSQL = "Select AlasanRevisi,RevisiBy,RevisiTime,RevisiKe from POPurchnRevisi " + this.Where;
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                result += "Revisi Ke : " + sdr["RevisiKe"].ToString() +
                         "\nAlasan Revisi : " + sdr["AlasanRevisi"].ToString() +
                         "\nRevisi By : " + sdr["RevisiBy"].ToString() +
                         "\nRevisi Date :" + DateTime.Parse(sdr["RevisiTime"].ToString()).ToString() +
                         "\n";
            }
        }
        return result;
    }
    public string GetReasonRevision(bool LastRevisi)
    {
        string result = string.Empty;
        string strSQL = "Select top 1 AlasanRevisi,RevisiBy,RevisiTime,RevisiKe from POPurchnRevisi " + this.Where + " Order By ID Desc";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                result += "Revisi Ke : " + sdr["RevisiKe"].ToString();
            }
        }
        return result;
    }
    public int JumlahKirimanDepo(string NOSJ, string ItemCode)
    {
        int result = 0;
        string strSQL = "SELECT COUNT(ID) JmlData FROM DeliveryKertas Where NoSJ='" + NOSJ + "' AND ItemCode='" + ItemCode + "' AND RowStatus>-1 AND ISNULL(POKAID,0)<1 ";
        DataAccess da = new DataAccess(Global.ConnectionString());
        SqlDataReader sdr = da.RetrieveDataByString(strSQL);
        if (da.Error == string.Empty && sdr.HasRows)
        {
            while (sdr.Read())
            {
                result = int.Parse(sdr["JmlData"].ToString());
            }
        }
        return result;
    }

}
public class Revisi : POPurchn
{
    //public int ID {get;set;}
    //public string NoPO{get;set;}
    //public int POID{get;set;}
    public int PODetailID { get; set; }
    public int RevisiKe { get; set; }
    public string AlasanRevisi { get; set; }
    public string RevisiBy { get; set; }
    public DateTime RevisiTime { get; set; }
    //public int Approval { get; set; }
}