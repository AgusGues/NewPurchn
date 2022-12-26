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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccessLayer;

namespace GRCweb1.Modul.Factory
{
    public partial class T3Retur1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                txtTglRetur.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                txtTglProduksi.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
                //clearform();
                LoadDataGridViewItem();
                LoadType();
            }
        }
        private void LoadType()
        {
            string plant1 = string.Empty;
            string plant2 = string.Empty;
            string plant3 = string.Empty;
            Users users = (Users)Session["Users"];
            Company company = new Company();
            CompanyFacade companyf = new CompanyFacade();
            company = companyf.RetrieveById(users.UnitKerjaID);
            if (users.UnitKerjaID == 7)
            {
                plant1 = "Karawang";
                plant2 = "Citeureup";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 1)
            {
                plant1 = "Citeureup";
                plant2 = "Karawang";
                plant3 = "Jombang";
            }
            if (users.UnitKerjaID == 13)
            {
                plant1 = "Jombang";
                plant2 = "Citeureup";
                plant3 = "Karawang";
            }
            RBType1.Text = plant1;
            RBType2.Text = plant2 + " di " + plant1;
            RBType3.Text = plant1 + " di " + plant2;
            RBType4.Text = plant1 + " di " + plant3;
            RBType5.Text = plant3 + " di " + plant1;
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
            ArrayList arrReturItems = (ArrayList)Session["ListofReturItems"];
            Session["ListofReturItems"] = arrReturItems;
            GridItem0.DataSource = arrReturItems;
            GridItem0.DataBind();
        }

        private void clearform()
        {
            Users users = (Users)Session["Users"];
            Session["ListofReturItems"] = null;
            T3_Retur Retur = new T3_Retur();
            ArrayList arrReturItems = new ArrayList();
            if (users.Apv < 1)
                RBApproval.Visible = false;
            txtReturNo.Text = string.Empty;
            txtSJNo.Text = string.Empty;
            txtOPNo.Text = string.Empty;
            txtCustomer.Text = string.Empty;
            txtExpedisi.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            ddlGP.Items.Clear();
            ddlDefect.Items.Clear();
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

        private void LoadDataGridViewItem()
        {
            ArrayList arrRetur = new ArrayList();
            T3_ReturFacade ReturF = new T3_ReturFacade();
            if (RBTanggal.Checked == true)
            {
                arrRetur = ReturF.RetrieveByTgl0(DateTime.Parse(txtTglRetur.Text).ToString("yyyyMMdd"));
            }

            //if (RBApproval.Checked == true)
            //{
            //    clearform();
            //    arrRetur = ReturF.RetrieveByapv("0");
            //    GridApprove.DataSource = arrRetur;
            //    GridApprove.DataBind();
            //    Session["arrT3Retur"] = arrRetur;
            //    return;
            //}
            GridItem.DataSource = arrRetur;
            GridItem.DataBind();
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            clearform();
            clearform();
        }

        protected void btnTansfer_ServerClick(object sender, EventArgs e)
        {
            T3_Retur Retur = new T3_Retur();
            ArrayList arrReturItems = new ArrayList();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            Users users = (Users)Session["Users"];
            FC_LokasiFacade lokasiF = new FC_LokasiFacade();
            int lokid = lokasiF.GetID(txtLokasi1.Text);
            if (txtLokasi1.Text == string.Empty || txtQty1.Text == string.Empty || lokid == 0)
            {
                DisplayAJAXMessage(this, "Data belum lengkap");
                return;
            }
            if (txtPartnoA.Text.Substring(3, 3) == "-S-")
            {
                DisplayAJAXMessage(this, "UpGrade produk BS, tidak diizinkan..");
                return;
            }
            #region Check Status Closing
            /**
             * check closing periode saat ini
             * added on 13-08-2014
             */
            ClosingFacade Closing = new ClosingFacade();
            int Tahun = Convert.ToInt32(DateTime.Parse(txtTglRetur.Text).ToString("yyyy"));
            int Bulan = Convert.ToInt32(DateTime.Parse(txtTglRetur.Text).ToString("MM"));
            int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
            int clsStat = Closing.GetClosingStatus("SystemClosing");
            if (status == 1 && clsStat == 1)
            {
                DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                return;
            }
            #endregion
            if (Session["ListofReturItems"] != null)
            {
                arrReturItems = (ArrayList)Session["ListofReturItems"];
                foreach (T3_Retur cekreturItems in arrReturItems)
                {
                    if (txtPartnoA.Text.Trim() == cekreturItems.PartnoRtr && txtLokasi1.Text.Trim() == cekreturItems.LokasiRtr.Trim())
                    {
                        DisplayAJAXMessage(this, "Partno : " + txtPartnoA.Text.Trim() + " sudah di input");
                        clearformitems();
                        return;
                    }
                }
            }
            int stock = 0;
            stock = SerahFacade.GetStock(int.Parse(Session["lokid1"].ToString()), int.Parse(Session["itemid1"].ToString()));
            Retur = new T3_Retur();
            Retur.ItemIDSer = int.Parse(Session["itemid1"].ToString());
            Retur.LokasiID = int.Parse(Session["lokid1"].ToString());
            Retur.SJNO = txtSJNo.Text;

            if (txtSJNo.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "nomor surat jalan tidak boleh kosong");
                return;
            }
            //if (txtTglProduksi.SelectedDate.ToString() == "01/01/0001 00.00.00" && txtPartnoA.Text.IndexOf("-P-") != -1)
            //{
            //    DisplayAJAXMessage(this, "Tanggal Produksi Belum Di Pilih !!!");
            //    return;
            //}
            if (txtPartnoA.Text.IndexOf("-P-") != -1 && ddlDefect.SelectedValue == "0" && txtPartnoA.Text.IndexOf("-P-") != -1)
            {
                DisplayAJAXMessage(this, "Jenis Defect Belum Di Pilih !!!");
                return;
            }
            if (ddlGP.SelectedValue == "0" && txtPartnoA.Text.IndexOf("-P-") != -1)
            {
                DisplayAJAXMessage(this, "Group Produksi Belum Di Pilih !!!");
                return;
            }

            Retur.PartnoRtr = txtPartnoA.Text;
            Retur.LokasiRtr = txtLokasi1.Text;
            Retur.Tgltrans = DateTime.Parse(txtTglRetur.Text);
            Retur.Customer = txtCustomer.Text;
            Retur.OPNO = txtOPNo.Text;
            Retur.Qty = int.Parse(txtQty1.Text);
            Retur.Expedisi = txtExpedisi.Text;
            Retur.CreatedBy = users.UserName;
            Retur.SA = stock;

            if (RBType1.Checked == true)
                Retur.TypeR = 1;
            if (RBType2.Checked == true)
                Retur.TypeR = 2;
            if (RBType3.Checked == true)
                Retur.TypeR = 3;
            if (RBType4.Checked == true)
                Retur.TypeR = 4;
            if (RBType5.Checked == true)
                Retur.TypeR = 5;

            if (txtPartnoA.Text.IndexOf("-P-") != -1)
            {
                Retur.JDefect = ddlDefect.SelectedItem.ToString().Trim();
                Retur.GroupProd = ddlGP.SelectedItem.ToString().Trim();
                Retur.TglProd = DateTime.Parse(txtTglProduksi.Text);
            }
            else
            {
                Retur.JDefect = "0";
                Retur.GroupProd = "0";
                Retur.TglProd = Convert.ToDateTime("01/01/2000 12:0:0".ToString());
            }



            arrReturItems.Add(Retur);
            Session["ListofReturItems"] = arrReturItems;
            GridItem0.DataSource = arrReturItems;
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
            ArrayList arrReturItems = new ArrayList();
            T3_ReturMaster T3ReturMaster = new T3_ReturMaster();
            T3_ReturMasterFacade T3ReturMasterFacade = new T3_ReturMasterFacade();
            T3_Retur T3Retur = new T3_Retur();
            T3_ReturFacade T3ReturFacade = new T3_ReturFacade();
            T3_Serah t3serah = new T3_Serah();
            T3_SerahFacade SerahFacade = new T3_SerahFacade();
            int intResult = 0;
            if (Session["ListofReturItems"] != null)
            {
                arrReturItems = (ArrayList)Session["ListofReturItems"];
                //rekam master
                int intdocno = T3ReturMasterFacade.GetDocNo(DateTime.Parse(txtTglRetur.Text)) + 1;
                string docno = "CRP" + DateTime.Parse(txtTglRetur.Text).ToString("yy") + DateTime.Parse(txtTglRetur.Text).ToString("MM") + "-" +
                    intdocno.ToString().PadLeft(5, '0');
                txtReturNo.Text = docno;
                T3ReturMaster.ReturNo = docno;
                T3ReturMaster.ReturDate = DateTime.Parse(txtTglRetur.Text);
                T3ReturMaster.Keterangan = txtKeterangan.Text;
                T3ReturMaster.CreatedBy = users.UserName;


                //if (DatePicker2.SelectedDate.ToString() == "01/01/0001 00.00.00")
                //{
                //    DisplayAJAXMessage(this, "Tanggal Produksi Belum Di Pilih !!!");
                //    return;
                //}
                //if (txtPartnoA.Text.IndexOf("-P-") != -1 && ddlDefect.SelectedValue == "0")
                //{
                //    DisplayAJAXMessage(this, "Jenis Defect Belum Di Pilih !!!");
                //    return;
                //}
                //if (ddlGP.SelectedValue == "0")
                //{
                //    DisplayAJAXMessage(this, "Group Produksi Belum Di Pilih !!!");
                //    return;
                //}
                //if (DatePicker2.SelectedDate.ToString() == "01/01/0001 00:00:00:00")
                //{
                //    DisplayAJAXMessage(this, "Group Produksi Belum Di Pilih !!!");
                //    return;
                //}

                //intResult = T3ReturMasterFacade.Insert(T3ReturMaster);
                //rekam detail
                if (intResult > 0)
                {
                    foreach (T3_Retur ReturItems in arrReturItems)
                    {

                        t3serah.Flag = "tambah";
                        if (RBType3.Checked == true)
                            t3serah.Qty = 0;
                        else
                            t3serah.Qty = ReturItems.Qty;
                        t3serah.GroupID = ReturItems.GroupID;
                        t3serah.ItemID = ReturItems.ItemIDSer;
                        t3serah.LokID = ReturItems.LokasiID;
                        t3serah.CreatedBy = users.UserName;
                        //intResult = SerahFacade.Insert(t3serah);
                        t3serah.JDefect = ddlDefect.SelectedItem.ToString().Trim();
                        t3serah.GroupProd = ddlGP.SelectedItem.ToString().Trim();
                        t3serah.TglProd = DateTime.Parse(txtTglProduksi.Text);

                        ReturItems.ReturID = intResult;

                        //T3ReturFacade.Insert(ReturItems);

                        //T3Retur.SJNO = txtSJNo.Text;
                        //T3Retur.OPNO = txtOPNo.Text;
                        //T3Retur.Customer = txtCustomer.Text;
                        //T3Retur.SerahID = KirimDetail.SerahID;
                        //T3Retur.LokasiID = lokid;
                        //T3Retur.ItemIDSer = fC_Items.ID;
                        //T3Retur.Tgltrans = txtDateRetur.SelectedDate;
                        //T3Retur.GroupID = fC_Items.GroupID;
                        //T3Retur.Qty = Convert.ToInt32(txtQty.Text);
                        //T3Retur.CreatedBy = users.UserName;
                        //T3Retur.ItemIDSJ = Convert.ToInt32(GridViewSJ.Rows[rowindex].Cells[0].Text);
                    }
                }
                T3_ReturProcessFacade SiapKirimProcessFacade = new T3_ReturProcessFacade(T3ReturMaster, arrReturItems);
                string strError = SiapKirimProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    LoadDataGridViewItem();
                    clearform();
                    clearformitems();

                    ddlGP.Enabled = false; ddlDefect.Enabled = false; txtTglProduksi.Enabled = false;
                    txtTglProduksi.Text = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
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
            //txtPartnoA.Focus();
        }

        protected void txtPartnoA_TextChanged(object sender, EventArgs e)
        {
            if (txtPartnoA.Text.IndexOf("-P-") != -1)
            {
                ddlDefect.Enabled = true;
                txtTglProduksi.Enabled = true;
                ddlGP.Enabled = true;
                LoadMasterDefect();
                LoadGroupProd();
            }

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
            //ArrayList arrRetur = new ArrayList();
            //T3_ReturFacade ReturF = new T3_ReturFacade();
            //if (txtPartnoC.Text != string.Empty)
            //    arrRetur = ReturF.RetrieveByPartno(txtPartnoC.Text);

            //GridItem.DataSource = arrRetur;
            //GridItem.DataBind();
            //LoadDataGridViewtrans();
            //txtPartnoC.Text = string.Empty;
        }
        protected void txtLokasiC_TextChanged(object sender, EventArgs e)
        {
            //ArrayList arrRetur = new ArrayList();
            //T3_ReturFacade ReturF = new T3_ReturFacade();

            //if (txtLokasiC.Text != string.Empty)
            //    arrRetur = ReturF.RetrieveByLokasi(txtLokasiC.Text);
            //GridItem.DataSource = arrRetur;
            //GridItem.DataBind();
            //LoadDataGridViewtrans();
            //txtLokasiC.Text = string.Empty;
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
            //if (txtPartnoA.Text == string.Empty)
            //    txtPartnoA.Focus();
            //else
            //    if (txtLokasi1.Text == string.Empty)
            //        txtLokasi1.Focus();
            //    else
            //if (txtQty1.Text == string.Empty)
            //    txtQty1.Focus();
            //else
            //    btnTansfer.Focus();
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

        protected void ChkHide2_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHide2.Checked == false)
                Panel3.Visible = false;
            else
                Panel3.Visible = true;
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlCari.SelectedIndex == 0)
            //{
            //    txtPartnoC.Visible = true;
            //    txtLokasiC.Visible = false;
            //    txtCariBA.Visible = false;
            //    txtPartnoC.Text = string.Empty;
            //    txtLokasiC.Text = string.Empty;
            //}
            //else
            //    if (ddlCari.SelectedIndex == 1)
            //    {
            //        txtPartnoC.Visible = false;
            //        txtCariBA.Visible = false;
            //        txtLokasiC.Visible = true;
            //        txtCariBA.Text = string.Empty;
            //        txtPartnoC.Text = string.Empty;
            //        txtLokasiC.Text = string.Empty;
            //    }
            //    else
            //    {
            //        txtPartnoC.Visible = false;
            //        txtLokasiC.Visible = false;
            //        txtCariBA.Visible = true;
            //        txtCariBA.Text = string.Empty;
            //        txtPartnoC.Text = string.Empty;
            //        txtLokasiC.Text = string.Empty;
            //    }
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
        protected void RBReturNo_CheckedChanged(object sender, EventArgs e)
        {
            //if (RBNoBA.Checked == true)
            //{
            //    PanelApprove.Visible = false;
            //    PanelApprove.Visible = false;
            //    GridApprove.Visible = false;
            //    GridItem.Visible = true;
            //    LoadDataGridViewItem();
            //}
        }
        protected void txtCariReturNo_TextChanged(object sender, EventArgs e)
        {
            ArrayList arrRetur = new ArrayList();
            T3_ReturFacade ReturF = new T3_ReturFacade();
            //if (txtCariReturNo.Text != string.Empty)
            //    arrRetur = ReturF.RetrieveByNoBA(txtCariBA.Text);

            GridItem.DataSource = arrRetur;
            GridItem.DataBind();
        }

        protected void GridItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Users users = (Users)Session["Users"];
            T3_Retur Retur = new T3_Retur();
            Button deletebtn = (Button)GridItem.FindControl("deletebtn");
            T3_ReturFacade ReturF = new T3_ReturFacade();
            if (e.CommandName == "hapus")
            {
                #region Verifikasi Closing Periode
                ClosingFacade Closing = new ClosingFacade();
                int Tahun = Convert.ToInt32(DateTime.Parse(txtTglRetur.Text).ToString("yyyy"));
                int Bulan = Convert.ToInt32(DateTime.Parse(txtTglRetur.Text).ToString("MM"));
                int status = Closing.GetMonthStatus(Tahun, Bulan, "Produksi");
                int clsStat = Closing.GetClosingStatus("SystemClosing");
                if (status == 1 && clsStat == 1)
                {
                    DisplayAJAXMessage(this, "Periode " + Global.nBulan(Bulan) + " " + Tahun + " sudah Closing. Transaksi Tidak bisa dilakukan");
                    return;
                }
                #endregion
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridItem.Rows[index];
                string ID = row.Cells[0].Text;
                ZetroView zl = new ZetroView();
                zl.QueryType = Operation.CUSTOM;
                zl.CustomQuery = "declare @ID int " +
                    "declare @qty int " +
                    "set @id=" + ID +
                    "set @qty=(select qty from t3_retur where ID=@ID) " +
                    "update t3_retur set rowstatus=-1 where ID=@ID " +
                    "update t3_serah set qty=qty-@qty where itemid in (select ItemID from t3_retur where ID=@ID) and lokid in (select lokID from t3_retur where ID=@ID)";
                SqlDataReader sdr = zl.Retrieve();
                LoadDataGridViewItem();
            }
        }
        protected void ChkAll_CheckedChanged(object sender, EventArgs e)
        {
            ArrayList arrT3Retur = (ArrayList)Session["arrT3Retur"];
            int i = 0;
            if (ChkAll.Checked == true) //& arrLembur.Count >0 
            {

                foreach (T3_Retur T3Retur in arrT3Retur)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = true;
                    i = i + 1;
                }
            }
            else
            {
                foreach (T3_Retur T3Retur in arrT3Retur)
                {
                    CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
                    chk.Checked = false;
                    i = i + 1;
                }
            }
        }

        protected void btnApprove_ServerClick(object sender, EventArgs e)
        {
            //T3_ReturFacade T3ReturFacade = new T3_ReturFacade();
            //ArrayList arrT3Retur = (ArrayList)Session["arrT3Retur"];
            //Users users = (Users)Session["Users"];
            //T3_Serah t3serah = new T3_Serah();
            //T3_SerahFacade SerahFacade = new T3_SerahFacade();

            //int intResult = 0;
            //int i = 0;

            //try
            //{
            //    foreach (T3_Retur T3Retur in arrT3Retur)
            //    {
            //        CheckBox chk = (CheckBox)GridApprove.Rows[i].FindControl("chkapv");
            //        if (chk.Checked)
            //        {
            //            if (users.Apv == 0)
            //            {
            //                DisplayAJAXMessage(this, "Level Approval tidak mencukupi");
            //                return;
            //            }
            //            T3Retur.Apv = users.Apv;
            //            T3Retur.ID = int.Parse(GridApprove.Rows[i].Cells[0].Text);
            //            T3Retur.CreatedBy = users.UserName;
            //            intResult = T3ReturFacade.Update(T3Retur);
            //            if (T3ReturFacade.Error != string.Empty)
            //            {
            //                break;
            //            }
            //            if (T3Retur.Qty  > 0)
            //            {
            //                t3serah.Flag = "kurang";
            //                t3serah.Qty = T3Retur.QtyOut;
            //                int stock = 0;
            //                stock = SerahFacade.GetStock(T3Retur.LokID, T3Retur.ItemID);
            //                if (stock - Convert.ToInt32(txtQty1.Text) < 0)
            //                {
            //                    DisplayAJAXMessage(this, "Stock tidak mencukupi, proses dibatalkan !");
            //                    return;
            //                }
            //            }
            //            else
            //            {
            //                t3serah.Flag = "tambah";
            //                t3serah.Qty = T3Retur.QtyIn;
            //            }
            //            t3serah.GroupID = 0;
            //            t3serah.ItemID = T3Retur.ItemID;
            //            t3serah.LokID = T3Retur.LokID;
            //            t3serah.CreatedBy = users.UserName;
            //            intResult = SerahFacade.Insert(t3serah);
            //        }
            //        i = i + 1;
            //    }
            //}
            //catch
            //{
            //    return;
            //}
            //LoadDataGridViewItem();
        }

        protected void txtSJNo_TextChanged(object sender, EventArgs e)
        {
            T3_Kirim t3kirim = new T3_Kirim();
            T3_KirimFacade t3kirimF = new T3_KirimFacade();
            t3kirim = t3kirimF.RetrieveBySJNo(txtSJNo.Text);
            txtOPNo.Text = t3kirim.OPNo;
            txtCustomer.Text = t3kirim.Customer;
        }

        private void LoadMasterDefect()
        {
            MasterDefect2 domainDefect = new MasterDefect2();
            FacadeMasterDefect2 facadeDefect = new FacadeMasterDefect2();
            ArrayList arrDefect = facadeDefect.RetrieveMasterDefect();
            if (arrDefect.Count > 0)
            {
                ddlDefect.Items.Clear();
                ddlDefect.Items.Add(new ListItem("---- Pilih ----", "0"));
                foreach (MasterDefect2 def in arrDefect)
                {
                    ddlDefect.Items.Add(new ListItem(def.JenisDefect, def.JenisDefect));
                }
            }
        }

        private void LoadGroupProd()
        {
            MasterDefect2 domainProd = new MasterDefect2();
            FacadeMasterDefect2 facadeProd = new FacadeMasterDefect2();
            ArrayList arrProd = facadeProd.RetrieveGProduksi();
            if (arrProd.Count > 0)
            {
                ddlGP.Items.Clear();
                ddlGP.Items.Add(new ListItem("Lain-lain", "100"));

                foreach (MasterDefect2 prod in arrProd)
                {
                    ddlGP.Items.Add(new ListItem(prod.GroupProduksi, prod.GroupProduksi));
                }
            }

        }
    }

    public class MasterDefect2
    {
        public string JenisDefect { get; set; }
        public string GroupProduksi { get; set; }
        public int ID { get; set; }
    }

    public class FacadeMasterDefect2
    {
        public string strError = string.Empty;
        private ArrayList arrData = new ArrayList();
        private MasterDefect2 pm = new MasterDefect2();

        public ArrayList RetrieveMasterDefect()
        {
            arrData = new ArrayList();
            string strSQL =
            " select ID,DefName JenisDefect from Def_MasterDefect where RowStatus >-1 order by Urutan ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new MasterDefect2
                    {
                        ID = int.Parse(sdr["ID"].ToString()),
                        JenisDefect = sdr["JenisDefect"].ToString()
                    });
                }
            }
            return arrData;
        }

        public ArrayList RetrieveGProduksi()
        {
            arrData = new ArrayList();
            string strSQL =
            " select ID,[Group]GroupProduksi from BM_PlantGroup order by ID ";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);

            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(new MasterDefect2
                    {
                        ID = int.Parse(sdr["ID"].ToString()),
                        GroupProduksi = sdr["GroupProduksi"].ToString()
                    });
                }
            }
            return arrData;
        }
    }
}