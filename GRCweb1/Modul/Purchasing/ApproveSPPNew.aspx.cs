using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BusinessFacade;
using Domain;

namespace GRCweb1.Modul.Purchasing
{
    public partial class ApproveSPPNew : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Global.link = "~/Default.aspx";
                LoadGroups(ddlGroupPurchn);
                LoadTipeBarang();
                if (((Users)Session["Users"]).Apv > 0)
                {
                    LoadOpenSPP();
                }
                //if (noSPP.Value != string.Empty)
                if (noSPP.Value != "0")
                {
                    string[] ListSPP = noSPP.Value.Split(',');
                    string[] ListOpenPO = ListSPP.Distinct().ToArray();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    LoadOpenSPP(ListOpenPO[0].ToString());
                    btnApprove.Enabled = true;
                    btnNext.Enabled = (ListOpenPO.Count() > 1) ? true : false;
                    ViewState["index"] = idx;
                }
                else
                {
                    btnApprove.Enabled = false;
                    btnNext.Enabled = false;
                }

                string[] ListOpenPOx = noSPP.Value.Split(',');
                string[] ListOpenPOd = ListOpenPOx.Distinct().ToArray();
                int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
                //btnNext.Enabled = ((idxx - 1) >= ListOpenPOd.Count()) ? false : true;
                ViewState["index"] = idxx;
                txtNotApproved.Attributes.Add("onkeyup", "onKeyUp()");
                if (Request.QueryString["NoSPP"] != null)
                {
                    LoadOpenSPP(Request.QueryString["NoSPP"].ToString());
                }
            }
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            string[] ListSPP = noSPP.Value.Split(',');
            string[] ListOpenPO = ListSPP.Distinct().ToArray();
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) - 1;
            btnPrev.Enabled = (idx > 0) ? true : false;
            btnNext.Enabled = ((idx + 1) == ListOpenPO.Count()) ? false : true;
            LoadOpenSPP(ListOpenPO[idx].ToString());
            ViewState["index"] = idx;
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                string[] ListSPP = noSPP.Value.Split(',');
                string[] ListOpenPO = ListSPP.Distinct().ToArray();
                int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
                //btnNext.Text += " " + idx.ToString();
                idx = (idx > ListOpenPO.Count() - 1) ? 0 : idx;
                btnNext.Enabled = ((idx) == ListOpenPO.Count()) ? false : true;
                try
                {
                    ViewState["index"] = idx;
                    LoadOpenSPP(ListOpenPO[idx].ToString());
                }
                catch
                {
                    LoadOpenSPP(ListOpenPO[0].ToString());
                    ViewState["index"] = 0;
                }
                btnPrev.Enabled = (idx > 0) ? true : false;
            }
            catch
            { }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            ApproveSPP();
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
                Response.Redirect("ApproveSPPNew.aspx");
            }
        }

        protected void btnNotApprove_Click(object sender, EventArgs e)
        {
            ApproveSPP(true);
        }
        protected void btnCari_Click(object sender, EventArgs e)
        {
            LoadOpenSPP(txtCari.Text.Trim());
        }
        private void LoadGroups(DropDownList ddl)
        {
            ArrayList arrGroups = new ArrayList();

            GroupsPurchnFacade groupsPurchnFacade = new GroupsPurchnFacade();
            arrGroups = groupsPurchnFacade.Retrieve();
            ddl.Items.Add(new ListItem("-- Pilih Group --", "0"));
            foreach (GroupsPurchn groupsPurchn in arrGroups)
            {
                ddl.Items.Add(new ListItem(groupsPurchn.GroupDescription, groupsPurchn.ID.ToString()));
            }
        }
        private void LoadTipeBarang()
        {
            ArrayList arrItemTypePurchn = new ArrayList();
            ItemTypePurchnFacade itemTypePurchnFacade = new ItemTypePurchnFacade();
            arrItemTypePurchn = itemTypePurchnFacade.Retrieve();
            ddlTipeBarang.Items.Add(new ListItem("-- Pilih Tipe Barang --", ""));
            foreach (ItemTypePurchn itemTypePurchn in arrItemTypePurchn)
            {
                ddlTipeBarang.Items.Add(new ListItem(itemTypePurchn.TypeDescription, itemTypePurchn.ID.ToString()));
            }
        }
        private void LoadOpenSPP()
        {
            Users user = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            SPPFacade spp = new SPPFacade();
            int headID = 0;
            int apvLevel = user.Apv - 1;
            #region old 
            //if (user.Apv >= 2)
            //{
            //    //headID = spp.GetHeadIDForMgrID(user.ID);
            //    //if (headID == user.ID)
            //    //    apvLevel = (user.Apv > 0) ? user.Apv - 2 : 0;
            //    //else
            //    //    apvLevel = (user.Apv > 0) ? user.Apv - 1 : 0;
            //    if (spp.isAuthHead(user.ID) == true)
            //    {

            //    }
            //}
            //else
            //{
            //    headID = spp.GetHeadIDForMgrID(user.ID);
            //    apvLevel = (user.Apv > 0) ? user.Apv - 1 : 0;
            //}
            #endregion
            /**
             * Added o 19-10-2016
             * User bisa aprove tanpa filter dept tetapi berdasarkan pengaturan di listuserhead
             * jika headID dan mgrID di table ListUserHead userid nya sama
             * maka status aproval langsung 2 and yng di panggil aproval status 0
             */
            if (spp.isAuthHead(user.ID) == true && spp.isAuthManager(user.ID) == true && spp.isAuthPM(user.ID) == true)
            {
                spp.Wherex = " AND (HeadID=" + user.ID + " AND MgrID=" + user.ID + " AND ManagerID=" + user.ID + ")";
                spp.Where = " AND (HeadID=" + user.ID + ")";
                spp.Criteria = " And ((Approval=0 AND UserID in(" + spp.isAuthHead(user.ID, true, true) + ")) " +
                           "OR (Approval = 1 AND HeadID in(" + spp.isAuthHead(user.ID, true) + ")) OR Approval >2 ) ";
            }
            else if (spp.isAuthHead(user.ID) == true && spp.isAuthManager(user.ID) == true && spp.isAuthPM(user.ID) == false)
            {
                spp.Where = " AND (HeadID=" + user.ID + " AND MgrID=" + user.ID + ")";
                spp.Wherex = " AND (MgrID=" + user.ID + ")";
                spp.Criteria = " And ((Approval=0 AND UserID in(" + spp.isAuthHead(user.ID, true, true) + ")) " +
                               "OR (Approval = 1 AND HeadID in(" + spp.isAuthHead(user.ID, true) + ")))";
            }
            else if (spp.isAuthHead(user.ID) == true && spp.isAuthManager(user.ID) == false && spp.isAuthPM(user.ID) == false)
            {
                spp.Where = " AND (HeadID=" + user.ID + ")";
                spp.Criteria = " And (Approval=0 AND UserID in(" + spp.isAuthHead(user.ID, true, true) + ")) ";
            }
            else if (spp.isAuthHead(user.ID) == false && spp.isAuthManager(user.ID) == true && spp.isAuthPM(user.ID) == false)
            {
                spp.Wherex = " AND ( MgrID=" + user.ID + ")";
                spp.Criteria = " AND (Approval = 1 AND HeadID in(" + spp.isAuthHead(user.ID, true) + "))";
            }
            else if (spp.isAuthHead(user.ID) == false && spp.isAuthManager(user.ID) == true && spp.isAuthPM(user.ID) == true)
            {
                spp.Where = " AND ( MgrID=" + user.ID + " AND ManagerID=" + user.ID + ")";
                spp.Wherex = " AND ( MgrID=" + user.ID + ")";
                spp.Criteria = " And ((Approval=1 AND HeadID in(" + spp.isAuthHead(user.ID, true) + ") OR Approval=2))";
            }
            else if (spp.isAuthHead(user.ID) == false && spp.isAuthManager(user.ID) == false && spp.isAuthPM(user.ID) == true)
            {
                spp.Where = " AND ( ManagerID=" + user.ID + ")";
                spp.Criteria = " And Approval=2 AND HeadID in(" + spp.isAuthHead(user.ID, false, true) + ")";
            }
            else
            {
                return;
            }
            //spp.Criteria = (UserIsHead() == true && UserIsManager(true) == true) ? " and Approval=0" : "";
            //spp.Criteria += (UserIsManager(false) == true) ? " and A.Approval=2" : "";
            //spp.Criteria += " AND Status>-1 ";
            arrData = spp.RetrieveForApproval(apvLevel);
            foreach (SPP sp in arrData)
            {
                if (sp.NoSPP != "")
                {
                    noSPP.Value += sp.NoSPP + ",";
                }
            }
            noSPP.Value = (noSPP.Value != string.Empty) ? noSPP.Value.Substring(0, (noSPP.Value.Length - 1)) : "0";
        }

        private void LoadOpenSPP(string SPPNum)
        {
            Users user = (Users)Session["Users"];
            ArrayList arrData = new ArrayList();
            SPPFacade spp = new SPPFacade();
            SPP sp = spp.RetrieveByNo(SPPNum);
            //if (txtCari.Text != "Find by Nomor SPP" && sp.NoSPP == string.Empty)
            if (sp.NoSPP == string.Empty || sp.Approval < 0)
            {
                sp = new SPP();
                DisplayAJAXMessage(this, "SPP tidak di temukan");
                //txtCari.Text = string.Empty;
                return;
            }
            if (sp.Approval == ((Users)Session["Users"]).Apv)
            {
                //DisplayAJAXMessage(this, "SPP Sudah di Approval");
                Response.Redirect("ApproveSPPNew.aspx");
                return;
            }
            if (sp.Approval < ((Users)Session["Users"]).Apv - 1)
            {
                if (((Users)Session["Users"]).Apv == 3)
                    DisplayAJAXMessage(this, "SPP belum approval Manager");
                if (((Users)Session["Users"]).Apv == 2)
                    DisplayAJAXMessage(this, "SPP belum approval Head");
                //Response.Redirect("ApproveSPPNew.aspx"); 
                return;
            }
            LoadTipeBarang();
            if (sp.NoSPP == string.Empty)
                return;
            txtNoSPP.Text = sp.NoSPP;
            txtTglSPP.Text = sp.Tanggal.ToString("dd-MMM-yyyy");
            ddlGroupPurchn.SelectedValue = sp.GroupID.ToString();
            ddlTypeSPP.SelectedValue = sp.PermintaanType.ToString();
            ddlTipeBarang.SelectedValue = (sp.ItemTypeID.ToString() != "") ? sp.ItemTypeID.ToString() : "0";

            SPPFacade Fspp = new SPPFacade();
            SPP Dspp = Fspp.RetrieveAliasUser(SPPNum);

            //txtUserName.Text = sp.UserName.ToString();
            //txtHeadName.Text = sp.HeadName.ToString();     
            txtUserName.Text = Dspp.UserAdmin.Trim();
            txtHeadName.Text = Dspp.UserHead.Trim();

            txtKirim.Text = sp.Minta.ToString("dd-MMM-yyyy");
            txtNotApproved.Text = sp.AlasanBatal.ToString();
            switch (sp.Approval)
            {
                case 0: txtStatus.Text = "Open"; break;
                case 1: txtStatus.Text = "Approval Head"; break;
                case 2: txtStatus.Text = "Approval Mgr"; break;
                case 3: txtStatus.Text = "Approval Plant Mgr"; break;
                case -1: txtStatus.Text = "Deleted"; break;
                default: txtStatus.Text = ""; break;
            }
            LoadDetailSPP(sp.ID);
        }

        protected void btnList_ServerClick(object sender, EventArgs e)
        {
            //Response.Redirect("ListSPP.aspx?approve=yes");
            Session["ListOfSPPDetail"] = null;
            Session["NoSPP"] = null;
            string[] ListSPP = noSPP.Value.Split(',');
            string[] ListOpenPO = ListSPP.Distinct().ToArray();
            string spp = string.Join(",", ListOpenPO);
            string sp = spp;
            Response.Redirect("ListSPPApproval.aspx?approve=" + (((Users)Session["Users"]).GroupID) + "&sp=" + sp);
        }
        private void LoadDetailSPP(int SPPID)
        {
            ArrayList arrData = new ArrayList();
            SPPDetailFacade spd = new SPPDetailFacade();
            arrData = spd.RetrieveBySPPID(SPPID);
            lstSPP.DataSource = arrData;
            lstSPP.DataBind();
        }
        protected void txtNotApproved_Change(object sender, EventArgs e)
        {
            btnApprove.Enabled = (txtNotApproved.Text.Length > 0) ? false : true;
            btnNotApprove.Enabled = (txtNotApproved.Text.Length > 0) ? true : false;
        }

        private void ApproveSPP()
        {
            Users users = (Users)Session["Users"];
            string UsName = users.UserName;
            int makeAPP = users.Apv;
            /* update tgl 18 Mei 2016 (Task Rakor)
             * Penambahan level approval menjadi 3 level (1.head,2.manager,3.PM) asalnya 2 level (1.Head/manager, 2.PM)
             * agar tidak banyak melakukan perubahan source code, update tanggal approve sbb:
             * ApproveDate1 = tgl. approve head
             * ApproveDate2 = tetap tgl. approve PM
             * ApproveDate3 = tgl. approve Manager
             */

            if (txtNoSPP.Text != string.Empty)
            {
                SPPFacade sPPFacade = new SPPFacade();
                SPP sPP = sPPFacade.RetrieveByNo(txtNoSPP.Text);
                if (sPPFacade.Error == string.Empty)
                {
                    sPP.Approval = makeAPP;
                    sPP.DepoID = users.UnitKerjaID;
                    sPP.HeadID = sPP.HeadID;
                    if (users.Apv == 1)
                        sPP.ApproveDate1 = DateTime.Now;
                    if (users.Apv == 2)
                        sPP.ApproveDate2 = DateTime.Now;
                    if (users.Apv == 3)
                        sPP.ApproveDate3 = DateTime.Now;


                    string strError = string.Empty;
                    ArrayList arrSPPDetail = new ArrayList();

                    SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(sPP, arrSPPDetail, new SPPNumber());

                    if (sPP.ID > 0)
                    {
                        strError = sPPProsessFacade.Update();
                        EventLogProcess mp = new EventLogProcess();
                        EventLog evl = new EventLog();
                        mp.Criteria = "UserID,DocNo,DocType,AppLevel,AppDate,IPAddress";
                        mp.Pilihan = "Insert";
                        evl.UserID = ((Users)Session["Users"]).ID;
                        evl.AppLevel = ((Users)Session["Users"]).Apv;
                        evl.DocNo = sPP.NoSPP.ToString();
                        evl.DocType = "SPP";
                        evl.AppDate = DateTime.Now;
                        evl.IPAddress = HttpContext.Current.Request.UserHostAddress.ToString();
                        mp.EventLogInsert(evl);

                    }
                    if (strError == string.Empty)
                    {
                        AutoNext();
                    }
                }
            }
        }
        private void ApproveSPP(bool NotApprove)
        {
            Users users = (Users)Session["Users"];
            string UsName = users.UserName;
            int makeAPP = users.Apv;


            if (txtNoSPP.Text != string.Empty)
            {
                SPPFacade sPPFacade = new SPPFacade();
                SPP sPP = sPPFacade.RetrieveByNo(txtNoSPP.Text);
                if (sPPFacade.Error == string.Empty)
                {
                    sPP.Approval = makeAPP;
                    sPP.DepoID = users.UnitKerjaID;
                    sPP.HeadID = sPP.HeadID;
                    sPP.Status = -2;
                    if (txtNotApproved.Text != string.Empty)
                    {
                        sPP.AlasanBatal = txtNotApproved.Text + " by :" + users.UserName;
                        txtNotApproved.Text = string.Empty;
                        btnApprove.Enabled = (txtNotApproved.Text.Length > 0) ? false : true;
                        btnNotApprove.Enabled = (txtNotApproved.Text.Length > 0) ? true : false;
                    }
                    else
                    {
                        DisplayAJAXMessage(this, "Alasan Not Approve tidak boleh kosong / harus di isi");
                        return;
                    }

                    if (users.Apv == 1)
                        sPP.ApproveDate1 = DateTime.Now;
                    if (users.Apv == 2)
                        sPP.ApproveDate2 = DateTime.Now;
                    if (users.Apv == 3)
                        sPP.ApproveDate3 = DateTime.Now;


                    string strError = string.Empty;
                    ArrayList arrSPPDetail = new ArrayList();

                    SPPProcessFacade sPPProsessFacade = new SPPProcessFacade(sPP, arrSPPDetail, new SPPNumber());

                    if (sPP.ID > 0)
                    {
                        strError = sPPProsessFacade.Update();
                    }

                    if (strError == string.Empty)
                    {
                        AutoNext();
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
        protected void lstSPP_DataBound(object sender, RepeaterItemEventArgs e)
        {
            SPPDetail spd = (SPPDetail)e.Item.DataItem;
            string[] UserPICView = new Inifiles(Server.MapPath("~/App_Data/PurchnConfig.ini")).Read("UserViewPICMinMax", "SPP").Split(',');
            var usr = Array.IndexOf(UserPICView, ((Users)Session["Users"]).UserName.ToString());
            ((Label)e.Item.FindControl("ket")).Text = (spd.ItemTypeID == 3) ? spd.Keterangan1.ToString() : spd.Keterangan;
            ((Label)e.Item.FindControl("itm")).ToolTip = CheckPICMinMax(spd.ItemID);
            ((Label)e.Item.FindControl("picMX")).Text = (usr > -1) ? CheckPICMinMax(spd.ItemID) : "";
            ((Label)e.Item.FindControl("picMX")).Visible = (usr > -1) ? true : false;
        }
        protected void lstSPP_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int intResult = 0;
            Users users = (Users)Session["Users"];
            string DetailID = e.CommandArgument.ToString();
            switch (e.CommandName)
            {
                case "hapus":
                    if (users.Apv > 0)
                    {
                        int index = Convert.ToInt32(e.CommandArgument);
                        SPPFacade spp = new SPPFacade();
                        SPP sp = spp.RetrieveByNo(txtNoSPP.Text);
                        POPurchnDetailEditFacade sPPDetailFacade = new POPurchnDetailEditFacade();
                        intResult = sPPDetailFacade.CancelSPPDetail(int.Parse(DetailID.ToString()));
                        LoadDetailSPP(sp.ID);
                    }
                    break;
            }
        }
        private string CheckPICMinMax(int ItemID)
        {
            string result = string.Empty;
            result = new SPPFacade().PICMinMax(ItemID);
            return result;
        }
        private bool UserIsHead()
        {
            bool result = false;
            int userid = ((Users)Session["Users"]).ID;
            ArrayList arrData = new ArrayList();
            SPPFacade sp = new SPPFacade();
            arrData = sp.ListUserHead(userid);
            result = (arrData.Count > 0) ? true : false;
            return result;
        }
        private bool UserIsManager(bool Manager)
        {
            bool result = false;
            int userid = ((Users)Session["Users"]).ID;
            ArrayList arrData = new ArrayList();
            SPPFacade sp = new SPPFacade();
            arrData = sp.ListUserHead(userid, Manager);
            result = (arrData.Count > 0) ? true : false;
            return result;
        }
    }
}