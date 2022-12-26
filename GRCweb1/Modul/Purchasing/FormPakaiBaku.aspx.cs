using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Domain;
using BusinessFacade;
using System.Collections;
using System.Threading;
using System.Xml.Linq;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;


namespace GRCweb1.Modul.Purchasing
{
    public partial class FormPakaiBaku : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "../../Default.aspx";

                //bisa diatur lewat users.DeptId
                //perlu tanya user utk per user bisa akses dept apa aja
                LoadDept();
                if (Request.QueryString["PakaiNo"] != null)
                {
                    LoadPakai(Request.QueryString["PakaiNo"].ToString());
                }
                else
                {
                    clearForm();
                }
                LoadListLockSPP();
                CheckReOrderPoint();
                //CheckSPBPending();
                stk.Visible = (PurchnConfig("ViewOnlyStockInSPB") == 1) ? true : false;
                stk.Checked = (PurchnConfig("ViewOnlyStockInSPB") == 1) ? true : false;

                /** Penambahan ddl Tebal by Beny**/
                LoadTebal();
            }
            else
            {
                if (txtKodeDept.Text != string.Empty && txtCariNamaBrg.Text != string.Empty)
                {
                    //base on grouppurchn: GroupID pada Inventory = 1 & GroupPurchn
                    LoadItems(1);

                    txtCariNamaBrg.Text = string.Empty;
                }
            }
            btnCancel.Attributes.Add("onclick", "return confirm_delete();");
        }

        private void LoadTebal()
        {
            ddlTebal.Items.Clear();
            ArrayList arrTebal = new ArrayList();
            PakaiFacade pFacade = new PakaiFacade();
            arrTebal = pFacade.RetrieveTebal();
            ddlTebal.Items.Add(new ListItem("-- Pilih --", "0"));
            foreach (Pakai pk in arrTebal)
            {
                ddlTebal.Items.Add(new ListItem(pk.Keterangan.ToString(), pk.Keterangan.ToString()));
            }
        }

        private void CheckSPBPending()
        {
            PakaiFacade pf = new PakaiFacade();
            string message = string.Empty; string Statuse = "";
            ArrayList result = pf.RetrieveOpenStatus("", ((Users)Session["Users"]).DeptID.ToString());
            foreach (Pakai p in result)
            {
                Statuse = (p.Status == 0) ? "Open" : "Head";
                message += p.PakaiNo + " - " + p.Tanggal.TrimEnd() + " - Status : " + Statuse + "\\n";
            }
            if (result.Count > 0)
            {
                string msg = "alert('Untuk Sementara ini tidak bisa melakukan SPB, " +
                            "\\nMasih ada SPB yang belum di ambil sebagai berikut :\\n" + message +
                            "\\nSilahkan Hubungi Logistic Material');" +
                            "window.location.href='home.aspx'";
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "SPB Alert", msg, true);
            }
        }

        private void clearForm()
        {
            Session["id"] = null;
            Session["ListOfPakaiDetail"] = new ArrayList();
            Session["Pakai"] = null;
            Session["PakaiNo"] = null;
            Session["Aprove"] = null;
            txtPakaiNo.Text = string.Empty;
            txtTanggal.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtCreatedBy.Text = ((Users)Session["Users"]).UserName;
            txtUom.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtKodeDept.Text = string.Empty;
            txtItemCode.Text = string.Empty;
            txtCariNamaBrg.Text = string.Empty;
            txtQtyPakai.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtSearch.Text = string.Empty;
            txtUomID.Text = string.Empty;
            txtTanggal.Enabled = true;
            //ddlDeptName.Items.Clear();
            if (ddlDeptName.SelectedIndex > 0)
                ddlDeptName.SelectedIndex = 0;
            //else
            //    ddlDeptName.Items.Clear();
            ddlDeptName.Enabled = true;
            ddlItemName.Items.Clear();
            ddlProdLine.Items.Clear();
            ddlProdLine.Enabled = false;

            ArrayList arrList = new ArrayList();
            arrList.Add(new PakaiDetail());

            btnUpdate.Disabled = false;
            btnCancel.Enabled = false;
            btnPrint.Disabled = true;
            lbAddOP.Enabled = true;
            ddlDeptName.Enabled = true;
            txtKeterangan.Text = ddlShift.SelectedValue;
            GridView1.Columns[5].Visible = true;
            GridView1.DataSource = arrList;
            GridView1.DataBind();
            lstSPB.DataSource = arrList;
            lstSPB.DataBind();

        }

        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }

        private void LoadDept()
        {
            ddlDeptName.Items.Clear();
            ArrayList arrDept = new ArrayList();
            DeptFacade deptFacade = new DeptFacade();
            arrDept = deptFacade.Retrieve();
            string[] arrDpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnlockDept", "SPB").Split(',');
            ddlDeptName.Items.Add(new ListItem("-- Pilih Dept --", "0"));
            foreach (Dept dept in arrDept)
            {
                if (PurchnConfig("MaterialByDept") == 0 || arrDpt.Contains(((Users)Session["Users"]).DeptID.ToString()))
                {
                    ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                }
                else
                {
                    if (dept.ID == ((Users)Session["Users"]).DeptID && ((Users)Session["Users"]).DeptID != 10)
                    {
                        ddlDeptName.Items.Add(new ListItem(dept.DeptName, dept.ID.ToString()));
                    }
                }
            }
        }

        protected void ddlDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDeptName.SelectedIndex > 0)
            {
                DeptFacade deptFacade = new DeptFacade();
                Dept dept = deptFacade.RetrieveById(int.Parse(ddlDeptName.SelectedValue));
                string StatusPilihLine = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Status", "SPBProduksi");
                if (deptFacade.Error == string.Empty && dept.ID > 0)
                {
                    txtKodeDept.Text = dept.DeptCode;
                    if (StatusPilihLine == "1") GetProdukLine(int.Parse(ddlDeptName.SelectedValue));
                    txtCariNamaBrg.Focus();
                    ddlDeptName.Enabled = false;
                }
            }
        }
        protected void GetProdukLine(int DeptID)
        {
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "SPBProduksi").Split(new string[] { "," }, StringSplitOptions.None);

            if (arrDept.Contains(DeptID.ToString()) || arrDept.Contains("All"))
            {
                ddlProdLine.Items.Clear();
                ddlProdLine.Items.Add(new ListItem("--Pilih Line --", "0"));
                ArrayList arrDepts = new DeptFacade().GetLine();
                foreach (Plant pl in arrDepts)
                {
                    ddlProdLine.Items.Add(new ListItem(pl.PlantCode.ToString(), pl.ID.ToString()));
                }
                ddlProdLine.Enabled = true;
            }
            else
            {
                ddlProdLine.Items.Clear();
                ddlProdLine.Enabled = false;
            }
        }
        private void LoadItems(int intGroupID)
        {
            if (txtKeterangan.Text.Trim() == string.Empty)
            {
                DisplayAJAXMessage(this, "Keterangan tidak boleh kosong");
                return;
            }
            ddlItemName.Items.Clear();
            ArrayList arrInventory = new ArrayList();
            InventoryFacade InventoryFacade = new InventoryFacade();
            string Stocked = (PurchnConfig("ViewOnlyStockInSPB") == 1 && stk.Checked == true) ? " jumlah >0 and " : "";
            arrInventory = InventoryFacade.RetrieveByCriteriaWithGroupID(Stocked + "A.ItemName", txtCariNamaBrg.Text, intGroupID);

            ddlItemName.Items.Add(new ListItem("-- Pilih Inventory --", "0"));
            foreach (Inventory Inv in arrInventory)
            {
                ddlItemName.Items.Add(new ListItem(Inv.ItemName + "  (" + Inv.ItemCode + ")", Inv.ID.ToString()));
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddDelete")
            {
                Session["AlasanBatal"] = "";
                // bisa cancel hanya bisa level head dept ke atas
                int intApv = ((Users)Session["Users"]).Apv;
                //string myScript = "confirm_hapus()";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "MyScript", myScript, true);
                //if (Session["AlasanBatal"] != null)
                //{
                //    if (Session["AlasanBatal"].ToString() == string.Empty)
                //    {
                //        DisplayAJAXMessage(this, "Alasan Hapus tidak boleh kosong");
                //        return;
                //    }
                if (intApv > 0)
                {
                    #region proses hapus
                    string strDocumentNo = string.Empty;
                    int intPakaiDetailID = 0;
                    string strEvent = string.Empty;

                    int index = Convert.ToInt32(e.CommandArgument);
                    ArrayList arrTransferDetail = new ArrayList();
                    arrTransferDetail = (ArrayList)Session["ListOfPakaiDetail"];


                    Pakai pakai = new Pakai();
                    if (Session["id"] != null && intApv > 0)
                    {
                        //if (btnCancel.Enabled == false) return;

                        int id = (int)Session["id"];

                        PakaiFacade pakaiFacade = new PakaiFacade();

                        // musti pake PakaiTipe agar bisa dibedakan No pemakaian-nya
                        // musti dipikirin utk gak bisa hapus jika accounting dah closing
                        // 2 = bahan baku
                        Company company = new Company();
                        CompanyFacade companyFacade = new CompanyFacade();
                        string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";
                        AccClosingFacade acc = new AccClosingFacade();
                        string CheckClosing = acc.CheckClosing(DateTime.Now.Month, DateTime.Now.Year);

                        Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);

                        #region validasi closing periode
                        if (CheckClosing == (pki.CreatedTime.Month.ToString() + pki.CreatedTime.Year.ToString()))
                        {
                            DisplayAJAXMessage(this, "SPB No. " + pki.PakaiNo + " tidak dapat dihapus periode sudah di close by Accounting");
                            return;
                        }
                        #endregion
                        #region validasi status SPB jika sudah di approve gudang tidak bisa
                        if (pki.Status == 2)
                        {
                            DisplayAJAXMessage(this, "Tidak bisa dihapus barang sudah diapprove gudang;");
                            return;
                        }
                        #endregion

                        if (pakaiFacade.Error == string.Empty && pki.ID > 0)
                        {
                            #region validasi closing periode
                            if (CheckClosing == (pki.CreatedTime.Month.ToString() + pki.CreatedTime.Year.ToString()))
                            {
                                DisplayAJAXMessage(this, "SPB No. " + pki.PakaiNo + " tidak dapat dihapus periode sudah di close by Accounting");
                                return;
                            }
                            #endregion
                            #region validasi status SPB jika sudah di approve gudang tidak bisa
                            if (pki.Status == 2)
                            {
                                DisplayAJAXMessage(this, "Tidak bisa dihapus barang sudah diapprove gudang");
                                return;
                            }
                            #endregion
                            int i = 0;
                            int x = 0;
                            PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                            ArrayList arrCurrentPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                            if (pakaiDetailFacade.Error == string.Empty)
                            {
                                PakaiDetail receiptDetail1 = (PakaiDetail)arrTransferDetail[index];
                                bool valid = false;
                                int pkiDetailID = 0;
                                #region validasi itemdetail
                                foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
                                {
                                    if (pkiDetail.ItemID == receiptDetail1.ItemID)
                                    {
                                        pkiDetailID = pkiDetail.ID;
                                        i = x;
                                        valid = true;
                                        break;
                                    }

                                    x = x + 1;
                                }
                                #endregion
                                if (valid == false)
                                {
                                    arrTransferDetail.RemoveAt(index);
                                    Session["ListOfPakaiDetail"] = arrTransferDetail;
                                    GridView1.DataSource = arrTransferDetail;
                                    GridView1.DataBind();
                                }
                                else
                                {
                                    PakaiDetail pakaiDetail = (PakaiDetail)arrTransferDetail[index];
                                    ArrayList arrPakaiDetail = new ArrayList();
                                    foreach (PakaiDetail pd in arrTransferDetail)
                                    {
                                        if (pd.ID == pkiDetailID)
                                        {
                                            //((ReceiptDetail)arrTransferDetail[i]).FlagPO = flagPO;
                                            arrPakaiDetail.Add(pd);
                                        }
                                    }
                                    PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(new Pakai(), arrPakaiDetail, new PakaiDocNo());
                                    intPakaiDetailID = pakaiDetail.ID;

                                    string strError = pakaiProcessFacade.CancelPakaiDetail();
                                    if (strError != string.Empty)
                                    {
                                        DisplayAJAXMessage(this, strError);
                                        return;
                                    }
                                    else
                                    {
                                        DisplayAJAXMessage(this, "Data berhasil dihapus");

                                        EventLog evn = new EventLog();
                                        evn.CreatedBy = ((Users)Session["Users"]).UserName;
                                        evn.DocumentNo = txtPakaiNo.Text;
                                        evn.EventName = "Delete SPB Detail ID :" + ID.ToString() + " karena " + Session["AlasanBatal"].ToString();
                                        evn.ModulName = "Pemakaian Bahan Baku";
                                        int rst = new EventLogFacade().Insert(evn);
                                    }
                                    ArrayList arrTransfer = new ArrayList();

                                    foreach (PakaiDetail pd in arrTransferDetail)
                                    {
                                        //if (rd.DocumentNo != strDocumentNo)
                                        if (pd.ID != intPakaiDetailID)
                                        {
                                            arrTransfer.Add(pd);
                                        }
                                    }
                                    string pno = (Request.QueryString["PakaiNo"] != null) ? string.Empty : "?PakaiNo=" + txtPakaiNo.Text;
                                    Response.Redirect(Request.RawUrl + pno);
                                    Session["ListOfPakaiDetail"] = arrTransfer;
                                    GridView1.DataSource = arrTransfer;
                                    GridView1.DataBind();

                                }
                            }
                        }
                    }
                    else
                    {
                        arrTransferDetail.RemoveAt(index);
                        Session["ListOfPakaiDetail"] = arrTransferDetail;
                        GridView1.DataSource = arrTransferDetail;
                        GridView1.DataBind();
                    }
                    #endregion
                }

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Text = DateTime.Parse(e.Row.Cells[0].Text).ToString("dd-MMM-yyyy");

                e.Row.Cells[2].Text = Convert.ToDecimal(e.Row.Cells[2].Text).ToString("N2");
            }
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";

            Response.Redirect("ListPakaiBaku.aspx?approve=" + kd);
            //Response.Redirect("ListPakaiBaku.aspx?approve=KP");
        }

        protected void btnCancel_ServerClick(object sender, EventArgs e)
        {
            // bisa cancel hanya bisa level head dept ke atas
            int intApv = ((Users)Session["Users"]).Apv;

            if (Session["id"] != null && intApv > 0)
            {

                // masuk sini krn sudah di save
                // next job
                // cek dulu jika mau hapus apakah stok - qty yg hapus >=0 or sdh terpakai

                int id = (int)Session["id"];
                string strEvent = string.Empty;

                PakaiFacade pakaiFacade = new PakaiFacade();
                // 2 = bahan baku
                Company company = new Company();
                CompanyFacade companyFacade = new CompanyFacade();
                string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";

                //string strPakaiTipe = "KP";
                Pakai pki = pakaiFacade.RetrieveByNoWithStatus(txtPakaiNo.Text, strPakaiTipe);
                if (pakaiFacade.Error == string.Empty && pki.ID > 0)
                {
                    if (pki.Status >= 3)
                    {
                        DisplayAJAXMessage(this, "Pemakaian tidak boleh di Cancel (harus di retur)");
                        //return; 
                    }
                    if (Session["AlasanCancel"] != null)
                    {
                        pki.AlasanCancel = Session["AlasanCancel"].ToString();
                        Session["AlasanCancel"] = null;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Alasan Cancel tidak boleh kosong / blank");
                        return;
                    }

                    PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                    ArrayList arrCurrentPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pki.ID);
                    if (pakaiDetailFacade.Error == string.Empty)
                    {

                        ArrayList arrPakaiDetail = new ArrayList();
                        foreach (PakaiDetail pkiDetail in arrCurrentPakaiDetail)
                        {

                            arrPakaiDetail.Add(pkiDetail);
                        }

                        PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pki, arrPakaiDetail, new PakaiDocNo());

                        string strError = pakaiProcessFacade.CancelPakai();
                        if (strError != string.Empty)
                        {
                            DisplayAJAXMessage(this, strError);
                            return;
                        }

                        else
                        {
                            strEvent = "Cancel All";
                            InsertLog(strEvent);

                            DisplayAJAXMessage(this, "Cancel berhasil .....");
                            clearForm();
                        }

                    }
                    //        }
                }
            }
        }

        protected void lbAddOP_Click(object sender, EventArgs e)
        {
            #region validasi input
            try
            {
                decimal cekNumber = decimal.Parse(txtQtyPakai.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity Pakai harus number");
                return;
            }
            if (decimal.Parse(txtQtyPakai.Text) == 0)
            {
                DisplayAJAXMessage(this, "Quantity Terima tidak boleh 0");
                return;
            }

            try
            {
                decimal cekNumber = decimal.Parse(txtStok.Text);
            }
            catch
            {
                DisplayAJAXMessage(this, "Quantity Stok harus number");
                return;
            }
            if (decimal.Parse(txtStok.Text) == 0)
            {
                DisplayAJAXMessage(this, "Quantity Stok tidak boleh 0");
                return;
            }

            if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text))
            {
                DisplayAJAXMessage(this, "Quantity Pakai lebih besar dari pada Stok");
                return;
            }

            if (txtKeterangan.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Keterangan harus diisi...");
                return;
            }

            #endregion
            //filter multigudang
            #region SPP Multigudang not used
            //SPPMultiGudangFacade sppmf = new SPPMultiGudangFacade();
            //decimal StockOtherDept = sppmf.RetrieveByStock(int.Parse(ddlDeptName.SelectedValue), int.Parse(ddlItemName.SelectedValue), int.Parse(txtGroupID.Text), 1);
            //if (decimal.Parse(txtQtyPakai.Text) > decimal.Parse(txtStok.Text) - StockOtherDept)
            //{
            //    DisplayAJAXMessage(this, "Sebagian Quantity Stock sudah dipesan oleh departemen lain, coba kurangi Qty pakainya");
            //    return;
            //}
            //ddlDeptName.Enabled = false;
            //end filter multigudang
            //"K" utk Karawang
            #endregion
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";
            Session["Aprove"] = null;
            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            #region Cek Tanggal terakhir input dan periode closing
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);

            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else
            {
                if (nowTgl < lastTgl && PurchnConfig("SystemClosing") == 0)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    //int intApv = ((Users)Session["Users"]).Apv;
                    //if (intApv == 0)
                    return;

                }
            }
            #endregion
            PakaiDetail pakaiDetail = new PakaiDetail();
            ArrayList arrListPakaiDetail = new ArrayList();
            #region validasi doble item barang
            if (Session["ListOfPakaiDetail"] != null)
            {
                arrListPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                if (arrListPakaiDetail.Count > 0)
                {
                    int ada = 0; decimal tspb = 0;
                    foreach (PakaiDetail pki in arrListPakaiDetail)
                    {
                        if (pki.ItemCode == txtItemCode.Text && pki.LineNo == int.Parse(ddlProdLine.SelectedValue))
                        {
                            DisplayAJAXMessage(this, "Material " + ddlItemName.SelectedItem + " untuk " + ddlProdLine.SelectedItem + " sudah ada di daftar");

                            clearQty();
                            return;
                        }
                        ada = ada + 1;
                    }
                }
            }
            #endregion
            #region Validasi Pilih line untuk dept produksi proses
            string[] arrDept = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("OnlyDept", "SPBProduksi").Split(new string[] { "," }, StringSplitOptions.None);
            string StatusPilihLine = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("Status", "SPBProduksi").ToString();
            if ((arrDept.Contains(ddlDeptName.SelectedValue) || arrDept.Contains("All")) &&
                StatusPilihLine == "1" && ddlProdLine.SelectedIndex == 0)
            {
                DisplayAJAXMessage(this, "Produksi Line belum dipilih!");
                return;
            }
            #endregion
            // cek ke re-order point
            #region informasi reorder poin status
            InventoryFacade InventoryFacade = new InventoryFacade();
            Inventory inv = InventoryFacade.RetrieveById2(int.Parse(ddlItemName.SelectedValue));
            if (InventoryFacade.Error == string.Empty && inv.ID > 0)
            {
                if (inv.Jumlah - decimal.Parse(txtQtyPakai.Text) <= inv.ReOrder)
                {
                    string aMess = "Jumlah Pemakaian Barang di stock sudah melampaui Reorder Point ( " + inv.ReOrder.ToString() + " )";
                    DisplayAJAXMessage(this, aMess);
                    // return; 
                }
            }
            //
            #endregion
            decimal stoke = 0;
            decimal.TryParse(txtStok.Text, out stoke);
            pakaiDetail.ItemID = int.Parse(ddlItemName.SelectedValue);
            pakaiDetail.Quantity = decimal.Parse(txtQtyPakai.Text);
            pakaiDetail.RowStatus = 0;
            pakaiDetail.Keterangan = txtKeterangan.Text;
            pakaiDetail.GroupID = int.Parse(txtGroupID.Text);
            pakaiDetail.UomID = int.Parse(txtUomID.Text);
            pakaiDetail.ItemCode = txtItemCode.Text;
            pakaiDetail.ItemName = ddlItemName.SelectedItem.ToString();
            pakaiDetail.UOMCode = txtUom.Text;
            pakaiDetail.KartuStock = stoke;
            //for inv
            pakaiDetail.ItemTypeID = 1;
            // for line number
            pakaiDetail.LineNo = (ddlProdLine.SelectedValue == string.Empty) ? 0 : int.Parse(ddlProdLine.SelectedValue.ToString());

            /** Beny **/
            pakaiDetail.Press = ddlTebal.SelectedItem.ToString().Trim();

            arrListPakaiDetail.Add(pakaiDetail);
            Session["ListOfPakaiDetail"] = arrListPakaiDetail;
            GridView1.Columns[5].Visible = true;
            GridView1.DataSource = arrListPakaiDetail;
            GridView1.DataBind();

            lstSPB.DataSource = arrListPakaiDetail;
            lstSPB.DataBind();
            clearQty();

        }

        private void clearQty()
        {
            txtItemCode.Text = string.Empty;
            txtQtyPakai.Text = string.Empty;
            txtStok.Text = string.Empty;
            txtUom.Text = string.Empty;
            txtKeterangan.Text = string.Empty;
            txtUomID.Text = string.Empty;
            txtGroupID.Text = string.Empty;

            ddlItemName.SelectedIndex = 0;
        }

        protected void btnNew_ServerClick(object sender, EventArgs e)
        {
            Session.Remove("id");
            Session.Remove("ListOfPakaiDetail");
            Session.Remove("Pakai");
            Session.Remove("PakaiNo");
            txtTanggal.Enabled = true;
            clearForm();
        }

        protected void btnUpdate_ServerClick(object sender, EventArgs e)
        {
            int bln = DateTime.Parse(txtTanggal.Text).Month;
            int thn = DateTime.Parse(txtTanggal.Text).Year;
            int noBaru = 0;
            Timer2.Enabled = false;
            Panel2.Visible = false;
            string strValidate = ValidateText();
            if (strValidate != string.Empty)
            {
                DisplayAJAXMessage(this, strValidate);
                CheckReOrderPoint();
                return;
            }

            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";

            string strPakaiCode = kd + DateTime.Now.Date.Year.ToString().Substring(2, 2);
            #region Cek Tanggal terakhir input dan periode closing
            //cek periode closing
            AccClosingFacade cls = new AccClosingFacade();
            AccClosing clsBln = cls.RetrieveByStatus((DateTime.Parse(txtTanggal.Text).Month), (DateTime.Parse(txtTanggal.Text).Year));
            DateTime nowTgl = new DateTime(DateTime.Parse(txtTanggal.Text).Year, DateTime.Parse(txtTanggal.Text).Month, DateTime.Parse(txtTanggal.Text).Day);

            Pakai pakai = new Pakai();
            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai lastEntry = pakaiFacade.CekLastDate(strPakaiCode, int.Parse(ddlDeptName.SelectedValue));
            DateTime lastTgl = new DateTime(lastEntry.PakaiDate.Year, lastEntry.PakaiDate.Month, lastEntry.PakaiDate.Day);

            if (clsBln.Status == 1)
            {
                string mess = "Periode " + clsBln.Bulan.ToString() + " " + clsBln.Tahun.ToString() + " sudah di close by Accounting";
                DisplayAJAXMessage(this, mess);
                return;
            }
            else
            {
                if (nowTgl < lastTgl && PurchnConfig("SystemClosing") == 0)
                {
                    DisplayAJAXMessage(this, "Anda melewati Tanggal input terakhir");
                    int intApv = ((Users)Session["Users"]).Apv;
                    if (intApv == 0)
                        return;

                }
            }
            #endregion

            string strEvent = "Insert";
            pakai = new Pakai();

            if (Session["id"] != null)
            {
                #region nonaktif line
                //    schedule.ID = int.Parse(Session["id"].ToString());

                //    ScheduleDetailFacade sDetailFacade = new ScheduleDetailFacade();
                //    ArrayList arrDistinctSchedule = sDetailFacade.RetrieveDistinctById(int.Parse(Session["id"].ToString()));

                //    SuratJalanFacade suratJalanFacade = new SuratJalanFacade();
                //    SuratJalanTOFacade suratJalanTOFacade = new SuratJalanTOFacade();

                //    ScheduleFacade scheduleFacade = new ScheduleFacade();
                //    Schedule sch = scheduleFacade.RetrieveById(schedule.ID);
                //    if (scheduleFacade.Error == string.Empty)
                //    {
                //        if (sch.ScheduleDate.ToString("yyyyMMdd") != DateTime.Parse(txtScheduleDate.Text).ToString("yyyyMMdd"))
                //        {

                //            foreach (int[] documentId in arrDistinctSchedule)
                //            {
                //                if (documentId[1] == 0)
                //                {
                //                    int jumlahOP = suratJalanFacade.GetJumOPById(documentId[0], schedule.ID);
                //                    if (jumlahOP > 0)
                //                    {
                //                        DisplayAJAXMessage(this, "Cancel dulu surat Jalan OP");
                //                        return;
                //                    }
                //                }
                //                else
                //                {
                //                    int jumlahTO = suratJalanTOFacade.GetJumTOById(documentId[0], schedule.ID);
                //                    if (jumlahTO > 0)
                //                    {
                //                        DisplayAJAXMessage(this, "Cancel dulu surat Jalan TO");
                //                        return;
                //                    }
                //                }

                //            }
                //        }
                //    }

                strEvent = "Edit";
                #endregion
            }
            //here for document number
            company = new Company();
            companyFacade = new CompanyFacade();
            kd = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";

            PakaiDocNoFacade pakaiDocNoFacade = new PakaiDocNoFacade();
            PakaiDocNo pakaiDocNo = new PakaiDocNo();
            if (strEvent == "Insert")
            {
                //"K" for Karawang
                pakaiDocNo = pakaiDocNoFacade.RetrieveByPakaiCode(bln, thn, kd);
                if (pakaiDocNo.ID == 0)
                {
                    noBaru = 1;
                    pakaiDocNo.PakaiCode = kd;
                    pakaiDocNo.NoUrut = 1;
                    pakaiDocNo.MonthPeriod = bln;
                    pakaiDocNo.YearPeriod = thn;
                }
                else
                {
                    noBaru = pakaiDocNo.NoUrut + 1;
                    pakaiDocNo.PakaiCode = kd;
                    pakaiDocNo.NoUrut = pakaiDocNo.NoUrut + 1;
                }
            }

            pakai.PakaiNo = txtPakaiNo.Text;
            pakai.PakaiDate = DateTime.Parse(txtTanggal.Text);
            //1 for bahan baku
            pakai.PakaiTipe = 1;
            pakai.DeptID = int.Parse(ddlDeptName.SelectedValue);
            pakai.DepoID = ((Users)Session["Users"]).UnitKerjaID;
            pakai.Status = 0;
            pakai.AlasanCancel = "";
            pakai.CreatedBy = ((Users)Session["Users"]).UserName;
            pakai.ItemTypeID = 1;

            string strError = string.Empty;
            ArrayList arrPakaiDetail = new ArrayList();
            if (Session["ListOfPakaiDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                //cek stok ada gak 1x lagi
                foreach (PakaiDetail pkiDetail in arrPakaiDetail)
                {
                    InventoryFacade invFacade = new InventoryFacade();
                    Inventory inv = invFacade.RetrieveById(pkiDetail.ItemID);
                    if (invFacade.Error == string.Empty && inv.ID > 0)
                    {
                        if ((pkiDetail.KartuStock - pkiDetail.Quantity) < 0)
                        {
                            string strItemCode = inv.ItemCode;
                            string strMessage = "Kode Barang " + strItemCode + " tidak mencukupi stok-nya...!";
                            DisplayAJAXMessage(this, strMessage);

                            clearQty();
                            return;
                        }
                    }
                }
            }
            // until here

            PakaiProcessFacade pakaiProcessFacade = new PakaiProcessFacade(pakai, arrPakaiDetail, pakaiDocNo);
            if (pakai.ID > 0)
            {
                #region nonaktif line
                //ScheduleFacade scheduleFacade = new ScheduleFacade();
                //Schedule sc = scheduleFacade.RetrieveByNo(txtPONo.Text);
                //if (scheduleFacade.Error == string.Empty)
                //{
                //    if (sc.ID > 0)
                //    {
                //        receipt.Status = sc.Status;
                //        strError = receiptProcessFacade.Update();
                //        // blom di cek
                //    }
                //}
                #endregion
            }
            else
            {
                strError = pakaiProcessFacade.Insert();
                if (strError == string.Empty)
                {
                    txtPakaiNo.Text = pakaiProcessFacade.PakaiNo;
                    Session["id"] = pakai.ID;
                    Session["PakaiNo"] = pakaiProcessFacade.PakaiNo;
                }
            }

            if (strError == string.Empty)
            {
                ddlItemName.Items.Clear();
                ddlProdLine.Items.Clear();
                ddlProdLine.Enabled = false;
                InsertLog(strEvent);
                btnUpdate.Disabled = true;
                btnPrint.Disabled = true;
                if (strEvent == "Edit")
                    clearForm();

            }
            CheckReOrderPoint();
            LoadPakai(txtPakaiNo.Text);
        }

        private void InsertLog(string eventName)
        {
            EventLog eventLog = new EventLog();
            eventLog.ModulName = "Entry Pemakaian Bahan Baku";
            eventLog.EventName = eventName;
            eventLog.DocumentNo = txtPakaiNo.Text;
            eventLog.CreatedBy = ((Users)Session["Users"]).UserName;

            EventLogFacade eventLogFacade = new EventLogFacade();
            int intResult = eventLogFacade.Insert(eventLog);
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                //nanti utk biaya & asset agak beda kayaknya
                ArrayList arrSpb = (Session["ListOfPakaiDetail"] != null) ? (ArrayList)Session["ListOfPakaiDetail"] : new ArrayList();
                InventoryFacade invFacade = new InventoryFacade();
                txtTanggal.Enabled = false;
                Inventory inv = invFacade.RetrieveById(int.Parse(ddlItemName.SelectedValue));
                invFacade.TglPeriode = " AND YMD <= " + DateTime.Parse(txtTanggal.Text).ToString("yyyyMMdd");
                decimal StockAkhir = invFacade.GetStockAkhir(ddlItemName.SelectedValue, 1, DateTime.Parse(txtTanggal.Text).Month.ToString(), DateTime.Parse(txtTanggal.Text).Year.ToString());
                if (invFacade.Error == string.Empty && inv.ID > 0)
                {
                    txtItemCode.Text = inv.ItemCode;
                    txtUomID.Text = inv.UOMID.ToString();
                    txtUom.Text = inv.UOMDesc;
                    txtStok.Text = StockAkhir.ToString("N2");
                    txtGroupID.Text = inv.GroupID.ToString();
                    decimal totalSPB = 0;
                    foreach (PakaiDetail iv in arrSpb)
                    {
                        if (iv.ItemCode == inv.ItemCode) { totalSPB += iv.Quantity; }
                    }

                    txtStok.Text = (StockAkhir - totalSPB).ToString("N2");

                    txtQtyPakai.Focus();
                }
            }
        }

        protected void btnSearch_ServerClick(object sender, EventArgs e)
        {
            LoadPakai(txtSearch.Text);
        }

        private void LoadPakai(string strPakaiNo)
        {
            // here too
            Company company = new Company();
            CompanyFacade companyFacade = new CompanyFacade();
            string strPakaiTipe = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID) + "P";
            clearForm();

            PakaiFacade pakaiFacade = new PakaiFacade();
            Pakai pakai = pakaiFacade.RetrieveByNoWithStatus(strPakaiNo, strPakaiTipe);
            if (pakaiFacade.Error == string.Empty && pakai.ID > 0)
            {
                Session["id"] = pakai.ID;

                txtPakaiNo.Text = pakai.PakaiNo;
                txtKodeDept.Text = pakai.DeptCode;
                LoadDept();
                ddlDeptName.SelectedValue = pakai.DeptID.ToString();
                txtTanggal.Text = pakai.PakaiDate.ToString("dd-MMM-yyyy");
                if (pakai.Status == 1)
                {
                    txtStatus.Text = "Head";
                }
                else if (pakai.Status == 2)
                {
                    txtStatus.Text = "Manager";
                }
                else if (pakai.Status == 3)
                {
                    txtStatus.Text = "Gudang";
                }
                else if (pakai.Status == 0)
                {
                    txtStatus.Text = "Admin";
                }
                txtCreatedBy.Text = pakai.CreatedBy;
                ArrayList arrListPakaiDetail = new ArrayList();
                PakaiDetailFacade pakaiDetailFacade = new PakaiDetailFacade();
                ArrayList arrPakaiDetail = pakaiDetailFacade.RetrieveByPakaiId(pakai.ID);
                if (pakaiDetailFacade.Error == string.Empty)
                {
                    foreach (PakaiDetail pakaiDetail in arrPakaiDetail)
                    {
                        if (pakaiDetail.ID > 0)
                            arrListPakaiDetail.Add(pakaiDetail);
                    }
                }
                GridView1.Columns[6].Visible = (pakai.Status > 1) ? false : true;
                Session["Aprove"] = pakai.Status;
                Session["PakaiNo"] = pakai.PakaiNo;
                Session["ListOfPakaiDetail"] = arrListPakaiDetail;
                GridView1.DataSource = arrListPakaiDetail;
                GridView1.DataBind();
                lstSPB.DataSource = arrListPakaiDetail;
                lstSPB.DataBind();
                string arrLockField = "txtKodeDept,txtStatus,txtStok,txtTanggal,ddlDeptName,txtCreatedBy,txtPakaiNo";
                //sehingga tdk bisa add dept yg laen
                //ddlDeptName.Enabled = false;
                DisableField(false, arrLockField);

                string[] arrDpt = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UnLockDept", "SPB").Split(',');
                btnCancel.Enabled = (pakai.Status > 2) ? false : true;
                if (pakai.ID > 0 && pakai.Status > 1)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = false;
                    btnUnlok.Visible = (arrDpt.Contains(((Users)Session["Users"]).DeptID.ToString())) ? true : false;
                }
                else if (pakai.ID > 0 && pakai.Status == 0 || pakai.ID > 0 && pakai.Status == 1)
                {
                    lbAddOP.Enabled = false;
                    btnUpdate.Disabled = true;
                    btnPrint.Disabled = true;
                }
                else
                {
                    btnUnlok.Visible = false;
                }

            }
        }
        private void DisableField(bool status, string arrID)
        {
            string[] objID = arrID.Split(',');

            if (objID[0] == string.Empty)
            {
                foreach (object c in Div5.Controls)
                {
                    if (c is TextBox) { ((TextBox)c).Enabled = status; }
                    if (c is DropDownList) { ((DropDownList)c).Enabled = status; }
                }
            }
            else
            {
                for (int i = 0; i < objID.Count(); i++)
                {
                    if (Div5.FindControl(objID[i].ToString().TrimStart().TrimEnd()) is TextBox)
                    {
                        TextBox txt = (TextBox)Div5.FindControl(objID[i].ToString().TrimStart().TrimEnd());
                        if (txt != null) ((TextBox)txt).Enabled = status;
                    }

                }
                for (int x = 0; x < objID.Count(); x++)
                {
                    DropDownList ddl = new DropDownList();
                    if (Div5.FindControl(objID[x].ToString().TrimStart().TrimEnd()) is DropDownList)
                    {
                        ddl = (DropDownList)Div5.FindControl(objID[x].ToString().TrimStart().TrimEnd());
                        if (ddl != null) ((DropDownList)ddl).Enabled = status;
                    }
                }
            }

        }
        private void SelectDept(string strDeptname)
        {
            ddlDeptName.ClearSelection();
            foreach (ListItem item in ddlDeptName.Items)
            {
                if (item.Text == strDeptname)
                {
                    item.Selected = true;
                    return;
                }
            }
        }

        private string ValidateText()
        {
            ArrayList arrPakaiDetail = new ArrayList();
            if (Session["ListOfPakaiDetail"] != null)
            {
                arrPakaiDetail = (ArrayList)Session["ListOfPakaiDetail"];
                if (arrPakaiDetail.Count == 0)
                    return "Tidak ada List Item yang di-input";
            }

            return string.Empty;
        }

        private void CheckReOrderPoint()
        {
            int ROP = 0;
            InventoryFacade InventoryFacade = new InventoryFacade();
            ROP = InventoryFacade.CheckReorderPoint("1", ((Users)Session["Users"]).ID);
            if (ROP == 1 && ((Users)Session["Users"]).DeptID == 10)
            {
                Timer2.Enabled = true;
                Panel2.Visible = true;
            }
            else
            {
                Timer2.Enabled = false;
                Panel2.Visible = false;
                Timer1.Enabled = false;
            }
        }
        private void LoadListLockSPP()
        {
            //int intROP = 0;
            int strerror = 0;
            InventoryFacade InventoryFacade = new InventoryFacade();
            ROPFacade ropfacade = new ROPFacade();
            //intROP = InventoryFacade.CheckReorderPoint("1");
            ArrayList arrInventory = new ArrayList();
            arrInventory = InventoryFacade.ListReorderPoint(1);
            if (arrInventory.Count > 0)
            {
                foreach (Inventory Inv in arrInventory)
                {
                    strerror = ropfacade.InsertROP(Inv.ItemID, ((Users)Session["Users"]).ID);
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            //playsound();
            Timer2.Enabled = false;
            Panel2.Visible = false;
        }

        public static void playsound()
        {
            // Declare the first few notes of the song, "Mary Had A Little Lamb".
            Note[] Mary =
                {
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.GbelowC, Duration.QUARTER),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.B, Duration.HALF),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.A, Duration.QUARTER),
        new Note(Tone.A, Duration.HALF),
        new Note(Tone.B, Duration.QUARTER),
        new Note(Tone.D, Duration.QUARTER),
        new Note(Tone.D, Duration.HALF)
        };
            // Play the song
            Play(Mary);
        }

        // Play the notes in a song.
        protected static void Play(Note[] tune)
        {
            foreach (Note n in tune)
            {
                if (n.NoteTone == Tone.REST)
                    Thread.Sleep((int)n.NoteDuration);
                else
                    Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
            }
        }

        // Define the frequencies of notes in an octave, as well as 
        // silence (rest).
        protected enum Tone
        {
            REST = 0,
            GbelowC = 196,
            A = 220,
            Asharp = 233,
            B = 247,
            C = 262,
            Csharp = 277,
            D = 294,
            Dsharp = 311,
            E = 330,
            F = 349,
            Fsharp = 370,
            G = 392,
            Gsharp = 415,
        }

        // Define the duration of a note in units of milliseconds.
        protected enum Duration
        {
            WHOLE = 1600,
            HALF = WHOLE / 2,
            QUARTER = HALF / 2,
            EIGHTH = QUARTER / 2,
            SIXTEENTH = EIGHTH / 2,
        }

        // Define a note as a frequency (tone) and the amount of 
        // time (duration) the note plays.
        protected struct Note
        {
            Tone toneVal;
            Duration durVal;

            // Define a constructor to create a specific note.
            public Note(Tone frequency, Duration time)
            {
                toneVal = frequency;
                durVal = time;
            }

            // Define properties to return the note's tone and duration.
            public Tone NoteTone { get { return toneVal; } }
            public Duration NoteDuration { get { return durVal; } }
        }
        protected void Timer2_Tick(object sender, EventArgs e)
        {
            if (Panel2.BackColor == System.Drawing.Color.White)
            {
                Panel2.BackColor = System.Drawing.Color.Red;
                Console.Beep();
            }
            else
            {
                Panel2.BackColor = System.Drawing.Color.White;
                Console.Beep();
            }
        }
        public int PurchnConfig(string ModulName)
        {
            POPurchnFacade purchn = new POPurchnFacade();
            POPurchn status = purchn.PurchnTools(ModulName);
            return status.Status;
        }
        protected void hps_TextChanged(object sender, EventArgs e)
        {
            if (hps.Text == "no")
            {
                DisplayAJAXMessage(this, "orasido");
            }
            else
            {
                DisplayAJAXMessage(this, "sido");
            }
        }
        public void testajax()
        {
            hpuse.Value = "test aja";
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void lstSPB_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Hapus")
            {
                if (txtPakaiNo.Text != string.Empty)
                {
                    int ID = int.Parse(e.CommandArgument.ToString());
                    PakaiDetail pkd = new PakaiDetail();
                    if (Session["AlasanBatal"] != null)
                    {
                        if (Session["AlasanBatal"].ToString() != string.Empty)
                        {
                            pkd.ID = ID;
                            pkd.LastModifiedBy = ((Users)Session["Users"]).UserName;
                            pkd.RowStatus = -2;
                            //int result = new PakaiDetailFacade().Delete(pkd);
                            EventLog evn = new EventLog();
                            evn.CreatedBy = ((Users)Session["Users"]).UserName;
                            evn.DocumentNo = txtPakaiNo.Text;
                            evn.EventName = "Delete SPB Detail ID :" + ID.ToString();
                            evn.ModulName = "Pemakaian BB";
                            int rst = new EventLogFacade().Insert(evn);

                        }
                        else
                        {
                            DisplayAJAXMessage(this, "Alasan hapus tidak boleh kosong");
                            return;
                        }
                    }
                }
                else
                {

                }
            }
        }
        protected void lstSPB_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lbl = (Label)e.Item.FindControl("Act");
                Image img = (Image)e.Item.FindControl("bntHapus");
                if (img != null)
                {
                    int apv = (Session["Aprove"] != null) ? (int)Session["Aprove"] : 0;
                    if (apv == 2)
                    {
                        img.Visible = false;
                    }
                    else
                    {
                        img.Visible = true;
                    }
                }
            }
        }
        protected void btnUnlock_ServerClick(object sender, EventArgs e)
        {
            int ID = int.Parse(Session["id"].ToString());
            Pakai objPakai = new Pakai();
            PakaiFacade pakai = new PakaiFacade();
            /** check closing periode */
            int bln = Convert.ToDateTime(txtTanggal.Text).Month;
            int Year = Convert.ToDateTime(txtTanggal.Text).Year;
            int BlnSkr = DateTime.Now.Month;
            int ThnSkr = DateTime.Now.Year;
            if (bln == BlnSkr && Year == ThnSkr)
            {
                int Result = pakai.UpdateRelease(ID, 1);//unlock status
                if (Result == 1)
                {
                    EventLog evn = new EventLog();
                    evn.EventName = "Un Approve SPB " + txtPakaiNo.Text;
                    evn.ModulName = "SPB Bahan Baku";
                    evn.DocumentNo = txtPakaiNo.Text;
                    evn.CreatedBy = ((Users)Session["Users"]).UserName;
                    int rst = new EventLogFacade().Insert(evn);
                    DisplayAJAXMessage(this, "Un Aproved document berhasil");
                    Response.Redirect(Request.RawUrl);
                }
            }
            else
            {
                DisplayAJAXMessage(this, "SPB Bulan " + Global.nBulan(bln) + " " + Year.ToString() + " tidak bisa di unlock");
            }
        }
        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtKeterangan.Text = ddlShift.SelectedValue;
        }

    }
}