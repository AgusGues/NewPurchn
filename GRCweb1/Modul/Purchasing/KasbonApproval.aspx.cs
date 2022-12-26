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

namespace GRCweb1.Modul.Purchasing
{
    public partial class KasbonApproval : System.Web.UI.Page
    {
        public string[] KasbonStatus = new string[] { "OPEN", "Approval Head", "Approval Manager" };
        public string OpenKasbon { get; set; }
        public decimal gHarga = 0;
        public decimal gTotal = 0;
        public string ItemCD = "";
        public decimal DanaCadangan = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Global.link = "~/Default.aspx";
                Users users = ((Users)Session["Users"]);
                if (users.DeptID == 15 || users.DeptID == 12 && users.Apv > 0)
                {
                    LoadOpenKasbon();
                }
                if (Request.QueryString["PengajuanNo"] != null)
                {
                    LoadOpenKasbon(Request.QueryString["PengajuanNo"].ToString());
                }
                else if (noPengajuan.Value != string.Empty)
                {
                    string[] ListOpenKasbon = noPengajuan.Value.Split(',');
                    //btnApprove.Text += " " + ListOpenPO.Count().ToString();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    //LoadMUID();
                    LoadOpenKasbon(ListOpenKasbon[idx].ToString());
                    ViewState["index"] = idx;
                }
                string[] ListOpenKasbonx = noPengajuan.Value.Split(',');
                int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
                btnNext.Enabled = ((idxx) == ListOpenKasbonx.Count()) ? false : true;
                txtNotApproved.Attributes.Add("onkeyup", "onKeyUp()");
                if (users.DeptID == 15)
                {
                    btnApprove.Enabled = false;
                }
                else
                {
                    btnApprove.Enabled = true;
                }
                //btnCancel.Enabled = (users.Apv < 1) ? false : true;
                //btnCancel.Enabled = (users.Apv < 1 && users.DeptID == 15) ? false : btnCancel.Enabled;
                //btnCancel.Enabled = (users.DeptID != 15) ? false : btnCancel.Enabled;
            }
        }

        protected void chk_CheckedChange(object sender, EventArgs e)
        {
            btnApprove.Enabled = true;
            btnNotApprove.Enabled = true;
            //string transID = string.Empty;
            //for (int i = 0; i < lstItemKasbon.Items.Count; i++)
            //{
            //    //lstItemKasbon = ((Repeater)(objDetail.FindControl("lstItemKasbon")));
            //    CheckBox chk = (CheckBox)lstItemKasbon.Items[i].FindControl("chk");
            //    chk.Checked = chkAll.Checked;
            //}
        }
        //protected void chk_CheckedChangeD(object sender, EventArgs e)
        //{
        //    CheckBox chk = (CheckBox)sender;
        //    string transID = chk.ToolTip;
        //    ZetroView zl = new ZetroView();
        //    zl.QueryType = Operation.CUSTOM;
        //    if (chk.Checked == true)
        //        zl.CustomQuery = "update KasbonDetail set Status=0 where id=" + transID;
        //    else
        //        zl.CustomQuery = "update KasbonDetail set Status=-1 where id=" + transID;
        //    SqlDataReader sdr = zl.Retrieve();
        //}
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            LoadOpenKasbon();
            string[] ListOpenKasbon = noPengajuan.Value.Split(',');
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            idx = (idx < 0) ? 0 : idx;
            LoadOpenKasbon(ListOpenKasbon[idx].ToString());
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenKasbon.Count()) ? false : true;
            ViewState["index"] = idx;
            if (ListOpenKasbon.Count() == 0) Response.Redirect("KasbonApproval.aspx");
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            LoadOpenKasbon();
            string[] ListOpenKasbon = noPengajuan.Value.Split(',');
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            btnNext.Enabled = ((idx) == ListOpenKasbon.Count()) ? false : true;
            ViewState["index"] = idx;
            btnPrev.Enabled = (idx > 0) ? true : false;
            try
            {
                //btnNext.Text += " " + idx.ToString();
                LoadOpenKasbon(ListOpenKasbon[idx].ToString());
                LoadOpenKasbon();
            }
            catch
            {
                ViewState["index"] = 0;
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                LoadOpenKasbon(ListOpenKasbon[0].ToString());
            }
        }
        private void LoadOpenKasbon()
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
            //POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            KasbonApprovalFacade kasbonApp = new KasbonApprovalFacade();
            ArrayList arrKasbon = new ArrayList();

            string AppGroup = kasbonApp.GetAppGroup(users.ID);
            string AppPIC = kasbonFacade.GetAppPIC(users.ID, users.Apv);
            if (users.DeptID == 15 && users.Apv > 1)
            {
                decimal Total = Convert.ToDecimal(kasbonFacade.GetAppWithTotal2(users.ID, users.Apv));
                int Apv = (users.Apv > 2 && users.DeptID == 12) ? 2 : users.Apv;
                kasbonApp.Criteria = (users.DeptID == 12 && Total > 3000000) ? "and k.ApvMgr=1" : " and (AlasanNotApproved='' or AlasanNotApproved is null) and k.Approval =" + (Apv - 1);
                kasbonApp.GroupApp = (AppPIC != string.Empty) ? " and k.Pic in(" + AppPIC + ")" : string.Empty;
                kasbonApp.GroupApv = (users.DeptID == 15 && Total > 3000000 && users.Apv > 1) ? "and k.ApvMgr=0" : string.Empty;
                //kasbonApp.Criteria += (AppGroup != string.Empty) ? " and Pod.GroupID in(" + AppGroup + ")" : string.Empty;
                kasbonApp.OrderBy = (users.Apv > 2 && users.DeptID == 12) ? "Order By ID Desc" : " Order By K.NoPengajuan Desc ";
                arrKasbon = kasbonApp.RetrieveOpenKasbon2();
            }
            else
            {
                decimal Total = Convert.ToDecimal(kasbonFacade.GetAppWithTotal(users.ID, users.Apv));
                decimal Total1 = Convert.ToDecimal(kasbonFacade.GetAppWithTotal1(users.ID, users.Apv));
                int Apv = (users.Apv > 2 && users.DeptID == 12) ? 2 : users.Apv;
                string finance = string.Empty;
                if (users.DeptID != 12)
                    finance = "and k.ApvMgr=1";

                kasbonApp.Criteria = (users.DeptID == 12 && Total < 3000000) ? " and (AlasanNotApproved='' or AlasanNotApproved is null) and k.status=0 and k.Approval =" + (Apv - 1) : string.Empty;
                kasbonApp.Criteria = (users.DeptID == 12 && Total1 > 3000000) ? finance + "and k.Approval =" + (Apv - 1) + " " : " and (AlasanNotApproved='' or AlasanNotApproved is null) and k.status=0 and k.Approval =" + (Apv - 1);
                kasbonApp.GroupApp = (AppPIC != string.Empty) ? " and k.Pic in(" + AppPIC + ")" : string.Empty;
                kasbonApp.GroupApv = (users.DeptID == 15 && Total > 3000000 && users.Apv > 1) ? "and k.ApvMgr=0" : string.Empty;
                //kasbonApp.Criteria += (AppGroup != string.Empty) ? " and Pod.GroupID in(" + AppGroup + ")" : string.Empty;
                kasbonApp.OrderBy = (users.Apv > 2 && users.DeptID == 12) ? "Order By ID Desc" : " Order By K.NoPengajuan Desc ";
                arrKasbon = kasbonApp.RetrieveOpenKasbon();
            }
            noPengajuan.Value = "";
            foreach (Kasbon kasbon in arrKasbon)
            {
                noPengajuan.Value += kasbon.NoPengajuan + ",";
            }
            noPengajuan.Value = (noPengajuan.Value != string.Empty) ? noPengajuan.Value.Substring(0, (noPengajuan.Value.Length - 1)) : "0";
        }
        protected void txtNotApproved_Change(object sender, EventArgs e)
        {
            btnApprove.Enabled = (txtNotApproved.Text.Length > 0) ? false : true;
            btnNotApprove.Enabled = (txtNotApproved.Text.Length > 0) ? true : false;
        }
        private void LoadOpenKasbon(string NoPengajuan)
        {
            Users users = (Users)Session["Users"];
            KasbonApprovalFacade kasbonApp = new KasbonApprovalFacade();
            KasbonFacade kasbonFacade = new KasbonFacade();
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            string AppGroup = pOPurchnFacade.GetAppGroup(users.ID);
            string AppPIC = kasbonFacade.GetAppPIC(users.ID, users.Apv);
            kasbonApp.Criteria = " and NoPengajuan='" + NoPengajuan + "'";
            kasbonApp.Criteria += " and (AlasanNotApproved='' or AlasanNotApproved is null)";
            pOPurchnFacade.GroupApp = (AppGroup != string.Empty) ? " and k.Pic in(" + AppPIC + ")" : string.Empty;
            ArrayList arrKasbon = kasbonApp.RetrieveOpenKasbon();
            if (txtCari.Text != "Find by Nomor Kasbon" && arrKasbon.Count == 0)
            {
                DisplayAJAXMessage(this, "Nomor Pengajuan tidak ada / Nomor Pengajuan Salah / Nomor Pengajuan Sudah di approve");
                return;
            }
            foreach (Kasbon kasbon in arrKasbon)
            {
                if (kasbon.Apv == users.Apv || kasbon.Apv < 0)
                {
                    if (txtCari.Text == kasbon.NoPengajuan)
                    {
                        DisplayAJAXMessage(this, "No Pengajuan Sudah di Approved");
                        return;
                    }
                    AutoNext();
                }
                //LoadMUID();
                //decimal TotalHarga = kasbonApp.TotalPricePO(po.ID.ToString());
                //decimal dis = (TotalHarga * (po.Disc / 100));
                //decimal ppn = ((TotalHarga - dis) * (po.PPN / 100));
                txtNoPengajuan.Text = NoPengajuan;
                txtTglKasbon.SelectedDate = Convert.ToDateTime(kasbon.KasbonDate.ToString("dd-MMM-yyyy"));
                txtDept.Text = "Purchasing";
                txtPic.Text = kasbon.PIC;
                txtDanaCadangan.Text = kasbon.DanaCadangan.ToString("###,##0.#0");
                txtStatus.Text = (kasbon.Apv == 0) ? "Open" : "Approved Head";
                txtKasbonType.Text = (kasbon.KasbonType == 0) ? "Biasa" : "Top Urgent";
                txtNotApproved.Text = kasbon.AlasanNotApproval.ToString();
                LoadDetailKasbon(kasbon.ID, kasbon.KasbonType);
            }
        }
        private void LoadDetailKasbon(int KID, int TipeKasbon)
        {
            MataUangFacade muf = new MataUangFacade();
            KasbonDetailFacade kd = new KasbonDetailFacade();
            if (TipeKasbon == 0)
            {
                kd.Where = " and A.Status>-1";
                ArrayList arrData = kd.RetrieveItemKID(KID);
                lstItemKasbon.DataSource = arrData;
                lstItemKasbon.DataBind();
            }
            else
            {
                ArrayList arrData = kd.RetrieveItemKID2(KID);
                lstItemKasbon.DataSource = arrData;
                lstItemKasbon.DataBind();
            }
        }
        private void ApprovalKasbon()
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
            string no_kasbon = string.Empty;
            string kas = txtTglKasbon.SelectedDate.ToString("yyyy");
            int urutan = kasbonFacade.GetLastUrutan(Convert.ToInt32(txtTglKasbon.SelectedDate.ToString("yyyy"))) + 1;
            string bulanR = txtTglKasbon.SelectedDate.ToString("MM");
            CompanyFacade companyFacade = new CompanyFacade();
            string ErMM = " ";
            string nourut = txtNoPengajuan.Text.ToString();
            string no = nourut.Substring(0, 4);
            string code = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            no_kasbon = code + no + "-" + bulanR + txtTglKasbon.SelectedDate.ToString("yy");
            if (txtNoPengajuan.Text != string.Empty)
            {
                Kasbon kasbon = kasbonFacade.RetrieveByNo(txtNoPengajuan.Text);
                if (kasbonFacade.Error == string.Empty)
                {
                    kasbon.Apv = users.Apv;
                    kasbon.LastModifiedBy = users.UserName;
                    int xKID = kasbon.ID;
                    string strError = string.Empty;

                    ArrayList arrKasbonDetail = new ArrayList();
                    KasbonDetailFacade kasbonDetailFacade = new KasbonDetailFacade();
                    arrKasbonDetail = kasbonDetailFacade.RetrieveItemKID(kasbon.ID);
                    int xSPPID = 0;
                    foreach (KasbonDetail kasbonDetail in arrKasbonDetail)
                    {
                        xSPPID = kasbonDetail.SPPID;
                        ItemCD = kasbonDetail.ItemCode;
                    }

                    //KasbonApprovalFacade kasbonApprovalFacade = new KasbonApprovalFacade(kasbon, sPP);
                    if (Session["ListOfKasbonDetail"] != null)
                        arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];

                    if (kasbon.ID > 0)
                    {
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        int i = 0;
                        if (users.Apv == 1)
                            zl.CustomQuery = "UPDATE Kasbon set Approval=" + users.Apv + ", LastModifiedBy='" + users.UserName + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                        SqlDataReader sdr = zl.Retrieve();

                        string transID = string.Empty;
                        foreach (RepeaterItem item in lstItemKasbon.Items)
                        //for (int i = 0; i < lstItemKasbon.Items.Count; i++)
                        {
                            //lstItemKasbon = ((Repeater)(objDetail.FindControl("lstItemKasbon")));
                            //CheckBox cb = (item.FindControl("chk") as CheckBox);
                            CheckBox cb = (CheckBox)lstItemKasbon.Items[i].FindControl("chk");
                            //chk.Checked = chkAll.Checked;
                            transID = cb.ToolTip;
                            ZetroView zV = new ZetroView();
                            zV.QueryType = Operation.CUSTOM;
                            if (cb.Checked == true)
                            {
                                zV.CustomQuery = "update KasbonDetail set Status=0 where id=" + transID;
                            }
                            else
                            {
                                zV.CustomQuery = "update KasbonDetail set Status=-1 where id=" + transID;
                            }
                            SqlDataReader sdR = zV.Retrieve();

                        }

                        //if (users.Apv == 2)
                        ZetroView zl2 = new ZetroView();
                        zl2.QueryType = Operation.CUSTOM;
                        if (users.Apv == 2)
                            zl2.CustomQuery = "UPDATE Kasbon set KasbonNo='" + no_kasbon + "', Approval=" + users.Apv + ", LastModifiedBy='" + users.UserName + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                        SqlDataReader sdr2 = zl2.Retrieve();

                        #region log approval process
                        //EventLogProcess mp = new EventLogProcess();
                        //EventLog evl = new EventLog();
                        //mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                        //mp.Pilihan = "Insert";
                        //evl.UserID = ((Users)Session["Users"]).ID;
                        //evl.AppLevel = ((Users)Session["Users"]).Apv;
                        //evl.DocNo = kasbon.NoPengajuan.ToString();
                        //evl.DocType = "Kasbon";
                        //evl.AppDate = DateTime.Now;
                        //evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                        //mp.EventLogInsert(evl);
                        #endregion
                    }

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

            Response.Redirect("KasbonApproval.aspx");
        }
        private void ApprovalKasbon(bool NotApproval)
        {
            if (txtNotApproved.Text == string.Empty)
            {
                DisplayAJAXMessage(this, "Alasan Not Approval tidak boleh kosong");
                return;
            }

            string strError = string.Empty;
            KasbonFacade kasbonFacade = new KasbonFacade();
            Kasbon kasbon = kasbonFacade.RetrieveByNo(txtNoPengajuan.Text);
            Users users = (Users)Session["Users"];
            if (kasbonFacade.Error == string.Empty && kasbon.ID > 0)
            {
                kasbon.AlasanNotApproval = txtNotApproved.Text;
                kasbon.Apv = 0;
                kasbon.CreatedBy = users.UserName;
                //kembali ke status Head Purchasing

                //KasbonApprovalFacade kasbonApprovalStatusFacade = new KasbonApprovalFacade(kasbon, new SPP());
                if (kasbon.ID > 0)
                {
                    //strError = kasbonApprovalStatusFacade.UpdateNotApproval();
                    ZetroView zl = new ZetroView();
                    zl.QueryType = Operation.CUSTOM;
                    zl.CustomQuery = "UPDATE Kasbon set Status=-1, AlasanNotApproved=" + kasbon.AlasanNotApproval + " WHERE ID=" + kasbon.ID + " ";
                    SqlDataReader sdr = zl.Retrieve();
                    if (strError == string.Empty)
                    {
                        AutoNext();
                    }
                }
                Response.Redirect("KasbonApproval.aspx");
            }
        }
        private void AutoNext()
        {
            //if (btnNext.Enabled == true)
            //{
            //    btnNext_Click(null, null);
            //}
            //else if (btnPrev.Enabled == true)
            //{
            //    btnPrev_Click(null, null);
            //}
            //else
            //{
            //    Response.Redirect("KasbonApproval.aspx");
            //}
        }
        protected void btnNotApprove_Click(object sender, EventArgs e)
        {
            ApprovalKasbon(true);
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
            string no_kasbon = string.Empty;
            string kas = txtTglKasbon.SelectedDate.ToString("yyyy");
            int urutan = kasbonFacade.GetLastUrutan(Convert.ToInt32(txtTglKasbon.SelectedDate.ToString("yyyy"))) + 1;
            string bulanR = txtTglKasbon.SelectedDate.ToString("MM");
            CompanyFacade companyFacade = new CompanyFacade();
            string ErMM = " ";
            string nourut = txtNoPengajuan.Text.ToString();
            string no = nourut.Substring(0, 4);
            string code = companyFacade.GetKodeCompany(((Users)Session["Users"]).UnitKerjaID);
            no_kasbon = code + no + "-" + bulanR + txtTglKasbon.SelectedDate.ToString("yy");
            if (txtNoPengajuan.Text != string.Empty)
            {
                //Kasbon kasbon = kasbonFacade.RetrieveByNo(txtNoPengajuan.Text);
                Kasbon kasbon = kasbonFacade.RetrieveByNo1(txtNoPengajuan.Text);
                if (kasbonFacade.Error == string.Empty)
                {
                    kasbon.Apv = users.Apv;
                    kasbon.LastModifiedBy = users.UserName;
                    int xKID = kasbon.ID;
                    string strError = string.Empty;

                    ArrayList arrKasbonDetail = new ArrayList();
                    KasbonDetailFacade kasbonDetailFacade = new KasbonDetailFacade();
                    arrKasbonDetail = kasbonDetailFacade.RetrieveItemKID(kasbon.ID);
                    int xSPPID = 0;
                    foreach (KasbonDetail kasbonDetail in arrKasbonDetail)
                    {
                        xSPPID = kasbonDetail.SPPID;
                        ItemCD = kasbonDetail.ItemCode;
                    }

                    //KasbonApprovalFacade kasbonApprovalFacade = new KasbonApprovalFacade(kasbon, sPP);
                    if (Session["ListOfKasbonDetail"] != null)
                        arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];

                    if (kasbon.ID > 0)
                    {
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        //int i = 0;
                        if (users.DeptID == 15)
                        {
                            if (users.Apv == 1 && kasbon.Total > 3000000)
                            {
                                zl.CustomQuery = "UPDATE Kasbon set Status=1, Approval=" + users.Apv + ", ApprovedDate1=getdate(), LastModifiedBy='" + users.UserName + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                                SqlDataReader sdr = zl.Retrieve();

                                string transID = string.Empty;
                                //foreach (RepeaterItem item in lstItemKasbon.Items)
                                for (int i = 0; i < lstItemKasbon.Items.Count; i++)
                                {
                                    //i = 0;
                                    //lstItemKasbon = ((Repeater)(objDetail.FindControl("lstItemKasbon")));
                                    //CheckBox cb = (item.FindControl("chk") as CheckBox);
                                    CheckBox cb = (CheckBox)lstItemKasbon.Items[i].FindControl("chk");
                                    //chk.Checked = chkAll.Checked;
                                    transID = cb.ToolTip;
                                    ZetroView zV = new ZetroView();
                                    zV.QueryType = Operation.CUSTOM;
                                    if (cb.Checked == true)
                                    {
                                        zV.CustomQuery = "update KasbonDetail set Status=0 where id=" + transID;
                                    }
                                    else
                                    {
                                        zV.CustomQuery = "update KasbonDetail set Status=-1 where id=" + transID;
                                    }
                                    SqlDataReader sdR = zV.Retrieve();
                                    //i++;
                                }
                            }
                            if (users.Apv == 1 && kasbon.Total <= 3000000)
                            {
                                zl.CustomQuery = "UPDATE Kasbon set Approval=" + users.Apv + ", ApprovedDate1=getdate(), LastModifiedBy='" + users.UserName + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                                SqlDataReader sdr = zl.Retrieve();

                                string transID = string.Empty;
                                for (int i = 0; i < lstItemKasbon.Items.Count; i++)
                                {
                                    CheckBox cb = (CheckBox)lstItemKasbon.Items[i].FindControl("chk");
                                    transID = cb.ToolTip;
                                    ZetroView zV = new ZetroView();
                                    zV.QueryType = Operation.CUSTOM;
                                    if (cb.Checked == true)
                                    {
                                        zV.CustomQuery = "update KasbonDetail set Status=0 where id=" + transID;
                                    }
                                    else
                                    {
                                        zV.CustomQuery = "update KasbonDetail set Status=-1 where id=" + transID;
                                    }
                                    SqlDataReader sdR = zV.Retrieve();
                                }
                            }
                            if (users.Apv == 2)
                            {
                                zl.CustomQuery = "UPDATE Kasbon set ApvMgr=1, ApprovedDateMgr1=getdate(), LastModifiedBy='" + users.UserName +
                                    "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                                SqlDataReader sdr = zl.Retrieve();

                                string transID = string.Empty;
                                for (int i = 0; i < lstItemKasbon.Items.Count; i++)
                                {
                                    CheckBox cb = (CheckBox)lstItemKasbon.Items[i].FindControl("chk");
                                    transID = cb.ToolTip;
                                    ZetroView zV = new ZetroView();
                                    zV.QueryType = Operation.CUSTOM;
                                    if (cb.Checked == true)
                                    {
                                        zV.CustomQuery = "update KasbonDetail set Status=0 where id=" + transID;
                                    }
                                    else
                                    {
                                        zV.CustomQuery = "update KasbonDetail set Status=-1 where id=" + transID;
                                    }
                                    SqlDataReader sdR = zV.Retrieve();
                                }
                            }
                        }

                        //if (users.Apv == 2)
                        ZetroView zl2 = new ZetroView();
                        zl2.QueryType = Operation.CUSTOM;
                        if (users.DeptID == 12)
                            zl2.CustomQuery = "UPDATE Kasbon set KasbonNo='" + no_kasbon + "', Approval=" + users.Apv + ", ApprovedDate2=getdate(), LastModifiedBy='" + users.UserName + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                        SqlDataReader sdr2 = zl2.Retrieve();

                        #region log approval process
                        //EventLogProcess mp = new EventLogProcess();
                        //EventLog evl = new EventLog();
                        //mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                        //mp.Pilihan = "Insert";
                        //evl.UserID = ((Users)Session["Users"]).ID;
                        //evl.AppLevel = ((Users)Session["Users"]).Apv;
                        //evl.DocNo = kasbon.NoPengajuan.ToString();
                        //evl.DocType = "Kasbon";
                        //evl.AppDate = DateTime.Now;
                        //evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                        //mp.EventLogInsert(evl);
                        #endregion
                    }

                    if (strError == string.Empty)
                    {
                        if (users.Apv < 2)
                        {
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

            Response.Redirect("KasbonApproval.aspx");
            //ApprovalKasbon();
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadOpenKasbon(txtCari.Text);
        }
        static public void DisplayAJAXMessage(Control page, string msg)
        {
            string myScript = "alert('" + msg + "');";
            ScriptManager.RegisterStartupScript(page, page.GetType(),
                "MyScript", myScript, true);
        }
        protected void lstItemKasbon_DataBound(object sender, RepeaterItemEventArgs e)
        {
            Users users = ((Users)Session["Users"]);
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                KasbonDetail kd = (KasbonDetail)e.Item.DataItem;
                CheckBox cb = (e.Item.FindControl("chk") as CheckBox);
                if (users.DeptID == 15 && users.Apv > 0)
                {
                    cb.Visible = true;
                }
                decimal tharga = kd.Qty * kd.EstimasiKasbon;
                gHarga += tharga;
                //  ((System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("dlv")).Attributes.Add("title", "SPP Approval date : ");
                ((Label)e.Item.FindControl("tHarga")).Text = tharga.ToString("###,##0.#0");
                ItemCD = kd.ItemCode;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //Repeater item = e.Item;
                KasbonDetail kdd = (KasbonDetail)e.Item.DataItem;
                DanaCadangan = Convert.ToDecimal(txtDanaCadangan.Text);
                //decimal DanaCadangan = (item.FindControl("tHarga") as Label).Text;
                ((Label)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                //nDisc = decimal.Parse(txtDiscount.Text);
                //gDisc = (nDisc / 100) * gHarga;
                //gDC = gHarga * (decimal.Parse(txtPPN.Text) / 100);
                gTotal = gHarga + DanaCadangan;
                //((Label)e.Item.FindControl("dsk")).Text = (nDisc > 0) ? nDisc.ToString("###.#0") + " %" : "";
                ((Label)e.Item.FindControl("gDC")).Text = DanaCadangan.ToString("###,##0.#0");
                //((Label)e.Item.FindControl("gDisc")).Text = gDisc.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("grnTotal")).Text = gTotal.ToString("###,##0.#0");
                //((Label)e.Item.FindControl("tDisc")).Text = (gHarga - gDisc).ToString("###,##0.#0");
                //((System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("tbDisc")).Visible = (nDisc > 0) ? true : false;
                //((System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("bDisc")).Visible = (nDisc > 0) ? true : false;

            }
        }


    }

    public class KasbonApprovalFacade : KasbonFacade
    {
        Kasbon objKasbon = new Kasbon();
        ArrayList arrData = new ArrayList();
        public string Criteria { get; set; }
        public string OrderBy { get; set; }
        public string GroupApp { get; set; }
        public string GroupApv { get; set; }

        public ArrayList RetrieveOpenKasbon()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT DISTINCT TOP 100 K.* " +
                          "FROM Kasbon K  " +
                          "LEFT JOIN KasbonDetail KD ON KD.KID=K.ID LEFT JOIN SPP AS s ON s.ID=kd.SPPID " +
                          "WHERE KD.Status>-1  and k.Status>-1 " + this.Criteria + this.GroupApp + this.GroupApv +
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


        public ArrayList RetrieveOpenKasbon2()
        {
            arrData = new ArrayList();
            string strSQL = "select * from ( " +
                            "SELECT TOP 100 k.ID,k.KasbonNo,k.NoPengajuan,k.DeptID,k.CreatedTime,k.Pic,k.status,k.Approval,k.TglKasbon,k.DanaCadangan, " +
                            "k.KasbonType,case when (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan = null then 0 " +
                            "else (sum(kd.Qty * kd.EstimasiKasbon))+k.DanaCadangan end as Total FROM Kasbon K  LEFT JOIN KasbonDetail KD ON KD.KID=K.ID " +
                            "LEFT JOIN SPP AS s ON s.ID=kd.SPPID WHERE K.Status=1 and KD.Status>-1 " + this.Criteria + this.GroupApp + this.GroupApv +
                            "group by k.ID,k.KasbonNo,k.NoPengajuan,k.DeptID,k.CreatedTime,k.Pic,k.DanaCadangan,k.status,k.Approval,k.TglKasbon, " +
                            "k.KasbonType " + this.OrderBy + " ) a where Total>3000000 ";
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

        public string GetAppGroup(int IDUsers)
        {
            string result = string.Empty;
            try
            {
                string strSQL = "Select AppGroup from UsersApp where RowStatus>-1 and UserID=" + IDUsers.ToString();
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = sdr["AppGroup"].ToString();
                    }
                }
                return result;
            }
            catch
            {
                return result;
            }
        }
        public Kasbon GetObject(SqlDataReader sdr, Kasbon objP)
        {
            objKasbon = (Kasbon)objP;
            objKasbon.NoPengajuan = sdr["NoPengajuan"].ToString();
            objKasbon.PIC = sdr["Pic"].ToString();
            objKasbon.DanaCadangan = Convert.ToDecimal(sdr["DanaCadangan"]);
            objKasbon.DeptID = Convert.ToInt32(sdr["DeptID"]);
            objKasbon.Apv = Convert.ToInt32(sdr["Approval"]);
            return objKasbon;
        }
    }
}