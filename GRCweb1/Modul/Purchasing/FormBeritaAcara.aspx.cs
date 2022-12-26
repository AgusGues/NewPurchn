using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using BusinessFacade;
using DataAccessLayer;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    public partial class FormBeritaAcara : System.Web.UI.Page
    {
        public decimal TimbanganSup = 0;
        public decimal TimbanganBpas = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadJenisBarang();
                txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                LoadDept();
                string OnlyKertas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyKertas", "BeritaAcara");
                kertas1.Visible = (OnlyKertas == "1") ? false : true;
                txtSubject.Text = "Penyesuaian Stock";
                LoadItemBarang(OnlyKertas);
                txtSubject.Focus();
                Session["ListBA"] = null;
                btnAddItem.Enabled = false;
                btnSimpan.Enabled = false;
                LoadDeptKertas();
                //btnPrint.Enabled = (txtBANum.Text == string.Empty) ? false : true;

                if (Request.QueryString["ba"] != null)
                {
                    LoadBA(Request.QueryString["ba"].ToString());
                    btnPrint.Attributes.Add("onclick", "return _print_prev('" + Request.QueryString["ba"].ToString() + "')");
                }
                else
                {
                    btnPrint.Enabled = false;
                }
            }
            else
            {

            }
        }

        protected void btnCari_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormBeritaAcara.aspx?ba=" + txtCari.Text);
        }

        private void LoadJenisBarang()
        {
            string[] JB = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("JenisBarang", "BeritaAcara").Split(',');
            chkJenisBarang.Items.Clear();
            chkJenisBarang.Font.Size = FontUnit.XSmall;// FontSize.XSmall;
            chkJenisBarang.Attributes.Add("style", "cursor:pointer");
            for (int i = 0; i < JB.Count(); i++)
            {
                chkJenisBarang.Items.Add(new ListItem(JB[i], JB[i]));
            }
            chkJenisBarang.SelectedIndex = 0;
        }

        private void LoadItemBarang(string Kertas)
        {
            //string NamaBarang = (Kertas == "1") ? "Kertas Kantong Semen" : txtCariNamaBarang.Text;
            //string ItemCode = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("MaterialKertas", "BeritaAcara");
            //ArrayList arrInventory = new ArrayList();
            //InventoryFacade inventoryFacade = new InventoryFacade();
            //inventoryFacade.Criteriane = " and ItemCode in('" + ItemCode.Replace(",", "','") + "')";
            //arrInventory = (ItemCode == string.Empty) ? inventoryFacade.RetrieveByCriteria("A.ItemName", NamaBarang) :
            //              inventoryFacade.Retrieve();
            //ddlNamaBarang.Items.Clear();
            //ddlNamaBarang.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            //foreach (Inventory Inv in arrInventory)
            //{
            //    ddlNamaBarang.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));
            //}
            ArrayList arrInventory = new ArrayList();
            InventoryFacade inventoryFacade = new InventoryFacade();
            inventoryFacade.Criteriane = " and ItemCode in(select itemcode from masterinputkertas where rowstatus>-1)";
            //arrInventory = (ItemCode == string.Empty) ? inventoryFacade.RetrieveByCriteria("A.ItemName", NamaBarang) :
            arrInventory = inventoryFacade.Retrieve();
            ddlNamaBarang.Items.Clear();
            ddlNamaBarang.Items.Add(new ListItem("-- Pilih Barang --", "0"));
            foreach (Inventory Inv in arrInventory)
            {
                ddlNamaBarang.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));
            }
            ddlNamaBarang.SelectedIndex = 1;
            string[] itmCode = ddlNamaBarang.SelectedItem.Text.Split('(');
            LoadRMSNo(ddlNamaBarang.SelectedValue, itmCode[1].ToString().Replace(")", ""));
        }
        private void LoadRMSNo(string ItemID, string ItemCode)
        {
            string StartDate = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StartDocument", "BeritaAcara");
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select ID,ReceiptNo From Receipt where Convert(Char,ReceiptDate,112)>='" + StartDate + "' and Status>-1";
            zl.CustomQuery += " and ID in(Select ReceiptID from ReceiptDetail where RowStatus>-1 and ItemID=" + ItemID + ")";
            zl.CustomQuery += " and (BANumber is null or BANumber='')";
            zl.CustomQuery += (ItemCode == "011102037020000" || ItemCode == "011102041060000") ? "and (SupplierID in(Select ID from SuppPurch where " +
            //"(SupplierName like '%berkah jaya%' or SupplierName like '%aneka plastik%') and " +
            "(SubCompanyID in (5,6,7)) or SupplierID in (select SupplierID from BeritaAcaraSupplier_ex where Rowstatus > -1) /*Or forDK in (5,6)*/ or SupplierName like '%papan dinamika%'))" : "";
            zl.CustomQuery += " order by receiptno desc";
            SqlDataReader sdr = zl.Retrieve();
            ddlNoRMS.Items.Clear();
            ddlNoRMS.Items.Add(new ListItem("", "0"));
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        ddlNoRMS.Items.Add(new ListItem(sdr["ReceiptNo"].ToString(), sdr["ID"].ToString()));
                    }
                }
            }
        }
        protected void ddlNoRMS_Change(object sender, EventArgs e)
        {
            txtNoRMS.Text = ddlNoRMS.SelectedValue.ToString();
            txtNoRMS_Change(null, null);
        }

        private void LoadDept()
        {
            ddlfromDept.Items.Clear();
            ddlToDept.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            ddlToDept.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                if (((Users)Session["Users"]).DeptID == dept.ID)
                {
                    ddlfromDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                }
                else
                {
                    ddlToDept.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                }
            }
            ddlToDept.SelectedValue = "24";//default value
        }
        protected void ddlNamaBarang_Change(object sender, EventArgs e)
        {
            if (ddlNamaBarang.SelectedIndex > 0)
            {
                string[] itmCode = ddlNamaBarang.SelectedItem.Text.Split('(');
                LoadRMSNo(ddlNamaBarang.SelectedValue, itmCode[1].ToString().Replace(")", ""));
            }
        }
        protected void txtNoRMS_Change(object sender, EventArgs e)
        {
            //Tambahan Cek Supplier INDOCEMENT
            Receipt rms0 = new Receipt();
            ReceiptDetailFacade rcp0 = new ReceiptDetailFacade();

            Receipt rms01 = new Receipt();
            ReceiptDetailFacade rcp01 = new ReceiptDetailFacade();
            if (ddlDepo.SelectedItem.ToString() == "TEAM KHUSUS")
            {
                int SupplierID01s = rcp01.RetrieveSupplierName2(txtNoRMS.Text);
                Session["SupplierID"] = SupplierID01s;
                Session["flag"] = 0;
            }
            else
            {
                int SupplierIDs = rcp0.RetrieveSupplierName(txtNoRMS.Text);
                Session["SupplierID"] = SupplierIDs;
                Session["flag"] = 1;
            }
            // End Tambahan
            int SupplierID = Convert.ToInt32(Session["SupplierID"]);

            Receipt rms = new Receipt();
            ReceiptDetailFacade rcp = new ReceiptDetailFacade();
            rcp.Where = " and rcd.ReceiptID='" + txtNoRMS.Text + "'";

            if (ddlDepo.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Silahkan pilih Depo terlebih dahulu !!!"); return;
            }

            // Tambahan 
            if (SupplierID > 0 || (ddlDepo.SelectedItem.ToString() == "Lain-Lain" && SupplierID == 0) || (ddlDepo.SelectedItem.ToString() == "TEAM KHUSUS" && SupplierID == 0))
            {
                rcp.Where1 = " DeliveryKertasKA ";
                rcp.Where2 = " '0'DepoID,'-'DepoName ";
                rcp.Where3 = " ";
            }
            else
            {
                rcp.Where1 = " DeliveryKertas ";
                rcp.Where2 = " dk.DepoID,dk.DepoName ";
                rcp.Where3 = " AND ka.Netto=rms.QtyPO ";
            }
            // Tambahan

            rms = rcp.GetReceiptDetail();
            txtDepo.Text = rms.SupplierName;
            txtDepoID.Value = rms.SupplierID.ToString();
            txtReceiptDate.Text = rms.ReceiptDate.ToString("dd-MMM-yyyy");
            txtQtySup.Text = rms.Gross.ToString();
            txtKadarAir.Text = rms.KursPajak.ToString();
            txtSJNo.Text = rms.Keterangan1;
            txtQtyRms.Text = rms.Quantity.ToString();
            txtNetto.Text = rms.Netto.ToString();
            txtJmlBal.Text = rms.JmlBal.ToString();
            btnAddItem.Enabled = true;
            txtStdKA.Value = rms.StdKadarAir.ToString();

            if (rms.DepoID > 0)
            {
                ddlDepo.SelectedValue = this.LoadDeptKertas(rms.DepoID).ToString();
            }


            //txtKABPAS.Text = rms.StdKadarAir.ToString();
            //check kadar air depo
            //decimal kaDepo = rcp.RetrieveKADepo(rms);

            //txtKadarAir.Text = rms.KadarAir.ToString("N4"); 
        }
        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            if (ddlDepo.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "alert", "alert('Depo belum di pilih')", true);
                return;
            }

            ArrayList arrData = (Session["ListBA"] == null) ? new ArrayList() : (ArrayList)Session["ListBA"];
            Receipt rcp = new Receipt();
            rcp.ReceiptNo = txtNoRMS.Text.ToUpper();
            rcp.ReceiptDate = DateTime.Parse(txtReceiptDate.Text);
            rcp.ItemID = int.Parse(ddlNamaBarang.SelectedValue);
            rcp.SupplierName = txtDepo.Text;
            rcp.SupplierID = int.Parse(txtDepoID.Value);
            rcp.Gross = decimal.Parse(txtQtySup.Text);
            rcp.Netto = decimal.Parse(txtNetto.Text);
            rcp.Quantity = decimal.Parse(txtQtyRms.Text);
            rcp.KadarAir = decimal.Parse(txtKadarAir.Text);
            rcp.JmlBal = decimal.Parse(txtJmlBal.Text);
            rcp.NoSJ = txtSJNo.Text;
            rcp.ID = int.Parse(ddlNoRMS.SelectedValue.ToString());
            arrData.Add(rcp);
            lstBA.DataSource = arrData;
            lstBA.DataBind();
            if (arrData.Count > 0)
            {
                Session["ListBA"] = arrData;
            }
            ClearReceipt();
            //btnSimpan.Enabled = true;
        }

        private void ClearReceipt()
        {
            foreach (Control ct in kertas.Controls)
            {
                if (ct is TextBox)
                {
                    TextBox tx = (TextBox)ct;
                    tx.Text = string.Empty;
                    btnAddItem.Enabled = false;
                }
                else
                {
                    foreach (Control ctl in ct.Controls)
                    {
                        if (ctl is TextBox)
                        {
                            TextBox tt = (TextBox)ctl;
                            tt.Text = string.Empty;
                            btnAddItem.Enabled = false;
                        }
                        else
                        {
                            foreach (Control xx in ctl.Controls)
                            {
                                if (ctl is TextBox)
                                {
                                    TextBox ttx = (TextBox)xx;
                                    ttx.Text = string.Empty;
                                    btnAddItem.Enabled = false;
                                }
                            }
                        }
                    }
                }
            }
            txtSampah.Text = "0";
            txtKABPAS.Text = "0";
            txtQtyBPAS.Text = "0";
            txtJmlBal.Text = "0";
        }
        protected void txtSubject_Change(object sender, EventArgs e)
        {
            if (txtSubject.Text == string.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "alert('Subject tidak boleh kosong')", true);
                txtSubject.Focus();
            }
        }
        protected void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["ListBA"] == null) { return; }
                ArrayList arrBaHead = new ArrayList();
                BeritaAcara ba = new BeritaAcara();
                decimal sampah = 0;
                decimal.TryParse(txtSampah.Text, out sampah);
                ba.BANum = txtBANum.Text;
                ba.FromDept = int.Parse(ddlfromDept.SelectedValue);
                ba.ToDept = int.Parse(ddlToDept.SelectedValue);
                ba.BADate = DateTime.Parse(txtTanggal.Text);
                ba.JenisBarang = chkJenisBarang.SelectedValue.ToString();
                ba.TotalSup = decimal.Parse(txtTotQtySup.Text);
                ba.TotalBPAS = decimal.Parse(txtQtyBPAS.Text);
                ba.Netto = decimal.Parse(txtNetBPAS.Text);
                ba.KadarAirBPAS = decimal.Parse(txtKABPAS.Text);
                ba.JmlBalBPAS = decimal.Parse(txtTotalBal.Text);
                ba.BAAttn = txtAttn.Text;
                ba.ItemID = int.Parse(ddlNamaBarang.SelectedValue);
                ba.Selisih = decimal.Parse(txtSelisih.Text);
                ba.ProsSelisih = decimal.Parse(txtProsKA.Text);
                ba.Subject = txtSubject.Text;
                ba.CreatedBy = ((Users)Session["Users"]).UserName;
                ba.DepoKertasID = int.Parse(ddlDepo.SelectedValue.ToString());
                ba.Sampah = sampah;
                arrBaHead.Add(ba);
                ArrayList arrReceipt = (ArrayList)Session["ListBA"];
                SimpanBA(arrBaHead, arrReceipt);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "alert", "alert(' error on: " + ex.StackTrace.Substring(ex.StackTrace.Length - 50) + "')", true);
                //errMsg.InnerHtml = ex.StackTrace[0].ToString();
                return;
            }

        }
        protected void btnList_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormBAApproval.aspx?tp=0");
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            foreach (Control ctl in ctn.Controls)
            {
                if (ctl is TextBox)
                {
                    TextBox txt = (TextBox)ctl;
                    txt.Text = string.Empty;
                }
                else if (ctl is DropDownList)
                {
                    DropDownList ddl = (DropDownList)ctl;
                    // ddl.SelectedIndex = 0;
                }
                else
                {
                    foreach (Control ct in ctl.Controls)
                    {
                        if (ct is TextBox)
                        {
                            TextBox txt = (TextBox)ct;
                            txt.Text = string.Empty;
                        }
                    }
                }
            }
            ClearReceipt();
            txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            Session["ListBA"] = null;
            ArrayList arrData = new ArrayList();
            lstBA.DataSource = arrData;
            lstBA.DataBind();
        }
        private decimal totAa = 0; private decimal totCa = 0;

        protected void lstBA_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Receipt rcp = (Receipt)e.Item.DataItem;

            // Tambahan 2018
            int SupplierID_ex = Convert.ToInt32(Session["SupplierID"]);
            if (rcp.SupplierID == SupplierID_ex)
            {
                TimbanganSup += rcp.Quantity;
            }
            else
            {
                TimbanganSup += rcp.Netto;
            }

            //TimbanganSup += rcp.Netto;
            txtTotQtySup.Text = TimbanganSup.ToString();
            totAa += rcp.Gross;
            totCa += rcp.JmlBal;
            totB.Text = TimbanganSup.ToString("N2");
            totA.Text = totAa.ToString("N2");
            totC.Text = totCa.ToString("N2");
            //load data KA plant
            txtStdKA.Value = string.Empty;
            string[] noSJ = rcp.NoSJ.Split(',');
            if (noSJ.Count() > 1)
            {
                DepoKertasKA dk = new DepoKertasKA();
                ArrayList arrData = new ArrayList();
                QAKadarAir qa = new QAKadarAir();
                string Criteria = " AND PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString() +
                //" AND NoSJ='" + noSJ[1].ToString().Trim() + "' AND NOPOL='" + noSJ[0].TrimEnd().Replace(" ", "-") + "'";
                " AND NoSJ='" + noSJ[1].ToString().Trim() + "' AND NOPOL='" + noSJ[0].TrimEnd().TrimStart() + "'";
                qa = dk.Retrieve(Criteria, true);
                txtQtyBPAS.Text = qa.GrossPlant.ToString("N0");
                txtKABPAS.Text = qa.AvgKA.ToString("N2");
                txtNetBPAS.Text = qa.NettPlant.ToString("N0");
                txtSampah.Text = qa.Sampah.ToString("N2");
                txtSelisih.Text = qa.Potongan.ToString("N0");
                txtTotalBal.Text = qa.JmlBAL.ToString();
                txtStdKA.Value = Convert.ToInt32(qa.StdKA).ToString();
            }
        }
        protected void txtQtyBPAS_Change(object sender, EventArgs e)
        {
            Users user = (Users)Session["Users"];
            string JenisKertas = ddlDepo.SelectedItem.ToString().Trim();
            try
            {
                string tanda = Session["flag"].ToString();
                decimal totalsup = 0; decimal selisih = 0;
                decimal.TryParse(txtTotQtySup.Text, out totalsup);

                string Selisih0 = (this.NettBPAS0() - totalsup).ToString();
                //txtProsKA.Text = (Math.Round((Convert.ToDecimal(Selisih0) / totalsup * 100), 2)).ToString();
                Decimal test = this.NettBPAS0();
                //txtNetBPAS.Text = Math.Round((this.NettBPAS0() * Convert.ToDecimal(0.8)),0).ToString();
                if (user.UnitKerjaID == 1)
                {
                    //txtNetBPAS.Text = Math.Round((this.NettBPAS0() * Convert.ToDecimal(0.8)), 0).ToString();
                    txtNetBPAS.Text = (JenisKertas == "IMPORT") ? Math.Round((this.NettBPAS0()), 0).ToString() : Math.Round((this.NettBPAS0()/** * Convert.ToDecimal(0.8)**/), 0).ToString();
                    txtSelisih.Text = (this.NettBPAS0() - totalsup).ToString();
                }
                else
                {
                     test = this.NettBPAS0();
                    txtNetBPAS.Text = (JenisKertas == "IMPORT") ? Math.Round((this.NettBPAS0() * Convert.ToDecimal(0.9)), 0).ToString() : Math.Round((this.NettBPAS0()/*** Convert.ToDecimal(0.8)**/), 0).ToString();
                    //txtSelisih.Text = (JenisKertas == "IMPORT") ? (this.NettBPAS() - totalsup).ToString() : (this.NettBPAS() - totalsup).ToString(); 
                    string strsb = ddlNamaBarang.SelectedItem.Text.Trim();
                    if (ddlNamaBarang.SelectedItem.Text.Trim() == "UNCOATED KRAFT PAPER  (016739001000000)")
                    { 
                        txtNetBPAS.Text = Math.Round((this.NettBPAS0() - (this.NettBPAS0() * Convert.ToDecimal(txtKABPAS.Text) / 100)), 0).ToString();
                        txtSelisih.Text = (decimal.Parse(txtNetBPAS.Text)- totalsup).ToString();
                    }
                    else    
                    txtSelisih.Text = (this.NettBPAS() - totalsup).ToString();
                }

                txtProsKA.Text = (Math.Round((Convert.ToDecimal(txtSelisih.Text) / totalsup * 100), 2)).ToString();
                //txtSelisih.Text = (this.NettBPAS() - totalsup).ToString(); 


                //if (tanda == "1")
                //{
                //    string Selisih0 = (this.NettBPAS0() - totalsup).ToString();
                //    txtProsKA.Text = (Math.Round((Convert.ToDecimal(Selisih0) / totalsup * 100), 2)).ToString();

                //    txtNetBPAS.Text = (this.NettBPAS0() * Convert.ToDecimal(0.8)).ToString();
                //    txtSelisih.Text = (this.NettBPAS() - totalsup).ToString(); 

                //}
                //else
                //{
                //    string Selisih = (this.NettBPAS() - totalsup).ToString();
                //    txtProsKA.Text = (Math.Round((Convert.ToDecimal(Selisih) / totalsup * 100), 2)).ToString();

                //    txtNetBPAS.Text = this.NettBPAS().ToString();
                //    txtSelisih.Text = (this.NettBPAS() - totalsup).ToString(); 

                //}

                //txtNetBPAS.Text = this.NettBPAS().ToString();
                //txtSelisih.Text = (this.NettBPAS() - totalsup).ToString();            

                //decimal.TryParse(txtSelisih.Text, out selisih);
                //txtProsKA.Text = (Math.Round((selisih / totalsup * 100), 2)).ToString();
                btnSimpan.Enabled = (txtKABPAS.Text == string.Empty) ? false : true;
            }
            catch { }
        }
        protected void txtKABPAS_Change(object sender, EventArgs e)
        {
            try
            {
                decimal totalsup = 0; decimal selisih = 0; decimal sampah = 0;
                decimal.TryParse(txtTotQtySup.Text, out totalsup);
                decimal.TryParse(txtSampah.Text, out sampah);
                txtNetBPAS.Text = NettBPAS().ToString();
                btnSimpan.Enabled = (txtKABPAS.Text == string.Empty) ? false : true;
                //txtSelisih.Text = (decimal.Parse(txtTotQtySup.Text) - this.NettBPAS()).ToString();
                txtSelisih.Text = (this.NettBPAS() - totalsup).ToString();
                decimal.TryParse(txtSelisih.Text, out selisih);
                txtProsKA.Text = (Math.Round((selisih / totalsup * 100), 2)).ToString();
            }
            catch { }
        }
        protected void txtSampah_Change(object sender, EventArgs e)
        {
            try
            {
                decimal totalsup = 0; decimal selisih = 0;
                decimal.TryParse(txtTotQtySup.Text, out totalsup);
                txtNetBPAS.Text = NettBPAS().ToString();
                btnSimpan.Enabled = (txtKABPAS.Text == string.Empty) ? false : true;
                //txtSelisih.Text = (decimal.Parse(txtTotQtySup.Text) - this.NettBPAS()).ToString();
                txtSelisih.Text = (this.NettBPAS() - totalsup).ToString();
                decimal.TryParse(txtSelisih.Text, out selisih);
                txtProsKA.Text = (Math.Round((selisih / totalsup * 100), 2)).ToString();
            }
            catch { }
        }
        private void SimpanBA(ArrayList HeadBA, ArrayList DetailBA)
        {
            ZetroLib zet = new ZetroLib();
            zet.hlp = new BeritaAcara();
            zet.Criteria = "BANum,FromDept,ToDept,BADate,JenisBarang,TotalSup,TotalBPAS,KadarAirBPAS,Netto,RowStatus,Approval,";
            zet.Criteria += "JmlBalBPAS,BAAttn,ItemID,Selisih,ProsSelisih,Subject,CreatedBy,CreatedTime,DepoKertasID,Sampah";
            zet.TableName = "BeritaAcara";
            zet.Option = "Insert";
            zet.ReturnID = false;
            zet.StoreProcedurName = "spBeritaAcara_Insert";
            string rst = zet.CreateProcedure();
            if (rst == string.Empty)
            {
                foreach (BeritaAcara ba in HeadBA)
                {
                    BeritaAcara bap = new BeritaAcara();
                    bap = ba;
                    bap.RowStatus = 0;
                    bap.Approval = 0;
                    zet.hlp = bap;
                    int res = zet.ProcessData();
                    int Num = this.GetNumberBA(ba.BADate);
                    if (res > 0)
                    {
                        txtBANum.Text = Num.ToString().PadLeft(4, '0') + "/BA/BPAS/" + Global.BulanRomawi(DateTime.Parse(txtTanggal.Text).Month) + "/" + DateTime.Parse(txtTanggal.Text).Year;

                        ZetroLib zb = new ZetroLib();
                        zb.TableName = "BeritaAcaraDetail";
                        zb.Option = "Insert";
                        zb.hlp = new Receipt();
                        zb.Criteria = "ReceiptNo,BAID,SupplierName,SupplierID,Gross,Netto,Quantity,";
                        zb.Criteria += "KadarAir,JmlBal,NoSJ,RowStatus,Approval,CreatedBy,CreatedTime";
                        zb.StoreProcedurName = "spBeritaAcaraDetail_Insert";
                        string resul = zb.CreateProcedure();
                        if (resul == string.Empty)
                        {
                            foreach (Receipt rc in DetailBA)
                            {
                                Receipt rcp = new Receipt();
                                rcp = rc;
                                rcp.BAID = res;
                                rcp.Approval = 0;
                                rcp.RowStatus = 0;
                                rcp.CreatedBy = ((Users)Session["Users"]).UserName;
                                zb.hlp = rcp;
                                zb.ReturnID = false;
                                int result = zb.ProcessData();
                                if (result > 0)
                                {
                                    UpdateReceiptBANUM(txtBANum.Text, rc.ID);
                                }
                                else
                                {
                                    return;
                                }
                            }
                            btnSimpan.Enabled = false;
                            btnPrint.Enabled = true;
                        }
                        UpdateBANUM(txtBANum.Text, res);
                    }
                }
            }
        }
        private int GetNumberBA(DateTime TglBA)
        {
            int result = 1;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "SELECT COUNT(ID) JMl From BeritaAcara Where Year(BADate)=" + TglBA.Year;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr != null)
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = Convert.ToInt32(sdr["JMl"].ToString());
                    }
                }
            }
            return result;
        }
        private decimal NettBPAS()
        {
            string JenisKertas = ddlDepo.SelectedItem.ToString().Trim();
            decimal rst = 0;
            decimal v; decimal qtyBPAS = 0; string stdKA_Now = string.Empty;
            if (txtStdKA.Value == "0" || txtStdKA.Value.Trim() == string.Empty)
            {
                string stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                txtStdKA.Value = stdKA;

                stdKA_Now = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA", "PO");
            }

            if (decimal.TryParse(txtKABPAS.Text, out v))
            {
                decimal aktualKA = 0; decimal.TryParse(txtKABPAS.Text, out aktualKA);
                decimal standarKA = 0; decimal standarKA_Now = Convert.ToDecimal(stdKA_Now);
                decimal.TryParse(txtStdKA.Value, out standarKA);
                decimal sampah = 0; decimal.TryParse(txtSampah.Text, out sampah);
                decimal selisihKA = (JenisKertas == "IMPORT") ? (10 - aktualKA) : (standarKA_Now - aktualKA);
                decimal.TryParse(txtQtyBPAS.Text, out qtyBPAS);
                decimal Gross = (txtQtyBPAS.Text == string.Empty) ? 0 : qtyBPAS;
                decimal selisihGA = Math.Round((Gross * (selisihKA - sampah) / 100), 0, MidpointRounding.AwayFromZero);
                //rst = Math.Round(Gross + selisihGA, 0, MidpointRounding.AwayFromZero);
                //rst = Math.Round(((Gross + selisihGA)*Convert.ToDecimal(0.8)), 0, MidpointRounding.AwayFromZero);
                rst = (JenisKertas == "IMPORT") ? Math.Round(((Gross + selisihGA) * Convert.ToDecimal(0.9)), 0, MidpointRounding.AwayFromZero) : Math.Round(((Gross + selisihGA) * Convert.ToDecimal(0.8)), 0, MidpointRounding.AwayFromZero);
            }
            return rst;
        }

        private decimal NettBPAS0()
        {
            Users user = (Users)Session["Users"]; decimal selisihKA2 = 0;
            string JenisKertas = ddlDepo.SelectedItem.ToString().Trim();
            string tanda = Session["flag"].ToString();
            decimal rst2 = 0;
            decimal v2; decimal qtyBPAS2 = 0; string stdKA = string.Empty; string DefaultstdKA = string.Empty;
            if (txtStdKA.Value == "0" || txtStdKA.Value.Trim() == string.Empty)
            {
                stdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("StdKA2", "PO");
                DefaultstdKA = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("DefaultKadarAir", "PO");
                //txtStdKA.Value = stdKA;            
            }

            if (decimal.TryParse(txtKABPAS.Text, out v2))
            {
                decimal aktualKA2 = 0; decimal.TryParse(txtKABPAS.Text, out aktualKA2);
                decimal standarKA2 = 0;
                decimal.TryParse(stdKA, out standarKA2);
                decimal sampah2 = 0; decimal.TryParse(txtSampah.Text, out sampah2);
                //decimal selisihKA2 = (standarKA2 - aktualKA2);
                if (user.UnitKerjaID == 1)
                {
                    //selisihKA2 = (standarKA2 - aktualKA2);
                    selisihKA2 = (JenisKertas == "IMPORT") ? (Convert.ToDecimal(DefaultstdKA)) - aktualKA2 : (standarKA2 - aktualKA2);
                }
                else
                {
                    selisihKA2 = (JenisKertas == "IMPORT") ? (10 - aktualKA2) : (standarKA2 - aktualKA2);
                    if (ddlNamaBarang.SelectedItem.Text.Trim() == "UNCOATED KRAFT PAPER  (016739001000000)")
                        selisihKA2= 0;
                }
               // selisihKA2 = 0;
                decimal.TryParse(txtQtyBPAS.Text, out qtyBPAS2);
                decimal Gross2 = (txtQtyBPAS.Text == string.Empty) ? 0 : qtyBPAS2;
                decimal selisihGA2 = Math.Round((Gross2 * (selisihKA2 - sampah2) / 100), 0, MidpointRounding.AwayFromZero);
                //rst2 = Math.Round(Gross2 + selisihGA2, 0, MidpointRounding.AwayFromZero);
                rst2 = Math.Round(((Gross2 + selisihGA2)), 0, MidpointRounding.AwayFromZero);
            }
            return rst2;
        }

        private void UpdateBANUM(string BANum, int ID)
        {
            ZetroLib ze = new ZetroLib();
            ze.hlp = new BeritaAcara();
            ze.Option = "Update";
            ze.Criteria = "ID,BANum";
            ze.TableName = "BeritaAcara";
            ze.StoreProcedurName = "spBeritaAcara_BANUM";
            string rs = ze.CreateProcedure();
            if (rs == string.Empty)
            {
                BeritaAcara ba = new BeritaAcara();
                ba.BANum = BANum;
                ba.ID = ID;
                ze.hlp = ba;
                int res = ze.ProcessData();
                if (res > 0)
                {
                    KirimEmail(0, ID.ToString());
                }
            }
        }

        private void UpdateReceiptBANUM(string BANUM, int receiptID)
        {
            ZetroLib ze = new ZetroLib();
            ze.hlp = new BeritaAcara();
            ze.Option = "Update";
            ze.Criteria = "ID,BANumber";
            ze.TableName = "Receipt";
            ze.StoreProcedurName = "spReceipt_BANUM";
            string rs = ze.CreateProcedure();
            if (rs == string.Empty)
            {
                BeritaAcara ba = new BeritaAcara();
                ba.BANumber = BANUM;
                ba.ID = receiptID;
                ze.hlp = ba;
                int res = ze.ProcessData();
            }
        }

        private void LoadDeptKertas()
        {
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DepoKertas where RowStatus>-1";
            SqlDataReader sdr = zl.Retrieve();
            ddlDepo.Items.Clear();
            ddlDepo.Items.Add(new ListItem("--Pilih Depo--", "0"));
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlDepo.Items.Add(new ListItem(sdr["DepoName"].ToString(), sdr["ID"].ToString()));
                }
            }

        }

        private int LoadDeptKertas(int DepoID)
        {
            int result = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "Select * from DepoKertas where RowStatus>-1 AND Alamat=" + DepoID;
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["ID"].ToString());
                }
            }
            return result;
        }

        private void KirimEmail(int NextApproval, string NoBA)
        {
            string[] AprovalList = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Approval", "BeritaAcara").Split(',');
            UsersFacade usf = new UsersFacade();
            Users users = usf.RetrieveById(int.Parse(AprovalList[NextApproval]));
            BeritaAcara ba = this.GetDetailBA(NoBA);
            MailMessage mail = new MailMessage();
            EmailReportFacade msg = new EmailReportFacade();
            string token = "id=" + NoBA + "&UserID=" + users.UserID + "&pwd=" + users.Password.ToString();
            mail.From = new MailAddress("system_support@grcboard.com");
            mail.To.Add(users.UsrMail.ToString());
            //mail.Bcc.Add("noreplay@grcboard.com");
            mail.Subject = "Approval BA Kertas Kantong Semen Depo :" + ba.DepoKertasName.ToString().ToUpper();
            mail.Body = "Mohon untuk di Approved BA No. " + ba.BANum.ToString().ToUpper() + "\n\r";
            mail.Body += "Kertas Kantong Semen Depo : " + ba.DepoKertasName + " \n\r\n\r";
            mail.Body += "Silahkan klik link berikut untuk Approval : \n\r";
            mail.Body += (users.UnitKerjaID == 7) ? "http://krwg.grcboard.com/?link=" ://Modul/Purchasing/FormBAApproval.aspx?token=" + token :
                        "http://ctrp.grcboard.com/?link=";//Modul/Purchasing/FormBAApproval.aspx?token=" + token;
            mail.Body += "\n\r";
            mail.Body += "Terimakasih, " + "\n\r";
            mail.Body += "Salam GRCBOARD " + "\n\r\n\r\n\r";
            mail.Body += "Regard's, " + "\n\r";
            mail.Body += ((Users)Session["Users"]).UserName + "\n\r\n\r\n\r";
            //msg.Body += emailFacade.mailFooter();
            SmtpClient smt = new SmtpClient(msg.mailSmtp());
            smt.Host = msg.mailSmtp();
            smt.Port = msg.mailPort();
            smt.EnableSsl = true;
            smt.DeliveryMethod = SmtpDeliveryMethod.Network;
            smt.UseDefaultCredentials = false;
            smt.Credentials = new System.Net.NetworkCredential("noreplay@grcboard.com", "grc123!@#");
            //bypas certificate
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                smt.Send(mail);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "alert('Kirim email error :" + ex.Message + "')", true);
            }
        }

        private BeritaAcara GetDetailBA(string BAID)
        {
            BeritaAcara ba = new BeritaAcara();
            string strSQL = "select *,(Select DepoKertas.DepoName From DepoKertas where ID=DepoKertasID)Depo " +
                            "from BeritaAcara where ID=" + BAID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ba.ID = Convert.ToInt32(sdr["ID"].ToString());
                    ba.BANum = sdr["BaNum"].ToString();
                    ba.DepoKertasName = sdr["Depo"].ToString();
                    ba.Selisih = (sdr["Selisih"] != DBNull.Value) ? Convert.ToDecimal(sdr["Selisih"].ToString()) : 0;
                }
            }
            return ba;
        }

        private void ProsesAdjustmentData(int BAID)
        {
            string AutoAdjust = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AutoAdjustProses", "BeritaAcara");
            AdjustDetail adjustDetail = new AdjustDetail();
            ArrayList arrData = new ArrayList();
            ArrayList arrAdjust = new ArrayList();
            BeritaAcara ba = GetDetailBA(BAID.ToString());
            adjustDetail.ItemID = ba.ItemID;
            adjustDetail.Quantity = ba.Selisih;
            adjustDetail.RowStatus = -2;
            adjustDetail.Keterangan = ba.BANum;
            adjustDetail.UomID = 12;
            adjustDetail.AdjustType = (ba.TotalSup < ba.TotalBPAS) ? "Tambah" : "Kurang";
            adjustDetail.ItemTypeID = 1;
            adjustDetail.GroupID = 1;
            arrData.Add(adjustDetail);
            Adjust adjust = new Adjust();
            adjust.AdjustNo = "";
            adjust.AdjustDate = DateTime.Now;
            adjust.AdjustType = ((ba.TotalSup < ba.TotalBPAS)) ? "Tambah" : "Kurang";
            adjust.Status = -2;
            adjust.AlasanCancel = "";
            adjust.CreatedBy = ((Users)Session["Users"]).UserName;
            adjust.ItemTypeID = 1;

            AdjustProcessFacade pakaiProcessFacade = new AdjustProcessFacade(adjust, arrData);
            string strError = (AutoAdjust == "1") ? pakaiProcessFacade.Insert() : string.Empty;
            if (strError == string.Empty)
            {
                InsertLog(pakaiProcessFacade.AdjustNo);
                KirimEmail(0, BAID.ToString());

            }


        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Adjustment Barang";
            eventLog.EventName = "Insert";
            eventLog.DocumentNo = eventName;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        private void LoadBA(string BANumber)
        {
            ArrayList arrData = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.TableName = "BeritaAcara";
            zw.Where = "where RowStatus>-1";
            zw.Criteria = " and BANum='" + BANumber + "'";
            zw.Field = " *,(select dbo.ItemNameInv(ItemID,1))ItemName,(select dbo.ItemCodeInv(ItemID,1))ItemCode ";
            zw.Field += ",(select dbo.SatuanInv(ItemID,1))Unit ";
            zw.Field += ",(Select ISNULL(SUM(JmlBal),0)JmlBal from BeritaAcaraDetail where BAID=BeritaAcara.ID)JmlBal";
            zw.Field += ",(Select top 1 DepoKertas.DepoName from DepoKertas where DepoKertas.ID=BeritaAcara.DepoKertasID)DepoName";
            zw.QueryType = Operation.SELECT;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    ddlNamaBarang.SelectedValue = sdr["ItemID"].ToString();
                    ddlDepo.SelectedValue = sdr["DepoKertasID"].ToString();
                    ddlfromDept.SelectedValue = sdr["FromDept"].ToString();
                    ddlToDept.SelectedValue = sdr["ToDept"].ToString();
                    txtTotalBal.Text = Convert.ToDecimal(sdr["JmlBalBPAS"].ToString()).ToString("###,##0.#0");
                    txtKABPAS.Text = Convert.ToDecimal(sdr["KadarAirBPAS"].ToString()).ToString("###,##0.#0");
                    txtSelisih.Text = (Convert.ToDecimal(sdr["JmlBal"].ToString()) - Convert.ToDecimal(sdr["JmlBalBPAS"].ToString())).ToString("###,##0.#0");
                    txtTotQtySup.Text = Convert.ToDecimal(sdr["TotalSup"].ToString()).ToString("###,##0.#0");
                    txtQtyBPAS.Text = Convert.ToDecimal(sdr["TotalBPAS"].ToString()).ToString("###,##0.#0");
                    txtSelisih.Text = Convert.ToDecimal(sdr["Selisih"].ToString()).ToString("###,##0.#0");
                    txtProsKA.Text = Convert.ToDecimal(sdr["ProsSelisih"].ToString()).ToString("###,##0.#0");
                    txtNetBPAS.Text = Convert.ToDecimal(sdr["Netto"].ToString()).ToString("###,##0.#0");
                    // header field
                    txtBANum.Text = sdr["BANum"].ToString();
                    btnPrint.Enabled = true;
                    LoadDetailBA(sdr["ID"].ToString(), lstBA);
                }
            }
        }

        private void LoadDetailBA(string BAID, Repeater lst)
        {
            ArrayList arrDetailBA = new ArrayList();
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "Select *,(select ReceiptDate from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptDate ";
            zw.CustomQuery += ",(select ReceiptNo from Receipt where ID=BeritaAcaraDetail.ReceiptNo)ReceiptNo1 ";
            zw.CustomQuery += "from BeritaAcaraDetail where RowStatus>-1 and BAID=" + BAID;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows && sdr != null)
            {
                while (sdr.Read())
                {
                    arrDetailBA.Add(new Receipt
                    {
                        ReceiptNo = sdr["ReceiptNo1"].ToString(),
                        ReceiptDate = (sdr["ReceiptDate"] == DBNull.Value) ? DateTime.MinValue : DateTime.Parse(sdr["ReceiptDate"].ToString()),
                        SupplierName = sdr["SupplierName"].ToString(),
                        SupplierID = int.Parse(sdr["SupplierID"].ToString()),
                        Gross = decimal.Parse(sdr["Gross"].ToString()),
                        Netto = decimal.Parse(sdr["Netto"].ToString()),
                        Quantity = decimal.Parse(sdr["Quantity"].ToString()),
                        KadarAir = decimal.Parse(sdr["KadarAir"].ToString()),
                        JmlBal = decimal.Parse(sdr["jmlBal"].ToString()),
                        NoSJ = sdr["NoSJ"].ToString()
                    });
                }
            }
            lst.DataSource = arrDetailBA;
            lst.DataBind();
        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
    }
}