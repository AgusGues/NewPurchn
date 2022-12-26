using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Domain;
using BusinessFacade;
using System.Data.SqlClient;

namespace GRCweb1.ModalDialog
{
    public partial class POKertas0 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string[] data = Request.QueryString["d"].ToString().Split('-');
                OpenSPP(data);
            }
        }

        private void OpenSPP(string[] data)
        {
            ArrayList arrData = LoadOpenSPP(data[0].ToString(), data[1].ToString());
            ddlNoSPP.Items.Clear();
            ddlNoSPP.Items.Add(new ListItem("--Pilih SPP--", "0"));
            foreach (DeliveryKertas d in arrData)
            {
                ddlNoSPP.Items.Add(new ListItem(d.NOSPP.ToString(), d.ID.ToString()));
            }
        }
        protected void ddlNoSPP_Change(object sender, EventArgs e)
        {
            string[] data = Request.QueryString["d"].ToString().Split('-');
            ArrayList arrData = LoadOpenSPP(data[0].ToString(), data[1].ToString());
            ddlItemSPP.Items.Clear();
            ddlItemSPP.Items.Add(new ListItem("--Pilih SPP--", "0"));
            foreach (DeliveryKertas d in arrData)
            {
                ddlItemSPP.Items.Add(new ListItem(d.ItemName, d.ItemID.ToString()));
            }
            ddlItemSPP.SelectedIndex = 1;
            LoadMataUang();
            LoadTermOfPay();
            LoadSupplierData(data);
        }
        private void LoadSupplierData(string[] data)
        {
            string thn = DateTime.Now.Year.ToString();
            string bln = (DateTime.Now.Month.ToString().Length == 1) ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
            string thnbln = thn + bln;

            DeliveryKertas d = new DeliveryKertas();
            SuppPurchFacade sp = new SuppPurchFacade();
            SuppPurch s = new SuppPurch();
            Inventory iv = new Inventory();
            InventoryFacade ivd = new InventoryFacade();
            UOM u = new UOM();
            UOMFacade um = new UOMFacade();
            QAKadarAir ka = new QAKadarAir();
            DepoKertas dp = new DepoKertas();
            DepoKertasKA dpa = new DepoKertasKA();
            Users user = (Users)Session["Users"];
            string nmTable = "";
            switch (data[1])
            {
                case "1":
                    //deliverykertas
                    nmTable = " AND ID=" + data[0];
                    d = dp.Retrieve0(nmTable, true);
                    s = sp.RetrieveById(d.SupplierID);
                    iv = ivd.RetrieveByCode(d.ItemCode);
                    u = um.RetrieveByID(iv.UOMID);
                    ka = KAPlant("AND NoSJ='" + d.NoSJ + "' AND NOPOL='" + d.NOPOL + "' AND PlantID=" + user.UnitKerjaID, true);

                    break;
                case "2":
                    //deliverykertaska
                    nmTable = " AND ID=" + data[0];
                    d = dpa.Retrieve0(nmTable, true);
                    s = sp.RetrieveById(d.SupplierID);
                    iv = ivd.RetrieveByCode(d.ItemCode);
                    u = um.RetrieveByID(iv.UOMID);
                    string where = "AND GrossPlant='" + d.GrossPlant.ToString("###") + "' AND NOPOL='" + d.NOPOL + "' AND PlantID=" + user.UnitKerjaID +
                                 "AND (POKAID IS NULL OR POKAID=0) AND NettPlant='" + d.NettPlant.ToString("###") + "'";
                    ka = KAPlant(where, true);
                    break;
            }
            //if (s.SupplierName.Substring(0, 4) == "TEAM")
            //{
            //    nmTable = " AND ID=" + data[0];
            //    d = dpa.Retrieve(nmTable, true);
            //    s = sp.RetrieveById(d.SupplierID);
            //    iv = ivd.RetrieveByCode(d.ItemCode);
            //    u = um.RetrieveByID(iv.UOMID);
            //    string where = "AND GrossPlant='" + d.GrossPlant.ToString("###") + "' AND NOPOL='" + d.NOPOL + "' AND PlantID=" + user.UnitKerjaID +
            //                 "AND (POKAID IS NULL OR POKAID=0) ";
            //    ka = KAPlant(where, true);
            //}

            txtNamaSupplier.Text = s.SupplierName;
            txtKodeSupplier.Text = s.SupplierCode;
            txttelpSupplier.Text = s.Telepon.ToString();
            txtFaxSupplier.Text = s.Fax;
            txtUpSupplier.Text = s.UP;
            txtItemName.Text = iv.ItemName.ToString();
            txtSupplierID.Value = s.ID.ToString();
            txtSatuan.Text = u.UOMCode;
            txtUomID.Value = u.ID.ToString();
            ddlMataUang.SelectedValue = "1";
            txtJumlah.Text = (data[1] == "1") ? d.NettPlant0.ToString("N2") : d.NettPlant0.ToString("N2");
            if (s.SupplierName.Substring(0, 4) == "TEAM")
                txtJumlah.Text = GetJumlahBBPlant(d.NoSJ, user.UnitKerjaID).ToString();
            txtTglPO.Text = DateTime.Parse(ka.TglCheck.ToString()).ToString("dd-MMM-yyyy");
            txtDliveryDate.Text = ka.TglCheck.ToString("dd-MMM-yyyy");
            ddlTermOfPay.SelectedIndex = 0;
            for (int i = 0; i < ddlTermOfPay.Items.Count; i++)
            {
                ddlTermOfPay.SelectedIndex = i;
                if (ddlTermOfPay.SelectedItem.Text.Trim().ToUpper() == "CASH")
                    break;
            }
            ddlTermOfPay.Items.FindByText("Cash");
            txtTOD.Text = "FRANCO ";

            /** PPN 11% added 01 April 2022 oleh : Beny **/
            if (Convert.ToInt32(thnbln) > 202203)
            {
                txtPPN.Text = (s.PKP.ToUpper().Trim() == "YES") ? "11" : "0";
            }
            else
            {
                txtPPN.Text = (s.PKP.ToUpper().Trim() == "YES") ? "10" : "0";
            }
            //txtPPN.Text = (s.PKP.ToUpper().Trim() == "YES") ? "10" : "0";

            txtPPH.Text = "0";
            txtTotalPrice.Text = "0";
            txtHarga.Text = "0";
            txtOngkosKirim.Text = "0";
            txtDKID.Value = d.ID.ToString();
            txtDKAID.Value = ka.ID.ToString();
            txtSubCompanyID.Value = s.SubCompanyID.ToString();
            ListGrid(d, ka);
        }
        protected decimal GetJumlahBBPlant(string nosj, int plantid)
        {
            decimal  jml = 0;
            ZetroView zw = new ZetroView();
            zw.QueryType = Operation.CUSTOM;
            zw.CustomQuery = "select NettPlant0  NettPlant from deliverykertaska where rowstatus>-1 and nosj='" + nosj + "' and plantid=" + plantid;
            SqlDataReader sdr = zw.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    jml = decimal.Parse(sdr["NettPlant"].ToString());
                }
            }
            return jml;

        }
        protected void btnSimpan_CLick(object sender, EventArgs e)
        {
            #region Header PO
            Session["Result"] = null;
            string[] data = Request.QueryString["d"].ToString().Split('-');
            POPurchn p = new POPurchn();
            POPurchnFacade poHeader = new POPurchnFacade();
            p.POPurchnDate = DateTime.Parse(txtTglPO.Text);
            p.SupplierID = int.Parse(txtSupplierID.Value);
            p.Termin = ddlTermOfPay.SelectedItem.Text;
            p.PPN = decimal.Parse(txtPPN.Text);
            p.PPH = decimal.Parse(txtPPH.Text);
            p.Crc = int.Parse(ddlMataUang.SelectedValue);
            p.Remark = txtKeterangan.Text;
            p.Cetak = 0;
            p.CountPrt = 0;
            p.NilaiKurs = 0;
            p.Status = 0;
            p.UangMuka = 0;
            p.Ongkos = 0;
            p.Approval = ((Users)Session["Users"]).Apv;
            p.CreatedTime = DateTime.Now;
            p.CreatedBy = ((Users)Session["Users"]).UserName;
            //p.SubCompanyID = int.Parse(txtSubCompanyID.Value);
            p.Delivery = txtTOD.Text;
            p.PaymentType = (int.Parse(ddlTermOfPay.SelectedValue) == 6) ? 0 : 1;
            p.NoPO = this.NomorPO();
            int result = poHeader.InsertHeaderPO(p);
            #endregion

            #region DetailPO
            if (result > 0)
            {
                //update sub companyID
                POPurchn ps = new POPurchn();

                ps.ID = result;
                ps.SubCompanyID = int.Parse(txtSubCompanyID.Value);
                int rst = new POPurchnFacade().UpdateSubCompanyID(ps);
                //insert popurchndetail
                SPPDetail spd = new SPPDetailFacade().RetrieveBySPPDetailID(int.Parse(ddlNoSPP.SelectedValue), true);
                POPurchnDetail pd = new POPurchnDetail();
                pd.POID = result;
                pd.DocumentNo = ddlNoSPP.SelectedItem.Text;
                pd.SPPID = int.Parse(ddlNoSPP.SelectedValue);
                pd.SPPDetailID = spd.ID;
                pd.GroupID = 1;
                pd.ItemID = int.Parse(ddlItemSPP.SelectedValue);
                pd.Price = decimal.Parse(txtHarga.Text);
                pd.Qty = decimal.Parse(txtJumlah.Text);
                pd.ItemTypeID = 1;
                pd.UOMID = int.Parse(txtUomID.Value);
                pd.NoUrut = 0;
                pd.Status = 0;
                pd.DlvDate = DateTime.Parse(txtDliveryDate.Text);
                pd.LastModifiedBy = ((Users)Session["Users"]).UserName;
                pd.LastModifiedTime = DateTime.Now;
                pd.Price2 = HargaKhususKertas(int.Parse(txtSupplierID.Value), int.Parse(ddlItemSPP.SelectedValue));
                int rest = poHeader.InsertDetailPO(pd);
                if (rest > 0)
                {
                    //update SPP
                    int resulte = 0;
                    SPPDetail objSPP = new SPPDetail();
                    objSPP.ID = spd.ID;
                    objSPP.QtyPO = decimal.Parse(txtJumlah.Text);
                    SPPDetailFacade spds = new SPPDetailFacade();
                    resulte = spds.UpdateSPPDetail(objSPP, true);
                    DeliveryKertas dk = new DeliveryKertas();
                    DepoKertas da = new DepoKertas();
                    //Check LastID for SJ more then 1 times
                    int ID = int.Parse(data[2].ToString());
                    int Jml = int.Parse(data[3].ToString());
                    decimal QtyPO = new POPurchnDetailFacade().CheckQtyPO(spd.ID.ToString());
                    SPPDetail spp = new SPPDetailFacade().RetrieveBySPPDetailID(spd.ID);
                    //if ((QtyPO + objSPP.QtyPO) > spp.Quantity)
                    //{
                    //    DisplayAJAXMessage(this, "Quantity PO sudah melebihi Qty SPP, silahkan Check Lagi");
                    //    return;
                    //}
                    //update DeliveryKertas
                    dk.POKAID = result;
                    dk.ID = int.Parse(txtDKID.Value);
                    dk.LastModifiedBy = ((Users)Session["Users"]).UserName;
                    dk.LastModifiedTime = DateTime.Now;
                    resulte += da.Update(dk);
                    //update DeliveryKertasKA
                    QAKadarAir ka = new QAKadarAir();
                    ka.POKAID = result;
                    ka.ID = int.Parse(txtDKAID.Value);
                    ka.LastModifiedTime = DateTime.Now;
                    ka.LastModifiedBy = ((Users)Session["Users"]).UserName.ToString();

                    if (ID == int.Parse(txtDKID.Value) && Jml > 1)
                    {
                        resulte += da.Update(ka, true);
                    }
                    if (int.Parse(data[1].ToString()) == 2)
                    {
                        resulte += da.Update(ka, true);
                    }
                    //insert POPurchnKadarAIR
                    POPurchn ppx = this.LoadDataKA(data, rest);
                    resulte += this.InsertPOKadarAir(ppx);
                    if (resulte > 0)
                    {
                        txtNoPO.Text = p.NoPO;
                        Session["Result"] = p.NoPO.ToString() + "," + pd.DocumentNo.ToString();
                        btnSimpan.Enabled = false;
                        //Response.Write("<script language='javascript'>window.close();</script>");
                    }
                    else
                    {

                    }
                }
                else
                {
                    //rolback detail dan header
                    DisplayAJAXMessage(this, poHeader.Error.ToString());
                    //this.DataRollBack(result, "POPurchn");
                    return;
                }
            }
            else
            {
                //rollback header;
                DisplayAJAXMessage(this, poHeader.Error.ToString());
                return;
            }
            #endregion
        }
        private decimal HargaKhususKertas(int SupplierID, int ItemID)
        {
            POPurchnFacade po = new POPurchnFacade();
            decimal harga = po.GetHargaKertas(SupplierID, ItemID);
            return harga;
        }
        private POPurchn LoadDataKA(string[] data, int PODetailID)
        {
            POPurchn pp = new POPurchn();
            QAKadarAir ka = new QAKadarAir();
            DepoKertas dp = new DepoKertas();
            DepoKertasKA dpa = new DepoKertasKA();
            DeliveryKertas d = new DeliveryKertas();
            Inventory iv = new Inventory();
            InventoryFacade ivd = new InventoryFacade();
            switch (data[1])
            {
                case "1":
                    d = dp.Retrieve("AND ID=" + data[0], true);
                    iv = ivd.RetrieveByCode(d.ItemCode.ToString());
                    break;
                case "2":
                    ka = dpa.Retrieve("AND ID=" + data[0], true);
                    iv = ivd.RetrieveByCode(ka.ItemCode.ToString());
                    break;
            }
            pp.StdKA = (data[1] == "1") ? d.StdKA : ka.StdKA;
            pp.Gross = (data[1] == "1") ? d.GrossDepo : ka.GrossPlant;
            pp.Netto = (data[1] == "1") ? d.NettDepo : ka.NettPlant;
            pp.AktualKA = (data[1] == "1") ? d.KADepo : ka.AvgKA;
            pp.POID = (data[1] == "1") ? d.POKAID : ka.POKAID;
            pp.ItemID = iv.ID;
            pp.PODetailID = PODetailID;
            pp.CreatedBy = ((Users)Session["Users"]).UserName;
            pp.CreatedTime = DateTime.Now;
            pp.NoPol = (data[1] == "1") ? d.NOPOL.ToUpper() : ka.NOPOL.ToUpper();
            pp.Sampah = (data[1] == "1") ? d.Sampah : ka.Sampah;
            pp.SchID = (data[1] == "1") ? d.ID : ka.ID;
            pp.SchNo = (data[1] == "1") ? d.NoSJ : ka.NoSJ;
            return pp;
        }
        private string NomorPO()
        {
            #region proses nomor PO
            string NoPO = "";
            Users users = (Users)Session["Users"];
            CompanyFacade companyFacade = new CompanyFacade();
            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            string kd = companyFacade.GetKodeCompany(users.UnitKerjaID) + groupsPurchnFacade.GetKodeSPP(1);
            //3 nomor PO
            string kdnew = string.Empty;

            kdnew = companyFacade.GetKodeCompany(users.UnitKerjaID) + "I";


            SPPNumber sPPNumber = new SPPNumber();
            SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
            sPPNumber = sPPNumberFacade.RetrieveByGroupsID(1);
            if (sPPNumberFacade.Error == string.Empty)
            {
                if (sPPNumber.ID > 0)
                {
                    //otomatis reset counter jika tahun baru
                    //added on 02-01-2016
                    if (sPPNumber.LastModifiedTime.Year == DateTime.Parse(txtTglPO.Text).Year)
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
                //SPPNumberFacade sPPNumberFacade = new SPPNumberFacade();
                int intResult = sPPNumberFacade.Update(sPPNumber);
                if (intResult > 0)
                {
                    string th = DateTime.Parse(txtTglPO.Text).Year.ToString().Substring(2, 2).PadLeft(2, '0');
                    string bl = DateTime.Parse(txtTglPO.Text).Month.ToString().PadLeft(2, '0');
                    NoPO = sPPNumber.KodeCompany + sPPNumber.KodeSPP + th + bl + "-" + sPPNumber.POCounter.ToString().PadLeft(5, '0');
                }
                else
                {
                    DisplayAJAXMessage(this, sPPNumberFacade.Error);

                }
            }
            return NoPO;
            #endregion
        }
        private ArrayList LoadOpenSPP(string ID, string table)
        {
            string nmTable = "";
            QAKadarAir dk = new QAKadarAir();
            DeliveryKertas d = new DeliveryKertas();
            DepoKertas dp = new DepoKertas();
            DepoKertasKA dpa = new DepoKertasKA();
            string ItemCode = "";
            switch (table)
            {
                case "1"://depo
                    nmTable = " AND ID=" + ID;
                    d = dp.Retrieve(nmTable, true);
                    ItemCode = d.ItemCode.ToString();
                    break;
                case "2"://local
                    nmTable = " AND ID=" + ID;
                    dk = dpa.Retrieve(nmTable, true);
                    ItemCode = dk.ItemCode.ToString();
                    break;
            }
            ArrayList arrData = dp.OpenSPP(ItemCode);
            return arrData;
        }
        private void ListGrid(DeliveryKertas d, QAKadarAir k)
        {
            HtmlTableRow trPlant = (HtmlTableRow)lstKAT.FindControl("plant");
            HtmlTableRow trDepo = (HtmlTableRow)lstKAT.FindControl("depo");
            trPlant.Cells[1].InnerHtml = (k.NoSJ == "0") ? k.DocNo : k.NoSJ;
            trPlant.Cells[2].InnerHtml = k.TglCheck.ToString("dd-MMM-yyyy");
            trPlant.Cells[3].InnerHtml = k.NOPOL;
            trPlant.Cells[4].InnerHtml = k.GrossPlant.ToString("N0");
            trPlant.Cells[5].InnerHtml = k.NettPlant.ToString("N0");
            trPlant.Cells[6].InnerHtml = k.AvgKA.ToString("N2");
            trPlant.Cells[7].InnerHtml = k.Sampah.ToString("N2");
            trPlant.Cells[8].InnerHtml = k.StdKA.ToString("N2");
            trPlant.Cells[9].InnerHtml = k.JmlBAL.ToString("N0");
            //trPlant.Cells[0].Attributes.Add("title", "Click for preview Kadar Air");
            //trPlant.Cells[0].Attributes.Add("onclick", "redirectToCart('" + k.DocNo + "'");
            if (d.DepoID > 0)
            {
                trDepo.Cells[0].InnerHtml = d.DepoName;
                trDepo.Cells[1].InnerHtml = d.NoSJ;
                trDepo.Cells[2].InnerHtml = d.TglKirim.ToString("dd-MMM-yyyy");
                trDepo.Cells[3].InnerHtml = d.NOPOL;
                trDepo.Cells[4].InnerHtml = d.GrossDepo.ToString("N0");
                trDepo.Cells[5].InnerHtml = d.NettDepo.ToString("N0");
                trDepo.Cells[6].InnerHtml = d.KADepo.ToString("N2");
                trDepo.Cells[7].InnerHtml = d.Sampah.ToString("N2");
                trDepo.Cells[8].InnerHtml = d.StdKA.ToString("N2");
                trDepo.Cells[9].InnerHtml = d.JmlBAL.ToString("N0");
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    trDepo.Cells[i].InnerHtml = "";
                }
            }
        }
        private DeliveryKertas KADepo(string ID)
        {
            DepoKertas d = new DepoKertas();
            DeliveryKertas dk = new DeliveryKertas();
            dk = d.Retrieve(" AND ID=" + ID, true);
            return dk;

        }
        private QAKadarAir KAPlant(string ID)
        {
            DepoKertasKA d = new DepoKertasKA();
            QAKadarAir dk = new QAKadarAir();
            dk = d.Retrieve(" AND ID=" + ID, true);
            return dk;
        }
        private QAKadarAir KAPlant(string Criteria, bool KAPlant)
        {
            DepoKertasKA d = new DepoKertasKA();
            QAKadarAir dk = new QAKadarAir();
            dk = d.Retrieve0(Criteria, true);
            return dk;
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
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        //private int DataRollBack(int ID, string TableName)
        //{
        //    DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
        //    //da.ConnectionString = Global.ConnectionString();
        //    //da.TableName = TableName;
        //    return da.RollBackData(ID);
        //}
        private int InsertPOKadarAir(POPurchn p)
        {
            ZetroLib zl = new ZetroLib();
            int result = 0;
            zl.hlp = new POPurchn();
            zl.Criteria = "StdKA,Gross,ItemID,AktualKA,NoPol,Netto,POID,PODetailID,Sampah,RowStatus,SchID,SchNo,CreatedBy,CreatedTime";
            zl.Option = "Insert";
            zl.TableName = "POPurchnKadarAir";
            zl.StoreProcedurName = "spPOPurchnKadarAir2_0_insert";
            string rst = zl.CreateProcedure();
            if (rst == string.Empty)
            {
                zl.hlp = p;
                result = zl.ProcessData();
            }
            return result;
        }

    }
}