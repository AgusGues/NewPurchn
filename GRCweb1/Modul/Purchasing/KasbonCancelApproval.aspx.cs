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
    public partial class KasbonCancelApproval : System.Web.UI.Page
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
                if (noPengajuan.Value != string.Empty)
                {
                    string[] ListOpenKasbon = noPengajuan.Value.Split(',');
                    //btnApprove.Text += " " + ListOpenPO.Count().ToString();
                    int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
                    //LoadMUID();
                    LoadOpenKasbon(ListOpenKasbon[idx].ToString());
                    ViewState["index"] = idx;
                }
            }
            string[] ListOpenKasbonx = noPengajuan.Value.Split(',');
            int idxx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString());
            btnNext.Enabled = ((idxx) == ListOpenKasbonx.Count()) ? false : true;
            //txtNotApproved.Attributes.Add("onkeyup", "onKeyUp()");   
            if (Request.QueryString["PengajuanNo"] != null)
            {
                LoadOpenKasbon(Request.QueryString["PengajuanNo"].ToString());
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            //LoadOpenKasbon();
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
            //LoadOpenKasbon();
            string[] ListOpenKasbon = noPengajuan.Value.Split(',');
            int idx5 = int.Parse(ViewState["index"].ToString()) + 1;
            int idx = (ViewState["index"] == null) ? 0 : int.Parse(ViewState["index"].ToString()) + 1;
            btnNext.Enabled = ((idx) == ListOpenKasbon.Count()) ? false : true;
            btnPrev.Enabled = (idx > 0) ? true : false;
            try
            {
                if (idx5 == 1)
                {
                    int idx2 = 1;
                    ViewState["index"] = idx2;
                }
                else if (idx5 > 1)
                {
                    int idx2 = idx;
                    ViewState["index"] = idx2;
                }

                int idx3 = int.Parse(ViewState["index"].ToString());
                LoadOpenKasbon(ListOpenKasbon[idx3].ToString());
                ViewState["index"] = idx3;

            }
            catch
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
                LoadOpenKasbon(ListOpenKasbon[0].ToString());
                ViewState["index"] = 0;
            }
        }
        private void LoadOpenKasbon()
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
            KasbonCancelApprovalFacade kasbonApp = new KasbonCancelApprovalFacade();
            ArrayList arrKasbon = new ArrayList();

            string AppGroup = kasbonApp.GetAppGroup(users.ID);
            string AppPIC = kasbonFacade.GetAppPIC(users.ID, users.Apv);
            int TopUrgent = Convert.ToInt32(kasbonApp.GetTopUrgent());
            int Apv = (users.Apv > 2 && users.DeptID == 12) ? 2 : users.Apv;
            kasbonApp.Criteria = " and k.ApprovalCancel =" + (Apv - 1);
            kasbonApp.GroupApp = (AppPIC != string.Empty) ? " and k.Pic in(" + AppPIC + ")" : string.Empty;
            kasbonApp.TopUrgent = (TopUrgent == 0) ? " LEFT JOIN SPP AS s ON s.ID=kd.SPPID " : string.Empty;
            kasbonApp.OrderBy = (users.Apv > 2 && users.DeptID == 12) ? "Order By ID Desc" : " Order By K.NoPengajuan,ID ";
            arrKasbon = kasbonApp.RetrieveOpenKasbon();
            noPengajuan.Value = "";
            foreach (Kasbon kasbon in arrKasbon)
            {
                noPengajuan.Value += kasbon.NoPengajuan + ",";
            }
            noPengajuan.Value = (noPengajuan.Value != string.Empty) ? noPengajuan.Value.Substring(0, (noPengajuan.Value.Length - 1)) : "0";
        }
        //protected void txtNotApproved_Change(object sender, EventArgs e)
        //{
        //    btnApprove.Enabled = (txtNotApproved.Text.Length > 0) ? false : true;
        //    btnNotApprove.Enabled = (txtNotApproved.Text.Length > 0) ? true : false;
        //}
        private void LoadOpenKasbon(string NoPengajuan)
        {
            Users users = (Users)Session["Users"];
            KasbonCancelApprovalFacade kasbonApp = new KasbonCancelApprovalFacade();
            POPurchnFacade pOPurchnFacade = new POPurchnFacade();
            string AppGroup = pOPurchnFacade.GetAppGroup(users.ID);
            kasbonApp.Criteria = " and NoPengajuan='" + NoPengajuan + "'";
            pOPurchnFacade.GroupApp = (AppGroup != string.Empty) ? " and s.GroupID in(" + AppGroup + ")" : string.Empty;
            ArrayList arrKasbon = new ArrayList();
            arrKasbon = kasbonApp.RetrieveOpenKasbon();
            if (txtCari.Text != "Find by Nomor Kasbon" && arrKasbon.Count == 0)
            {
                DisplayAJAXMessage(this, "Nomor Pengajuan tidak ada / Nomor Pengajuan Salah / Nomor Pengajuan Sudah di approve");
                return;
            }
            int x;
            foreach (Kasbon kasbon in arrKasbon)
            {
                if (kasbon.Apv == users.Apv || kasbon.Apv < 0)
                {
                    if (txtCari.Text == kasbon.NoPengajuan)
                    {
                        DisplayAJAXMessage(this, "No Pengajuan Sudah di Approved");
                        return;
                    }
                    //arrKasbon.Add(kasbon);
                    x = arrKasbon.Count;
                    if (x > 1)
                    {
                        AutoNext();
                    }
                    else
                    {
                        Response.Redirect("KasbonApproval.aspx");
                    }
                }
                txtNoPengajuan.Text = NoPengajuan;
                txtTglKasbon.SelectedDate = Convert.ToDateTime(kasbon.KasbonDate.ToString("dd-MMM-yyyy"));
                txtDept.Text = "Purchasing";
                txtPic.Text = kasbon.PIC;
                txtDanaCadangan.Text = kasbon.DanaCadangan.ToString("###,##0.#0");
                txtStatus.Text = (kasbon.Apv == 0) ? "Open" : "Approved Head";
                if (kasbon.Status == -1)
                {
                    txtCancel.Visible = true;
                    txtCancel.Text = "Cancel (" + kasbon.AlasanNotApproval + ") ";
                }
                //txtNotApproved.Text = kasbon.AlasanNotApproval.ToString();
                if (kasbon.KasbonType == 0)
                {
                    LoadDetailKasbon(kasbon.ID);
                }
                else
                {
                    LoadDetailKasbon2(kasbon.ID);
                }
            }
        }
        private void LoadDetailKasbon(int KID)
        {
            KasbonDetailFacade kd = new KasbonDetailFacade();
            kd.Where = " and A.Status>-1";
            ArrayList arrData = new ArrayList();
            arrData = kd.RetrieveItemKID(KID);
            lstItemKasbon.DataSource = arrData;
            lstItemKasbon.DataBind();
        }
        private void LoadDetailKasbon2(int KID)
        {
            KasbonDetailFacade kd = new KasbonDetailFacade();
            ArrayList arrData = new ArrayList();
            arrData = kd.RetrieveItemKID2(KID);
            lstItemKasbon.DataSource = arrData;
            lstItemKasbon.DataBind();
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
                Response.Redirect("KasbonApproval.aspx");
            }
        }
        //protected void btnNotApprove_Click(object sender, EventArgs e)
        //{
        //    ApprovalKasbon(true);
        //}
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            Users users = (Users)Session["Users"];
            KasbonFacade kasbonFacade = new KasbonFacade();
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

                    if (Session["ListOfKasbonDetail"] != null)
                        arrKasbonDetail = (ArrayList)Session["ListOfKasbonDetail"];

                    if (kasbon.ID > 0)
                    {
                        ZetroView zl = new ZetroView();
                        zl.QueryType = Operation.CUSTOM;
                        //int i = 0;
                        if (users.Apv == 1)
                        {
                            zl.CustomQuery = "UPDATE Kasbon set ApprovalCancel=" + users.Apv + ", ApprovedDateCancel1=getdate(), LastModifiedBy='" + users.UserName + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                            SqlDataReader sdr = zl.Retrieve();
                            #region ceklis
                            //string transID = string.Empty;
                            ////foreach (RepeaterItem item in lstItemKasbon.Items)
                            //for (int i = 0; i < lstItemKasbon.Items.Count; i++)
                            //{
                            //    //i = 0;
                            //    //lstItemKasbon = ((Repeater)(objDetail.FindControl("lstItemKasbon")));
                            //    //CheckBox cb = (item.FindControl("chk") as CheckBox);
                            //    CheckBox cb = (CheckBox)lstItemKasbon.Items[i].FindControl("chk");
                            //    //chk.Checked = chkAll.Checked;
                            //    transID = cb.ToolTip;
                            //    ZetroView zV = new ZetroView();
                            //    zV.QueryType = Operation.CUSTOM;
                            //    if (cb.Checked == true)
                            //    {
                            //        zV.CustomQuery = "update KasbonDetail set Status=0 where id=" + transID;
                            //    }
                            //    else
                            //    {
                            //        zV.CustomQuery = "update KasbonDetail set Status=-1 where id=" + transID;
                            //    }
                            //    SqlDataReader sdR = zV.Retrieve();
                            //    //i++;
                            //}
                            #endregion
                        }

                        //if (users.Apv == 2)
                        ZetroView zl2 = new ZetroView();
                        zl2.QueryType = Operation.CUSTOM;
                        if (users.Apv == 2)
                            zl2.CustomQuery = "UPDATE Kasbon set ApprovalCancel=" + users.Apv + ", ApprovedDateCancel2=getdate(), LastModifiedBy='" + users.UserName + "', LastModifiedTime=getdate() WHERE ID=" + kasbon.ID + " ";
                        SqlDataReader sdr2 = zl2.Retrieve();
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

            //Response.Redirect("KasbonApproval.aspx");
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
                decimal tharga = kd.Qty * kd.EstimasiKasbon;
                gHarga += tharga;
                ((Label)e.Item.FindControl("tHarga")).Text = tharga.ToString("###,##0.#0");
                ItemCD = kd.ItemCode;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                KasbonDetail kdd = (KasbonDetail)e.Item.DataItem;
                DanaCadangan = Convert.ToDecimal(txtDanaCadangan.Text);
                ((Label)e.Item.FindControl("gTotal")).Text = gHarga.ToString("###,##0.#0");
                gTotal = gHarga + DanaCadangan;
                ((Label)e.Item.FindControl("gDC")).Text = DanaCadangan.ToString("###,##0.#0");
                ((Label)e.Item.FindControl("grnTotal")).Text = gTotal.ToString("###,##0.#0");
            }
        }

    }



    public class KasbonCancelApprovalFacade : KasbonFacade
    {
        Kasbon objKasbon = new Kasbon();
        ArrayList arrData = new ArrayList();
        public string Criteria { get; set; }
        public string OrderBy { get; set; }
        public string GroupApp { get; set; }
        public string TopUrgent { get; set; }

        public ArrayList RetrieveOpenKasbon()
        {
            arrData = new ArrayList();
            string strSQL = "SELECT DISTINCT TOP 100 K.* " +
                          "FROM Kasbon K  " +
                          "LEFT JOIN KasbonDetail KD ON KD.KID=K.ID " + this.TopUrgent +
                          "WHERE KD.Status>-1 and k.Status=-1 " + this.Criteria + this.GroupApp +
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
        public int GetTopUrgent()
        {
            int result = 0;
            try
            {
                string strSQL = "SELECT KasbonType FROM Kasbon WHERE Status=-1";
                DataAccess da = new DataAccess(Global.ConnectionString());
                SqlDataReader sdr = da.RetrieveDataByString(strSQL);
                if (da.Error == string.Empty && sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = Convert.ToInt32(sdr["KasbonType"].ToString());
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
            objKasbon.Apv = Convert.ToInt32(sdr["ApprovalCancel"]);
            objKasbon.Status = Convert.ToInt32(sdr["Status"]);
            objKasbon.AlasanNotApproval = sdr["AlasanNotApproved"].ToString();
            return objKasbon;
        }
    }
}