using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Domain;
using BusinessFacade;
using DataAccessLayer;
using System.Net.Mail;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ApprovalPONew2 : System.Web.UI.Page
    {
        public string[] POStatus = new string[] { "OPEN", "Approval Head", "Approval Manager" };
        public string OpenPO { get; set; }
        public decimal gHarga = 0;
        public decimal gPPN = 0;
        public decimal gDisc = 0;
        public decimal nDisc = 0;
        public decimal gTotal = 0;
        public string ItemCD = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = ((Users)Session["Users"]);
                if ((users.DeptID == 15 || (users.DeptID == 11 && users.TypeUnitKerja == 1)) && users.Apv > 0)
                {
                    LoadOpenPO();
                }
                if (Request.QueryString["PONo"] != null)
                {
                    LoadOpenPO(Request.QueryString["PONo"].ToString());
                }
                else if (noPO.Value != string.Empty)
                {
                    string[] ListOpenPO = noPO.Value.Split(',');
                    //btnApprove.Text += " " + ListOpenPO.Count().ToString();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    //LoadMUID();
                    LoadOpenPO(ListOpenPO[idx].ToString());
                    ViewState["index"] = idx;
                }
                string[] ListOpenPOx = noPO.Value.Split(',');
                int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
                btnNext.Enabled = ((idxx) == ListOpenPOx.Count()) ? false : true;
                txtNotApproved.Attributes.Add("onkeyup", "onKeyUp()");
                btnCancel.Enabled = (users.Apv < 1) ? false : true;
                btnCancel.Enabled = (users.Apv < 1 && users.DeptID == 15) ? false : btnCancel.Enabled;
                btnCancel.Enabled = (users.DeptID != 15) ? false : btnCancel.Enabled;
            }
        }
        private int GetGroup(string NoPO)
        {
            int intresult = 0;
            ZetroView zl = new ZetroView();
            zl.QueryType = Operation.CUSTOM;
            zl.CustomQuery = "select count(id)jml from terminbayar where poid in (select ID from popurchn where nopo='" + NoPO + "') ";
            SqlDataReader sdr = zl.Retrieve();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    intresult = Int32.Parse(sdr["jml"].ToString());
                }
            }
            return intresult;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private void loadDynamicGrid(string NoPO)
        {
            Users users = (Users)Session["Users"];
            string strSQL = string.Empty;
            strSQL = "select TerminKe,Persentase,TotalBayar from TerminBayar where rowstatus >-1 and POID in (select id from POPurchn where NoPO='" + NoPO + "') order by terminke";
            SqlConnection sqlCon = new SqlConnection(Global.ConnectionString());
            sqlCon.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, sqlCon);
            da.SelectCommand.CommandTimeout = 0;
            #region Code for preparing the DataTable
            DataTable dt = new DataTable();
            da.Fill(dt);
            DataColumn dcol = new DataColumn(ID, typeof(System.Int32));
            dcol.AutoIncrement = true;
            #endregion
            GrdDynamic.Columns.Clear();

            foreach (DataColumn col in dt.Columns)
            {
                BoundField bfield = new BoundField();

                bfield.DataField = col.ColumnName;
                bfield.HeaderText = col.ColumnName;
                if (col.ColumnName.ToUpper() == "TOTALBAYAR")
                {
                    bfield.DataFormatString = "{0:N0}";
                }
                //else
                //{
                //    bfield.DataFormatString = "{0:N0}";
                //    bfield.HeaderText = col.ColumnName;
                //}
                //if (col.ColumnName.Substring(0, 2) == "M3")
                //{
                //    bfield.HeaderText = "M3";
                //    bfield.DataFormatString = formdeci;
                //}
                GrdDynamic.Columns.Add(bfield);
            }
            GrdDynamic.DataSource = dt;
            GrdDynamic.DataBind();

        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            LoadOpenPO();
            string[] ListOpenPO = noPO.Value.Split(',');
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            idx = (idx < 0) ? 0 : idx;
            LoadOpenPO(ListOpenPO[idx].ToString());
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenPO.Count()) ? false : true;
            ViewState["index"] = idx;
            if (ListOpenPO.Count() == 0) Response.Redirect("ApprovalPONew2.aspx");
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            LoadOpenPO();
            string[] ListOpenPO = noPO.Value.Split(',');
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            btnNext.Enabled = ((idx) == ListOpenPO.Count()) ? false : true;
            ViewState["index"] = idx;
            btnPrev.Enabled = (idx > 0) ? true : false;
            try
            {
                //btnNext.Text += " " + idx.ToString();
                LoadOpenPO(ListOpenPO[idx].ToString());
                LoadOpenPO();
            }
            catch
            {
                ViewState["index"] = 0;
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                //LoadOpenPO(ListOpenPO[0].ToString());
            }
        }
        private void LoadOpenPO()
        {
            Users users = (Users)Session["Users"];
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            POApprovalFacade poApp = new POApprovalFacade();
            ArrayList arrPOPurchn = new ArrayList();
            /**
             * Pemisahan List Open PO yang muncul base on userid
             * Pengaturan group approval ada tabel usersapproval
             */
            string AppGroup = pOPurchnFacade.GetAppGroup(users.ID);
            int Apv = (users.Apv > 2) ? 2 : users.Apv;
            poApp.Criteria = " and (AlasanNotApproval='' or AlasanNotApproval is null) and Approval =" + (Apv - 1);
            poApp.Criteria += (AppGroup != string.Empty) ? " and Pod.GroupID in(" + AppGroup + ")" : string.Empty;
            poApp.OrderBy = (users.Apv > 2) ? "Order By ID Desc" : "Order By PO.NoPO,ID ";
            arrPOPurchn = poApp.RetrieveOpenPO();
            noPO.Value = "";
            foreach (POPurchn po in arrPOPurchn)
            {
                noPO.Value += po.NoPO + ",";
            }
            noPO.Value = (noPO.Value != string.Empty) ? noPO.Value.Substring(0, (noPO.Value.Length - 1)) : "0";
        }
        protected void txtNotApproved_Change(object sender, EventArgs e)
        {
            btnApprove.Enabled = (txtNotApproved.Text.Length > 0) ? false : true;
            btnNotApprove.Enabled = (txtNotApproved.Text.Length > 0) ? true : false;
        }
        private void LoadOpenPO(string NoPO)
        {
            Users users = (Users)Session["Users"];
            POApprovalFacade poApp = new POApprovalFacade();
            poApp.Criteria = " and NoPO='" + NoPO + "'";
            poApp.Criteria += " and (AlasanNotApproval='' or AlasanNotApproval is null)";
            ArrayList arrPO = poApp.RetrieveOpenPO();
            if (txtCari.Text != "Find by Nomor PO" && arrPO.Count == 0)
            {
                DisplayAJAXMessage(this, "Nomor PO tidak ada / Nomor PO Salah / Nomor PO Sudah di approve");
                return;
            }
            foreach (POPurchn po in arrPO)
            {
                if (po.Approval == users.Apv || po.Approval < 0)
                {
                    if (txtCari.Text == po.NoPO)
                    {
                        DisplayAJAXMessage(this, "PO Sudah di Approved");
                        return;
                    }
                    AutoNext();
                }
                LoadMUID();
                decimal TotalHarga = poApp.TotalPricePO(po.ID.ToString());
                decimal dis = (TotalHarga * (po.Disc / 100));
                decimal ppn = ((TotalHarga - dis) * (po.PPN / 100));
                txtNoPO.Text = NoPO;
                txtTglPO.Text = po.POPurchnDate.ToString("dd-MMM-yyyy");
                txtSupplier.Text = po.SupplierName.ToString();
                txtUpSupplier.Text = po.UP;
                txtTOD.Text = po.Delivery;
                txtTOP.Text = po.Termin;
                txtPPN.Text = po.PPN.ToString();
                txtPPH.Text = po.PPH.ToString();
                ddlMUID.SelectedValue = po.Crc.ToString();
                txtStatus.Text = POStatus[po.Approval].ToString();
                txtDiscount.Text = po.Disc.ToString();
                txtRemarks.Text = po.Remark.ToString();
                txtTotalPrice.Text = (TotalHarga - dis + ppn).ToString("###,##0.#0");
                txtOngkosKirim.Text = po.Ongkos.ToString();
                txtUangMuka.Text = po.UangMuka.ToString();
                txtNotApproved.Text = po.AlasanNotApproval.ToString();
                txtKurs.Text = (po.Crc > 1) ? "Nilai Kurs : " + po.NilaiKurs.ToString("###,##0.##") : "";
                txtFrom.Text = (po.ItemFrom == 0) ? "Barang : Local" : "Barang : Import";
                LoadDetailPO(po.ID);
                if (GetGroup(NoPO) > 0)
                {
                    Panel1.Visible = true;
                    loadDynamicGrid(NoPO);
                }
                else
                {
                    Panel1.Visible = false;
                }
            }
        }
        private void LoadDetailPO(int POID)
        {
            MataUangFacade muf = new MataUangFacade();
            POPurchnDetailFacade pod = new POPurchnDetailFacade();
            pod.Where = " and A.Status>-1";
            ArrayList arrData = pod.RetrieveItemPOID(POID);
            lstItemPO.DataSource = arrData;
            lstItemPO.DataBind();
        }
        private void LoadMUID()
        {
            ddlMUID.Items.Clear();
            ddlMUID.Items.Add(new ListItem("--Mata Uang--", "0"));
            ArrayList arrData = new MataUangFacade().Retrieve();
            foreach (MataUang mu in arrData)
            {
                ddlMUID.Items.Add(new ListItem(mu.Nama, mu.ID.ToString()));
            }
        }
        private void ApprovalPO()
        {
            Users users = (Users)Session["Users"];
            if (txtNoPO.Text != string.Empty)
            {
                POPurchnFacade pOPurchnFacade = new POPurchnFacade();
                POPurchn pOPurchn = pOPurchnFacade.RetrieveByNo(txtNoPO.Text);
                if (pOPurchnFacade.Error == string.Empty)
                {
                    pOPurchn.Approval = users.Apv;
                    pOPurchn.LastModifiedBy = users.UserName;
                    int xPOID = pOPurchn.ID;
                    string strError = string.Empty;

                    ArrayList arrPOPurchnDetail = new ArrayList();
                    POPurchnDetailFacade pOPurchnDetailFacade = new POPurchnDetailFacade();
                    arrPOPurchnDetail = pOPurchnDetailFacade.RetrieveItemPOID(pOPurchn.ID);
                    int xSPPID = 0;
                    foreach (POPurchnDetail pOPurchnDetail in arrPOPurchnDetail)
                    {
                        xSPPID = pOPurchnDetail.SPPID;
                        ItemCD = pOPurchnDetail.ItemCode;
                    }

                    SPPFacade sPPFacade = new SPPFacade();
                    SPP sPP = sPPFacade.RetrieveByIdStatus(xSPPID);

                    ArrayList arrSPPDetail = new ArrayList();
                    SPPDetailFacade sPPDetailFacade = new SPPDetailFacade();
                    arrSPPDetail = sPPDetailFacade.RetrieveBySPPID(xSPPID);

                    foreach (SPPDetail sPPDetail in arrSPPDetail)
                    {
                        if (sPPDetail.Quantity > sPPDetail.QtyPO)
                        {
                            sPP.Status = 1;
                        }
                        else
                        {
                            sPP.Status = 2;
                        }
                    }
                    POApprovalStatusFacade pOApprovalStatusFacade = new POApprovalStatusFacade(pOPurchn, sPP);
                    if (Session["ListOfPOPurchnDetail"] != null)
                        arrPOPurchnDetail = (ArrayList)Session["ListOfPOPurchnDetail"];

                    if (pOPurchn.ID > 0)
                    {
                        //Users users = (Users)Session["Users"];
                        if (users.Apv == 0)
                            strError = pOApprovalStatusFacade.Update(); //admin upate
                        if (users.Apv == 1)
                            strError = pOApprovalStatusFacade.UpdateApprovalPO1();// head aprove
                        if (users.Apv == 2)
                            strError = pOApprovalStatusFacade.UpdateApprovalPO2();// manager Approve
                        if (users.Apv == 3)
                            strError = pOApprovalStatusFacade.UpdateApprovalPO3();// receipt parsial 
                        if (users.Apv == 4)
                            strError = pOApprovalStatusFacade.UpdateApprovalPO4();//Receipt Proses clear
                        #region log approval process
                        EventLogProcess mp = new EventLogProcess();
                        EventLog evl = new EventLog();
                        mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                        mp.Pilihan = "Insert";
                        evl.UserID = ((Users)Session["Users"]).ID;
                        evl.AppLevel = ((Users)Session["Users"]).Apv;
                        evl.DocNo = pOPurchn.NoPO.ToString();
                        evl.DocType = "PO";
                        evl.AppDate = DateTime.Now;
                        evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                        mp.EventLogInsert(evl);
                        #endregion
                        try
                        {
                            #region Transfer point ke lapak jika aproval level manager (Aproval 2)
                            if (users.Apv == 2)
                            {
                                /**
                                 * Tampilkan informasi point lapak jika material kertas
                                 */
                                string AgenLapakAktif = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("AgenLapakAktif", "Receipt");
                                //string[] CodeKertas = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("KertasItemCode", "Receipt").Split(',');
                                POApprovalFacade ppo = new POApprovalFacade();
                                string[] CodeKertas = ppo.GetItemKertas().ToString().Split(',');
                                int Pos = Array.IndexOf(CodeKertas, ItemCD);
                                if (Pos > -1 && AgenLapakAktif == "1")
                                {

                                    this.SupplierID = pOPurchn.SupplierID.ToString();
                                    int AgenID = this.GetIDLapak();
                                    //jika agenID tidak ditemukan ==0 maka kirim email gagal refil
                                    if (AgenID == 0)
                                    {
                                        KirimEmail(txtNoPO.Text, this.GetSupplierName()); return;
                                    }
                                    txtLapakID.Text = this.GetIDLapak().ToString();
                                    int AreaKirim = this.GetAreaKirim(AgenID.ToString());
                                    bpas_api.WebService1 api = new bpas_api.WebService1();
                                    string bln = Global.BulanRomawi(DateTime.Parse(txtTglPO.Text).Month).ToString();
                                    string thn = DateTime.Parse(txtTglPO.Text).Year.ToString();
                                    foreach (POPurchnDetail ppd in arrPOPurchnDetail)
                                    {
                                        POApprovalFacade pop = new POApprovalFacade();
                                        //check point sudah pernah terkirimatau blm
                                        if (pop.CheckPointTerkirim(ppd.POID) > 0) { continue; }
                                        int rst = 0;
                                        KadarAir ka = pOPurchnDetailFacade.RetrieveKAData(ppd.POID.ToString(), ppd.ID.ToString(), true);
                                        int nettplant = pOPurchnDetailFacade.GetNettPlant(ppd.POID.ToString());
                                        int sjNumber = this.GetNumerator(AgenCode(txtLapakID.Text)) + 1;
                                        rst = api.InsertKirimKeBPASFromPO(
                                             pOPurchn.POPurchnDate.ToString("yyyyMMdd"),
                                             ka.NoPol, sjNumber.ToString().PadLeft(6, '0') + "/" + AgenCode(txtLapakID.Text) + "/SJ/" + bln + "/" + thn,
                                             //ppd.Qty, AgenID.ToString(), ka.ID.ToString(),
                                             nettplant, AgenID.ToString(), ka.ID.ToString(),
                                             ((Users)Session["Users"]).UnitKerjaID.ToString(),
                                             ((Users)Session["Users"]).UserID.ToString());
                                        if (rst > -1)
                                        {
                                            KadarAir kaUPD = new KadarAir();
                                            kaUPD.NoSJ = sjNumber.ToString().PadLeft(6, '0') + "/" + AgenCode(txtLapakID.Text) + "/SJ/" + bln + "/" + thn;
                                            kaUPD.PointStatus = rst;
                                            kaUPD.IPAddress = Request.ServerVariables["REMOTE_ADDR"].ToString();
                                            int rs = pOPurchnDetailFacade.UpdateStatusPoint(ka.ID, kaUPD);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        catch { }
                    }
                    //else
                    //    strError = transferOrderProsessFacade.Insert();


                    if (strError == string.Empty)
                    {
                        if (users.Apv < 2)
                        {
                            //LoadOpenPO();
                            btnNext_Click(null, null);
                        }
                        else
                        {
                            AutoNext();
                        }
                    }
                    else
                    {
                        DisplayAJAXMessage(this, strError);
                    }
                }
            }

            Response.Redirect("ApprovalPONew2.aspx");
        }


        private void KirimEmail(string NOPO, string Supplier)
        {
            MailMessage mail = new MailMessage();
            EmailReportFacade msg = new EmailReportFacade();
            mail.From = new MailAddress("system_support@grcboard.com");
            mail.To.Add("aying@grcboard.com");
            string PO = NOPO.Substring(0, 1);
            if (PO == "K") { mail.To.Add("purchasing_spv.krwg@grcboard.com"); }
            if (PO == "C") { mail.To.Add("indrabudiawan@grcboard.com"); }
            if (PO == "J") { mail.To.Add("purchasing_spv.krwg@grcboard.com"); }
            mail.Bcc.Add("noreplay@grcboard.com");
            mail.Subject = "Gagal Refil Poin  PO " + NOPO;
            mail.Body = "Refil Poin dari PO " + NOPO + " tidak berhasil karena supplier " + Supplier + " belum masuk di data Lapak\n\r";
            mail.Body += "Terimakasih, " + "\n\r";
            mail.Body += "Salam GRCBOARD " + "\n\r";
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
            smt.Send(mail);

        }
        private void ApprovalPO(bool NotApproval)
        {
            if (txtNotApproved.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Alasan Not Approval tidak boleh kosong");
                return;
            }

            string strError = string.Empty;
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            POPurchn pOPurchn = pOPurchnFacade.RetrieveByNo(txtNoPO.Text);
            Users users = (Users)Session["Users"];
            if (pOPurchnFacade.Error == string.Empty && pOPurchn.ID > 0)
            {
                pOPurchn.AlasanNotApproval = txtNotApproved.Text;
                pOPurchn.Approval = 0;
                pOPurchn.CreatedBy = users.UserName;
                //kembali ke status Head Purchasing

                POApprovalStatusFacade pOApprovalStatusFacade = new POApprovalStatusFacade(pOPurchn, new SPP());
                if (pOPurchn.ID > 0)
                {
                    strError = pOApprovalStatusFacade.UpdateNotApproval();
                    if (strError == string.Empty)
                    {
                        AutoNext();
                    }
                }
                Response.Redirect("ApprovalPONew2.aspx");
            }
        }
        private void AutoNext()
        {
            if (btnNext.Enabled == true)
            {
                btnNext_Click(null, null);
            }
            else if (btnPrev.Enabled == true)
            {
                btnPrev_Click(null, null);
            }
            else
            {
                Response.Redirect("ApprovalPONew2.aspx");
            }
        }
        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            Users users = ((Users)Session["Users"]);
            string AppGroup = pOPurchnFacade.GetAppGroup(users.ID);
            Response.Redirect("ListPOPending.aspx?approve=" + users.Apv + "&pogroup=" + AppGroup);
        }
        protected void btnNotApprove_Click(object sender, EventArgs e)
        {
            ApprovalPO(true);
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ApprovalPO();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadOpenPO(txtCari.Text);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void lstPO_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            string ItemName = e.CommandArgument.ToString();

            int ItemTypeID = int.Parse(e.CommandName);
            POPurchn hsp = e.Item.DataItem as POPurchn;
            ArrayList arrHsp = new ArrayList();

            string inv = string.Empty;
            //switch (e.CommandName)
            //{
            //    case "showHis":
            if (ItemTypeID == 1)
            {
                inv = "popurchndetail.itemtypeid=1 and popurchndetail.itemid in (select id from inventory where itemname like";
            }
            else if (ItemTypeID == 2)
            {
                inv = "popurchndetail.itemtypeid=2 and popurchndetail.itemid in (select id from asset where itemname like";
            }
            else if (ItemTypeID == 3)
            {
                inv = "popurchndetail.itemtypeid=3 and popurchndetail.itemid in (select id from biaya where itemname like";
            }
            string myScript = "show_history('" + inv + "','" + ItemName.Replace("+", "%2B") + "')";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript", myScript, true);
            //        break;
            //    case "showstock":
            //        if (ItemTypeID == 1)
            //        {
            //            inv = "popurchndetail.itemtypeid=1 and popurchndetail.itemid in (select id from inventory where itemname like";
            //        }
            //        else if (ItemTypeID == 2)
            //        {
            //            inv = "popurchndetail.itemtypeid=2 and popurchndetail.itemid in (select id from asset where itemname like";
            //        }
            //        else if (ItemTypeID == 3)
            //        {
            //            inv = "popurchndetail.itemtypeid=3 and popurchndetail.itemid in (select id from biaya where itemname like";
            //        }
            //        string myScript1 = "show_stock('" + inv + "','" + ItemName + "')";
            //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "MyScript1", myScript1, true);
            //        break;
            //}
        }
        protected void lstItemPO_DataBound(object sender, RepeaterItemEventArgs e)
        {

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POPurchnDetail pod = (POPurchnDetail)e.Item.DataItem;
                decimal tharga = pod.Qty * pod.Price;
                gHarga += tharga;
                //  ((System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("dlv")).Attributes.Add("title", "SPP Approval date : ");
                ((Label)e.Item.FindControl("tHarga")).Text = tharga.ToString("###,##0.#0");
                ItemCD = pod.ItemCode;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                ((Label)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                nDisc = decimal.Parse(txtDiscount.Text);
                gDisc = (nDisc / 100) * gHarga;
                gPPN = (gHarga - gDisc) * (decimal.Parse(txtPPN.Text) / 100);
                gTotal = gHarga - gDisc + gPPN;
                ((Label)e.Item.FindControl("dsk")).Text = (nDisc > 0) ? nDisc.ToString("###.#0") + " %" : "";
                ((Label)e.Item.FindControl("gPPN")).Text = gPPN.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("gDisc")).Text = gDisc.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("grnTotal")).Text = gTotal.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("tDisc")).Text = (gHarga - gDisc).ToString("###,##0.#0");
                ((System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("tbDisc")).Visible = (nDisc > 0) ? true : false;
                ((System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("bDisc")).Visible = (nDisc > 0) ? true : false;

            }
        }
        /**
             * Posting receipt material kertas kantong semen
             * untuk proses point ke lapak
             */
        private string ReceiptNo { get; set; }
        private string Qty { get; set; }
        private string PlanID { get; set; }
        private string IDLapak { get; set; }
        private string AgenID { get; set; }
        private string SupplierID { get; set; }
        private bpas_api.WebService1 api = new bpas_api.WebService1();
        private Global2 api2 = new Global2();
        private void LoadSJLapak()
        {
            txtLapakID.Text = this.GetIDLapak().ToString();
            if (txtLapakID.Text != "-1" || txtLapakID.Text != string.Empty)
            {
                LoadSJ();
            }
            else
            {
                DisplayAJAXMessage(this, "Koneksi Internet Error Untuk Point Lapak, Silahkan hubungi IT Dept.");
                return;
            }
        }
        private string LoadAgen()
        {
            string result = string.Empty;
            try
            {
                //string IDAgen = "0";
                DataSet ds = new DataSet();
                ds = api2.GetDataFromTable("Agen_DtSupplier", " where SuppPurchID=" + this.SupplierID, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["AgenID"].ToString();
                }

            }
            catch (Exception)
            {
                result = "Koneksi internet error ";
            }
            return result;
        } //not used
        private void LoadSJ()
        {

            //DataSet ds = new DataSet();
            //ddlSjLapak.Items.Clear();
            //ddlSjLapak.Items.Add(new ListItem("--Pilih No SJ--", ""));
            //string Criteria = " where AgenID=" + txtLapakID.Text;
            //Criteria += " and ConfirmReceipt=0";
            //ds = api.GetDataTable("Agen_DtKirimKeBpas", "NoSJ,PlatNomor,ID", Criteria, "GRCBoardLapak");
            //foreach (DataRow d in ds.Tables[0].Rows)
            //{
            //    ddlSjLapak.Items.Add(new ListItem(d["NoSJ"].ToString() + " - " + d["PlatNomor"].ToString().ToUpper(), d["NoSJ"].ToString()));
            //}


        }
        protected void ddlSjLapak_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataSet ds2 = new DataSet();
            //txtKirimanID.Text = "0";
            //string Criteria2 = " where NoSJ='" + ddlSjLapak.SelectedValue.ToString() + "'";
            //ds2 = api.GetDataTable("Agen_DtKirimKeBpas", "ID", Criteria2, "GRCBoardLapak");
            //string kirimanID = "0";
            //foreach (DataRow dd in ds2.Tables[0].Rows)
            //{
            //    kirimanID = dd["ID"].ToString();
            //}
            //txtKirimanID.Text = kirimanID;
            //lbAddOP.Enabled = (ddlSjLapak.SelectedIndex > 0) ? true : false;
        }
        private void UpdateReceiptKS()
        {
            bpas_api.WebService1 bpas = new bpas_api.WebService1();
            string ID = this.IDLapak;
            decimal Jumlah = decimal.Parse(this.Qty);
            string Plant = this.PlanID;
            string ReceiptID = this.ReceiptNo;
            string rst = bpas.UpdateKiriman(ID, ReceiptID, Jumlah, PlanID);
        }
        private int GetIDLapak()
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                Global2 bpas = new Global2();
                string Criteria = " where SupplierID=" + this.SupplierID;
                Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                Criteria += " and RowStatus>-1";
                ds = bpas.GetDataFromTable("Agen_DtAgenIDtoSupID", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = int.Parse(d["AgenID"].ToString());
                }
                return result;
            }
            catch (Exception)
            {
                return -1;
            }
        }
        private int GetAreaKirim(string AgenID)
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where ID=" + AgenID;
                //Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = api2.GetDataFromTable("Agen_DtAgen", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = int.Parse(d["AreaKirim"].ToString());
                }
                return result;
            }
            catch (Exception) { return -1; }

        }
        private string AgenCode(string AgenID)
        {
            try
            {
                DataSet ds = new DataSet();
                string result = string.Empty;
                bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where ID=" + AgenID;
                //Criteria += " and PlantID=" + ((Users)Session["Users"]).UnitKerjaID.ToString();
                ds = api2.GetDataFromTable("Agen_DtAgen", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["Code"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private int GetNumerator(string AgenCode)
        {
            try
            {
                DataSet ds = new DataSet();
                int result = 0;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where Type='SJ Code " + AgenCode + "'";
                ds = api2.GetDataFromTable("Agen_DtNumerator", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = Convert.ToInt32(d["LastNumber"].ToString());
                }
                return result;
            }
            catch (Exception ex)
            {
                return 0; ;
            }

        }
        private string GetSJfromPointLapak(int ReceiptID)
        {
            try
            {
                DataSet ds = new DataSet();
                string result = string.Empty;
                //bpas_api.WebService1 bpas = new bpas_api.WebService1();
                string Criteria = " where ConfirmReceipt=" + ReceiptID;
                ds = api2.GetDataFromTable("Agen_DtKirimKeBpas", Criteria, "GRCBoardLapak");
                foreach (DataRow d in ds.Tables[0].Rows)
                {
                    result = d["NoSJ"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        private void KirimDataPointLapak()
        {

        }
        public string GetSupplierName()
        {
            string result = string.Empty;
            string strSQL = "SELECT SupplierName FROM SuppPurch WHERE ID=" + this.SupplierID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = sdr["SupplierName"].ToString();
                }
            }
            return result;
        }
    }
    public class POApprovalFacade : POPurchnFacade
    {
        POPurchn objPO = new POPurchn();
        ArrayList arrData = new ArrayList();
        public string Criteria { get; set; }
        public string OrderBy { get; set; }

        public ArrayList RetrieveOpenPO()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT DISTINCT TOP 100 PO.*,SP.SupplierName,SP.UP " +
                          "FROM POPurchn PO  " +
                          "LEFT JOIN POPurchnDetail POD ON POD.POID=PO.ID " +
                          "LEFT JOIN SuppPurch SP ON SP.ID=PO.SupplierID " +
                          "WHERE PO.Status =0 and POD.Status>-1 " + this.Criteria +
                          this.OrderBy;
            DataAccessLayer.DataAccess da = new DataAccessLayer.DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    arrData.Add(GetObject(sdr, GenerateObjectNodetail(sdr)));
                }
            }
            return arrData;
        }
        public POPurchn GetObject(SqlDataReader sdr, POPurchn objP)
        {
            objPO = (POPurchn)objP;
            objPO.SupplierName = sdr["SupplierName"].ToString();
            objPO.UP = sdr["UP"].ToString();
            objPO.ItemFrom = int.Parse(sdr["ItemFrom"].ToString());
            return objPO;
        }
        public decimal TotalPricePO(string POID)
        {
            decimal total = 0;
            string sqlQuery = "Select ISNULL(SUM(Qty*Price),0)Price From POPurchnDetail Where Status>-1 and POID=" + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(sqlQuery);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    total = Convert.ToDecimal(sdr["Price"].ToString());
                }
            }
            return total;
        }
        public string GetItemKertas()
        {
            string kertas = string.Empty;
            string strSQL = "SELECT DISTINCT ItemCode FROM DeliveryKertasKA GROUP By ItemCode";
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    kertas += sdr["ItemCode"].ToString() + ",";
                }
            }
            return kertas.Substring(0, (kertas.Length - 1));
        }
        public int CheckPointTerkirim(int POID)
        {
            int result = -1;
            string strSQL = "SELECT ISNULL(PointStatus,0)PointID FROM POPurchnKadarAir WHERE RowStatus>-1 AND POID=" + POID;
            DataAccess da = new DataAccess(Global.ConnectionString());
            SqlDataReader sdr = da.RetrieveDataByString(strSQL);
            if (da.Error == string.Empty && sdr.HasRows)
            {
                while (sdr.Read())
                {
                    result = int.Parse(sdr["PointID"].ToString());
                }
            }
            return result;
        }
    }
}